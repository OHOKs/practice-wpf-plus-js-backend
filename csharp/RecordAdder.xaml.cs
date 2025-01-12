using MySql.Data.MySqlClient;
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

namespace Teszt
{
    public partial class RecordAdder : Window
    {
        private readonly string connectionString = "Server=localhost;Database=ingatlan;Uid=root;Pwd=;";
        public RecordAdder()
        {
            InitializeComponent();
            LoadDropdown();
        }
        private void LoadDropdown()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "SELECT id, nev FROM kategoriak ORDER BY nev";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categoryDropdown.Items.Add(new { Id = reader.GetInt32("id"), Name = reader.GetString("nev") });
                            }
                        }
                    }
                }
                categoryDropdown.DisplayMemberPath = "Name";
                categoryDropdown.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a kategóriák betöltésekor: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO ingatlanok (kategoria, leiras, tehermentes, ar, kepUrl) VALUES (@kategoria, @leiras, @tehermentes, @ar, @kepUrl)";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@kategoria", categoryDropdown.SelectedValue);
                        cmd.Parameters.AddWithValue("@leiras", Leiras.Text.Trim());
                        cmd.Parameters.AddWithValue("@tehermentes", tehermentes.IsChecked == true ? 1 : 0);
                        cmd.Parameters.AddWithValue("@ar", int.Parse(Ar.Text.Trim()));
                        cmd.Parameters.AddWithValue("@kepUrl", url.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Az ingatlan sikeresen rögzítve lett!");
                        ClearInputs();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a rögzítés során: {ex.Message}");
            }
        }

        private bool ValidateInputs()
        {
            //if (cbKategoria.SelectedValue == null)
            //{
            //    MessageBox.Show("Válasszon kategóriát!");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(tbLeiras.Text))
            //{
            //    MessageBox.Show("Adja meg a leírást!");
            //    return false;
            //}

            //if (!int.TryParse(tbAr.Text, out int ar) || ar <= 0)
            //{
            //    MessageBox.Show("Az árnak pozitív egész számnak kell lennie!");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(tbKepUrl.Text))
            //{
            //    MessageBox.Show("Adja meg a kép URL-jét!");
            //    return false;
            //}

            return true;
        }
        private void ClearInputs()
        {
            //cbKategoria.SelectedIndex = -1;
            //tbLeiras.Clear();
            //chkTehermentes.IsChecked = false;
            //tbAr.Clear();
            //tbKepUrl.Clear();
        }
    }
}
