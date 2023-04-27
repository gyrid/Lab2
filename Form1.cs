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
            var db = new Datebase();
            //db.Insert("Color", "123");
            dataGridView.ColumnCount = 2;
            dataGridView.Rows.Add(1, db.Select(1));
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            painter.addRectangle(e);
            painter.show();
        }

    }
}