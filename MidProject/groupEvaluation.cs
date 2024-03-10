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

namespace MidTermProjectDB
{
    public partial class groupEvaluation : UserControl
    {
        int groupId;
        public groupEvaluation()
        {
            InitializeComponent();
            EvaluationToComboBox();
            comboxGroupid();
            comboEva.SelectedIndexChanged += indexchange;
            numericUpDownTotal.Enabled = false;

        }

        private void groupEvaluation_Load(object sender, EventArgs e)
        {

        }
        public bool validation()
        {
            if (comboGroup.Text == string.Empty || comboEva.Text == string.Empty )
            {
                MessageBox.Show("ComboBox shouldn't be empty");
                return false;
            }
            return true;
        }
        /* private void EvaluationToComboBox()
         {
             var con = Configuration.getInstance().getConnection();

            // SqlCommand cmd = new SqlCommand("SELECT E.Name FROM Evaluation E EXCEPT SELECT E.Name FROM Evaluation E JOIN GroupEvaluation GE ON GE.EvaluationId = E.Id JOIN [Group] G ON G.Id = GE.GroupId WHERE  E.Name not like 'del%' and G.Id= '"+groupId+ " ' ", con);
             SqlCommand cmd = new SqlCommand("SELECT E.Name FROM Evaluation E WHERE E.Name NOT LIKE 'del%' AND E.Id NOT IN (SELECT GE.EvaluationId FROM GroupEvaluation GE JOIN [Group] G ON G.Id = GE.GroupId WHERE G.Id =' "+groupId+ " ' ", con);
             SqlDataAdapter d = new SqlDataAdapter(cmd);
             DataSet dt = new DataSet();
             d.Fill(dt);
             comboEva.DataSource = dt.Tables[0];
             comboEva.DisplayMember = "Name";
         }*/
        private void EvaluationToComboBox()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT E.Name FROM Evaluation E WHERE E.Name NOT LIKE 'del%' AND E.Id NOT IN (SELECT GE.EvaluationId FROM GroupEvaluation GE JOIN [Group] G ON G.Id = GE.GroupId WHERE G.Id = @GroupId)", con);
            cmd.Parameters.AddWithValue("@GroupId", groupId);
            SqlDataAdapter d = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            d.Fill(dt);
            comboEva.DataSource = dt.Tables[0];
            comboEva.DisplayMember = "Name";
        }

