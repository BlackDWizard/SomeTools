
namespace TestForm
{
    partial class GenerateIDForm
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
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnIDNO = new System.Windows.Forms.Button();
            this.btnRPNO = new System.Windows.Forms.Button();
            this.btnCNO = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnBatchGenerate = new System.Windows.Forms.Button();
            this.rbM = new System.Windows.Forms.RadioButton();
            this.rbF = new System.Windows.Forms.RadioButton();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCheckRPNO = new System.Windows.Forms.Button();
            this.btnCheckCNO = new System.Windows.Forms.Button();
            this.btnCheckIDNO = new System.Windows.Forms.Button();
            this.tbCheckResult = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(9, 10);
            this.tbOutput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbOutput.MaxLength = 10;
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(535, 208);
            this.tbOutput.TabIndex = 1;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(548, 10);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(216, 207);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnIDNO
            // 
            this.btnIDNO.Location = new System.Drawing.Point(104, 246);
            this.btnIDNO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnIDNO.Name = "btnIDNO";
            this.btnIDNO.Size = new System.Drawing.Size(143, 77);
            this.btnIDNO.TabIndex = 3;
            this.btnIDNO.Text = "產生身分證號";
            this.btnIDNO.UseVisualStyleBackColor = true;
            this.btnIDNO.Click += new System.EventHandler(this.btnIDNO_Click);
            // 
            // btnRPNO
            // 
            this.btnRPNO.Location = new System.Drawing.Point(252, 246);
            this.btnRPNO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRPNO.Name = "btnRPNO";
            this.btnRPNO.Size = new System.Drawing.Size(143, 77);
            this.btnRPNO.TabIndex = 4;
            this.btnRPNO.Text = "產生居留證號";
            this.btnRPNO.UseVisualStyleBackColor = true;
            this.btnRPNO.Click += new System.EventHandler(this.btnRPNO_Click);
            // 
            // btnCNO
            // 
            this.btnCNO.Location = new System.Drawing.Point(400, 246);
            this.btnCNO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCNO.Name = "btnCNO";
            this.btnCNO.Size = new System.Drawing.Size(143, 77);
            this.btnCNO.TabIndex = 5;
            this.btnCNO.Text = "產生統編";
            this.btnCNO.UseVisualStyleBackColor = true;
            this.btnCNO.Click += new System.EventHandler(this.btnCNO_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(4, 30);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(83, 20);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnBatchGenerate
            // 
            this.btnBatchGenerate.Location = new System.Drawing.Point(643, 246);
            this.btnBatchGenerate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBatchGenerate.Name = "btnBatchGenerate";
            this.btnBatchGenerate.Size = new System.Drawing.Size(121, 77);
            this.btnBatchGenerate.TabIndex = 7;
            this.btnBatchGenerate.Text = "批次產生";
            this.btnBatchGenerate.UseVisualStyleBackColor = true;
            this.btnBatchGenerate.Click += new System.EventHandler(this.btnBatchGenerate_Click);
            // 
            // rbM
            // 
            this.rbM.AutoSize = true;
            this.rbM.Checked = true;
            this.rbM.Location = new System.Drawing.Point(4, 30);
            this.rbM.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbM.Name = "rbM";
            this.rbM.Size = new System.Drawing.Size(35, 16);
            this.rbM.TabIndex = 8;
            this.rbM.TabStop = true;
            this.rbM.Text = "男";
            this.rbM.UseVisualStyleBackColor = true;
            // 
            // rbF
            // 
            this.rbF.AutoSize = true;
            this.rbF.Location = new System.Drawing.Point(54, 30);
            this.rbF.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbF.Name = "rbF";
            this.rbF.Size = new System.Drawing.Size(35, 16);
            this.rbF.TabIndex = 9;
            this.rbF.Text = "女";
            this.rbF.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "臺北市(A)",
            "臺中市(B)",
            "基隆市(C)",
            "臺南市(D)",
            "高雄市(E)",
            "臺北縣(F)",
            "宜蘭縣(G)",
            "桃園縣(H)",
            "新竹縣(J)",
            "苗栗縣(K)",
            "臺中縣(L)",
            "南投縣(M)",
            "彰化縣(N)",
            "雲林縣(P)",
            "嘉義縣(Q)",
            "臺南縣(R)",
            "高雄縣(S)",
            "屏東縣(T)",
            "花蓮縣(U)",
            "臺東縣(V)",
            "澎湖縣(X)",
            "陽明山(Y)",
            "金門縣(W)",
            "連江縣(Z)",
            "新竹市(O)",
            "嘉義市(I)"});
            this.comboBox2.Location = new System.Drawing.Point(4, 30);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(83, 20);
            this.comboBox2.TabIndex = 10;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbM);
            this.groupBox1.Controls.Add(this.rbF);
            this.groupBox1.Location = new System.Drawing.Point(9, 246);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(91, 77);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "性別";
            // 
            // btnCheckRPNO
            // 
            this.btnCheckRPNO.Location = new System.Drawing.Point(252, 327);
            this.btnCheckRPNO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCheckRPNO.Name = "btnCheckRPNO";
            this.btnCheckRPNO.Size = new System.Drawing.Size(143, 77);
            this.btnCheckRPNO.TabIndex = 12;
            this.btnCheckRPNO.Text = "驗證居留證號";
            this.btnCheckRPNO.UseVisualStyleBackColor = true;
            this.btnCheckRPNO.Click += new System.EventHandler(this.btnCheckRPNO_Click);
            // 
            // btnCheckCNO
            // 
            this.btnCheckCNO.Location = new System.Drawing.Point(400, 327);
            this.btnCheckCNO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCheckCNO.Name = "btnCheckCNO";
            this.btnCheckCNO.Size = new System.Drawing.Size(143, 77);
            this.btnCheckCNO.TabIndex = 13;
            this.btnCheckCNO.Text = "驗證統編";
            this.btnCheckCNO.UseVisualStyleBackColor = true;
            this.btnCheckCNO.Click += new System.EventHandler(this.btnCheckCNO_Click);
            // 
            // btnCheckIDNO
            // 
            this.btnCheckIDNO.Location = new System.Drawing.Point(104, 327);
            this.btnCheckIDNO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCheckIDNO.Name = "btnCheckIDNO";
            this.btnCheckIDNO.Size = new System.Drawing.Size(143, 77);
            this.btnCheckIDNO.TabIndex = 14;
            this.btnCheckIDNO.Text = "驗證身分證號";
            this.btnCheckIDNO.UseVisualStyleBackColor = true;
            this.btnCheckIDNO.Click += new System.EventHandler(this.btnCheckIDNO_Click);
            // 
            // tbCheckResult
            // 
            this.tbCheckResult.Location = new System.Drawing.Point(4, 19);
            this.tbCheckResult.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbCheckResult.Multiline = true;
            this.tbCheckResult.Name = "tbCheckResult";
            this.tbCheckResult.Size = new System.Drawing.Size(207, 54);
            this.tbCheckResult.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Location = new System.Drawing.Point(9, 327);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(91, 77);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "縣市";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbCheckResult);
            this.groupBox3.Location = new System.Drawing.Point(548, 327);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(215, 77);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "結果";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox1);
            this.groupBox4.Location = new System.Drawing.Point(548, 246);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Size = new System.Drawing.Size(91, 77);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "批次產生類型";
            // 
            // GenerateIDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 412);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCheckIDNO);
            this.Controls.Add(this.btnCheckCNO);
            this.Controls.Add(this.btnCheckRPNO);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBatchGenerate);
            this.Controls.Add(this.btnCNO);
            this.Controls.Add(this.btnRPNO);
            this.Controls.Add(this.btnIDNO);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.tbOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GenerateIDForm";
            this.Text = "GenerateIDForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnIDNO;
        private System.Windows.Forms.Button btnRPNO;
        private System.Windows.Forms.Button btnCNO;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnBatchGenerate;
        private System.Windows.Forms.RadioButton rbM;
        private System.Windows.Forms.RadioButton rbF;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCheckRPNO;
        private System.Windows.Forms.Button btnCheckCNO;
        private System.Windows.Forms.Button btnCheckIDNO;
        private System.Windows.Forms.TextBox tbCheckResult;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}