using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        Painter painter;
        Datebase db;
        public Form1()
        {
            InitializeComponent();
            db = new Datebase();
            dataGridView.ColumnCount = 2;
            painter = new Painter(pictureBox.CreateGraphics());
            painter.add += addRectangle;
            painter.change += ChangePoint;
           // painter.update += UpdateData;
            //db.Insert("Score", "123");
           // dataGridView.Rows.Add(1, db.Select(100));
        }
        void addRectangle(int id)
        {
            db.Insert("Score", "0");
            dataGridView.Rows.Add(id, 0);
        }
        void ChangePoint(int id, int point)
        {
            db.Update(id, point);
            dataGridView.Rows[id-1].SetValues(id, point);
        }
        void UpdateData()
        {
            dataGridView.Rows.Clear();
            for (int i = 0; i < Painter.ID; i++)
            {
                dataGridView.Rows.Add(i, db.Select(i+1));
            }
            Thread.Sleep(800);
        }


        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            painter.addRectangle(e);
            painter.show();
        }

    }
}