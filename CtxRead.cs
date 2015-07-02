using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UpperMachine
{
    public partial class CtxRead : UserControl
    {
        public string Label { get { return label1.Text; } set { label1.Text = value; } }
        public object Value { set { textBox1.Text = value.ToString(); } }
        public bool Alarm { get { return textBox1.BackColor != Color.Yellow; } set { textBox1.BackColor = value ? Color.Yellow : SystemColors.Window; } }
        public bool ReadOnly { get { return textBox1.ReadOnly; } set { textBox1.ReadOnly = value; } }

        public CtxRead(string label)
        {
            InitializeComponent();
            Label = label;
        }
    }
}
