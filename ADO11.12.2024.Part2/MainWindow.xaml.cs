using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADO11._12._2024.Part2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection connection = null;
        private const string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;
            Initial Catalog=Storage;";

        private const string selectAllInfo = 
            "select I.Id, P.Name ProductName," +
            " S.Name SupplierName, " +
            "T.Name TypeProdName " +
            "from Products P " +
            "join Informations I on P.Id = I.ProductId " +
            "join Suppliers S on I.SuppliersId = S.Id " +
            "join TypeProducts T on T.Id = I.TypeProductId";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (connection == null) return;

            using (SqlCommand command
                = new SqlCommand(selectAllInfo, connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                int line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            table.Columns.Add(reader.GetName(i));
                        }
                        line++;
                    }
                    DataRow row = table.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader[i];
                    }
                    table.Rows.Add(row);
                }
                dataGrid.ItemsSource = table.DefaultView;
                MessageBox.Show("Данный выведены. Честное слово");
            }

        }
    }
}

