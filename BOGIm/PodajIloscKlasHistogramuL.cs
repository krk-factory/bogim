using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BOGIm
{
    public partial class PodajIloscKlasHistogramuL : Form
    {
        public static int iloscKlas = 0;
        public static int iloscBlokow = 0;
        public static bool operacja = false;
        public static bool operacja_b = false;
        
        public PodajIloscKlasHistogramuL()
        {
            InitializeComponent();
        }

        private void potwierdzButton_Click(object sender, EventArgs e)
        {
            try
            {
                iloscKlas = Convert.ToInt32(ileKlasTextBox.Text);
                iloscBlokow = Convert.ToInt32(ileBlokowTextBox.Text);
                operacja = true;
                operacja_b = true;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Błąd formatu!\n\n" + ex.Message);
                operacja = false;
            }


            // TO DO: napisać warunek, żeby iloscKlas była potęgą 2
            if (iloscKlas < 2 || iloscKlas > 256 || !(iloscKlas%2 == 0))
            {
                MessageBox.Show("Ilość klas nie może być mniejsza od 1 i większa 256 oraz musi być potęgą liczby 2. Podaj ją ponownie");
                operacja = false;
            }
            if (iloscBlokow != 16 && iloscBlokow != 32)
            {
                MessageBox.Show("Rozmiar bloku musi wynosić 16 lub 32.");
                operacja_b = false;
            }


            this.Close();
        }
    }
}
