using MySql.Data.MySqlClient;
using System;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GepjarmuvekGUI
{
    public partial class MainWindow : Window
    {
        public string ConnectionString = "server=localhost;uid=root;database=gepjarmuvek2";
        public MySqlConnection connection;
        public MySqlCommand command;
        public int currentUserId;

        public MainWindow()
        {
            InitializeComponent();
            connection = new MySqlConnection(ConnectionString);
            LoadList();
        }

        private void HirdetesekBetoltese_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = $"SELECT COUNT(*) as szama FROM jarmu WHERE elado = {Azonosito.Text}";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HiretesekSzama.Content = reader.GetInt32("szama");
            }
            connection.Close();
        }

        private void LoadList()
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = "SELECT id, nev FROM elado";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = reader.GetString("nev");
                item.Tag = reader.GetInt32("id");
                Hirdetok.Items.Add(item);
            }
            connection.Close();
        }

        private void Hirdetok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Hirdetok.SelectedItem is ListBoxItem selectedItem)
            {
                int userId = (int)selectedItem.Tag;
                Azonosito.Text = userId.ToString();
                Tulajdonos.Text = selectedItem.Content.ToString();
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = $"SELECT telefon FROM elado WHERE id = {userId}";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Telefonszam.Text = reader.GetString("telefon");
                }
                connection.Close();

            }
        }
    }
}