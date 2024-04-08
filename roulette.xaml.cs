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

namespace Ruletka
{
    /// <summary>
    /// Logika interakcji dla klasy roulette.xaml
    /// </summary>
    public partial class roulette : Window
    {
        string currentbet = "";
        int postawionezetony = 0;
        string zaklad = "";
        int wylosowanaliczba = 0;
        int przelicznik = 0;
        bool evenoddwin = false;
        int wygranezetony = 0;
        bool win = false;
        int[] reds = [32, 4, 21, 25, 34, 27, 56, 30, 23, 5, 16, 1, 14, 9, 18, 7, 12, 3];
        int[] blacks = [15, 19, 2, 17, 6, 13, 11, 8, 10, 24, 33, 20, 31, 22, 29, 28, 35, 26];
        public roulette()
        {
            InitializeComponent();
        }
        private void updateinfo()
        {
            info.Content = "Postawiono " + postawionezetony + " na " + currentbet;
        }

        private void table_Click(object sender, RoutedEventArgs e)
        {
            var objekt = (sender as Button);
            currentbet = objekt.Name;
            zaklad = objekt.Uid;
            updateinfo();
        }

        private void sliderzetony_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            postawionezetony = Convert.ToInt32(sliderzetony.Value);
            żetony.Content = postawionezetony;
        }
        private void handlewin()
        {
            if(win)
            {
                wygranezetony = postawionezetony * przelicznik;
                if (evenoddwin)
                {
                    wygranezetony += 1;
                }
                MessageBox.Show("Gratulacje! wygrales " + wygranezetony + " zetonow");
            }
            else
            {
                wygranezetony = 0;
                MessageBox.Show("Przegrales! tracisz wszystkie zetony");
            }
            win = false;
            evenoddwin = false;
            currentbet = "";
            postawionezetony = 0;
            zaklad = "";
            wylosowanaliczba = 0;
            przelicznik = 0;
            wygranezetony = 0;
            sliderzetony.Value = 0;
            info.Content = "Jeszcze nic nie postawiono";
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(postawionezetony == 0 || currentbet == "")
            {
                MessageBox.Show("Postaw zaklad");
            }
            else
            {
                Random rand = new Random();
                wylosowanaliczba= rand.Next(0, 37);
                MessageBox.Show("Wypadła liczba " + wylosowanaliczba);
                if(zaklad == "first 12")
                {
                    if(wylosowanaliczba >=1 && wylosowanaliczba <= 12)
                    {
                        win = true;
                        przelicznik = 2;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "second 12")
                {
                    if (wylosowanaliczba >= 13 && wylosowanaliczba <= 24)
                    {
                        win = true;
                        przelicznik = 2;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "third 12")
                {
                    if (wylosowanaliczba >= 25 && wylosowanaliczba <= 36)
                    {
                        win = true;
                        przelicznik = 2;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "1-18")
                {
                    if (wylosowanaliczba >= 1 && wylosowanaliczba <= 18)
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "19-36")
                {
                    if (wylosowanaliczba >= 19 && wylosowanaliczba <= 36)
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "odd")
                {
                    if (wylosowanaliczba%2 == 1)
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "even")
                {
                    if (wylosowanaliczba % 2 == 0)
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "red")
                {
                    if (reds.Contains(wylosowanaliczba))
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "black")
                {
                    if (blacks.Contains(wylosowanaliczba))
                    {
                        win = true;
                        evenoddwin = true;
                        przelicznik = 1;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "1-3")
                {
                    if (wylosowanaliczba >= 1 && wylosowanaliczba <= 3)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "4-6")
                {
                    if (wylosowanaliczba >= 4 && wylosowanaliczba <= 6)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "7-9")
                {
                    if (wylosowanaliczba >= 7 && wylosowanaliczba <= 9)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "10-12")
                {
                    if (wylosowanaliczba >= 10 && wylosowanaliczba <=12)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "13-15")
                {
                    if (wylosowanaliczba >= 13 && wylosowanaliczba <= 15)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "16-18")
                {
                    if (wylosowanaliczba >= 16 && wylosowanaliczba <= 18)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "19-21")
                {
                    if (wylosowanaliczba >= 19 && wylosowanaliczba <= 21)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "22-24")
                {
                    if (wylosowanaliczba >= 22 && wylosowanaliczba <= 24)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "25-27")
                {
                    if (wylosowanaliczba >= 25 && wylosowanaliczba <= 27)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "28-30")
                {
                    if (wylosowanaliczba >= 28 && wylosowanaliczba <= 30)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "31-33")
                {
                    if (wylosowanaliczba >= 31 && wylosowanaliczba <= 33)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else if (zaklad == "34-36")
                {
                    if (wylosowanaliczba >= 34 && wylosowanaliczba <= 36)
                    {
                        win = true;
                        przelicznik = 11;
                    }
                    else
                    {
                        win = false;
                    }
                }
                else
                {
                    if(wylosowanaliczba == Convert.ToInt32(zaklad))
                    {
                        win = true;
                        przelicznik = 35;
                    }
                    else
                    {
                        win = false;
                    }
                }
                handlewin();
            }
        }
    }
}