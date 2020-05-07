using System;
using System.Windows.Forms;

namespace Survival.Views
{
    public partial class Start : UserControl
    {
        private Game game;

        public Start()
        {
            InitializeComponent();
        }

        public void Configure(Game game)
        {
            if (this.game != null)
                return;

            this.game = game;

            //startButton.Click += StartButton_Click;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            //game.Start("2*2");
        }
    }
}