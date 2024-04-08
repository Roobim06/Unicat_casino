﻿using System;
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
using System.Collections.Generic;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace Unicat_Casino
{
    public static class temp
    {
        public static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    
    public partial class BlackJack : Window
    {
        private List<karty> talia = StworzTalie();
        private int currentcard = 0;
        private List<karty> playercards = new List<karty>();
        private List<karty> dealercards = new List<karty>();
        private int playerpoints = 0;
        private int dealerpoints = 0;
        private bool end = false;
        private bool tokenchange = true;
        private int tokens = 0;
        public BlackJack()
        {
            InitializeComponent();
            zetony.Content = 0;
        }
        private void firstturn()
        {
            dobierzkartedealer(false);
            dobierzkartedealer(true);
            dobierzkarteplayer();
            dobierzkarteplayer();
        }
        private void resetgame()
        {
            reset.Visibility = Visibility.Collapsed;
            resetnie.Visibility = Visibility.Collapsed;
            resettak.Visibility = Visibility.Collapsed;
            dobierzkarte.IsEnabled = true;
            zakonczture.IsEnabled = true;
            playercards.Clear();
            dealercards.Clear();
            playerpoints = 0;
            tokens = 0;
            zetony.Content = 0;
            dealerpoints = 0;
            end = false;
            temp.rng = new Random();
            currentcard = 0;
            talia = StworzTalie();
            zetony.Visibility = Visibility.Visible;
            textzetony.Visibility = Visibility.Visible;
            plus.Visibility = Visibility.Visible;
            minus.Visibility = Visibility.Visible;
            zatwierdz.Visibility = Visibility.Visible;
            dobierzkarte.Visibility = Visibility.Collapsed;
            zakonczture.Visibility = Visibility.Collapsed;
            playertable.Visibility = Visibility.Collapsed;
            dealertable.Visibility = Visibility.Collapsed;
            updatecards();
        }
        private static List<karty> StworzTalie()
        {
            List<karty> talia = new List<karty>();

            string[] symbole = { "pik", "kier", "trefl", "karo" };
            string[] numery = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string path = "";
            foreach (string symbol in symbole)
            {
                foreach (string numer in numery)
                {
                    path = "images/cards/" + numer + "_" + symbol + ".png";
                    talia.Add(new karty(symbol, numer, path, true));
                }
            }
            temp.Shuffle(talia);
            return talia;
        }
        private void handleendgame()
        {
            if (end)
            {

            }
            else
            {
                if (playerpoints > dealerpoints)
                {
                    tokens = tokens * 2;
                    reset.Text = "Wygrałes " + tokens + " zetonow\n grasz od nowa?";
                    end = true;
                    dobierzkarte.IsEnabled = false;
                    zakonczture.IsEnabled = false;
                }
                else if(playerpoints < dealerpoints)
                {
                    reset.Text = "Przegrales\n grasz od nowa?";
                    end = true;
                    dobierzkarte.IsEnabled = false;
                    zakonczture.IsEnabled = false;
                }
                else if(playerpoints == dealerpoints)
                {
                    reset.Text = "Remis, twoje zetony wracaja do ciebie\n grasz od nowa?";
                    end = true;
                    dobierzkarte.IsEnabled = false;
                    zakonczture.IsEnabled = false;
                }
            }
            showreset();
        }
        private void showreset()
        {
            reset.Visibility = Visibility.Visible;
            resettak.Visibility = Visibility.Visible;
            resetnie.Visibility = Visibility.Visible;
        }
        private void handleplayerpoints()
        {
            playerpoints = 0;
            foreach(karty karta in playercards)
            {
                if (karta.numer == "K" || karta.numer == "J" || karta.numer == "Q")
                {
                    playerpoints += 10;
                }
                else if (karta.numer == "A")
                {
                    if(playerpoints <= 10)
                    {
                        playerpoints += 11;
                    }
                    else
                    {
                        playerpoints += 1;
                    }
                }
                else
                {
                    playerpoints += Convert.ToInt32(karta.numer);
                }
            }
            if(playerpoints == 21)
            {
                tokens = Convert.ToInt32(tokens * 1.5);
                reset.Text = "Blackjack! Wygrywasz " + tokens + " zetonow\n grasz od nowa?";
                end = true;
                dobierzkarte.IsEnabled = false;
                zakonczture.IsEnabled = false;
                showreset();
            }
            if(playerpoints > 21)
            {
                reset.Text = "Przegrales\n grasz od nowa?";
                end = true;
                dobierzkarte.IsEnabled = false;
                zakonczture.IsEnabled = false;
                showreset();
            }
        }
        private void handledealerpoints()
        {
            dealerpoints = 0;
            foreach (karty karta in dealercards)
            {
                if (karta.numer == "K" || karta.numer == "J" || karta.numer == "Q")
                {
                    dealerpoints += 10;
                }
                else if (karta.numer == "A")
                {
                    if (playerpoints <= 10)
                    {
                        dealerpoints += 11;
                    }
                    else
                    {
                        dealerpoints += 1;
                    }
                }
                else
                {
                    dealerpoints += Convert.ToInt32(karta.numer);
                }
            }
            if (dealerpoints == 21)
            {
                reset.Text = "Przegrales\n grasz od nowa?";
                end = true;
                dobierzkarte.IsEnabled = false;
                zakonczture.IsEnabled = false;
                showreset();
            }
            if (dealerpoints > 21)
            {
                tokens = Convert.ToInt32(tokens * 2);
                reset.Text = "Dealer przegral! Wygrywasz " + tokens + " zetonow\n grasz od nowa?";
                end = true;
                dobierzkarte.IsEnabled = false;
                zakonczture.IsEnabled = false;
                showreset();
            }
        }
        private void updatecards()
        {
            playertable.Children.Clear();
            dealertable.Children.Clear();
            List<String> imagePaths = new List<string>();
            foreach (karty karta in playercards)
            {
                if(karta.odkryte == false)
                {
                    imagePaths.Add("/images/back.png");
                }
                else
                {
                    imagePaths.Add(karta.zdj);
                }
            }

            // Dodawanie obrazów do WrapPanel
            foreach (string path in imagePaths)
            {
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                BitmapImage bitmap = new BitmapImage(new Uri(path, UriKind.Relative));
                image.Source = bitmap;
                //image.Width = 165;
                //image.Height = 240;
                //image.Stretch = Stretch.Fill;
                image.Margin = new Thickness(5);
                playertable.Children.Add(image);
            }
            imagePaths = new List<string>();
            foreach (karty karta in dealercards)
            {
                if (karta.odkryte == false)
                {
                    imagePaths.Add("/images/back.png");
                }
                else
                {
                    imagePaths.Add(karta.zdj);
                }
            }

            // Dodawanie obrazów do WrapPanel
            foreach (string path in imagePaths)
            {
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                BitmapImage bitmap = new BitmapImage(new Uri(path, UriKind.Relative));
                image.Source = bitmap;
                //image.Width = 165;
                //image.Height = 240;
                //image.Stretch = Stretch.Fill;
                image.Margin = new Thickness(5);
                dealertable.Children.Add(image);
            }
        }
        private void dobierzkarteplayer()
        {
            if (currentcard == 51)
            {
                MessageBox.Show("Koniec kart, nie można dobrac wiecej");
            }
            else
            {
                playercards.Add(talia[currentcard]);
                currentcard++;
                updatecards();
                handleplayerpoints();
            }
        }
        private void dobierzkartedealer(bool cover)
        {
            if (cover)
            {
                if (currentcard == 51)
                {
                    MessageBox.Show("Koniec kart, nie można dobrac wiecej");
                }
                else
                {
                    talia[currentcard].odkryte = false;
                    dealercards.Add(talia[currentcard]);
                    currentcard++;
                    updatecards();
                    handledealerpoints();
                }
            }
            else
            {
                if (currentcard == 51)
                {
                    MessageBox.Show("Koniec kart, nie można dobrac wiecej");
                }
                else
                {
                    dealercards.Add(talia[currentcard]);
                    currentcard++;
                    updatecards();
                    handledealerpoints();
                }
            }
        }
        private void koniectury()
        {
            dobierzkarte.IsEnabled = false;
            dealercards[1].odkryte = true;
            while (dealerpoints < 17)
            {
                dobierzkartedealer(false);
            }
            handleendgame();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dobierzkarteplayer();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            koniectury();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            resetgame();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if(tokens >= 2000)
            {
                minus.IsEnabled = true;
                plus.IsEnabled = false;
                zatwierdz.IsEnabled = true;
            }
            else
            {
                tokens += 10;
                updatetokens();
                minus.IsEnabled = true;
                plus.IsEnabled = true;
                zatwierdz.IsEnabled = true;
            }
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if(tokens == 0)
            {
                minus.IsEnabled = false;
                plus.IsEnabled = true;
                zatwierdz.IsEnabled = false;
            }
            else{
                tokens -= 10;
                updatetokens();
                minus.IsEnabled = true;
                plus.IsEnabled = true;
                zatwierdz.IsEnabled = true;
                if (tokens == 0)
                {
                    zatwierdz.IsEnabled = false; 
                    minus.IsEnabled = false;
                }
            }
        }
        private void updatetokens()
        {
            zetony.Content = tokens;
        }

        private void zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            zetony.Visibility = Visibility.Collapsed;
            textzetony.Visibility = Visibility.Collapsed;
            plus.Visibility = Visibility.Collapsed;
            minus.Visibility = Visibility.Collapsed;
            zatwierdz.Visibility = Visibility.Collapsed;
            dobierzkarte.Visibility = Visibility.Visible;
            zakonczture.Visibility = Visibility.Visible;
            playertable.Visibility = Visibility.Visible;
            dealertable.Visibility = Visibility.Visible;
            firstturn();
        }
    }
}