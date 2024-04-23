using EventScheduleApp.Views.UserControls;
using MySqlConnector;
using System;
using System.Data;
using System.Windows.Forms;

namespace EventScheduleApp.Views
{
    public partial class AddEventForm : Form
    {
        public event EventHandler<EventAddedEventArgs> EventAdded;
        MySqlConnection connection = new MySqlConnection("server=localhost;database=eventdb;port=3306;username=root;password=");

        public AddEventForm()
        {
            InitializeComponent();
        }

        private void AddEventForm_Load(object sender, EventArgs e)
        {
            txdate.Text = EventDashboard.static_month + "/" + UserControlDay.static_day + "/" + EventDashboard.static_year;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                string iquery = "INSERT INTO eventdb.users(`ID`, `Event`, `Date`) VALUES(NULL, @Event, @Date)";

                MySqlCommand commandDatabase = new MySqlCommand(iquery, connection);
                commandDatabase.Parameters.AddWithValue("@Event", txevent.Text);
                commandDatabase.Parameters.AddWithValue("@Date", txdate.Text);

                commandDatabase.ExecuteNonQuery();
                MessageBox.Show("Data Added Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //need to pass Event Name and Date to UserControlDay class
                // Raise the EventAdded event and pass event name and date
                EventAdded?.Invoke(this, new EventAddedEventArgs(txevent.Text, txdate.Text));

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
