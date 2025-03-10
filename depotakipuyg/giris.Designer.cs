namespace depotakipuyg
{
    partial class giris
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
            button2 = new Button();
            button3 = new Button();
            button1 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(191, 128);
            button2.Name = "button2";
            button2.Size = new Size(173, 23);
            button2.TabIndex = 1;
            button2.Text = "Müşteri Ekle";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(44, 35);
            button3.Name = "button3";
            button3.Size = new Size(173, 23);
            button3.TabIndex = 2;
            button3.Text = "Ürün Ekleme ve Düzenleme";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button1
            // 
            button1.Location = new Point(44, 64);
            button1.Name = "button1";
            button1.Size = new Size(173, 23);
            button1.TabIndex = 3;
            button1.Text = "Ürün Düzenleme Veya Silme";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button4
            // 
            button4.Location = new Point(191, 157);
            button4.Name = "button4";
            button4.Size = new Size(173, 23);
            button4.TabIndex = 4;
            button4.Text = "Alıcı Ekle";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // giris
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(407, 272);
            Controls.Add(button4);
            Controls.Add(button1);
            Controls.Add(button3);
            Controls.Add(button2);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "giris";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Giriş";
            ResumeLayout(false);

        }

        #endregion
        private Button button2;
        private Button button3;
        private Button button1;
        private Button button4;
    }
}