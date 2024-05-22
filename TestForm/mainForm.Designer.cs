
namespace TestForm
{
    partial class mainForm
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
            this.btnAPITestForm = new System.Windows.Forms.Button();
            this.btnGenerateID = new System.Windows.Forms.Button();
            this.btnJsonTransform = new System.Windows.Forms.Button();
            this.btnPandaman = new System.Windows.Forms.Button();
            this.btnAPITest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAPITest
            // 
            this.btnAPITestForm.Location = new System.Drawing.Point(12, 12);
            this.btnAPITestForm.Name = "btnAPITestForm";
            this.btnAPITestForm.Size = new System.Drawing.Size(168, 76);
            this.btnAPITestForm.TabIndex = 0;
            this.btnAPITestForm.Text = "APITestForm";
            this.btnAPITestForm.UseVisualStyleBackColor = true;
            this.btnAPITestForm.Click += new System.EventHandler(this.btnAPITestForm_Click);
            // 
            // btnGenerateID
            // 
            this.btnGenerateID.Location = new System.Drawing.Point(12, 94);
            this.btnGenerateID.Name = "btnGenerateID";
            this.btnGenerateID.Size = new System.Drawing.Size(168, 76);
            this.btnGenerateID.TabIndex = 1;
            this.btnGenerateID.Text = "GenerateID";
            this.btnGenerateID.UseVisualStyleBackColor = true;
            this.btnGenerateID.Click += new System.EventHandler(this.btnGenerateID_Click);
            // 
            // btnJsonTransform
            // 
            this.btnJsonTransform.Location = new System.Drawing.Point(12, 176);
            this.btnJsonTransform.Name = "btnJsonTransform";
            this.btnJsonTransform.Size = new System.Drawing.Size(168, 76);
            this.btnJsonTransform.TabIndex = 2;
            this.btnJsonTransform.Text = "JsonTransform";
            this.btnJsonTransform.UseVisualStyleBackColor = true;
            this.btnJsonTransform.Click += new System.EventHandler(this.btnJsonTransform_Click);
            // 
            // btnPandaman
            // 
            this.btnPandaman.Location = new System.Drawing.Point(12, 258);
            this.btnPandaman.Name = "btnPandaman";
            this.btnPandaman.Size = new System.Drawing.Size(168, 76);
            this.btnPandaman.TabIndex = 3;
            this.btnPandaman.Text = "Pandaman";
            this.btnPandaman.UseVisualStyleBackColor = true;
            this.btnPandaman.Click += new System.EventHandler(this.btnPandaman_Click);
            // 
            // btnAPITest
            // 
            this.btnAPITest.Location = new System.Drawing.Point(12, 340);
            this.btnAPITest.Name = "btnAPITest";
            this.btnAPITest.Size = new System.Drawing.Size(168, 76);
            this.btnAPITest.TabIndex = 4;
            this.btnAPITest.Text = "APITest";
            this.btnAPITest.UseVisualStyleBackColor = true;
            this.btnAPITest.Click += new System.EventHandler(this.btnAPITest_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAPITest);
            this.Controls.Add(this.btnPandaman);
            this.Controls.Add(this.btnJsonTransform);
            this.Controls.Add(this.btnGenerateID);
            this.Controls.Add(this.btnAPITestForm);
            this.Name = "mainForm";
            this.Text = "mainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAPITestForm;
        private System.Windows.Forms.Button btnGenerateID;
        private System.Windows.Forms.Button btnJsonTransform;
        private System.Windows.Forms.Button btnPandaman;
        private System.Windows.Forms.Button btnAPITest;
    }
}