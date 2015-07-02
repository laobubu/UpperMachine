using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.IO.Ports;
using WeifenLuo.WinFormsUI.Docking;

namespace UpperMachine
{
    public partial class frmMain : Form
    {
        //一些配置
        frmSerialConfig serialConfig;
        Dictionary<string, string> serialList;

        public string appPath { get { return _appPath; } }
        private string _appPath;

        //UI界面恢复
        string dockPanelConfig;

        //数据包解析器
        public HashSet<IPackageReader> packageReader;

        //控制小部件
        public HashSet<Widget> widget;

        //插件相关
        private struct pluginItem
        {
            public ToolStripMenuItem menuItem;
            public object widget;
            public string path;
        };
        private HashSet<pluginItem> widgetPlugin;
        private string widgetPluginConfig;

        public frmMain()
        {
            InitializeComponent();

            _appPath = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar;
            dockPanelConfig = Path.Combine(appPath, "DockPanel.config");
            widgetPluginConfig = Path.Combine(appPath, "plugin.config");

            //串口列表 刷新程序
            serialList = new Dictionary<string, string>();
            Timer serialPortRefresher = new Timer();
            serialPortRefresher.Interval = 500;
            serialPortRefresher.Enabled = true;
            serialPortRefresher.Tick += new EventHandler(serialPortRefresher_Tick);

            //PackageReader Manager
            packageReader = new HashSet<IPackageReader>();
            widget = new HashSet<Widget>();
            serialConfig = new frmSerialConfig();

            //载入自带 Widget
            //NOTICE: Native Widget shall be loaded here
            widget.Add(new WdgSender());

            //初始化插件系统，并载入已有插件
            InitPluginSys();

            if (File.Exists(dockPanelConfig))
            {
                try
                {
                    DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
                    dockPanel1.LoadFromXml(dockPanelConfig, m_deserializeDockContent);
                }
                catch (Exception err) {
                    Console.Error.WriteLine(err.ToString());
                }
            }

            foreach (Widget wi in widget)
            {
                wi.setMaster(this);
                if (wi.IsHidden)     wi.Show(dockPanel1);
            }
            ShowAllPlugins();
        }


