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
using System.Xml.Linq;
using static iTextSharp.text.pdf.PdfDocument;

namespace MidTermProjectDB
{
    public partial class projectcrud : UserControl
    {
        public projectcrud()
        {
            InitializeComponent();
        }

        private void projectcrud_Load(object sender, EventArgs e)
        {
           
        }
        private void RefreshProjectDataGrid()
        {
            dataGridView1.Rows.Clear();
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT p.title,p.description from Project p where title not like 'del%' ", con);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Name = reader["Title"].ToString();
                            string desc = reader["Description"].ToString();
                            dataGridView1.Rows.Add(Name, desc);
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Refreshing: " + ex.Message);
            }
        }
        public void getTextBoxValFromGrid()
        {
            try
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                if (row != null)
                {

                    string Name = row.Cells["Title"].Value.ToString();
                    string desc = row.Cells["Description"].Value.ToString();
                    title.Text = Name;
                    des.Text = desc;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error"+ex.Message);
            }
        
        }
        private void ClearTextBoxes()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
            }
        }
        public bool validation()
        {
            if (title.Text == string.Empty || des.Text == string.Empty)
            {
                MessageBox.Show("TextBox Empty", "Error");
                return false;
            }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string titl = title.Text;
            string desc = des.Text;
            if (validation())
            {
                try
                {
                    using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))

                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Project(Title,Description)" +
                           " VALUES (@titl,@desc)", con))
                        {
                            cmd.Parameters.AddWithValue("@titl", titl);
                            cmd.Parameters.AddWithValue("@desc", desc);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully Added");
                            RefreshProjectDataGrid();
                            ClearTextBoxes();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    return;
                }
            }
        }
      
        public void addColumnsToDatagrid()
        {
            dataGridView1.Columns.Add("Title", "Title");
            dataGridView1.Columns.Add("Description", "Description");
        }
        private void readbtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            ClearTextBoxes();
           
            addColumnsToDatagrid();
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT p.Title, p.Description FROM  Project p  where Title NOT LIKE 'del%'", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string Name = reader["Title"].ToString();
                            string desc = reader["Description"].ToString();
                            dataGridView1.Rows.Add(Name, desc);
                        }
                        reader.Close();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error In Read: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("Update Project set Title=@Title, Description=@Description where id in (SELECT Id FROM Project WHERE Title = @Title); ", con))
                    {
                        cmd.Parameters.AddWithValue("@Title", title.Text);
                        cmd.Parameters.AddWithValue("@Description", des.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        RefreshProjectDataGrid();
                        ClearTextBoxes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Updation: " + ex.Message);
            }
        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();

                    if (row != null)
                    {
                        string name = row.Cells["Title"].Value.ToString();
                        using (SqlCommand cmd = new SqlCommand("UPDATE Project SET Title = 'del' + Title WHERE Id IN (SELECT Id FROM Project WHERE Title = @Title) ", con))
                        {
                            cmd.Parameters.AddWithValue("@Title", title.Text);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully deleted");
                            RefreshProjectDataGrid();
                            ClearTextBoxes();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in deletion: " + ex.Message);
            }
        }

        private void cellclick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                if (row != null && e.RowIndex >= 0 && e.ColumnIndex >= 0) // Ensure it's a valid cell click
                {
                    object titleValue = row.Cells["Title"].Value;
                    object descValue = row.Cells["Description"].Value;

                    if (titleValue != null && descValue != null && titleValue != DBNull.Value && descValue != DBNull.Value)
                    {
                        string Name = titleValue.ToString();
                        string desc = descValue.ToString();
                        title.Text = Name;
                        des.Text = desc;
                    }
                    else
                    {
                        MessageBox.Show("Selected cell is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
