using System;
using System.Windows.Forms;

namespace Survival.Views
{
    public partial class MainForm : Form
    {
        private Game game;
        private readonly Timer timer;

        public MainForm()
        {
            InitializeComponent();
            Game.Start(4, 4);
        }
    }
}