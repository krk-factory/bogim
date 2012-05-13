using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

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
        private int[] wartosciHistogramuWy;

        private int iloscKlas;

        public const int iloscOdcieniSzarosci = 256;

        private MainWindow mw;

        /* --- Metody Klasy --- */
        public Histogram(Bitmap obraz, MainWindow mw)
        {
            this.mw = mw;
            this.obrazWe = obraz;

            obrazWy = new Bitmap(obrazWe.Width, obrazWe.Height);

            wartosciDystrybuanty = new double[iloscOdcieniSzarosci];
            tablicaLUT = new double[iloscOdcieniSzarosci];

            wartosciHistogramu = new int[iloscOdcieniSzarosci];
            wartosciHistogramuWy = new int[iloscOdcieniSzarosci];
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
            //int[] final_eq = new int[256]; 
            double[] final_eq = new double[256];
            double[] min_max = new double[2];
            double[] binary_limits = new double[iloscKlas];

            for (int k1 = 0; k1 < obrazWe.Height; k1++)
            {
                for (int k2 = 0; k2 < obrazWe.Width; k2++)
                {
                    wartosciHistogramu[obrazWe.GetPixel(k1, k2).R]++;
                }
            }

            // --- Histogram We ---
            mw.chartHistoWe.Series["Series1"].Points.Clear();       // na wszelki wypadek
            
            for (int k = 0; k < iloscOdcieniSzarosci; k++)
            {
                mw.chartHistoWe.Series["Series1"].Points.AddY(wartosciHistogramu[k]);
            }
            //--------------------

            int n = obrazWe.Width * obrazWe.Height;

            for (int i = 0; i < 256; i++)  // pdf of image
            {
                s_hist_eq[i] = (double)wartosciHistogramu[i] / (double)n;
            }

            sum_of_hist[0] = s_hist_eq[0];
            Color c;
            int t;

            for (int i = 1; i < 256; i++)	 // cdf of image
            {
                sum_of_hist[i] = sum_of_hist[i - 1] + s_hist_eq[i];
            }

            binary_limits = binary_limits_finder(sum_of_hist, sum_of_hist.Sum());   // wyznaczenie przedzialow w ktorych mieszcza sie odpowiednie kolory
            final_eq = group_histo(sum_of_hist, binary_limits);                     // przesuniecie histogramow do wyznaczonych wczesniej przedzialow

            for (int i = 0; i < obrazWe.Height; i++)
            {
                for (int j = 0; j < obrazWe.Width; j++)
                {
                    c = obrazWe.GetPixel(i, j);
                    t = (int)final_eq[c.R];

                    c = Color.FromArgb(t, t, t);

                    obrazWy.SetPixel(i, j, c);
                }
            }

            // --- Histogram Wy ---
            
            mw.chartHistoWy.Series["Series1"].Points.Clear();       // na wszelki wypadek

            for (int k1 = 0; k1 < obrazWy.Height; k1++)
            {
                for (int k2 = 0; k2 < obrazWy.Width; k2++)
                {
                    wartosciHistogramuWy[obrazWy.GetPixel(k1, k2).R]++;
                }
            }

            for (int k = 0; k < iloscOdcieniSzarosci; k++)
            {
                mw.chartHistoWy.Series["Series1"].Points.AddY(wartosciHistogramuWy[k]);
            }

            //--------------------

            return obrazWy;
        }

        
        public double[] binary_limits_finder(double[] histo, double sum)
        {
            double[] binary_limits = new double[iloscKlas-1];           //tablica z limitami --> Limitow powinno byc (iloscKlas-1) - przeciez one powinny podzielic nasz obraz na tyle obszarow, ile wynosi iloscKlas
            double percent_part = (double)1 / (double)iloscKlas;        //skok procentowy z jakim sie poruszamy (np 12.5)
            double percent_part_floating = percent_part;                //przesuwajacy sie przedzial procentowy (np 12.5->25>37.5)

            double foo = 0;
            int curr_index = 0; //nr przedzialu w ktorym jestesmy
            
            for (int i = 0; i < 256; i++)
            {
                foo += histo[i];
                if (foo/sum > percent_part_floating)        //sprawdzenie w jakim przedziale miesci sie suma prawdopodobienstw
                {
                    percent_part_floating += percent_part;  //jezeli juz w nastepnym, to zwieksz przedzial procentowy
                    binary_limits[curr_index++] = i-1;      //zapisz nr koloru granicznego
                }
            }
            //binary_limits[iloscKlas - 2] = 255; //ostatni i tak zawsze jest bialy, a niestety petla nigdy do niego nie dojdzie TO FIX
            
            return binary_limits;   //funkcja zwraca tablice limitow tzn bin_lim[0] = 101 oznacza, ze wszystkie piksele
        }                           //do koloru 101 zostana wrzucone do tego samego przedzialu (pokolorowane na ten sam kolor)
        
        
        public double[] group_histo(double[] histo, double[] limits)
        {
            //int[] new_histo = new int[256];
            double[] new_histo = new double[256];
            int temp_index = 0;
            //int temp_value = 0; //zawsze zaczynamy od zera
            double temp_value = ((double)255 / (double)(iloscKlas - 1));
            double floating_value = 0;

            for (int i = 0; i <= limits.Length; i++)
            {
                if (i < limits.Length)
                    while (temp_index < limits[i])  // warunek ten nie uwzględniał ostatniego przedziału -> np. 4 klasy, czyli 3 progi binaryzacyjne
                                                    // --> [0; bin[0]], [bin[0]; bin[1]], [bin[1]; bin[2]]; [bin[2]; 255] => mamy 4 przedziały/klasy
                        new_histo[temp_index++] = floating_value;   //kolorowanie kolejnych pikseli

                if (i == limits.Length)
                    while (temp_index < 256)        // ostatni przedział
                        new_histo[temp_index++] = 255;
                
                floating_value += temp_value; //wyznaczamy nowy kolor z wzoru ktory podal Maciek

                //temp_value = ((double)(iloscKlas - i)/(double)(iloscKlas)) * 255;
            }
            
            return new_histo;
        }

        
        public static double[] _min_max(double[] dist_func)
        {
            double[] answer = new double[2];

            for (int i = 0; i < dist_func.Length; i++)
            {
                if (dist_func[i] > 0)
                {
                    answer[0] = i;
                    break;
                }
            }

            for (int i = dist_func.Length - 1; i >= 0; i--)
            {
                if (dist_func[i] > 0)
                {
                    answer[1] = i;
                    break;
                }
            }
            return answer;
        }

        /*
//------------------------------- OLD VERSION ---------------------------------------------------------

        public Bitmap wyrownajHistogramGlobalnie(int iloscKlas)
        {
            this.iloscKlas = iloscKlas;

            double[] s_hist_eq = new double[256];
            double[] sum_of_hist = new double[256];
            double[] final_eq = new double[256];
            double[] min_max = new double[2];

            for (int k1 = 0; k1 < obrazWe.Height; k1++)
            {
                for (int k2 = 0; k2 < obrazWe.Width; k2++)
                {
                    wartosciHistogramu[obrazWe.GetPixel(k1, k2).R]++;
                }
            }

            // --- Histogra We ---
            mw.chartHistoWe.Series["Series1"].Points.Clear();       // na wszelki wypadek

            for (int k = 0; k < iloscOdcieniSzarosci; k++)
            {
                mw.chartHistoWe.Series["Series1"].Points.AddY(wartosciHistogramu[k]);
            }
            //--------------------

            int n = obrazWe.Width * obrazWe.Height;

            for (int i = 0; i < 256; i++)  // pdf of image
            {
                s_hist_eq[i] = (double)wartosciHistogramu[i] / (double)n;
            }

            sum_of_hist[0] = s_hist_eq[0];
            Color c;
            int t;

            for (int i = 1; i < 256; i++)	 // cdf of image
            {
                sum_of_hist[i] = sum_of_hist[i - 1] + s_hist_eq[i];
            }

            final_eq = sum_of_hist;

            for (int i = 0; i < obrazWe.Height; i++)
            {
                for (int j = 0; j < obrazWe.Width; j++)
                {
                    //k = img_data[i][j];
                    //img_data[i][j] = (unsigned char)round( sum_of_hist[k] * 255.0 );

                    c = obrazWe.GetPixel(i, j);
                    t = (int)(final_eq[c.R] * 255.0);

                    c = Color.FromArgb(t, t, t);

                    obrazWy.SetPixel(i, j, c);
                }
            }

            // --- Histogra Wy ---
            mw.chartHistoWy.Series["Series1"].Points.Clear();       // na wszelki wypadek

            for (int k1 = 0; k1 < obrazWy.Height; k1++)
            {
                for (int k2 = 0; k2 < obrazWy.Width; k2++)
                {
                    wartosciHistogramuWy[obrazWy.GetPixel(k1, k2).R]++;
                }
            }

            for (int k = 0; k < iloscOdcieniSzarosci; k++)
            {
                mw.chartHistoWy.Series["Series1"].Points.AddY(wartosciHistogramuWy[k]);
            }

            //--------------------

            return obrazWy;
        }

// ------------------------------ OLDIES --------------------------------------------------------------

               /*public Bitmap wyrownajHistogramGlobalnie(int iloscKlas)
                {
                    this.iloscKlas = iloscKlas;

                    double[] s_hist_eq = new double[256];
                    double[] sum_of_hist = new double[256];
                    int[] final_eq = new int[256];
                    double[] min_max = new double[2];

                    for (int k1 = 0; k1 < obrazWe.Height; k1++)
                    {
                        for (int k2 = 0; k2 < obrazWe.Width; k2++)
                        {
                            wartosciHistogramu[obrazWe.GetPixel(k1, k2).R]++;
                        }
                    }

                    // --- Histogra We ---
                    for (int k = 0; k < iloscOdcieniSzarosci; k++)
                    {
                        mw.chartHistoWe.Series["Series1"].Points.AddY(wartosciHistogramu[k]);
                    }
                    //--------------------

                    int n = obrazWe.Width * obrazWe.Height;

                    for (int i = 0; i < 256; i++)  // pdf of image
                    {
                        s_hist_eq[i] = (double)wartosciHistogramu[i] / (double)n;
                    }


                    sum_of_hist[0] = s_hist_eq[0];
                    Color c;
                    int t;

                    for (int i = 1; i < 256; i++)	 // cdf of image
                    {
                        sum_of_hist[i] = s_hist_eq[i - 1] + s_hist_eq[i];
                    }

                    min_max = _min_max(sum_of_hist);

                    for (int i = 0; i < 256; i++)
                    {
                        int temp = (int)(((sum_of_hist[i] - min_max[0]) / (n - min_max[0])) * 255);
                        //final_eq[i] = (int)( ( (sum_of_hist[i] - min_max[0]) / (n - min_max[0]) ) * 255);
                        int temp2 = 256 / this.iloscKlas;
                        final_eq[i] *= temp*temp2;
                    }

                    for (int i = 0; i < obrazWe.Height; i++)
                    {
                        for (int j = 0; j < obrazWe.Width; j++)
                        {
                            //k = img_data[i][j];
                            //img_data[i][j] = (unsigned char)round( sum_of_hist[k] * 255.0 );

                            c = obrazWe.GetPixel(i, j);
                            t = final_eq[c.R];

                            c = Color.FromArgb(t, t, t);

                            obrazWy.SetPixel(i, j, c);
                        }
                    }

                    // --- Histogram Wy ---
                    for (int k1 = 0; k1 < obrazWy.Height; k1++)
                    {
                        for (int k2 = 0; k2 < obrazWy.Width; k2++)
                        {
                            wartosciHistogramuWy[obrazWy.GetPixel(k1, k2).R]++;
                        }
                    }

                    for (int k = 0; k < iloscOdcieniSzarosci; k++)
                    {
                        mw.chartHistoWy.Series["Series1"].Points.AddY(wartosciHistogramuWy[k]);
                    }

                    //--------------------

                    return obrazWy;
                }


        /*{
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
        }*/


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
