namespace Task5
{
    partial class ExamView
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
            this.startBtn = new System.Windows.Forms.Button();
            this.studentMarksListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(13, 13);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // studentMarksListView
            // 
            this.studentMarksListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.studentMarksListView.GridLines = true;
            this.studentMarksListView.Location = new System.Drawing.Point(13, 43);
            this.studentMarksListView.Name = "studentMarksListView";
            this.studentMarksListView.Size = new System.Drawing.Size(428, 250);
            this.studentMarksListView.TabIndex = 2;
            this.studentMarksListView.UseCompatibleStateImageBehavior = false;
            // 
            // ExamView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 305);
            this.Controls.Add(this.studentMarksListView);
            this.Controls.Add(this.startBtn);
            this.Name = "ExamView";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.ListView studentMarksListView;
    }
}

