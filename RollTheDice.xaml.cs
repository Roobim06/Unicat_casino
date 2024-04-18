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

namespace Unicat_Casino
{
    public partial class RollTheDice : Window
    {
        // 1 - orzeł, 0 - reszka
        int znak = 1;
        int postawionePieniądze = 0;
        Random random = new Random();

        public RollTheDice()
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

           
        }
    }
}