        public SerialPort _serialPort { get { return serialPort1; } }
        object serialWriteLocker = new object();
        public bool serialWrite(byte[] data)
        {
            const int chunkSize = 64;
            if (!serialPort1.IsOpen) return false;
            lock (serialWriteLocker)
            {
                try
                {
                    for (int chunkS = 0; chunkS < data.Length; chunkS += chunkSize)
                    {
                        int toSendLen = chunkSize;
                        if (toSendLen + chunkS > data.Length)
                            toSendLen = data.Length - chunkS;
                        serialPort1.Write(data, chunkS, toSendLen);
                        RXTXCnt.TX += toSendLen;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
        public IAsyncResult serialWriteAsync(byte[] data, AsyncCallback callback = null)
        {
            Func<byte[], bool> func = serialWrite;

            return func.BeginInvoke(data, callback, null);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (serialPort1.IsOpen && serialPort1.BytesToRead > 0)
            {
                byte data = (byte)serialPort1.ReadByte();
                RXTXCnt.RX++;
                foreach (var i in packageReader)
                {
                    i.Feed(data);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                dockPanel1.SaveAsXml(dockPanelConfig);
            }
            catch (Exception er)
            {
                Console.Error.WriteLine("Cannot save dockPanel status!");
                Console.Error.WriteLine(er.ToString());
            }
            foreach (pluginItem fn in widgetPlugin) {
                try
                {
                    //TODO: PluginRemoveCallback
                    MethodInfo setMaster = fn.widget.GetType().GetMethod("Close");
                    setMaster.Invoke(fn.widget, new object[] { this });
                }
                catch (Exception) {
                    ((Widget)fn.widget).Close();
                }
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            foreach (Widget wi in widget)
            {
                if (wi.ID == persistString)
                    return wi;
            }
            foreach (pluginItem plugin in widgetPlugin)
            {
                MemberInfo[] mbi = plugin.widget.GetType().GetMember("ID");
                if (mbi[0].ToString() == persistString)
                    return (Widget)plugin.widget;
            }
            return null;
        }

        #region 连接相关(Connect Menu and Serial Enumerator)

        void serialPortRefresher_Tick(object sender, EventArgs e)
        {
            连接ToolStripMenuItem.Text = serialPort1.IsOpen ? "断开连接" : "创建连接";
            string[] spTemp = SerialPort.GetPortNames();
            if (serialList.Keys.Count != spTemp.Count())
            {
                lock (serialList)
                {
                    serialList.Clear();
                    try
                    {
                        ManagementObjectSearcher searcher =
                            new ManagementObjectSearcher("root\\CIMV2",
                            "SELECT * FROM Win32_PnPEntity");

                        foreach (ManagementObject queryObj in searcher.Get())
                        {
                            Match ct = Regex.Match(queryObj["Caption"].ToString(), @"^(.*?)\s*\((COM\d+)\)$");
                            if (ct.Length > 0)
                                serialList.Add(ct.Groups[2].Value, ct.Groups[0].Value);

                        }
                    }
                    catch (ManagementException)
                    {
                        foreach (string comNameT in SerialPort.GetPortNames())
                        {
                            ToolStripMenuItem comItem = new ToolStripMenuItem();
                            string comName = Regex.Match(comNameT, @"COM\d+").Captures[0].Value;
                            serialList.Add(comName, comName);
                        }
                    }
                }
            }
        }

        private void 连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                连接ToolStripMenuItem.Text = "创建连接";
            }
        }

        void comItem_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = (string)((ToolStripMenuItem)sender).Tag;
                serialPort1.BaudRate = serialConfig.BaudRate;
                serialPort1.Open();
                RXTXCounter.PerformClick();
                连接ToolStripMenuItem.Text = "断开连接";
            }
        }

        private void ToolStripMenuItem_SerialConfig_Click(object sender, EventArgs e)
        {
            serialConfig.ShowDialog();
        }

        private void 连接ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            while (连接ToolStripMenuItem.DropDownItems.Count > 2)
            {
                连接ToolStripMenuItem.DropDownItems.RemoveAt(0);
            }

            连接ToolStripMenuItem.DropDownItems[0].Visible = !serialPort1.IsOpen;
            连接ToolStripMenuItem.DropDownItems[1].Visible = !serialPort1.IsOpen;
            if (!serialPort1.IsOpen)
            {
                lock (serialList)
                {
                    foreach (KeyValuePair<string, string> kvp in serialList)
                    {
                        ToolStripMenuItem tsmi = new ToolStripMenuItem();
                        tsmi.Tag = kvp.Key;
                        tsmi.Text = kvp.Value;
                        tsmi.Click += comItem_Click;
                        连接ToolStripMenuItem.DropDownItems.Insert(0,tsmi);
                    }
                }
            }
        }

#endregion

        #region 插件系统(Plugin Loader and Selector)

        private void ToolStripMenuItemLoadPlugin_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = @"插件(*.dll)|*.dll|所有文件(*.*)|*";
            ofd.ShowDialog();

            LoadPlugin(ofd.FileName, true);
            UpdatePluginConfig();
            ShowAllPlugins();
        }

        private void LoadPlugin(string FileName, bool alerting = false)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(FileName);
                Type[] types = assembly.GetTypes();

