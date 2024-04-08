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
using System.IO;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Unicat_Casino
{
    public partial class FlipTheCoin : Window
    {
        // 1 - orzeł, 0 - reszka
        int znak = 1;
        int postawionePieniądze = 0;
        Random random = new Random();

        public FlipTheCoin()
        {
            InitializeComponent();
        }

        private void FlipOrzel(object sender, RoutedEventArgs e)
        {
            znak = 1;
            FilpCoin();
        }

        private void FlipReszka(object sender, RoutedEventArgs e)
        {
            znak = 0;
            FilpCoin();
        }

        private void FilpCoin()
        {

            int wynikLosowania = random.Next(0, 2);
            coin.Visibility = Visibility.Collapsed;

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("images/files/flip.gif");
            image.EndInit();
            //ImageBehavior.SetAnimatedSource(coin, image);

            Thread.Sleep(1500);

            wynik.Text = znak == wynikLosowania ? "Gratulacje! Wygrałeś pieniądze!" : "Przegrałeś pieniądze!";
            coin.Source = wynikLosowania == 0 ? new BitmapImage(new Uri("images/files/reszka.png", UriKind.Relative)) : new BitmapImage(new Uri("images/files/orzel.png", UriKind.Relative)); ;

            
            

        }
    }
}