        private int GetSelectedEvaluationId(string name)
        { 

            int evaId=0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Evaluation WHERE Name=@Name and  Name not like 'del%' ", con);
            cmd.Parameters.AddWithValue("@Name", name);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                evaId = int.Parse(reader["Id"].ToString());
            }
            reader.Close();
            return evaId;
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
                comboGroup.DataSource = dt.Tables[0];
                comboGroup.DisplayMember = "Id";
                comboGroup.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error GroupId : " + ex.Message);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            int evaId=GetSelectedEvaluationId(comboEva.Text.ToString());
            if (validation())
            {
                int obtainedMarks = int.Parse(numericUpDownObtMarks.Text);
                int totalMarks = GetTotalMarksForEvaluation(evaId);
                if (obtainedMarks < 0 || obtainedMarks > totalMarks)
                {
                    MessageBox.Show("Obtained marks should be between 0 and " + totalMarks);
                    return;
                }
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("INSERT INTO GroupEvaluation VALUES (@GroupId,@EvaluationId,@ObtainedMarks, @EvaluationDate)", con);
                    cmd.Parameters.AddWithValue("@GroupId", comboGroup.Text);
                    cmd.Parameters.AddWithValue("@EvaluationId", evaId);
                    cmd.Parameters.AddWithValue("@ObtainedMarks", int.Parse(numericUpDownObtMarks.Text));
                    cmd.Parameters.AddWithValue("@EvaluationDate", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    return;
                }
            }
        }

        private void Obtainedmarks_Click(object sender, EventArgs e)
        {

        }

        private void indexchange(object sender, EventArgs e)
        {
            string selectedEvaluation = comboEva.Text;

            if (!string.IsNullOrEmpty(selectedEvaluation))
            {
                int evaId = GetSelectedEvaluationId(selectedEvaluation);
                numericUpDownTotal.Value = evaId > 0 ? GetTotalMarksForEvaluation(evaId) : 0;
            }
        }
        private int GetTotalMarksForEvaluation(int evaluationId)
        {
            int totalMarks = 0;
            var con = Configuration.getInstance().getConnection();

            using (SqlCommand cmd = new SqlCommand("SELECT TotalMarks FROM Evaluation WHERE Id = @EvaluationId", con))
            {
                cmd.Parameters.AddWithValue("@EvaluationId", evaluationId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    totalMarks = int.Parse(reader["TotalMarks"].ToString());
                }
                reader.Close();
            }

            return totalMarks;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("GroupId", "Group Id");
            dataGridView1.Columns.Add("EvaluationId", "Evaluation Id");
            dataGridView1.Columns.Add("ObtainedMarks", "Obtained Marks");
            dataGridView1.Columns.Add("EvaluationDate", "Evaluation Date");
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT GroupId,EvaluationId,ObtainedMarks,EvaluationDate from [GroupEvaluation] gp join Evaluation e on gp.EvaluationId=e.Id where e.Name NOT LIKE 'del%' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string gid = reader["GroupId"].ToString();
                            string eid = reader["EvaluationId"].ToString();
                            string obt = reader["ObtainedMarks"].ToString();
                            DateTime evadate = Convert.ToDateTime(reader["EvaluationDate"]);
                            dataGridView1.Rows.Add(gid, eid, obt, evadate);
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

        private void button4_Click(object sender, EventArgs e)
        {
            int evaId = GetSelectedEvaluationId(comboEva.Text);
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("Update GroupEvaluation set ObtainedMarks=@ObtainedMarks where EvaluationId=@EvaluationId and  GroupId=@GroupId and  EvaluationDate=@EvaluationDate", con))
                    {

                        cmd.Parameters.AddWithValue("@GroupId", comboGroup.Text);
                        cmd.Parameters.AddWithValue("@EvaluationId", evaId);
                        cmd.Parameters.AddWithValue("@ObtainedMarks", numericUpDownObtMarks.Text);
                        cmd.Parameters.AddWithValue("@EvaluationDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        RefreshGroupEvaluationDataGrid();
                      //  ClearTextBoxes();

                    }
                    con.Close();
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
                /*string name = row.Cells["Name"].Value.ToString();
                string[] names = name.Split(' ');*/
                string gid = row.Cells["GroupId"].Value.ToString();
                string eid = row.Cells["EvaluationId"].Value.ToString();
                string obt = row.Cells["ObtainedMarks"].Value.ToString();
                string datee = row.Cells["EvaluationDate"].Value.ToString();

                comboGroup.Text = gid;
                comboEva.Text = eid;
                numericUpDownObtMarks.Text = obt;
                dateTimePicker1.Text = datee;

            }


        }
        private void RefreshGroupEvaluationDataGrid()
        {
            dataGridView1.Rows.Clear();
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open(); 
                    SqlCommand cmd = new SqlCommand(" SELECT GroupId,EvaluationId,ObtainedMarks,EvaluationDate from [GroupEvaluation] gp join Evaluation e on gp.EvaluationId=e.Id where e.Name NOT LIKE 'del%'", con);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string gid = reader["GroupId"].ToString();
                            string eid = reader["EvaluationId"].ToString();
                            string obt = reader["ObtainedMarks"].ToString();
                            DateTime evadate = Convert.ToDateTime(reader["EvaluationDate"]);

                            dataGridView1.Rows.Add(gid, eid, obt, evadate);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getTextBoxValFromGrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }
    }
}
