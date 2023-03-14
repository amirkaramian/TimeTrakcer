using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTracker.Model;
using TimeTracker.Modules;
using TimeTracker.Properties;
using TimeTracker.Service;
using TimeTracker.Service.Interface;


namespace TimeTracker
{
    public partial class MainForm : Form
    {
        public IService ServiceAccess { get; set; }
        public UserInfo userInfo { get; set; }
        public List<ProjectModel> Projects { get; set; }
        private System.Windows.Forms.Timer tmrDigitalClock;
        private System.Windows.Forms.Timer tmrScreenCapture;
        private DateTime starttime, stoptime, today;
        private TimeSpan pauseTime;
        private int mouseClick, keyboardClick;
        private StringBuilder KeyBoardStock;
        private UserActivityHook actHook;
        private ProjectHistory currentProject;
        private bool isStop, isRunning;
        private List<ProjectModel> ProjectModels;
        private List<string> applicationList;
        public MainForm()
        {
            InitializeComponent();
            InitAsync();
        }
        protected override void OnLoad(EventArgs e)
        {
            try
            {


                ProjectModels = ServiceAccess.GetProjectsAsync(userInfo).GetAwaiter().GetResult();
                cmbProjects.Items.Add("Select Project");
                foreach (var item in ProjectModels)
                    cmbProjects.Items.Add(item.Name);
                cmbProjects.SelectedIndex = 0;
                tmrDigitalClock = new System.Windows.Forms.Timer
                {
                    Enabled = false,
                    Interval = 1000
                };
                tmrDigitalClock.Tick += new System.EventHandler(tmrDigitalClock_Tick);

                tmrScreenCapture = new System.Windows.Forms.Timer
                {
                    Enabled = false,
                    Interval = userInfo.ScreenshotTimeinterval * 60000
                };

                tmrScreenCapture.Tick += new System.EventHandler(tmrScreenCapture_Tick);
                GetApps().GetAwaiter();
                this.toolStripStatusLabel2.Text = $"wellcome {userInfo.UserName}";
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void InitAsync()
        {
            try
            {
                this.statusStrip1.BackColor = Color.FromArgb(0, 212, 255);
                this.toolStripStatusLabel1.BackColor = Color.FromArgb(0, 212, 255);
                this.toolStripStatusLabel2.BackColor = Color.FromArgb(0, 212, 255);
                this.btnStart.FlatAppearance.BorderColor = Color.FromArgb(0, 212, 255);
                this.btnStart.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 212, 255);
                this.btnStart.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 212, 255);
                this.btnReset.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 229, 229);
                this.btnReset.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 229, 229);
                this.btnStop.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 229, 229);
                this.btnStop.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 229, 229);
                this.FormClosing += MainForm_FormClosing;
                actHook = new UserActivityHook();
                actHook.OnMouseActivity += new MouseEventHandler(MouseMoved);
                actHook.KeyPress += new KeyPressEventHandler(MyKeyPress);
                actHook.Stop();
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;

                notifyIcon1.BalloonTipText = "I am a NotifyIcon Balloon";

                notifyIcon1.BalloonTipTitle = "Welcome Message";



                notifyIcon1.ShowBalloonTip(1000);
                await Task.Run(() => Config());
            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void MyKeyPress(object sender, KeyPressEventArgs e)
        {
            lblKeyboardStock.Text = $"Key Stock count : {++keyboardClick}";// + e.KeyChar;
            KeyBoardStock.Append(e.KeyChar);
        }
        public void MouseMoved(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 0)
                lblMouseClick.Text = $"Mouse Click Count : {++mouseClick}"; //e.Button.ToString();
        }
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await ServiceAccess.LogoutAsync(userInfo);
            Application.Exit();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                isStop = false;
                if (cmbProjects.SelectedIndex == 0)
                {
                    MessageForm.Show("please select a project", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtDescription.Text))
                {
                    MessageForm.Show("please write task description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                KeyBoardStock = new StringBuilder();
                currentProject = new ProjectHistory
                {
                    ProjectActions = new List<ProjectAction>()
                };
                if (currentProject.Id != cmbProjects.SelectedIndex)
                {
                    currentProject.Id = cmbProjects.SelectedIndex;
                    currentProject.projectId = ProjectModels.FirstOrDefault(x => x.Name == cmbProjects.SelectedItem.ToString()).Id;
                    currentProject.Date = DateTime.Now;
                    currentProject.Description = txtDescription.Text;
                    currentProject.workerId = userInfo.WorkerId;
                    currentProject.UserId = userInfo.UserId;
                    currentProject.StartTime = DateTime.Now;
                }
                tmrDigitalClock.Enabled = tmrScreenCapture.Enabled = true;
                StartCapture();
                ControlStatus(true);
                actHook.Start();
                starttime = DateTime.Now;
                NotifyIcon trayIcon = new NotifyIcon
                {
                    Icon = Resources.Info,
                    Text = "Tracker is runing",
                    Visible = true
                };
                trayIcon.ShowBalloonTip(2000, "Information", "Your Screen page, Keyboard, the mouse is capturing now", ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                actHook.Stop();
                tmrDigitalClock.Enabled = tmrScreenCapture.Enabled = false;
                stoptime = DateTime.Now;
                starttime = System.DateTime.MinValue;
                today = DateTime.Now;
                pauseTime = new TimeSpan(0);


                currentProject.EndTime = DateTime.Now;
                currentProject.Duration = TimeSpan.Parse(lblTime.Text);
                currentProject.ClickCount += mouseClick;
                currentProject.KeyboardCount += keyboardClick;
                currentProject.ProjectActions.Add(new ProjectAction()
                {
                    ClickCount = mouseClick,
                    KeyboardCount = keyboardClick,
                    keyboardStock = KeyBoardStock.ToString(),
                    Time = DateTime.Now,
                    StartTime = currentProject.StartTime,
                    EndTime = DateTime.Now,
                    UserId = userInfo.UserId
                });
                AddItemTolist();
                this.Text = "waiting.....";
                var task = Task.Run(() =>
                  {
                      int projectWorkerActivityId = ServiceAccess.SendProjectHistory(currentProject, userInfo).GetAwaiter().GetResult();
                      SendPicture(projectWorkerActivityId);
                      currentProject = new ProjectHistory
                      {
                          ProjectActions = new List<ProjectAction>()
                      };
                      mouseClick = keyboardClick = 0;
                      KeyBoardStock = new StringBuilder();
                  });
                Task.WaitAny(task);
                this.Text = "Tracker";
                ControlStatus(false);
                txtDescription.Clear();
            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SendPicture(int projectWorkerActivityId)
        {
            var files = Directory.GetFiles(@"img\", "*.png");
            var resp = ServiceAccess.UploadPictures(userInfo, files.ToList(), projectWorkerActivityId);
            if (resp)
            {
                foreach (var item in files)
                {
                    if (File.Exists(item))
                        File.Delete(item);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            actHook.Stop();
            isStop = true;
            stoptime = DateTime.Now;
            tmrDigitalClock.Enabled = tmrScreenCapture.Enabled = false;
            pauseTime = TimeSpan.Parse(lblTime.Text);
            btnStart.Enabled = true;
            btnStop.Enabled = true;
            btnReset.Enabled = false;
        }
        private async void tmrDigitalClock_Tick(object sender, System.EventArgs e)
        {
            if (!isStop && (await CheckIfAnyBrowserIsRunning()))
            {
                stoptime = DateTime.Now;
                var duration = stoptime.Subtract(starttime);
                lblTime.Text = ReadableDuration(duration.Add(pauseTime));
            }
        }
        private void tmrScreenCapture_Tick(object sender, EventArgs e)
        {
            if (!isStop && isRunning)
            {
                StartCapture();
                currentProject.ClickCount += mouseClick;
                currentProject.KeyboardCount += keyboardClick;
                currentProject.ProjectActions.Add(new ProjectAction()
                {
                    ClickCount = mouseClick,
                    KeyboardCount = keyboardClick,
                    keyboardStock = KeyBoardStock.ToString(),
                    Time = DateTime.Now,
                    StartTime = currentProject.StartTime,
                    EndTime = DateTime.Now,
                });
                KeyBoardStock = new StringBuilder();
                mouseClick = keyboardClick = 0;
            }

        }
        private void cmbProjects_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            using var format = new StringFormat
            {
                LineAlignment = StringAlignment.Center
            };
            using var brush = new SolidBrush(Color.FromArgb(0, 212, 255));

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.FillRectangle(brush, e.Bounds);
            else if ((e.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit)
            {
                using var customPen = new Pen(brush, 2);
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                e.Graphics.DrawLine(customPen, new Point(e.Bounds.Left, e.Bounds.Bottom - 1), new Point(e.Bounds.Right, e.Bounds.Bottom - 1));
            }
            else
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            e.Graphics.DrawString(this.cmbProjects.Items[e.Index].ToString(), this.cmbProjects.Font, Brushes.Black,
               e.Bounds, format);
            e.DrawFocusRectangle();
        }
        private void ControlStatus(bool val)
        {
            this.btnReset.Enabled = val;
            this.btnStop.Enabled = val;
            if (val)
            {
                this.btnStop.FlatAppearance.BorderColor = Color.FromArgb(255, 229, 229);
                this.btnReset.FlatAppearance.BorderColor = Color.FromArgb(255, 229, 229);
            }
            else
            {
                this.btnStop.FlatAppearance.BorderColor = Color.Gray;
                this.btnReset.FlatAppearance.BorderColor = Color.Gray;
            }
            this.btnStart.Enabled = !val;
            cmbProjects.Enabled = !val;
        }
        private void StartCapture()
        {
            lblscreen.Visible = true;
            lblscreen.Text = $"Last screenshot at {DateTime.Now.ToString("HH:mm:ss")}";
            Point curPos = new Point(Cursor.Position.X, Cursor.Position.Y);
            Size curSize = new Size
            {
                Height = Cursor.Current?.Size.Height ?? 200,
                Width = Cursor.Current?.Size.Width ?? 200
            };
            Rectangle bounds = Screen.GetBounds(Screen.GetBounds(Point.Empty));
            picBox.Image = ScreenShot.CaptureImage(true, curSize, curPos, Point.Empty, Point.Empty, bounds, string.Empty, string.Empty);
            if (!Directory.Exists("img"))
                Directory.CreateDirectory("img");
            picBox.Image.Save($"img/{DateTime.Now.Millisecond}.png");

        }
        private void Config()
        {
            while (true)
            {
                try
                {
                    if (statusStrip1.Items.Count > 0)
                    {
                        statusStrip1.Items[0].Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }
        private string ReadableDuration(System.TimeSpan duration)
        {
            return duration.Hours.ToString().PadLeft(2, '0') + ":" +
                duration.Minutes.ToString().PadLeft(2, '0') + ":" +
                duration.Seconds.ToString().PadLeft(2, '0');
        }
        private void AddItemTolist()
        {
            var listViewItem = new ListViewItem(new string[] {$"{ProjectModels.FirstOrDefault(x => x.Id == currentProject.projectId).Name}" ,
                $"{currentProject.Date:yy/MM/dd}",
                $"{currentProject.StartTime.ToShortTimeString()}",
                $"{currentProject.EndTime.ToShortTimeString()}",
                $"{currentProject.Duration}",
                $"{currentProject.Description}" });
            lstHistory.Items.Add(listViewItem);
        }
        internal async Task<bool> CheckIfAnyBrowserIsRunning()
        {
            var runningPrg = false;
            foreach (var app in applicationList)
            {
                Process[] Processes = Process.GetProcessesByName(app);
                if (Processes.Length <= 0)
                    continue;
                if (!Processes.Any(d => d.MainWindowHandle != IntPtr.Zero))
                    continue;
                runningPrg = true;
                break;
            }
            isRunning = runningPrg;
            return runningPrg;
        }
        internal async Task<List<string>> GetApps()
        {
            var list = await this.ServiceAccess.GetAppsAsync(this.userInfo);
            applicationList = list.Select(x => x.ApplicationName).ToList();
            return applicationList;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {

                base.OnClosing(e);
                if (currentProject != null && currentProject.Id > 0)
                {
                    currentProject.EndTime = DateTime.Now;
                    currentProject.Duration = TimeSpan.Parse(lblTime.Text);
                    currentProject.ClickCount = mouseClick;
                    currentProject.KeyboardCount = keyboardClick;
                    currentProject.ProjectActions.Add(new ProjectAction()
                    {
                        ClickCount = mouseClick,
                        KeyboardCount = keyboardClick,
                        keyboardStock = KeyBoardStock.ToString(),
                        Time = DateTime.Now,
                        StartTime = currentProject.StartTime,
                        EndTime = DateTime.Now,
                        UserId = userInfo.UserId
                    });
                    int projectWorkerActivityId = ServiceAccess.SendProjectHistory(currentProject, userInfo).GetAwaiter().GetResult();
                    SendPicture(projectWorkerActivityId);
                }

                Application.Exit();

            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

}
