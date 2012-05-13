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
        private int[] wartosciHistogramuWy2;
        private int[] wartosciHistogramuWy3;

        private int iloscKlas;

        public const int iloscOdcieniSzarosci = 256;
        public const int kolorBialy = 255;

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
            wartosciHistogramuWy2 = new int[iloscOdcieniSzarosci];
            wartosciHistogramuWy3 = new int[iloscOdcieniSzarosci];
        }

        public Bitmap wyrownajHistogramLokalnie(int iloscKlas)
        {
            Bitmap obrazWe_temp=obrazWe;
            Bitmap obrazWe_temp2;
            int pp = obrazWe.Height;
            int kk = obrazWe.Width;
            int licznik1=0,licznik2=0;
            for (int i = 0; i < pp; i += 16)
                for (int j = 0; j < kk; j += 16)
                {
                    if (j >= i)
                    {
                        obrazWe_temp2 = new Bitmap(16, 16);
                        for (int k = i; k < i + 16; k++)
                        {
                            for (int l = j; l < j + 16; l++)
                            {
                                obrazWe_temp2.SetPixel(licznik1, licznik2, obrazWe.GetPixel(k, l));
                                licznik2++;
                            }
                            licznik1++;
                            licznik2 = 0;
                        }
                            
                        obrazWe = new Bitmap(16, 16);
                        obrazWe = obrazWe_temp2;
                        Bitmap obrazwy_temp=wyrownajHistogramGlobalnie(8);
                        licznik1 = 0; licznik2 = 0;
                        for (int k = i; k < i + 16; k++)
                        {
                            for (int l = j; l < j + 16; l++)
                            {
                                obrazWy.SetPixel(k, l, obrazwy_temp.GetPixel(licznik1, licznik2));
                                licznik2++;
                            }
                            licznik1++;
                            licznik2 = 0;
                        }
                        licznik1 = 0;
                        obrazWe = obrazWe_temp;
                    }
                    
                }

             return obrazWy;
        }

        public Bitmap wyrownajHistogramGlobalnie(int iloscKlas)
        {
            this.iloscKlas = iloscKlas;

            double[] s_hist_eq = new double[iloscOdcieniSzarosci];
            double[] sum_of_hist = new double[iloscOdcieniSzarosci];
            int[] final_eq = new int[iloscOdcieniSzarosci];
            double[] min_max = new double[2];
            int[] binary_limits = new int[iloscKlas];

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

            for (int i = 0; i < iloscOdcieniSzarosci; i++)  // pdf of image
            {
                s_hist_eq[i] = (double)wartosciHistogramu[i] / (double)n;
            }

            sum_of_hist[0] = s_hist_eq[0];
            Color c;
            int t;

            for (int i = 1; i < iloscOdcieniSzarosci; i++)	 // cdf of image
            {
                sum_of_hist[i] = sum_of_hist[i - 1] + s_hist_eq[i];
            }

            for (int i = 0; i < obrazWe.Height; i++)
            {
                for (int j = 0; j < obrazWe.Width; j++)
                {
                    c = obrazWe.GetPixel(i, j);
                    t = (int)(sum_of_hist[c.R] * 255.0);
                    if (t > 255) t = 255;

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

            binary_limits = binary_limits_finder(wartosciHistogramuWy, wartosciHistogramuWy.Sum());   // wyznaczenie przedzialow w ktorych mieszcza sie odpowiednie kolory
            wartosciHistogramuWy2 = group_histo(wartosciHistogramuWy, binary_limits);                 // przesuniecie histogramow do wyznaczonych wczesniej przedzialow

            for (int i = 0; i < obrazWe.Height; i++)
            {
                for (int j = 0; j < obrazWe.Width; j++)
                {
                    c = obrazWy.GetPixel(i, j);
                    t = wartosciHistogramuWy2[c.R];

                    c = Color.FromArgb(t, t, t);

                    obrazWy.SetPixel(i, j, c);
                }
            }

            for (int k1 = 0; k1 < obrazWy.Height; k1++)
            {
                for (int k2 = 0; k2 < obrazWy.Width; k2++)
                {
                    wartosciHistogramuWy3[obrazWy.GetPixel(k1, k2).R]++;
                }
            }

            for (int k = 0; k < iloscOdcieniSzarosci; k++)
            {
                mw.chartHistoWy.Series["Series1"].Points.AddY(wartosciHistogramuWy3[k]);
            }


            return obrazWy;
        }

        
        public int[] binary_limits_finder(int[] histo, int sum)
        {
            int[] binary_limits = new int[iloscKlas-1];           //tablica z limitami --> Limitow powinno byc (iloscKlas-1) - przeciez one powinny podzielic nasz obraz na tyle obszarow, ile wynosi iloscKlas
            double percent_part = (double)1 / (double)iloscKlas;        //skok procentowy z jakim sie poruszamy (np 12.5)
            double percent_part_floating = percent_part;                //przesuwajacy sie przedzial procentowy (np 12.5->25>37.5)
            //MessageBox.Show("Suma pikseli: " + sum*

            double foo = 0;
            int curr_index = 0; //nr przedzialu w ktorym jestesmy

            for (int i = 0; i < iloscOdcieniSzarosci; i++)
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
        
        
        public int[] group_histo(int[] histo, int[] limits)
        {
            int[] new_histo = new int[iloscOdcieniSzarosci];
            int temp_index = 0;
            double temp_value = ((double)kolorBialy / (double)(iloscKlas - 1));
            double floating_value = 0;

            for (int i = 0; i <= limits.Length; i++)
            {
                if (i < limits.Length)
                    while (temp_index < limits[i])  // warunek ten nie uwzględniał ostatniego przedziału -> np. 4 klasy, czyli 3 progi binaryzacyjne
                                                    // --> [0; bin[0]], [bin[0]; bin[1]], [bin[1]; bin[2]]; [bin[2]; 255] => mamy 4 przedziały/klasy
                        new_histo[temp_index++] = (int)floating_value;   //kolorowanie kolejnych pikseli

                if (i == limits.Length)
                    while (temp_index < iloscOdcieniSzarosci)        // ostatni przedział
                        new_histo[temp_index++] = kolorBialy;
                
                floating_value += temp_value; //wyznaczamy nowy kolor z wzoru ktory podal Maciek
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


    }
}
