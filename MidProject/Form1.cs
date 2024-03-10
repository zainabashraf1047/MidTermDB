using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidTermProjectDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void addAdminControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
         
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            advisorcrud adv = new advisorcrud();
            addAdminControl(adv);
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void createGroup_Click(object sender, EventArgs e)
        {
       
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
         
        }

        private void button8_Click(object sender, EventArgs e)
        {
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void Reports_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            studentcrud stu = new studentcrud();
            addAdminControl(stu);

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            advisorcrud stu = new advisorcrud();
            addAdminControl(stu);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            projectcrud projectcrud = new projectcrud();
            addAdminControl(projectcrud);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Evaluationcrud evcrud = new Evaluationcrud();
            addAdminControl(evcrud);
        }

        private void createGroup_Click_1(object sender, EventArgs e)
        {
            group group = new group();
            addAdminControl(group);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            groupStudent groupStudent = new groupStudent();
            addAdminControl(groupStudent);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            groupProject groupProject = new groupProject();
            addAdminControl(groupProject);

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            ProjectAdvisor projectAdvisor = new ProjectAdvisor();
            addAdminControl(projectAdvisor);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            groupEvaluation groupEvaluation = new groupEvaluation();
            addAdminControl(groupEvaluation);
        }

        private void Reports_Click_1(object sender, EventArgs e)
        {
            reportGen rep = new reportGen();
            addAdminControl(rep);
        }
    }
}
