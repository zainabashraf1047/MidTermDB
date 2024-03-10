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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace MidTermProjectDB
{
    public partial class Evaluationcrud : UserControl
    {
        public Evaluationcrud()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Name = name.Text; 
            int totalMarks = (int)marksnum.Value;
            int totalWeightage = (int)weightnum.Value;
            if (validation())
            {
                try
                {
                    using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))

                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Evaluation(Name,TotalMarks,TotalWeightage)" +
                            " VALUES (@Name,@TotalMarks,@TotalWeightage)", con))
                        {
                            cmd.Parameters.AddWithValue("@Name", Name);
                            cmd.Parameters.AddWithValue("@TotalMarks", totalMarks);
                            cmd.Parameters.AddWithValue("@TotalWeightage", totalWeightage);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully saved");
                            RefreshEvaluationDataGrid();
                            ClearTextBoxes();
                        }
                        //  con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    return;
                }
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
        public void addColumnsToDatagrid()
        {
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("TotalMarks", "Total Marks");
            dataGridView1.Columns.Add("TotalWeightage", "Total Weightage");
           
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
            addColumnsToDatagrid();
            if (validation())
            {
                try
                {
                    using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT e.Name, e.TotalMarks, e.TotalWeightage " +
                                                         "FROM  Evaluation e " +
                                                        " where e.Name NOT LIKE 'del%'", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                string Name = reader["Name"].ToString();
                                string marks = reader["TotalMarks"].ToString();
                                string weight = reader["TotalWeightage"].ToString();
                                dataGridView1.Rows.Add(Name, marks, weight);
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("Update Evaluation set Name=@Name,TotalMarks=@TotalMarks,TotalWeightage=@TotalWeightage where Name = @Name; ", con))
                    {

                        cmd.Parameters.AddWithValue("@Name", name.Text);
                        cmd.Parameters.AddWithValue("@TotalMarks",marksnum.Text);
                        cmd.Parameters.AddWithValue("@TotalWeightage", weightnum.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        RefreshEvaluationDataGrid();
                        ClearTextBoxes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private bool ObtainedMarksValidations(string marks)
        {
            string numbers = "0123456789";
            bool isValid = true;
            foreach (char n in marks)
            {
                if (!numbers.Contains(n.ToString()))
                {
                    return false;
                }
            }
            return isValid;
        }
        private void RefreshEvaluationDataGrid()
        {
            dataGridView1.Rows.Clear();
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT e.Name,e.TotalMarks,e.TotalWeightage from Evaluation e where Name not like 'del%' ", con);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Name = reader["Name"].ToString();
                            string TotalMarks = reader["TotalMarks"].ToString();
                            string TotalWeightage = reader["TotalWeightage"].ToString();
                            dataGridView1.Rows.Add(Name, TotalMarks, TotalWeightage);
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
        private void Evaluationcrud_Load(object sender, EventArgs e)
        {

        }
        public bool validation()
        {
            if (name.Text == string.Empty)
            {
                MessageBox.Show("TextBox shouldn't be empty");
                return false;
            }
            return true;
        }
        public void getTextBoxValFromGrid()
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            if (row != null)
            {
                string Name = row.Cells["Name"].Value.ToString();
                string marks = row.Cells["TotalMarks"].Value.ToString();
                string weight = row.Cells["TotalWeightage"].Value.ToString();
                name.Text =Name;
                marksnum.Text = marks;
                weightnum.Text = weight;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getTextBoxValFromGrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();

                    if (row != null)
                    {
                        string name = row.Cells["Name"].Value.ToString();
                        using (SqlCommand cmd = new SqlCommand("UPDATE Evaluation SET Name = 'del' + Name WHERE Name=@name", con))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully deleted");
                            RefreshEvaluationDataGrid();
                            ClearTextBoxes();
                        }
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
