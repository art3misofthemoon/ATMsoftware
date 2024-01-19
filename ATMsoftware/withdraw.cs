using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMsoftware
{
    public partial class withdraw : Form
    {
        public withdraw()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = AKI2404; Initial Catalog = atmtable; Integrated Security = True;");
        string Acc = Login.AccNumber;
        int bal;
        private void getbalance()
        {
            con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("SELECT Balance FROM acntTable WHERE AccNum = @AccNum", con);
            sda.SelectCommand.Parameters.AddWithValue("@AccNum", Acc);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                label3.Text = "Balance Rs " + dt.Rows[0]["Balance"].ToString();
            }
            else
            {
                label3.Text = "N/A";
            }
            bal = Convert.ToInt32(dt.Rows[0][0].ToString());

            con.Close();
        }
        private void withdraw_Load(object sender, EventArgs e)
        {
            getbalance();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            home.Show();
            this.Close();
        }

        int newbalance;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else if (Convert.ToInt32(textBox1.Text) <= 0)
            {
                MessageBox.Show("Enter a valid amount");
            }
            else if (Convert.ToInt32(textBox1.Text) > bal)
            {
                MessageBox.Show("Amount below available balance");
            }
            else
            {
                try
                {
                    newbalance = bal - Convert.ToInt32(textBox1.Text);
                    try
                    {
                        con.Open();
                        string query = "UPDATE acntTable SET Balance = @NewBalance WHERE AccNum = @AccNum";
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@NewBalance", newbalance);
                        cmd.Parameters.AddWithValue("@AccNum", Acc);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Withdrawal successful");
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
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
