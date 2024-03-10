namespace MidTermProjectDB
{
    partial class groupStudent
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
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Status = new System.Windows.Forms.Label();
            this.assignDate = new System.Windows.Forms.Label();
            this.StudentName = new System.Windows.Forms.Label();
            this.groupId = new System.Windows.Forms.Label();
            this.comboBoxgrp = new System.Windows.Forms.ComboBox();
            this.comboBoxstu = new System.Windows.Forms.ComboBox();
            this.comboBoxstatus = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(673, 187);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(275, 26);
            this.dateTimePicker1.TabIndex = 61;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Sienna;
            this.button4.Location = new System.Drawing.Point(598, 300);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(113, 63);
            this.button4.TabIndex = 56;
            this.button4.Text = "Update";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Sienna;
            this.button3.Location = new System.Drawing.Point(454, 300);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 63);
            this.button3.TabIndex = 55;
            this.button3.Text = "Read";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Sienna;
            this.button2.Location = new System.Drawing.Point(298, 300);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 63);
            this.button2.TabIndex = 54;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(298, 388);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(425, 275);
            this.dataGridView1.TabIndex = 52;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(44, 189);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(56, 20);
            this.Status.TabIndex = 47;
            this.Status.Text = "Status";
            // 
            // assignDate
            // 
            this.assignDate.AutoSize = true;
            this.assignDate.Location = new System.Drawing.Point(529, 193);
            this.assignDate.Name = "assignDate";
            this.assignDate.Size = new System.Drawing.Size(132, 20);
            this.assignDate.TabIndex = 46;
            this.assignDate.Text = "Assignment Date";
            // 
            // StudentName
            // 
            this.StudentName.AutoSize = true;
            this.StudentName.Location = new System.Drawing.Point(529, 116);
            this.StudentName.Name = "StudentName";
            this.StudentName.Size = new System.Drawing.Size(66, 20);
            this.StudentName.TabIndex = 45;
            this.StudentName.Text = "Student";
            // 
            // groupId
            // 
            this.groupId.AutoSize = true;
            this.groupId.Location = new System.Drawing.Point(46, 127);
            this.groupId.Name = "groupId";
            this.groupId.Size = new System.Drawing.Size(54, 20);
            this.groupId.TabIndex = 42;
            this.groupId.Text = "Group";
            // 
            // comboBoxgrp
            // 
            this.comboBoxgrp.FormattingEnabled = true;
            this.comboBoxgrp.Location = new System.Drawing.Point(142, 119);
            this.comboBoxgrp.Name = "comboBoxgrp";
            this.comboBoxgrp.Size = new System.Drawing.Size(275, 28);
            this.comboBoxgrp.TabIndex = 64;
            // 
            // comboBoxstu
            // 
            this.comboBoxstu.FormattingEnabled = true;
            this.comboBoxstu.Location = new System.Drawing.Point(673, 108);
            this.comboBoxstu.Name = "comboBoxstu";
            this.comboBoxstu.Size = new System.Drawing.Size(275, 28);
            this.comboBoxstu.TabIndex = 65;
            this.comboBoxstu.SelectedIndexChanged += new System.EventHandler(this.comboBoxstu_SelectedIndexChanged);
            // 
            // comboBoxstatus
            // 
            this.comboBoxstatus.FormattingEnabled = true;
            this.comboBoxstatus.Location = new System.Drawing.Point(142, 187);
            this.comboBoxstatus.Name = "comboBoxstatus";
            this.comboBoxstatus.Size = new System.Drawing.Size(275, 28);
            this.comboBoxstatus.TabIndex = 66;
            // 
            // groupStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.Controls.Add(this.comboBoxstatus);
            this.Controls.Add(this.comboBoxstu);
            this.Controls.Add(this.comboBoxgrp);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.assignDate);
            this.Controls.Add(this.StudentName);
            this.Controls.Add(this.groupId);
            this.Name = "groupStudent";
            this.Size = new System.Drawing.Size(982, 740);
            this.Load += new System.EventHandler(this.groupStudent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Label assignDate;
        private System.Windows.Forms.Label StudentName;
        private System.Windows.Forms.Label groupId;
        private System.Windows.Forms.ComboBox comboBoxgrp;
        private System.Windows.Forms.ComboBox comboBoxstu;
        private System.Windows.Forms.ComboBox comboBoxstatus;
    }
}
