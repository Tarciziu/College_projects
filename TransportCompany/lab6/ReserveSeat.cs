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
    public partial class ReserveSeat : Form
    {
        private ClientCtrl ctrl;
        private Reservation reservation;
        public ReserveSeat(ClientCtrl ctrl, Reservation reservation)
        {
            InitializeComponent();
            this.ctrl = ctrl;
            this.reservation = reservation;
        }

        private void reserveBtn_Click(object sender, EventArgs e)
        {
            reservation.clientName = nameTxt.Text;
            ctrl.updateReservation(reservation);
            ctrl.notifyNewReservation(reservation);
            this.Close();
        }
    }
}
