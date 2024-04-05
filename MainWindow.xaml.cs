using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Unicat_Casino
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            /*
             * polaczenie sie z baza danych
             * sprawdzenie czy podany login ma haslo
             * dekrypcja hasla
             * sprawdzanie warunku haslo = dane wpisane przez uzytkownika
             * jesi wszystko dobrze to okno jest zamykane i przechodziny do menu window
             */


            Menu menuWindow = new Menu();
            menuWindow.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Visible;
            LoginPanel.Visibility = Visibility.Collapsed;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Collapsed;
            LoginPanel.Visibility = Visibility.Visible;
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            /*
             * polaczenie sie z baza danych
             * jesli haslo dobre email nie zostal wprowadzony
             * tworzenie uzytkownika i zapis jego danych lokalnie
             * jesi wszystko dobrze to okno jest zamykane i przechodziny do menu window
             */
            Menu menuWindow = new Menu();
            menuWindow.Show();
            this.Close();
        }
    }
}