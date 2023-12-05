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
    public partial class Form1 : Form
    {
        DataBase dataBase = new DataBase();
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ans = "";
            string query = $"SELECT * FROM countsumofproducts()";
            SqlCommand command = new SqlCommand(query, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ans += $"Продукт - {reader.GetString(0)}. Количество - {reader.GetInt32(1)}\n";
            }
            reader.Close();
            dataBase.CloseConnection();
            MessageBox.Show(ans);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClientOrders clientOrders = new ClientOrders();
            clientOrders.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button7.Visible = false;
            button8.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button7.Visible = true;
            button8.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button7.Visible = false;
            button8.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DateTime dateTimestart = Convert.ToDateTime(dateTimePicker1.Value.ToString().Split(' ')[0] + " 00:00:00");
            DateTime dateTimefinish = Convert.ToDateTime(dateTimePicker2.Value.ToString().Split(' ')[0] + " 00:00:00");
            if (dateTimestart==dateTimefinish)
            {
                MessageBox.Show("Выбрана одинаковая дата","Внимание!");
            }
            else
            {
                string query = $"Select  dbo.countsumofsails('{dateTimestart}','{dateTimefinish}')";
                SqlCommand command = new SqlCommand(query, dataBase.GetConnection());

                dataBase.OpenConnection();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MessageBox.Show( $"Сумма продаж за данную дату - {reader.GetInt32(0)} рублей.");
                }
                reader.Close();
                dataBase.CloseConnection();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeClientInfo changeClientInfo = new ChangeClientInfo();
            changeClientInfo.Show();
            this.Hide();
        }
    }
}
