namespace Math_Quiz
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label_Time = new System.Windows.Forms.Label();
            this.label_TimeLeft = new System.Windows.Forms.Label();
            this.label_PlusLeft = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_PlusRight = new System.Windows.Forms.Label();
            this.numericUpDown_Sum = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Difference = new System.Windows.Forms.NumericUpDown();
            this.label_MinusRight = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_MinusLeft = new System.Windows.Forms.Label();
            this.numericUpDown_Product = new System.Windows.Forms.NumericUpDown();
            this.label_TimesRight = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_TimesLeft = new System.Windows.Forms.Label();
            this.numericUpDown_Quotient = new System.Windows.Forms.NumericUpDown();
            this.label_DivideRight = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label_DivideLeft = new System.Windows.Forms.Label();
            this.button_Start = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Sum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Difference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Product)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Quotient)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Time
            // 
            this.label_Time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Time.Location = new System.Drawing.Point(272, 9);
            this.label_Time.Name = "label_Time";
            this.label_Time.Size = new System.Drawing.Size(200, 30);
            this.label_Time.TabIndex = 0;
            // 
            // label_TimeLeft
            // 
            this.label_TimeLeft.AutoSize = true;
            this.label_TimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TimeLeft.Location = new System.Drawing.Point(165, 10);
            this.label_TimeLeft.Name = "label_TimeLeft";
            this.label_TimeLeft.Size = new System.Drawing.Size(101, 25);
            this.label_TimeLeft.TabIndex = 1;
            this.label_TimeLeft.Text = "Time Left";
            // 
            // label_PlusLeft
            // 
            this.label_PlusLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_PlusLeft.Location = new System.Drawing.Point(62, 75);
            this.label_PlusLeft.Name = "label_PlusLeft";
            this.label_PlusLeft.Size = new System.Drawing.Size(60, 50);
            this.label_PlusLeft.TabIndex = 2;
            this.label_PlusLeft.Text = "?";
            this.label_PlusLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(128, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 50);
            this.label1.TabIndex = 3;
            this.label1.Text = "+";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(260, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 50);
            this.label2.TabIndex = 4;
            this.label2.Text = "=";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_PlusRight
            // 
            this.label_PlusRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_PlusRight.Location = new System.Drawing.Point(194, 75);
            this.label_PlusRight.Name = "label_PlusRight";
            this.label_PlusRight.Size = new System.Drawing.Size(60, 50);
            this.label_PlusRight.TabIndex = 5;
            this.label_PlusRight.Text = "?";
            this.label_PlusRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Sum
            // 
            this.numericUpDown_Sum.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_Sum.Location = new System.Drawing.Point(338, 84);
            this.numericUpDown_Sum.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_Sum.Name = "numericUpDown_Sum";
            this.numericUpDown_Sum.Size = new System.Drawing.Size(100, 35);
            this.numericUpDown_Sum.TabIndex = 2;
            this.numericUpDown_Sum.Enter += new System.EventHandler(this.answer_Enter);
            // 
            // numericUpDown_Difference
            // 
            this.numericUpDown_Difference.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_Difference.Location = new System.Drawing.Point(338, 144);
            this.numericUpDown_Difference.Name = "numericUpDown_Difference";
            this.numericUpDown_Difference.Size = new System.Drawing.Size(100, 35);
            this.numericUpDown_Difference.TabIndex = 3;
            this.numericUpDown_Difference.Enter += new System.EventHandler(this.answer_Enter);
            // 
            // label_MinusRight
            // 
            this.label_MinusRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_MinusRight.Location = new System.Drawing.Point(194, 135);
            this.label_MinusRight.Name = "label_MinusRight";
            this.label_MinusRight.Size = new System.Drawing.Size(60, 50);
            this.label_MinusRight.TabIndex = 10;
            this.label_MinusRight.Text = "?";
            this.label_MinusRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(260, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 50);
            this.label4.TabIndex = 9;
            this.label4.Text = "=";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(128, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 50);
            this.label5.TabIndex = 8;
            this.label5.Text = "-";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_MinusLeft
            // 
            this.label_MinusLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_MinusLeft.Location = new System.Drawing.Point(62, 135);
            this.label_MinusLeft.Name = "label_MinusLeft";
            this.label_MinusLeft.Size = new System.Drawing.Size(60, 50);
            this.label_MinusLeft.TabIndex = 7;
            this.label_MinusLeft.Text = "?";
            this.label_MinusLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Product
            // 
            this.numericUpDown_Product.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_Product.Location = new System.Drawing.Point(338, 208);
            this.numericUpDown_Product.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_Product.Name = "numericUpDown_Product";
            this.numericUpDown_Product.Size = new System.Drawing.Size(100, 35);
            this.numericUpDown_Product.TabIndex = 4;
            this.numericUpDown_Product.Enter += new System.EventHandler(this.answer_Enter);
            // 
            // label_TimesRight
            // 
            this.label_TimesRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TimesRight.Location = new System.Drawing.Point(194, 199);
            this.label_TimesRight.Name = "label_TimesRight";
            this.label_TimesRight.Size = new System.Drawing.Size(60, 50);
            this.label_TimesRight.TabIndex = 15;
            this.label_TimesRight.Text = "?";
            this.label_TimesRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(260, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 50);
            this.label6.TabIndex = 14;
            this.label6.Text = "=";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(128, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 50);
            this.label7.TabIndex = 13;
            this.label7.Text = "x";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_TimesLeft
            // 
            this.label_TimesLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TimesLeft.Location = new System.Drawing.Point(62, 199);
            this.label_TimesLeft.Name = "label_TimesLeft";
            this.label_TimesLeft.Size = new System.Drawing.Size(60, 50);
            this.label_TimesLeft.TabIndex = 12;
            this.label_TimesLeft.Text = "?";
            this.label_TimesLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Quotient
            // 
            this.numericUpDown_Quotient.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_Quotient.Location = new System.Drawing.Point(338, 269);
            this.numericUpDown_Quotient.Name = "numericUpDown_Quotient";
            this.numericUpDown_Quotient.Size = new System.Drawing.Size(100, 35);
            this.numericUpDown_Quotient.TabIndex = 5;
            this.numericUpDown_Quotient.Enter += new System.EventHandler(this.answer_Enter);
            // 
            // label_DivideRight
            // 
            this.label_DivideRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DivideRight.Location = new System.Drawing.Point(194, 260);
            this.label_DivideRight.Name = "label_DivideRight";
            this.label_DivideRight.Size = new System.Drawing.Size(60, 50);
            this.label_DivideRight.TabIndex = 20;
            this.label_DivideRight.Text = "?";
            this.label_DivideRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(260, 260);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 50);
            this.label10.TabIndex = 19;
            this.label10.Text = "=";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(128, 260);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 50);
            this.label11.TabIndex = 18;
            this.label11.Text = "÷";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_DivideLeft
            // 
            this.label_DivideLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DivideLeft.Location = new System.Drawing.Point(62, 260);
            this.label_DivideLeft.Name = "label_DivideLeft";
            this.label_DivideLeft.Size = new System.Drawing.Size(60, 50);
            this.label_DivideLeft.TabIndex = 17;
            this.label_DivideLeft.Text = "?";
            this.label_DivideLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_Start
            // 
            this.button_Start.AutoSize = true;
            this.button_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Start.Location = new System.Drawing.Point(157, 337);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(163, 34);
            this.button_Start.TabIndex = 1;
            this.button_Start.Text = "Start quiz";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 383);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.numericUpDown_Quotient);
            this.Controls.Add(this.label_DivideRight);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label_DivideLeft);
            this.Controls.Add(this.numericUpDown_Product);
            this.Controls.Add(this.label_TimesRight);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label_TimesLeft);
            this.Controls.Add(this.numericUpDown_Difference);
            this.Controls.Add(this.label_MinusRight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label_MinusLeft);
            this.Controls.Add(this.numericUpDown_Sum);
            this.Controls.Add(this.label_PlusRight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_PlusLeft);
            this.Controls.Add(this.label_TimeLeft);
            this.Controls.Add(this.label_Time);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Sum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Difference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Product)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Quotient)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Time;
        private System.Windows.Forms.Label label_TimeLeft;
        private System.Windows.Forms.Label label_PlusLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_PlusRight;
        private System.Windows.Forms.NumericUpDown numericUpDown_Sum;
        private System.Windows.Forms.NumericUpDown numericUpDown_Difference;
        private System.Windows.Forms.Label label_MinusRight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_MinusLeft;
        private System.Windows.Forms.NumericUpDown numericUpDown_Product;
        private System.Windows.Forms.Label label_TimesRight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_TimesLeft;
        private System.Windows.Forms.NumericUpDown numericUpDown_Quotient;
        private System.Windows.Forms.Label label_DivideRight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label_DivideLeft;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Timer timer1;
    }
}

