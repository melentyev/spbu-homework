namespace NetTask6.Views
{
    partial class SearchView
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
            this.label1 = new System.Windows.Forms.Label();
            this.searchViewFilmNameEdit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchViewDirector = new System.Windows.Forms.ComboBox();
            this.searchViewActor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.searchFormBtnSearch = new System.Windows.Forms.Button();
            this.searchFormBtnClear = new System.Windows.Forms.Button();
            this.searchMovieFormErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.searchViewCountryInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.searchViewYearInput = new NetTask6.Views.YearInput();
            ((System.ComponentModel.ISupportInitialize)(this.searchMovieFormErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // searchViewFilmNameEdit
            // 
            this.searchViewFilmNameEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchViewFilmNameEdit.Location = new System.Drawing.Point(13, 34);
            this.searchViewFilmNameEdit.Name = "searchViewFilmNameEdit";
            this.searchViewFilmNameEdit.Size = new System.Drawing.Size(278, 22);
            this.searchViewFilmNameEdit.TabIndex = 1;
            this.searchViewFilmNameEdit.TextChanged += new System.EventHandler(this.searchViewFilmNameEdit_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Режиссер";
            // 
            // searchViewDirector
            // 
            this.searchViewDirector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchViewDirector.FormattingEnabled = true;
            this.searchViewDirector.Location = new System.Drawing.Point(13, 179);
            this.searchViewDirector.Name = "searchViewDirector";
            this.searchViewDirector.Size = new System.Drawing.Size(278, 24);
            this.searchViewDirector.TabIndex = 3;
            this.searchViewDirector.TextChanged += new System.EventHandler(this.searchViewDirector_TextChanged);
            // 
            // searchViewActor
            // 
            this.searchViewActor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchViewActor.FormattingEnabled = true;
            this.searchViewActor.Location = new System.Drawing.Point(12, 230);
            this.searchViewActor.Name = "searchViewActor";
            this.searchViewActor.Size = new System.Drawing.Size(278, 24);
            this.searchViewActor.TabIndex = 5;
            this.searchViewActor.TextChanged += new System.EventHandler(this.searchViewActor_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Актер";
            // 
            // searchFormBtnSearch
            // 
            this.searchFormBtnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchFormBtnSearch.Location = new System.Drawing.Point(206, 346);
            this.searchFormBtnSearch.Name = "searchFormBtnSearch";
            this.searchFormBtnSearch.Size = new System.Drawing.Size(99, 23);
            this.searchFormBtnSearch.TabIndex = 6;
            this.searchFormBtnSearch.Text = "Поиск";
            this.searchFormBtnSearch.UseVisualStyleBackColor = true;
            this.searchFormBtnSearch.Click += new System.EventHandler(this.searchFormBtnSearch_Click);
            // 
            // searchFormBtnClear
            // 
            this.searchFormBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchFormBtnClear.Location = new System.Drawing.Point(97, 346);
            this.searchFormBtnClear.Name = "searchFormBtnClear";
            this.searchFormBtnClear.Size = new System.Drawing.Size(101, 23);
            this.searchFormBtnClear.TabIndex = 7;
            this.searchFormBtnClear.Text = "Очистить";
            this.searchFormBtnClear.UseVisualStyleBackColor = true;
            this.searchFormBtnClear.Click += new System.EventHandler(this.searchFormBtnClear_Click);
            // 
            // searchMovieFormErrorProvider
            // 
            this.searchMovieFormErrorProvider.ContainerControl = this;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Год выпуска";
            // 
            // searchViewCountryInput
            // 
            this.searchViewCountryInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchViewCountryInput.Location = new System.Drawing.Point(12, 81);
            this.searchViewCountryInput.Name = "searchViewCountryInput";
            this.searchViewCountryInput.Size = new System.Drawing.Size(278, 22);
            this.searchViewCountryInput.TabIndex = 10;
            this.searchViewCountryInput.TextChanged += new System.EventHandler(this.searchViewCountryInput_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Страна";
            // 
            // searchViewYearInput
            // 
            this.searchViewYearInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchViewYearInput.Location = new System.Drawing.Point(12, 129);
            this.searchViewYearInput.Name = "searchViewYearInput";
            this.searchViewYearInput.Size = new System.Drawing.Size(279, 22);
            this.searchViewYearInput.TabIndex = 8;
            this.searchViewYearInput.TextChanged += new System.EventHandler(this.searchViewYearInput_TextChanged);
            // 
            // SearchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 381);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.searchViewCountryInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.searchViewYearInput);
            this.Controls.Add(this.searchFormBtnClear);
            this.Controls.Add(this.searchFormBtnSearch);
            this.Controls.Add(this.searchViewActor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchViewDirector);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.searchViewFilmNameEdit);
            this.Controls.Add(this.label1);
            this.Name = "SearchView";
            this.Text = "SearchView";
            ((System.ComponentModel.ISupportInitialize)(this.searchMovieFormErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchViewFilmNameEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox searchViewDirector;
        private System.Windows.Forms.ComboBox searchViewActor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button searchFormBtnSearch;
        private System.Windows.Forms.Button searchFormBtnClear;
        private System.Windows.Forms.ErrorProvider searchMovieFormErrorProvider;
        private System.Windows.Forms.Label label4;
        private YearInput searchViewYearInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox searchViewCountryInput;
    }
}