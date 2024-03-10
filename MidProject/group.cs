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

namespace MidTermProjectDB
{
    public partial class group : UserControl
    {
        public group()
        {
            InitializeComponent();
        }

        private void addGroupbtn_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
          
            try
            {
                using (var con = new SqlConnection(Configuration.getInstance().ConnectionStr))

                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [Group](Created_On)  VALUES (@dateTime);", con))
                    {
                        cmd.Parameters.AddWithValue("@dateTime", dateTime);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully saved");
                      //  RefreshAdvisorDataGrid();
                      //  ClearTextBoxes();
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
}
