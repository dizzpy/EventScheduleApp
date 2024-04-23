using EventScheduleApp.Views.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventScheduleApp.Views
{
    public partial class AddEventForm : Form
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;database=studeedb;port=3306;username=root;password=");

        public AddEventForm()
        {
            InitializeComponent();
        }

        private void AddEventForm_Load(object sender, EventArgs e)
        {
            txdate.Text = EventDashboard.static_month + "/" + UserControlDay.static_day + "/" + EventDashboard.static_year;
        }
    }
}
