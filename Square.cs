using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Square
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Side { get; set; }
        public int Id { get; set; }
        public int Point { get; set; }
        public Color Color { get; set; }
        public Square(int x, int y, int side)
        {
            var rndColor = new Random();
            X = x;
            Y = y;
            Side = side;
            Color = Color.FromArgb(rndColor.Next(255), rndColor.Next(255), rndColor.Next(255));
        }
    }
}
