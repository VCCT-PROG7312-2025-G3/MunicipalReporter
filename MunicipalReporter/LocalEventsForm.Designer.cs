namespace MunicipalReporter.Forms
{
    partial class LocalEventsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvEvents;
        private System.Windows.Forms.ComboBox comboCategory;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.CheckBox checkUseDateRange;
        private System.Windows.Forms.ListBox lstRecommendations;
        private System.Windows.Forms.Button btnAddSampleEvent;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvEvents = new DataGridView();
            comboCategory = new ComboBox();
            txtSearch = new TextBox();
            btnSearch = new Button();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            checkUseDateRange = new CheckBox();
            lstRecommendations = new ListBox();
            btnAddSampleEvent = new Button();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            SuspendLayout();
            // 
            // dgvEvents
            // 
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEvents.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
            dgvEvents.Location = new Point(12, 77);
            dgvEvents.Name = "dgvEvents";
            dgvEvents.ReadOnly = true;
            dgvEvents.RowTemplate.Height = 24;
            dgvEvents.Size = new Size(900, 350);
            dgvEvents.TabIndex = 0;
            // 
            // comboCategory
            // 
            comboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCategory.Location = new Point(12, 20);
            comboCategory.Name = "comboCategory";
            comboCategory.Size = new Size(200, 23);
            comboCategory.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(230, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(280, 23);
            txtSearch.TabIndex = 2;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(780, 16);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(132, 30);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(530, 20);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(180, 23);
            dtpStart.TabIndex = 4;
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(530, 48);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(180, 23);
            dtpEnd.TabIndex = 5;
            // 
            // checkUseDateRange
            // 
            checkUseDateRange.AutoSize = true;
            checkUseDateRange.Location = new Point(430, 22);
            checkUseDateRange.Name = "checkUseDateRange";
            checkUseDateRange.Size = new Size(81, 19);
            checkUseDateRange.TabIndex = 6;
            checkUseDateRange.Text = "Use Range";
            checkUseDateRange.UseVisualStyleBackColor = true;
            // 
            // lstRecommendations
            // 
            lstRecommendations.FormattingEnabled = true;
            lstRecommendations.Location = new Point(12, 440);
            lstRecommendations.Name = "lstRecommendations";
            lstRecommendations.Size = new Size(900, 94);
            lstRecommendations.TabIndex = 7;
            // 
            // btnAddSampleEvent
            // 
            btnAddSampleEvent.Location = new Point(660, 16);
            btnAddSampleEvent.Name = "btnAddSampleEvent";
            btnAddSampleEvent.Size = new Size(100, 30);
            btnAddSampleEvent.TabIndex = 8;
            btnAddSampleEvent.Text = "Add Sample";
            btnAddSampleEvent.UseVisualStyleBackColor = true;
            btnAddSampleEvent.Click += btnAddSampleEvent_Click;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // LocalEventsForm
            // 
            ClientSize = new Size(930, 560);
            Controls.Add(btnAddSampleEvent);
            Controls.Add(lstRecommendations);
            Controls.Add(checkUseDateRange);
            Controls.Add(dtpEnd);
            Controls.Add(dtpStart);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(comboCategory);
            Controls.Add(dgvEvents);
            Name = "LocalEventsForm";
            Text = "Local Events and Announcements";
            Load += LocalEventsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}