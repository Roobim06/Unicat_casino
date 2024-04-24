using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Numerics;

namespace Unicat_Casino
{
    public partial class MainWindow : Window
    {
        string connectionString = "Data Source=(local);Initial Catalog=UniCatCasino_BaziaDanych;Integrated Security=True; TrustServerCertificate=True";
        public MainWindow()
        {
            InitializeComponent();

            MusicPlayer okno = new MusicPlayer();
            okno.Show();
        }

        public MainWindow(bool abrbr)
        {
            InitializeComponent();
        }

        static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute hash from the password
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string representation
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // Convert to hexadecimal format
                }
                return builder.ToString();
            }
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
            if (NickLog.Text != "" && Passlog2.Text != "")
            {
                bool loggedin = false;
                try
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    string query = "SELECT Nick, Tokens FROM [User] WHERE CONVERT(varchar(max), Nick) = @Nick AND CONVERT(varchar(max), Password) = @Password;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nick", NickLog.Text);
                    command.Parameters.AddWithValue("@Password", HashPassword(PassLog.Password));
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            MessageBox.Show("Udało sie zalogowac", "Pomyslne zalogowanie");
                            konta.konto = new User(Convert.ToString(reader["Nick"]), Convert.ToInt32(reader["Tokens"]));
                            loggedin = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Błedne dane wprowadzone.", "Information");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error");
                }
                if (loggedin)
                {
                    Menu menuWindow = new Menu();
                    menuWindow.Show();
                }
            }
            
        }

        private void GoMenu(object sender, RoutedEventArgs e)
        {
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
            if(EmailEnter.Foreground == Brushes.Red||PasswordEnter.Foreground == Brushes.Red)
            {
                MessageBox.Show("Email lub hasło nie jest poprawne", "Information");
            }
            else
            {
                bool emailtaken = false;
                bool nicktaken = false;
                bool loggedin = false;
                try
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    string query = "SELECT * FROM [User]";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (EmailEnter.Text == Convert.ToString(reader["Email"]))
                        {
                            emailtaken = true;
                        }
                        if (NickEnter.Text == Convert.ToString(reader["Nick"]))
                        {
                            nicktaken = true;
                        }
                    }
                    reader.Close();
                    if (emailtaken)
                    {
                        MessageBox.Show("Email jest juz wykorzystany");
                    }
                    else if (nicktaken)
                    {
                        MessageBox.Show("Nick jest juz wykorzystany");
                    }
                    else
                    {
                        query = "insert into[dbo].[User] values('" + NickEnter.Text + "', '" + EmailEnter.Text + "', '" + HashPassword(PasswordEnter.Password) + "', 200)";
                        command = new SqlCommand(query, connection);
                        reader = command.ExecuteReader();
                        MessageBox.Show("Założono konto");
                        konta.konto = new User(NickEnter.Text, 200);
                        loggedin = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd");
                }
                if (loggedin)
                {
                    Menu menuWindow = new Menu();
                    menuWindow.Show();
                    this.Close();
                }
            }   
        }

        private void EmailEnter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (Regex.IsMatch(textBox.Text, "[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}"))
            {
                ToolTipService.SetToolTip(textBox, null);
                textBox.Foreground = Brushes.Black;
            }
            else
            {
                ToolTip tooltip = new ToolTip();
                tooltip.Content = "Musi byc prawdziwy email";
                ToolTipService.SetToolTip(textBox, tooltip);
                textBox.Foreground = Brushes.Red;
            }
        }


        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PassReset okno = new PassReset();
            okno.Show();
            this.Close();
        }
        private void PasswordEnter_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = sender as PasswordBox;
            if (Regex.IsMatch(textBox.Password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"))
            {
                ToolTipService.SetToolTip(textBox, null);
                textBox.Foreground = Brushes.Black;
            }
            else
            {
                ToolTip tooltip = new ToolTip();
                tooltip.Content = "Hasło musi posiadac 1 duza litere, 1 liczbe, 1 znak specialny i 8 znakow";
                ToolTipService.SetToolTip(textBox, tooltip);
                textBox.Foreground = Brushes.Red;
            }
        }

        private void RememberMe(object sender, RoutedEventArgs e)
        {
            CheckBox checks = sender as CheckBox;
            if (checks.IsChecked == true)
            {
                Passlog2.Text = PassLog.Password;
                PassLog.Visibility = Visibility.Collapsed;
                Passlog2.Visibility = Visibility.Visible;
            }
            if(checks.IsChecked == false)
            {
                PassLog.Password = Passlog2.Text;
                Passlog2.Visibility = Visibility.Collapsed;
                PassLog.Visibility = Visibility.Visible;
            }
        }

        private void RememberMe2(object sender, RoutedEventArgs e)
        {
            CheckBox checks = sender as CheckBox;
            if (checks.IsChecked == true)
            {
                PasswordEnter2.Text = PasswordEnter.Password;
                PasswordEnter.Visibility = Visibility.Collapsed;
                PasswordEnter2.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordEnter.Password = PasswordEnter2.Text;
                PasswordEnter2.Visibility = Visibility.Collapsed;
                PasswordEnter.Visibility = Visibility.Visible;
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Zaloguj_gosc(object sender, RoutedEventArgs e)
        {
            konta.konto = new User("Guest",200);
            Menu menuWindow = new Menu();
            menuWindow.Show();
            this.Close();
        }
    }
}