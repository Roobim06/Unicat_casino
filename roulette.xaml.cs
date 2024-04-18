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
using System.Threading.Tasks;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Unicat_Casino;

namespace Ruletka
{
    /// <summary>
    /// Logika interakcji dla klasy roulette.xaml
    /// </summary>
    /// heehehehe
    public partial class roulette : System.Windows.Window
    {
        string currentbet = "";
        int postawionezetony = 0;
        string zaklad = "";
        int wylosowanaliczba = 0;
        int przelicznik = 0;
        bool evenoddwin = false;
        int wygranezetony = 0;
        bool win = false;
        int[] reds = [32, 4, 21, 25, 34, 27, 56, 30, 23, 5, 16, 1, 14, 9, 18, 7, 12, 3];
        int[] blacks = [15, 19, 2, 17, 6, 13, 11, 8, 10, 24, 33, 20, 31, 22, 29, 28, 35, 26];
        public roulette()
        {
            InitializeComponent();
            sliderzetony.Maximum = konta.konto.Tokens;
        }
        private void updateinfo()
        {
            info.Content = "Postawiono " + postawionezetony + " na " + currentbet;
        }

        private void table_Click(object sender, RoutedEventArgs e)
        {
            if(konta.konto.Tokens == 0)
            {
                MessageBox.Show("nie stac cie biedaku");
                Unicat_Casino.Menu okno = new Unicat_Casino.Menu();
                okno.Show();
                this.Close();
            }
            var objekt = (sender as System.Windows.Controls.Button);
            currentbet = objekt.Name;
            zaklad = objekt.Uid;
            updateinfo();
        }

        private void sliderzetony_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sliderzetony.Maximum = konta.konto.Tokens;
            postawionezetony = Convert.ToInt32(sliderzetony.Value);
            żetony.Content = postawionezetony;
        }
        private void handlewin()
        {
            if (win)
            {
                wygranezetony = postawionezetony * przelicznik;
                if (evenoddwin)
                {
                    wygranezetony += 1;
                }
                MessageBox.Show("Gratulacje! wygrales " + wygranezetony + " zetonow");
                konta.UpdateTokens(wygranezetony);
            }
            else
            {
                wygranezetony = 0;
                MessageBox.Show("Przegrales! tracisz wszystkie zetony");
            }
            win = false;
            evenoddwin = false;
            currentbet = "";
            postawionezetony = 0;
            zaklad = "";
            wylosowanaliczba = 0;
            przelicznik = 0;
            wygranezetony = 0;
            sliderzetony.Value = 0;
            info.Content = "Jeszcze nic nie postawiono";
        }
        private void ShowGif()
        {
            spin.Visibility = Visibility.Visible;
            nospin.Visibility = Visibility.Collapsed;
        }

        private void HideGif()
        {
            nospin.Visibility = Visibility.Visible;
            spin.Visibility = Visibility.Collapsed;
        }
        private async void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (postawionezetony == 0 || currentbet == "")
            {
                MessageBox.Show("Postaw zaklad");
            }
            else
            {
                konta.UpdateTokens(postawionezetony * -1);
                ShowGif();
                sliderzetony.IsEnabled = false;
                temp.IsEnabled = false;
                await Task.Delay(5000);
                HideGif();
                temp.IsEnabled = true;
                sliderzetony.IsEnabled = true;
                Random rand = new Random();
                int wylosowanaliczba = rand.Next(0, 37);
                string imageName = "roulettewheel_" + wylosowanaliczba + ".png";
                string imagePath = "images\\roulette\\" + imageName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                nospin.Source = bitmap;
                if (zaklad == "first 12")
                {
                    if (wylosowanaliczba >= 1 && wylosowanaliczba <= 12)
                    {
                        win = true;
                        przelicznik = 2;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "second 12")
                {
                    if (wylosowanaliczba >= 13 && wylosowanaliczba <= 24)
                    {
                        win = true;
                        przelicznik = 2;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "third 12")
                {
                    if (wylosowanaliczba >= 25 && wylosowanaliczba <= 36)
                    {
                        win = true;
                        przelicznik = 2;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "1-18")
                {
                    if (wylosowanaliczba >= 1 && wylosowanaliczba <= 18)
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "19-36")
                {
                    if (wylosowanaliczba >= 19 && wylosowanaliczba <= 36)
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "odd")
                {
                    if (wylosowanaliczba % 2 == 1)
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "even")
                {
                    if (wylosowanaliczba % 2 == 0)
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "red")
                {
                    if (reds.Contains(wylosowanaliczba))
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "black")
                {
                    if (blacks.Contains(wylosowanaliczba))
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "1-3")
                {
                    if (wylosowanaliczba >= 1 && wylosowanaliczba <= 3)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "4-6")
                {
                    if (wylosowanaliczba >= 4 && wylosowanaliczba <= 6)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "7-9")
                {
                    if (wylosowanaliczba >= 7 && wylosowanaliczba <= 9)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "10-12")
                {
                    if (wylosowanaliczba >= 10 && wylosowanaliczba <= 12)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "13-15")
                {
                    if (wylosowanaliczba >= 13 && wylosowanaliczba <= 15)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "16-18")
                {
                    if (wylosowanaliczba >= 16 && wylosowanaliczba <= 18)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "19-21")
                {
                    if (wylosowanaliczba >= 19 && wylosowanaliczba <= 21)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "22-24")
                {
                    if (wylosowanaliczba >= 22 && wylosowanaliczba <= 24)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "25-27")
                {
                    if (wylosowanaliczba >= 25 && wylosowanaliczba <= 27)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "28-30")
                {
                    if (wylosowanaliczba >= 28 && wylosowanaliczba <= 30)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "31-33")
                {
                    if (wylosowanaliczba >= 31 && wylosowanaliczba <= 33)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "34-36")
                {
                    if (wylosowanaliczba >= 34 && wylosowanaliczba <= 36)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else
                {
                    if (wylosowanaliczba == Convert.ToInt32(zaklad))
                    {
                        win = true;
                        przelicznik = 35;
                    }
                    else
                    {
                        win = false;
                    }
                }
                handlewin();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Unicat_Casino.Menu okno = new Unicat_Casino.Menu();
            okno.Show();
            this.Close();
        }
    }
}
