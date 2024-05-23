using ICSharpCode.TextEditor.Document;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TestForm
{
    public partial class PandamanForm : Form
    {
        #region 成員變數
        bool isTest;

        string connectionString;
        string drvConnID;
        string tableName;
        List<string> pkColumns;

        DataTable ConnList;
        DataTable ConnIDList;
        DataTable ColumnsTable;

        internal static readonly Dictionary<SqlDbType, Type> equivalentSystemType = new Dictionary<SqlDbType, Type>
{
    { SqlDbType.BigInt, typeof(long) },
    { SqlDbType.Binary, typeof(byte[]) },
    { SqlDbType.Bit, typeof(bool) },
    { SqlDbType.Char, typeof(string) },
    { SqlDbType.Date, typeof(DateTime) },
    { SqlDbType.DateTime, typeof(DateTime) },
    { SqlDbType.DateTime2, typeof(DateTime) }, // SQL2008+
    { SqlDbType.DateTimeOffset, typeof(DateTimeOffset) }, // SQL2008+
    { SqlDbType.Decimal, typeof(decimal) },
    { SqlDbType.Float, typeof(double) },
    { SqlDbType.Image, typeof(byte[]) },
    { SqlDbType.Int, typeof(int) },
    { SqlDbType.Money, typeof(decimal) },
    { SqlDbType.NChar, typeof(string) },
    { SqlDbType.NVarChar, typeof(string) },
    { SqlDbType.Real, typeof(float) },
    { SqlDbType.SmallDateTime, typeof(DateTime) },
    { SqlDbType.SmallInt, typeof(short) },
    { SqlDbType.SmallMoney, typeof(decimal) },
    { SqlDbType.Time, typeof(TimeSpan) }, // SQL2008+
    { SqlDbType.TinyInt, typeof(byte) },
    { SqlDbType.UniqueIdentifier, typeof(Guid) },
    { SqlDbType.VarBinary, typeof(byte[]) },
    { SqlDbType.VarChar, typeof(string) },
    { SqlDbType.Xml, typeof(SqlXml) },
    { SqlDbType.NText, typeof(string)}
    // omitted special types: timestamp
    // omitted deprecated types: ntext, text
    // not supported by enum: numeric, FILESTREAM, rowversion, sql_variant
};
        #endregion

        public PandamanForm()
        {
            InitializeComponent();
            LoadConnectionStrings();

            isTest = false;
            connectionString = "";
            drvConnID = "";
            tableName = "";
            pkColumns = new List<string>();
            ColumnsTable = new DataTable();
            cbbVersion.SelectedIndex = 0;
        }

        #region Class
        private class connectionStringObj
        {
            public connectionStringObj()
            {
                this._ConnID = "";
                this._ServerName = "";
                this._DBName = "";
                this._UID = "";
                this._Password = "";
            }

            private string _ConnID;
            private string _ServerName;
            private string _DBName;
            private string _UID;
            private string _Password;

            public string ConnID
            {
                get { return _ConnID; }
                set { _ConnID = value; }
            }

            public string ServerName
            {
                get { return _ServerName; }
                set { _ServerName = value; }
            }

            public string DBName
            {
                get { return _DBName; }
                set { _DBName = value; }
            }

            public string UID
            {
                get { return _UID; }
                set { _UID = value; }
            }

            public string Password
            {
                get { return _Password; }
                set { _Password = value; }
            }
        }
        #endregion

        #region 元件Event

        private void btnLoadTable_Click(object sender, EventArgs e)
        {
            CBLTables.ClearSelected();

            DataRowView drv = (DataRowView)cbbConnID.SelectedItem;
            drvConnID = drv.Row["ConnID"].ToString();
            connectionString = string.Empty;

            foreach (DataRow dr in ConnList.Rows)
            {
                if (dr["ConnID"].ToString() == drvConnID)
                {
                    connectionString = "Data Source=" + dr["ServerName"].ToString() + ";Initial Catalog=" + dr["DBName"].ToString() + ";User ID=" + dr["UID"].ToString() + ";Password=" + dr["Password"].ToString() + ";";

                    Database db = new SqlDatabase(connectionString);
                    StringBuilder sbCmd = new StringBuilder();

                    sbCmd.Append("	SELECT * FROM INFORMATION_SCHEMA.TABLES WITH (Nolock) ");
                    sbCmd.Append("	WHERE (1=1) ");
                    sbCmd.Append("		AND TABLE_TYPE = 'BASE TABLE' 		");
                    sbCmd.Append("		ORDER BY TABLE_NAME 		");

                    DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

                    #region Add In Parameter

                    //db.AddInParameter(dbCommand, "@ApplyID", DbType.String, this._ApplyID);

                    #endregion

                    try
                    {
                        DataTable dt = db.ExecuteDataSet(dbCommand).Tables[0];

                        ((ListBox)CBLTables).DataSource = dt;
                        ((ListBox)CBLTables).DisplayMember = "TABLE_NAME";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Table清單讀取錯誤，原因：" + ex.Message);
                    }
                }
            }
        }

        private void btnConnTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbConnID.Text.Trim()))
            {
                MessageBox.Show("ConnID不可為空");
                return;
            }
            if (string.IsNullOrEmpty(tbServerName.Text.Trim()))
            {
                MessageBox.Show("ServerName不可為空");
                return;
            }
            if (string.IsNullOrEmpty(tbDBName.Text.Trim()))
            {
                MessageBox.Show("DBName不可為空");
                return;
            }
            if (string.IsNullOrEmpty(tbUID.Text.Trim()))
            {
                MessageBox.Show("UID不可為空");
                return;
            }
            if (string.IsNullOrEmpty(tbPassword.Text.Trim()))
            {
                MessageBox.Show("Password不可為空");
                return;
            }

            foreach (DataRow dr in ConnList.Rows)
            {
                if (tbConnID.Text == dr["ConnID"].ToString() || tbDBName.Text == dr["DBName"].ToString())
                {
                    MessageBox.Show("連線字串重複！");
                    return;
                }
            }

            connectionString = "Data Source=" + tbServerName.Text + ";Initial Catalog=" + tbDBName.Text + ";User ID=" + tbUID.Text + ";Password=" + tbPassword.Text + ";";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("測試成功");
                    isTest = true;
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("測試失敗，原因：" + ex.Message);
                }
            }
        }

        private void btnSaveConnID_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("ConnID為Script產生時所需之連線字串名稱，請再次確認是否正確？", "", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes)
                return;

            if (!isTest)
            {
                MessageBox.Show("請先進行連線測試！");
                return;
            }

            connectionStringObj connection = new connectionStringObj();

            connection.ConnID = tbConnID.Text.ToUpper();
            connection.ServerName = tbServerName.Text;
            connection.DBName = tbDBName.Text;
            connection.UID = tbUID.Text;
            connection.Password = tbPassword.Text;

            //寫入
            string connJsonString = JsonConvert.SerializeObject(connection);
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\ConnectionStrings.json", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(connJsonString);
            sw.Flush();
            sw.Close();
            fs.Close();

            //讀取
            LoadConnectionStrings();

            isTest = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                if (e.RowIndex >= ConnList.Rows.Count)
                {
                    return;
                }

                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\ConnectionStrings.json");

                ConnList.Rows.RemoveAt(e.RowIndex);

                List<string> connTempList = new List<string>();
                foreach (DataRow dr in ConnList.Rows)
                {
                    connectionStringObj conn = new connectionStringObj();
                    conn.ConnID = dr["ConnID"].ToString();
                    conn.ServerName = dr["ServerName"].ToString();
                    conn.DBName = dr["DBName"].ToString();
                    conn.UID = dr["UID"].ToString();
                    conn.Password = dr["Password"].ToString();

                    string connJsonString = JsonConvert.SerializeObject(conn);

                    connTempList.Add(connJsonString);
                }

                File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\ConnectionStrings.json", connTempList.ToArray());
                //讀取
                LoadConnectionStrings();
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Name == tabGenerateInfoScript.Name)
            {
                LoadJsonConnID();
            }
        }

        private void CBLTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((DataRowView)CBLTables.SelectedItem == null)
                return;
            DataRowView drv = (DataRowView)CBLTables.SelectedItem;
            string drvTableName = drv.Row["TABLE_NAME"].ToString();
            pkColumns = new List<string>();

            Database db = new SqlDatabase(connectionString);
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.AppendLine("SELECT IC.TABLE_NAME AS TableName,");
            sbCmd.AppendLine("       IC.COLUMN_NAME AS ColumnName,");
            sbCmd.AppendLine("       IC.ORDINAL_POSITION AS SN,");
            sbCmd.AppendLine("       CASE WHEN PK.COLUMN_NAME = IC.COLUMN_NAME THEN 'Y' ELSE 'N' END isPK,");
            sbCmd.AppendLine("       IC.DATA_TYPE AS DataType,");
            sbCmd.AppendLine("       IC.CHARACTER_MAXIMUM_LENGTH AS Length,");
            sbCmd.AppendLine("       IC.IS_NULLABLE AS isNullable,");
            sbCmd.AppendLine("       Identity_Description.Description,");
            sbCmd.AppendLine("       CASE WHEN Identity_Description.isIdentity = 0 THEN 'N' WHEN Identity_Description.isIdentity = 1 THEN 'Y' END isIdentity");
            sbCmd.AppendLine("       FROM INFORMATION_SCHEMA.COLUMNS AS IC WITH (Nolock) ");

            sbCmd.AppendLine("       LEFT JOIN ");
            sbCmd.AppendLine("       (SELECT ST.name [Table],");
            sbCmd.AppendLine("       SC.name [Column], ");
            sbCmd.AppendLine("       SC.is_identity [isIdentity], ");
            sbCmd.AppendLine("       SEP.value [Description] ");
            sbCmd.AppendLine("       FROM sys.tables ST ");
            sbCmd.AppendLine("       INNER JOIN sys.columns SC on ST.object_id = SC.object_id ");
            sbCmd.AppendLine("       LEFT JOIN sys.extended_properties SEP on ST.object_id = SEP.major_id ");
            sbCmd.AppendLine("       AND SC.column_id = SEP.minor_id ");
            sbCmd.AppendLine("       AND SEP.name = 'MS_Description' ");
            sbCmd.AppendLine("       WHERE ST.name = @TABLE_NAME) AS Identity_Description  ");
            sbCmd.AppendLine("       ON Identity_Description.[Column] = IC.COLUMN_NAME ");

            sbCmd.AppendLine("       LEFT JOIN ");
            sbCmd.AppendLine("       (SELECT ICC.TABLE_NAME,");
            sbCmd.AppendLine("       ICC.COLUMN_NAME ");
            sbCmd.AppendLine("       FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS ITC ");
            sbCmd.AppendLine("       JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS ICC ");
            sbCmd.AppendLine("       ON ITC.CONSTRAINT_NAME = ICC.CONSTRAINT_NAME ");
            sbCmd.AppendLine("       WHERE ITC.CONSTRAINT_TYPE = 'PRIMARY KEY'  ");
            sbCmd.AppendLine("             AND ICC.TABLE_NAME = @TABLE_NAME) AS PK");
            sbCmd.AppendLine("       ON PK.[COLUMN_NAME] = Identity_Description.[Column] ");

            sbCmd.AppendLine("WHERE (1=1) ");
            sbCmd.AppendLine("	     AND IC.TABLE_NAME = @TABLE_NAME 		");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter

            db.AddInParameter(dbCommand, "@TABLE_NAME", DbType.String, drvTableName);

            #endregion

            try
            {
                DataTable dt = db.ExecuteDataSet(dbCommand).Tables[0];
                dt.TableName = dt.Rows[0]["TableName"].ToString();
                gvTableAttributes.DataSource = dt;
                ColumnsTable = dt;
                tableName = drvTableName;

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    if (dr["isPK"].ToString() == "Y")
                    {
                        pkColumns.Add(dr["ColumnName"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Table清單讀取錯誤，原因：" + ex.Message);
            }
        }

        private void tecInfoScript_Load(object sender, EventArgs e)
        {
            tecInfoScript.Encoding = System.Text.Encoding.UTF8;
            FileSyntaxModeProvider fsmp;

            string pathDebug = AppDomain.CurrentDomain.BaseDirectory + "../../Highlighting";
            string pathNormal = AppDomain.CurrentDomain.BaseDirectory + "./Highlighting";
            if (Directory.Exists(pathDebug))
            {
                fsmp = new FileSyntaxModeProvider(pathDebug);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp);
                tecInfoScript.SetHighlighting("C#");
            }
            else if (Directory.Exists(pathNormal))
            {
                fsmp = new FileSyntaxModeProvider(pathNormal);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp);
                tecInfoScript.SetHighlighting("C#");
            }
        }

        private void btnGenerateInfo_Click(object sender, EventArgs e)
        {
            if (ColumnsTable.Rows.Count == 0)
            {
                MessageBox.Show("請先載入DB並點選Table");
                return;
            }

            if (cbbVersion.SelectedIndex == 0)
            {

                #region V1
                RadioButton checkedButtonThrowException = gbThrowException.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                RadioButton checkedButtonIsSuccess = gbIsSuccess.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                RadioButton checkedButtonWriteExplog = gbWriteExplog.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

                StringBuilder infoScript = new StringBuilder();

                infoScript.AppendLine("using System;");
                infoScript.AppendLine("using System.Collections.Generic;");
                infoScript.AppendLine("using System.Data;");
                infoScript.AppendLine("using System.Data.Common;");
                infoScript.AppendLine("using System.Text;");
                infoScript.AppendLine("using System.Xml;");
                infoScript.AppendLine("using Microsoft.Practices.EnterpriseLibrary.Data;");
                infoScript.AppendLine("using System.Configuration;");
                infoScript.AppendLine("");
                infoScript.AppendLine("namespace " + tbNameSpace.Text);
                infoScript.AppendLine("{");
                infoScript.AppendLine("    public partial class " + tableName + "Info : baseDB");
                infoScript.AppendLine("    {");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// Constructors");
                infoScript.AppendLine("        /// </summary>		");
                infoScript.AppendLine("        public " + tableName + "Info()");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            base.DBInstanceName = \"" + drvConnID + "\"; //SEC Connection ID");
                infoScript.AppendLine("            this.Init();");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("        #region Init");
                infoScript.AppendLine("        private void Init()");
                infoScript.AppendLine("        {");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("        	this._" + String.Format("{0,-60}", dr["ColumnName"].ToString() + wording[0].ToString() + ";") + "//" + dr["Description"].ToString().Replace("\r\n", "\t"));
                }

                infoScript.AppendLine("        }");
                infoScript.AppendLine("        #endregion");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("        #region Private Properties");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("        	private " + wording[1].ToString() + " _" + dr["ColumnName"].ToString() + ";");
                }

                infoScript.AppendLine("        #endregion");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("        #region Public Properties");
                infoScript.AppendLine("        ");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("        /// <summary>");
                    infoScript.AppendLine("        /// " + dr["Description"].ToString().Replace("\r\n", "\t"));
                    infoScript.AppendLine("        /// </summary>");
                    infoScript.AppendLine("        public " + wording[1] + " " + dr["ColumnName"].ToString());
                    infoScript.AppendLine("        {");
                    infoScript.AppendLine("        	get { return _" + dr["ColumnName"].ToString() + "; }");
                    infoScript.AppendLine("        	set { _" + dr["ColumnName"].ToString() + " = value; }");
                    infoScript.AppendLine("        }");
                }
                infoScript.AppendLine("        #endregion");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("");
                infoScript.AppendLine("        #region Methods");
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                infoScript.AppendLine("");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// 依據PK載入一筆資料");
                infoScript.AppendLine("        /// </summary>");
                infoScript.AppendLine("        /// <returns>true代表成功載入，false代表找不到任何資料</returns>");
                infoScript.Append("        public bool Load(");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dr["isPK"].ToString() == "Y")
                    {
                        if (ColumnsTable.Rows.IndexOf(dr) == pkColumns.Count - 1)
                        {
                            infoScript.Append(wording[1] + " i" + dr["ColumnName"].ToString());
                        }
                        else
                        {
                            infoScript.Append(wording[1] + " i" + dr["ColumnName"].ToString() + ", ");
                        }
                    }
                }

                infoScript.AppendLine(")");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            bool Result = false;");
                infoScript.AppendLine("");
                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("            this._" + pkColumn + " = i" + pkColumn + ";");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            Database db = base.GetDatabase();");
                infoScript.AppendLine("            StringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("            sbCmd.Append(\"   SELECT * FROM [" + tableName + "] WITH (Nolock) \");");
                infoScript.AppendLine("            sbCmd.Append(\"   WHERE(1 = 1) \");");
                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("            sbCmd.Append(\"       AND " + pkColumn + " = @" + pkColumn + "      \");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("            #region Add In Parameter");
                infoScript.AppendLine("");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dr["isPK"].ToString() == "Y")
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            #endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("            try");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                DataTable dtTemp = db.ExecuteDataSet(dbCommand).Tables[0];");
                infoScript.AppendLine("                if (dtTemp.Rows.Count == 0)");
                infoScript.AppendLine("                {");
                infoScript.AppendLine("                    base.EditMode = EditType.Insert;");
                infoScript.AppendLine("                    Result = false;");
                infoScript.AppendLine("                }");
                infoScript.AppendLine("                else");
                infoScript.AppendLine("                {");
                infoScript.AppendLine("                    base.EditMode = EditType.Update;");
                infoScript.AppendLine("                    Result = true;");
                infoScript.AppendLine("");
                infoScript.AppendLine("                    DataRow dr = dtTemp.Rows[0];");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("\t\t\t\t\t" + wording[3]);
                }
                infoScript.AppendLine("                }");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = true;");
                else
                    infoScript.AppendLine("                base.ErrFlag = true;");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("            catch (Exception ex)");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                #region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)");
                infoScript.AppendLine("                //取得目前MethodName");
                infoScript.AppendLine("                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();");
                infoScript.AppendLine("                System.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();");
                infoScript.AppendLine("");
                infoScript.AppendLine("                Result = false;");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = false;");
                else
                    infoScript.AppendLine("                base.ErrFlag = false;");
                infoScript.AppendLine("                base.ErrMsg = ex.ToString();");
                infoScript.AppendLine("                base.ErrMethodName = myMethodBase.Name.ToString();");
                if (checkedButtonWriteExplog.Name == "rbWriteExplogY")
                {
                    infoScript.AppendLine("                StringBuilder sbParameter = new StringBuilder();");
                    infoScript.AppendLine("                foreach (DbParameter Value in dbCommand.Parameters)");
                    infoScript.AppendLine("                {");
                    infoScript.AppendLine("                    sbParameter.AppendLine(\"declare \" + Value.ParameterName + \" nvarchar(500) set \" + Value.ParameterName  + \" = '\" + Convert.ToString(Value.Value) + \"'\");");
                    infoScript.AppendLine("                }");
                    infoScript.AppendLine("                base.ErrDbCommand = sbParameter.ToString() + dbCommand.CommandText;");
                }
                infoScript.AppendLine("                base.LogExpInf();");
                infoScript.AppendLine("                #endregion");
                infoScript.AppendLine("");
                if (checkedButtonThrowException.Name == "rbThrowExceptionY")
                    infoScript.AppendLine("                throw; //將原來的 exception 再次的抛出去");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("");
                infoScript.AppendLine("            return Result;");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// Insert");
                infoScript.AppendLine("        /// </summary>");
                infoScript.AppendLine("        public void Insert()");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            Database db = base.GetDatabase();");
                infoScript.AppendLine("            StringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("            sbCmd.Append(\"	INSERT INTO [" + tableName + "]		\");");
                infoScript.AppendLine("            sbCmd.Append(\"		(				\");");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    if (ColumnsTable.Rows.IndexOf(dr) == 0)
                        infoScript.AppendLine("            sbCmd.Append(\"		" + dr["ColumnName"].ToString() + "		\");");
                    else
                        infoScript.AppendLine("            sbCmd.Append(\"		," + dr["ColumnName"].ToString() + "		\");");
                }
                infoScript.AppendLine("            sbCmd.Append(\"		)				\");");
                infoScript.AppendLine("            sbCmd.Append(\"	VALUES		\");");
                infoScript.AppendLine("            sbCmd.Append(\"		(				\");");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    if (ColumnsTable.Rows.IndexOf(dr) == 0)
                        infoScript.AppendLine("            sbCmd.Append(\"		@" + dr["ColumnName"].ToString() + "		\");");
                    else
                        infoScript.AppendLine("            sbCmd.Append(\"		,@" + dr["ColumnName"].ToString() + "		\");");
                }
                infoScript.AppendLine("            sbCmd.Append(\"		)				\");");
                infoScript.AppendLine("");
                infoScript.AppendLine("            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("            #region Add In Parameter");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (wording[1] == "XmlDocument")
                    {
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType.Xml, this._" + dr["ColumnName"].ToString() + ".OuterXml);");
                    }
                    else
                    {
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                    }
                }
                infoScript.AppendLine("            #endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("            try");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                db.ExecuteNonQuery(dbCommand);");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = true;");
                else
                    infoScript.AppendLine("                base.ErrFlag = false;");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("            catch (Exception ex)");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                #region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)");
                infoScript.AppendLine("                //取得目前MethodName");
                infoScript.AppendLine("                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();");
                infoScript.AppendLine("                System.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();");
                infoScript.AppendLine("");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = false;");
                else
                    infoScript.AppendLine("                base.ErrFlag = false;");
                infoScript.AppendLine("                base.ErrMsg = ex.ToString();");
                infoScript.AppendLine("                base.ErrMethodName = myMethodBase.Name.ToString();");
                if (checkedButtonWriteExplog.Name == "rbWriteExplogY")
                {
                    infoScript.AppendLine("                StringBuilder sbParameter = new StringBuilder();");
                    infoScript.AppendLine("                foreach (DbParameter Value in dbCommand.Parameters)");
                    infoScript.AppendLine("                {");
                    infoScript.AppendLine("                    sbParameter.AppendLine(\"declare \" + Value.ParameterName + \" nvarchar(500) set \" + Value.ParameterName  + \" = '\" + Convert.ToString(Value.Value) + \"'\");");
                    infoScript.AppendLine("                }");
                    infoScript.AppendLine("                base.ErrDbCommand = sbParameter.ToString() + dbCommand.CommandText;");
                }
                infoScript.AppendLine("                base.LogExpInf();");
                infoScript.AppendLine("                #endregion");
                infoScript.AppendLine("");
                if (checkedButtonThrowException.Name == "rbThrowExceptionY")
                    infoScript.AppendLine("                throw; //將原來的 exception 再次的抛出去");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// Update");
                infoScript.AppendLine("        /// </summary>");
                infoScript.AppendLine("        public void Update()");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            Database db = base.GetDatabase();");
                infoScript.AppendLine("            StringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("            sbCmd.Append(\"	UPDATE [" + tableName + "] SET 		\");");
                DataTable dt = new DataTable();
                dt = ColumnsTable.Copy();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["isPK"].ToString() == "Y")
                    {
                        dr.Delete();
                    }
                }
                dt.AcceptChanges();
                foreach (DataRow dr in dt.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dt.Rows.IndexOf(dr) == 0)
                        infoScript.AppendLine("            sbCmd.Append(\"		" + dr["ColumnName"].ToString() + " = @" + dr["ColumnName"].ToString() + " 		\");");
                    else
                        infoScript.AppendLine("            sbCmd.Append(\"		," + dr["ColumnName"].ToString() + " = @" + dr["ColumnName"].ToString() + " 		\");");
                }
                infoScript.AppendLine("            sbCmd.Append(\"	WHERE (1=1) \");");
                foreach (string ColumnName in pkColumns)
                {
                    infoScript.AppendLine("            sbCmd.Append(\"		AND " + ColumnName + " = @" + ColumnName + " 		\");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("            #region Add In Parameter");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (wording[1] == "XmlDocument")
                    {
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType.Xml, this._" + dr["ColumnName"].ToString() + ".OuterXml);");
                    }
                    else
                    {
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                    }
                }
                infoScript.AppendLine("            #endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("            try");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                db.ExecuteNonQuery(dbCommand);");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = true;");
                else
                    infoScript.AppendLine("                base.ErrFlag = false;");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("            catch (Exception ex)");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                #region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)");
                infoScript.AppendLine("                //取得目前MethodName");
                infoScript.AppendLine("                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();");
                infoScript.AppendLine("                System.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();");
                infoScript.AppendLine("");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = false;");
                else
                    infoScript.AppendLine("                base.ErrFlag = false;");
                infoScript.AppendLine("                base.ErrMsg = ex.ToString();");
                infoScript.AppendLine("                base.ErrMethodName = myMethodBase.Name.ToString();");
                if (checkedButtonWriteExplog.Name == "rbWriteExplogY")
                {
                    infoScript.AppendLine("                StringBuilder sbParameter = new StringBuilder();");
                    infoScript.AppendLine("                foreach (DbParameter Value in dbCommand.Parameters)");
                    infoScript.AppendLine("                {");
                    infoScript.AppendLine("                    sbParameter.AppendLine(\"declare \" + Value.ParameterName + \" nvarchar(500) set \" + Value.ParameterName  + \" = '\" + Convert.ToString(Value.Value) + \"'\");");
                    infoScript.AppendLine("                }");
                    infoScript.AppendLine("                base.ErrDbCommand = sbParameter.ToString() + dbCommand.CommandText;");
                }
                infoScript.AppendLine("                base.LogExpInf();");
                infoScript.AppendLine("                #endregion");
                infoScript.AppendLine("");
                if (checkedButtonThrowException.Name == "rbThrowExceptionY")
                    infoScript.AppendLine("                throw; //將原來的 exception 再次的抛出去");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// Delete");
                infoScript.AppendLine("        /// </summary>");
                infoScript.Append("        public void Delete(");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dr["isPK"].ToString() == "Y")
                    {
                        if (ColumnsTable.Rows.IndexOf(dr) == pkColumns.Count - 1)
                        {
                            infoScript.Append(wording[1] + " i" + dr["ColumnName"].ToString());
                        }
                        else
                        {
                            infoScript.Append(wording[1] + " i" + dr["ColumnName"].ToString() + ", ");
                        }
                    }
                }

                infoScript.AppendLine(")");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            Database db = base.GetDatabase();");
                infoScript.AppendLine("            StringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");

                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("            this._" + pkColumn + " = i" + pkColumn + ";");
                }

                infoScript.AppendLine("");
                infoScript.AppendLine("            sbCmd.Append(\"	DELETE [" + tableName + "]		\");");
                infoScript.AppendLine("            sbCmd.Append(\"	WHERE (1=1) 		\");");
                foreach (string ColumnName in pkColumns)
                {
                    infoScript.AppendLine("            sbCmd.Append(\"		AND " + ColumnName + " = @" + ColumnName + " 		\");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("            #region Add In Parameter");
                infoScript.AppendLine("");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dr["isPK"].ToString() == "Y")
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            #endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("            try");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                db.ExecuteNonQuery(dbCommand);");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = true;");
                else
                    infoScript.AppendLine("                base.ErrFlag = false;");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("            catch (Exception ex)");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                #region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)");
                infoScript.AppendLine("                //取得目前MethodName");
                infoScript.AppendLine("                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();");
                infoScript.AppendLine("                System.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();");
                infoScript.AppendLine("");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = false;");
                else
                    infoScript.AppendLine("                base.ErrFlag = false;");
                infoScript.AppendLine("                base.ErrMsg = ex.ToString();");
                infoScript.AppendLine("                base.ErrMethodName = myMethodBase.Name.ToString();");
                if (checkedButtonWriteExplog.Name == "rbWriteExplogY")
                {
                    infoScript.AppendLine("                StringBuilder sbParameter = new StringBuilder();");
                    infoScript.AppendLine("                foreach (DbParameter Value in dbCommand.Parameters)");
                    infoScript.AppendLine("                {");
                    infoScript.AppendLine("                    sbParameter.AppendLine(\"declare \" + Value.ParameterName + \" nvarchar(500) set \" + Value.ParameterName  + \" = '\" + Convert.ToString(Value.Value) + \"'\");");
                    infoScript.AppendLine("                }");
                    infoScript.AppendLine("                base.ErrDbCommand = sbParameter.ToString() + dbCommand.CommandText;");
                }
                infoScript.AppendLine("                base.LogExpInf();");
                infoScript.AppendLine("                #endregion");
                infoScript.AppendLine("");
                if (checkedButtonThrowException.Name == "rbThrowExceptionY")
                    infoScript.AppendLine("                throw; //將原來的 exception 再次的抛出去");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// Save用法： 1. Info.Load() 2. Set Value 3. Info.Save()");
                infoScript.AppendLine("        /// </summary>	");
                infoScript.AppendLine("        public void Save()");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            if (base.EditMode == EditType.Insert)");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                this.Insert();");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("            else");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                this.Update();");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("        #endregion");
                infoScript.AppendLine("    }");
                infoScript.AppendLine("}");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("#region Use Sample");
                infoScript.AppendLine("/*");
                infoScript.AppendLine("Vista.Information." + tableName + "Info Info = new Vista.Information." + tableName + "Info();");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("Info." + String.Format("{0,-50}", dr["ColumnName"].ToString() + wording[0].ToString() + ";") + "//" + dr["Description"].ToString().Replace("\r\n", "\t"));
                }
                infoScript.AppendLine("*/");
                infoScript.AppendLine("#endregion");
                infoScript.AppendLine("");
                #endregion

                tecInfoScript.Text = infoScript.ToString();
            }
            else if (cbbVersion.SelectedIndex == 1)
            {

                #region V2
                RadioButton checkedButtonThrowException = gbThrowException.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                RadioButton checkedButtonIsSuccess = gbIsSuccess.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                RadioButton checkedButtonWriteExplog = gbWriteExplog.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

                StringBuilder infoScript = new StringBuilder();

                infoScript.AppendLine("using System;");
                infoScript.AppendLine("using System.Collections.Generic;");
                infoScript.AppendLine("using System.Data;");
                infoScript.AppendLine("using System.Data.Common;");
                infoScript.AppendLine("using System.Text;");
                infoScript.AppendLine("using System.Xml;");
                infoScript.AppendLine("using System.Configuration;");
                infoScript.AppendLine("");
                infoScript.AppendLine("namespace " + tbNameSpace.Text);
                infoScript.AppendLine("{");
                infoScript.AppendLine("    public partial class " + tableName + "Info");
                infoScript.AppendLine("    {");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// Constructors");
                infoScript.AppendLine("        /// </summary>		");
                infoScript.AppendLine("        public " + tableName + "Info()");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            this.Init();");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("        #region Init");
                infoScript.AppendLine("        private void Init()");
                infoScript.AppendLine("        {");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("        	this._" + String.Format("{0,-60}", dr["ColumnName"].ToString() + wording[0].ToString() + ";") + "//" + dr["Description"].ToString().Replace("\r\n", "\t"));
                }

                infoScript.AppendLine("        }");
                infoScript.AppendLine("        #endregion");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("        #region Private Properties");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("        	private " + wording[1].ToString() + " _" + dr["ColumnName"].ToString() + ";");
                }

                infoScript.AppendLine("        #endregion");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("        #region Public Properties");
                infoScript.AppendLine("        ");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("        /// <summary>");
                    infoScript.AppendLine("        /// " + dr["Description"].ToString().Replace("\r\n", "\t"));
                    infoScript.AppendLine("        /// </summary>");
                    infoScript.AppendLine("        public " + wording[1] + " " + dr["ColumnName"].ToString());
                    infoScript.AppendLine("        {");
                    infoScript.AppendLine("        	get { return _" + dr["ColumnName"].ToString() + "; }");
                    infoScript.AppendLine("        	set { _" + dr["ColumnName"].ToString() + " = value; }");
                    infoScript.AppendLine("        }");
                }
                infoScript.AppendLine("        #endregion");
                infoScript.AppendLine("        ");
                infoScript.AppendLine("");
                infoScript.AppendLine("        #region Methods");
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                infoScript.AppendLine("");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// 依據PK載入一筆資料");
                infoScript.AppendLine("        /// </summary>");
                infoScript.AppendLine("        /// <returns>true代表成功載入，false代表找不到任何資料</returns>");
                infoScript.Append("        public bool Load(");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dr["isPK"].ToString() == "Y")
                    {
                        if (ColumnsTable.Rows.IndexOf(dr) == pkColumns.Count - 1)
                        {
                            infoScript.Append(wording[1] + " i" + dr["ColumnName"].ToString());
                        }
                        else
                        {
                            infoScript.Append(wording[1] + " i" + dr["ColumnName"].ToString() + ", ");
                        }
                    }
                }

                infoScript.AppendLine(")");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            bool Result = false;");
                infoScript.AppendLine("");
                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("            this._" + pkColumn + " = i" + pkColumn + ";");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            Database db = base.GetDatabase();");
                infoScript.AppendLine("            StringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("            sbCmd.Append(\"   SELECT * FROM [" + tableName + "] WITH (Nolock) \");");
                infoScript.AppendLine("            sbCmd.Append(\"   WHERE(1 = 1) \");");
                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("            sbCmd.Append(\"       AND " + pkColumn + " = @" + pkColumn + "      \");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("            #region Add In Parameter");
                infoScript.AppendLine("");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dr["isPK"].ToString() == "Y")
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            #endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("            try");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                DataTable dtTemp = db.ExecuteDataSet(dbCommand).Tables[0];");
                infoScript.AppendLine("                if (dtTemp.Rows.Count == 0)");
                infoScript.AppendLine("                {");
                infoScript.AppendLine("                    base.EditMode = EditType.Insert;");
                infoScript.AppendLine("                    Result = false;");
                infoScript.AppendLine("                }");
                infoScript.AppendLine("                else");
                infoScript.AppendLine("                {");
                infoScript.AppendLine("                    base.EditMode = EditType.Update;");
                infoScript.AppendLine("                    Result = true;");
                infoScript.AppendLine("");
                infoScript.AppendLine("                    DataRow dr = dtTemp.Rows[0];");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("\t\t\t\t\t" + wording[3]);
                }
                infoScript.AppendLine("                }");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = true;");
                else
                    infoScript.AppendLine("                base.ErrFlag = true;");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("            catch (Exception ex)");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("");
                infoScript.AppendLine("            return Result;");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// Insert");
                infoScript.AppendLine("        /// </summary>");
                infoScript.AppendLine("        public void Insert()");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            Database db = base.GetDatabase();");
                infoScript.AppendLine("            StringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("            sbCmd.Append(\"	INSERT INTO [" + tableName + "]		\");");
                infoScript.AppendLine("            sbCmd.Append(\"		(				\");");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    if (ColumnsTable.Rows.IndexOf(dr) == 0)
                        infoScript.AppendLine("            sbCmd.Append(\"		" + dr["ColumnName"].ToString() + "		\");");
                    else
                        infoScript.AppendLine("            sbCmd.Append(\"		," + dr["ColumnName"].ToString() + "		\");");
                }
                infoScript.AppendLine("            sbCmd.Append(\"		)				\");");
                infoScript.AppendLine("            sbCmd.Append(\"	VALUES		\");");
                infoScript.AppendLine("            sbCmd.Append(\"		(				\");");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    if (ColumnsTable.Rows.IndexOf(dr) == 0)
                        infoScript.AppendLine("            sbCmd.Append(\"		@" + dr["ColumnName"].ToString() + "		\");");
                    else
                        infoScript.AppendLine("            sbCmd.Append(\"		,@" + dr["ColumnName"].ToString() + "		\");");
                }
                infoScript.AppendLine("            sbCmd.Append(\"		)				\");");
                infoScript.AppendLine("");
                infoScript.AppendLine("            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("            #region Add In Parameter");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (wording[1] == "XmlDocument")
                    {
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType.Xml, this._" + dr["ColumnName"].ToString() + ".OuterXml);");
                    }
                    else
                    {
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                    }
                }
                infoScript.AppendLine("            #endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("            try");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                db.ExecuteNonQuery(dbCommand);");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = true;");
                else
                    infoScript.AppendLine("                base.ErrFlag = false;");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("            catch (Exception ex)");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// Update");
                infoScript.AppendLine("        /// </summary>");
                infoScript.AppendLine("        public void Update()");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            Database db = base.GetDatabase();");
                infoScript.AppendLine("            StringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("            sbCmd.Append(\"	UPDATE [" + tableName + "] SET 		\");");
                DataTable dt = new DataTable();
                dt = ColumnsTable.Copy();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["isPK"].ToString() == "Y")
                    {
                        dr.Delete();
                    }
                }
                dt.AcceptChanges();
                foreach (DataRow dr in dt.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dt.Rows.IndexOf(dr) == 0)
                        infoScript.AppendLine("            sbCmd.Append(\"		" + dr["ColumnName"].ToString() + " = @" + dr["ColumnName"].ToString() + " 		\");");
                    else
                        infoScript.AppendLine("            sbCmd.Append(\"		," + dr["ColumnName"].ToString() + " = @" + dr["ColumnName"].ToString() + " 		\");");
                }
                infoScript.AppendLine("            sbCmd.Append(\"	WHERE (1=1) \");");
                foreach (string ColumnName in pkColumns)
                {
                    infoScript.AppendLine("            sbCmd.Append(\"		AND " + ColumnName + " = @" + ColumnName + " 		\");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("            #region Add In Parameter");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (wording[1] == "XmlDocument")
                    {
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType.Xml, this._" + dr["ColumnName"].ToString() + ".OuterXml);");
                    }
                    else
                    {
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                    }
                }
                infoScript.AppendLine("            #endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("            try");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                db.ExecuteNonQuery(dbCommand);");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = true;");
                else
                    infoScript.AppendLine("                base.ErrFlag = false;");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("            catch (Exception ex)");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// Delete");
                infoScript.AppendLine("        /// </summary>");
                infoScript.Append("        public void Delete(");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dr["isPK"].ToString() == "Y")
                    {
                        if (ColumnsTable.Rows.IndexOf(dr) == pkColumns.Count - 1)
                        {
                            infoScript.Append(wording[1] + " i" + dr["ColumnName"].ToString());
                        }
                        else
                        {
                            infoScript.Append(wording[1] + " i" + dr["ColumnName"].ToString() + ", ");
                        }
                    }
                }

                infoScript.AppendLine(")");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            Database db = base.GetDatabase();");
                infoScript.AppendLine("            StringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");

                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("            this._" + pkColumn + " = i" + pkColumn + ";");
                }

                infoScript.AppendLine("");
                infoScript.AppendLine("            sbCmd.Append(\"	DELETE [" + tableName + "]		\");");
                infoScript.AppendLine("            sbCmd.Append(\"	WHERE (1=1) 		\");");
                foreach (string ColumnName in pkColumns)
                {
                    infoScript.AppendLine("            sbCmd.Append(\"		AND " + ColumnName + " = @" + ColumnName + " 		\");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("            #region Add In Parameter");
                infoScript.AppendLine("");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dr["isPK"].ToString() == "Y")
                        infoScript.AppendLine("            db.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("            #endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("            try");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                db.ExecuteNonQuery(dbCommand);");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("                base.IsSuccess = true;");
                else
                    infoScript.AppendLine("                base.ErrFlag = false;");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("            catch (Exception ex)");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("        /// <summary>");
                infoScript.AppendLine("        /// Save用法： 1. Info.Load() 2. Set Value 3. Info.Save()");
                infoScript.AppendLine("        /// </summary>	");
                infoScript.AppendLine("        public void Save()");
                infoScript.AppendLine("        {");
                infoScript.AppendLine("            if (base.EditMode == EditType.Insert)");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                this.Insert();");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("            else");
                infoScript.AppendLine("            {");
                infoScript.AppendLine("                this.Update();");
                infoScript.AppendLine("            }");
                infoScript.AppendLine("        }");
                infoScript.AppendLine("");
                infoScript.AppendLine("        #endregion");
                infoScript.AppendLine("    }");
                infoScript.AppendLine("}");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("#region Use Sample");
                infoScript.AppendLine("/*");
                infoScript.AppendLine("Vista.Information." + tableName + "Info Info = new Vista.Information." + tableName + "Info();");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("Info." + String.Format("{0,-50}", dr["ColumnName"].ToString() + wording[0].ToString() + ";") + "//" + dr["Description"].ToString().Replace("\r\n", "\t"));
                }
                infoScript.AppendLine("*/");
                infoScript.AppendLine("#endregion");
                infoScript.AppendLine("");
                #endregion

                tecInfoScript.Text = infoScript.ToString();
            }
        }
        #endregion

        #region Functions

        private void LoadConnectionStrings()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\ConnectionStrings.json"))
            {
                using (FileStream fs = File.Create(AppDomain.CurrentDomain.BaseDirectory + @"\ConnectionStrings.json"))
                {

                }
            }

            ConnList = new DataTable();

            ConnList.Columns.Add("ConnID");
            ConnList.Columns.Add("ServerName");
            ConnList.Columns.Add("DBName");
            ConnList.Columns.Add("UID");
            ConnList.Columns.Add("Password");


            string[] connJsonStrings = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\ConnectionStrings.json");

            foreach (string connJson in connJsonStrings)
            {
                connectionStringObj connectionStringObject = JsonConvert.DeserializeObject<connectionStringObj>(connJson);

                DataRow dr = ConnList.NewRow();
                dr["ConnID"] = connectionStringObject.ConnID;
                dr["ServerName"] = connectionStringObject.ServerName;
                dr["DBName"] = connectionStringObject.DBName;
                dr["UID"] = connectionStringObject.UID;
                dr["Password"] = connectionStringObject.Password;
                ConnList.Rows.Add(dr);
            }

            gvConnStrings.DataSource = ConnList;
        }

        private void LoadJsonConnID()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\ConnectionStrings.json"))
            {
                using (FileStream fs = File.Create(AppDomain.CurrentDomain.BaseDirectory + @"\ConnectionStrings.json"))
                {

                }
            }

            ConnIDList = new DataTable();

            ConnIDList.Columns.Add("ConnID");

            string[] connJsonStrings = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\ConnectionStrings.json");
            if (connJsonStrings.Length > 0)
            {
                foreach (string connJson in connJsonStrings)
                {
                    connectionStringObj connectionStringObject = JsonConvert.DeserializeObject<connectionStringObj>(connJson);

                    DataRow dr = ConnIDList.NewRow();
                    dr["ConnID"] = connectionStringObject.ConnID;
                    ConnIDList.Rows.Add(dr);
                }

                cbbConnID.DataSource = ConnIDList.DefaultView;
                cbbConnID.DisplayMember = "ConnID";
            }

        }

        private string generateWording(DataRow dr)
        {
            string declareType = "";
            string initialVariables = "";
            string convertObject = "";
            SqlDbType type = (SqlDbType)Enum.Parse(typeof(SqlDbType), dr["DataType"].ToString(), true);
            switch (equivalentSystemType[type].Name)
            {
                case "String":
                    initialVariables = " = \"\"";
                    declareType = "string";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = Convert.ToString(dr[\"" + dr["ColumnName"].ToString() + "\"]);";
                    break;
                case "Byte[]":
                    initialVariables = "";
                    declareType = "byte[]";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = (byte[])dr[\"" + dr["ColumnName"].ToString() + "\"]);";
                    break;
                case "Decimal":
                    initialVariables = " = 0";
                    declareType = "decimal";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = Convert.ToDecimal(dr[\"" + dr["ColumnName"].ToString() + "\"]);";
                    break;
                case "Single":
                    initialVariables = " = 0";
                    declareType = "double";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = Convert.ToDouble(dr[\"" + dr["ColumnName"].ToString() + "\"]);";
                    break;
                case "Double":
                    initialVariables = " = 0";
                    declareType = "double";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = Convert.ToDouble(dr[\"" + dr["ColumnName"].ToString() + "\"]);";
                    break;
                case "Int16":
                    initialVariables = " = 0";
                    declareType = "short";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = Convert.ToInt16(dr[\"" + dr["ColumnName"].ToString() + "\"]);";
                    break;
                case "Int32":
                    initialVariables = " = 0";
                    declareType = "int";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = Convert.ToInt32(dr[\"" + dr["ColumnName"].ToString() + "\"]);";
                    break;
                case "Int64":
                    initialVariables = " = 0";
                    declareType = "long";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = Convert.ToInt64(dr[\"" + dr["ColumnName"].ToString() + "\"]);";
                    break;
                case "Boolean":
                    initialVariables = " = false";
                    declareType = "bool";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = Convert.ToBoolean(dr[\"" + dr["ColumnName"].ToString() + "\"]);";
                    break;
                case "TimeSpan":
                case "DateTime":
                case "DateTimeOffset":
                    initialVariables = " = null";
                    declareType = "DateTime?";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = dr[\"" + dr["ColumnName"].ToString() + "\"] == DBNull.Value ? new Nullable<DateTime>() : Convert.ToDateTime(dr[\"" + dr["ColumnName"].ToString() + "\"]);";
                    break;
                case "SqlXml":
                    initialVariables = " = new XmlDocument()";
                    declareType = "XmlDocument";
                    convertObject = "if (dr[\"" + dr["ColumnName"].ToString() + "\"] != null && dr[\"" + dr["ColumnName"].ToString() + "\"].ToString().Trim() != \"\")\n" +
                        "\t\t\t\t\t{\n" +
                        "\t\t\t\t\t\tthis._" + dr["ColumnName"].ToString() + ".LoadXml(Convert.ToString(dr[\"" + dr["ColumnName"].ToString() + "\"]));\n" +
                        "\t\t\t\t\t}";
                    break;
                case "Guid":
                    initialVariables = " = new Guid()";
                    declareType = "Guid";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = Convert.ToString(dr[\"" + dr["ColumnName"].ToString() + "\"]);";
                    break;
            }
            return initialVariables + "@" + declareType + "@" + equivalentSystemType[type].Name + "@" + convertObject;
        }

        #endregion
    }
}
