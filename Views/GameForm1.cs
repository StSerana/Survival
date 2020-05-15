using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Survival.Views
{
    public partial class GameForm1 : Form
    {
        private readonly Game game;
        private TableLayoutPanel map;
        public GameForm1(MainForm  mainForm)
        {
            game = new Game();
            game.Start(5,5);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ClientSize = new Size((game.MapHeight + 1) * 100, (game.MapWidth + 1) * 100);
            var soundPlayer = new WMPLib.WindowsMediaPlayer
            {
                URL = @"C:\Users\админ\Documents\sharp_projects\Survival\Resources\bgSound.mp3"
            };
            soundPlayer.controls.play(); //это должно было фоновой музыкой,но случились баги
            DrawMap();
            InitializeComponent();
        }
        private void Start()
        {
            Controls.Remove(map);
            GetHumanTurn();
            GetAITurn();
            DrawMap();
            Controls.Add(map);
            if(game.IsOver.Item1) GetResult();
        }
        
        private void DrawMap()
        { 
            map = new TableLayoutPanel {ColumnCount = game.MapHeight, RowCount = game.MapWidth, Height = (game.MapHeight + 1) * 100, Width = (game.MapWidth + 1) * 100, Name = "Map"};
            for (var x = 0; x < game.MapWidth; x++)
            {
                for (var y = 0; y < game.MapHeight; y++)
                {
                    var image = ResizeImage(GetImage(game.Map[x, y]), new Size(100,100));
                    var posX = x;
                    var posY = y;
                    var box = new PictureBox
                    {
                        Image = image, Height = 100, Width = 100
                    };
                    box.Click += (sender, args) =>
                    {
                        game.Human.X = posX;
                        game.Human.Y = posY;
                        Start();
                    };
                    map.Controls.Add(box); 
                }
            }
        }

        private void GetAITurn()
        {
            ((Ai)game.Ai).GenerateRandomTurn(game);
            Text = $"AI position: ({game.Ai.X}, {game.Ai.Y})";
            game.Act(game.Ai);
        }

        private void GetHumanTurn()
        {
            game.Act(game.Human);
        }
        
        private void GetResult()
        {
            var text = game.IsOver.Item2 == game.Human ? "Вы победили!" : "Вы проиграли(";
            var result = MessageBox.Show(text, "", MessageBoxButtons.OK);
            var mainForm = new MainForm();
            if (result == DialogResult.OK) Close();
            mainForm.Show();
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

        private void GameForm1_Load(object sender, EventArgs e)
        {
            ClientSize = new Size((game.MapHeight + 1) * 100, (game.MapWidth + 1) * 100);
            Start();
        }
    }
}