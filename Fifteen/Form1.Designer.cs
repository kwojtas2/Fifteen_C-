namespace Fifteen
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
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.RandomizeButton = new System.Windows.Forms.Button();
            this.RandomStepsNUD = new System.Windows.Forms.NumericUpDown();
            this.gamer = new Fifteen.Gamer();
            ((System.ComponentModel.ISupportInitialize)(this.RandomStepsNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(13, 13);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(75, 23);
            this.LoadImageButton.TabIndex = 0;
            this.LoadImageButton.Text = "Load image";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // RandomizeButton
            // 
            this.RandomizeButton.Location = new System.Drawing.Point(94, 12);
            this.RandomizeButton.Name = "RandomizeButton";
            this.RandomizeButton.Size = new System.Drawing.Size(75, 23);
            this.RandomizeButton.TabIndex = 2;
            this.RandomizeButton.Text = "Shake";
            this.RandomizeButton.UseVisualStyleBackColor = true;
            this.RandomizeButton.Click += new System.EventHandler(this.RandomizeButton_Click);
            // 
            // RandomStepsNUD
            // 
            this.RandomStepsNUD.Location = new System.Drawing.Point(175, 12);
            this.RandomStepsNUD.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.RandomStepsNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RandomStepsNUD.Name = "RandomStepsNUD";
            this.RandomStepsNUD.Size = new System.Drawing.Size(47, 20);
            this.RandomStepsNUD.TabIndex = 3;
            this.RandomStepsNUD.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // gamer
            // 
            this.gamer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gamer.Location = new System.Drawing.Point(12, 41);
            this.gamer.Name = "gamer";
            this.gamer.Size = new System.Drawing.Size(704, 396);
            this.gamer.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 450);
            this.Controls.Add(this.RandomStepsNUD);
            this.Controls.Add(this.RandomizeButton);
            this.Controls.Add(this.gamer);
            this.Controls.Add(this.LoadImageButton);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.RandomStepsNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadImageButton;
        private Gamer gamer;
        private System.Windows.Forms.Button RandomizeButton;
        private System.Windows.Forms.NumericUpDown RandomStepsNUD;
    }
}

