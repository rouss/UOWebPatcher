using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReduxLauncher.Modules
{
    public partial class PatcherInterface : Form
    {
        PatchHandler handler;

        public PatcherInterface()
        {
            InitializeComponent();

            Main_action_btn.Enabled = false;
            Main_action_btn.Text = "Please Wait..";

            handler = new PatchHandler(this);

            progressBar.Style = ProgressBarStyle.Blocks;
            progressBar.Maximum = 100;

            try
            {
                Background.Load(PatchData.BackgroundURL);
                UpdateText.Text = handler.UpdateNotes();
            }
            catch { } /// Incase background can't load from url.
            UpdateText.SelectionStart = UpdateText.Text.Length;
            UpdateText.ScrollToCaret();
        }

        internal void UpdatePercentage(int i)
        {
            UpdateProgressBar(i);

            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    if (!percentage_lbl.Visible)
                        percentage_lbl.Visible = true;

                    percentage_lbl.Text = i.ToString() + "%";
                }));
            }
            else
            {
                if (!percentage_lbl.Visible)
                    percentage_lbl.Visible = true;

                percentage_lbl.Text = i.ToString() + "%";
            }
        }

        void ToggleRazor()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    if (UseRazor)
                    {
                        RazorImg.BackgroundImage = Properties.Resources.razor1_300x213;
                        UseRazor = false;
                    }

                    else
                    {
                        RazorImg.BackgroundImage = Properties.Resources.razor_active_img;
                        UseRazor = true;
                    }
                }));
            }

            else
            {
                if (UseRazor)
                {
                    RazorImg.BackgroundImage = Properties.Resources.razor1_300x213;
                    UseRazor = false;
                }

                else
                {
                    RazorImg.BackgroundImage = Properties.Resources.razor_active_img;
                    UseRazor = true;
                }
            }
        }

        internal void UpdateFileName(string name, double total, double bytesIn)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    if (!file_name_lbl.Visible)
                        file_name_lbl.Visible = true;

                    file_name_lbl.Text = name;
                }));

            }
            else
            {
                if (!file_name_lbl.Visible)
                    file_name_lbl.Visible = true;

                file_name_lbl.Text = name;
            }
        }

        internal void ReadyLaunch()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    Main_action_btn.Enabled = true;
                    Main_action_btn.Text = "Launch";
                    ProgressBar().Value = ProgressBar().Maximum;

                    file_name_lbl.Visible = false;
                    percentage_lbl.Visible = false;
                }));
            }

            else
            {
                Main_action_btn.Enabled = true;
                Main_action_btn.Text = "Launch";
                ProgressBar().Value = ProgressBar().Maximum;

                file_name_lbl.Visible = false;
                percentage_lbl.Visible = false;
            }
        }

        internal void ReadyDownload()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    Main_action_btn.Enabled = true;
                    Main_action_btn.Text = "Patch Files";
                    ProgressBar().Value = ProgressBar().Minimum;
                }));
            }

            else
            {
                Main_action_btn.Enabled = true;
                Main_action_btn.Text = "Patch Files";
                ProgressBar().Value = ProgressBar().Minimum;
            }
        }

        internal void ReadyInstall()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    Main_action_btn.Enabled = true;
                    Main_action_btn.Text = "Install Redux";
                    ProgressBar().Value = ProgressBar().Minimum;
                }));
            }

            else
            {
                Main_action_btn.Enabled = true;
                Main_action_btn.Text = "Install Redux";
                ProgressBar().Value = ProgressBar().Minimum;
            }
        }

        internal ProgressBar ProgressBar()
        {
            return progressBar;
        }

        internal void UpdatePatchNotes(string notes)
        {
            LogHandler.LogActions(notes);
        }

        internal void CompleteProgressBar()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    ProgressBar().Value = ProgressBar().Maximum;
                }));
            }

            else ProgressBar().Value = ProgressBar().Maximum;
        }

        internal void UpdateProgressBar(int val)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    progressBar.Value = val;
                }));
            }

            else progressBar.Value = val;
        }

        internal void UpdateProgressBar()
        {
            if (new Random().Next(16) == 0)
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        if (ProgressBar().Value < ProgressBar().Maximum)
                            ProgressBar().PerformStep();

                        else
                            ProgressBar().Value = ProgressBar().Minimum;
                    }));
                }

                else
                {
                    if (ProgressBar().Value < ProgressBar().Maximum)
                        ProgressBar().PerformStep();

                    else
                        ProgressBar().Value = ProgressBar().Minimum;
                }
            }
        }

        private void Main_action_btn_Click(object sender, EventArgs e)
        {
            if (handler.isReady == false)
            {
                Main_action_btn.Enabled = false;

                Main_action_btn.Text = "Waiting..";

                if (handler.InitialDownload())
                    MessageBox.Show("The initial installation may take a while.");

                Task.Factory.StartNew(InitializeDownload);

                Main_action_btn.Text = "Downloading..";
            }

            else if (handler.isReady)
            {
                handler.LaunchClient();
            }
        }

        private async void InitializeDownload()
        {
            await handler.InitializeDownload();
        }

        bool useRzr = false;
        internal bool UseRazor
        {
            get { return useRzr; }
            set { useRzr = value; }
        }

        private void Redux_Chat_Click(object sender, EventArgs e)
        {

        }

        private void RazorImg_Click(object sender, EventArgs e)
        {
            ToggleRazor();
        }

        private void Help_Click(object sender, EventArgs e)
        {

        }

        private void Exit_Btn_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void UpdateText_TextChanged(object sender, EventArgs e)
        {

        }

        private void file_name_lbl_Click(object sender, EventArgs e)
        {

        }
    }
}
