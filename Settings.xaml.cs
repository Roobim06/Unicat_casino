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
using System.Windows.Markup;

namespace Unicat_Casino
{

    public partial class Settings : Window
    {
        string theme = "Themes/Dark.xaml";
        public Settings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            konta.UpdateTokens(200);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Unicat_Casino.Menu okno = new Menu();
            okno.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var button = (sender as Button);
            konta.konto.cardback = Convert.ToInt32(button.Uid);
            uno.IsEnabled = true;
            dos.IsEnabled = true;
            tres.IsEnabled = true;
            button.IsEnabled = false;
        }
        private void ChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            AppTheme.ChangeTheme(new Uri(theme, UriKind.Relative));
        }

        private void DarkMode_Checked(object sender, RoutedEventArgs e)
        {
            theme = "Themes/Dark.xaml";
        }

        private void LightMode_Checked(object sender, RoutedEventArgs e)
        {
            theme = "Themes/Light.xaml";
        }
        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            Menu menuWindow = new Menu();
            menuWindow.Show();
            this.Close();
        }

        private void MuteMusic_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MuteMusic(object sender, RoutedEventArgs e)
        {

        }
    }
}
