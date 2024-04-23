using EventScheduleApp.Views;
using EventScheduleApp.Views.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventScheduleApp
{
    public partial class EventDashboard : Form
    {
        int month, year;

        public static int static_month, static_year;

        public EventDashboard()
        {
            InitializeComponent();
        }

        private void EventDashboard_Load(object sender, EventArgs e)
        {
            displadays();
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            daycontainer.Controls.Clear();

            month++;
            static_month = month;
            static_year = year;

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbdate.Text = monthname + " " + year;

            DateTime startofthemonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);

            int dayofweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayofweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Controls.Add(ucblank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDay ucdays = new UserControlDay();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }
        }

        private void displadays()
        {
            DateTime now = DateTime.Now;

            month = now.Month;
            year = now.Year;
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);

            static_month = month;
            static_year = year;


            lbdate.Text = monthname + " " + year;

            DateTime startofthemonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);

            int dayofweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayofweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Controls.Add(ucblank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDay ucdays = new UserControlDay();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }
        }

        private void btnprevious_Click(object sender, EventArgs e)
        {
            daycontainer.Controls.Clear();

            month--;
            static_month = month;
            static_year = year;

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbdate.Text = monthname + " " + year;

            DateTime startofthemonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);

            int dayofweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayofweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Controls.Add(ucblank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDay ucdays = new UserControlDay();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }
        }

        private void OpenAddEventForm()
        {
            AddEventForm addEventForm = new AddEventForm();
            addEventForm.EventAdded += (sender, args) =>
            {
                // Pass event name and date to UserControlDay instance
                foreach (Control control in daycontainer.Controls)
                {
                    if (control is UserControlDay userControlDay)
                    {
                        userControlDay.SetEvent(args.EventName, args.Date);
                    }
                }
            };
            addEventForm.Show();
        }
    }
}
