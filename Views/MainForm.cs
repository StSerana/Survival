using System;
using System.Windows.Forms;

namespace Survival.Views
{
    public partial class MainForm : Form
    {
        private Game game;
        public MainForm()
        {
            InitializeComponent();
            
            game = new Game();

            ShowStartScreen();
        }

        private void ShowStartScreen()
        {
            HideScreens();
            Start.Configure(game);
            Start.Show();
        }
        
        private void HideScreens()
        {
            Start.Hide();
            //arrangingControl.Hide();
           // battleControl.Hide();
            //finishedControl.Hide();
        }
    }
}