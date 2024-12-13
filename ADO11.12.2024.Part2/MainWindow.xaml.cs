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

        private Dictionary<Button, string> selects = null;


        public MainWindow()
        {
            InitializeComponent();
            selects = new Dictionary<Button, string>
            {
                   [allBtn] = "selectAllInfo",
                   [typeProductBtn] = "selectTypeProduct",
                   [supplierBtn] = "selectSupplier",
                   [minCountBtn] = "minCount",
                   [maxCountBtn] = "maxCount",
                   [minPriceBtn] = "minPrice",
                   [maxPriceBtn] = "maxPrice",
                   [productByTypeBtn] = "typeProduct",
                   [productBySupplyBtn] = "supplierProduct",
                   [oldDateProductBtn] = "oldProduct",
                   [avgCountProductBtn] = "avgCountProduct"
            };
        }

        private void OpenConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                MessageBox.Show("Подключено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showSelectQuery(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string parameter = null;

            if (btn == productByTypeBtn ||
                btn == productBySupplyBtn)
            {
                parameter = textBox.Text;
            }

            ReadData(selects[sender as Button], parameter);         
        }
        private void ReadData(string query, string parameter)
        {
            if (connection == null) return;

            using (SqlCommand command
                = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (parameter != null) {
                    command.Parameters.Add("@inputName", SqlDbType.NVarChar, 20)
                        .Value = parameter;
                }
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
                MessageBox.Show("Данные выведены");
                reader.Close();
            }
        }
    }
}

