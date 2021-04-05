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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //To Check Login surname & Pass 
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM[dbo].[Login] 
            Where UserName = '"+ textBox1.Text + "' and Password = '" + textBox2.Text + "'", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();//
                main.Show();
            }
            else
            {
                MessageBox.Show("Neteisingas vartotojo vardas arba slaptažodis...", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }
    }
}
