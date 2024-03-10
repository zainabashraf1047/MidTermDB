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

namespace MidTermProjectDB
{
    public partial class studentcrud : UserControl
    {
        public int ids;
        public int gender = 1;
        public studentcrud()
        {
            InitializeComponent();
            ValueforComboBxOfGender();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void studentcrud_Load(object sender, EventArgs e)
        {

        }
        public void getIdFromRegNo(string Regno)
        {
            using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT P.Id FROM Person P JOIN Student S ON S.Id = P.Id JOIN Lookup L ON L.Id = P.Gender where S.RegistrationNo=@RegNo", con))
                {
                    cmd.Parameters.AddWithValue("@RegNo", Regno);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ids = reader.GetInt32(0);
                        }
                    }

                }
                con.Close();
            }
        }

        public void getTextBoxValFromGrid()
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            if (row != null)
            {
                /*string name = row.Cells["Name"].Value.ToString();
                string[] names = name.Split(' ');*/
                string FName = row.Cells["Firstname"].Value.ToString();
                string LName = row.Cells["Lastname"].Value.ToString();
                string contactt = row.Cells["Contact"].Value.ToString();
                string emaill = row.Cells["Email"].Value.ToString();
                string regno = row.Cells["RegistrationNo"].Value.ToString();
                string gender = row.Cells["Gender"].Value.ToString();

                fname.Text = FName;
                lname.Text = LName;
                contact.Text = contactt;
                email.Text = emaill;
                RegNo.Text = regno;
                comboBoxgender.Text = gender;
                getIdFromRegNo(regno);
            }


        }
        private void ValueforComboBxOfGender()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id, Value FROM Lookup WHERE Category='GENDER'", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                comboBoxgender.DataSource = dt.Tables[0]; // Set the data source
                comboBoxgender.DisplayMember = "Value"; // Specify the display member
                comboBoxgender.ValueMember = "Id"; // Specify the value member
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err: " + ex.Message);
            }
        }

        public int returnSelectedGender(string gen)
        {
            int g = 0;

            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open(); // Open the connection
                    SqlCommand cmd = new SqlCommand("SELECT Id FROM Lookup WHERE Category='GENDER' AND Value=@gender", con);
                    cmd.Parameters.AddWithValue("@gender", gen);
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
            }
            return g;
        }

        private void valueforComboBoxUpdate()
        {

        }

       
        private bool Validation()

        {

            bool isValid = true;
            if (!Validations.RegistrationNoValidations(RegNo.Text))
              {
                  return false;
              }
            if (!Validations.FirstNameValidations(fname.Text))
            {
                return false;
            }
            if (!Validations.LastNameValidations(lname.Text))
            {
                return false;
            }
            if (!Validations.ContactValidations(contact.Text))
            {
                return false;
            }
            if (!Validations.EmailValidations(email.Text))
            {
                return false;
            }
            if (!Validations.DoBValidations(dateTimePicker1.Text, 1995, 2007))
            {
                return false;
            }
            if (comboBoxgender.Text == "")
            {
                MessageBox.Show("Select a Gender First", "Error");
                return false;
            }
            /*if(!Validations.AreControlsFilled(studentcrud))
            {
                MessageBox.Show("No Text or Combo Box should be empty");
                return false;
            }*/

            return isValid;
        }


        private void button2_Click(object sender, EventArgs e)//add
        {
            if (Validation())
            {
                string selectedGender = comboBoxgender.Text;
                int genderId = returnSelectedGender(selectedGender);

                try
                {
                    using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))

                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender)" +
                             " VALUES (@FirstName,@LastName,@Contact,@Email,@DateOfBirth,@Gender);" +
                             " INSERT INTO Student(Id,RegistrationNo)" +
                             " VALUES ((select id from person WHERE FirstName = @FirstName AND LastName=@LastName AND " +
                             "Contact=@Contact AND Email=@Email AND DateOfBirth=@DateOfBirth AND Gender=@Gender), @RegNo)", con))
                        {

                            cmd.Parameters.AddWithValue("@FirstName", fname.Text);
                            cmd.Parameters.AddWithValue("@LastName", lname.Text);
                            cmd.Parameters.AddWithValue("@Contact", contact.Text);
                            cmd.Parameters.AddWithValue("@Email", email.Text);
                            cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@Gender", genderId);
                            cmd.Parameters.AddWithValue("@RegNo", RegNo.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully saved");
                            RefreshStudentDataGrid();
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
        private void readStudentFromDb()//read
        {
            ClearTextBoxes();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT p.FirstName, p.LastName, p.Contact, p.Email, p.DateOfBirth, l.Value as Gender, s.RegistrationNo " +
                                                     "FROM Person p " +
                                                     "INNER JOIN Student s ON p.Id = s.Id " +
                                                     "INNER JOIN Lookup l ON p.Gender = l.Id where p.FirstName NOT LIKE 'del%'", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
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
        private void button3_Click(object sender, EventArgs e)
        {

            dataGridView1.Columns.Add("FirstName", "First Name");
            dataGridView1.Columns.Add("LastName", "Last Name");
            dataGridView1.Columns.Add("Contact", "Contact");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("DateOfBirth", "Date of Birth");
            dataGridView1.Columns.Add("Gender", "Gender");
            dataGridView1.Columns.Add("RegistrationNo", "Registration No");
            readStudentFromDb();
         //   getTextBoxValFromGrid();
        }

        private void button4_Click(object sender, EventArgs e)//update
        {
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("Update person set firstName=@FirstName,LastName=@LastName,Contact=@Contact,Email=@Email,Dateofbirth=@DateOfBirth,Gender=@Gender where id = @ids; " +
                         "Update student set registrationNo=@RegNo where id = @ids; ", con))
                    {

                        cmd.Parameters.AddWithValue("@FirstName", fname.Text);
                        cmd.Parameters.AddWithValue("@LastName", lname.Text);
                        cmd.Parameters.AddWithValue("@Contact", contact.Text);
                        cmd.Parameters.AddWithValue("@Email", email.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@RegNo", RegNo.Text);
                        cmd.Parameters.AddWithValue("@Ids", ids);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        RefreshStudentDataGrid();
                        ClearTextBoxes();

                    }
                    //  con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
      

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxgender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RefreshStudentDataGrid()
        {
            dataGridView1.Rows.Clear();
            try
            {
                using (var con = Configuration.getInstance().getConnection())
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT p.FirstName, p.LastName, p.Contact, p.Email, p.DateOfBirth, l.Value as Gender, s.RegistrationNo " +
                                                    "FROM Person p " +
                                                    "INNER JOIN Student s ON p.Id = s.Id " +
                                                    "INNER JOIN Lookup l ON p.Gender = l.Id", con);
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




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            getTextBoxValFromGrid();
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
        
        private void button5_Click(object sender, EventArgs e)//deletee
        {
    
                DataGridViewRow row = dataGridView1.CurrentRow;
                try
                {
                    using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                    {
                        con.Open();

                        if (row != null)
                        {
                            string regno = row.Cells["RegistrationNo"].Value.ToString();

                            using (SqlCommand cmd = new SqlCommand("UPDATE Person SET FirstName = 'del' + FirstName WHERE Id IN (SELECT Id FROM Student WHERE RegistrationNo = @RegNo)", con))
                            {
                                cmd.Parameters.AddWithValue("@RegNo", regno);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show("Successfully deleted");
                                RefreshStudentDataGrid();
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

        private void dataCellClick(object sender, DataGridViewCellEventArgs e)
        {
            getTextBoxValFromGrid();

        }
    }
}
    


