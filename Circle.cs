using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Circle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }
        public int Id { get; set; }
        public Color Color { get; set; }
        public Circle(int x, int y, int r, int id, Color color)
        {
            X = x;
            Y = y;
            R = r;
            Id = id;
            Color = color;
        }
    }
}
