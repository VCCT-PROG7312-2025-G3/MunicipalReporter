using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MunicipalReporter.Forms
{
    public class ReportIssuesForm : Form
    {
        // Controls
        private TextBox txtLocation;
        private ComboBox cmbCategory;
        private RichTextBox rtbDescription;
        private Label lblAttachment;
        private string attachmentPath = "";
        private ProgressBar progressBar;
        private ListView lvReports;
        private List<Report> reports = new List<Report>();
        private Label lblProgress;

        public ReportIssuesForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Report an Issue";
            this.Width = 600;
            this.Height = 700;
            this.StartPosition = FormStartPosition.CenterParent;

            // Location
            Label lblLocation = new Label() { Text = "Location:", Left = 20, Top = 20, Width = 80 };
            txtLocation = new TextBox() { Left = 110, Top = 18, Width = 450 };

            // Category
            Label lblCategory = new Label() { Text = "Category:", Left = 20, Top = 60, Width = 80 };
            cmbCategory = new ComboBox() { Left = 110, Top = 58, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCategory.Items.AddRange(new string[] { "Sanitation", "Roads", "Water", "Electricity", "Waste Collection", "Other" });
            cmbCategory.SelectedIndex = 0;

            // Description
            Label lblDescription = new Label() { Text = "Description:", Left = 20, Top = 100, Width = 80 };
            rtbDescription = new RichTextBox() { Left = 110, Top = 98, Width = 450, Height = 150 };

            // Attach File Button
            Button btnAttach = new Button() { Text = "Attach File...", Left = 110, Top = 260, Width = 120 };
            lblAttachment = new Label() { Text = "No file attached", Left = 240, Top = 265, Width = 320 };
            btnAttach.Click += BtnAttach_Click;

            // Submit Button
            Button btnSubmit = new Button() { Text = "Submit Report", Left = 110, Top = 300, Width = 120 };
            btnSubmit.Click += BtnSubmit_Click;

            // Progress Bar & label
            progressBar = new ProgressBar() { Left = 20, Top = 370, Width = 550, Height = 20, Minimum = 0, Maximum = 100, Value = 0 };
            lblProgress = new Label() { Text = "No reports yet.", Left = 20, Top = 400, Width = 550 };

            // Reports ListView
            lvReports = new ListView() { Left = 20, Top = 430, Width = 550, Height = 200, View = View.Details };
            lvReports.Columns.Add("Location", 100);
            lvReports.Columns.Add("Category", 100);
            lvReports.Columns.Add("Description", 200);
            lvReports.Columns.Add("Status", 80);
            lvReports.Columns.Add("Submitted At", 120);

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

        private void BtnAttach_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    attachmentPath = ofd.FileName;
                    lblAttachment.Text = Path.GetFileName(attachmentPath);
                }
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            string location = txtLocation.Text.Trim();
            string category = cmbCategory.SelectedItem?.ToString() ?? "Other";
            string description = rtbDescription.Text.Trim();

            if (string.IsNullOrEmpty(location) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please fill in the Location and Description fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Update progress bar and label as a simple indicator
            int percent = Math.Min(100, (int)(reports.Count * 10));
            progressBar.Value = percent;
            lblProgress.Text = $"{reports.Count} report(s) submitted.";

            MessageBox.Show("Report submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetForm()
        {
            txtLocation.Text = "";
            cmbCategory.SelectedIndex = 0;
            rtbDescription.Text = "";
            attachmentPath = "";
            lblAttachment.Text = "No file attached";
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

    // Keep Report model near the form or move it to a Models folder as needed
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