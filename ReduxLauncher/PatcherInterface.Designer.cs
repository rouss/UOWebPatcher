namespace ReduxLauncher.Modules
{
    partial class PatcherInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatcherInterface));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Main_action_btn = new System.Windows.Forms.Button();
            this.file_name_lbl = new System.Windows.Forms.Label();
            this.percentage_lbl = new System.Windows.Forms.Label();
            this.UpdateText = new System.Windows.Forms.RichTextBox();
            this.Background = new System.Windows.Forms.PictureBox();
            this.RazorImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Background)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RazorImg)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.progressBar.Location = new System.Drawing.Point(8, 528);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(555, 14);
            this.progressBar.TabIndex = 0;
            this.progressBar.UseWaitCursor = true;
            // 
            // Main_action_btn
            // 
            this.Main_action_btn.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.Main_action_btn.BackColor = System.Drawing.Color.Gray;
            this.Main_action_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Main_action_btn.ForeColor = System.Drawing.Color.Black;
            this.Main_action_btn.Location = new System.Drawing.Point(8, 633);
            this.Main_action_btn.Name = "Main_action_btn";
            this.Main_action_btn.Size = new System.Drawing.Size(555, 41);
            this.Main_action_btn.TabIndex = 3;
            this.Main_action_btn.Text = "Download";
            this.Main_action_btn.UseVisualStyleBackColor = false;
            this.Main_action_btn.Click += new System.EventHandler(this.Main_action_btn_Click);
            // 
            // file_name_lbl
            // 
            this.file_name_lbl.AutoSize = true;
            this.file_name_lbl.BackColor = System.Drawing.Color.Black;
            this.file_name_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_name_lbl.ForeColor = System.Drawing.Color.White;
            this.file_name_lbl.Location = new System.Drawing.Point(5, 508);
            this.file_name_lbl.Name = "file_name_lbl";
            this.file_name_lbl.Size = new System.Drawing.Size(76, 17);
            this.file_name_lbl.TabIndex = 5;
            this.file_name_lbl.Text = "fileName:";
            this.file_name_lbl.Visible = false;
            this.file_name_lbl.Click += new System.EventHandler(this.file_name_lbl_Click);
            // 
            // percentage_lbl
            // 
            this.percentage_lbl.AutoSize = true;
            this.percentage_lbl.BackColor = System.Drawing.Color.Black;
            this.percentage_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percentage_lbl.ForeColor = System.Drawing.Color.White;
            this.percentage_lbl.Location = new System.Drawing.Point(522, 506);
            this.percentage_lbl.Name = "percentage_lbl";
            this.percentage_lbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.percentage_lbl.Size = new System.Drawing.Size(44, 17);
            this.percentage_lbl.TabIndex = 6;
            this.percentage_lbl.Text = "100%";
            this.percentage_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.percentage_lbl.Visible = false;
            // 
            // UpdateText
            // 
            this.UpdateText.BackColor = System.Drawing.Color.Black;
            this.UpdateText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateText.ForeColor = System.Drawing.Color.White;
            this.UpdateText.Location = new System.Drawing.Point(8, 558);
            this.UpdateText.Name = "UpdateText";
            this.UpdateText.Size = new System.Drawing.Size(555, 69);
            this.UpdateText.TabIndex = 8;
            this.UpdateText.Text = "";
            this.UpdateText.TextChanged += new System.EventHandler(this.UpdateText_TextChanged);
            // 
            // Background
            // 
            this.Background.Location = new System.Drawing.Point(8, 6);
            this.Background.Name = "Background";
            this.Background.Size = new System.Drawing.Size(555, 546);
            this.Background.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Background.TabIndex = 4;
            this.Background.TabStop = false;
            // 
            // RazorImg
            // 
            this.RazorImg.BackColor = System.Drawing.Color.Transparent;
            this.RazorImg.BackgroundImage = global::ReduxLauncher.Properties.Resources.razor1_300x213;
            this.RazorImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RazorImg.Location = new System.Drawing.Point(527, 6);
            this.RazorImg.Name = "RazorImg";
            this.RazorImg.Size = new System.Drawing.Size(36, 36);
            this.RazorImg.TabIndex = 9;
            this.RazorImg.TabStop = false;
            this.RazorImg.Click += new System.EventHandler(this.RazorImg_Click);
            // 
            // PatcherInterface
            // 
            this.AcceptButton = this.Main_action_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(571, 683);
            this.Controls.Add(this.RazorImg);
            this.Controls.Add(this.UpdateText);
            this.Controls.Add(this.percentage_lbl);
            this.Controls.Add(this.file_name_lbl);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.Main_action_btn);
            this.Controls.Add(this.Background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(555, 547);
            this.Name = "PatcherInterface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Redux Patcher";
            this.TransparencyKey = System.Drawing.Color.PaleGreen;
            ((System.ComponentModel.ISupportInitialize)(this.Background)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RazorImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button Main_action_btn;
        private System.Windows.Forms.PictureBox Background;
        private System.Windows.Forms.Label file_name_lbl;
        private System.Windows.Forms.Label percentage_lbl;
        private System.Windows.Forms.RichTextBox UpdateText;
        private System.Windows.Forms.PictureBox RazorImg;
    }
}

