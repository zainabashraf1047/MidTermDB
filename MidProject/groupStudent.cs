using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace MidTermProjectDB
{
    public partial class groupStudent : UserControl
    {
        public groupStudent()
        {
            InitializeComponent();
            comboxGroupid();
            comboxStudentId();
            comboxStatus();
         //   comboxProject();
        }

        private void groupStudent_Load(object sender, EventArgs e)
        {

        }
        public bool validation()
        {
            if (comboBoxgrp.Text == string.Empty || comboBoxstu.Text == string.Empty || comboBoxstatus.Text == string.Empty)
            {
                MessageBox.Show("ComboBox shouldn't be empty");
                return false;
            }

            int studentId = Convert.ToInt32(comboBoxstu.Text);

            if (IsStudentAssignedToAnyGroup(studentId))
            {
                MessageBox.Show("This student is already assigned to a group. Please select a different student.");
                return false;
            }

            return true;
        }
        public void comboxGroupid()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM [Group] ", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                comboBoxgrp.DataSource = dt.Tables[0];
                comboBoxgrp.DisplayMember = "Id";
                comboBoxgrp.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error GroupId : " + ex.Message);
            }

        }
       /* public void comboxProject()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Title FROM [Project] ", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                comboBoxgrp.DataSource = dt.Tables[0];
                comboBoxgrp.DisplayMember = "Title";
                comboBoxgrp.ValueMember = "Title";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Project : " + ex.Message);
            }

        }*/
        public void comboxStudentId()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Student ", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                comboBoxstu.DataSource = dt.Tables[0];
                comboBoxstu.DisplayMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error StudentId : " + ex.Message);
            }
        }
        public void comboxStatus()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id, Value FROM Lookup WHERE Category='Status'", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                comboBoxstatus.DataSource = dt.Tables[0]; // Set the data source
                comboBoxstatus.DisplayMember = "Value"; // Specify the display member
                comboBoxstatus.ValueMember = "Id"; // Specify the value member
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err: " + ex.Message);
            }
        }

        public int returnSelectedStatus(string statuss)
        {
            int g = 0;
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open(); // Open the connection
                    SqlCommand cmd = new SqlCommand("SELECT Id FROM Lookup WHERE Category='Status' AND Value=@Status", con);
                    cmd.Parameters.AddWithValue("@Status", statuss);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        g = Convert.ToInt32(reader["Id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }return g;
        }
      /*  int projectId;
        private void GetProjectId(string title)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Project WHERE Title=@title and Title not like 'del%", con);
                cmd.Parameters.AddWithValue("@title", title);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    projectId = int.Parse(reader["Id"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
        }*/
      /*  private void ProjectToComboBox()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Title FROM Project Pro WHERE Id NOT IN (SELECT p.Id FROM Project p JOIN GroupProject Gp ON p.Id = Gp.ProjectId JOIN [Group] g ON g.id = gp.GroupId JOIN GroupStudent gs ON gs.GroupId = g.id WHERE gs.Status = 3 and Pro.Title NOT LIKE '%!%');", con);
            SqlDataAdapter d = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            d.Fill(dt);

            if (dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                comboBoxPro.DataSource = dt.Tables[0];
                comboBoxPro.DisplayMember = "Title";
            }
            else
            {
                // If no projects available, display a message
                comboBoxPro.DisplayMember = string.Empty;
                MessageBox.Show("No projects available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }*/
        private void button2_Click(object sender, EventArgs e)
        {
            // string selectedGender = comboBoxgender.Text;
            int statusId = returnSelectedStatus(comboBoxstatus.Text);
            if (validation())
            {
                try
                {
                    using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))

                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO GroupStudent(GroupId,StudentId,Status,AssignmentDate)" +
                             " VALUES (@GroupId,@StudentId,@Status,@AssignmentDate);", con))
                        {

                            cmd.Parameters.AddWithValue("@GroupId", comboBoxgrp.Text);
                            cmd.Parameters.AddWithValue("@StudentId", comboBoxstu.Text);
                            cmd.Parameters.AddWithValue("@Status", statusId);
                            cmd.Parameters.AddWithValue("@AssignmentDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully saved");
                            RefreshGroupStudentDataGrid();
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
        /*public void getStudentId()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Person join Student s on Person.Id=s.Id ", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                comboBoxstu.DataSource = dt.Tables[0];
                comboBoxstu.DisplayMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error StudentId : " + ex.Message);
            }
        }*/
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("GroupId", "Group Id");
            dataGridView1.Columns.Add("StudentId", "Student Id");
            dataGridView1.Columns.Add("Status", "Status");
            dataGridView1.Columns.Add("AssignmentDate","Assignment Date");
            ClearTextBoxes();

            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT GroupId,StudentId,Status,AssignmentDate from [GroupStudent] ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string gid = reader["GroupId"].ToString();
                            string sid = reader["StudentId"].ToString();
                            string statuss = reader["Status"].ToString();
                            DateTime asdate = Convert.ToDateTime(reader["AssignmentDate"]);
                            dataGridView1.Rows.Add(gid, sid, statuss, asdate);
                        }
                        reader.Close();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void comboBoxstu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void getTextBoxValFromGrid()
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            if (row != null)
            {
                /*string name = row.Cells["Name"].Value.ToString();
                string[] names = name.Split(' ');*/
                string gid = row.Cells["GroupId"].Value.ToString();
                string sid = row.Cells["StudentId"].Value.ToString();
                string status = row.Cells["Status"].Value.ToString();
                string datee = row.Cells["AssignmentDate"].Value.ToString();
          
                comboBoxgrp.Text = gid;
                comboBoxstu.Text = sid;
                comboBoxstatus.Text = status;
                dateTimePicker1.Text = datee;
             
            }


        }
        
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)//update
        {
            int statusId = returnSelectedStatus(comboBoxstatus.Text);
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("Update GroupStudent set Status=@Status,AssignmentDate=@AssignmentDate where GroupId=@GroupId and  StudentId=@StudentId", con))
                       // "Update Advisor set Designation=@Designation ,salary=@salary where id = @ids; "
                    {

                        cmd.Parameters.AddWithValue("@GroupId", comboBoxgrp.Text);
                        cmd.Parameters.AddWithValue("@StudentId", comboBoxstu.Text);
                        cmd.Parameters.AddWithValue("@Status", statusId);
                        cmd.Parameters.AddWithValue("@AssignmentDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        RefreshGroupStudentDataGrid();
                        ClearTextBoxes();

                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void RefreshGroupStudentDataGrid()
        {
            // Assuming you've already set up your DataGridView columns
            dataGridView1.Rows.Clear();

            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open(); // Open the connection

                    SqlCommand cmd = new SqlCommand(" SELECT GroupId,StudentId,Status,AssignmentDate from [GroupStudent] where GroupId NOT LIKE 'del%'", con);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string gid = reader["GroupId"].ToString();
                            string sid = reader["StudentId"].ToString();
                            string status = reader["Status"].ToString();
                            DateTime assdate = Convert.ToDateTime(reader["AssignmentDate"]);
                       
                            dataGridView1.Rows.Add(gid, sid, status, assdate);
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error In Refreshing: " + ex.Message);
            }

        }
        public bool IsStudentAssignedToAnyGroup(int studentId)
        {
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM GroupStudent WHERE StudentId=@studentId", con);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getTextBoxValFromGrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
   /*         DataGridViewRow row = dataGridView1.CurrentRow;
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();

                    if (row != null)
                    {
                        string gid = row.Cells["GroupId"].Value.ToString();

                        using (SqlCommand cmd = new SqlCommand("UPDATE GroupStudent SET GroupId = '0123' + GroupId", con))
                        {
                            cmd.Parameters.AddWithValue("@GroupId", gid);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully deleted");
                            RefreshGroupStudentDataGrid();
                            ClearTextBoxes();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }*/

        }
        /*  private void RefreshDataGrid()
{
dataGridView1.Rows.Clear();
try
{
using (var con = Configuration.getInstance().getConnection())
{
con.Open();

SqlCommand cmd = new SqlCommand("SELECT g.Id, s.id, GroupStudent.AssignmentDate,l.Status " +
"FROM Group g " +
"INNER JOIN [Group]  ON g.Id = GroupStudent.Id " +
"INNER JOIN Lookup l ON g.Status = l.Id", con);
using (SqlDataReader reader = cmd.ExecuteReader())
{
while (reader.Read())
{
string firstName = reader["FirstName"].ToString();
string lastName = reader["LastName"].ToString();
string contact = reader["Contact"].ToString();
string email = reader["Email"].ToString();
DateTime dateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
string gender = reader["Gender"].ToString();
string registrationNo = reader["RegistrationNo"].ToString();

dataGridView1.Rows.Add(firstName, lastName, contact, email, dateOfBirth, gender, registrationNo);
}
}
con.Close();
}
}
catch (Exception ex)
{
MessageBox.Show("Error: " + ex.Message);
}
}
*/
    }
}
