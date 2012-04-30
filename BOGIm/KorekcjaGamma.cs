using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BOGIm
{
    class KorekcjaGamma
    {
        private Bitmap obrazWe;
        private Bitmap obrazWy;

        private double[] tablicaLUT;

        private Color kolor;

        // Konstruktor
        public KorekcjaGamma(Bitmap obraz)
        {
            this.obrazWe = obraz;

            obrazWy = new Bitmap(obrazWe.Width, obrazWe.Height);

            tablicaLUT = new double[256];

            for (int k = 0; k < 256; k++)
            {
                tablicaLUT[k] = k;
            }
        }

        // Zasadnicze operacje korekcji gamma
        public Bitmap wykonajKorekcje(double wartoscKorekcji)
        {
            int odcienSzarosciObrazuWe;
            int wartoscPoKorekcji;
            
            wypelnijLUT(wartoscKorekcji);

            for (int k1 = 0; k1 < obrazWe.Size.Width; k1++)
            {
                for (int k2 = 0; k2 < obrazWe.Size.Height; k2++)
                {
                    kolor = obrazWe.GetPixel(k1, k2);                   // Pobieranie 'Coloru' każdego piksela

                    odcienSzarosciObrazuWe = kolor.R;                   // Z racji, ze obraz jest szary, wyciągamy tylko wartość R, bo pozostałe są takie same

                    wartoscPoKorekcji = (int)tablicaLUT[odcienSzarosciObrazuWe];                        // Pobieramy wartość po korekcji z tablicy LUT wg odcienia szarosci poszczególnego piksela

                    kolor = Color.FromArgb(wartoscPoKorekcji, wartoscPoKorekcji, wartoscPoKorekcji);    // Zapisujemy do 'Coloru' wartość piksela po korekcji

                    obrazWy.SetPixel(k1, k2, kolor);
                }
            }

            return obrazWy;
        }

        // Wypełnienie tablicy LUT dla korekcji gamma dla zadanego współczynnika
        private void wypelnijLUT(double wartoscKorekcji)
        {
            for (int k = 0; k < 256; k++)
                if ((255 * Math.Pow(k / 255.0, 1 / wartoscKorekcji)) > 255)
                    tablicaLUT[k] = 255;
                else
                    tablicaLUT[k] = 255 * Math.Pow(k / 255.0, 1 / wartoscKorekcji);
        }
    }
}
