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

namespace Unicat_Casino
{
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        }

        
    }
}
