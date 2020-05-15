using System;
using System.Windows.Forms;

namespace Survival.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            FormClosing += (sender, args) =>
            {
                var result = MessageBox.Show("Действительно закрыть?", "", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes) args.Cancel = true;
            };
            InitializeComponent();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            var gameForm = new GameForm1(this);
            gameForm.Show();
            Hide();
        }
    }
}