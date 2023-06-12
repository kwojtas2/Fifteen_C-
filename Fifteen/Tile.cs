using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace Fifteen
{
    internal class TilePoint
    {
        public enum Direction { Left, Right, Top, Bottom }

        public int x { get; set; }
        public int y { get; set; }

        public TilePoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public TilePoint(TilePoint p)
        {
            this.x = p.x;
            this.y = p.y;
        }
        public TilePoint GetWithModifier(Direction direction)
        {
            switch (direction)
            {
                case Direction.Top: return new TilePoint(x, y - 1);
                case Direction.Bottom: return new TilePoint(x, y + 1);
                case Direction.Left: return new TilePoint(x - 1, y);
                case Direction.Right: return new TilePoint(x + 1, y);
                default: return new TilePoint(x, y);
            }
        }
        public override string ToString() => $"{x}:{y}";
        public override bool Equals(object obj) => obj is TilePoint && ((TilePoint)obj).x == this.x && ((TilePoint)obj).y == this.y;
        public override int GetHashCode()
        {
            int hashCode = x.GetHashCode();
            hashCode = (hashCode * 123) ^ y.GetHashCode();
            return hashCode;
        }
    }
    internal class Tile
    {
        public Bitmap bitmap { get; private set; }
        public TilePoint currentLocation { get; private set; }
        public TilePoint originalLocation { get; private set; }

        public bool isEmpty { get; private set; }

        int gridSize;



        public Tile(Bitmap bitmap, int x, int y, int gridSize, bool isEmpty)
        {
            this.bitmap = bitmap;
            this.currentLocation = new TilePoint(x, y);
            this.gridSize = gridSize;
            this.originalLocation = new TilePoint(x, y);
            this.isEmpty = isEmpty;
        }

        public List<Tile> GetNeigbours(List<Tile> tiles)
        {
            List<Tile> neigbours = new List<Tile>();

            if (this.currentLocation.y > 0) // TOP
            {
                Tile n = tiles.GetByLocation(this.currentLocation.GetWithModifier(TilePoint.Direction.Top));
                if (n != null) neigbours.Add(n);
            }
            if (this.currentLocation.y < this.gridSize) // BOTTOM
            {
                Tile n = tiles.GetByLocation(this.currentLocation.GetWithModifier(TilePoint.Direction.Bottom));
                if (n != null) neigbours.Add(n);
            }
            if (this.currentLocation.x > 0) // LEFT
            {
                Tile n = tiles.GetByLocation(this.currentLocation.GetWithModifier(TilePoint.Direction.Left));
                if (n != null) neigbours.Add(n);
            }

            if (this.currentLocation.x < this.gridSize) // RIGHT
            {
                Tile n = tiles.GetByLocation(this.currentLocation.GetWithModifier(TilePoint.Direction.Right));
                if (n != null) neigbours.Add(n);
            }
            return neigbours;
        }

        public bool TileCanBeMovedToBlackTile(List<Tile> tiles)
        {
            if (this.currentLocation.y > 0) // TOP
            {
                Tile nTile = tiles.GetByLocation(this.currentLocation.GetWithModifier(TilePoint.Direction.Top));
                if (nTile != null && nTile.isEmpty) return true;
            }
            if (this.currentLocation.y < this.gridSize) // BOTTOM
            {
                Tile nTile = tiles.GetByLocation(this.currentLocation.GetWithModifier(TilePoint.Direction.Bottom));
                if (nTile != null && nTile.isEmpty) return true;
            }
            if (this.currentLocation.x > 0)  // LEFT 
            {
                Tile nTile = tiles.GetByLocation(this.currentLocation.GetWithModifier(TilePoint.Direction.Left));
                if (nTile != null && nTile.isEmpty) return true;
            }
            if (this.currentLocation.x < this.gridSize) // RIGHT
            {
                Tile nTile = tiles.GetByLocation(this.currentLocation.GetWithModifier(TilePoint.Direction.Right));
                if (nTile != null && nTile.isEmpty) return true;
            }
            return false;
        }
        public void SetLocation(TilePoint location) => this.currentLocation = new TilePoint(location);

        public override string ToString() => $"current location: {this.currentLocation.ToString()}, blk: {this.isEmpty}";
    }

    internal static class TileHelper
    {
        public static bool IsSolved(this List<Tile> tiles)
        {
            foreach (Tile tile in tiles) if (!tile.originalLocation.Equals(tile.currentLocation)) return false;
            return true;
        }
        public static Tile GetBlackTile(this List<Tile> tiles)
        {
            foreach (Tile t in tiles) if (t.isEmpty) return t;
            return null;
        }
        public static Tile GetByLocation(this List<Tile> tiles, TilePoint location)
        {
            foreach (Tile t in tiles) if (t.currentLocation.Equals(location)) return t;
            return null;
        }
        public static void Swap(Tile a, Tile b)
        {
            TilePoint tmpPoint = a.currentLocation;

            a.SetLocation(b.currentLocation);
            b.SetLocation(tmpPoint);
        }
    }
}
