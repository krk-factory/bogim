﻿using System;
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

            wartosciDystrybuanty = new double[iloscOdcieniSzarosci];
            tablicaLUT = new double[iloscOdcieniSzarosci];
        }

        /***********************************************************************************************/

        public Bitmap wyrownajHistogramLokalnie(int iloscKlas, int rozmiarBloku)
        {
            Bitmap obrazWe_temp = obrazWe;
            Bitmap obrazWe_temp2;
            Bitmap obrazWy = new Bitmap(obrazWe.Width, obrazWe.Height);

            calculate_histogram_in();

            for (int i = 0; i < obrazWe.Height; i += rozmiarBloku)
            {
                for (int j = 0; j < obrazWe.Width; j += rozmiarBloku)
                {
                    obrazWe_temp2 = new Bitmap(rozmiarBloku, rozmiarBloku);
                    for (int k = i; k < i + rozmiarBloku; k++)
                        for (int l = j; l < j + rozmiarBloku; l++)
                            obrazWe_temp2.SetPixel(k % rozmiarBloku, l % rozmiarBloku, obrazWe.GetPixel(k, l));
                    obrazWe = obrazWe_temp2;

                    Bitmap obrazwy_temp = wyrownajHistogramGlobalnie_ver2(iloscKlas);
                    //Bitmap obrazwy_temp = obrazWe;

                    for (int k = i; k < i + rozmiarBloku; k++)
                        for (int l = j; l < j + rozmiarBloku; l++)
                            obrazWy.SetPixel(k, l, obrazwy_temp.GetPixel(k % rozmiarBloku, l % rozmiarBloku));
                    obrazWe = obrazWe_temp;
                    //mw.label1.Text = (i / obrazWe.Height).ToString();
                    mw.label1.Text = "i: " + i.ToString() + " j: " + j.ToString();
                    mw.label1.Refresh();
                }
            }
            calculate_histogram_out(obrazWy);
            return obrazWy;
        }

        /***********************************************************************************************/

        public Bitmap wyrownajHistogramGlobalnie(int iloscKlas)
        {
            this.iloscKlas = iloscKlas;

            double[] s_hist_eq = new double[iloscOdcieniSzarosci];
            double[] sum_of_hist = new double[iloscOdcieniSzarosci];
            int[] final_eq = new int[iloscOdcieniSzarosci];
            int[] binary_limits = new int[iloscKlas];
            Bitmap obrazWy = new Bitmap(obrazWe.Width, obrazWe.Height);

            wartosciHistogramu = new int[iloscOdcieniSzarosci];
            wartosciHistogramuWy = new int[iloscOdcieniSzarosci];
            wartosciHistogramuWy2 = new int[iloscOdcieniSzarosci];
            wartosciHistogramuWy3 = new int[iloscOdcieniSzarosci];

            for (int k1 = 0; k1 < obrazWe.Height; k1++)
                for (int k2 = 0; k2 < obrazWe.Width; k2++)
                    wartosciHistogramu[obrazWe.GetPixel(k1, k2).R]++;

            calculate_histogram_in();

            int n = obrazWe.Width * obrazWe.Height;

            for (int i = 0; i < iloscOdcieniSzarosci; i++)  // pdf of image
                s_hist_eq[i] = (double)wartosciHistogramu[i] / (double)n;

            sum_of_hist[0] = s_hist_eq[0];
            Color c;
            int t;

            for (int i = 1; i < iloscOdcieniSzarosci; i++)	 // cdf of image
                sum_of_hist[i] = sum_of_hist[i - 1] + s_hist_eq[i];

            for (int i = 0; i < obrazWe.Height; i++)
            {
                for (int j = 0; j < obrazWe.Width; j++)
                {
                    c = obrazWe.GetPixel(i, j);
                    t = (int)(sum_of_hist[c.R] * 255.0);

                    c = Color.FromArgb(t, t, t);

                    obrazWy.SetPixel(i, j, c);
                }
            }

            // --- Histogram Wy ---

            for (int k1 = 0; k1 < obrazWy.Height; k1++)
                for (int k2 = 0; k2 < obrazWy.Width; k2++)
                    wartosciHistogramuWy[obrazWy.GetPixel(k1, k2).R]++;

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

            calculate_histogram_out(obrazWy);
            mw.label1.Text = "";

            return obrazWy;
        }

        /***********************************************************************************************/

        public Bitmap wyrownajHistogramGlobalnie_ver2(int iloscKlas)
        {
            this.iloscKlas = iloscKlas;

            double[] s_hist_eq = new double[iloscOdcieniSzarosci];
            double[] sum_of_hist = new double[iloscOdcieniSzarosci];
            int[] final_eq = new int[iloscOdcieniSzarosci];
            int[] min_max = new int[2];
            int[] binary_limits = new int[iloscKlas];
            Bitmap obrazWy = new Bitmap(obrazWe.Width, obrazWe.Height);

            wartosciHistogramu = new int[iloscOdcieniSzarosci];
            wartosciHistogramuWy = new int[iloscOdcieniSzarosci];
            wartosciHistogramuWy2 = new int[iloscOdcieniSzarosci];

            for (int k1 = 0; k1 < obrazWe.Height; k1++)
                for (int k2 = 0; k2 < obrazWe.Width; k2++)
                    wartosciHistogramu[obrazWe.GetPixel(k1, k2).R]++;

            min_max = _min_max(wartosciHistogramu);

            int n = obrazWe.Width * obrazWe.Height;

            for (int i = min_max[0]; i < min_max[1]; i++)  // pdf of image
                s_hist_eq[i] = (double)wartosciHistogramu[i] / (double)n;

            sum_of_hist[min_max[0]] = s_hist_eq[min_max[0]];
            Color c;
            int t;

            for (int i = min_max[0]+1; i < min_max[1]; i++)	 // cdf of image
                sum_of_hist[i] = sum_of_hist[i - 1] + s_hist_eq[i];

            for (int i = 0; i < obrazWe.Height; i++)
            {
                for (int j = 0; j < obrazWe.Width; j++)
                {
                    c = obrazWe.GetPixel(i, j);
                    t = (int)(sum_of_hist[c.R] * 255.0);

                    t = (int)(((double)t * (double)(min_max[1] - min_max[0])) / 255.0) + min_max[0];

                    c = Color.FromArgb(t, t, t);

                    obrazWy.SetPixel(i, j, c);
                }
            }

            for (int k1 = 0; k1 < obrazWy.Height; k1++)
                for (int k2 = 0; k2 < obrazWy.Width; k2++)
                    wartosciHistogramuWy[obrazWy.GetPixel(k1, k2).R]++;

            binary_limits = binary_limits_finder_ver2(wartosciHistogramuWy, wartosciHistogramuWy.Sum(), min_max);   // wyznaczenie przedzialow w ktorych mieszcza sie odpowiednie kolory
            wartosciHistogramuWy2 = group_histo_ver2(wartosciHistogramuWy, binary_limits, min_max);                 // przesuniecie histogramow do wyznaczonych wczesniej przedzialow

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

            return obrazWy;
        }

        /***********************************************************************************************/

        private void calculate_histogram_in()
        {
            int[] wartosciHistogramu_we = new int[iloscOdcieniSzarosci];

            for (int k1 = 0; k1 < obrazWe.Height; k1++)
                for (int k2 = 0; k2 < obrazWe.Width; k2++)
                    wartosciHistogramu_we[obrazWe.GetPixel(k1, k2).R]++;
            // --- Histogram We ---
            mw.chartHistoWe.Series["Series1"].Points.Clear();       // na wszelki wypadek

            for (int k = 0; k < iloscOdcieniSzarosci; k++)
                mw.chartHistoWe.Series["Series1"].Points.AddY(wartosciHistogramu_we[k]);
            //--------------------
        }

        /***********************************************************************************************/

        private void calculate_histogram_out(Bitmap obrazWy)
        {
            int[] war_hist = new int[iloscOdcieniSzarosci];

            for (int k1 = 0; k1 < obrazWy.Height; k1++)
                for (int k2 = 0; k2 < obrazWy.Width; k2++)
                    war_hist[obrazWy.GetPixel(k1, k2).R]++;

            mw.chartHistoWy.Series["Series1"].Points.Clear();       // na wszelki wypadek
            for (int k = 0; k < iloscOdcieniSzarosci; k++)
                mw.chartHistoWy.Series["Series1"].Points.AddY(war_hist[k]);
        }

        /***********************************************************************************************/


        public int[] binary_limits_finder(int[] histo, int sum)
        {
            int[] binary_limits = new int[iloscKlas - 1];           //tablica z limitami --> Limitow powinno byc (iloscKlas-1) - przeciez one powinny podzielic nasz obraz na tyle obszarow, ile wynosi iloscKlas
            double percent_part = (double)1 / (double)iloscKlas;        //skok procentowy z jakim sie poruszamy (np 12.5)
            double percent_part_floating = percent_part;                //przesuwajacy sie przedzial procentowy (np 12.5->25>37.5)
            //MessageBox.Show("Suma pikseli: " + sum*

            double foo = 0;
            int curr_index = 0; //nr przedzialu w ktorym jestesmy

            for (int i = 0; i < iloscOdcieniSzarosci; i++)
            {
                foo += histo[i];
                if (foo / sum > percent_part_floating)        //sprawdzenie w jakim przedziale miesci sie suma prawdopodobienstw
                {
                    percent_part_floating += percent_part;  //jezeli juz w nastepnym, to zwieksz przedzial procentowy
                    binary_limits[curr_index++] = i - 1;      //zapisz nr koloru granicznego
                }
            }
            //binary_limits[iloscKlas - 2] = 255; //ostatni i tak zawsze jest bialy, a niestety petla nigdy do niego nie dojdzie TO FIX

            return binary_limits;   //funkcja zwraca tablice limitow tzn bin_lim[0] = 101 oznacza, ze wszystkie piksele
        }                           //do koloru 101 zostana wrzucone do tego samego przedzialu (pokolorowane na ten sam kolor)

        /***********************************************************************************************/

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

        public int[] binary_limits_finder_ver2(int[] histo, int sum, int[] min_max)
        {
            int[] binary_limits = new int[iloscKlas - 1];           //tablica z limitami --> Limitow powinno byc (iloscKlas-1) - przeciez one powinny podzielic nasz obraz na tyle obszarow, ile wynosi iloscKlas
            double percent_part = (double)1 / (double)iloscKlas;        //skok procentowy z jakim sie poruszamy (np 12.5)
            double percent_part_floating = percent_part;                //przesuwajacy sie przedzial procentowy (np 12.5->25>37.5)
            //MessageBox.Show("Suma pikseli: " + sum*

            double foo = 0;
            int curr_index = 0; //nr przedzialu w ktorym jestesmy

            for (int i = min_max[0]; i < min_max[1]; i++)
            {
                foo += histo[i];
                if (foo / sum > percent_part_floating)        //sprawdzenie w jakim przedziale miesci sie suma prawdopodobienstw
                {
                    percent_part_floating += percent_part;  //jezeli juz w nastepnym, to zwieksz przedzial procentowy
                    binary_limits[curr_index++] = i - 1;      //zapisz nr koloru granicznego
                }
            }
            //binary_limits[iloscKlas - 2] = 255; //ostatni i tak zawsze jest bialy, a niestety petla nigdy do niego nie dojdzie TO FIX

            return binary_limits;   //funkcja zwraca tablice limitow tzn bin_lim[0] = 101 oznacza, ze wszystkie piksele
        }                           //do koloru 101 zostana wrzucone do tego samego przedzialu (pokolorowane na ten sam kolor)


        public int[] group_histo_ver2(int[] histo, int[] limits, int[] min_max)
        {
            int[] new_histo = new int[iloscOdcieniSzarosci];
            int temp_index = 0;
            double temp_value = ((double)(min_max[1]-min_max[0]) / (double)(iloscKlas - 1));
            double floating_value = min_max[0];

            for (int i = 0; i <= limits.Length; i++)
            {
                if (i < limits.Length)
                    while (temp_index < limits[i])  // warunek ten nie uwzględniał ostatniego przedziału -> np. 4 klasy, czyli 3 progi binaryzacyjne
                        // --> [0; bin[0]], [bin[0]; bin[1]], [bin[1]; bin[2]]; [bin[2]; 255] => mamy 4 przedziały/klasy
                        new_histo[temp_index++] = (int)floating_value;   //kolorowanie kolejnych pikseli

                if (i == limits.Length)
                    while (temp_index < iloscOdcieniSzarosci)        // ostatni przedział
                        new_histo[temp_index++] = min_max[1];

                floating_value += temp_value; //wyznaczamy nowy kolor z wzoru ktory podal Maciek
            }
            return new_histo;
        }

        /***********************************************************************************************/

        public static int[] _min_max(int[] dist_func)
        {
            int[] answer = new int[2];

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
