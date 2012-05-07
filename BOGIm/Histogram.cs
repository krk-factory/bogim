﻿using System;
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
            this.iloscKlas = iloscKlas;

            double[] s_hist_eq = new double[256];
            double[] sum_of_hist = new double[256];
            
            for (int k1 = 0; k1 < obrazWe.Height; k1++)
            {
                for (int k2 = 0; k2 < obrazWe.Width; k2++)
                {
                    wartosciHistogramu[obrazWe.GetPixel(k1, k2).R]++;
                }
            }
	
	        int n = obrazWe.Width * obrazWe.Height;

	        for (int i=0;i<256;i++)  // pdf of image
	        {
		        s_hist_eq[i] = (double)wartosciHistogramu[i]/(double)n;
	        }
	        
            
            sum_of_hist[0] = s_hist_eq[0];
            Color c;
            int t;
	
            for (int i=1;i<256;i++)	 // cdf of image
	        {
		        sum_of_hist[i] = sum_of_hist[i-1] + s_hist_eq[i];
	        }

	        for(int i=0;i<obrazWe.Height;i++)
	        {
		        for(int j=0;j<obrazWe.Width;j++)
		        {
			        //k = img_data[i][j];
			        //img_data[i][j] = (unsigned char)round( sum_of_hist[k] * 255.0 );

                    c = obrazWe.GetPixel(i, j);
                    t = (int)Math.Floor(sum_of_hist[c.R] * 255.0);
                    
                    c = Color.FromArgb(t, t, t);

                    obrazWy.SetPixel(i, j, c);
		        }
	        }

            //-----------------------------------

            return obrazWy;
        }


        /*public Bitmap wyrownajHistogramGlobalnie(int iloscKlas)
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

        /*private void obliczDystrybuanteEmpiryczna()
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
        }*/
    }
}