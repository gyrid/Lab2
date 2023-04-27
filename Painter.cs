using System.Windows.Forms;

namespace Lab2
{
    internal class Painter
    {
        int ID = 0;
        public Dictionary<int, Square> rects = new Dictionary<int, Square>();
        private List<Animator> animators = new List<Animator>();
        Thread? t0 = null;
        private BufferedGraphics bg;
        object locker = new object();
        Pen pen = new Pen(Color.Black);


        public Size ContainerSize { get; set; }
        private Graphics g;
        public Graphics G
        {
            get { return g; }
            set
            {
                g = value;
                //ContainerSize = g.ClipBounds.Size.ToSize();
                ContainerSize = g.VisibleClipBounds.Size.ToSize();
                Rectangle p = new Rectangle(new Point(0, 0), ContainerSize);
                bg = BufferedGraphicsManager.Current.Allocate(g, p);
            }
        }

        public Painter(Graphics g)
        {
            G = g;
            //bg = BufferedGraphicsManager.Current.Allocate(g, p);
        }

        public void addRectangle(MouseEventArgs e)
        {
            ID += 1;
            Square rect = new Square(e.X, e.Y, 30);
            rects[ID] = rect;
            var t = new Thread(() => addNewCircle(rect.X, rect.Y, ID, rect.Color));
            t.Start();
        }
        public void addRectangle(int x, int y)
        {
            ID += 1;
            Square rect = new Square(x, y, 30);
            rects[ID] = rect;
            var t = new Thread(() => addNewCircle(rect.X, rect.Y, ID, rect.Color));
            t.Start();
        }

        public void circlePaint(int x, int y, int id, Color color)
        {
            Circle c = new Circle(x, y, 20, id, color);
            Animator anim = new Animator(c, ContainerSize);
            animators.Add(anim);
        }

        public void addNewCircle(int x, int y, int id, Color color)
        {
            while (rects.ContainsKey(id))
            {
                Thread.Sleep(2500);
                circlePaint(x, y, id, color);
            }
        }

        public void show()
        {
            Thread t = new Thread(new ThreadStart(moving));
            t.Start();
        }

        public void moving()
        {

            while (true)
            {
                Random rndDead = new Random();
                int Number = rndDead.Next(1);
                int key;
                draw();

                int count = animators.Count;
                if (count > 0)
                {
                    for (int i = 0; i < animators.Count; i++)
                    {
                        if (animators[i] != null)
                        {
                            animators[i].Move();
                        }
                        for (int j = 0; j < animators.Count && i != j; j++)
                        {
                            if (Getdist(animators[i]._circle, animators[j]._circle) < animators[i]._circle.R * 2)
                            {
                                if (Number == 1)
                                {
                                    animators[i].Dead = true;
                                    key = animators[i]._circle.Id;
                                    if (rects.ContainsKey(key))
                                    {
                                        rects[key].Point += 1;
                                    }

                                }
                                else
                                {
                                    animators[j].Dead = true;
                                    key = animators[j]._circle.Id;
                                    if (rects.ContainsKey(key))
                                    {
                                        rects[key].Point += 1;
                                    }
                                }
                            }

                        }
                        if (AsAlive(animators[i]) == false)
                        {
                            try
                            {
                                Thread.CurrentThread.Abort();
                            }
                            catch
                            {

                            }
                        }
;
                    }
                }
            }

        }

        private bool AsAlive(Animator animator)
        {
            if (animator.Dead)
            {
                animators.Remove(animator);
                return false;
            }
            int x = animator._circle.X;
            int y = animator._circle.Y;
            int r = animator._circle.R;
            if (x + r < 0 || x - r > ContainerSize.Width)
            {
                animators.Remove(animator);
                return false;
            }
            if (y + r < 0 || y - r > ContainerSize.Height)
            {
                animators.Remove(animator);
                return false;
            }
            return true;
        }

        public int Getdist(Circle c1, Circle c2)
        {
            return Math.Abs(c1.X - c2.X) + Math.Abs(c1.Y - c2.Y);
        }


        public void draw()
        {
            var rndColor = new Random();
            lock (locker)
            {

                bg.Graphics.Clear(Color.White);
                foreach (var animator in animators.ToList())
                {
                    int r = animator._circle.R;
                    SolidBrush myBrush = new SolidBrush(animator._circle.Color);
                    bg.Graphics.FillEllipse(myBrush, animator._circle.X - r, animator._circle.Y - r, 2 * r, 2 * r);
                }
                foreach (var id in rects.Keys)
                {
                    var rect = rects[id];
                    if (rect.Point > 5)
                    {
                        rects.Remove(id);
                    }
                    else
                    {
                        SolidBrush myBrush = new SolidBrush(rect.Color);
                        //var r = new Rectangle(rect.X - rect.Side / 2, rect.Y - rect.Side / 2, rect.Side, rect.Side);
                        bg.Graphics.FillRectangle(myBrush, rect.X - rect.Side / 2, rect.Y - rect.Side / 2, rect.Side, rect.Side);
                        bg.Graphics.FillRectangle(new SolidBrush(Color.Red), rect.X - rect.Side / 8, rect.Y - rect.Side / 2, rect.Side / 7, Convert.ToInt32(rect.Side * (rect.Point / 5.0)));
                    }
                }
                bg.Render();
                Thread.Sleep(100);
            }
        }
    }
}