                foreach (Type t in types)
                {
                    if (t.BaseType != null && t.BaseType.FullName == typeof(Widget).FullName)
                    {
                        bool repeated = false;

                        foreach (pluginItem plugin in widgetPlugin)
                        {
                            if (plugin.widget.GetType().FullName == t.FullName)
                            {
                                if (alerting)
                                    MessageBox.Show(
                                        String.Format("插件 {0} 已经存在，不可以重复载入！", t.FullName)
                                        );
                                repeated = true;
                                continue;
                            }
                        }

                        if (repeated) continue;

                        Object wi = System.Activator.CreateInstance(t);
                        MethodInfo setMaster = t.GetMethod("setMaster");
                        setMaster.Invoke(wi, new object[] { this });

                        pluginItem pi = new pluginItem();
                        pi.widget = wi;
                        pi.path = FileName;
                        pi.menuItem = new ToolStripMenuItem(((Widget)wi).Text);

                        widgetPlugin.Add(pi);

                        pi.menuItem.Tag = pi;
                        pi.menuItem.Checked = true;
                        pi.menuItem.Click += new EventHandler(pluginMenuItem_Click);
                        插件ToolStripMenuItem.DropDownItems.Insert(0, pi.menuItem);
                    }
                }
            }
            catch (Exception) { }
        }

        void pluginMenuItem_Click(object sender, EventArgs e)
        {
            unloadPlugin(((pluginItem)((ToolStripMenuItem)sender).Tag).widget);
        }

        public void unloadPlugin(object pluginWidget)
        {
            foreach (var fn in widgetPlugin)
            {
                if (pluginWidget == fn.widget)
                {
                    widgetPlugin.Remove(fn);

                    try
                    {
                        //TODO: PluginRemoveCallback
                        MethodInfo setMaster = fn.widget.GetType().GetMethod("Close");
                        setMaster.Invoke(fn.widget, new object[] { this });
                    }
                    catch (Exception)
                    {
                        ((Widget)fn.widget).Close();
                    }

                    插件ToolStripMenuItem.DropDownItems.Remove(fn.menuItem);

                    break;
                }
            }

            UpdatePluginConfig();
        }

        private void InitPluginSys()
        {
            widgetPlugin = new HashSet<pluginItem>();
            if (File.Exists(widgetPluginConfig))
            {
                FileStream fs = File.OpenRead(widgetPluginConfig);
                byte[] buff = new byte[fs.Length];
                fs.Read(buff, 0, buff.Length);
                fs.Close();
                string[] pluginList = Encoding.Default.GetString(buff).Split('\n');
                foreach (string i in pluginList)
                {
                    LoadPlugin(i);
                }
            }
        }

        private void UpdatePluginConfig()
        {
            var fs = File.CreateText(widgetPluginConfig);
            foreach(pluginItem fn in widgetPlugin){
                fs.WriteLine(Utils.MakeRelative(fn.path, appPath));
            }
            fs.Close();
        }

        private void ShowAllPlugins()
        {
            foreach (pluginItem plugin in widgetPlugin)
            {
                Widget wi = ((Widget)plugin.widget);
                if (wi.IsHidden)
                    wi.Show(dockPanel1);
            }
        }
#endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout abt = new frmAbout();
            abt.ShowDialog(this);
        }

        struct RXTXCntType
        {
            public int RX, TX;
        };
        RXTXCntType RXTXCnt;
        private void UpdateRXTXCounter()
        {
            RXTXCounter.Text = string.Format("Rx{0} Tx{1}", RXTXCnt.RX, RXTXCnt.TX);
        }
        private void RXTXCounter_Click(object sender, EventArgs e)
        {
            RXTXCnt.RX = 0;
            RXTXCnt.TX = 0;
            UpdateRXTXCounter();
        }

        private void timerRXTXUpdater_Tick(object sender, EventArgs e)
        {
            /*
            if (serialPort1.IsOpen != true)
                return;
            int t1 = serialPort1.BytesToRead;
            int t2 = serialPort1.BytesToWrite;
            if (t1 > RXTXCnt.lastBufIn)
            {
                RXTXCnt.RX += t1 - RXTXCnt.lastBufIn;
            }
            if (t2 > RXTXCnt.lastBufOut)
            {
                RXTXCnt.TX += t2 - RXTXCnt.lastBufOut;
            }
            RXTXCnt.lastBufIn = t1;
            RXTXCnt.lastBufOut = t2;
            UpdateRXTXCounter();
            */
            UpdateRXTXCounter();
        }
    }
}
