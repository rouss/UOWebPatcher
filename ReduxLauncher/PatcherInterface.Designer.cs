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
            this.label1 = new System.Windows.Forms.Label();
            this.UpdateText = new System.Windows.Forms.RichTextBox();
            this.Background = new System.Windows.Forms.PictureBox();
            this.RazorImg = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.redux_chat_img = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Background)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RazorImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redux_chat_img)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.progressBar.Location = new System.Drawing.Point(15, 528);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(538, 14);
            this.progressBar.TabIndex = 0;
            this.progressBar.UseWaitCursor = true;
            // 
            // Main_action_btn
            // 
            this.Main_action_btn.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.Main_action_btn.BackColor = System.Drawing.Color.Gray;
            this.Main_action_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Main_action_btn.Location = new System.Drawing.Point(577, 504);
            this.Main_action_btn.Name = "Main_action_btn";
            this.Main_action_btn.Size = new System.Drawing.Size(309, 41);
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
            this.file_name_lbl.Location = new System.Drawing.Point(13, 504);
            this.file_name_lbl.Name = "file_name_lbl";
            this.file_name_lbl.Size = new System.Drawing.Size(76, 17);
            this.file_name_lbl.TabIndex = 5;
            this.file_name_lbl.Text = "fileName:";
            this.file_name_lbl.Visible = false;
            // 
            // percentage_lbl
            // 
            this.percentage_lbl.AutoSize = true;
            this.percentage_lbl.BackColor = System.Drawing.Color.Black;
            this.percentage_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percentage_lbl.ForeColor = System.Drawing.Color.White;
            this.percentage_lbl.Location = new System.Drawing.Point(514, 504);
            this.percentage_lbl.Name = "percentage_lbl";
            this.percentage_lbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.percentage_lbl.Size = new System.Drawing.Size(44, 17);
            this.percentage_lbl.TabIndex = 6;
            this.percentage_lbl.Text = "100%";
            this.percentage_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.percentage_lbl.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(574, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Updates";
            // 
            // UpdateText
            // 
            this.UpdateText.BackColor = System.Drawing.Color.Black;
            this.UpdateText.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateText.ForeColor = System.Drawing.Color.White;
            this.UpdateText.Location = new System.Drawing.Point(578, 46);
            this.UpdateText.Name = "UpdateText";
            this.UpdateText.RightMargin = 192;
            this.UpdateText.Size = new System.Drawing.Size(308, 443);
            this.UpdateText.TabIndex = 8;
            this.UpdateText.Text = "";
            this.UpdateText.ZoomFactor = 1.75F;
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
            this.RazorImg.BackgroundImage = global::ReduxLauncher.Properties.Resources.razor1_300x213;
            this.RazorImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RazorImg.Location = new System.Drawing.Point(822, 5);
            this.RazorImg.Name = "RazorImg";
            this.RazorImg.Size = new System.Drawing.Size(36, 36);
            this.RazorImg.TabIndex = 9;
            this.RazorImg.TabStop = false;
            this.RazorImg.Click += new System.EventHandler(this.RazorImg_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::ReduxLauncher.Properties.Resources.red_btn;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(861, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(43, 38);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.Exit_Btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ReduxLauncher.Properties.Resources.blue_hlp_btn;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(743, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 29);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.Help_Click);
            // 
            // redux_chat_img
            // 
            this.redux_chat_img.BackgroundImage = global::ReduxLauncher.Properties.Resources.uologorender;
            this.redux_chat_img.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.redux_chat_img.Location = new System.Drawing.Point(774, -3);
            this.redux_chat_img.Name = "redux_chat_img";
            this.redux_chat_img.Size = new System.Drawing.Size(50, 50);
            this.redux_chat_img.TabIndex = 13;
            this.redux_chat_img.TabStop = false;
            this.redux_chat_img.Click += new System.EventHandler(this.Redux_Chat_Click);
            // 
            // PatcherInterface
            // 
            this.AcceptButton = this.Main_action_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(903, 557);
            this.Controls.Add(this.RazorImg);
            this.Controls.Add(this.UpdateText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.percentage_lbl);
            this.Controls.Add(this.file_name_lbl);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.Main_action_btn);
            this.Controls.Add(this.Background);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.redux_chat_img);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redux_chat_img)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button Main_action_btn;
        private System.Windows.Forms.PictureBox Background;
        private System.Windows.Forms.Label file_name_lbl;
        private System.Windows.Forms.Label percentage_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox UpdateText;
        private System.Windows.Forms.PictureBox RazorImg;
        private System.Windows.Forms.PictureBox redux_chat_img;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

