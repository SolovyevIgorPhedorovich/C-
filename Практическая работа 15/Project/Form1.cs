using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Project;
public delegate void PositionBee(int x ,int y, int index);

public partial class Form1 : Form
{
    List<Thread> thBee = new List<Thread>();
    public Form1()
    {
        InitializeComponent();
    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
        int newValue = Convert.ToInt32(Math.Round(ColBee.Value, 0)); 
        int index = groupBox2.Controls.IndexOfKey("Bee"+(newValue+1));
        if(index == -1)
        {
            for (int i = oldValue+1; i <= newValue; i++)
            {
                CreatImageBox(i);
                CreatStatisticMenu(i.ToString());
            }
            AddThread();
        }
        else if (index > -1)
            {
                source.Cancel();
                thBee[newValue].Join();
                for(int i = groupBox2.Controls.IndexOfKey("Bee"+oldValue), number = oldValue; i >= index;i--, number--)
                {
                    DeliteElementlPanel(number);
                    groupBox2.Controls.RemoveAt(i);
                }
                source.Dispose();
                source = new CancellationTokenSource();
                ReplaceList();
            }
        oldValue = newValue;
    }

    CancellationTokenSource source = new CancellationTokenSource();
    
    private void AddThread()
    {
        if (Run)
        {
            for (int i = thBee.Count; i < Convert.ToInt32(Math.Round(ColBee.Value, 0)); i++)
            {
                thBee.Add(new Thread(Moved));
                thBee[i].Name = "WorkingBee" + (i+1);
                thBee[i].Start(i);
            }
        }
    }

    private void ReplaceList()
    {
        thBee = new List<Thread>();
        for (int i = 0; i < Convert.ToInt32(Math.Round(ColBee.Value, 0)); i++)
            {
                thBee.Add(new Thread(Moved));
                thBee[i].Name = "WorkingBee" + (i+1);
            }
            foreach (var t in thBee)
                t.Start(thBee.IndexOf(t));
    }

    private void buttonDone_Click(object sender, EventArgs e)
    {
        if (!Run)
        {
            Run = true;
            Thread progresLive;
            Thread honeyEat;
            progresLive = new Thread(Live);
            progresLive.Name = "Live";
            honeyEat = new Thread(HoneyEat);
            honeyEat.Name = "Eat";        
            progresLive.Start();
            honeyEat.Start(); 
            ReplaceList();
        }
        //MyThread th = new MyThread();
        //th.StartProces();
        //th.ThreadWorkinBeegAdd(Convert.ToInt32(Math.Round(ColBee.Value, 0)));
        //th.ThreadWorkingBeeStart();
    }

    private void buttonStop_Click(object sender, EventArgs e)
    {
        Run = false;
    }

    private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboBox cbox = sender as ComboBox;
        if (cbox != null)
        {
            int number = int.Parse(Regex.Replace(cbox.Name,"[^\\d]+",""));
            PriorityBee(--number, cbox.Text);
        }
    }  
}
