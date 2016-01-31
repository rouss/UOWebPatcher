using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchBuilder.Modules
{
    public partial class BuilderInterface : Form
    {
        BuildHandler handler = null;

        public BuilderInterface()
        {
            InitializeComponent();

            handler = new BuildHandler(this);

            OpacityBar.Maximum = 100;
            OpacityBar.Minimum = 20;

            OpacityBar.Value = 100;

            Task.Factory.StartNew(ReadSettings);
        }

        internal void ReadSettings()
        {
            Task.Delay(618); //All Hail Phi

            if (XmlHandler.CanReadSettings("settings.xml"))
            {
                Invoke(new MethodInvoker(delegate
                    {
                        patch_file_box.Text = PatchHelper.PatchURL;
                        master_uri_box.Text = PatchHelper.MasterURL;
                        version_box.Text = PatchHelper.VersionURL;
                        bg_img_box.Text = PatchHelper.BackgroundURL;
                        update_log_loc.Text = PatchHelper.UpdateLogURL;

                        manual_ver_box.Text = PatchHelper.VersionString();
                        VersionLabel.Text = PatchHelper.VersionString();
                    }));
            }

            else LogHandler.LogErrors("Error: Unable to read or find settings.xml");
        }

        internal void Parse_URL_Entries()
        {

            if (!(String.IsNullOrEmpty(patch_file_box.Text)))
                PatchHelper.PatchURL = patch_file_box.Text;

            else 
            {
                MessageBox.Show("You appear to have left something empty.");
                return;
            }

            if (!(String.IsNullOrEmpty(master_uri_box.Text)))
                PatchHelper.MasterURL = master_uri_box.Text;

            else 
            { 
                MessageBox.Show("You appear to have left something empty."); 
                return; 
            }

            if (!(String.IsNullOrEmpty(version_box.Text)))
                PatchHelper.VersionURL = version_box.Text;

            else 
            {
                MessageBox.Show("You appear to have left something empty.");
                return;
            }

            if (!(String.IsNullOrEmpty(bg_img_box.Text)))
                PatchHelper.BackgroundURL = bg_img_box.Text;

            else 
            {
                MessageBox.Show("You appear to have left something empty.");
                return;
            }

            if (!(String.IsNullOrEmpty(update_log_loc.Text)))
                PatchHelper.UpdateLogURL = update_log_loc.Text;

            else
            {
                MessageBox.Show("You appear to have left something empty.");
                return;
            }
        }

        private void BuildPatch_Btn_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(RelayPatchGeneration);
        }

        void RelayPatchGeneration() { handler.RelayPatchGeneration(); }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Parse_URL_Entries(); handler.RelaySettingsGeneration();
        }

        internal void UpdatePatchNotes(string notes)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(new MethodInvoker
                        (()=>
                        {
                            PatchBuildNotes.Text += notes + "\n";
                            PatchBuildNotes.SelectionStart = PatchBuildNotes.Text.Length;
                            PatchBuildNotes.ScrollToCaret();
                        }));

                else 
                { 
                    PatchBuildNotes.Text += notes + "\n";
                    PatchBuildNotes.SelectionStart = PatchBuildNotes.Text.Length;
                    PatchBuildNotes.ScrollToCaret();
                }
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString());
                UpdatePatchNotes(e.ToString());
            }
        }

        internal void UpdateProgressBar()
        {
            /// Really.., really..?
            /// Yeah, lolz
        }

        private void opacity_bar_Scroll(object sender, EventArgs e)
        {
            Opacity = (double)((double)OpacityBar.Value / 100);
        }

        private void Add_Manually_Click(object sender, EventArgs args)
        {
            try
            {
                PatchFile file = Parse_Manual_Entry();

                if (file != null)
                {
                    XmlHandler.AddPatchManually(PatchHelper.PatchURL, file);
                    UpdatePatchNotes(string.Format("New Patch Added: {0}", file.filePath));
                }

                ClearManualPatch();
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString());
                UpdatePatchNotes(e.ToString());
            }
        }

        internal void ClearManualPatch()
        {
            manual_name_box.Text = string.Empty;
            manual_url_box.Text = string.Empty;
        }

        internal PatchFile Parse_Manual_Entry()
        {
            try
            {
                string version;
                string name;
                string url;

                if (!(String.IsNullOrEmpty(manual_ver_box.Text)))
                    version = manual_ver_box.Text;

                else
                {
                    MessageBox.Show("You appear to have left something empty.");
                    return null;
                }

                if (!(String.IsNullOrEmpty(manual_url_box.Text)))
                    url = manual_url_box.Text;

                else
                {
                    MessageBox.Show("You appear to have left something empty.");
                    return null;
                }

                if (!(String.IsNullOrEmpty(manual_name_box.Text)))
                    name = manual_name_box.Text;

                else
                {
                    MessageBox.Show("You appear to have left something empty.");
                    return null;
                }

                int bytes = WebDirectory.ParseFileSizeViaHTTP(url);

                return new PatchFile(url, name, bytes, version);
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString());
                UpdatePatchNotes(e.ToString());

                return null;
            }
        }

        private void manual_url_box_TextChanged(object sender, EventArgs e)
        {
            AttemptNameParseFromURL(manual_url_box.Text);
        }

        void AttemptNameParseFromURL(string url)
        {
            try
            {
                string[] temp_ = url.Split('/');
                manual_name_box.Text = temp_[temp_.Length-1];
            }

            catch {}
        }
    }
}
