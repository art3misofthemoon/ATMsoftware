using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMsoftware
{
    public partial class Deposit : Form
    {
        public Deposit()
        {
            InitializeComponent();
        }

        int oldbalance, newbalance;
        private void getbalance()
        {
            con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("SELECT Balance FROM acntTable WHERE AccNum = @AccNum", con);
            sda.SelectCommand.Parameters.AddWithValue("@AccNum", textBox1.Text);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                oldbalance = Convert.ToInt32("Rs " + dt.Rows[0]["Balance"].ToString());
            }
            else
            {
                oldbalance = -1;
            }

            con.Close();
        }
        private void Deposit_Load(object sender, EventArgs e)
        {

        }

        SqlConnection con = new SqlConnection("Data Source = AKI2404; Initial Catalog = atmtable; Integrated Security = True;");
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || Convert.ToInt32(textBox1.Text) <= 0)
            {
                MessageBox.Show("Enter the amount to be deposited");
            }
            else
            {
                string Acc = Login.AccNumber;
                newbalance = oldbalance + Convert.ToInt32(textBox1.Text);
                try
                {
                    con.Open();
                    string query = "UPDATE acntTable SET Balance = @NewBalance WHERE AccNum = @AccNum";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@NewBalance", newbalance);
                    cmd.Parameters.AddWithValue("@AccNum", Acc);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Deposit successful");
                    con.Close();

                    HOME home = new HOME();
                    home.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            home.Show();
            this.Close();
        }
    }
}
