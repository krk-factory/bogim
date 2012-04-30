namespace BOGIm
{
    partial class PodajIloscKlasHistogramuG
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
            this.histInfoLabel = new System.Windows.Forms.Label();
            this.potwierdzButton = new System.Windows.Forms.Button();
            this.ileKlasTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // histInfoLabel
            // 
            this.histInfoLabel.AutoSize = true;
            this.histInfoLabel.Location = new System.Drawing.Point(33, 9);
            this.histInfoLabel.Name = "histInfoLabel";
            this.histInfoLabel.Size = new System.Drawing.Size(172, 26);
            this.histInfoLabel.TabIndex = 2;
            this.histInfoLabel.Text = "Podaj ilość klas, dla których będzie\r\nwyrównywany histogram";
            // 
            // potwierdzButton
            // 
            this.potwierdzButton.Location = new System.Drawing.Point(147, 41);
            this.potwierdzButton.Name = "potwierdzButton";
            this.potwierdzButton.Size = new System.Drawing.Size(75, 23);
            this.potwierdzButton.TabIndex = 4;
            this.potwierdzButton.Text = "OK";
            this.potwierdzButton.UseVisualStyleBackColor = true;
            this.potwierdzButton.Click += new System.EventHandler(this.potwierdzButton_Click);
            // 
            // ileKlasTextBox
            // 
            this.ileKlasTextBox.Location = new System.Drawing.Point(12, 41);
            this.ileKlasTextBox.Name = "ileKlasTextBox";
            this.ileKlasTextBox.Size = new System.Drawing.Size(105, 20);
            this.ileKlasTextBox.TabIndex = 3;
            // 
            // PodajIloscKlasHistogramuG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 72);
            this.ControlBox = false;
            this.Controls.Add(this.potwierdzButton);
            this.Controls.Add(this.ileKlasTextBox);
            this.Controls.Add(this.histInfoLabel);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(250, 110);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(250, 110);
            this.Name = "PodajIloscKlasHistogramuG";
            this.ShowIcon = false;
            this.Text = "Podaj ilość klas - histogram globalny";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label histInfoLabel;
        private System.Windows.Forms.Button potwierdzButton;
        private System.Windows.Forms.TextBox ileKlasTextBox;
    }
}