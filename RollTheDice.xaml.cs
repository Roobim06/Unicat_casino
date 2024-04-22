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
using WpfAnimatedGif;
using System.Printing;

namespace Unicat_Casino
{
    public partial class RollTheDice : Window
    {
        int bet = 0;
        int postawionePieniądze = 0;
        Random random = new Random();

        public RollTheDice()
        {
            InitializeComponent();
            foreach (Button button in RollButtons.Children)
            {
                button.IsEnabled = false;
            }
        }

        private async void RollClick(object sender, MouseButtonEventArgs e)
        {
            if(bet != 0){
                dice.Visibility = Visibility.Collapsed;
                diceRoll.Visibility = Visibility.Visible;
                int wynikLosowania = random.Next(1, 7);
                await Task.Delay(1500);
                coWypadlo.Text = "Co wypadło: " + wynikLosowania.ToString();
                wynik.Text = bet == wynikLosowania ? "Gratulacje! Wygrałeś pieniądze!" : "Przegrałeś pieniądze!";
                dice.Source = new BitmapImage(new Uri("images/dice/" + wynikLosowania.ToString() + "_dice.png", UriKind.Relative));
                dice.Visibility = Visibility.Visible;
                diceRoll.Visibility = Visibility.Collapsed;
                bet = 0;
                Zetony.IsEnabled = true;


            }
        }
        private void Roll(object sender, RoutedEventArgs e)
        {
            foreach (Button button in RollButtons.Children)
            {
                button.IsEnabled = false;
            }

            Button clickedButton = (Button)sender;
            bet = int.Parse(clickedButton.Content.ToString());

            coPostawiles.Text = "Co postawiłeś: " + bet.ToString();
            Zetony.IsEnabled = false;

        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            Menu okno = new Menu();
            okno.Show();
            this.Close();
        }
        private void ZetonyPostawione(object sender, RoutedEventArgs e)
        {
            foreach (Button button in RollButtons.Children)
            {
                button.IsEnabled = true;
            }
            coWypadlo.Text = "";
            coPostawiles.Text = "";
            wynik.Text = "";
        }
    }
}
