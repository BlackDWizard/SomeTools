
namespace TestForm
{
    partial class APITest
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
            this.cbbHTTPMethods = new System.Windows.Forms.ComboBox();
            this.lbDisplayURL = new System.Windows.Forms.Label();
            this.lbURL = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.tbcParams = new System.Windows.Forms.TabControl();
            this.tpParams = new System.Windows.Forms.TabPage();
            this.plParams = new System.Windows.Forms.Panel();
            this.tecRequestBody = new ICSharpCode.TextEditor.TextEditorControl();
            this.rbBodyNone = new System.Windows.Forms.RadioButton();
            this.cbbRawType = new System.Windows.Forms.ComboBox();
            this.rbBodyFormData = new System.Windows.Forms.RadioButton();
            this.rbBodyBinary = new System.Windows.Forms.RadioButton();
            this.rbXWWWFormUrlEndcoded = new System.Windows.Forms.RadioButton();
            this.rbBodyRaw = new System.Windows.Forms.RadioButton();
            this.tpAuth = new System.Windows.Forms.TabPage();
            this.tpHeaders = new System.Windows.Forms.TabPage();
            this.tpBody = new System.Windows.Forms.TabPage();
            this.btnSend = new System.Windows.Forms.Button();
            this.tecResponseBody = new ICSharpCode.TextEditor.TextEditorControl();
            this.tbcParams.SuspendLayout();
            this.tpParams.SuspendLayout();
            this.plParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbHTTPMethods
            // 
            this.cbbHTTPMethods.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbbHTTPMethods.FormattingEnabled = true;
            this.cbbHTTPMethods.Items.AddRange(new object[] {
            "GET",
            "POST",
            "PUT",
            "PATCH",
            "DELETE",
            "HEAD",
            "OPTIONS"});
            this.cbbHTTPMethods.Location = new System.Drawing.Point(18, 45);
            this.cbbHTTPMethods.Name = "cbbHTTPMethods";
            this.cbbHTTPMethods.Size = new System.Drawing.Size(121, 24);
            this.cbbHTTPMethods.TabIndex = 0;
            // 
            // lbDisplayURL
            // 
            this.lbDisplayURL.AutoSize = true;
            this.lbDisplayURL.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbDisplayURL.Location = new System.Drawing.Point(12, 9);
            this.lbDisplayURL.Name = "lbDisplayURL";
            this.lbDisplayURL.Size = new System.Drawing.Size(83, 32);
            this.lbDisplayURL.TabIndex = 1;
            this.lbDisplayURL.Text = "URL:";
            // 
            // lbURL
            // 
            this.lbURL.AutoSize = true;
            this.lbURL.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbURL.Location = new System.Drawing.Point(101, 9);
            this.lbURL.Name = "lbURL";
            this.lbURL.Size = new System.Drawing.Size(75, 32);
            this.lbURL.TabIndex = 2;
            this.lbURL.Text = "URL";
            // 
            // tbURL
            // 
            this.tbURL.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbURL.Location = new System.Drawing.Point(145, 42);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(929, 27);
            this.tbURL.TabIndex = 3;
            // 
            // tbcParams
            // 
            this.tbcParams.Controls.Add(this.tpParams);
            this.tbcParams.Controls.Add(this.tpAuth);
            this.tbcParams.Controls.Add(this.tpHeaders);
            this.tbcParams.Controls.Add(this.tpBody);
            this.tbcParams.Location = new System.Drawing.Point(18, 76);
            this.tbcParams.Name = "tbcParams";
            this.tbcParams.SelectedIndex = 0;
            this.tbcParams.Size = new System.Drawing.Size(1151, 267);
            this.tbcParams.TabIndex = 0;
            // 
            // tpParams
            // 
            this.tpParams.Controls.Add(this.plParams);
            this.tpParams.Location = new System.Drawing.Point(4, 22);
            this.tpParams.Name = "tpParams";
            this.tpParams.Padding = new System.Windows.Forms.Padding(3);
            this.tpParams.Size = new System.Drawing.Size(1143, 241);
            this.tpParams.TabIndex = 0;
            this.tpParams.Text = "Params";
            this.tpParams.UseVisualStyleBackColor = true;
            // 
            // plParams
            // 
            this.plParams.Controls.Add(this.tecRequestBody);
            this.plParams.Controls.Add(this.rbBodyNone);
            this.plParams.Controls.Add(this.cbbRawType);
            this.plParams.Controls.Add(this.rbBodyFormData);
            this.plParams.Controls.Add(this.rbBodyBinary);
            this.plParams.Controls.Add(this.rbXWWWFormUrlEndcoded);
            this.plParams.Controls.Add(this.rbBodyRaw);
            this.plParams.Location = new System.Drawing.Point(3, 6);
            this.plParams.Name = "plParams";
            this.plParams.Size = new System.Drawing.Size(1134, 229);
            this.plParams.TabIndex = 6;
            // 
            // tecRequestBody
            // 
            this.tecRequestBody.IsReadOnly = false;
            this.tecRequestBody.Location = new System.Drawing.Point(4, 26);
            this.tecRequestBody.Name = "tecRequestBody";
            this.tecRequestBody.Size = new System.Drawing.Size(1127, 200);
            this.tecRequestBody.TabIndex = 6;
            // 
            // rbBodyNone
            // 
            this.rbBodyNone.AutoSize = true;
            this.rbBodyNone.Checked = true;
            this.rbBodyNone.Location = new System.Drawing.Point(2, 3);
            this.rbBodyNone.Name = "rbBodyNone";
            this.rbBodyNone.Size = new System.Drawing.Size(46, 16);
            this.rbBodyNone.TabIndex = 0;
            this.rbBodyNone.TabStop = true;
            this.rbBodyNone.Text = "none";
            this.rbBodyNone.UseVisualStyleBackColor = true;
            // 
            // cbbRawType
            // 
            this.cbbRawType.FormattingEnabled = true;
            this.cbbRawType.Location = new System.Drawing.Point(380, 2);
            this.cbbRawType.Name = "cbbRawType";
            this.cbbRawType.Size = new System.Drawing.Size(121, 20);
            this.cbbRawType.TabIndex = 5;
            // 
            // rbBodyFormData
            // 
            this.rbBodyFormData.AutoSize = true;
            this.rbBodyFormData.Location = new System.Drawing.Point(54, 4);
            this.rbBodyFormData.Name = "rbBodyFormData";
            this.rbBodyFormData.Size = new System.Drawing.Size(69, 16);
            this.rbBodyFormData.TabIndex = 1;
            this.rbBodyFormData.Text = "form-data";
            this.rbBodyFormData.UseVisualStyleBackColor = true;
            // 
            // rbBodyBinary
            // 
            this.rbBodyBinary.AutoSize = true;
            this.rbBodyBinary.Location = new System.Drawing.Point(321, 4);
            this.rbBodyBinary.Name = "rbBodyBinary";
            this.rbBodyBinary.Size = new System.Drawing.Size(53, 16);
            this.rbBodyBinary.TabIndex = 4;
            this.rbBodyBinary.Text = "binary";
            this.rbBodyBinary.UseVisualStyleBackColor = true;
            // 
            // rbXWWWFormUrlEndcoded
            // 
            this.rbXWWWFormUrlEndcoded.AutoSize = true;
            this.rbXWWWFormUrlEndcoded.Location = new System.Drawing.Point(129, 3);
            this.rbXWWWFormUrlEndcoded.Name = "rbXWWWFormUrlEndcoded";
            this.rbXWWWFormUrlEndcoded.Size = new System.Drawing.Size(140, 16);
            this.rbXWWWFormUrlEndcoded.TabIndex = 2;
            this.rbXWWWFormUrlEndcoded.Text = "x-www-form-urlencoded";
            this.rbXWWWFormUrlEndcoded.UseVisualStyleBackColor = true;
            // 
            // rbBodyRaw
            // 
            this.rbBodyRaw.AutoSize = true;
            this.rbBodyRaw.Location = new System.Drawing.Point(275, 3);
            this.rbBodyRaw.Name = "rbBodyRaw";
            this.rbBodyRaw.Size = new System.Drawing.Size(40, 16);
            this.rbBodyRaw.TabIndex = 3;
            this.rbBodyRaw.Text = "raw";
            this.rbBodyRaw.UseVisualStyleBackColor = true;
            this.rbBodyRaw.CheckedChanged += new System.EventHandler(this.rbBodyRaw_CheckedChanged);
            // 
            // tpAuth
            // 
            this.tpAuth.Location = new System.Drawing.Point(4, 22);
            this.tpAuth.Name = "tpAuth";
            this.tpAuth.Padding = new System.Windows.Forms.Padding(3);
            this.tpAuth.Size = new System.Drawing.Size(1143, 241);
            this.tpAuth.TabIndex = 1;
            this.tpAuth.Text = "Authorization";
            this.tpAuth.UseVisualStyleBackColor = true;
            // 
            // tpHeaders
            // 
            this.tpHeaders.Location = new System.Drawing.Point(4, 22);
            this.tpHeaders.Name = "tpHeaders";
            this.tpHeaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpHeaders.Size = new System.Drawing.Size(1143, 241);
            this.tpHeaders.TabIndex = 2;
            this.tpHeaders.Text = "Headers";
            this.tpHeaders.UseVisualStyleBackColor = true;
            // 
            // tpBody
            // 
            this.tpBody.Location = new System.Drawing.Point(4, 22);
            this.tpBody.Name = "tpBody";
            this.tpBody.Padding = new System.Windows.Forms.Padding(3);
            this.tpBody.Size = new System.Drawing.Size(1143, 241);
            this.tpBody.TabIndex = 3;
            this.tpBody.Text = "Body";
            this.tpBody.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(1080, 42);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(102, 27);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "傳送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tecResponseBody
            // 
            this.tecResponseBody.IsReadOnly = false;
            this.tecResponseBody.Location = new System.Drawing.Point(22, 349);
            this.tecResponseBody.Name = "tecResponseBody";
            this.tecResponseBody.Size = new System.Drawing.Size(1143, 349);
            this.tecResponseBody.TabIndex = 6;
            // 
            // APITest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 710);
            this.Controls.Add(this.tecResponseBody);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbcParams);
            this.Controls.Add(this.tbURL);
            this.Controls.Add(this.lbURL);
            this.Controls.Add(this.lbDisplayURL);
            this.Controls.Add(this.cbbHTTPMethods);
            this.Name = "APITest";
            this.Text = "APITest";
            this.tbcParams.ResumeLayout(false);
            this.tpParams.ResumeLayout(false);
            this.plParams.ResumeLayout(false);
            this.plParams.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbHTTPMethods;
        private System.Windows.Forms.Label lbDisplayURL;
        private System.Windows.Forms.Label lbURL;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.TabControl tbcParams;
        private System.Windows.Forms.TabPage tpParams;
        private System.Windows.Forms.TabPage tpAuth;
        private System.Windows.Forms.TabPage tpHeaders;
        private System.Windows.Forms.TabPage tpBody;
        private System.Windows.Forms.Button btnSend;
        private ICSharpCode.TextEditor.TextEditorControl tecResponseBody;
        private System.Windows.Forms.ComboBox cbbRawType;
        private System.Windows.Forms.RadioButton rbBodyBinary;
        private System.Windows.Forms.RadioButton rbBodyRaw;
        private System.Windows.Forms.RadioButton rbXWWWFormUrlEndcoded;
        private System.Windows.Forms.RadioButton rbBodyFormData;
        private System.Windows.Forms.RadioButton rbBodyNone;
        private System.Windows.Forms.Panel plParams;
        private ICSharpCode.TextEditor.TextEditorControl tecRequestBody;
    }
}