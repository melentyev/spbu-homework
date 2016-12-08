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
            this.searchMovieFormEerrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.searchMovieFormEerrorProvider)).BeginInit();
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
            this.searchViewFilmNameEdit.Location = new System.Drawing.Point(13, 34);
            this.searchViewFilmNameEdit.Name = "searchViewFilmNameEdit";
            this.searchViewFilmNameEdit.Size = new System.Drawing.Size(278, 22);
            this.searchViewFilmNameEdit.TabIndex = 1;
            this.searchViewFilmNameEdit.TextChanged += new System.EventHandler(this.searchViewFilmNameEdit_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Режиссер";
            // 
            // searchViewDirector
            // 
            this.searchViewDirector.FormattingEnabled = true;
            this.searchViewDirector.Location = new System.Drawing.Point(13, 80);
            this.searchViewDirector.Name = "searchViewDirector";
            this.searchViewDirector.Size = new System.Drawing.Size(278, 24);
            this.searchViewDirector.TabIndex = 3;
            this.searchViewDirector.TextChanged += new System.EventHandler(this.searchViewDirector_TextChanged);
            // 
            // searchViewActor
            // 
            this.searchViewActor.FormattingEnabled = true;
            this.searchViewActor.Location = new System.Drawing.Point(12, 128);
            this.searchViewActor.Name = "searchViewActor";
            this.searchViewActor.Size = new System.Drawing.Size(278, 24);
            this.searchViewActor.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Актер";
            // 
            // searchFormBtnSearch
            // 
            this.searchFormBtnSearch.Location = new System.Drawing.Point(192, 346);
            this.searchFormBtnSearch.Name = "searchFormBtnSearch";
            this.searchFormBtnSearch.Size = new System.Drawing.Size(99, 23);
            this.searchFormBtnSearch.TabIndex = 6;
            this.searchFormBtnSearch.Text = "Поиск";
            this.searchFormBtnSearch.UseVisualStyleBackColor = true;
            this.searchFormBtnSearch.Click += new System.EventHandler(this.searchFormBtnSearch_Click);
            // 
            // searchFormBtnClear
            // 
            this.searchFormBtnClear.Location = new System.Drawing.Point(83, 346);
            this.searchFormBtnClear.Name = "searchFormBtnClear";
            this.searchFormBtnClear.Size = new System.Drawing.Size(101, 23);
            this.searchFormBtnClear.TabIndex = 7;
            this.searchFormBtnClear.Text = "Очистить";
            this.searchFormBtnClear.UseVisualStyleBackColor = true;
            this.searchFormBtnClear.Click += new System.EventHandler(this.searchFormBtnClear_Click);
            // 
            // searchMovieFormEerrorProvider
            // 
            this.searchMovieFormEerrorProvider.ContainerControl = this;
            // 
            // SearchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 381);
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
            ((System.ComponentModel.ISupportInitialize)(this.searchMovieFormEerrorProvider)).EndInit();
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
        private System.Windows.Forms.ErrorProvider searchMovieFormEerrorProvider;
    }
}