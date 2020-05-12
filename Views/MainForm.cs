using System;
using System.Windows.Forms;

namespace Survival.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            var gameForm = new GameForm();
            gameForm.Show();
            //Hide();
        }
    }
}