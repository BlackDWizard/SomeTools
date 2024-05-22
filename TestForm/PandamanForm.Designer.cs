
namespace TestForm
{
    partial class PandamanForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConnTest = new System.Windows.Forms.TabPage();
            this.gvConnStrings = new System.Windows.Forms.DataGridView();
            this.ColDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColConnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDBName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSaveConnID = new System.Windows.Forms.Button();
            this.btnConnTest = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbUID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDBName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbConnID = new System.Windows.Forms.TextBox();
            this.lbServerName = new System.Windows.Forms.Label();
            this.tabGenerateInfoScript = new System.Windows.Forms.TabPage();
            this.tecInfoScript = new ICSharpCode.TextEditor.TextEditorControl();
            this.gvTableAttributes = new System.Windows.Forms.DataGridView();
            this.CBLTables = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gbWriteExplog = new System.Windows.Forms.GroupBox();
            this.rbWriteExplogN = new System.Windows.Forms.RadioButton();
            this.rbWriteExplogY = new System.Windows.Forms.RadioButton();
            this.gbIsSuccess = new System.Windows.Forms.GroupBox();
            this.rbIsSuccessN = new System.Windows.Forms.RadioButton();
            this.rbIsSuccessY = new System.Windows.Forms.RadioButton();
            this.gbThrowException = new System.Windows.Forms.GroupBox();
            this.rbThrowExceptionN = new System.Windows.Forms.RadioButton();
            this.rbThrowExceptionY = new System.Windows.Forms.RadioButton();
            this.tbNameSpace = new System.Windows.Forms.TextBox();
            this.lbNameSpace = new System.Windows.Forms.Label();
            this.btnGenerateInfo = new System.Windows.Forms.Button();
            this.lbVersion = new System.Windows.Forms.Label();
            this.cbbVersion = new System.Windows.Forms.ComboBox();
            this.btnLoadTable = new System.Windows.Forms.Button();
            this.lbSelectedConnID = new System.Windows.Forms.Label();
            this.cbbConnID = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabConnTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvConnStrings)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabGenerateInfoScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTableAttributes)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gbWriteExplog.SuspendLayout();
            this.gbIsSuccess.SuspendLayout();
            this.gbThrowException.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabConnTest);
            this.tabControl1.Controls.Add(this.tabGenerateInfoScript);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1757, 785);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabConnTest
            // 
            this.tabConnTest.Controls.Add(this.gvConnStrings);
            this.tabConnTest.Controls.Add(this.groupBox1);
            this.tabConnTest.Location = new System.Drawing.Point(4, 22);
            this.tabConnTest.Name = "tabConnTest";
            this.tabConnTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnTest.Size = new System.Drawing.Size(1749, 759);
            this.tabConnTest.TabIndex = 0;
            this.tabConnTest.Text = "連線測試";
            this.tabConnTest.UseVisualStyleBackColor = true;
            // 
            // gvConnStrings
            // 
            this.gvConnStrings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvConnStrings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColDelete,
            this.ColConnID,
            this.ColServerName,
            this.ColDBName,
            this.ColUID,
            this.ColPassword});
            this.gvConnStrings.Location = new System.Drawing.Point(6, 108);
            this.gvConnStrings.Name = "gvConnStrings";
            this.gvConnStrings.ReadOnly = true;
            this.gvConnStrings.RowTemplate.Height = 40;
            this.gvConnStrings.Size = new System.Drawing.Size(1737, 609);
            this.gvConnStrings.TabIndex = 1;
            this.gvConnStrings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ColDelete
            // 
            this.ColDelete.HeaderText = "刪除";
            this.ColDelete.Name = "ColDelete";
            this.ColDelete.ReadOnly = true;
            this.ColDelete.Text = "刪除";
            this.ColDelete.UseColumnTextForButtonValue = true;
            // 
            // ColConnID
            // 
            this.ColConnID.DataPropertyName = "ConnID";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColConnID.DefaultCellStyle = dataGridViewCellStyle11;
            this.ColConnID.HeaderText = "ConnID";
            this.ColConnID.Name = "ColConnID";
            this.ColConnID.ReadOnly = true;
            this.ColConnID.Width = 300;
            // 
            // ColServerName
            // 
            this.ColServerName.DataPropertyName = "ServerName";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColServerName.DefaultCellStyle = dataGridViewCellStyle12;
            this.ColServerName.HeaderText = "ServerName";
            this.ColServerName.Name = "ColServerName";
            this.ColServerName.ReadOnly = true;
            this.ColServerName.Width = 300;
            // 
            // ColDBName
            // 
            this.ColDBName.DataPropertyName = "DBName";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColDBName.DefaultCellStyle = dataGridViewCellStyle13;
            this.ColDBName.HeaderText = "DBName";
            this.ColDBName.Name = "ColDBName";
            this.ColDBName.ReadOnly = true;
            this.ColDBName.Width = 300;
            // 
            // ColUID
            // 
            this.ColUID.DataPropertyName = "UID";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColUID.DefaultCellStyle = dataGridViewCellStyle14;
            this.ColUID.HeaderText = "UID";
            this.ColUID.Name = "ColUID";
            this.ColUID.ReadOnly = true;
            this.ColUID.Width = 300;
            // 
            // ColPassword
            // 
            this.ColPassword.DataPropertyName = "Password";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColPassword.DefaultCellStyle = dataGridViewCellStyle15;
            this.ColPassword.HeaderText = "Password";
            this.ColPassword.Name = "ColPassword";
            this.ColPassword.ReadOnly = true;
            this.ColPassword.Width = 300;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSaveConnID);
            this.groupBox1.Controls.Add(this.btnConnTest);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbUID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbDBName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbServerName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbConnID);
            this.groupBox1.Controls.Add(this.lbServerName);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1737, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DB相關區域";
            // 
            // btnSaveConnID
            // 
            this.btnSaveConnID.Location = new System.Drawing.Point(1034, 23);
            this.btnSaveConnID.Name = "btnSaveConnID";
            this.btnSaveConnID.Size = new System.Drawing.Size(191, 42);
            this.btnSaveConnID.TabIndex = 11;
            this.btnSaveConnID.Text = "儲存";
            this.btnSaveConnID.UseVisualStyleBackColor = true;
            this.btnSaveConnID.Click += new System.EventHandler(this.btnSaveConnID_Click);
            // 
            // btnConnTest
            // 
            this.btnConnTest.Location = new System.Drawing.Point(837, 23);
            this.btnConnTest.Name = "btnConnTest";
            this.btnConnTest.Size = new System.Drawing.Size(191, 42);
            this.btnConnTest.TabIndex = 10;
            this.btnConnTest.Text = "連線測試";
            this.btnConnTest.UseVisualStyleBackColor = true;
            this.btnConnTest.Click += new System.EventHandler(this.btnConnTest_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(731, 35);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(100, 22);
            this.tbPassword.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(668, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Password : ";
            // 
            // tbUID
            // 
            this.tbUID.Location = new System.Drawing.Point(562, 35);
            this.tbUID.Name = "tbUID";
            this.tbUID.Size = new System.Drawing.Size(100, 22);
            this.tbUID.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(522, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "UID : ";
            // 
            // tbDBName
            // 
            this.tbDBName.Location = new System.Drawing.Point(416, 35);
            this.tbDBName.Name = "tbDBName";
            this.tbDBName.Size = new System.Drawing.Size(100, 22);
            this.tbDBName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "DBName : ";
            // 
            // tbServerName
            // 
            this.tbServerName.Location = new System.Drawing.Point(247, 35);
            this.tbServerName.Name = "tbServerName";
            this.tbServerName.Size = new System.Drawing.Size(100, 22);
            this.tbServerName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "ServerName : ";
            // 
            // tbConnID
            // 
            this.tbConnID.Location = new System.Drawing.Point(64, 35);
            this.tbConnID.Name = "tbConnID";
            this.tbConnID.Size = new System.Drawing.Size(100, 22);
            this.tbConnID.TabIndex = 1;
            // 
            // lbServerName
            // 
            this.lbServerName.AutoSize = true;
            this.lbServerName.Location = new System.Drawing.Point(6, 38);
            this.lbServerName.Name = "lbServerName";
            this.lbServerName.Size = new System.Drawing.Size(52, 12);
            this.lbServerName.TabIndex = 0;
            this.lbServerName.Text = "ConnID : ";
            // 
            // tabGenerateInfoScript
            // 
            this.tabGenerateInfoScript.Controls.Add(this.tecInfoScript);
            this.tabGenerateInfoScript.Controls.Add(this.gvTableAttributes);
            this.tabGenerateInfoScript.Controls.Add(this.CBLTables);
            this.tabGenerateInfoScript.Controls.Add(this.groupBox2);
            this.tabGenerateInfoScript.Location = new System.Drawing.Point(4, 22);
            this.tabGenerateInfoScript.Name = "tabGenerateInfoScript";
            this.tabGenerateInfoScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabGenerateInfoScript.Size = new System.Drawing.Size(1749, 759);
            this.tabGenerateInfoScript.TabIndex = 1;
            this.tabGenerateInfoScript.Text = "產生InformationScript";
            this.tabGenerateInfoScript.UseVisualStyleBackColor = true;
            // 
            // tecInfoScript
            // 
            this.tecInfoScript.IsReadOnly = false;
            this.tecInfoScript.Location = new System.Drawing.Point(7, 448);
            this.tecInfoScript.Name = "tecInfoScript";
            this.tecInfoScript.Size = new System.Drawing.Size(1736, 305);
            this.tecInfoScript.TabIndex = 6;
            this.tecInfoScript.Load += new System.EventHandler(this.tecInfoScript_Load);
            // 
            // gvTableAttributes
            // 
            this.gvTableAttributes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTableAttributes.Location = new System.Drawing.Point(332, 135);
            this.gvTableAttributes.Name = "gvTableAttributes";
            this.gvTableAttributes.ReadOnly = true;
            this.gvTableAttributes.RowTemplate.Height = 24;
            this.gvTableAttributes.Size = new System.Drawing.Size(1411, 307);
            this.gvTableAttributes.TabIndex = 4;
            // 
            // CBLTables
            // 
            this.CBLTables.FormattingEnabled = true;
            this.CBLTables.Location = new System.Drawing.Point(9, 132);
            this.CBLTables.Name = "CBLTables";
            this.CBLTables.Size = new System.Drawing.Size(317, 310);
            this.CBLTables.TabIndex = 3;
            this.CBLTables.SelectedIndexChanged += new System.EventHandler(this.CBLTables_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gbWriteExplog);
            this.groupBox2.Controls.Add(this.gbIsSuccess);
            this.groupBox2.Controls.Add(this.gbThrowException);
            this.groupBox2.Controls.Add(this.tbNameSpace);
            this.groupBox2.Controls.Add(this.lbNameSpace);
            this.groupBox2.Controls.Add(this.btnGenerateInfo);
            this.groupBox2.Controls.Add(this.lbVersion);
            this.groupBox2.Controls.Add(this.cbbVersion);
            this.groupBox2.Controls.Add(this.btnLoadTable);
            this.groupBox2.Controls.Add(this.lbSelectedConnID);
            this.groupBox2.Controls.Add(this.cbbConnID);
            this.groupBox2.Location = new System.Drawing.Point(7, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1736, 122);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "屬性設定";
            // 
            // gbWriteExplog
            // 
            this.gbWriteExplog.Controls.Add(this.rbWriteExplogN);
            this.gbWriteExplog.Controls.Add(this.rbWriteExplogY);
            this.gbWriteExplog.Location = new System.Drawing.Point(500, 44);
            this.gbWriteExplog.Name = "gbWriteExplog";
            this.gbWriteExplog.Size = new System.Drawing.Size(170, 61);
            this.gbWriteExplog.TabIndex = 16;
            this.gbWriteExplog.TabStop = false;
            this.gbWriteExplog.Text = "寫入DbCommand至ExpLog : ";
            // 
            // rbWriteExplogN
            // 
            this.rbWriteExplogN.AutoSize = true;
            this.rbWriteExplogN.Checked = true;
            this.rbWriteExplogN.Location = new System.Drawing.Point(6, 36);
            this.rbWriteExplogN.Name = "rbWriteExplogN";
            this.rbWriteExplogN.Size = new System.Drawing.Size(35, 16);
            this.rbWriteExplogN.TabIndex = 1;
            this.rbWriteExplogN.TabStop = true;
            this.rbWriteExplogN.Text = "否";
            this.rbWriteExplogN.UseVisualStyleBackColor = true;
            // 
            // rbWriteExplogY
            // 
            this.rbWriteExplogY.AutoSize = true;
            this.rbWriteExplogY.Location = new System.Drawing.Point(6, 18);
            this.rbWriteExplogY.Name = "rbWriteExplogY";
            this.rbWriteExplogY.Size = new System.Drawing.Size(35, 16);
            this.rbWriteExplogY.TabIndex = 0;
            this.rbWriteExplogY.TabStop = true;
            this.rbWriteExplogY.Text = "是";
            this.rbWriteExplogY.UseVisualStyleBackColor = true;
            // 
            // gbIsSuccess
            // 
            this.gbIsSuccess.Controls.Add(this.rbIsSuccessN);
            this.gbIsSuccess.Controls.Add(this.rbIsSuccessY);
            this.gbIsSuccess.Location = new System.Drawing.Point(365, 44);
            this.gbIsSuccess.Name = "gbIsSuccess";
            this.gbIsSuccess.Size = new System.Drawing.Size(117, 61);
            this.gbIsSuccess.TabIndex = 16;
            this.gbIsSuccess.TabStop = false;
            this.gbIsSuccess.Text = "產生IsSuccess : ";
            // 
            // rbIsSuccessN
            // 
            this.rbIsSuccessN.AutoSize = true;
            this.rbIsSuccessN.Checked = true;
            this.rbIsSuccessN.Location = new System.Drawing.Point(6, 36);
            this.rbIsSuccessN.Name = "rbIsSuccessN";
            this.rbIsSuccessN.Size = new System.Drawing.Size(35, 16);
            this.rbIsSuccessN.TabIndex = 1;
            this.rbIsSuccessN.TabStop = true;
            this.rbIsSuccessN.Text = "否";
            this.rbIsSuccessN.UseVisualStyleBackColor = true;
            // 
            // rbIsSuccessY
            // 
            this.rbIsSuccessY.AutoSize = true;
            this.rbIsSuccessY.Location = new System.Drawing.Point(6, 18);
            this.rbIsSuccessY.Name = "rbIsSuccessY";
            this.rbIsSuccessY.Size = new System.Drawing.Size(35, 16);
            this.rbIsSuccessY.TabIndex = 0;
            this.rbIsSuccessY.TabStop = true;
            this.rbIsSuccessY.Text = "是";
            this.rbIsSuccessY.UseVisualStyleBackColor = true;
            // 
            // gbThrowException
            // 
            this.gbThrowException.Controls.Add(this.rbThrowExceptionN);
            this.gbThrowException.Controls.Add(this.rbThrowExceptionY);
            this.gbThrowException.Location = new System.Drawing.Point(229, 44);
            this.gbThrowException.Name = "gbThrowException";
            this.gbThrowException.Size = new System.Drawing.Size(117, 61);
            this.gbThrowException.TabIndex = 15;
            this.gbThrowException.TabStop = false;
            this.gbThrowException.Text = "throw Exception : ";
            // 
            // rbThrowExceptionN
            // 
            this.rbThrowExceptionN.AutoSize = true;
            this.rbThrowExceptionN.Checked = true;
            this.rbThrowExceptionN.Location = new System.Drawing.Point(6, 36);
            this.rbThrowExceptionN.Name = "rbThrowExceptionN";
            this.rbThrowExceptionN.Size = new System.Drawing.Size(35, 16);
            this.rbThrowExceptionN.TabIndex = 1;
            this.rbThrowExceptionN.TabStop = true;
            this.rbThrowExceptionN.Text = "否";
            this.rbThrowExceptionN.UseVisualStyleBackColor = true;
            // 
            // rbThrowExceptionY
            // 
            this.rbThrowExceptionY.AutoSize = true;
            this.rbThrowExceptionY.Location = new System.Drawing.Point(6, 18);
            this.rbThrowExceptionY.Name = "rbThrowExceptionY";
            this.rbThrowExceptionY.Size = new System.Drawing.Size(35, 16);
            this.rbThrowExceptionY.TabIndex = 0;
            this.rbThrowExceptionY.Text = "是";
            this.rbThrowExceptionY.UseVisualStyleBackColor = true;
            // 
            // tbNameSpace
            // 
            this.tbNameSpace.Location = new System.Drawing.Point(550, 18);
            this.tbNameSpace.Name = "tbNameSpace";
            this.tbNameSpace.Size = new System.Drawing.Size(120, 22);
            this.tbNameSpace.TabIndex = 14;
            this.tbNameSpace.Text = "Vista.Information";
            // 
            // lbNameSpace
            // 
            this.lbNameSpace.AutoSize = true;
            this.lbNameSpace.Location = new System.Drawing.Point(410, 21);
            this.lbNameSpace.Name = "lbNameSpace";
            this.lbNameSpace.Size = new System.Drawing.Size(134, 12);
            this.lbNameSpace.TabIndex = 9;
            this.lbNameSpace.Text = "ProjectName (namespace) : ";
            // 
            // btnGenerateInfo
            // 
            this.btnGenerateInfo.Location = new System.Drawing.Point(676, 17);
            this.btnGenerateInfo.Name = "btnGenerateInfo";
            this.btnGenerateInfo.Size = new System.Drawing.Size(242, 88);
            this.btnGenerateInfo.TabIndex = 7;
            this.btnGenerateInfo.Text = "產生Info Class Script";
            this.btnGenerateInfo.UseVisualStyleBackColor = true;
            this.btnGenerateInfo.Click += new System.EventHandler(this.btnGenerateInfo_Click);
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(227, 21);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(50, 12);
            this.lbVersion.TabIndex = 6;
            this.lbVersion.Text = "Version : ";
            // 
            // cbbVersion
            // 
            this.cbbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbVersion.FormattingEnabled = true;
            this.cbbVersion.Items.AddRange(new object[] {
            "Version 1"});
            this.cbbVersion.Location = new System.Drawing.Point(283, 18);
            this.cbbVersion.Name = "cbbVersion";
            this.cbbVersion.Size = new System.Drawing.Size(121, 20);
            this.cbbVersion.TabIndex = 5;
            // 
            // btnLoadTable
            // 
            this.btnLoadTable.Location = new System.Drawing.Point(10, 48);
            this.btnLoadTable.Name = "btnLoadTable";
            this.btnLoadTable.Size = new System.Drawing.Size(213, 57);
            this.btnLoadTable.TabIndex = 2;
            this.btnLoadTable.Text = "載入";
            this.btnLoadTable.UseVisualStyleBackColor = true;
            this.btnLoadTable.Click += new System.EventHandler(this.btnLoadTable_Click);
            // 
            // lbSelectedConnID
            // 
            this.lbSelectedConnID.AutoSize = true;
            this.lbSelectedConnID.Location = new System.Drawing.Point(6, 21);
            this.lbSelectedConnID.Name = "lbSelectedConnID";
            this.lbSelectedConnID.Size = new System.Drawing.Size(88, 12);
            this.lbSelectedConnID.TabIndex = 1;
            this.lbSelectedConnID.Text = "請選擇ConnID : ";
            // 
            // cbbConnID
            // 
            this.cbbConnID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbConnID.FormattingEnabled = true;
            this.cbbConnID.Location = new System.Drawing.Point(100, 18);
            this.cbbConnID.Name = "cbbConnID";
            this.cbbConnID.Size = new System.Drawing.Size(121, 20);
            this.cbbConnID.TabIndex = 0;
            // 
            // PandamanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 810);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PandamanForm";
            this.Text = "PandamanForm";
            this.tabControl1.ResumeLayout(false);
            this.tabConnTest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvConnStrings)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabGenerateInfoScript.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvTableAttributes)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbWriteExplog.ResumeLayout(false);
            this.gbWriteExplog.PerformLayout();
            this.gbIsSuccess.ResumeLayout(false);
            this.gbIsSuccess.PerformLayout();
            this.gbThrowException.ResumeLayout(false);
            this.gbThrowException.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabConnTest;
        private System.Windows.Forms.TabPage tabGenerateInfoScript;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbServerName;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbUID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDBName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbConnID;
        private System.Windows.Forms.Button btnSaveConnID;
        private System.Windows.Forms.Button btnConnTest;
        private System.Windows.Forms.DataGridView gvConnStrings;
        private System.Windows.Forms.DataGridViewButtonColumn ColDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColConnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColServerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDBName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPassword;
        private System.Windows.Forms.Label lbSelectedConnID;
        private System.Windows.Forms.ComboBox cbbConnID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoadTable;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.ComboBox cbbVersion;
        private System.Windows.Forms.Button btnGenerateInfo;
        private System.Windows.Forms.Label lbNameSpace;
        private System.Windows.Forms.CheckedListBox CBLTables;
        private System.Windows.Forms.DataGridView gvTableAttributes;
        private ICSharpCode.TextEditor.TextEditorControl tecInfoScript;
        private System.Windows.Forms.TextBox tbNameSpace;
        private System.Windows.Forms.GroupBox gbThrowException;
        private System.Windows.Forms.RadioButton rbThrowExceptionN;
        private System.Windows.Forms.RadioButton rbThrowExceptionY;
        private System.Windows.Forms.GroupBox gbWriteExplog;
        private System.Windows.Forms.RadioButton rbWriteExplogN;
        private System.Windows.Forms.RadioButton rbWriteExplogY;
        private System.Windows.Forms.GroupBox gbIsSuccess;
        private System.Windows.Forms.RadioButton rbIsSuccessN;
        private System.Windows.Forms.RadioButton rbIsSuccessY;
    }
}