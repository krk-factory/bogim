namespace BOGIm
{
    partial class MainWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wczytajPlikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszObrazWynikowyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.korekcjaGammaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalneWyrownanieHistogramuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lokalneWyrownanieHistogramuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.koniecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.obrazWejsciowyPictureBox = new System.Windows.Forms.PictureBox();
            this.obrazWyjsciowyPictureBox = new System.Windows.Forms.PictureBox();
            this.obrazWejsciowyLabel = new System.Windows.Forms.Label();
            this.obrazWyjsciowyLabel = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.obrazWejsciowyPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.obrazWyjsciowyPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.pomocToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(573, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wczytajPlikToolStripMenuItem,
            this.zapiszObrazWynikowyToolStripMenuItem,
            this.toolStripSeparator1,
            this.korekcjaGammaToolStripMenuItem,
            this.globalneWyrownanieHistogramuToolStripMenuItem,
            this.lokalneWyrownanieHistogramuToolStripMenuItem,
            this.toolStripSeparator2,
            this.koniecToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // wczytajPlikToolStripMenuItem
            // 
            this.wczytajPlikToolStripMenuItem.Name = "wczytajPlikToolStripMenuItem";
            this.wczytajPlikToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.wczytajPlikToolStripMenuItem.Text = "Wczytaj obraz...";
            this.wczytajPlikToolStripMenuItem.Click += new System.EventHandler(this.wczytajPlikToolStripMenuItem_Click);
            // 
            // zapiszObrazWynikowyToolStripMenuItem
            // 
            this.zapiszObrazWynikowyToolStripMenuItem.Name = "zapiszObrazWynikowyToolStripMenuItem";
            this.zapiszObrazWynikowyToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.zapiszObrazWynikowyToolStripMenuItem.Text = "Zapisz obraz wynikowy";
            this.zapiszObrazWynikowyToolStripMenuItem.Click += new System.EventHandler(this.zapiszObrazWynikowyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(249, 6);
            // 
            // korekcjaGammaToolStripMenuItem
            // 
            this.korekcjaGammaToolStripMenuItem.Name = "korekcjaGammaToolStripMenuItem";
            this.korekcjaGammaToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.korekcjaGammaToolStripMenuItem.Text = "Korekcja gamma";
            this.korekcjaGammaToolStripMenuItem.Click += new System.EventHandler(this.korekcjaGammaToolStripMenuItem_Click);
            // 
            // globalneWyrownanieHistogramuToolStripMenuItem
            // 
            this.globalneWyrownanieHistogramuToolStripMenuItem.Name = "globalneWyrownanieHistogramuToolStripMenuItem";
            this.globalneWyrownanieHistogramuToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.globalneWyrownanieHistogramuToolStripMenuItem.Text = "Globalne wyrównanie histogramu";
            this.globalneWyrownanieHistogramuToolStripMenuItem.Click += new System.EventHandler(this.globalneWyrownanieHistogramuToolStripMenuItem_Click);
            // 
            // lokalneWyrownanieHistogramuToolStripMenuItem
            // 
            this.lokalneWyrownanieHistogramuToolStripMenuItem.Name = "lokalneWyrownanieHistogramuToolStripMenuItem";
            this.lokalneWyrownanieHistogramuToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.lokalneWyrownanieHistogramuToolStripMenuItem.Text = "Lokalne wyrównanie histogramu";
            this.lokalneWyrownanieHistogramuToolStripMenuItem.Click += new System.EventHandler(this.lokalneWyrownanieHistogramuToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(249, 6);
            // 
            // koniecToolStripMenuItem
            // 
            this.koniecToolStripMenuItem.Name = "koniecToolStripMenuItem";
            this.koniecToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.koniecToolStripMenuItem.Text = "Koniec";
            this.koniecToolStripMenuItem.Click += new System.EventHandler(this.koniecToolStripMenuItem_Click);
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oProgramieToolStripMenuItem});
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            // 
            // oProgramieToolStripMenuItem
            // 
            this.oProgramieToolStripMenuItem.Name = "oProgramieToolStripMenuItem";
            this.oProgramieToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.oProgramieToolStripMenuItem.Text = "O programie";
            this.oProgramieToolStripMenuItem.Click += new System.EventHandler(this.oProgramieToolStripMenuItem_Click);
            // 
            // obrazWejsciowyPictureBox
            // 
            this.obrazWejsciowyPictureBox.Location = new System.Drawing.Point(12, 57);
            this.obrazWejsciowyPictureBox.Name = "obrazWejsciowyPictureBox";
            this.obrazWejsciowyPictureBox.Size = new System.Drawing.Size(256, 256);
            this.obrazWejsciowyPictureBox.TabIndex = 1;
            this.obrazWejsciowyPictureBox.TabStop = false;
            // 
            // obrazWyjsciowyPictureBox
            // 
            this.obrazWyjsciowyPictureBox.Location = new System.Drawing.Point(305, 57);
            this.obrazWyjsciowyPictureBox.Name = "obrazWyjsciowyPictureBox";
            this.obrazWyjsciowyPictureBox.Size = new System.Drawing.Size(256, 256);
            this.obrazWyjsciowyPictureBox.TabIndex = 2;
            this.obrazWyjsciowyPictureBox.TabStop = false;
            // 
            // obrazWejsciowyLabel
            // 
            this.obrazWejsciowyLabel.AutoSize = true;
            this.obrazWejsciowyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.obrazWejsciowyLabel.Location = new System.Drawing.Point(80, 36);
            this.obrazWejsciowyLabel.Name = "obrazWejsciowyLabel";
            this.obrazWejsciowyLabel.Size = new System.Drawing.Size(121, 18);
            this.obrazWejsciowyLabel.TabIndex = 3;
            this.obrazWejsciowyLabel.Text = "Obraz wejściowy";
            // 
            // obrazWyjsciowyLabel
            // 
            this.obrazWyjsciowyLabel.AutoSize = true;
            this.obrazWyjsciowyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.obrazWyjsciowyLabel.Location = new System.Drawing.Point(376, 36);
            this.obrazWyjsciowyLabel.Name = "obrazWyjsciowyLabel";
            this.obrazWyjsciowyLabel.Size = new System.Drawing.Size(117, 18);
            this.obrazWyjsciowyLabel.TabIndex = 4;
            this.obrazWyjsciowyLabel.Text = "Obraz wynikowy";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(13, 320);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(255, 300);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "chart1";
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(305, 320);
            this.chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(256, 300);
            this.chart2.TabIndex = 6;
            this.chart2.Text = "chart2";
            this.chart2.Click += new System.EventHandler(this.chart2_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 638);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.obrazWyjsciowyLabel);
            this.Controls.Add(this.obrazWejsciowyLabel);
            this.Controls.Add(this.obrazWyjsciowyPictureBox);
            this.Controls.Add(this.obrazWejsciowyPictureBox);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "MainWindow";
            this.Text = "BOGIm";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.obrazWejsciowyPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.obrazWyjsciowyPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wczytajPlikToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem koniecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oProgramieToolStripMenuItem;
        private System.Windows.Forms.PictureBox obrazWejsciowyPictureBox;
        private System.Windows.Forms.PictureBox obrazWyjsciowyPictureBox;
        private System.Windows.Forms.ToolStripMenuItem korekcjaGammaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalneWyrownanieHistogramuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label obrazWejsciowyLabel;
        private System.Windows.Forms.Label obrazWyjsciowyLabel;
        private System.Windows.Forms.ToolStripMenuItem zapiszObrazWynikowyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lokalneWyrownanieHistogramuToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}

