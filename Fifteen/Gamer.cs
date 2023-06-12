using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Fifteen.Tile;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace Fifteen
{
    public class GameSolvedEventArgs : EventArgs
    {
        public int moves { get; private set; }

        public GameSolvedEventArgs(int moves)
        {
            this.moves = moves;
        }
    }

    public partial class Gamer : UserControl
    {
        static Font mainFont = new Font("Microsoft Sans Serif", 24);
        static SolidBrush mainFontBrush = new SolidBrush(Color.Black);
        static Pen blackPen = new Pen(Color.Black, 3);
        static string notLoaded = "Image not loaded!";

        Bitmap emptyBitmap;

        public bool loaded { get; private set; }
        public Bitmap originalImage { get; private set; }
        public int gridSize { get; private set; }
        List<Tile> tiles;

        Random r;


        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when game is being solved")]
        public event EventHandler GameSolved;

        int moves = 0;


        public Gamer()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.Click += Gamer_Click;
        }

        private void Gamer_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            if (args.Button == MouseButtons.Left && loaded)
            {
                // mouseXY to tile index
                int clickedCol = args.X / tiles[0].bitmap.Width;
                int clickedRow = args.Y / tiles[0].bitmap.Height;

                Tile clickedTile = tiles.GetByLocation(new TilePoint(clickedCol, clickedRow));

                Tile blackTile = tiles.GetBlackTile();

                if (!clickedTile.isEmpty && clickedTile.TileCanBeMovedToBlackTile(tiles))
                {
                    TileHelper.Swap(clickedTile, blackTile);
                    this.Invalidate();
                }
                moves++;

                if (tiles.IsSolved() && GameSolved != null) GameSolved.Invoke(this, new GameSolvedEventArgs(moves));
            }
        }

        public void RandomizeSwap()
        {
            Tile blackTile = tiles.GetBlackTile();
            List<Tile> neighbours = blackTile.GetNeigbours(tiles);
            int r = new Random().Next(0, neighbours.Count);

            TileHelper.Swap(blackTile, neighbours[r]);
            this.Invalidate();
        }

        string newImagepath;
        public void LoadImage(string path, int divider)
        {
            this.gridSize = divider;

            try
            {
                newImagepath = path;

                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += LoadImageWorker;
                bw.RunWorkerCompleted += LoadImageCompleted;
                bw.RunWorkerAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{e.Message}");
            }
        }

        private void LoadImageCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Width = originalImage.Width;
            this.Height = originalImage.Height;

            this.Invalidate();
            loaded = true;
            moves = 0;
        }

        private void LoadImageWorker(object sender, DoWorkEventArgs e)
        {
            tiles = new List<Tile>();
            originalImage = (Bitmap)Image.FromFile(newImagepath);

            int tileHeight = originalImage.Height / gridSize;
            int tileWidth = originalImage.Width / gridSize;

            emptyBitmap = new Bitmap(tileWidth, tileHeight);
            using (Graphics gfx = Graphics.FromImage(emptyBitmap))
            using (SolidBrush brush = new SolidBrush(Color.White)) gfx.FillRectangle(brush, 0, 0, tileWidth, tileHeight);

            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    Rectangle rect = new Rectangle(
                        x: x * tileWidth,
                        y: y * tileHeight,
                        width: tileWidth,
                        height: tileHeight
                    );
                    tiles.Add(new Tile(originalImage.Clone(rect, originalImage.PixelFormat), x, y, gridSize, x == gridSize - 1 && y == gridSize - 1));
                }
            }
        }

        private void Onpaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (loaded && tiles.Count > 0)
            {
                int tileWidth = tiles[0].bitmap.Width;
                int tileHeight = tiles[0].bitmap.Height;

                foreach(Tile tile in tiles)
                {
                    Point startPoint = new Point(tile.currentLocation.x * tileWidth, tile.currentLocation.y * tileHeight);
                    if (!tile.isEmpty) g.DrawImage(tile.bitmap, startPoint);
                }

                // draw fancy lines
                for(int i = 1; i < gridSize; i++)
                {
                    g.DrawLine(blackPen, i * tileWidth + 1, 0, i * tileWidth + 1, this.Height);
                    g.DrawLine(blackPen, 0, i * tileHeight + 1, this.Width, i * tileHeight + 1);
                }
            }
            else
            {
                SizeF textSize = g.MeasureString(notLoaded, mainFont);

                int x = (int)(this.Width / 2 - textSize.Width / 2);
                int y = (int)(this.Height / 2 - textSize.Height / 2);

                Rectangle textRect = new Rectangle(x, y, (int)textSize.Width, (int)textSize.Height);
                g.DrawString(notLoaded, mainFont, mainFontBrush, textRect);
                //g.DrawRectangle(new Pen(Color.Red), textRect);

            }
        }
    }
}
