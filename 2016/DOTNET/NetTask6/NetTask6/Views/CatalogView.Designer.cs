﻿namespace NetTask6.Views
{
    partial class CatalogView
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refillDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editFilmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFilmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findFilmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMoviePanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.editFormCountryText = new NetTask6.Views.PlainTextInput();
            this.editFormYear = new NetTask6.Views.YearInput();
            this.editFormActorsListBox = new System.Windows.Forms.ListBox();
            this.editFormDeleteActor = new System.Windows.Forms.Button();
            this.editFormAddActorBtn = new System.Windows.Forms.Button();
            this.editFormAddActor = new NetTask6.Views.Autocomplete();
            this.editFormDirector = new NetTask6.Views.Autocomplete();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uploadMovieBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.editMovieBackBtn = new System.Windows.Forms.Button();
            this.editFormNameText = new NetTask6.Views.PlainTextInput();
            this.editMovieSaveBtn = new System.Windows.Forms.Button();
            this.editMovieFormErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gridTitleLabel = new System.Windows.Forms.Label();
            this.editMovieFormHelpProvider = new System.Windows.Forms.HelpProvider();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.editMoviePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uploadMovieBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editMovieFormErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 56);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(232, 426);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1019, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refillDatabaseToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // refillDatabaseToolStripMenuItem
            // 
            this.refillDatabaseToolStripMenuItem.Name = "refillDatabaseToolStripMenuItem";
            this.refillDatabaseToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.refillDatabaseToolStripMenuItem.Text = "Refill Database";
            this.refillDatabaseToolStripMenuItem.Click += new System.EventHandler(this.OnRefillDatabaseToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExitToolStripMenuItemClick);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editFilmToolStripMenuItem,
            this.deleteFilmToolStripMenuItem,
            this.findFilmToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // editFilmToolStripMenuItem
            // 
            this.editFilmToolStripMenuItem.Name = "editFilmToolStripMenuItem";
            this.editFilmToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.editFilmToolStripMenuItem.Text = "&Edit movie";
            this.editFilmToolStripMenuItem.Click += new System.EventHandler(this.OnEditFilmToolStripMenuItemClick);
            // 
            // deleteFilmToolStripMenuItem
            // 
            this.deleteFilmToolStripMenuItem.Name = "deleteFilmToolStripMenuItem";
            this.deleteFilmToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.deleteFilmToolStripMenuItem.Text = "Delete movie";
            this.deleteFilmToolStripMenuItem.Click += new System.EventHandler(this.OnDeleteFilmToolStripMenuItemClick);
            // 
            // findFilmToolStripMenuItem
            // 
            this.findFilmToolStripMenuItem.Name = "findFilmToolStripMenuItem";
            this.findFilmToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.findFilmToolStripMenuItem.Text = "&Find movie...";
            this.findFilmToolStripMenuItem.Click += new System.EventHandler(this.OnFindFilmToolStripMenuItemClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutToolStripMenuItemClick);
            // 
            // editMoviePanel
            // 
            this.editMoviePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editMoviePanel.Controls.Add(this.label5);
            this.editMoviePanel.Controls.Add(this.editFormCountryText);
            this.editMoviePanel.Controls.Add(this.editFormYear);
            this.editMoviePanel.Controls.Add(this.editFormActorsListBox);
            this.editMoviePanel.Controls.Add(this.editFormDeleteActor);
            this.editMoviePanel.Controls.Add(this.editFormAddActorBtn);
            this.editMoviePanel.Controls.Add(this.editFormAddActor);
            this.editMoviePanel.Controls.Add(this.editFormDirector);
            this.editMoviePanel.Controls.Add(this.label4);
            this.editMoviePanel.Controls.Add(this.label3);
            this.editMoviePanel.Controls.Add(this.label2);
            this.editMoviePanel.Controls.Add(this.uploadMovieBox);
            this.editMoviePanel.Controls.Add(this.label1);
            this.editMoviePanel.Controls.Add(this.editMovieBackBtn);
            this.editMoviePanel.Controls.Add(this.editFormNameText);
            this.editMoviePanel.Controls.Add(this.editMovieSaveBtn);
            this.editMoviePanel.Location = new System.Drawing.Point(352, 32);
            this.editMoviePanel.Name = "editMoviePanel";
            this.editMoviePanel.Size = new System.Drawing.Size(619, 463);
            this.editMoviePanel.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(307, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Страна";
            // 
            // editFormCountryText
            // 
            this.editFormCountryText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editFormCountryText.Location = new System.Drawing.Point(311, 151);
            this.editFormCountryText.Name = "editFormCountryText";
            this.editFormCountryText.Size = new System.Drawing.Size(271, 22);
            this.editFormCountryText.TabIndex = 19;
            // 
            // editFormYear
            // 
            this.editFormYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editFormYear.Location = new System.Drawing.Point(311, 104);
            this.editFormYear.Name = "editFormYear";
            this.editFormYear.Size = new System.Drawing.Size(270, 22);
            this.editFormYear.TabIndex = 18;
            // 
            // editFormActorsListBox
            // 
            this.editFormActorsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editFormActorsListBox.FormattingEnabled = true;
            this.editFormActorsListBox.ItemHeight = 16;
            this.editFormActorsListBox.Location = new System.Drawing.Point(311, 278);
            this.editFormActorsListBox.Name = "editFormActorsListBox";
            this.editFormActorsListBox.Size = new System.Drawing.Size(210, 148);
            this.editFormActorsListBox.TabIndex = 17;
            // 
            // editFormDeleteActor
            // 
            this.editFormDeleteActor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editFormDeleteActor.Location = new System.Drawing.Point(527, 277);
            this.editFormDeleteActor.Name = "editFormDeleteActor";
            this.editFormDeleteActor.Size = new System.Drawing.Size(80, 24);
            this.editFormDeleteActor.TabIndex = 23;
            this.editFormDeleteActor.Text = "Удалить";
            this.editFormDeleteActor.UseVisualStyleBackColor = true;
            this.editFormDeleteActor.Click += new System.EventHandler(this.OnEditFormDeleteActorClick);
            // 
            // editFormAddActorBtn
            // 
            this.editFormAddActorBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editFormAddActorBtn.Location = new System.Drawing.Point(527, 247);
            this.editFormAddActorBtn.Name = "editFormAddActorBtn";
            this.editFormAddActorBtn.Size = new System.Drawing.Size(80, 24);
            this.editFormAddActorBtn.TabIndex = 22;
            this.editFormAddActorBtn.Text = "+";
            this.editFormAddActorBtn.UseVisualStyleBackColor = true;
            this.editFormAddActorBtn.Click += new System.EventHandler(this.OnEditFormAddActorBtnClick);
            // 
            // editFormAddActor
            // 
            this.editFormAddActor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editFormAddActor.FormattingEnabled = true;
            this.editFormAddActor.Location = new System.Drawing.Point(310, 247);
            this.editFormAddActor.Name = "editFormAddActor";
            this.editFormAddActor.Size = new System.Drawing.Size(211, 24);
            this.editFormAddActor.TabIndex = 21;
            // 
            // editFormDirector
            // 
            this.editFormDirector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editFormDirector.FormattingEnabled = true;
            this.editFormDirector.Location = new System.Drawing.Point(310, 199);
            this.editFormDirector.Name = "editFormDirector";
            this.editFormDirector.Size = new System.Drawing.Size(271, 24);
            this.editFormDirector.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(307, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Режиссер";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(307, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Актеры";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Год";
            // 
            // uploadMovieBox
            // 
            this.uploadMovieBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uploadMovieBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uploadMovieBox.Location = new System.Drawing.Point(3, 3);
            this.uploadMovieBox.Name = "uploadMovieBox";
            this.uploadMovieBox.Size = new System.Drawing.Size(301, 457);
            this.uploadMovieBox.TabIndex = 4;
            this.uploadMovieBox.TabStop = false;
            this.uploadMovieBox.Click += new System.EventHandler(this.OnUploadMovieBoxClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(307, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Название";
            // 
            // editMovieBackBtn
            // 
            this.editMovieBackBtn.Location = new System.Drawing.Point(310, 3);
            this.editMovieBackBtn.Name = "editMovieBackBtn";
            this.editMovieBackBtn.Size = new System.Drawing.Size(167, 23);
            this.editMovieBackBtn.TabIndex = 2;
            this.editMovieBackBtn.Text = "<< Назад к списку";
            this.editMovieBackBtn.UseVisualStyleBackColor = true;
            this.editMovieBackBtn.Click += new System.EventHandler(this.OnEditMovieBackBtnClick);
            // 
            // editFormNameText
            // 
            this.editFormNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editFormNameText.Location = new System.Drawing.Point(310, 56);
            this.editFormNameText.Name = "editFormNameText";
            this.editFormNameText.Size = new System.Drawing.Size(271, 22);
            this.editFormNameText.TabIndex = 3;
            // 
            // editMovieSaveBtn
            // 
            this.editMovieSaveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editMovieSaveBtn.Location = new System.Drawing.Point(505, 435);
            this.editMovieSaveBtn.Name = "editMovieSaveBtn";
            this.editMovieSaveBtn.Size = new System.Drawing.Size(102, 23);
            this.editMovieSaveBtn.TabIndex = 24;
            this.editMovieSaveBtn.Text = "Сохранить";
            this.editMovieSaveBtn.UseVisualStyleBackColor = true;
            this.editMovieSaveBtn.Click += new System.EventHandler(this.OnEditMovieSaveBtnClick);
            // 
            // editMovieFormErrorProvider
            // 
            this.editMovieFormErrorProvider.ContainerControl = this;
            // 
            // gridTitleLabel
            // 
            this.gridTitleLabel.AutoSize = true;
            this.gridTitleLabel.Location = new System.Drawing.Point(13, 33);
            this.gridTitleLabel.Name = "gridTitleLabel";
            this.gridTitleLabel.Size = new System.Drawing.Size(89, 17);
            this.gridTitleLabel.TabIndex = 3;
            this.gridTitleLabel.Text = "Все фильмы";
            // 
            // CatalogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 501);
            this.Controls.Add(this.gridTitleLabel);
            this.Controls.Add(this.editMoviePanel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1037, 546);
            this.Name = "CatalogView";
            this.Text = "Movies";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnCatalogViewFormClosing);
            this.Resize += new System.EventHandler(this.OnCatalogViewResize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.editMoviePanel.ResumeLayout(false);
            this.editMoviePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uploadMovieBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editMovieFormErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editFilmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFilmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findFilmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel editMoviePanel;
        private PlainTextInput editFormNameText;
        private System.Windows.Forms.Button editMovieSaveBtn;
        private System.Windows.Forms.Button editMovieBackBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider editMovieFormErrorProvider;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label gridTitleLabel;
        private System.Windows.Forms.PictureBox uploadMovieBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private Autocomplete editFormDirector;
        private System.Windows.Forms.Button editFormAddActorBtn;
        private Autocomplete editFormAddActor;
        private System.Windows.Forms.Button editFormDeleteActor;
        private System.Windows.Forms.ListBox editFormActorsListBox;
        private System.Windows.Forms.HelpProvider editMovieFormHelpProvider;
        private YearInput editFormYear;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolStripMenuItem refillDatabaseToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private PlainTextInput editFormCountryText;
    }
}

