using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Windows.Media.Animation;
using WpfAnimatedGif;
using System.Windows.Threading;

namespace Unicat_Casino
{
    public partial class FlipTheCoin : Window
    {
        // 1 - head, 0 - tail
        int znak = 1;
        int postawionePieniądze = 0;
        Random random = new Random();

        public FlipTheCoin()
        {
            InitializeComponent();
            Zetony.Maximum = konta.konto.Tokens;
            betOrzel.IsEnabled = false;
            betReszka.IsEnabled = false;
        }

        private void FlipOrzel(object sender, RoutedEventArgs e)
        {
            znak = 1;
            Update();


        }

        private void FlipReszka(object sender, RoutedEventArgs e)
        {
            znak = 0;
            Update();

        }
        private void Update()
        {
            konta.UpdateTokens(postawionePieniądze * -1);
            betOrzel.IsEnabled = false;
            betReszka.IsEnabled = false;
            coin.IsEnabled = true;
            coPostawiles.Text = znak == 0 ? "Co postawiono: tail" : "Co postawiono: head";
            wynik2.Text = "";
            coWypadlo.Text = "";
        }

        private void Zetony_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            Zetony.Maximum = konta.konto.Tokens;
            postawionePieniądze = Convert.ToInt32(Zetony.Value);
            zastaw.Content = "Postawione tokeny:" + postawionePieniądze;
            if (postawionePieniądze != 1)
            {
                betOrzel.IsEnabled = true;
                betReszka.IsEnabled = true;
            }
            else
            {
                betOrzel.IsEnabled = false;
                betReszka.IsEnabled = false;
            }
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            Unicat_Casino.Menu okno = new Unicat_Casino.Menu();
            okno.Show();
            this.Close();
        }

        private async void Flip_Click(object sender, MouseButtonEventArgs e)
        {
            coin.Visibility = Visibility.Collapsed;
            coinFlip.Visibility = Visibility.Visible;
            Zetony.IsEnabled = false;



            int wynikLosowania = random.Next(0, 2);
            await Task.Delay(1500);
            wynik2.Text = znak == wynikLosowania ? $"Gratulacje! Wygrałeś {postawionePieniądze} pieniądzy!" : $"Przegrałeś {postawionePieniądze} pieniędzy!";

            coin.Source = wynikLosowania == 0 ? new BitmapImage(new Uri("images/coin/reszka.png", UriKind.Relative)) : new BitmapImage(new Uri("images/coin/orzel.png", UriKind.Relative));
            if (wynikLosowania == znak)
            {
                konta.UpdateTokens(Convert.ToInt32(postawionePieniądze * 2));
            }


            coWypadlo.Text = wynikLosowania == 0 ? "Co wypadło: tail" : "Co wypadło: head";
            postawionePieniądze = 0;
            Zetony.Value = 1;
            coin.Visibility = Visibility.Visible;
            coinFlip.Visibility = Visibility.Collapsed;
            Zetony.IsEnabled = true;
            coin.IsEnabled = false;
        }


    }
}
