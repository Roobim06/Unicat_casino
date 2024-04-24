using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Unicat_Casino
{
    public partial class MniejszeWieksze : Window
    {
        private List<string> imagePaths = new List<string>
        {
            "\\images\\SlotMachine\\los1.jpg",
            "\\images\\SlotMachine\\los2.jpg",
            "\\images\\SlotMachine\\los3.jpg",
            "\\images\\SlotMachine\\los4.jpg",
            "\\images\\SlotMachine\\los5.jpg",
            "\\images\\SlotMachine\\los6.jpg",
            "\\images\\SlotMachine\\los7.jpg",
            "\\images\\SlotMachine\\los8.jpg"
        };

        private Random random = new Random();
        private DispatcherTimer timer = new DispatcherTimer();
        private int currentImageIndex = 0;

        public MniejszeWieksze()
        {
            InitializeComponent();
            creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int newIndex1 = random.Next(imagePaths.Count);
            int newIndex2 = random.Next(imagePaths.Count);
            int newIndex3 = random.Next(imagePaths.Count);

            slot1.Source = new BitmapImage(new Uri(imagePaths[newIndex1], UriKind.Relative));
            slot2.Source = new BitmapImage(new Uri(imagePaths[newIndex2], UriKind.Relative));
            slot3.Source = new BitmapImage(new Uri(imagePaths[newIndex3], UriKind.Relative));

            currentImageIndex = newIndex1;
        }

        private void SpinButton_Click(object sender, RoutedEventArgs e)
        {
            spinButton.IsEnabled = false;
            timer.Start();

            DispatcherTimer stopTimer = new DispatcherTimer();
            stopTimer.Interval = TimeSpan.FromSeconds(2);
            stopTimer.Tick += (s, args) =>
            {
                timer.Stop();
                stopTimer.Stop();

                spinButton.IsEnabled = true;

                int currentCredits = int.Parse(creditsTextBlock.Text);

                if (currentCredits > 0)
                {
                    konta.UpdateTokens(-2);
                    creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                }
                else
                {
                    MessageBox.Show("Brak kredytów! Wprowadź więcej kredytów, aby kontynuować.", "Brak kredytów", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (slot1.Source.ToString().Contains("los1.jpg") && slot2.Source.ToString().Contains("los1.jpg") && slot3.Source.ToString().Contains("los1.jpg"))
                {
                    konta.UpdateTokens(4000);
                    creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                    MessageBox.Show("Wygrana! Kombinacja: los1 - los1 - los1", "Gratulacje!");
                }
                else if (slot1.Source.ToString().Contains("los2.jpg") && slot2.Source.ToString().Contains("los2.jpg") && slot3.Source.ToString().Contains("los2.jpg"))
                {
                    konta.UpdateTokens(800);
                    creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                    MessageBox.Show("Wygrana! Kombinacja: los2 - los2 - los2", "Gratulacje!");
                }
                else if (slot1.Source.ToString().Contains("los3.jpg") && slot2.Source.ToString().Contains("los3.jpg") && slot3.Source.ToString().Contains("los3.jpg"))
                {
                    konta.UpdateTokens(100);
                    creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                    MessageBox.Show("Wygrana! Kombinacja: los3 - los3 - los3", "Gratulacje!");
                }
                else if (slot1.Source.ToString().Contains("los4.jpg") && slot2.Source.ToString().Contains("los4.jpg") && slot3.Source.ToString().Contains("los4.jpg"))
                {
                    konta.UpdateTokens(50);
                    creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                    MessageBox.Show("Wygrana! Kombinacja: los4 - los4 - los4", "Gratulacje!");
                }
                else if (((slot1.Source.ToString().Contains("los3.jpg") || slot2.Source.ToString().Contains("los3.jpg") || slot3.Source.ToString().Contains("los3.jpg")) &&
                          (slot1.Source.ToString().Contains("los4.jpg") || slot2.Source.ToString().Contains("los4.jpg") || slot3.Source.ToString().Contains("los4.jpg"))) &&
                         !(slot1.Source.ToString().Contains("los1.jpg") || slot2.Source.ToString().Contains("los1.jpg") || slot3.Source.ToString().Contains("los1.jpg")) &&
                         !(slot1.Source.ToString().Contains("los2.jpg") || slot2.Source.ToString().Contains("los2.jpg") || slot3.Source.ToString().Contains("los2.jpg")) &&
                         !(slot1.Source.ToString().Contains("los5.jpg") || slot2.Source.ToString().Contains("los5.jpg") || slot3.Source.ToString().Contains("los5.jpg")) &&
                         !(slot1.Source.ToString().Contains("los6.jpg") || slot2.Source.ToString().Contains("los6.jpg") || slot3.Source.ToString().Contains("los6.jpg")) &&
                         !(slot1.Source.ToString().Contains("los7.jpg") || slot2.Source.ToString().Contains("los7.jpg") || slot3.Source.ToString().Contains("los7.jpg")) &&
                         !(slot1.Source.ToString().Contains("los8.jpg") || slot2.Source.ToString().Contains("los8.jpg") || slot3.Source.ToString().Contains("los8.jpg")))
                {
                    konta.UpdateTokens(40);
                    creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                    MessageBox.Show("Wygrana! Kombinacja: los3 - los4", "Gratulacje!");
                }
                else if (slot1.Source.ToString().Contains("los8.jpg") || slot2.Source.ToString().Contains("los8.jpg") || slot3.Source.ToString().Contains("los8.jpg"))
                {
                    if (slot1.Source.ToString().Contains("los8.jpg") && slot2.Source.ToString().Contains("los8.jpg") && slot3.Source.ToString().Contains("los8.jpg"))
                    {
                        konta.UpdateTokens(20);
                        creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                        MessageBox.Show("Wygrana! Kombinacja: los8 - los8 - los8", "Gratulacje!");
                    }
                    else if ((slot1.Source.ToString().Contains("los8.jpg") ? 1 : 0) + (slot2.Source.ToString().Contains("los8.jpg") ? 1 : 0) + (slot3.Source.ToString().Contains("los8.jpg") ? 1 : 0) == 2)
                    {
                        konta.UpdateTokens(5);
                        creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                        MessageBox.Show("Wygrana! Kombinacja: los8 - los8", "Gratulacje!");
                    }
                    else
                    {
                        konta.UpdateTokens(2);
                        creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                        MessageBox.Show("Wygrana! Kombinacja: los8", "Gratulacje!");
                    }
                }
                else if (((slot1.Source.ToString().Contains("los5.jpg") || slot2.Source.ToString().Contains("los5.jpg") || slot3.Source.ToString().Contains("los5.jpg")) ||
                         (slot1.Source.ToString().Contains("los6.jpg") || slot2.Source.ToString().Contains("los6.jpg") || slot3.Source.ToString().Contains("los6.jpg")) ||
                         (slot1.Source.ToString().Contains("los7.jpg") || slot2.Source.ToString().Contains("los7.jpg") || slot3.Source.ToString().Contains("los7.jpg"))) &&
                         (!(slot1.Source.ToString().Contains("los1.jpg") || slot2.Source.ToString().Contains("los1.jpg") || slot3.Source.ToString().Contains("los1.jpg")) &&
                          !(slot1.Source.ToString().Contains("los2.jpg") || slot2.Source.ToString().Contains("los2.jpg") || slot3.Source.ToString().Contains("los2.jpg")) &&
                          !(slot1.Source.ToString().Contains("los3.jpg") || slot2.Source.ToString().Contains("los3.jpg") || slot3.Source.ToString().Contains("los3.jpg")) &&
                          !(slot1.Source.ToString().Contains("los4.jpg") || slot2.Source.ToString().Contains("los4.jpg") || slot3.Source.ToString().Contains("los4.jpg")) &&
                          !(slot1.Source.ToString().Contains("los8.jpg") || slot2.Source.ToString().Contains("los8.jpg") || slot3.Source.ToString().Contains("los8.jpg"))))
                {
                    konta.UpdateTokens(40);
                    creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                    MessageBox.Show("Wygrana! Kombinacja: los5/los6/los7", "Gratulacje!");
                }
                else if ((slot1.Source.ToString().Contains("los5.jpg") ? 1 : 0) + (slot2.Source.ToString().Contains("los5.jpg") ? 1 : 0) + (slot3.Source.ToString().Contains("los5.jpg") ? 1 : 0) == 3)
                {
                    konta.UpdateTokens(30);
                    creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                    MessageBox.Show("Wygrana! Kombinacja: los5 - los5 - los5", "Gratulacje!");
                }
                else if ((slot1.Source.ToString().Contains("los6.jpg") ? 1 : 0) + (slot2.Source.ToString().Contains("los6.jpg") ? 1 : 0) + (slot3.Source.ToString().Contains("los6.jpg") ? 1 : 0) == 3)
                {
                    konta.UpdateTokens(20);
                    creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                    MessageBox.Show("Wygrana! Kombinacja: los6 - los6 - los6", "Gratulacje!");
                }
                else if ((slot1.Source.ToString().Contains("los7.jpg") ? 1 : 0) + (slot2.Source.ToString().Contains("los7.jpg") ? 1 : 0) + (slot3.Source.ToString().Contains("los7.jpg") ? 1 : 0) == 3)
                {
                    konta.UpdateTokens(10);
                    creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                    MessageBox.Show("Wygrana! Kombinacja: los7 - los7 - los7", "Gratulacje!");
                }
                else if (((slot1.Source.ToString().Contains("los5.jpg") || slot2.Source.ToString().Contains("los5.jpg") || slot3.Source.ToString().Contains("los5.jpg")) &&
                          (slot1.Source.ToString().Contains("los6.jpg") || slot2.Source.ToString().Contains("los6.jpg") || slot3.Source.ToString().Contains("los6.jpg")) &&
                          (slot1.Source.ToString().Contains("los7.jpg") || slot2.Source.ToString().Contains("los7.jpg") || slot3.Source.ToString().Contains("los7.jpg"))) &&
                         (!(slot1.Source.ToString().Contains("los1.jpg") || slot2.Source.ToString().Contains("los1.jpg") || slot3.Source.ToString().Contains("los1.jpg")) &&
                          !(slot1.Source.ToString().Contains("los2.jpg") || slot2.Source.ToString().Contains("los2.jpg") || slot3.Source.ToString().Contains("los2.jpg")) &&
                          !(slot1.Source.ToString().Contains("los3.jpg") || slot2.Source.ToString().Contains("los3.jpg") || slot3.Source.ToString().Contains("los3.jpg")) &&
                          !(slot1.Source.ToString().Contains("los4.jpg") || slot2.Source.ToString().Contains("los4.jpg") || slot3.Source.ToString().Contains("los4.jpg")) &&
                          !(slot1.Source.ToString().Contains("los8.jpg") || slot2.Source.ToString().Contains("los8.jpg") || slot3.Source.ToString().Contains("los8.jpg"))))
                {
                    konta.UpdateTokens(5);
                    creditsTextBlock.Text = Convert.ToString(konta.konto.Tokens);
                    MessageBox.Show("Wygrana! Kombinacja: los5/los6/los7", "Gratulacje!");
                }

            };
            stopTimer.Start();
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            Menu okno = new Menu();
            okno.Show();
            this.Close();
        }
    }
}
