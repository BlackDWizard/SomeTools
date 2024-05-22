
namespace TestForm
{
    partial class APITestForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbForwardContent = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbReplyContent = new System.Windows.Forms.TextBox();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbException = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbForwardContent
            // 
            this.tbForwardContent.Location = new System.Drawing.Point(9, 319);
            this.tbForwardContent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbForwardContent.Multiline = true;
            this.tbForwardContent.Name = "tbForwardContent";
            this.tbForwardContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbForwardContent.Size = new System.Drawing.Size(306, 272);
            this.tbForwardContent.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(628, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "CallAPI";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbReplyContent
            // 
            this.tbReplyContent.Location = new System.Drawing.Point(319, 319);
            this.tbReplyContent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbReplyContent.Multiline = true;
            this.tbReplyContent.Name = "tbReplyContent";
            this.tbReplyContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbReplyContent.Size = new System.Drawing.Size(306, 272);
            this.tbReplyContent.TabIndex = 3;
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(9, 26);
            this.tbURL.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbURL.Multiline = true;
            this.tbURL.Name = "tbURL";
            this.tbURL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbURL.Size = new System.Drawing.Size(306, 272);
            this.tbURL.TabIndex = 4;
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(319, 26);
            this.tbUserName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbUserName.Multiline = true;
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbUserName.Size = new System.Drawing.Size(306, 116);
            this.tbUserName.TabIndex = 6;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(318, 182);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbPassword.Multiline = true;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPassword.Size = new System.Drawing.Size(306, 116);
            this.tbPassword.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 300);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "ForwardContent";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(316, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "UserName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(318, 305);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "ReplyContent";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 156);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "Password";
            // 
            // tbException
            // 
            this.tbException.Location = new System.Drawing.Point(628, 75);
            this.tbException.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbException.Multiline = true;
            this.tbException.Name = "tbException";
            this.tbException.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbException.Size = new System.Drawing.Size(306, 516);
            this.tbException.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(628, 61);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Exception";
            // 
            // APITestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 600);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbException);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.tbURL);
            this.Controls.Add(this.tbReplyContent);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbForwardContent);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "APITestForm";
            this.Text = "APITestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbForwardContent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbReplyContent;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbException;
        private System.Windows.Forms.Label label6;
    }
}

