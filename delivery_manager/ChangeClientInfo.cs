using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace delivery_manager
{
    public partial class ChangeClientInfo : Form
    {
        public ChangeClientInfo()
        {
            InitializeComponent();
        }
        private void updateComboBox()
        {
            comboBox1.Items.Clear();
            foreach (DataRow row in this.deliveryDataSet.Clients)
            {
                comboBox1.Items.Add(row["ID_Client"]);
            }
        }

        private void ChangeClientInfo_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "deliveryDataSet.Clients". При необходимости она может быть перемещена или удалена.
            this.clientsTableAdapter.Fill(this.deliveryDataSet.Clients);
            updateComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.clientsTableAdapter.Fill(this.deliveryDataSet.Clients);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = deliveryDataSet.Clients.Select($"ID_Client={Convert.ToInt32(comboBox1.SelectedItem)}")[0]["FIO"].ToString();
            textBox2.Text = deliveryDataSet.Clients.Select($"ID_Client={Convert.ToInt32(comboBox1.SelectedItem)}")[0]["Address"].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var RowsToUpdate = from r in deliveryDataSet.Clients
                               let name = r.Field<string>("FIO")
                               let id = r.Field<int>("ID_Client")
                               let address = r.Field<string>("Address")
                               where id == Convert.ToInt32(comboBox1.SelectedItem)
                               select r;
            foreach (var row in RowsToUpdate)
            {
                row.SetField("FIO", textBox1.Text);
                row.SetField("Address", textBox2.Text);
            }
            clientsTableAdapter.Update(deliveryDataSet.Clients);
            updateComboBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < deliveryDataSet.Clients.Rows.Count; i++)
            {
                if (Convert.ToInt32(deliveryDataSet.Clients.Rows[i]["ID_Client"]) == Convert.ToInt32(comboBox1.SelectedItem))
                {
                    deliveryDataSet.Clients.Rows[i].Delete();
                }
            }
            try
            {
                clientsTableAdapter.Update(deliveryDataSet.Clients);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно удалить");
            }
            updateComboBox();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataRow dr = deliveryDataSet.Clients.NewRow();
            dr["FIO"] = textBox3.Text;
            dr["Address"] = textBox4.Text;
            deliveryDataSet.Clients.Rows.Add(dr);
            clientsTableAdapter.Update(deliveryDataSet.Clients);
            updateComboBox();
        }
    }
}
