﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        Brain brain;
        public Form1()
        {
            InitializeComponent();
            brain = new Brain(new ChangeTextDelegate(ChangeText));
        }
        private void ChangeText(string text)
        {
            textBox1.Text = text;
        }

        private void BtnClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            brain.Process(button.Text);
        }
    }
}
