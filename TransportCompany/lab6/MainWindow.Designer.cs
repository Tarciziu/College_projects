
namespace client
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logoutBtn = new System.Windows.Forms.Button();
            this.searchBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.destinationTxt = new System.Windows.Forms.TextBox();
            this.tableReservations = new System.Windows.Forms.DataGridView();
            this.tableRoutes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tableReservations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableRoutes)).BeginInit();
            this.SuspendLayout();
            // 
            // logoutBtn
            // 
            this.logoutBtn.Location = new System.Drawing.Point(699, 30);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(75, 23);
            this.logoutBtn.TabIndex = 15;
            this.logoutBtn.Text = "Log out";
            this.logoutBtn.UseVisualStyleBackColor = true;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(600, 30);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 14;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(317, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Date:";
            // 
            // datePicker
            // 
            this.datePicker.Location = new System.Drawing.Point(371, 34);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(169, 20);
            this.datePicker.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Destination:";
            // 
            // destinationTxt
            // 
            this.destinationTxt.Location = new System.Drawing.Point(138, 33);
            this.destinationTxt.Name = "destinationTxt";
            this.destinationTxt.Size = new System.Drawing.Size(144, 20);
            this.destinationTxt.TabIndex = 10;
            // 
            // tableReservations
            // 
            this.tableReservations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableReservations.Location = new System.Drawing.Point(525, 64);
            this.tableReservations.Name = "tableReservations";
            this.tableReservations.Size = new System.Drawing.Size(240, 322);
            this.tableReservations.TabIndex = 17;
            this.tableReservations.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tableReservations_MouseDoubleClick);
            // 
            // tableRoutes
            // 
            this.tableRoutes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableRoutes.Location = new System.Drawing.Point(36, 64);
            this.tableRoutes.Name = "tableRoutes";
            this.tableRoutes.Size = new System.Drawing.Size(413, 322);
            this.tableRoutes.TabIndex = 16;
            this.tableRoutes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tableRoutes_MouseClick);
            this.tableRoutes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tableReservations_MouseDoubleClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 396);
            this.Controls.Add(this.tableReservations);
            this.Controls.Add(this.tableRoutes);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.destinationTxt);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            ((System.ComponentModel.ISupportInitialize)(this.tableReservations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableRoutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button logoutBtn;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox destinationTxt;
        private System.Windows.Forms.DataGridView tableReservations;
        private System.Windows.Forms.DataGridView tableRoutes;
    }
}