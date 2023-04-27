using System.Reflection;

namespace Lab2
{
    public partial class Form1 : Form
    {
        Painter painter;
        public Form1()
        {
            InitializeComponent();
            painter = new Painter(pictureBox.CreateGraphics());
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            painter.addRectangle(e);
            painter.show();
        }

    }
}