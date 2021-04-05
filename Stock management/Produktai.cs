using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_management
{
    public partial class Produktai : Form
    {
        public Produktai()
        {
            InitializeComponent();
            LoadData();
        }

        private void Produktai_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;//by default show first value: "Yra"
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (Validation())
            {
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=stock;Integrated Security=True");
                connection.Open();
                bool status = false;
                if (comboBox1.SelectedIndex == 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }

                var sqlQuery = "";
                if (IfProductsExists(connection, textBox1.Text))
                {
                    sqlQuery = @"UPDATE [Produktai] SET [Produkto pavadinimas] = '" + textBox2.Text + "' ,[Statusas] = '" + status + "' WHERE [Produkto kodas] = '" + textBox1.Text + "'";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [stock].[dbo].[Produktai] ([Produkto kodas],[Produkto pavadinimas],[Statusas]) VALUES
                            ('" + textBox1.Text + "','" + textBox2.Text + "','" + status + "')";
                }

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                LoadData();
                ResetRecords();
           
            }
        }
        private void ResetRecords()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            button1.Text = "Add";
            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetRecords();
        }
        private bool IfProductsExists(SqlConnection connection, string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select 1 From [Produktai] WHERE [Produkto kodas]='" + productCode + "'", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public void LoadData() 
        {
            
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [stock].[dbo].[Produktai]", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Produkto kodas"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Produkto pavadinimas"].ToString();
                if ((bool)item["Statusas"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Yra";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Nėra";
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)//delete button
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=stock;Integrated Security=True");
            DialogResult dialogResult = MessageBox.Show("Ar tikrai norite ištrintį įrašą?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (Validation())
                {
                    var sqlQuery = "";
                    if (IfProductsExists(connection, textBox1.Text))
                    {
                        connection.Open();
                        sqlQuery = @"DELETE [Produktai] WHERE [Produkto kodas] = '" + textBox1.Text + "'";
                        SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Įrašas neegzistuoja...!");
                    }


                    LoadData();
                    ResetRecords();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button1.Text = "Update";
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Yra")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }

        private bool Validation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Pamiršote įvesti kodą");
                textBox1.Focus();
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Pamiršote įvesti produkto pavadinimą");
                textBox2.Focus();
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox1, "Pamiršote pažymėti statusą");
                comboBox1.Focus();
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}
