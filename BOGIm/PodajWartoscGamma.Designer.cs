namespace BOGIm
{
    partial class PodajWartoscGamma
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
            this.gammaWartoscTextBox = new System.Windows.Forms.TextBox();
            this.gammaInfoLabel = new System.Windows.Forms.Label();
            this.potwierdzButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gammaWartoscTextBox
            // 
            this.gammaWartoscTextBox.Location = new System.Drawing.Point(12, 30);
            this.gammaWartoscTextBox.Name = "gammaWartoscTextBox";
            this.gammaWartoscTextBox.Size = new System.Drawing.Size(105, 20);
            this.gammaWartoscTextBox.TabIndex = 0;
            // 
            // gammaInfoLabel
            // 
            this.gammaInfoLabel.AutoSize = true;
            this.gammaInfoLabel.Location = new System.Drawing.Point(24, 9);
            this.gammaInfoLabel.Name = "gammaInfoLabel";
            this.gammaInfoLabel.Size = new System.Drawing.Size(188, 13);
            this.gammaInfoLabel.TabIndex = 1;
            this.gammaInfoLabel.Text = "Podaj wartość współczynnika gamma:";
            // 
            // potwierdzButton
            // 
            this.potwierdzButton.Location = new System.Drawing.Point(147, 30);
            this.potwierdzButton.Name = "potwierdzButton";
            this.potwierdzButton.Size = new System.Drawing.Size(75, 23);
            this.potwierdzButton.TabIndex = 2;
            this.potwierdzButton.Text = "OK";
            this.potwierdzButton.UseVisualStyleBackColor = true;
            this.potwierdzButton.Click += new System.EventHandler(this.potwierdzButton_Click);
            // 
            // PodajWartoscGamma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 62);
            this.ControlBox = false;
            this.Controls.Add(this.potwierdzButton);
            this.Controls.Add(this.gammaInfoLabel);
            this.Controls.Add(this.gammaWartoscTextBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(250, 100);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(250, 100);
            this.Name = "PodajWartoscGamma";
            this.ShowIcon = false;
            this.Text = "Współczynnik gamma";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox gammaWartoscTextBox;
        private System.Windows.Forms.Label gammaInfoLabel;
        private System.Windows.Forms.Button potwierdzButton;
    }
}