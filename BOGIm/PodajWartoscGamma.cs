using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BOGIm
{
    public partial class PodajWartoscGamma : Form
    {
        public static double wartoscGamma = 0;
        public static bool operacja = false;
        
        public PodajWartoscGamma()
        {
            InitializeComponent();
        }

        private void potwierdzButton_Click(object sender, EventArgs e)
        {
            try
            {
                wartoscGamma = Convert.ToDouble(gammaWartoscTextBox.Text);
                operacja = true;
            }
            catch (FormatException ex)
            {
                if (gammaWartoscTextBox.Text.Contains('.'))
                {
                    wartoscGamma = Convert.ToDouble(gammaWartoscTextBox.Text.Replace('.', ','));
                    operacja = true;
                }
                else
                {
                    MessageBox.Show("Błąd formatu!\n\n" + ex.Message);
                    operacja = false;
                }
            }

            if (wartoscGamma < 0)
            {
                MessageBox.Show("Wartość współczynnika gamma nie może być ujemna. Podaj ją ponownie");
                operacja = false;
            }

            this.Close();
        }
    }
}
