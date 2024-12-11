using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO11._12._2024
{
    public partial class Form1 : Form
    {
        /*private SqlConnection conn = null;
        private SqlDataReader reader = null;
        private DataTable table = null;*/

        private SqlConnection conn = null;
        private SqlDataAdapter adapter = null;
        DataSet ds = null;
        SqlCommandBuilder cmd = null;
        const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentData;";
        public Form1()
        {
            InitializeComponent();

            /*DataTable table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("FirstName");
            table.Columns.Add("LastName");
            DataRow row = table.NewRow();
            row[0] = 1;
            row[1] = "Ivan";
            row[2] = "Ivanov";

            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = 2;
            row[1] = "Petr";
            row[2] = "Petrov";

            table.Rows.Add(row);


            dataGridView1.DataSource = table;*/

            /*           conn = new SqlConnection();
                       conn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentData;";
           */

            conn = new SqlConnection(connectionString);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            /*try
            {
                SqlCommand comm = new SqlCommand();
                //comm.CommandText = "select * from Students";
                comm.CommandText = textBox1.Text;
                comm.Connection = conn;
                conn.Open();

                table = new DataTable();
                reader = comm.ExecuteReader();

                int line = 0;
                do
                {
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
                            //row[i] = reader.GetString(i);
                        }
                        table.Rows.Add(row);
                    }
                } while (reader.NextResult());

                dataGridView1.DataSource = table;

                reader.Close();
                conn.Close();
            }
            catch { }*/

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                ds = new DataSet();
                string sql = textBox1.Text;
                adapter = new SqlDataAdapter(sql, conn);
                dataGridView1.DataSource = null;

                cmd = new SqlCommandBuilder(adapter);
                adapter.Fill(ds, "myStudents");
                dataGridView1.DataSource = ds.Tables["myStudents"];

                Debug.WriteLine(cmd.GetInsertCommand().CommandText);
                Debug.WriteLine(cmd.GetUpdateCommand().CommandText);
                Debug.WriteLine(cmd.GetDeleteCommand().CommandText);
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adapter.Update(ds, "myStudents");
        }
    }
}
