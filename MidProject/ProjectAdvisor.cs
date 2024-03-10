using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MidTermProjectDB
{
    public partial class ProjectAdvisor : UserControl
    {
        public ProjectAdvisor()
        {
            InitializeComponent();
            comboxAdvisor();
            comboxProject();
            comboBoxAdvRole();
        }

        private void Studentid_Click(object sender, EventArgs e)
        {

        }

        private void ProjectAdvisor_Load(object sender, EventArgs e)
        {

        }
        public bool validation()
        {
            if(comboBoxAdv.Text==string.Empty ||comboBoxProject.Text == string.Empty||comboBoxRole.Text == string.Empty)
            {
                MessageBox.Show("ComboBox shouldn't be empty");
                return false;
            }
            return true;
        }
        public void comboxAdvisor()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT FirstName FROM [Person] join Advisor a on a.Id=Person.Id where FirstName not like 'del%' ", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                comboBoxAdv.DataSource = dt.Tables[0];
                comboBoxAdv.DisplayMember = "FirstName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Advisor : " + ex.Message);
            }
        }
        public void comboxProject()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Title FROM [Project] where Title not like 'del%' ", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                comboBoxProject.DataSource = dt.Tables[0];
                comboBoxProject.DisplayMember = "Title";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Project : " + ex.Message);
            }
        }
        public void comboxAdvId()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM [Advisor] ", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                comboBoxAdv.DataSource = dt.Tables[0];
                comboBoxAdv.DisplayMember = "Id";
                comboBoxAdv.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Advisor : " + ex.Message);
            }
        }
        public void comboBoxAdvRole()
        {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Value from Lookup WHERE Category=\'ADVISOR_ROLE\'", con);
                    SqlDataAdapter d = new SqlDataAdapter(cmd);
                    DataSet dt = new DataSet();
                    d.Fill(dt);
                    comboBoxRole.DataSource = dt.Tables[0];
                    comboBoxRole.DisplayMember = "Value";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Role: " + ex.Message);
                }
            }
        public int returnSelectedRole(string role)
        {
            int g = 0;
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open(); // Open the connection
                    SqlCommand cmd = new SqlCommand("SELECT Id FROM Lookup WHERE Category='ADVISOR_ROLE' AND Value=@ADVISOR_ROLE", con);
                    cmd.Parameters.AddWithValue("@ADVISOR_ROLE", role);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        g = Convert.ToInt32(reader["Id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Role return: " + ex);
            }
            return g;
        }
        public int returnSelectedProject(string pro)
        {
            int proid=0;
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Project WHERE Title=@title and Title not like 'del%' ", con);
                cmd.Parameters.AddWithValue("@title", pro);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    proid = int.Parse(reader["Id"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            return proid;
        }
        public int returnSelectedAdv(string adv)
        {
            int advisorId = 0;
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT p.Id FROM Person p join Advisor a on p.Id =a.Id WHERE p.FirstName=@name and p.FirstName not like 'del%' ", con);
                cmd.Parameters.AddWithValue("@name", adv);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    advisorId = int.Parse(reader["Id"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            return advisorId;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            int role = returnSelectedRole(comboBoxRole.Text);
            int project = returnSelectedProject(comboBoxProject.Text);
            int adv = returnSelectedAdv(comboBoxAdv.Text);
            if (IsAdvisorAssignedToRole(adv, project,role))
            {
                MessageBox.Show("This advisor is already assigned as " + comboBoxRole.Text + " for the selected project. Please select a different advisor, project, or role.");
                return;
            }
            if (validation())
            {
                try
                {
                    using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO ProjectAdvisor(AdvisorId,ProjectId,AdvisorRole,AssignmentDate)" +
                             " VALUES (@AdvisorId,@ProjectId,@AdvisorRole,@AssignmentDate);", con))
                        {
                            cmd.Parameters.AddWithValue("@AdvisorId", adv);
                            cmd.Parameters.AddWithValue("@ProjectId", project);
                            cmd.Parameters.AddWithValue("@AdvisorRole", role);
                            cmd.Parameters.AddWithValue("@AssignmentDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully Added");
                            //RefreshGroupStudentDataGrid();
                            //ClearTextBoxes();
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

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear(); 
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("Advisor", "Advisor");
            dataGridView1.Columns.Add("Project", "Project");
            dataGridView1.Columns.Add("AdvisorRole", "AdvisorRole");
            dataGridView1.Columns.Add("Date", "Date");

            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT p.FirstName AS Advisor, pr.Title AS Project, l.Value AS AdvisorRole, pa.AssignmentDate " +
                                                         "FROM ProjectAdvisor pa " +
                                                         "JOIN Lookup l ON pa.AdvisorRole = l.Id " +
                                                         "JOIN Person p ON pa.AdvisorId = p.Id " +
                                                         "JOIN Project pr ON pa.ProjectId = pr.Id " +
                                                         "WHERE p.FirstName NOT LIKE 'del%' AND pr.Title NOT LIKE 'del%';", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string advisorName = reader["Advisor"].ToString();
                            string projectName = reader["Project"].ToString();
                            string advisorRole = reader["AdvisorRole"].ToString();
                            DateTime assignmentDate = Convert.ToDateTime(reader["AssignmentDate"]);

                            // Use the data as needed (e.g., display in DataGridView)
                            dataGridView1.Rows.Add(advisorName, projectName, advisorRole, assignmentDate);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        public void getTextBoxValFromGrid()
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            if (row != null)
            {
                string adv = row.Cells["Advisor"].Value.ToString();
                string pro = row.Cells["Project"].Value.ToString();
                string role = row.Cells["AdvisorRole"].Value.ToString();
                string date = row.Cells["Date"].Value.ToString();
                comboBoxAdv.Text = adv;
                comboBoxProject.Text = pro;
                comboBoxRole.Text = role;
                dateTimePicker1.Text = date;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            // int statusId = returnSelectedStatus(comboBoxstatus.Text);
            int advId = returnSelectedAdv(comboBoxAdv.Text);
            int role= returnSelectedRole(comboBoxRole.Text);
            int projectId = returnSelectedProject(comboBoxProject.Text);
            
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("Update ProjectAdvisor set AdvisorRole=@AdvisorRole,AssignmentDate=@AssignmentDate where AdvisorId=@AdvisorId and  ProjectId=@ProjectId", con))
                    {
                        cmd.Parameters.AddWithValue("@AdvisorRole", role);
                        cmd.Parameters.AddWithValue("@AdvisorId", advId);
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
                        cmd.Parameters.AddWithValue("@AssignmentDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                       /* RefreshGroupStudentDataGrid();
                        ClearTextBoxes();*/

                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public bool IsAdvisorAssignedToRole(int advisorId, int projectId, int advisorRole)
        {
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ProjectAdvisor WHERE AdvisorId=@advisorId AND ProjectId=@projectId AND AdvisorRole=@advisorRole", con);
                    cmd.Parameters.AddWithValue("@advisorId", advisorId);
                    cmd.Parameters.AddWithValue("@projectId", projectId);
                    cmd.Parameters.AddWithValue("@advisorRole", advisorRole);
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("This advisor is already assigned as " + comboBoxRole.Text + " for the selected project. Please select a different advisor, project, or role.");
                        return true;
                    }

                    return false;
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
        }
    }
}
