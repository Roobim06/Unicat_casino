using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Unicat_Casino
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Random rand_dice = new Random();
        public Window1()
        {
            InitializeComponent();
            ShowRandomDice();
        }
        private void ShowRandomDice()
        {
            for (int i = 1; i <= 6; i++)
            {
                string diceFile = $"PathToYourDiceImagesFolder/{i}_dice.png"; // Ścieżka do pliku kostki
                BitmapImage diceImage = new BitmapImage(new Uri(diceFile));
                Image imageControl = new Image
                {
                    Source = diceImage,
                    Margin = new Thickness(5) // Margines między obrazami
                };
                diceStackPanel.Children.Add(imageControl);

            }
        }
    }
}