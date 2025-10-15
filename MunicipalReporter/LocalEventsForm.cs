using System;
using System.Linq;
using System.Windows.Forms;
using MunicipalReporter.Models;

namespace MunicipalReporter.Forms
{
    public partial class LocalEventsForm : Form
    {
        private EventRepository repo;
        private RecommendationEngine recommender;

        public LocalEventsForm(EventRepository repository, RecommendationEngine recommendationEngine)
        {
            InitializeComponent();
            repo = repository;
            recommender = recommendationEngine;
        }

        private void LocalEventsForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadEventGrid(repo.GetAllEvents());
            ShowRecommendations();
            checkUseDateRange.Checked = false;
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today.AddDays(30);
        }

        private void LoadCategories()
        {
            comboCategory.Items.Clear();
            comboCategory.Items.Add("(All)");
            foreach (var c in repo.GetCategories())
                comboCategory.Items.Add(c);
            comboCategory.SelectedIndex = 0;
        }

        private void LoadEventGrid(System.Collections.Generic.IEnumerable<Event> events)
        {
            dgvEvents.Rows.Clear();
            foreach (var ev in events)
            {
                dgvEvents.Rows.Add(ev.Date.ToString("yyyy-MM-dd"), ev.Title, ev.Category, ev.Location, ev.Description);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var q = txtSearch.Text.Trim();
            var category = comboCategory.SelectedItem?.ToString();
            if (category == "(All)") category = null;
            DateTime? start = null, end = null;
            if (checkUseDateRange.Checked)
            {
                start = dtpStart.Value.Date;
                end = dtpEnd.Value.Date;
            }

            if (!string.IsNullOrWhiteSpace(q))
                recommender.RegisterSearch(q);

            var results = repo.SearchEvents(q, category, start, end);
            LoadEventGrid(results);
            ShowRecommendations();
        }

        private void ShowRecommendations()
        {
            lstRecommendations.Items.Clear();
            var recs = recommender.RecommendEvents(5);
            foreach (var r in recs)
                lstRecommendations.Items.Add(r.ToString());
        }

        private void btnAddSampleEvent_Click(object sender, EventArgs e)
        {
            // helpful button while developing to add a sample event quickly
            var ev = new Event("New Sample", "Sample", "Added by developer", DateTime.Today.AddDays(2), "Test Hall");
            repo.AddEvent(ev);
            LoadCategories();
            LoadEventGrid(repo.GetAllEvents());
        }
    }
}