using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Animator
    {
        public Circle _circle;
        private int dx = -1, dy = 1;
        private Size containerSize;
        public bool Dead { get; set; } = false;
        public Animator(Circle circle, Size containerSize)
        {
            Random random = new Random();
            this._circle = circle;
            this.containerSize = containerSize;
            dx = random.Next(18) - 9;
            dy = (9 - Math.Abs(dx)) * Math.Sign(random.Next(10) - 20);
        }
        public void Move()
        {
            _circle.X += dx;
            _circle.Y += dy;
        }
    }
}
