namespace MidTermProjectDB
{
    partial class groupEvaluation
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
            this.evaDate = new System.Windows.Forms.Label();
            this.evaluationId = new System.Windows.Forms.Label();
            this.Obtainedmarks = new System.Windows.Forms.Label();
            this.grpId = new System.Windows.Forms.Label();
            this.comboGroup = new System.Windows.Forms.ComboBox();
            this.comboEva = new System.Windows.Forms.ComboBox();
            this.numericUpDownObtMarks = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownTotal = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObtMarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(497, 202);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(275, 26);
            this.dateTimePicker1.TabIndex = 91;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Sienna;
            this.button4.Location = new System.Drawing.Point(592, 308);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(159, 63);
            this.button4.TabIndex = 88;
            this.button4.Text = "Update";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Sienna;
            this.button3.Location = new System.Drawing.Point(419, 308);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 63);
            this.button3.TabIndex = 87;
            this.button3.Text = "Read";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Sienna;
            this.button2.Location = new System.Drawing.Point(248, 308);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 63);
            this.button2.TabIndex = 86;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(176, 402);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(653, 275);
            this.dataGridView1.TabIndex = 84;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // evaDate
            // 
            this.evaDate.AutoSize = true;
            this.evaDate.Location = new System.Drawing.Point(349, 207);
            this.evaDate.Name = "evaDate";
            this.evaDate.Size = new System.Drawing.Size(122, 20);
            this.evaDate.TabIndex = 81;
            this.evaDate.Text = "Evaluation Date";
            // 
            // evaluationId
            // 
            this.evaluationId.AutoSize = true;
            this.evaluationId.Location = new System.Drawing.Point(547, 52);
            this.evaluationId.Name = "evaluationId";
            this.evaluationId.Size = new System.Drawing.Size(87, 20);
            this.evaluationId.TabIndex = 80;
            this.evaluationId.Text = "Evaluation ";
            this.evaluationId.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Obtainedmarks
            // 
            this.Obtainedmarks.AutoSize = true;
            this.Obtainedmarks.Location = new System.Drawing.Point(538, 129);
            this.Obtainedmarks.Name = "Obtainedmarks";
            this.Obtainedmarks.Size = new System.Drawing.Size(121, 20);
            this.Obtainedmarks.TabIndex = 79;
            this.Obtainedmarks.Text = "Obtained Marks";
            this.Obtainedmarks.Click += new System.EventHandler(this.Obtainedmarks_Click);
            // 
            // grpId
            // 
            this.grpId.AutoSize = true;
            this.grpId.Location = new System.Drawing.Point(13, 58);
            this.grpId.Name = "grpId";
            this.grpId.Size = new System.Drawing.Size(54, 20);
            this.grpId.TabIndex = 78;
            this.grpId.Text = "Group";
            // 
            // comboGroup
            // 
            this.comboGroup.FormattingEnabled = true;
            this.comboGroup.Location = new System.Drawing.Point(176, 52);
            this.comboGroup.Name = "comboGroup";
            this.comboGroup.Size = new System.Drawing.Size(275, 28);
            this.comboGroup.TabIndex = 93;
            // 
            // comboEva
            // 
            this.comboEva.FormattingEnabled = true;
            this.comboEva.Location = new System.Drawing.Point(690, 52);
            this.comboEva.Name = "comboEva";
            this.comboEva.Size = new System.Drawing.Size(275, 28);
            this.comboEva.TabIndex = 94;
            this.comboEva.SelectedIndexChanged += new System.EventHandler(this.indexchange);
            // 
            // numericUpDownObtMarks
            // 
            this.numericUpDownObtMarks.Location = new System.Drawing.Point(690, 123);
            this.numericUpDownObtMarks.Name = "numericUpDownObtMarks";
            this.numericUpDownObtMarks.Size = new System.Drawing.Size(275, 26);
            this.numericUpDownObtMarks.TabIndex = 95;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 96;
            this.label1.Text = "Total Marks";
            // 
            // numericUpDownTotal
            // 
            this.numericUpDownTotal.Location = new System.Drawing.Point(176, 127);
            this.numericUpDownTotal.Name = "numericUpDownTotal";
            this.numericUpDownTotal.Size = new System.Drawing.Size(275, 26);
            this.numericUpDownTotal.TabIndex = 97;
            // 
            // groupEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.Controls.Add(this.numericUpDownTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownObtMarks);
            this.Controls.Add(this.comboEva);
            this.Controls.Add(this.comboGroup);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.evaDate);
            this.Controls.Add(this.evaluationId);
            this.Controls.Add(this.Obtainedmarks);
            this.Controls.Add(this.grpId);
            this.Name = "groupEvaluation";
            this.Size = new System.Drawing.Size(982, 740);
            this.Load += new System.EventHandler(this.groupEvaluation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObtMarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label evaDate;
        private System.Windows.Forms.Label evaluationId;
        private System.Windows.Forms.Label Obtainedmarks;
        private System.Windows.Forms.Label grpId;
        private System.Windows.Forms.ComboBox comboGroup;
        private System.Windows.Forms.ComboBox comboEva;
        private System.Windows.Forms.NumericUpDown numericUpDownObtMarks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownTotal;
    }
}
