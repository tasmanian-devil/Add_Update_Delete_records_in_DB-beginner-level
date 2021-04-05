using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_management
{
    public partial class StockMain : Form
    {
        public StockMain()
        {
            InitializeComponent();
        }

        private void produktaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Produktai prod = new Produktai();
            prod.MdiParent = this;//Multiple-doc interface, otherwise shows like two window forms 
            prod.StartPosition = FormStartPosition.CenterScreen;
            prod.Show();
        }
        bool close = true;
        private void StockMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close)
            {
                DialogResult result = MessageBox.Show("Ar tikrai norite išeiti iš programos?", "Išeiti", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (result == DialogResult.Yes)
                {
                    close = false;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void StockMain_Load(object sender, EventArgs e)
        {

        }
    }
}
