namespace Project;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        buttonDone = new Button();
        buttonStop = new Button();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        textBox1 = new TextBox();
        textBox2 = new TextBox();
        textBox3 = new TextBox();
        groupBox1 = new GroupBox();
        groupBox2 = new GroupBox();
        ColBee = new NumericUpDown();
        pictureBox1 = new PictureBox();
        pictureBox2 = new PictureBox();
        progressBar1 = new ProgressBar();
        panel1 = new Panel();
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)ColBee).BeginInit();
        groupBox2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
        SuspendLayout();
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(ColBee);
        groupBox1.Controls.Add(textBox2);
        groupBox1.Controls.Add(textBox3);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(buttonDone);
        groupBox1.Controls.Add(buttonStop);
        groupBox1.Location = new Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(286, 111);
        groupBox1.TabIndex = 5;
        groupBox1.TabStop = false;
        groupBox1.Text = "groupBox1";
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(textBox1);
        groupBox2.Controls.Add(progressBar1);
        groupBox2.Controls.Add(pictureBox1);
        groupBox2.Controls.Add(pictureBox2);
        groupBox2.Location = new Point(14, 144);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(774, 294);
        groupBox2.TabIndex = 6;
        groupBox2.TabStop = false;
        groupBox2.Text = "groupBox2";
        // 
        // buttonDone
        // 
        buttonDone.Location = new Point(35, 82);
        buttonDone.Name = "buttonDone";
        buttonDone.Size = new Size(75, 23);
        buttonDone.TabIndex = 0;
        buttonDone.Text = "Применить";
        buttonDone.UseVisualStyleBackColor = true;
        buttonDone.Click += buttonDone_Click;
        //
        // buttonStop
        //
        buttonStop.Location = new Point(185, 82);
        buttonStop.Name = "buttonStop";
        buttonStop.Size = new Size(50, 23);
        buttonStop.TabIndex = 0;
        buttonStop.Text = "Стоп";
        buttonStop.UseVisualStyleBackColor = true;
        buttonStop.Click += buttonStop_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(6, 19);
        label1.Name = "label1";
        label1.Size = new Size(102, 15);
        label1.TabIndex = 1;
        label1.Text = "Количество пчел";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(6, 49);
        label2.Name = "label2";
        label2.Size = new Size(164, 15);
        label2.TabIndex = 2;
        label2.Text = "Скорость:";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(120, 49);
        label3.Name = "label3";
        label3.Size = new Size(164, 15);
        label3.TabIndex = 2;
        label3.Text = "Время(секундах):";
        // 
        // textBox1
        // 
        textBox1.BackColor = SystemColors.Control;
        textBox1.BorderStyle = BorderStyle.None;
        textBox1.Font = new Font("Nirmala UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        textBox1.Location = new Point(24, 223);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(73, 16);
        textBox1.TabIndex = 4;
        textBox1.Text = "000000";
        textBox1.TextAlign = HorizontalAlignment.Center;
        // 
        // textBox2
        // 
        textBox2.Location = new Point(label2.Right+3, 46);
        textBox2.Name = "textBox2";
        textBox2.Size = new Size(40, 23);
        textBox2.TabIndex = 4;
        textBox2.Text = "0";
        textBox2.TextAlign = HorizontalAlignment.Right;
        //
        // texBox3
        //
        textBox3.Location = new Point(label3.Right+3, 46);
        textBox3.Name = "textBox3";
        textBox3.Size = new Size(40,23);
        textBox3.TabIndex = 4;
        textBox3.Text = "0";
        textBox3.TextAlign = HorizontalAlignment.Right;
        // 
        // ColBee
        // 
        ColBee.Location = new Point(176, 19);
        ColBee.Name = "ColBee";
        ColBee.Size = new Size(81, 23);
        ColBee.TabIndex = 5;
        ColBee.ValueChanged += numericUpDown1_ValueChanged;
        // 
        // pictureBox1
        // 
        pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
        pictureBox1.Location = new Point(6, 78);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(115, 123);
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox1.TabIndex = 2;
        pictureBox1.TabStop = false;
        // 
        // pictureBox2
        // 
        pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
        pictureBox2.Location = new Point(543, 68);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new Size(231, 156);
        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox2.TabIndex = 1;
        pictureBox2.TabStop = false;
        // 
        // progressBar1
        // 
        progressBar1.Location = new Point(6, 207);
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(115, 10);
        progressBar1.TabIndex = 3;
        progressBar1.Value = 100;
        // 
        // panel1
        // 
        panel1.Location = new Point(300, 12);
        panel1.Name = "panel1";
        panel1.Size = new Size(490, 136);
        panel1.TabIndex = 7;
        panel1.AutoScroll = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(panel1);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Name = "Form1";
        Text = "Form1";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)ColBee).EndInit();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Button buttonDone;
    private Button buttonStop;
    private Label label1;
    private Label label2;
    private Label label3;
    private TextBox textBox1;
    private TextBox textBox2;
    private TextBox textBox3;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private NumericUpDown ColBee;
    private ProgressBar progressBar1;
    private Panel panel1;
}
