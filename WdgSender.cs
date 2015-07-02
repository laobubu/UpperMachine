using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using WeifenLuo.WinFormsUI.Docking;

namespace UpperMachine
{
    public partial class WdgSender : Widget
    {
        frmMain master;
        PRQueueByte reader;
        Queue<byte> fifo = new Queue<byte>(4096);

        public WdgSender()
        {
            InitializeComponent();
            cbSendConfig.SelectedIndex = 2;

            reader = new PRQueueByte(fifo);
        }

        public override void setMaster(frmMain master)
        {
            this.master = master;
            master.packageReader.Add(reader);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] data;
            if (rbSendText.Checked)
            {
                data = Encoding.ASCII.GetBytes(txtSend.Text);
            }
            else
            {
                data = Utils.HexStr2Byte(txtSend.Text);
            }
            master.serialWrite(data);
        }

        private void txtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbSendConfig.SelectedIndex != 0 && e.KeyCode == Keys.Enter)
            {
                if ((cbSendConfig.SelectedIndex == 1) || (cbSendConfig.SelectedIndex == 2 && e.Control))
                {
                    btnSend.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.A && e.Control)
            {
                txtSend.SelectAll();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtRecv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                txtRecv.SelectAll();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        OpenFileDialog ofg1 = null;
        bool isSending = false;
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            if (null == ofg1)
            {
                ofg1 = new OpenFileDialog();
                ofg1.Filter = "所有文件|*";
            }
            if (isSending)
            {
                master._serialPort.DiscardOutBuffer();
                isSending = false;
            }
            else if (ofg1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                var f = File.OpenRead(ofg1.FileName);
                byte[] b = new byte[f.Length];
                f.Read(b, 0, b.Length);
                f.Close();
                isSending = true;
                master.serialWriteAsync(b, SendFileFin);
            }
        }
        private void SendFileFin(IAsyncResult iar)
        {
            isSending = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            string s1 = isSending ? "停止发送文件" : "发送文件";
            if (s1 != btnSendFile.Text) btnSendFile.Text = s1;

            StringBuilder sb = new StringBuilder();

            while (fifo.Count > 0)
            {
                var data = fifo.Dequeue();

                if (rbRecvHex.Checked)
                {
                    sb.AppendFormat(" {0:X2}", data);
                }
                else
                {
                    sb.Append((char)data);
                }
            }

            if (sb.Length > 0)
                txtRecv.AppendText(sb.ToString());

            timer1.Enabled = true;
        }

        private void WdgSender_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            reader.Enabled = false;
            master.packageReader.Remove(reader);
        }

        private void btnEmpty_Click(object sender, EventArgs e)
        {
            txtRecv.Clear();
        }

        private SaveFileDialog sfd = null;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (sfd == null)
            {
                sfd = new SaveFileDialog();
            }
            bool hex = rbRecvHex.Checked;
            if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                var fo = File.OpenWrite(sfd.FileName);
                if (hex)
                {
                    byte[] b = Utils.HexStr2Byte(txtRecv.Text);
                    fo.Write(b, 0, b.Length);
                }
                else
                {
                    StreamWriter sw = new StreamWriter(fo);
                    sw.Write(txtRecv.Text);
                    sw.Close();
                }
                fo.Close();
            }
        }
    }
}
