using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project
{
    public partial class Form1 : Form
    {
        public int oldValue;
        private void PositionBee(ref int x, ref int y)
        {
            if (x> 10 && x <= 20)
            {
                y = 20;
                if(x - 10 == 1) x = 1;
                else x -= 10;
            }
            else if (x> 20 && x <= 30)
            {
                y = 40;
                if(x - 20 == 1) x = 1;
                else x -= 20;
            }
            else if (x> 30 && x <= 40)
            {
                y = 60;
                if(x - 30 == 1) x = 1;
                else x -= 30;
            }
            else if (x> 40 && x <= 50)
            {
                y = 80;
                if(x - 40 == 1) x = 1;
                else x -= 40;
            }
            else if (x> 50 && x <= 60)
            {
                y = 100;
                if(x - 50 == 1) x = 1;
                else x -= 50;
            }
            else if (x> 60 && x <= 70)
            {
                y = 120;
                if(x - 60 == 1) x = 1;
                else x -= 60;
            }
            else if (x> 70 && x <= 80)
            {
                y = 140;
                if(x - 70 == 1) x = 1;
                else x -= 70;
            }
            else if (x> 80 && x <= 90)
            {
                y = 160;
                if(x - 80 == 1) x = 1;
                else x -= 80;
            }
            else if (x> 90 && x <= 100)
            {
                y = 180;
                if(x - 90 == 1) x = 1;
                else x -= 90;
            }
        }
        private void PositionLabel(ref int x, ref int y, string number)
        {
            int num = int.Parse(number); 
            if (num % 2 == 0)
            {
                x = panel1.Width / 2;
            }
            if (num >= 3)
            {
                if (num % 2 != 0) y = y * num;
                else y = y * (num - 1);
            }
        }
        private void PositionHoney(ref int x, ref int y, string number)
        {
            var index = panel1.Controls.Find("Bee" + number, true);
            x = index[0].Right+2;
            y = index[0].Top;
        }
        private void PositionPriorityComboBox(ref int x, ref int y, string number)
        {
            var index = panel1.Controls.Find("Honey" + number, true);
            x = index[0].Right + 2;
            y = index[0].Top - 3;
        }
        private void DeliteElementlPanel(int number)
        {
            int index = panel1.Controls.IndexOfKey("Priority" + number);
            for (int i = index; i > index - 3; i --)
                panel1.Controls.RemoveAt(i);
        }
        private void CreatStatisticMenu(string number)
        {
            CreatLabelNameStatisticMenu(number);
            CreatLabelHoneyBoxMenu(number);
            CreatPriorityComboBox(number);
        }
        private void CreatImageBox(int count)
        {
            int x = count;
            int y = 0;
            PositionBee(ref x, ref y);
            var pb = new PictureBox();
            pb.Name = "Bee" + count;
            pb.Location = new Point(120+x*2, 35+y);
            pb.Size = new Size(20, 20);
            pb.Image = Image.FromFile("C:\\Users\\igor2\\OneDrive\\Рабочий стол\\ООП\\Практическая работа 15\\Project\\image\\pngtree-bee.jpg");
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            groupBox2.Controls.Add(pb);
        }
        private void CreatPriorityComboBox(string number)
        {
            int x = 10, y = 10;
            PositionPriorityComboBox(ref x, ref y, number);
            var cb = new ComboBox();
            cb.Size = new Size(120,10);
            cb.Location = new Point(x,y);
            cb.Name = "Priority" + number;
            cb.Items.AddRange(new string[]{"Самый низкий", "Ниже нормы", "Нормальный", "Выше нормы", "Самый высокий"});
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            cb.SelectedIndex = 2;
            panel1.Controls.Add(cb);
        }
        private void CreatLabelNameStatisticMenu(string number)
        {
            int x = 5, y = 10;
            PositionLabel(ref x,ref y, number);
            var lb = new Label();
            lb.AutoSize = true;
            lb.Location = new Point(x, y);
            lb.Name = "Bee"+number;
            lb.Text = "Пчела "+ number;
            panel1.Controls.Add(lb);
        }
        private void CreatLabelHoneyBoxMenu(string number)
        {
            int x = 10, y = 10;
            PositionHoney(ref x,ref y, number);
            var lb = new Label();
            lb.AutoSize = true;
            lb.Location = new Point(x, y);
            lb.Name = "Honey" + number;
            lb.Text = "0";
            lb.BackColor = SystemColors.Control;
            lb.BorderStyle = BorderStyle.None;
            lb.Font = new Font("Nirmala UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            panel1.Controls.Add(lb);
        }
    }
}