using Ruletka;
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
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using static System.Formats.Asn1.AsnWriter;

namespace Unicat_Casino
{
    public partial class Menu : Window
    {
        int latestSentence = 0;
        Random rnd = new Random();
        string[] sentences = [ "Paypal me money!",
            "Co chcesz wylicytować? meow",
            "Oddaj mi całe swoje oszczędności! meow",
            "Oddawaj hajs! meow",
            "Meow meow meow meow",
            "Jestem sobie uni tak, a piękność to mój znak!",
            "Uni Uni Uni Uni Uni Uni!",
            "Hahahahah meow! mało brakowało!",
            "Money money money! Must be funny!",
            "jestem najbogatszym kotem!",
            "Otwórz przede mną portfel!",
            "Daj złotóweczkę!",
            "Hsssssssssssssss!",
            "Kiss kiss kiss kiss",
            "Lubię tuńczyka!",
            "Głupcze! Byle śmiertelnik nie pokona boskiego Uni!",
            "Mlem",
            "チキンナゲットが好き"
            ];
        public Menu()
        {
            InitializeComponent();
            MenuUniImg.Visibility = Visibility.Visible;
            MainMenu.Visibility = Visibility.Visible;
            Datacontexthere.DataContext = konta.konto;
            UniSay();
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz wyjść z programu?", "Prośba o wyjście", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        private void ClickLog(object sender, RoutedEventArgs e){
          konta.konto = null;
            MainWindow okno = new MainWindow(true);
            okno.Show();
            this.Close();
        }
        private void ClickSettings(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.Show();
            this.Close();
        }
        private void ClickChooseGame(object sender, RoutedEventArgs e)
        {
            MenuUniImg.Visibility = Visibility.Collapsed;
            MainMenu.Visibility = Visibility.Collapsed;

            GamesPanel.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;

        }

        private void ClickCancelButton(object sender, RoutedEventArgs e) 
        {
            MenuUniImg.Visibility = Visibility.Visible;
            MainMenu.Visibility = Visibility.Visible;

            GamesPanel.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
        }

        private void FilpTheCoinGame(object sender, RoutedEventArgs e)
        {
            FlipTheCoin flipTheCoinGame = new FlipTheCoin();
            flipTheCoinGame.Show();
            this.Close();
        }
        private void OdpalRuletke(object sender, RoutedEventArgs e)
        {
            roulette okno = new roulette();
            okno.Show();
            this.Close();
        }

        private void OdpalBlackJack(object sender, RoutedEventArgs e)
        {
            BlackJack okno = new BlackJack();
            okno.Show();
            this.Close();
        }

        private void OdpalRollTheDice(object sender, RoutedEventArgs e)
        {
            RollTheDice okno = new RollTheDice();
            okno.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MniejszeWieksze okno = new MniejszeWieksze();
            okno.Show();
            this.Close();
        }


        private async void UniSay()
        {
            while (true)
            {
                await Task.Delay(4500);
                while (true)
                {
                    int newsentence = rnd.Next(0, sentences.Length);
                    if (latestSentence != newsentence)
                    {
                        uniSay.Text = sentences[newsentence];
                        latestSentence = rnd.Next(0, sentences.Length);
                        break;
                    }
                }
            }
        }
    }
}
