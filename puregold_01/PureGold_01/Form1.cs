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
using MySql.Data.MySqlClient;
namespace PureGold_01
{
    public partial class PureGold : Form
    {

        MySqlConnection con = new MySqlConnection(
        "datasource=localhost;port=3306;username=root;password=;database=puregold_pos");
        MySqlCommand cmd;
        MySqlDataAdapter adapt;

        decimal sessionTotalSales = 0;

        public PureGold() { 
            InitializeComponent();
            this.KeyPreview = true;
            timer1_Tick(null, null);
            LoadTotalSales();
        }


     
       

    

       

      

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e) { }

        private void CalculateTotal()
        {
            decimal grandTotal = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[2].Value != null && row.Cells[3].Value != null)
                {
                    decimal price = Convert.ToDecimal(row.Cells[2].Value);
                    int qty = Convert.ToInt32(row.Cells[3].Value);

                    grandTotal += (price * qty);
                }
            }

            labelAmount.Text = grandTotal.ToString("C2");
        }

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                try
                {
                    con.Open();
                    string query = "SELECT product_id, product_name, price FROM products WHERE product_code = @code";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@code", txtProductCode.Text.Trim());

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string id = dr["product_id"].ToString();
                            string name = dr["product_name"].ToString();
                            decimal price = Convert.ToDecimal(dr["price"]);
                            bool exists = false;

                            // Check if product is already in the DataGridView
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == id)
                                {
                                    // Product found! Increase quantity (Column Index 3)
                                    int currentQty = Convert.ToInt32(row.Cells[3].Value);
                                    row.Cells[3].Value = currentQty + 1;
                                    exists = true;
                                    break;
                                }
                            }

                            // If it's a new product, add a new row with Qty = 1
                            if (!exists)
                            {
                                // Order: ID (0), Name (1), Price (2), Qty (3)
                                dataGridView1.Rows.Add(id, name, price, 1);
                            }

                            CalculateTotal();
                        }
                        else
                        {
                            MessageBox.Show("Not Found");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                    txtProductCode.Clear();
                }
            }
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtProductCode.Text += btn.Text;

            txtProductCode.Focus();

            txtProductCode.SelectionStart = txtProductCode.Text.Length;
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtProductCode.Text += btn.Text;

            txtProductCode.Focus();

            txtProductCode.SelectionStart = txtProductCode.Text.Length;
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtProductCode.Text += btn.Text;

            txtProductCode.Focus();

            txtProductCode.SelectionStart = txtProductCode.Text.Length;
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtProductCode.Text += btn.Text;

            txtProductCode.Focus();

            txtProductCode.SelectionStart = txtProductCode.Text.Length;
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtProductCode.Text += btn.Text;

            txtProductCode.Focus();

            txtProductCode.SelectionStart = txtProductCode.Text.Length;
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtProductCode.Text += btn.Text;

            txtProductCode.Focus();

            txtProductCode.SelectionStart = txtProductCode.Text.Length;
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtProductCode.Text += btn.Text;

            txtProductCode.Focus();

            txtProductCode.SelectionStart = txtProductCode.Text.Length;
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtProductCode.Text += btn.Text;

            txtProductCode.Focus();

            txtProductCode.SelectionStart = txtProductCode.Text.Length;
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtProductCode.Text += btn.Text;

            txtProductCode.Focus();

            txtProductCode.SelectionStart = txtProductCode.Text.Length;
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtProductCode.Text += btn.Text;

            txtProductCode.Focus();

            txtProductCode.SelectionStart = txtProductCode.Text.Length;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Length > 0)
            {
                txtProductCode.Text = txtProductCode.Text.Substring(0, txtProductCode.Text.Length - 1);
            }
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtProductCode.Text += btn.Text;

            txtProductCode.Focus();

            txtProductCode.SelectionStart = txtProductCode.Text.Length;
        }

        private void LoadTotalSales()
        {
            try
            {
                con.Open();
                string query = "SELECT COALESCE(SUM(total_purchase), 0) FROM customers";
                MySqlCommand cmd = new MySqlCommand(query, con);
                decimal total = Convert.ToDecimal(cmd.ExecuteScalar());
                sessionTotalSales = total;
                labelTotalSales.Text = total.ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading total sales: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void PureGold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (dataGridView1.Rows.Count == 0 || (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
                {
                    MessageBox.Show("No items in transaction!", "Empty Receipt");
                    return;
                }

                // Show the Payment Panel
                panelPayment.Visible = true;
                panelPayment.BringToFront();

                // Set the Total Due on the payment panel (assuming you have a label there)
                // lblTotalAmountDue.Text = labelAmount.Text;

                // Ready for typing amount
                txtAmountTendered.Clear();
                txtAmountTendered.Focus();
                txtAmounttoPay.Text = labelAmount.Text;
            }

            if (e.KeyCode == Keys.F2)
            {
                dataGridView1.Rows.Clear();
                labelAmount.Text = "$0.00";
                txtAmountTendered.Clear();
                txtProductCode.Clear();
                panelPayment.Visible = false;
                txtProductCode.Focus();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // labelTime will show: 11:45:30 AM
            labelTime.Text = DateTime.Now.ToString("hh:mm:ss tt");

            // labelDate will show: Thursday, April 02, 2026
            labelDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
        }

        private void label18_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCompleteSale_Click(object sender, EventArgs e)
        {
            // ---  DATA PREPARATION & CALCULATIONS ---

            // Get the Grand Total from the label and clean up the string
            string currentTotalStr = labelAmount.Text.Replace("$", "").Trim();
            decimal currentTransactionTotal = Convert.ToDecimal(currentTotalStr);

            // Get the Amount Tendered from the user input
            decimal amountTendered = 0;
            if (!decimal.TryParse(txtAmountTendered.Text, out amountTendered))
            {
                MessageBox.Show("Please enter a valid amount tendered.");
                return;
            }

            // Calculate the Change
            decimal change = amountTendered - currentTransactionTotal;

            // Check if the payment is enough
            if (change < 0)
            {
                MessageBox.Show("Insufficient amount tendered!");
                return;
            }

            // ---  BUILD THE RECEIPT STRING ---

            string receipt = "--- PURE GOLD POS ---\n";
            receipt += "Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "\n";
            receipt += "---------------------------\n";
            receipt += "ITEM          QTY     PRICE\n";

            // Loop through GridView rows to add items to the receipt string
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string name = row.Cells[1].Value.ToString();
                    string price = row.Cells[2].Value.ToString();
                    string qty = row.Cells[3].Value.ToString();

                    // Formats the line (e.g., Bread  x2  ₱5.00)
                    receipt += $"{name}  x{qty}  {price}\n";
                }
            }

            // ---  ADD PAYMENT FOOTER TO RECEIPT ---

            receipt += "---------------------------\n";
            receipt += $"TOTAL AMOUNT:    {currentTransactionTotal:C2}\n";
            receipt += $"CASH TENDERED:   {amountTendered:C2}\n";
            receipt += $"CHANGE:          {change:C2}\n";
            receipt += "---------------------------\n";
            receipt += "THANK YOU FOR SHOPPING!";

            // Display the Official Receipt pop-up
            MessageBox.Show(receipt, "Official Receipt");

            // Update the running total of sales for the session
            sessionTotalSales += currentTransactionTotal;
            labelTotalSales.Text = sessionTotalSales.ToString("C2");

            // ---  DATABASE OPERATIONS ---

            try
            {
                con.Open();

                // Insert into 'customers' table and get the new ID
                string queryCustomer = "INSERT INTO customers (quantity, total_purchase, transaction_time) VALUES (@qty, @total, @txnTime); SELECT LAST_INSERT_ID();";
                MySqlCommand cmdCust = new MySqlCommand(queryCustomer, con);

                // Sum up total quantity of items for the database
                int totalQty = 0;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (r.Cells[3].Value != null) totalQty += Convert.ToInt32(r.Cells[3].Value);
                }

                cmdCust.Parameters.AddWithValue("@qty", totalQty);
                cmdCust.Parameters.AddWithValue("@total", currentTransactionTotal);
                cmdCust.Parameters.AddWithValue("@txnTime", DateTime.Now);

                // ExecuteScalar returns the ID from LAST_INSERT_ID()
                int newCustomerId = Convert.ToInt32(cmdCust.ExecuteScalar());

                // Loop through Grid and Insert each item into 'purchase' table
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        string queryPurchase = "INSERT INTO purchase (customer_id, product_id) VALUES (@cId, @pId)";
                        MySqlCommand cmdPurch = new MySqlCommand(queryPurchase, con);
                        cmdPurch.Parameters.AddWithValue("@cId", newCustomerId);
                        cmdPurch.Parameters.AddWithValue("@pId", row.Cells[0].Value);
                        cmdPurch.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            // --- UI RESET ---

            // Clear the transaction data to prepare for the next customer
            dataGridView1.Rows.Clear();
            labelAmount.Text = "$0.00";
            txtAmountTendered.Clear();
            txtProductCode.Clear();
            panelPayment.Visible = false;
            txtProductCode.Focus();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void panel45_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtAmounttoPay_Click(object sender, EventArgs e)
        {


            
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (dataGridView1.CurrentCell != null)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelPayment.Visible = false;
            txtAmountTendered.Clear();
        }
    }
}
