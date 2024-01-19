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
using System.Data.OleDb;

namespace ATMsoftware
{
    public partial class Balance : Form
    {
        public Balance()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        SqlConnection con = new SqlConnection("Data Source = AKI2404; Initial Catalog = atmtable; Integrated Security = True;");
        private void getbalance()
        {
            con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("SELECT Balance FROM acntTable WHERE AccNum = @AccNum", con);
            sda.SelectCommand.Parameters.AddWithValue("@AccNum", label7.Text);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                label6.Text = "Rs " + dt.Rows[0]["Balance"].ToString();
            }
            else
            {
                label6.Text = "N/A";
            }

            con.Close();
        }

        private void Balance_Load(object sender, EventArgs e)
        {
            label7.Text = HOME.AccNumber;
            getbalance();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            home.Show();
            this.Close();
        }

    }
}
