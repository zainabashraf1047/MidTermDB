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
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MidTermProjectDB
{
    public partial class groupProject : UserControl
    {
        public groupProject()
        {
            InitializeComponent();
            comboxGroupId();
            comboxProject();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        /*  private void button2_Click(object sender, EventArgs e)//add
          {
              int pid = GetProjectId(comboBoxProject.Text);
              if (IsProjectAlreadyAssigned(pid))
              {
                  MessageBox.Show("This project is already assigned to a group. Please select a different project.");
                  return;
              }
              if (validation())
              {
                  try
                  {
                      using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))

                      {
                          con.Open();
                          if (IsProjectAssignedToGroup(comboBoxGroup.Text, pid))
                          {
                              MessageBox.Show("This project is already assigned to the selected group. Please select a different project or group.");
                              return;
                          }

                          using (SqlCommand cmd = new SqlCommand("INSERT INTO GroupProject(GroupId,ProjectId,AssignmentDate)" +
                               " VALUES (@GroupId,@ProjectId,@AssignmentDate);", con))
                          {

                              cmd.Parameters.AddWithValue("@GroupId", comboBoxGroup.Text);
                              cmd.Parameters.AddWithValue("@ProjectId", pid);
                              cmd.Parameters.AddWithValue("@AssignmentDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                              cmd.ExecuteNonQuery();
                              MessageBox.Show("Successfully saved");
                              RefreshGroupProjectDataGrid();
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
          }*/
        private void button2_Click(object sender, EventArgs e)//add
        {
            int pid = GetProjectId(comboBoxProject.Text);
            if (IsProjectAlreadyAssigned(pid))
            {
                MessageBox.Show("This project is already assigned to a group. Please select a different project.");
                return;
            }
            if (validation())
            {
                try
                {
                    using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                    {
                        con.Open();
                        if (IsProjectAssignedToGroup(comboBoxGroup.Text, pid))
                        {
                            MessageBox.Show("This project is already assigned to the selected group. Please select a different project or group.");
                            return;
                        }

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO GroupProject(GroupId,ProjectId,AssignmentDate)" +
                             " VALUES (@GroupId,@ProjectId,@AssignmentDate);", con))
                        {
                            cmd.Parameters.AddWithValue("@GroupId", comboBoxGroup.Text);
                            cmd.Parameters.AddWithValue("@ProjectId", pid);
                            cmd.Parameters.AddWithValue("@AssignmentDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully saved");
                            RefreshGroupProjectDataGrid();
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

        int proid =0;
        private int GetProjectId(string title)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Project WHERE Title=@title and Title not like 'del%' ", con);
                cmd.Parameters.AddWithValue("@title", title);
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
                //comboBoxProject.ValueMember = "Title";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Project : " + ex.Message);
            }
        }
        public void comboxGroupId()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM [Group] ", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                comboBoxGroup.DataSource = dt.Tables[0];
                comboBoxGroup.DisplayMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error GroupId : " + ex.Message);
            }
        }
        private void RefreshGroupProjectDataGrid()
        {
            dataGridView1.Rows.Clear();

            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open(); // Open the connection

                    SqlCommand cmd = new SqlCommand(" SELECT GroupId,ProjectId,AssignmentDate from [GroupProject] ", con);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string gid = reader["GroupId"].ToString();
                            string pid = reader["ProjectId"].ToString();
                            DateTime assidate = Convert.ToDateTime(reader["AssignmentDate"]);

                            dataGridView1.Rows.Add(gid, pid, assidate);
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

        public bool validation()
        {
            if (comboBoxGroup.Text == string.Empty || comboBoxProject.Text == string.Empty)
            {
                MessageBox.Show("ComboBox shouldn't be empty");
                return false;
            }
            return true;
        }
        private void groupProject_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)//read
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("GroupId", "Group Id");
            dataGridView1.Columns.Add("ProjectId", "Project Id");
            dataGridView1.Columns.Add("AssignmentDate", "Assignment Date");
            //ClearTextBoxes();

            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT GroupId,ProjectId,AssignmentDate from [GroupProject] ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string gid = reader["GroupId"].ToString();
                            string pid = reader["ProjectId"].ToString();
                            DateTime asdate = Convert.ToDateTime(reader["AssignmentDate"]);
                            dataGridView1.Rows.Add(gid, pid, asdate);
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

        private void button4_Click(object sender, EventArgs e)//update
        {
            int proId = GetProjectId(comboBoxProject.Text);
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    if (IsProjectAssignedToGroup(comboBoxGroup.Text, proId))
                    {
                        MessageBox.Show("This project is already assigned to the selected group. Please select a different project or group.");
                        return;
                    }
                    using (SqlCommand cmd = new SqlCommand("Update GroupProject set ProjectId=@ProjectId where GroupId=@GroupId ", con))
                    {

                        cmd.Parameters.AddWithValue("@GroupId", comboBoxGroup.Text);
                        cmd.Parameters.AddWithValue("@ProjectId", proId);
                        cmd.Parameters.AddWithValue("@AssignmentDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        RefreshGroupProjectDataGrid();
                       // ClearTextBoxes();

                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private bool IsProjectAssignedToGroup(string groupId, int projectId)
        {
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM GroupProject WHERE GroupId=@groupId AND ProjectId=@projectId", con);
                    cmd.Parameters.AddWithValue("@groupId", groupId);
                    cmd.Parameters.AddWithValue("@projectId", projectId);
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
        private bool IsProjectAlreadyAssigned(int projectId)
        {
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM GroupProject WHERE ProjectId=@projectId", con);
                    cmd.Parameters.AddWithValue("@projectId", projectId);
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

        private void comboBoxProject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
