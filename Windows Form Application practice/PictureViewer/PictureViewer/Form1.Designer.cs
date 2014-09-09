namespace PictureViewer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox_Strech = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button_ShowPicture = new System.Windows.Forms.Button();
            this.button_ClearPicture = new System.Windows.Forms.Button();
            this.button_SetBackgroundColor = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_Strech, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(531, 316);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox1, 2);
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(525, 278);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox_Strech
            // 
            this.checkBox_Strech.AutoSize = true;
            this.checkBox_Strech.Location = new System.Drawing.Point(3, 287);
            this.checkBox_Strech.Name = "checkBox_Strech";
            this.checkBox_Strech.Size = new System.Drawing.Size(57, 17);
            this.checkBox_Strech.TabIndex = 1;
            this.checkBox_Strech.Text = "Strech";
            this.checkBox_Strech.UseVisualStyleBackColor = true;
            this.checkBox_Strech.CheckedChanged += new System.EventHandler(this.checkBox_Strech_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button_ShowPicture);
            this.flowLayoutPanel1.Controls.Add(this.button_ClearPicture);
            this.flowLayoutPanel1.Controls.Add(this.button_SetBackgroundColor);
            this.flowLayoutPanel1.Controls.Add(this.button_Close);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(82, 287);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(446, 26);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // button_ShowPicture
            // 
            this.button_ShowPicture.AutoSize = true;
            this.button_ShowPicture.Location = new System.Drawing.Point(355, 3);
            this.button_ShowPicture.Name = "button_ShowPicture";
            this.button_ShowPicture.Size = new System.Drawing.Size(88, 23);
            this.button_ShowPicture.TabIndex = 0;
            this.button_ShowPicture.Text = "Show a picture";
            this.button_ShowPicture.UseVisualStyleBackColor = true;
            this.button_ShowPicture.Click += new System.EventHandler(this.button_ShowPicture_Click);
            // 
            // button_ClearPicture
            // 
            this.button_ClearPicture.AutoSize = true;
            this.button_ClearPicture.Location = new System.Drawing.Point(264, 3);
            this.button_ClearPicture.Name = "button_ClearPicture";
            this.button_ClearPicture.Size = new System.Drawing.Size(85, 23);
            this.button_ClearPicture.TabIndex = 1;
            this.button_ClearPicture.Text = "Clear a picture";
            this.button_ClearPicture.UseVisualStyleBackColor = true;
            this.button_ClearPicture.Click += new System.EventHandler(this.button_ClearPicture_Click);
            // 
            // button_SetBackgroundColor
            // 
            this.button_SetBackgroundColor.AutoSize = true;
            this.button_SetBackgroundColor.Location = new System.Drawing.Point(121, 3);
            this.button_SetBackgroundColor.Name = "button_SetBackgroundColor";
            this.button_SetBackgroundColor.Size = new System.Drawing.Size(137, 23);
            this.button_SetBackgroundColor.TabIndex = 2;
            this.button_SetBackgroundColor.Text = "Set the background color";
            this.button_SetBackgroundColor.UseVisualStyleBackColor = true;
            this.button_SetBackgroundColor.Click += new System.EventHandler(this.button_SetBackgroundColor_Click);
            // 
            // button_Close
            // 
            this.button_Close.AutoSize = true;
            this.button_Close.Location = new System.Drawing.Point(40, 3);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 3;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All file" +
    "s (*.*)|*.*";
            this.openFileDialog1.Title = "Select a picture file";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 316);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Picture Viewer";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox_Strech;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button_ShowPicture;
        private System.Windows.Forms.Button button_ClearPicture;
        private System.Windows.Forms.Button button_SetBackgroundColor;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

