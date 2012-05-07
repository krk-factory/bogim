using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BOGIm
{    
    class Histogram
    {
        /* --- Pola Klasy --- */
        private Bitmap obrazWe;
        private Bitmap obrazWy;

        private double[] tablicaLUT;
        private double[] wartosciDystrybuanty;
        private int[] wartosciHistogramu;

        private int maxObraz;
        private int iloscKlas;

        public const int iloscOdcieniSzarosci = 256;


        /* --- Metody Klasy --- */
        public Histogram(Bitmap obraz)
        {
            this.obrazWe = obraz;

            obrazWy = new Bitmap(obrazWe.Width, obrazWe.Height);

            wartosciDystrybuanty = new double[iloscOdcieniSzarosci];
            tablicaLUT = new double[iloscOdcieniSzarosci];

            wartosciHistogramu = new int[iloscOdcieniSzarosci];
        }

        public Bitmap wyrownajHistogramLokalnie()
        {


            return obrazWy;
        }


        public Bitmap wyrownajHistogramGlobalnie(int iloscKlas)
        {
        }

        public Bitmap wyrownajHistogramGlobalnie(int iloscKlas)
        {
            this.iloscKlas = iloscKlas;
            
            Color kolor;

            int wartoscPoOperacji;

            wyznaczMaksimum();
            obliczHistogram();        
            wypelnijLUT();

            for (int k1 = 0; k1 < obrazWe.Height; k1++)
            {
                for (int k2 = 0; k2 < obrazWe.Width; k2++)
                {
                    wartoscPoOperacji = (int)(tablicaLUT[obrazWe.GetPixel(k1, k2).R] * ((maxObraz-1) / iloscKlas));

                    kolor = Color.FromArgb(wartoscPoOperacji, wartoscPoOperacji, wartoscPoOperacji);

                    obrazWy.SetPixel(k1, k2, kolor);
                }
            }

            return obrazWy;
        }

        // TODO: przystosować do liczenia lokalnego
        private void obliczHistogram()
        {            
            for (int k1 = 0; k1 < obrazWe.Height; k1++)
            {
                for (int k2 = 0; k2 < obrazWe.Width; k2++)
                {
                    wartosciHistogramu[obrazWe.GetPixel(k1, k2).R]++;
                }
            }
        }

        // Wyznaczamy maksymalną wartość piksela, aby m.in. zaoszczędzić trochę na pamięci
        private void wyznaczMaksimum()
        {
            maxObraz = obrazWe.GetPixel(0, 0).R;
            
            for (int k1 = 0; k1 < obrazWe.Height; k1++)
            {
                for (int k2 = 0; k2 < obrazWe.Width; k2++)
                {
                    if (obrazWe.GetPixel(k1, k2).R > maxObraz)
                        maxObraz = obrazWe.GetPixel(k1, k2).R;
                }
            }
        }

        private void obliczDystrybuanteEmpiryczna()
        {
            double temp = 0;
            int iloscPikseli = obrazWe.Height * obrazWe.Width;

            for (int k = 0; k < iloscOdcieniSzarosci; k++)
            {
                temp += ((double)wartosciHistogramu[k] / (double)iloscPikseli);
                wartosciDystrybuanty[k] = temp;
            }
        }

        private void wypelnijLUT()
        {
            obliczDystrybuanteEmpiryczna();

            double dystrybuantaNiezerowa = 0;

            // Wyznaczanie _pierwszej_ niezerowej dystrybuanty
            for (int k = 0; k < iloscOdcieniSzarosci; k++)
            {
                if (wartosciDystrybuanty[k] > 0)
                {
                    dystrybuantaNiezerowa = wartosciDystrybuanty[k];
                    break;
                }
            }

            for (int k = 0; k < iloscOdcieniSzarosci; k++)
            {
                tablicaLUT[k] = Math.Floor(((wartosciDystrybuanty[k] - dystrybuantaNiezerowa) / (1 - dystrybuantaNiezerowa)) * (iloscKlas - 1));
            }
        }

        private void obliczDystrybuanteEmpiryczna()
        {
            double temp = 0;
            int iloscPikseli = obrazWe.Height * obrazWe.Width;
            
            wartosciDystrybuanty = new double[maxObraz + 1];

            for (int k = 0; k < (maxObraz + 1); k++)
            {
                temp += ((double)wartosciHistogramu[k] / (double)iloscPikseli);
                wartosciDystrybuanty[k] = temp;
            }
        }

        private void wypelnijLUT()
        {
            obliczDystrybuanteEmpiryczna();
            
            double dystrybuantaNiezerowa = 0;

            tablicaLUT = new double[maxObraz + 1];

            // Wyznaczanie _pierwszej_ niezerowej dystrybuanty
            for (int k = 0; k < (maxObraz + 1); k++)
            {
                if (wartosciDystrybuanty[k] > 0)
                {
                    dystrybuantaNiezerowa = wartosciDystrybuanty[k];
                    break;
                }
            }

            for (int k = 0; k < (maxObraz + 1); k++)
            {
                tablicaLUT[k] = ((wartosciDystrybuanty[k] - dystrybuantaNiezerowa) / (1 - dystrybuantaNiezerowa)) * (iloscKlas - 1);
            }
        }
    }
}
