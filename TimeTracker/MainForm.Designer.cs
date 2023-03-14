
namespace TimeTracker
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cmbProjects = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstHistory = new System.Windows.Forms.ListView();
            this.Project = new System.Windows.Forms.ColumnHeader();
            this.Date = new System.Windows.Forms.ColumnHeader();
            this.StartTime = new System.Windows.Forms.ColumnHeader();
            this.EndTime = new System.Windows.Forms.ColumnHeader();
            this.Duration = new System.Windows.Forms.ColumnHeader();
            this.Description = new System.Windows.Forms.ColumnHeader();
            this.lblscreen = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblKeyboardStock = new System.Windows.Forms.Label();
            this.lblMouseClick = new System.Windows.Forms.Label();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbProjects
            // 
            this.cmbProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProjects.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmbProjects.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjects.DropDownWidth = 589;
            this.cmbProjects.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbProjects.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbProjects.FormattingEnabled = true;
            this.cmbProjects.ItemHeight = 50;
            this.cmbProjects.Location = new System.Drawing.Point(12, 12);
            this.cmbProjects.Name = "cmbProjects";
            this.cmbProjects.Size = new System.Drawing.Size(589, 56);
            this.cmbProjects.TabIndex = 0;
            this.cmbProjects.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbProjects_DrawItem);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.White;
            this.btnStart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStart.BackgroundImage")));
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.LightSkyBlue;
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.Location = new System.Drawing.Point(12, 80);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 64);
            this.btnStart.TabIndex = 1;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.White;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStop.Enabled = false;
            this.btnStop.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(174, 80);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 64);
            this.btnStop.TabIndex = 3;
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTime.Location = new System.Drawing.Point(12, 170);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(103, 35);
            this.lblTime.TabIndex = 12;
            this.lblTime.Text = "00:00:00";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.White;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReset.Enabled = false;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.Location = new System.Drawing.Point(93, 80);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 64);
            this.btnReset.TabIndex = 2;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 807);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(613, 34);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "status online";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(122, 28);
            this.toolStripStatusLabel1.Text = "status online";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel2.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(200, 4, 0, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(51, 28);
            this.toolStripStatusLabel2.Text = "Name";
            // 
            // lstHistory
            // 
            this.lstHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Project,
            this.Date,
            this.StartTime,
            this.EndTime,
            this.Duration,
            this.Description});
            this.lstHistory.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lstHistory.FullRowSelect = true;
            this.lstHistory.GridLines = true;
            this.lstHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstHistory.HideSelection = false;
            this.lstHistory.Location = new System.Drawing.Point(12, 366);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(589, 182);
            this.lstHistory.TabIndex = 14;
            this.lstHistory.TabStop = false;
            this.lstHistory.UseCompatibleStateImageBehavior = false;
            this.lstHistory.View = System.Windows.Forms.View.Details;
            // 
            // Project
            // 
            this.Project.Name = "Project";
            this.Project.Text = "Title";
            this.Project.Width = 80;
            // 
            // Date
            // 
            this.Date.Name = "Date";
            this.Date.Text = "Date";
            this.Date.Width = 80;
            // 
            // StartTime
            // 
            this.StartTime.Name = "StartTime";
            this.StartTime.Text = "Start Time";
            this.StartTime.Width = 100;
            // 
            // EndTime
            // 
            this.EndTime.Name = "EndTime";
            this.EndTime.Text = "End Time";
            this.EndTime.Width = 100;
            // 
            // Duration
            // 
            this.Duration.Name = "Duration";
            this.Duration.Text = "Duration";
            this.Duration.Width = 100;
            // 
            // Description
            // 
            this.Description.Name = "Description";
            this.Description.Text = "Description";
            this.Description.Width = 120;
            // 
            // lblscreen
            // 
            this.lblscreen.AutoSize = true;
            this.lblscreen.Location = new System.Drawing.Point(12, 551);
            this.lblscreen.Name = "lblscreen";
            this.lblscreen.Size = new System.Drawing.Size(184, 20);
            this.lblscreen.TabIndex = 15;
            this.lblscreen.Text = "Last screenshot at 00:00:00";
            this.lblscreen.Visible = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(12, 208);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.PlaceholderText = "Task Description";
            this.txtDescription.Size = new System.Drawing.Size(589, 142);
            this.txtDescription.TabIndex = 4;
            // 
            // lblKeyboardStock
            // 
            this.lblKeyboardStock.AutoSize = true;
            this.lblKeyboardStock.Location = new System.Drawing.Point(461, 80);
            this.lblKeyboardStock.Name = "lblKeyboardStock";
            this.lblKeyboardStock.Size = new System.Drawing.Size(133, 20);
            this.lblKeyboardStock.TabIndex = 17;
            this.lblKeyboardStock.Text = "key Stock Count : 0";
            // 
            // lblMouseClick
            // 
            this.lblMouseClick.AutoSize = true;
            this.lblMouseClick.Location = new System.Drawing.Point(444, 111);
            this.lblMouseClick.Name = "lblMouseClick";
            this.lblMouseClick.Size = new System.Drawing.Size(150, 20);
            this.lblMouseClick.TabIndex = 18;
            this.lblMouseClick.Text = "Mouse Click Count : 0";
            // 
            // picBox
            // 
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox.Location = new System.Drawing.Point(13, 572);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(588, 232);
            this.picBox.TabIndex = 19;
            this.picBox.TabStop = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "sdsddssdsddsds";
            this.notifyIcon1.BalloonTipTitle = "sdsasd";
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(613, 841);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.lblMouseClick);
            this.Controls.Add(this.lblKeyboardStock);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblscreen);
            this.Controls.Add(this.lstHistory);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cmbProjects);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tracker";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProjects;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ListView lstHistory;
        private System.Windows.Forms.ColumnHeader Project;
        private System.Windows.Forms.ColumnHeader Date;
        private System.Windows.Forms.ColumnHeader StartTime;
        private System.Windows.Forms.ColumnHeader EndTime;
        private System.Windows.Forms.ColumnHeader Duration;
        private System.Windows.Forms.Label lblscreen;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label lblKeyboardStock;
        private System.Windows.Forms.Label lblMouseClick;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

