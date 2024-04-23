using System;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace EventScheduleApp.Views.UserControls
{
    public partial class UserControlDay : UserControl
    {
        private FirestoreDb db;
        public static string static_day;

        public UserControlDay()
        {
            InitializeComponent();
            InitializeFirestore();
            DisplayEvents();
        }

        private void InitializeFirestore()
        {
            string projectId = "testeventlast";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\User\\Desktop\\Studee\\EventScheduleApp\\EventScheduleApp\\FirebaseCred.json");
            db = FirestoreDb.Create(projectId);
        }

        public void days(int numday)
        {
            lbdays.Text = numday.ToString();
        }

        private void DayClick(object sender, EventArgs e)
        {
            static_day = lbdays.Text;
            timer1.Start();
            // Open the form for adding events when the day is clicked
            OpenAddEventForm();
        }

        private async void DisplayEvents()
        {
            try
            {
                // Construct the date string
                string dateString = $"{EventDashboard.static_month}/{UserControlDay.static_day}/{EventDashboard.static_year}";

                // Query Firestore for events on the specified date
                CollectionReference eventsCollection = db.Collection("userLoggedEmail");
                QuerySnapshot querySnapshot = await eventsCollection.WhereEqualTo("Date", dateString).GetSnapshotAsync();

                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    // Extract event details from the document
                    if (documentSnapshot.TryGetValue("Event", out object eventName))
                    {
                        // Display the event in the user control
                        lbevent.Text = eventName.ToString();
                        Console.WriteLine($"Event loaded: {lbevent.Text}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TimerRun(object sender, EventArgs e)
        {
            // Refresh events periodically
            DisplayEvents();
        }

        public void SetEvent(string eventName, string date)
        {
            // Set event details in the user control
            lbevent.Text = eventName;
        }

        private void OpenAddEventForm()
        {
            // Open the form for adding events
            AddEventForm addEventForm = new AddEventForm();
            addEventForm.EventAdded += (sender, args) =>
            {
                // Update the displayed event after adding a new event
                lbevent.Text = args.EventName;
            };
            addEventForm.Show();
        }
    }
}
