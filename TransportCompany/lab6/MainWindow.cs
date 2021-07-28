using domain;
using services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class MainWindow : Form
    {
        private readonly ClientCtrl ctrl;
        private readonly IList<Route> routes;
        private readonly IList<Reservation> reservations;
        public MainWindow(ClientCtrl ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
            routes = ctrl.findAllRoutes().ToList();
            tableRoutes.DataSource = routes;
            tableReservations.DataSource = reservations;
            ctrl.updateEvent += userUpdate;
        }

        public void userUpdate(object sender, UserEventArgs e)
        {
            if (e.UserEventType == UserEvent.NewReservation)
            {
                Reservation reservation = (Reservation)e.Data;
                tableRoutes.BeginInvoke(new UpdateGridViewCallback(this.updateGridView), new object[] { tableRoutes, ctrl});
                tableReservations.DataSource = null;
            }
            
        }

        public delegate void UpdateGridViewCallback(DataGridView gridView, ClientCtrl ctrl);

        private void updateGridView(DataGridView gridView, ClientCtrl ctrl)
        {
            gridView.DataSource = null;
            gridView.DataSource = ctrl.findAllRoutes();
        }


        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("MainWindow closing " + e.CloseReason);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                ctrl.logout();
                ctrl.updateEvent -= userUpdate;
                Application.Exit();
            }

        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            if (destinationTxt.Text.Length > 0)
            {
                tableRoutes.DataSource = ctrl.findAllRoutes().Where(x => x.destination.ToLower()
                .StartsWith(destinationTxt.Text.ToLower()) &&
                x.departureDateTime.Date.CompareTo(datePicker.Value.Date) == 0).ToList();
            }
            else
            {
                tableRoutes.DataSource = ctrl.findAllRoutes()
                    .Where(x => x.departureDateTime.Date
                                .CompareTo(datePicker.Value.Date) == 0).ToList();
            }
        }

        private bool checkDate(Route x)
        {
            Console.WriteLine(x.departureDateTime.Date
                                .CompareTo(datePicker.Value.Date) == 0);
            return x.departureDateTime.Date
                                .CompareTo(datePicker.Value.Date) == 0;
        }

        private void tableRoutes_MouseClick(object sender, MouseEventArgs e)
        {
            int selectedrowindex = tableRoutes.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tableRoutes.Rows[selectedrowindex];
            int cellValue = Convert.ToInt32(selectedRow.Cells["id"].Value);
            tableReservations.DataSource = ctrl.findReservationByRoute((Route)selectedRow.DataBoundItem);

        }

        private void tableReservations_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int selectedrowindex = tableReservations.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tableReservations.Rows[selectedrowindex];
            string cellValue = Convert.ToString(selectedRow.Cells["clientName"].Value);
            if (cellValue == "-")
            {
                ReserveSeat reserveSeat = new ReserveSeat(ctrl, (Reservation)selectedRow.DataBoundItem);
                reserveSeat.Show();
            }
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            ctrl.logout();
            Form1 login = new Form1(ctrl);
            this.Hide();
            login.ShowDialog();
            this.Close();
        }
    }
}
