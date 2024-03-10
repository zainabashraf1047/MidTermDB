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
using System.Diagnostics.SymbolStore;

namespace MidTermProjectDB
{
    public partial class advisorcrud : UserControl
    {
        public int genderVar = 1;
        public int designationVar = 6;
        public advisorcrud()
        {
            InitializeComponent();
            comboBoxDesign();
            ValueforComboBxOfGender();
           
        }
        int ids;

        public bool Validation()

        {

            bool isValid = true;
          
            if (!Validations.FirstNameValidations(fName.Text))
            {
                return false;
            }
            if (!Validations.LastNameValidations(lName.Text))
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
            if (!Validations.SalaryValidations(salary.Text))
            {
                return false;
            }


            if (gender.Text == string.Empty || designation.Text == string.Empty)
            {
                MessageBox.Show("ComboBox Empty", "Error");
                return false;
            }
            return isValid;
        }


        private void advisorcrud_Load(object sender, EventArgs e)
        {

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
        private void ValueforComboBxOfGender()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id, Value FROM Lookup WHERE Category='GENDER'", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                gender.DataSource = dt.Tables[0]; 
                gender.DisplayMember = "Value"; 
                gender.ValueMember = "Id"; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Gender : " + ex.Message);
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
                Console.WriteLine("Error Gender return: " + ex);
            }
            return g;
        }
        public int returnSelectedDesignation(string des)
        {
            int g= 0;
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open(); // Open the connection
                    SqlCommand cmd = new SqlCommand("SELECT Id FROM Lookup WHERE Category='DESIGNATION' AND Value=@DESIGNATION", con);
                    cmd.Parameters.AddWithValue("@DESIGNATION", des);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        g = Convert.ToInt32(reader["Id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Designation return: " + ex);
            }
            return g;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string selectedGender = gender.Text;
            string desig = designation.Text;
            int genderId = returnSelectedGender(selectedGender);
            int desid = returnSelectedDesignation(desig);
            if (Validation())
            {
                try
                {
                    using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))

                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender)" +
                             " VALUES (@FirstName,@LastName,@Contact,@Email,@DateOfBirth,@Gender);" +
                             " INSERT INTO Advisor(Id,Designation,Salary)" +
                             " VALUES ((select id from person WHERE FirstName = @FirstName AND LastName=@LastName AND " +
                             "Contact=@Contact AND Email=@Email AND DateOfBirth=@DateOfBirth AND Gender=@Gender), @Designation,@salary)", con))
                        {

                            cmd.Parameters.AddWithValue("@FirstName", fName.Text);
                            cmd.Parameters.AddWithValue("@LastName", lName.Text);
                            cmd.Parameters.AddWithValue("@Contact", contact.Text);
                            cmd.Parameters.AddWithValue("@Email", email.Text);
                            cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@Gender", genderId);
                            cmd.Parameters.AddWithValue("@Designation", desid);
                            cmd.Parameters.AddWithValue("@Salary", salary.Text);

                            // cmd.Parameters.AddWithValue("@RegNo", RegistrationNo.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully saved");
                            RefreshAdvisorDataGrid();
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
        public void comboBoxDesign()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select Value from Lookup WHERE Category=\'DESIGNATION\'", con);
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                d.Fill(dt);
                designation.DataSource = dt.Tables[0];
                designation.DisplayMember = "Value"; // Specify the display member
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Designation: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("FirstName", "First Name");
            dataGridView1.Columns.Add("LastName", "Last Name");
            dataGridView1.Columns.Add("Contact", "Contact");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("DateOfBirth", "Date of Birth");
            dataGridView1.Columns.Add("Gender", "Gender");
            dataGridView1.Columns.Add("Designation", "Designation");
            dataGridView1.Columns.Add("Salary", "Salary");
            readAdvisorFromDb();
        }
        public void getIdFromFirstNameLastName(string fname,string lname)
        {
            using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT P.Id FROM Person P JOIN Advisor a ON a.Id = P.Id JOIN Lookup L ON L.Id = P.Gender where P.FirstName=@fname and P.LastName=@lname", con))
                {
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@lname", lname);
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
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("Update person set firstName=@FirstName,LastName=@LastName,Contact=@Contact,Email=@Email," +
                        "DateOfBirth=@DateOfBirth,Gender=@Gender where id = @ids; " +
                         "Update Advisor set Designation=@Designation ,salary=@salary where id = @ids; ", con))
                    {

                        cmd.Parameters.AddWithValue("@FirstName", fName.Text);
                        cmd.Parameters.AddWithValue("@LastName", lName.Text);
                        cmd.Parameters.AddWithValue("@Contact", contact.Text);
                        cmd.Parameters.AddWithValue("@Email", email.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Gender", genderVar);
                        cmd.Parameters.AddWithValue("@Designation", designationVar);
                        cmd.Parameters.AddWithValue("@salary", salary.Text);
                        cmd.Parameters.AddWithValue("@Ids", ids);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        RefreshAdvisorDataGrid();
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
                        string fname = row.Cells["FirstName"].Value.ToString();
                        string lname = row.Cells["LastName"].Value.ToString();

                        using (SqlCommand cmd = new SqlCommand("UPDATE Person SET FirstName = 'del' + FirstName WHERE Id IN (SELECT Id FROM Advisor WHERE FirstName = @fname and LastName=@lName)", con))
                        {
                            cmd.Parameters.AddWithValue("@fName", fname);
                            cmd.Parameters.AddWithValue("@lName", lname);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully deleted");
                            RefreshAdvisorDataGrid();
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
        private void RefreshAdvisorDataGrid()
        {
            // Assuming you've already set up your DataGridView columns
            dataGridView1.Rows.Clear();

            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open(); // Open the connection

                    SqlCommand cmd = new SqlCommand("SELECT p.FirstName, p.LastName, p.Contact, p.Email, p.DateOfBirth, l.Value as Gender, a.designation, a.salary " +
                                                    "FROM Person p " +
                                                    "INNER JOIN Advisor a ON p.Id = a.Id " +
                                                    "INNER JOIN Lookup l ON p.Gender = l.Id where p.FirstName NOT LIKE 'del%'", con);

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
                            string designation = reader["Designation"].ToString();
                            string salary = reader["Salary"].ToString();

                            dataGridView1.Rows.Add(firstName, lastName, contact, email, dateOfBirth, gender, designation, salary);
                        }
                    }

                    // Close the connection after reading
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void readAdvisorFromDb()//read
        {
            ClearTextBoxes();

            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT p.FirstName, p.LastName, p.Contact, p.Email, p.DateOfBirth, l.Value as Gender, a.designation,a.salary " +
                                                     "FROM Person p " +
                                                     "INNER JOIN Advisor a ON p.Id = a.Id " +
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
                            string designation = reader["Designation"].ToString();
                            string salary = reader["Salary"].ToString();

                            dataGridView1.Rows.Add(firstName, lastName, contact, email, dateOfBirth, gender, designation, salary);
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
                string genderr = row.Cells["Gender"].Value.ToString();
                string dob = row.Cells["Dateofbirth"].Value.ToString();
                string deg = row.Cells["Designation"].Value.ToString();
                string sal = row.Cells["salary"].Value.ToString();

                fName.Text = FName;
                lName.Text = LName;
                contact.Text = contactt;
                email.Text = emaill;
                dateTimePicker1.Text = dob;
                gender.Text = genderr;
                getIdFromFirstNameLastName(FName, LName);
            }


        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getTextBoxValFromGrid();
        }

        private void gender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void designation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
