namespace MidTermProjectDB
{
    partial class ProjectAdvisor
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comboBoxRole = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.assignDate = new System.Windows.Forms.Label();
            this.proId = new System.Windows.Forms.Label();
            this.advRol = new System.Windows.Forms.Label();
            this.advId = new System.Windows.Forms.Label();
            this.comboBoxAdv = new System.Windows.Forms.ComboBox();
            this.comboBoxProject = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(695, 123);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(275, 26);
            this.dateTimePicker1.TabIndex = 77;
            // 
            // comboBoxRole
            // 
            this.comboBoxRole.FormattingEnabled = true;
            this.comboBoxRole.Location = new System.Drawing.Point(241, 122);
            this.comboBoxRole.Name = "comboBoxRole";
            this.comboBoxRole.Size = new System.Drawing.Size(275, 28);
            this.comboBoxRole.TabIndex = 76;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Sienna;
            this.button4.Location = new System.Drawing.Point(592, 305);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(159, 63);
            this.button4.TabIndex = 74;
            this.button4.Text = "Update";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Sienna;
            this.button3.Location = new System.Drawing.Point(421, 305);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 63);
            this.button3.TabIndex = 73;
            this.button3.Text = "Read";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Sienna;
            this.button2.Location = new System.Drawing.Point(260, 305);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 63);
            this.button2.TabIndex = 72;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(157, 395);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(667, 275);
            this.dataGridView1.TabIndex = 70;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // assignDate
            // 
            this.assignDate.AutoSize = true;
            this.assignDate.Location = new System.Drawing.Point(551, 129);
            this.assignDate.Name = "assignDate";
            this.assignDate.Size = new System.Drawing.Size(132, 20);
            this.assignDate.TabIndex = 66;
            this.assignDate.Text = "Assignment Date";
            // 
            // proId
            // 
            this.proId.AutoSize = true;
            this.proId.Location = new System.Drawing.Point(551, 55);
            this.proId.Name = "proId";
            this.proId.Size = new System.Drawing.Size(58, 20);
            this.proId.TabIndex = 65;
            this.proId.Text = "Project";
            this.proId.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.proId.Click += new System.EventHandler(this.Studentid_Click);
            // 
            // advRol
            // 
            this.advRol.AutoSize = true;
            this.advRol.Location = new System.Drawing.Point(78, 132);
            this.advRol.Name = "advRol";
            this.advRol.Size = new System.Drawing.Size(98, 20);
            this.advRol.TabIndex = 64;
            this.advRol.Text = "Advisor Role";
            // 
            // advId
            // 
            this.advId.AutoSize = true;
            this.advId.Location = new System.Drawing.Point(78, 55);
            this.advId.Name = "advId";
            this.advId.Size = new System.Drawing.Size(61, 20);
            this.advId.TabIndex = 63;
            this.advId.Text = "Advisor";
            // 
            // comboBoxAdv
            // 
            this.comboBoxAdv.FormattingEnabled = true;
            this.comboBoxAdv.Location = new System.Drawing.Point(241, 52);
            this.comboBoxAdv.Name = "comboBoxAdv";
            this.comboBoxAdv.Size = new System.Drawing.Size(275, 28);
            this.comboBoxAdv.TabIndex = 78;
            // 
            // comboBoxProject
            // 
            this.comboBoxProject.FormattingEnabled = true;
            this.comboBoxProject.Location = new System.Drawing.Point(695, 49);
            this.comboBoxProject.Name = "comboBoxProject";
            this.comboBoxProject.Size = new System.Drawing.Size(275, 28);
            this.comboBoxProject.TabIndex = 79;
            // 
            // ProjectAdvisor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.Controls.Add(this.comboBoxProject);
            this.Controls.Add(this.comboBoxAdv);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.comboBoxRole);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.assignDate);
            this.Controls.Add(this.proId);
            this.Controls.Add(this.advRol);
            this.Controls.Add(this.advId);
            this.Name = "ProjectAdvisor";
            this.Size = new System.Drawing.Size(982, 740);
            this.Load += new System.EventHandler(this.ProjectAdvisor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comboBoxRole;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label assignDate;
        private System.Windows.Forms.Label proId;
        private System.Windows.Forms.Label advRol;
        private System.Windows.Forms.Label advId;
        private System.Windows.Forms.ComboBox comboBoxAdv;
        private System.Windows.Forms.ComboBox comboBoxProject;
    }
}
