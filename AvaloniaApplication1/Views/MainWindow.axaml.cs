using Avalonia.Controls;
using System;
using Microsoft.Data.SqlClient;






// nic nie działa
// przeszukałem internet, próbowałem instalować pakiety, pakietów nie znajduje ani z visual studio, ani nie mogę ich pobrać za pomocą komendy
// w przypadku wersji .db nie znajduje w ogóle biblioteki System.Data.SQLite
// przy instalacji pakietu wyskakują błędy, że nie można zainstalować
// wszędzie gdzie sprawdzałem, każą je instalować, jednak każde rozwiązanie nie działa

// ostatecznie korzystając z czata dostaję informację, bym upewnił się że mam zainstalowaną paczkę








namespace AvaloniaApplication1.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void btn_ONClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            WriteData(1, "John Doe");
        }

        private string connectionString = @"Data Source=localhost;Initial Catalog=Database.mdf;Integrated Security=True";
        public void ReadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users";
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int userId = reader.GetInt32(0); // Assuming Id is the first column
                        string userName = reader.GetString(1); // Assuming Name is the second column
                        Console.WriteLine($"User Id: {userId}, Name: {userName}");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        public void WriteData(int userId, string userName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Id, Name) VALUES (@Id, @Name)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", userId);
                command.Parameters.AddWithValue("@Name", userName);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}