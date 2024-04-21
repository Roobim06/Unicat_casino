using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Unicat_Casino
{
    public class User
    {
        public string Nick { get; set; }
        public int Tokens { get; set; }
        public int cardback { get; set; }
        public User(string Nick, int Tokens)
        {
            this.Nick = Nick;
            this.Tokens = Tokens;
            cardback = 1;
        }
    }
    public static class konta
    {
        static string connectionString = "Data Source=(local);Initial Catalog=UniCatCasino_BaziaDanych;Integrated Security=True; TrustServerCertificate=True";
        public static User konto = null;
        public static void UpdateTokens(int Value)
        {
            if (konto.Nick == "Guest")
            {
                konto.Tokens += Value;
            }
            else
            {
                try
                {
                    konto.Tokens = konto.Tokens + Value;
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    string query = "UPDATE [dbo].[User] \r\nSET Tokens = @Tokens \r\nWHERE CONVERT(varchar(Max), Nick) = @Nick;\r\n";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Tokens", konto.Tokens);
                    command.Parameters.AddWithValue("@Nick", konto.Nick);
                    SqlDataReader reader = command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error");
                }
            }
        }
    }
}
