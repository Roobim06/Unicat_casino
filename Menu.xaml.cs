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
            MenuUniImg.Visibility = Visibility.Visible;
            MainMenu.Visibility = Visibility.Visible;
            Datacontexthere.DataContext = konta.konto;
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
            MainWindow okno = new MainWindow();
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

        
    }
}
