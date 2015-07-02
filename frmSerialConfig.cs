using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace UpperMachine
{
    public partial class frmSerialConfig : Form
    {
        public int BaudRate { get { return int.Parse(baudRate.Text); } }

        public frmSerialConfig()
        {
            InitializeComponent();
            baudRate.Text = "115200";
        }

        private void frmSerialConfig_Load(object sender, EventArgs e)
        {
        }
    }
}
