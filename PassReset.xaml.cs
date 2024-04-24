using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MailKit.Net.Smtp;
using MimeKit;
using System.Security.Cryptography;
using MailKit.Security;

namespace Unicat_Casino
{
    public partial class PassReset : Window
    {
        string connectionString = "Data Source=(local);Initial Catalog=UniCatCasino_BaziaDanych;Integrated Security=True; TrustServerCertificate=True";
        string code = "";
        string email = "";

        public PassReset()
        {
            InitializeComponent();
        }
        private string GenerateRandomUpperCaseLetters()
        {
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                char randomChar = (char)random.Next('A', 'Z' + 1);
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (emailenter.Foreground == Brushes.Red)
            {
                MessageBox.Show("Wprowadz prawdziwy email");
            }
            else
            {
                bool foundmail = false;
                try
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    string query = "SELECT Email FROM [User] WHERE CONVERT(varchar(max), Email) = @Mail;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Mail", emailenter.Text);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            email = Convert.ToString(reader["Email"]);
                            foundmail = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nieznaleziono emaila.", "Information");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error");
                }
                if (foundmail)
                {
                    code = GenerateRandomUpperCaseLetters();
                    string emailsend = "damig@onet.pl";
                    string password = "45*54(qwer";
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Resetowanie hasla", emailsend));
                    message.To.Add(new MailboxAddress("Odbiorca", email));
                    message.Subject = "Resetowanie hasła";
                    Random random = new Random();
                    message.Body = new TextPart("plain")
                    {
                        Text = "Kod do zresetowania hasła: " + code
                    };
                    using var client = new SmtpClient();
                    client.Connect("smtp.poczta.onet.pl", 587, false);
                    client.Authenticate(emailsend, password);
                    client.Send(message);
                    client.Disconnect(true);
                    stage1.Visibility = Visibility.Collapsed;
                    stage2.Visibility = Visibility.Visible;
                }

                
            }
        }
        private void emailenter_TextChanged(object sender, TextChangedEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(codeenter.Text == code)
            {
                stage2.Visibility = Visibility.Collapsed;
                stage3.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("niepoprawny kod");
                MainWindow okno = new MainWindow(true);
                okno.Show();
                this.Close();
            }
        }

        private void passenter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (Regex.IsMatch(textBox.Text, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"))
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(passenter.Foreground == Brushes.Red)
            {
                MessageBox.Show("Wpisz poprawne hasło");
            }
            else
            {
                try
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    string query = "UPDATE [dbo].[User] SET Password = @Pass WHERE CAST(Email AS nvarchar(max)) = @Mail;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Mail", email);
                    command.Parameters.AddWithValue("@Pass", HashPassword(passenter.Text));
                    SqlDataReader reader = command.ExecuteReader();
                    MessageBox.Show("Zmieniono hasło");
                    MainWindow okno = new MainWindow(true);
                    okno.Show();
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error");
                }
            }
        }

        private void ClickCancelButton(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow(true);
            okno.Show();
            this.Close();
        }
    }
}

