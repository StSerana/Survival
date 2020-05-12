using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Survival.Views
{
    public partial class GameForm : Form
    {
        private readonly Game game;
        public GameForm()
        {
            game = new Game();
            game.Start(5,5);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Controls.Add(DrawMap());
            ClientSize = new Size((game.MapHeight + 1) * 100, (game.MapWidth + 1) * 100);
            InitializeComponent();
        }

        private TableLayoutPanel DrawMap()
        {
            var layout = new TableLayoutPanel {ColumnCount = game.MapHeight, RowCount = game.MapWidth, Height = (game.MapHeight + 1) * 100, Width = (game.MapWidth + 1) * 100};
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
                        DrawMap();
                    };
                    layout.Controls.Add(btn); 
                }
            }
            return layout;
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
    }
}