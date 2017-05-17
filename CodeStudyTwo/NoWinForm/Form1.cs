using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NoWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //MessageBox.Show("this");
            //if (MessageBox.Show("this") == DialogResult.OK)
            //{
            //    Application.Exit();
            //}
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            Timer t = new Timer();
            t.Tick += t_Tick;
            t.Interval = 10000;
            t.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Hide();
        }
    }
}
