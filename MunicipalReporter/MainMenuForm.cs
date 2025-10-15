using System;
using System.Windows.Forms;
using MunicipalReporter.Models;

namespace MunicipalReporter.Forms
{
    public partial class MainMenuForm : Form
    {
        private EventRepository eventRepo;
        private RecommendationEngine recommender;

        public MainMenuForm()
        {
            InitializeComponent();
            eventRepo = new EventRepository();
            recommender = new RecommendationEngine(eventRepo);
        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            // Hide the main menu while report form is open
            this.Hide();

            using (var reportForm = new MunicipalReporter.Forms.ReportIssuesForm())
            {
                reportForm.ShowDialog(); // modal — user must close it to continue
            }

            // After report form closes, show the main menu again
            this.Show();
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            var eventsForm = new LocalEventsForm(eventRepo, recommender);
            eventsForm.ShowDialog();
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Service Request Status will be implemented in Task 3.", "Service Status");
        }
    }
}