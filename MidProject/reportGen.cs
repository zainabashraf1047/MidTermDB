using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Layout.Properties;
using System;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace MidTermProjectDB
{
    public partial class reportGen : UserControl
    {
        Font boldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 16);
        Font textFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12);

        public reportGen()
        {
            InitializeComponent();
        }

      

        private Document TitlePage(ref Document document)
        {
            // Add content to the document
            Paragraph title = new Paragraph("Mid Term Project Fyp Management System", boldFont);
            title.Font.Size = 28;
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            System.Drawing.Image image = Properties.Resources.logo;
            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Png);
            image1.Alignment = Element.ALIGN_CENTER;
            image1.ScaleAbsolute(150f, 150f);
            document.Add(image1);
            // Add session info

            Paragraph session = new Paragraph("Session: 2022 - 2026", textFont);
            session.Alignment = Element.ALIGN_CENTER;
            document.Add(session);

            // Add submitted by
            Paragraph submittedBy = new Paragraph("Submitted By:", boldFont);
            submittedBy.Font.Size = 16;
            submittedBy.Alignment = Element.ALIGN_CENTER;
            document.Add(submittedBy);
            Paragraph submittedByDetails = new Paragraph("Zainab Ashraf    2022-CS-181", textFont);
            submittedByDetails.Font.Size = 14;
            submittedByDetails.Alignment = Element.ALIGN_CENTER;
            document.Add(submittedByDetails);

            // Add submitted to
            Paragraph submittedTo = new Paragraph("Submitted To:", boldFont);
            submittedTo.Font.Size = 16;
            submittedTo.Alignment = Element.ALIGN_CENTER;
            document.Add(submittedTo);
            Paragraph submittedToDetails = new Paragraph("Sir Nazeef Ul Haq and Nauman Babar", textFont);
            submittedToDetails.Font.Size = 14;
            submittedToDetails.Alignment = Element.ALIGN_CENTER;
            document.Add(submittedToDetails);

            // Add department and university info
            Paragraph department = new Paragraph("Department of Computer Science", textFont);
            department.Font.Size = 18;
            department.Alignment = Element.ALIGN_CENTER;
            document.Add(department);
            Paragraph university = new Paragraph("University of Engineering And Technology, Lahore", boldFont);
            university.Font.Size = 24;
            university.Alignment = Element.ALIGN_CENTER;
            document.Add(university);
            return document;
        }


        private Document GetProjectsAndAdvisoryBoardWithStudents(ref Document document)
        {
            try
            {
                document.NewPage();
                Paragraph title = new Paragraph("List of Projects with Advisory Board and Students", boldFont);
                title.SpacingBefore = 20f;
                title.SpacingAfter = 20f;
                title.Font.Size = 20;
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT P.Title AS ProjectTitle, " +
                                                 "(PE.FirstName + ' ' + PE.LastName) AS ProjectAdvisor, " +
                                                 "(PEs.FirstName + ' ' + PEs.LastName) AS StudentName, GP.GroupId " +
                                                 "FROM Project P " +
                                                 "LEFT JOIN ProjectAdvisor PA ON P.Id = PA.ProjectId " +
                                                 "JOIN Advisor A ON A.Id = PA.AdvisorId " +
                                                 "JOIN Person PE ON PE.Id = A.Id " +
                                                 "JOIN GroupProject GP ON GP.ProjectId = P.Id " +
                                                 "JOIN [Group] G ON G.Id = GP.GroupId " +
                                                 "JOIN GroupStudent GS ON GS.GroupId = GP.GroupId " +
                                                 "JOIN Student S ON S.Id = GS.StudentId " +
                                                 "JOIN Person PEs ON PEs.Id = S.Id " +
                                                 "WHERE P.Title NOT LIKE 'del%' AND PE.FirstName NOT LIKE 'del%' AND PEs.FirstName NOT LIKE 'del%'",
                                                 con);
                SqlDataReader reader = cmd.ExecuteReader();

                PdfPTable table = new PdfPTable(reader.FieldCount);
                table.WidthPercentage = 100;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(reader.GetName(i)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = new BaseColor(128, 128, 128);
                    table.AddCell(cell);
                }

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(reader[i].ToString()));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }
                reader.Close();
                document.Add(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return document;
        }


        private void GenerateProjectsAndAdvisoryBoardWithStudentsReport()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (.pdf)|.pdf";
            sfd.FileName = "Projects_AdvisoryBoard_Students_Report.pdf";
            bool errorMessage = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = true;
                        MessageBox.Show("Unable to write data to disk: " + ex.Message);
                    }
                }

                if (!errorMessage)
                {
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sfd.FileName, FileMode.Create));
                    document.Open();

                    document = TitlePage(ref document);
                    document = GetProjectsAndAdvisoryBoardWithStudents(ref document);

                    document.Close();
                    writer.Close();
                }
            }
        }

        private Document GetUnassignedStudents(ref Document document)
        {
            try
            {
                document.NewPage();
                Paragraph title = new Paragraph("Unassigned Students", boldFont);
                title.SpacingBefore = 20f;
                title.SpacingAfter = 20f;
                title.Font.Size = 20;
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT (P.FirstName + ' ' + P.LastName) AS StudentName, S.RegistrationNo, (SELECT FORMAT(DateOfBirth, 'dd-MM-yyyy')) AS [DoB], P.Contact, P.Email, L.Value AS Gender " +
                                                 "FROM Student S " +
                                                 "LEFT JOIN Person P ON S.Id = P.Id " +
                                                 "LEFT JOIN GroupStudent GS ON GS.StudentId = S.Id " +
                                                 "LEFT JOIN [Group] G ON G.Id = GS.GroupId " +
                                                 "LEFT JOIN Lookup L ON L.Id = P.Gender " +
                                                 "WHERE G.Id IS NULL AND P.FirstName NOT LIKE 'del%'",
                                                 con);
                SqlDataReader reader = cmd.ExecuteReader();

                PdfPTable table = new PdfPTable(reader.FieldCount);
                table.WidthPercentage = 100;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(reader.GetName(i)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = new BaseColor(128, 128, 128);
                    table.AddCell(cell);
                }

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(reader[i].ToString()));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }
                reader.Close();
                document.Add(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return document;
        }

        // Usage
        private void GenerateUnassignedStudentsReport()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (.pdf)|.pdf";
            sfd.FileName = "Students_Not_In_Any_Group.pdf";
            bool errorMessage = false;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = true;
                        MessageBox.Show("Unable to write data in disk" + ex.Message);
                    }
                }
                if (!errorMessage)
                {
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sfd.FileName, FileMode.Create));
                    document.Open();

                    document = TitlePage(ref document);
                    document = GetUnassignedStudents(ref document);

                    document.Close();
                    writer.Close();
                }
            }
        }

        private Document InactiveStudentsList(ref Document document)
        {
            try
            {
                document.NewPage();
                Paragraph title = new Paragraph("Inactive Students List", boldFont);
                title.SpacingBefore = 20f;
                title.SpacingAfter = 20f;
                title.Font.Size = 20;
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT (P.FirstName + ' ' + P.LastName) AS StudentName, S.RegistrationNo, " +
                    "P.DateOfBirth AS DOB, P.Contact, P.Email, P.Gender " +
                    "FROM Student S " +
                    "LEFT JOIN Person P ON S.Id = P.Id " +
                    "JOIN GroupStudent GS ON GS.StudentId = S.Id " +
                    "WHERE GS.Status IN (SELECT Id FROM Lookup WHERE Value='Inactive')", con);
                SqlDataReader reader = cmd.ExecuteReader();

                PdfPTable table = new PdfPTable(reader.FieldCount);
                table.WidthPercentage = 100;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(reader.GetName(i)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = new BaseColor(128, 128, 128);
                    table.AddCell(cell);
                }

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(reader[i].ToString()));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }
                reader.Close();
                document.Add(table);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return document;
        }


        private void GenerateInactiveStudentsReport()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (.pdf)|.pdf";
            sfd.FileName = "Inactive_Students.pdf";
            bool errorMessage = false;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = true;
                        MessageBox.Show("Unable to write data in disk" + ex.Message);
                    }
                }
                if (!errorMessage)
                {
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sfd.FileName, FileMode.Create));
                    document.Open();

                    document = TitlePage(ref document);
                    document = InactiveStudentsList(ref document);

                    document.Close();
                    writer.Close();
                }
            }
        }
        private Document GroupEvaluationDetails(ref Document document)
        {
            try
            {
                document.NewPage();
                Paragraph title = new Paragraph("Group-wise Evaluation Summary", boldFont);
                title.SpacingBefore = 20f;
                title.SpacingAfter = 20f;
                title.Font.Size = 20;
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT G.Id AS GroupId, Pr.Title AS ProjectTitle, " +
                    "E.Name AS EvaluationName, SUM(GE.ObtainedMarks) AS TotalMarks " +
                    "FROM GroupEvaluation GE " +
                    "JOIN Evaluation E ON GE.EvaluationId = E.Id " +
                    "JOIN [Group] G ON GE.GroupId = G.Id " +
                    "JOIN GroupProject GP ON GP.GroupId = G.Id " +
                    "JOIN Project Pr ON Pr.Id = GP.ProjectId " +
                    "GROUP BY G.Id, Pr.Title, E.Name", con);
                SqlDataReader reader = cmd.ExecuteReader();

                PdfPTable table = new PdfPTable(reader.FieldCount);
                table.WidthPercentage = 100;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(reader.GetName(i)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = new BaseColor(128, 128, 128);
                    table.AddCell(cell);
                }

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(reader[i].ToString()));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }
                reader.Close();
                document.Add(table);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return document;
        }
        private void GenerateGroupEvaluationSummaryReport()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (.pdf)|.pdf";
            sfd.FileName = "Group_Evaluation_Summary.pdf";
            bool errorMessage = false;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = true;
                        MessageBox.Show("Unable to write data in disk" + ex.Message);
                    }
                }
                if (!errorMessage)
                {
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sfd.FileName, FileMode.Create));
                    document.Open();

                    document = TitlePage(ref document);
                    document = GroupEvaluationDetails(ref document);

                    document.Close();
                    writer.Close();
                }
            }
        }
        private List<int> GetEvaluationIds()
        {
            List<int> evaluationIds = new List<int>();

            using (var con = Configuration.getInstance().getConnection())
            {
                con.Open();

                string query = "SELECT Id FROM Evaluation ORDER BY Id ASC";

                using (var cmd = new SqlCommand(query, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            evaluationIds.Add(Convert.ToInt32(reader["Id"]));
                        }
                    }
                }
            }

            return evaluationIds;
        }

        private List<string> GetEvaluationTitle()
        {
            List<string> evaluationTitles = new List<string>();

            using (var con = Configuration.getInstance().getConnection())
            {
                con.Open();

                string query = "SELECT CONCAT(Name, CHAR(13), TotalMarks) AS name FROM Evaluation WHERE Evaluation.Name NOT LIKE 'del%' ORDER BY Id ASC";

                using (var cmd = new SqlCommand(query, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            evaluationTitles.Add(reader["name"].ToString());
                        }
                    }
                }
            }

            return evaluationTitles;
        }

        private Document GetMarkSheetOfProjects(ref Document document)
        {
            try
            {
                List<int> evaluationIds = GetEvaluationIds();
                List<string> evaluationTitles = GetEvaluationTitle();
                if (evaluationIds.Count > 0)
                {
                    string query = "";
                    int idx = 0;
                    foreach (int evaluationId in evaluationIds)
                    {
                        query += ",MAX(CASE WHEN E.Id=" + evaluationId + " THEN GE.ObtainedMarks END) AS [" + evaluationTitles[idx] + "]";
                        idx++;
                    }
                    document.NewPage();

                    Paragraph title = new Paragraph("Mark Sheet of Projects", boldFont);
                    title.SpacingBefore = 20f;
                    title.SpacingAfter = 20f;
                    title.Font.Size = 20;
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("SELECT S.RegistrationNo AS [Reg No], " +
                                                     "MAX(CONCAT(P.FirstName,' ',P.LastName)) AS [Student Name], " +
                                                     "MAX(Pr.Title) AS [Project Title] " + query + ", " +
                                                     "SUM((GE.ObtainedMarks*E.TotalWeightage)/E.TotalMarks) AS [Total Marks] " +
                                                     "FROM GroupEvaluation AS GE " +
                                                     "JOIN Evaluation AS E ON GE.EvaluationId=E.Id " +
                                                     "JOIN GroupStudent AS GS ON GS.GroupId=GE.GroupId " +
                                                     "JOIN Student AS S ON S.Id=GS.StudentId " +
                                                     "JOIN Person AS P ON P.Id=S.Id " +
                                                     "JOIN GroupProject AS GP ON GP.GroupId=GE.GroupId " +
                                                     "JOIN Project AS Pr ON Pr.Id=GP.ProjectId " +
                                                     "WHERE GS.Status IN (SELECT Id FROM Lookup WHERE Value='Active') " +
                                                     "AND P.FirstName NOT LIKE 'del%' " +
                                                     "AND Pr.Title NOT LIKE 'del%' " +
                                                     "AND E.Name NOT LIKE 'del%' " +
                                                     "GROUP BY GE.GroupId,Pr.Id,S.Id,S.RegistrationNo " +
                                                     "ORDER BY GE.GroupId,Pr.Id,S.RegistrationNo",
                                                     con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    PdfPTable table = new PdfPTable(reader.FieldCount);
                    table.WidthPercentage = 100;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(reader.GetName(i)));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = new BaseColor(128, 128, 128);
                        table.AddCell(cell);
                    }

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(reader[i].ToString()));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(cell);
                        }
                    }
                    reader.Close();
                    document.Add(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return document;
        }
        private void GenerateMarkSheetOfProjectsReport()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (.pdf)|.pdf";
            sfd.FileName = "MarkSheet_Projects_Report.pdf";
            bool errorMessage = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = true;
                        MessageBox.Show("Unable to write data to disk: " + ex.Message);
                    }
                }

                if (!errorMessage)
                {
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sfd.FileName, FileMode.Create));
                    document.Open();

                    document = TitlePage(ref document);
                    document = GetMarkSheetOfProjects(ref document);

                    document.Close();
                    writer.Close();
                }
            }
        }
        private Document GetNonGroupProjects(ref Document document)
        {
            try
            {
                document.NewPage();
                Paragraph title = new Paragraph("Non-Group Projects", boldFont);
                title.SpacingBefore = 20f;
                title.SpacingAfter = 20f;
                title.Font.Size = 20;
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT P.Title AS ProjectTitle, " +
                                                 "(PE.FirstName + ' ' + PE.LastName) AS ProjectAdvisor " +
                                                 "FROM Project P " +
                                                 "LEFT JOIN ProjectAdvisor PA ON P.Id = PA.ProjectId " +
                                                 "JOIN Advisor A ON A.Id = PA.AdvisorId " +
                                                 "JOIN Person PE ON PE.Id = A.Id " +
                                                 "WHERE P.Title NOT LIKE 'del%' AND PE.FirstName NOT LIKE 'del%'",
                                                 con);
                SqlDataReader reader = cmd.ExecuteReader();

                PdfPTable table = new PdfPTable(reader.FieldCount);
                table.WidthPercentage = 100;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(reader.GetName(i)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = new BaseColor(128, 128, 128);
                    table.AddCell(cell);
                }

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(reader[i].ToString()));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }
                reader.Close();
                document.Add(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return document;
        }

        // Usage
        private void GenerateNonGroupProjectsReport()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (.pdf)|.pdf";
            sfd.FileName = "Non_Group_Projects_Report.pdf";
            bool errorMessage = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = true;
                        MessageBox.Show("Unable to write data to disk: " + ex.Message);
                    }
                }

                if (!errorMessage)
                {
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sfd.FileName, FileMode.Create));
                    document.Open();

                    document = TitlePage(ref document);
                    document = GetNonGroupProjects(ref document);

                    document.Close();
                    writer.Close();
                }
            }
        }

        private void genReport1_Click(object sender, EventArgs e)
        {
            GenerateProjectsAndAdvisoryBoardWithStudentsReport();
        }

        private void genReport2_Click(object sender, EventArgs e)
        {
            GenerateNonGroupProjectsReport();
        }

        private void genReport3_Click(object sender, EventArgs e)
        {
            GenerateInactiveStudentsReport();
        }
        private void genReport4_Click(object sender, EventArgs e)
        {
            GenerateUnassignedStudentsReport();
        }

        private void genReport5_Click(object sender, EventArgs e)
        {
            GenerateGroupEvaluationSummaryReport();
        }

        private void reportGen_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateMarkSheetOfProjectsReport();
        }
    }
}