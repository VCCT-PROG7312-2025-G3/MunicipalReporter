using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalReporter
{
    public class MainForm : Form
    {
        // Declare controls as class members
        private TextBox txtLocation;
        private ComboBox cmbCategory;
        private RichTextBox rtbDescription;
        private Label lblAttachment;
        private string attachmentPath = "";
        private ProgressBar progressBar;
        private ListView lvReports;
        private List<Report> reports = new List<Report>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Municipal Services Reporter";
            this.Width = 600;
            this.Height = 700;

            // Location
            Label lblLocation = new Label() { Text = "Location:", Left = 20, Top = 20, Width = 80 };
            txtLocation = new TextBox() { Left = 110, Top = 18, Width = 450 };

            // Category
            Label lblCategory = new Label() { Text = "Category:", Left = 20, Top = 60, Width = 80 };
            cmbCategory = new ComboBox() { Left = 110, Top = 58, Width = 200 };
            cmbCategory.Items.AddRange(new string[] { "Sanitation", "Roads", "Water", "Electricity", "Waste Collection", "Other" });
            cmbCategory.SelectedIndex = 0;

            // Description
            Label lblDescription = new Label() { Text = "Description:", Left = 20, Top = 100, Width = 80 };
            rtbDescription = new RichTextBox() { Left = 110, Top = 98, Width = 450, Height = 150 };

            // Attach File Button
            Button btnAttach = new Button() { Text = "Attach File...", Left = 110, Top = 260, Width = 120 };
            lblAttachment = new Label() { Text = "No file attached", Left = 240, Top = 265, Width = 350 };
            btnAttach.Click += (s, e) =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    attachmentPath = ofd.FileName;
                    lblAttachment.Text = Path.GetFileName(attachmentPath);
                }
            };

            // Submit Button
            Button btnSubmit = new Button() { Text = "Submit Report", Left = 110, Top = 300, Width = 120 };
            btnSubmit.Click += (s, e) =>
            {
                string location = txtLocation.Text.Trim();
                string category = cmbCategory.SelectedItem.ToString();
                string description = rtbDescription.Text.Trim();

                if (string.IsNullOrEmpty(location) || string.IsNullOrEmpty(description))
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Report report = new Report()
                {
                    Location = location,
                    Category = category,
                    Description = description,
                    AttachmentPath = attachmentPath,
                    SubmittedAt = DateTime.Now,
                    Status = "Submitted"
                };

                reports.Add(report);
                UpdateReportList();
                ResetForm();
                MessageBox.Show("Report submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Progress Bar
            progressBar = new ProgressBar() { Left = 20, Top = 370, Width = 550, Height = 20, Minimum = 0, Maximum = 100 };
            Label lblProgress = new Label() { Text = "No reports yet.", Left = 20, Top = 400, Width = 550 };

            // Reports ListView
            lvReports = new ListView() { Left = 20, Top = 430, Width = 550, Height = 200 };
            lvReports.View = View.Details;
            lvReports.Columns.Add("Location", 100);
            lvReports.Columns.Add("Category", 100);
            lvReports.Columns.Add("Description", 200);
            lvReports.Columns.Add("Status", 80);
            lvReports.Columns.Add("Submitted At", 70);

            // Add controls to form
            this.Controls.Add(lblLocation);
            this.Controls.Add(txtLocation);
            this.Controls.Add(lblCategory);
            this.Controls.Add(cmbCategory);
            this.Controls.Add(lblDescription);
            this.Controls.Add(rtbDescription);
            this.Controls.Add(btnAttach);
            this.Controls.Add(lblAttachment);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(progressBar);
            this.Controls.Add(lblProgress);
            this.Controls.Add(lvReports);
        }

        private void ResetForm()
        {
            txtLocation.Text = "";
            cmbCategory.SelectedIndex = 0;
            rtbDescription.Text = "";
            attachmentPath = "";
        }

        private void UpdateReportList()
        {
            lvReports.Items.Clear();
            foreach (var report in reports)
            {
                var item = new ListViewItem(report.Location);
                item.SubItems.Add(report.Category);
                item.SubItems.Add(report.Description);
                item.SubItems.Add(report.Status);
                item.SubItems.Add(report.SubmittedAt.ToString("g"));
                lvReports.Items.Add(item);
            }
        }
    }

    public class Report
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string Status { get; set; }
    }
}