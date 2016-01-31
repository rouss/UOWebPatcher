namespace PatchBuilder.Modules
{
    partial class BuilderInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuilderInterface));
            this.PatchBuildNotes = new System.Windows.Forms.RichTextBox();
            this.version_box = new System.Windows.Forms.TextBox();
            this.version_lbl = new System.Windows.Forms.Label();
            this.bg_img_box = new System.Windows.Forms.TextBox();
            this.bg_img_lbl = new System.Windows.Forms.Label();
            this.master_uri_box = new System.Windows.Forms.TextBox();
            this.master_uri_lbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BuildPatch_Btn = new System.Windows.Forms.Button();
            this.Gen_Settings_Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.patch_file_box = new System.Windows.Forms.TextBox();
            this.patch_lbl = new System.Windows.Forms.Label();
            this.OpacityBar = new System.Windows.Forms.TrackBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Add_Manually = new System.Windows.Forms.Button();
            this.manual_ver_lbl = new System.Windows.Forms.Label();
            this.manual_ver_box = new System.Windows.Forms.TextBox();
            this.manual_url_lbl = new System.Windows.Forms.Label();
            this.manual_url_box = new System.Windows.Forms.TextBox();
            this.manual_name_lbl = new System.Windows.Forms.Label();
            this.manual_name_box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.update_log_loc = new System.Windows.Forms.TextBox();
            this.updateLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityBar)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // PatchBuildNotes
            // 
            this.PatchBuildNotes.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PatchBuildNotes.Location = new System.Drawing.Point(13, 25);
            this.PatchBuildNotes.Name = "PatchBuildNotes";
            this.PatchBuildNotes.ReadOnly = true;
            this.PatchBuildNotes.Size = new System.Drawing.Size(927, 204);
            this.PatchBuildNotes.TabIndex = 0;
            this.PatchBuildNotes.Text = "";
            this.PatchBuildNotes.ZoomFactor = 1.05F;
            // 
            // version_box
            // 
            this.version_box.Location = new System.Drawing.Point(137, 264);
            this.version_box.Name = "version_box";
            this.version_box.Size = new System.Drawing.Size(399, 20);
            this.version_box.TabIndex = 4;
            // 
            // version_lbl
            // 
            this.version_lbl.AutoSize = true;
            this.version_lbl.BackColor = System.Drawing.SystemColors.Control;
            this.version_lbl.Location = new System.Drawing.Point(13, 269);
            this.version_lbl.Name = "version_lbl";
            this.version_lbl.Size = new System.Drawing.Size(113, 13);
            this.version_lbl.TabIndex = 3;
            this.version_lbl.Text = "Version Text Location:";
            // 
            // bg_img_box
            // 
            this.bg_img_box.Location = new System.Drawing.Point(137, 290);
            this.bg_img_box.Name = "bg_img_box";
            this.bg_img_box.Size = new System.Drawing.Size(399, 20);
            this.bg_img_box.TabIndex = 6;
            // 
            // bg_img_lbl
            // 
            this.bg_img_lbl.AutoSize = true;
            this.bg_img_lbl.BackColor = System.Drawing.SystemColors.Control;
            this.bg_img_lbl.Location = new System.Drawing.Point(14, 294);
            this.bg_img_lbl.Name = "bg_img_lbl";
            this.bg_img_lbl.Size = new System.Drawing.Size(112, 13);
            this.bg_img_lbl.TabIndex = 5;
            this.bg_img_lbl.Text = "Background Location:";
            // 
            // master_uri_box
            // 
            this.master_uri_box.Location = new System.Drawing.Point(137, 342);
            this.master_uri_box.Name = "master_uri_box";
            this.master_uri_box.Size = new System.Drawing.Size(399, 20);
            this.master_uri_box.TabIndex = 8;
            // 
            // master_uri_lbl
            // 
            this.master_uri_lbl.AutoSize = true;
            this.master_uri_lbl.BackColor = System.Drawing.SystemColors.Control;
            this.master_uri_lbl.Location = new System.Drawing.Point(52, 346);
            this.master_uri_lbl.Name = "master_uri_lbl";
            this.master_uri_lbl.Size = new System.Drawing.Size(74, 13);
            this.master_uri_lbl.TabIndex = 7;
            this.master_uri_lbl.Text = "Master Folder:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.BuildPatch_Btn);
            this.panel1.Controls.Add(this.Gen_Settings_Btn);
            this.panel1.Location = new System.Drawing.Point(541, 238);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 128);
            this.panel1.TabIndex = 9;
            // 
            // BuildPatch_Btn
            // 
            this.BuildPatch_Btn.BackColor = System.Drawing.SystemColors.Control;
            this.BuildPatch_Btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BuildPatch_Btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuildPatch_Btn.Location = new System.Drawing.Point(4, 3);
            this.BuildPatch_Btn.Name = "BuildPatch_Btn";
            this.BuildPatch_Btn.Size = new System.Drawing.Size(389, 51);
            this.BuildPatch_Btn.TabIndex = 1;
            this.BuildPatch_Btn.Text = "Generate Patch";
            this.BuildPatch_Btn.UseVisualStyleBackColor = false;
            this.BuildPatch_Btn.Click += new System.EventHandler(this.BuildPatch_Btn_Click);
            // 
            // Gen_Settings_Btn
            // 
            this.Gen_Settings_Btn.BackColor = System.Drawing.SystemColors.Control;
            this.Gen_Settings_Btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gen_Settings_Btn.Location = new System.Drawing.Point(4, 68);
            this.Gen_Settings_Btn.Name = "Gen_Settings_Btn";
            this.Gen_Settings_Btn.Padding = new System.Windows.Forms.Padding(5);
            this.Gen_Settings_Btn.Size = new System.Drawing.Size(389, 51);
            this.Gen_Settings_Btn.TabIndex = 0;
            this.Gen_Settings_Btn.Text = "Generate Settings";
            this.Gen_Settings_Btn.UseVisualStyleBackColor = false;
            this.Gen_Settings_Btn.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Action Log:";
            // 
            // patch_file_box
            // 
            this.patch_file_box.Location = new System.Drawing.Point(137, 316);
            this.patch_file_box.Name = "patch_file_box";
            this.patch_file_box.Size = new System.Drawing.Size(399, 20);
            this.patch_file_box.TabIndex = 12;
            // 
            // patch_lbl
            // 
            this.patch_lbl.AutoSize = true;
            this.patch_lbl.BackColor = System.Drawing.SystemColors.Control;
            this.patch_lbl.Location = new System.Drawing.Point(25, 320);
            this.patch_lbl.Name = "patch_lbl";
            this.patch_lbl.Size = new System.Drawing.Size(101, 13);
            this.patch_lbl.TabIndex = 11;
            this.patch_lbl.Text = "Patch File Location:";
            // 
            // OpacityBar
            // 
            this.OpacityBar.Location = new System.Drawing.Point(16, 485);
            this.OpacityBar.Name = "OpacityBar";
            this.OpacityBar.Size = new System.Drawing.Size(925, 45);
            this.OpacityBar.TabIndex = 13;
            this.OpacityBar.Scroll += new System.EventHandler(this.opacity_bar_Scroll);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.Add_Manually);
            this.panel2.Controls.Add(this.manual_ver_lbl);
            this.panel2.Controls.Add(this.manual_ver_box);
            this.panel2.Controls.Add(this.manual_url_lbl);
            this.panel2.Controls.Add(this.manual_url_box);
            this.panel2.Controls.Add(this.manual_name_lbl);
            this.panel2.Controls.Add(this.manual_name_box);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(12, 373);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(929, 106);
            this.panel2.TabIndex = 14;
            // 
            // Add_Manually
            // 
            this.Add_Manually.BackColor = System.Drawing.Color.DarkGray;
            this.Add_Manually.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add_Manually.Location = new System.Drawing.Point(749, 53);
            this.Add_Manually.Name = "Add_Manually";
            this.Add_Manually.Size = new System.Drawing.Size(175, 23);
            this.Add_Manually.TabIndex = 7;
            this.Add_Manually.Text = "Add";
            this.Add_Manually.UseVisualStyleBackColor = false;
            this.Add_Manually.Click += new System.EventHandler(this.Add_Manually_Click);
            // 
            // manual_ver_lbl
            // 
            this.manual_ver_lbl.AutoSize = true;
            this.manual_ver_lbl.Location = new System.Drawing.Point(594, 39);
            this.manual_ver_lbl.Name = "manual_ver_lbl";
            this.manual_ver_lbl.Size = new System.Drawing.Size(100, 13);
            this.manual_ver_lbl.TabIndex = 6;
            this.manual_ver_lbl.Text = "Associated Version:";
            // 
            // manual_ver_box
            // 
            this.manual_ver_box.Location = new System.Drawing.Point(597, 55);
            this.manual_ver_box.Name = "manual_ver_box";
            this.manual_ver_box.Size = new System.Drawing.Size(146, 20);
            this.manual_ver_box.TabIndex = 5;
            // 
            // manual_url_lbl
            // 
            this.manual_url_lbl.AutoSize = true;
            this.manual_url_lbl.Location = new System.Drawing.Point(245, 36);
            this.manual_url_lbl.Name = "manual_url_lbl";
            this.manual_url_lbl.Size = new System.Drawing.Size(76, 13);
            this.manual_url_lbl.TabIndex = 4;
            this.manual_url_lbl.Text = "URL Location:";
            // 
            // manual_url_box
            // 
            this.manual_url_box.Location = new System.Drawing.Point(245, 55);
            this.manual_url_box.Name = "manual_url_box";
            this.manual_url_box.Size = new System.Drawing.Size(346, 20);
            this.manual_url_box.TabIndex = 3;
            this.manual_url_box.TextChanged += new System.EventHandler(this.manual_url_box_TextChanged);
            // 
            // manual_name_lbl
            // 
            this.manual_name_lbl.AutoSize = true;
            this.manual_name_lbl.Location = new System.Drawing.Point(16, 36);
            this.manual_name_lbl.Name = "manual_name_lbl";
            this.manual_name_lbl.Size = new System.Drawing.Size(57, 13);
            this.manual_name_lbl.TabIndex = 2;
            this.manual_name_lbl.Text = "File Name:";
            // 
            // manual_name_box
            // 
            this.manual_name_box.Location = new System.Drawing.Point(16, 55);
            this.manual_name_box.Name = "manual_name_box";
            this.manual_name_box.Size = new System.Drawing.Size(223, 20);
            this.manual_name_box.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Add Patch Manually";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(868, 9);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(68, 13);
            this.VersionLabel.TabIndex = 8;
            this.VersionLabel.Text = "VersionLabel";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.updateLabel);
            this.panel3.Location = new System.Drawing.Point(12, 235);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(119, 128);
            this.panel3.TabIndex = 15;
            // 
            // update_log_loc
            // 
            this.update_log_loc.Location = new System.Drawing.Point(137, 238);
            this.update_log_loc.Name = "update_log_loc";
            this.update_log_loc.Size = new System.Drawing.Size(399, 20);
            this.update_log_loc.TabIndex = 17;
            // 
            // updateLabel
            // 
            this.updateLabel.AutoSize = true;
            this.updateLabel.BackColor = System.Drawing.SystemColors.Control;
            this.updateLabel.Location = new System.Drawing.Point(4, 8);
            this.updateLabel.Name = "updateLabel";
            this.updateLabel.Size = new System.Drawing.Size(110, 13);
            this.updateLabel.TabIndex = 16;
            this.updateLabel.Text = "Update Log Location:";
            // 
            // BuilderInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(952, 516);
            this.Controls.Add(this.update_log_loc);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.OpacityBar);
            this.Controls.Add(this.patch_file_box);
            this.Controls.Add(this.patch_lbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.master_uri_box);
            this.Controls.Add(this.master_uri_lbl);
            this.Controls.Add(this.bg_img_box);
            this.Controls.Add(this.bg_img_lbl);
            this.Controls.Add(this.version_box);
            this.Controls.Add(this.version_lbl);
            this.Controls.Add(this.PatchBuildNotes);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BuilderInterface";
            this.Text = "UO Patch Builder";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OpacityBar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox PatchBuildNotes;
        private System.Windows.Forms.TextBox version_box;
        private System.Windows.Forms.Label version_lbl;
        private System.Windows.Forms.TextBox bg_img_box;
        private System.Windows.Forms.Label bg_img_lbl;
        private System.Windows.Forms.TextBox master_uri_box;
        private System.Windows.Forms.Label master_uri_lbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BuildPatch_Btn;
        private System.Windows.Forms.Button Gen_Settings_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox patch_file_box;
        private System.Windows.Forms.Label patch_lbl;
        private System.Windows.Forms.TrackBar OpacityBar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Add_Manually;
        private System.Windows.Forms.Label manual_ver_lbl;
        private System.Windows.Forms.TextBox manual_ver_box;
        private System.Windows.Forms.Label manual_url_lbl;
        private System.Windows.Forms.TextBox manual_url_box;
        private System.Windows.Forms.Label manual_name_lbl;
        private System.Windows.Forms.TextBox manual_name_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.TextBox update_log_loc;
        private System.Windows.Forms.Label updateLabel;
    }
}

