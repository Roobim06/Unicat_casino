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

    public partial class Settings : Window
    {
        string theme = "Themes/Dark.xaml";
        public Settings()
        {
            InitializeComponent();
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
