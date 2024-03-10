namespace MidTermProjectDB
{
    partial class groupProject
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GroupId = new System.Windows.Forms.Label();
            this.projectId = new System.Windows.Forms.Label();
            this.Assignment = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comboBoxProject = new System.Windows.Forms.ComboBox();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Sienna;
            this.button4.Location = new System.Drawing.Point(620, 297);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 63);
            this.button4.TabIndex = 51;
            this.button4.Text = "Update";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Sienna;
            this.button3.Location = new System.Drawing.Point(409, 297);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 63);
            this.button3.TabIndex = 50;
            this.button3.Text = "Read";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Sienna;
            this.button2.Location = new System.Drawing.Point(202, 297);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 63);
            this.button2.TabIndex = 49;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(202, 383);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(594, 275);
            this.dataGridView1.TabIndex = 47;
            // 
            // GroupId
            // 
            this.GroupId.AutoSize = true;
            this.GroupId.Location = new System.Drawing.Point(494, 107);
            this.GroupId.Name = "GroupId";
            this.GroupId.Size = new System.Drawing.Size(58, 20);
            this.GroupId.TabIndex = 58;
            this.GroupId.Text = "Group ";
            // 
            // projectId
            // 
            this.projectId.AutoSize = true;
            this.projectId.Location = new System.Drawing.Point(47, 107);
            this.projectId.Name = "projectId";
            this.projectId.Size = new System.Drawing.Size(62, 20);
            this.projectId.TabIndex = 57;
            this.projectId.Text = "Project ";
            // 
            // Assignment
            // 
            this.Assignment.AutoSize = true;
            this.Assignment.Location = new System.Drawing.Point(277, 187);
            this.Assignment.Name = "Assignment";
            this.Assignment.Size = new System.Drawing.Size(132, 20);
            this.Assignment.TabIndex = 61;
            this.Assignment.Text = "Assignment Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(452, 182);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(294, 26);
            this.dateTimePicker1.TabIndex = 62;
            // 
            // comboBoxProject
            // 
            this.comboBoxProject.FormattingEnabled = true;
            this.comboBoxProject.Location = new System.Drawing.Point(164, 98);
            this.comboBoxProject.Name = "comboBoxProject";
            this.comboBoxProject.Size = new System.Drawing.Size(216, 28);
            this.comboBoxProject.TabIndex = 63;
            this.comboBoxProject.SelectedIndexChanged += new System.EventHandler(this.comboBoxProject_SelectedIndexChanged);
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(605, 98);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(251, 28);
            this.comboBoxGroup.TabIndex = 64;
            // 
            // groupProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.Controls.Add(this.comboBoxGroup);
            this.Controls.Add(this.comboBoxProject);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.Assignment);
            this.Controls.Add(this.GroupId);
            this.Controls.Add(this.projectId);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "groupProject";
            this.Size = new System.Drawing.Size(982, 740);
            this.Load += new System.EventHandler(this.groupProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label GroupId;
        private System.Windows.Forms.Label projectId;
        private System.Windows.Forms.Label Assignment;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comboBoxProject;
        private System.Windows.Forms.ComboBox comboBoxGroup;
    }
}
