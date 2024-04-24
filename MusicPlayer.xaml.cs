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
    public partial class MusicPlayer : Window
    {
        public static MediaPlayer mediaPlayer = new MediaPlayer();
        public static bool wasPaused = false;

        public MusicPlayer()
        {
            InitializeComponent();
            Uri uri = new Uri(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "choir.mp3"));
            mediaPlayer.Open(uri);
            mediaPlayer.Play();
        }

    }
}
