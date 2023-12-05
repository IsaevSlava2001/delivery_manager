using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace delivery_manager
{
    public partial class ClientOrders : Form
    {
        DataBase dataBase = new DataBase();
        public ClientOrders()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {

            dataGridView1.Columns.Add("Name", "Имя продукта");
            dataGridView1.Columns.Add("Sostav", "Состав");
            dataGridView1.Columns.Add("Product_value", "цена продукта");
            dataGridView1.Columns.Add("Product_Quantity", "Количество продукта в заказе");
            dataGridView1.Columns.Add("FIO_Courier", "ФИО курьера");
            dataGridView1.Columns.Add("Condition", "Условия");
            dataGridView1.Columns.Add("Value", "Цена доставки");
            dataGridView1.Columns.Add("Total_Price", "Итоговая цена");
            dataGridView1.Columns.Add("Time", "Дата");
            dataGridView1.Columns.Add("Rate", "Оценка");

        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(
                record.GetString(0),
                record.GetString(1),
                record.GetString(2),
                record.GetInt32(3),
                record.GetString(4),
                record.GetString(5),
                record.GetString(6),
                record.GetDouble(7),
                record.GetDateTime(8),
                record.GetDouble(9));
        }

        private void RefreshDataGrid(DataGridView dgw, string name, DateTime data)
        {
            string querrystring = $"SELECT * FROM selor('{name}','{data}')";
            Console.WriteLine(querrystring);

            SqlCommand command = new SqlCommand(querrystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
            dataBase.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dateTime = Convert.ToDateTime(dateTimePicker1.Value.ToString().Split(' ')[0] + " 00:00:00");
            string FIO = textBox1.Text;
            Update(FIO, dateTime);
        }

        private void Update(string fIO, DateTime dateTime)
        {
            dataGridView1.Columns.Clear();
            CreateColumns();
            RefreshDataGrid(dataGridView1,fIO,dateTime);
        }
    }
}
