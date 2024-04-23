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
        public static string static_day;
        public UserControlDay()
        {
            InitializeComponent();
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
    }
}
