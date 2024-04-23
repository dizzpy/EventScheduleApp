using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventScheduleApp.Views.UserControls
{
    public partial class UserControlDay : UserControl
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;database=eventdb;port=3306;username=root;password=");
        public static string static_day;
        public UserControlDay()
        {
            InitializeComponent();
            DisplayEvents();
        }

        public void days(int numday)
        {
            lbdays.Text = numday + "";
        }

        private void DayClick(object sender, EventArgs e)
        {
            static_day = lbdays.Text;
            timer1.Start();
            AddEventForm eventform = new AddEventForm();
            eventform.Show();
        }

        private void DisplayEvents()
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM eventdb.users WHERE Date = @Date";
                MySqlCommand commandDatabase = new MySqlCommand(query, connection);

                // Construct the date string in the format expected by the database
                string dateString = EventDashboard.static_month + "/" + UserControlDay.static_day + "/" + EventDashboard.static_year;

                commandDatabase.Parameters.AddWithValue("@Date", dateString);

                using (MySqlDataReader reader = commandDatabase.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lbevent.Text = reader["Event"].ToString();
                        Console.WriteLine($"Event loaded: {lbevent.Text}");
                    }
                    else
                    {
                        Console.WriteLine("No event found for the specified date.");
                    }
                }
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



        private void TimerRun(object sender, EventArgs e)
        {
            DisplayEvents();
        }

        public void SetEvent(string eventName, string date)
        {
            lbevent.Text = eventName;
        }
    }
}
