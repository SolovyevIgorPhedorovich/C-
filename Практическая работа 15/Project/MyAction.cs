using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project
{
    public partial class Form1 : Form
    {
        private bool Run = false;
        private void PriorityBee(int number, string priority)
        {
            if (thBee.Count > number)
            {
                if (priority == "Самый низкий")
                    thBee[number].Priority = ThreadPriority.Lowest;
                else if (priority == "Ниже нормы")
                    thBee[number].Priority = ThreadPriority.BelowNormal;
                else if (priority == "Нормальный")
                    thBee[number].Priority = ThreadPriority.Normal;
                else if (priority == "Выше нормы")
                    thBee[number].Priority = ThreadPriority.AboveNormal;
                else if (priority == "Самый высокий")
                    thBee[number].Priority = ThreadPriority.Highest;
            }
        }

        public void Moved(object number)
        {
            Random rnd = new Random();
            int index = groupBox2.Controls.IndexOfKey("Bee" + ((int)number + 1));
            int start = (groupBox2.Controls.Find("PictureBox1", true)[0].Right) + 3;
            int end = (groupBox2.Controls.Find("PictureBox2", true)[0].Left) - 3;
            int honeyValue = 0;
            while(Run)
            {   
                if (source.Token.IsCancellationRequested)
                {
                    break;
                }
                while (groupBox2.Controls[index].Right < end && Run && !source.Token.IsCancellationRequested)
                {
                    int x = groupBox2.Controls[index].Left, y = groupBox2.Controls[index].Top;
                    x = x + 30;
                    if (x > end) x = end - groupBox2.Controls[index].Width;
                    if (InvokeRequired)
                        this.Invoke(new Action(()=>groupBox2.Controls[index].Location = new Point(x,y)));
                    Thread.Sleep(rnd.Next(300, 1001));
                }
                if (groupBox2.Controls[index].Right == end)
                {    
                    honeyValue = 5;
                    if (InvokeRequired)
                        this.Invoke(new Action(()=>panel1.Controls.Find("Honey"+ ((int)number + 1), true)[0].Text = honeyValue.ToString()));
                }
                while (groupBox2.Controls[index].Left > start && Run && !source.Token.IsCancellationRequested)
                {
                    int x = groupBox2.Controls[index].Left, y = groupBox2.Controls[index].Top;
                    x = x - 30;
                    if (x < start) x = start;
                    if (InvokeRequired)
                        this.Invoke(new Action(()=>groupBox2.Controls[index].Location = new Point(x,y)));
                    Thread.Sleep(rnd.Next(300, 1001));
                }
                if (groupBox2.Controls[index].Left == start)
                {
                    Thread t = new Thread(CounterHoney);
                    t.Start(honeyValue);
                    honeyValue = 0; 
                    if (InvokeRequired)
                        this.Invoke(new Action(()=>panel1.Controls.Find("Honey"+ ((int)number + 1), true)[0].Text = honeyValue.ToString()));
                }
            }
        }

        private void CounterHoney(object? counter)
        {
            if (counter is int c)
            while (c != 0)
            {
                if (textBox1.InvokeRequired)
                    textBox1.Invoke(new Action<TextBox>(textBox => textBox.Text = AddNumberStr(textBox.Text)), textBox1);
                --c;
                Thread.Sleep(100);
            }
        }

        private string AddNumberStr(string text)
        {
            int x = int.Parse(text) + 1;
            string result = "";
            if( 10 > x) result = "00000"+x;
            else if (100 > x) result = "0000" + x;
            else if (1000 > x) result = "000" + x;
            else if (10000 > x) result = "00" + x;
            else if (100000 > x) result = "0" + x;
            else if (1000000 > x) result = x.ToString();
            return result;
        }

        private string LessNumberStr(string text)
        {
            int x = int.Parse(text) - 1;
            string result = "";
            if( 10 > x) result = "00000"+x;
            else if (100 > x) result = "0000" + x;
            else if (1000 > x) result = "000" + x;
            else if (10000 > x) result = "00" + x;
            else if (100000 > x) result = "0" + x;
            else if (1000000 > x) result = x.ToString();
            return result;
        }

        public void HoneyEat()
        {
            while(Run)
            {
                int col = int.Parse(textBox2.Text);
                while(col != 0 && int.Parse(textBox1.Text) > 0)
                {
                    if (textBox1.InvokeRequired)
                    {
                        textBox1.Invoke(new Action<TextBox>(textBox => textBox.Text = LessNumberStr(textBox.Text)), textBox1);
                        --col;
                    }
                }
                Thread.Sleep((int.Parse(textBox3.Text))*1000);
            }
        }

        public void Live()
        {
            while(Run)
            {
                while (int.Parse(textBox1.Text) == 0 && progressBar1.Value > 0 && Run)
                {
                    if (progressBar1.InvokeRequired)
                        progressBar1.Invoke(new Action<ProgressBar>(progressBar => progressBar.Value -= 1), progressBar1);
                    Thread.Sleep((int.Parse(textBox3.Text)*1000)/100);
                }
            
                while (int.Parse(textBox1.Text) > 0 && progressBar1.Value < 100 && Run)
                {
                    if (progressBar1.InvokeRequired)
                        progressBar1.Invoke(new Action<ProgressBar>(progressBar => progressBar.Value += 1), progressBar1);
                    Thread.Sleep(300);
                }
            }
        }
    }
}