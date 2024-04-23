using EventScheduleApp.Models;
using EventScheduleApp.Views.UserControls;
using Google.Cloud.Firestore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventScheduleApp.Views
{
    public partial class AddEventForm : Form
    {
        public event EventHandler<EventAddedEventArgs> EventAdded;
        private FirestoreDb db;

        

        public AddEventForm()
        {
            InitializeComponent();
            InitializeFirestore();
        }

        private void InitializeFirestore()
        {
            // Change the credentials file path to the path of your own credentials file
            string projectId = "testeventlast";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\User\\Desktop\\Studee\\EventScheduleApp\\EventScheduleApp\\FirebaseCred.json");
            db = FirestoreDb.Create(projectId);
        }

        private void AddEventForm_Load(object sender, EventArgs e)
        {
            txdate.Text = EventDashboard.static_month + "/" + UserControlDay.static_day + "/" + EventDashboard.static_year;
        }

        private string GenerateTaskID()
        {
            // Generate a unique task ID using timestamp and a random number
            string timestamp = System.DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            string randomNumber = new Random().Next(1000, 9999).ToString();
            string taskID ="event" + timestamp + randomNumber;
            return taskID;
        }


        private async void btnSave_Click(object sender, EventArgs e)
        {

            EventData EventData = new EventData
            {
                EventID = GenerateTaskID(),
                Event = txevent.Text,
                Date = txdate.Text,
            };

            // Raise the EventAdded event and pass event name and date
            EventAdded?.Invoke(this, new EventAddedEventArgs(txevent.Text, txdate.Text));

            var EventItemData = new Dictionary<string, object>
            {
                { "EventID", EventData.EventID },
                { "Event", EventData.Event },
                { "Date", EventData.Date },
            };
            try
            {
                // Ensure that userLoggedEmail is replaced with the actual user's email
                DocumentReference eventDoc = db.Collection("userLoggedEmail").Document(EventData.EventID);

                // Pass EventItemData instead of EventData
                await eventDoc.SetAsync(EventItemData);

                MessageBox.Show("Event Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding event: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
