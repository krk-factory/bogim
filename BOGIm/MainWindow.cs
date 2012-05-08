using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace BOGIm
{    
    public partial class MainWindow : Form
    {     
        protected Bitmap obrazWejsciowy;    // ew. zrobić public, aby korzystać z niego wszędzie
        protected int wysokoscObrazka;
        protected int szerokoscObrazka;

        private double gammaWartosc;

        private int iloscKlasH;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void koniecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();

            a.ShowDialog();
        }

        private void wczytajPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sciezkaPliku;
            OpenFileDialog ofd = new OpenFileDialog();

            //ofd.InitialDirectory = @"C:\";
            ofd.Filter = "Pliki graficzne (*.jpg)|*.jpg|Pliki graficzne (*.gif)|*.gif|Pliki graficzne (*.tif)|*.tif|Wszystkie pliki (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            ofd.Title = "Otwórz plik...";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                sciezkaPliku = ofd.FileName;

                try
                {
                    obrazWejsciowy = new Bitmap(sciezkaPliku);

                    szerokoscObrazka = obrazWejsciowy.Size.Width;
                    wysokoscObrazka = obrazWejsciowy.Size.Height;

                    obrazWejsciowyPictureBox.Image = obrazWejsciowy;
                    obrazWyjsciowyPictureBox.Image = null;

                    chartHistoWe.Series["Series1"].Points.Clear();
                    chartHistoWy.Series["Series1"].Points.Clear();

                    udostepnianieOperacji(true);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd odczytu pliku. Błąd: " + ex.Message);
                }
            }
            else
            {
                udostepnianieOperacji(false);
            }

        }

        // TODO: nie działa jak powinno
        private void udostepnianieOperacji(bool b)
        {
            if (b)
            {
                korekcjaGammaToolStripMenuItem.Enabled = true;
                globalneWyrownanieHistogramuToolStripMenuItem.Enabled = true;
                lokalneWyrownanieHistogramuToolStripMenuItem.Enabled = true;
                zapiszObrazWynikowyToolStripMenuItem.Enabled = true;
            }
            else
            {
                korekcjaGammaToolStripMenuItem.Enabled = false;
                lokalneWyrownanieHistogramuToolStripMenuItem.Enabled = false;
                globalneWyrownanieHistogramuToolStripMenuItem.Enabled = false;
                zapiszObrazWynikowyToolStripMenuItem.Enabled = false;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            obrazWejsciowyPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            obrazWyjsciowyPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            korekcjaGammaToolStripMenuItem.Enabled = false;
            globalneWyrownanieHistogramuToolStripMenuItem.Enabled = false;
            lokalneWyrownanieHistogramuToolStripMenuItem.Enabled = false;
            zapiszObrazWynikowyToolStripMenuItem.Enabled = false;
        }

        private void korekcjaGammaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KorekcjaGamma kg = new KorekcjaGamma(obrazWejsciowy);

            // Nowa forma dla okienka do podania wartości gamma
            PodajWartoscGamma pdw = new PodajWartoscGamma();
            pdw.ShowDialog();

            if (PodajWartoscGamma.operacja == true)
            {
                gammaWartosc = PodajWartoscGamma.wartoscGamma;

                obrazWyjsciowyPictureBox.Image = kg.wykonajKorekcje(gammaWartosc);
            }
        }

        private void zapiszObrazWynikowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            //sfd.InitialDirectory = @"C:\";
            sfd.Filter = "Pliki graficzne (*.jpg)|*.jpg|Pliki graficzne (*.gif)|*.gif|Pliki graficzne (*.tif)|*.tif|Wszystkie pliki (*.*)|*.*";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;
            sfd.Title = "Zapisz plik...";

            sfd.ShowDialog();

            // Jeśli nie ma stringu z nazwą, nie zapisuj
            if (sfd.FileName != "")
            {
                try
                {
                    // Zapis przez FileStream stworzony przez metodę OpenFile
                    FileStream fs = (FileStream)sfd.OpenFile();
                    // Zapis pliku względem dostępnych formatów
                    switch (sfd.FilterIndex)
                    {
                        case 1:
                            obrazWyjsciowyPictureBox.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case 2:
                            obrazWyjsciowyPictureBox.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                            break;

                        case 3:
                            obrazWyjsciowyPictureBox.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Tiff);
                            break;
                    }

                    fs.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd!\n\n" + ex.Message);
                }
            }
        }

        private void globalneWyrownanieHistogramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Histogram h = new Histogram(obrazWejsciowy, this);

            PodajIloscKlasHistogramuG pikhg = new PodajIloscKlasHistogramuG();
            pikhg.ShowDialog();

            if (PodajIloscKlasHistogramuG.operacja == true)
            {
                iloscKlasH = PodajIloscKlasHistogramuG.iloscKlas;

                obrazWyjsciowyPictureBox.Image = h.wyrownajHistogramGlobalnie(iloscKlasH);
            }
        }

        private void lokalneWyrownanieHistogramuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
