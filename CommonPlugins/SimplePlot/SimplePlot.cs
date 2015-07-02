using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using UpperMachine;
using NPlot;

namespace UpperMachine
{
    public partial class SimplePlot : Widget
    {
        public PRFixLength packageReader;

        const int channelLength = 1024;
        const int channelCount = 4;
        Color[] channelColor = new Color[]{
            Color.Red, Color.Orange, Color.DarkGreen, Color.DarkCyan, 
            Color.Blue, Color.Purple, Color.Pink, Color.Black, Color.Gray
        };

        int currentDataPos = 0;

        float[][] plotData = new float[channelCount][];
        StepPlot[] line = new StepPlot[channelCount];
        ToolStripTextBox[] readBox = new ToolStripTextBox[channelCount];
        ToolStripMenuItem[] channelCheckbox = new ToolStripMenuItem[channelCount];

        public SimplePlot()
        {
            InitializeComponent();
            packageReader = new PRFixLength(new byte[] { 0xAA, 0xEE }, 4 * channelCount);
            packageReader.PackReceived += new EventHandler<PackageEventArgs>(packageReader_PackReceived);

            for (int i = 0; i < channelCount; i++)
            {
                plotData[i] = new float[channelLength];
                plotData[i][0] = 100;
                plotData[i][1] = -100;

                line[i] = new StepPlot();
                line[i].Pen = new Pen(channelColor[i], 1);
                line[i].OrdinateData = plotData[i];
                line[i].HideVerticalSegments = false;

                readBox[i] = new ToolStripTextBox();
                readBox[i].ForeColor = channelColor[i];
                readBox[i].Text = "0.0";

                channelCheckbox[i] = new ToolStripMenuItem();
                channelCheckbox[i].Checked = true;
                channelCheckbox[i].Text = "通道\t" + i;
                channelCheckbox[i].Tag = i;
                channelCheckbox[i].Click += new EventHandler(ChannelToggleClick);

                toolStripDropDownChannel.DropDownItems.Add(channelCheckbox[i]);
                toolStrip1.Items.Add(readBox[i]);
                plotSurface2D1.Add(line[i]);
            }

            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());

            NPlot.Windows.PlotSurface2D.Interactions.AxisDrag mw = new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true);
            mw.Sensitivity = 100;
            plotSurface2D1.AddInteraction(mw);
            plotSurface2D1.Update();
        }

        void ChannelToggleClick(object sender, EventArgs e)
        {
            int i = (int)((ToolStripMenuItem)sender).Tag;
            bool newstatus = !channelCheckbox[i].Checked;

            if (newstatus)
                plotSurface2D1.Add(line[i]);
            else
                plotSurface2D1.Remove(line[i], true);
            readBox[i].Visible = newstatus;
            channelCheckbox[i].Checked = newstatus;
        }

        void packageReader_PackReceived(object sender, PackageEventArgs e)
        {
            if (toolStripButtonLock.Checked) return;

            if (currentDataPos >= channelLength)
            {
                if (toolStripButtonLoop.Checked)
                    currentDataPos = 0;
                else
                    return;
            }

            for (int i = 0; i < channelCount; i++)
            {
                plotData[i][currentDataPos] = BitConverter.ToSingle(e.ByteData, i * 4);
            }

            currentDataPos++;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            plotSurface2D1.Refresh();
            for (int i = 0; i < channelCount; i++)
            {
                readBox[i].Text = plotData[i][(currentDataPos == 0) ? 0 : (currentDataPos-1)].ToString();
            }
        }

        private void toolStripButtonLock_Click(object sender, EventArgs e)
        {
            toolStripButtonLock.Checked = !toolStripButtonLock.Checked;
        }

        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            currentDataPos = 0;
            for (int i = 0; i < channelCount; i++)
                for (int j = 0; j < channelLength; j++)
                    plotData[i][j] = 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            float max=-1000, min=1000;
            for (int i = 0; i < channelCount; i++)
            {
                if (channelCheckbox[i].Checked == false)
                    continue;
                for (int j = 0; j < channelLength; j++)
                {
                    if (plotData[i][j] > max) max = plotData[i][j];
                    if (plotData[i][j] < min) min = plotData[i][j];
                }
            }
            plotSurface2D1.XAxis1.WorldMin = 0;
            plotSurface2D1.XAxis1.WorldMax = channelLength - 1;
            plotSurface2D1.YAxis1.WorldMin = min-0.5;
            plotSurface2D1.YAxis1.WorldMax = max+0.5;
            plotSurface2D1.Update();
        }

        private void toolStripButtonLoop_Click(object sender, EventArgs e)
        {
            toolStripButtonLoop.Checked = !toolStripButtonLoop.Checked;
        }

        public override void setMaster(frmMain master)
        {
            master.packageReader.Add(packageReader);
        }
    }
}
