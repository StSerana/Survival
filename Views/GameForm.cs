using System;
using System.Drawing;
using System.Windows.Forms;

namespace Survival.Views
{
    public partial class GameForm : Form
    {
        private readonly Game game;
        public GameForm(MainForm mainForm)
        {
            game = new Game();
            game.Start(5,5);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            var soundPlayer = new WMPLib.WindowsMediaPlayer
            {
                URL = @"C:\Users\админ\Documents\sharp_projects\Survival\Resources\bgSound.mp3"
            };
           soundPlayer.controls.play();
           FormClosing += (sender, args) =>
            {
                mainForm.Show();
            };
            InitializeComponent();
        }


        private TableLayoutPanel DrawMap()
        {
            var map = new TableLayoutPanel {ColumnCount = game.MapHeight, RowCount = game.MapWidth, Height = (game.MapHeight + 1) * 100, Width = (game.MapWidth + 1) * 100, Name = "Map"};
            for (var x = 0; x < game.MapWidth; x++)
            {
                for (var y = 0; y < game.MapHeight; y++)
                {
                    var image = ResizeImage(GetImage(game.Map[x, y]), new Size(100,100));
                    var btn = new Button {Image = image, Height = 100, Width = 100};
                    var posX = x;
                    var posY = y;
                    btn.Click += (sender, args) =>
                    {
                        game.Human.X = posX;
                        game.Human.Y = posY;
                        game.Act(game.Human);
                        game.IsHumanTurn = false;
                        var humanMap = DrawMap();
                        Controls.Add(humanMap);
                        Controls.Remove(map);
                        if (game.IsOver.Item1) GetResult();
                        GetAITurn();
                        var aiMap = DrawMap();
                        Controls.Add(aiMap);
                        Controls.Remove(humanMap);
                        if (game.IsOver.Item1) GetResult();
                        game.IsHumanTurn = true;
                       /* game.Human.X = posX;
                        game.Human.Y = posY;
                        if (game.IsHumanTurn) return;
                        game.Act(game.Human);
                        game.IsHumanTurn = false;*/
                        
                    };
                    map.Controls.Add(btn); 
                }
            }
            return map;
        }

        private TableLayoutPanel GetAITurn()
        {
            ((Ai)game.Ai).GenerateRandomTurn(game);
            Text = $"AI position: ({game.Ai.X}, {game.Ai.Y})";
            game.Act(game.Ai);
            var map = DrawMap();
            game.IsHumanTurn = true;
            return map;
        }
        private void GetResult()
        {
            var text = game.IsOver.Item2 == game.Human ? "Вы победили!" : "Вы проиграли(";
            var result = MessageBox.Show(text, "", MessageBoxButtons.OK);
            if (result == DialogResult.OK) Close();
        }

        private static Image GetImage(ICell cell)
        {
            switch (cell)
            {
                case Wall _:
                    return Resources.Wall;
                case Bomb _:
                    return Resources.Bomb;
                case ColorCell colorCell:
                    switch (cstate: colorCell.State, color: colorCell.Color)
                    {
                        case (State.One, Color.Blue):
                            return Resources.BlueOne;
                        case (State.Two, Color.Blue):
                            return Resources.BlueTwo;
                        case (State.Three, Color.Blue):
                            return Resources.BlueThree;
                        case (State.One, Color.Red):
                            return Resources.RedOne;
                        case (State.Two, Color.Red):
                            return Resources.RedTwo;
                        case (State.Three, Color.Red):
                            return Resources.RedThree;
                    }

                    break;
            }

            return Resources.Empty;
        }

        private static Image ResizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            Controls.Clear();
            ClientSize = new Size((game.MapHeight + 1) * 100, (game.MapWidth + 1) * 100);
            Controls.Add(game.IsHumanTurn ? DrawMap() : GetAITurn());
            if(game.IsOver.Item1) GetResult();
        }
    }
}