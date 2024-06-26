﻿using ICSharpCode.TextEditor.Document;
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

        internal static readonly Dictionary<string, string> SqlDbTypeToUpper = new Dictionary<string, string>
{
    { "bigint","BigInt" },
    { "binary","Binary" },
    { "bit","Bit" },
    { "char","Char" },
    { "date","Date" },
    { "datetime","DateTime" },
    { "datetime2","DateTime2" },
    { "datetimeoffset","DateTimeOffset" },
    { "decimal","Decimal" },
    { "float","Float" },
    { "image","Image" },
    { "int","Int" },
    { "Money","Money" },
    { "nchar","NChar" },
    { "nvarchar","NVarChar" },
    { "real","Real" },
    { "smalldatetime","SmallDateTime" },
    { "smallint","SmallInt" },
    { "smallmoney","SmallMoney" },
    { "time","Time" },
    { "tinyint","TinyInt" },
    { "uniqueidentifier","UniqueIdentifier" },
    { "varbinary","VarBinary" },
    { "varchar","VarChar" },
    { "xml","Xml" },
    { "ntext","NText" }
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
            sbCmd.AppendLine("\t   IC.COLUMN_NAME AS ColumnName,");
            sbCmd.AppendLine("\t   IC.ORDINAL_POSITION AS SN,");
            sbCmd.AppendLine("\t   CASE WHEN PK.COLUMN_NAME = IC.COLUMN_NAME THEN 'Y' ELSE 'N' END isPK,");
            sbCmd.AppendLine("\t   IC.DATA_TYPE AS DataType,");
            sbCmd.AppendLine("\t   IC.CHARACTER_MAXIMUM_LENGTH AS Length,");
            sbCmd.AppendLine("\t   IC.IS_NULLABLE AS isNullable,");
            sbCmd.AppendLine("\t   Identity_Description.Description,");
            sbCmd.AppendLine("\t   CASE WHEN Identity_Description.isIdentity = 0 THEN 'N' WHEN Identity_Description.isIdentity = 1 THEN 'Y' END isIdentity");
            sbCmd.AppendLine("\t   FROM INFORMATION_SCHEMA.COLUMNS AS IC WITH (Nolock) ");

            sbCmd.AppendLine("\t   LEFT JOIN ");
            sbCmd.AppendLine("\t   (SELECT ST.name [Table],");
            sbCmd.AppendLine("\t   SC.name [Column], ");
            sbCmd.AppendLine("\t   SC.is_identity [isIdentity], ");
            sbCmd.AppendLine("\t   SEP.value [Description] ");
            sbCmd.AppendLine("\t   FROM sys.tables ST ");
            sbCmd.AppendLine("\t   INNER JOIN sys.columns SC on ST.object_id = SC.object_id ");
            sbCmd.AppendLine("\t   LEFT JOIN sys.extended_properties SEP on ST.object_id = SEP.major_id ");
            sbCmd.AppendLine("\t   AND SC.column_id = SEP.minor_id ");
            sbCmd.AppendLine("\t   AND SEP.name = 'MS_Description' ");
            sbCmd.AppendLine("\t   WHERE ST.name = @TABLE_NAME) AS Identity_Description  ");
            sbCmd.AppendLine("\t   ON Identity_Description.[Column] = IC.COLUMN_NAME ");

            sbCmd.AppendLine("\t   LEFT JOIN ");
            sbCmd.AppendLine("\t   (SELECT ICC.TABLE_NAME,");
            sbCmd.AppendLine("\t   ICC.COLUMN_NAME ");
            sbCmd.AppendLine("\t   FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS ITC ");
            sbCmd.AppendLine("\t   JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS ICC ");
            sbCmd.AppendLine("\t   ON ITC.CONSTRAINT_NAME = ICC.CONSTRAINT_NAME ");
            sbCmd.AppendLine("\t   WHERE ITC.CONSTRAINT_TYPE = 'PRIMARY KEY'  ");
            sbCmd.AppendLine("\t\t\t AND ICC.TABLE_NAME = @TABLE_NAME) AS PK");
            sbCmd.AppendLine("\t   ON PK.[COLUMN_NAME] = Identity_Description.[Column] ");

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
                infoScript.AppendLine("\tpublic partial class " + tableName + "Info : baseDB");
                infoScript.AppendLine("\t{");
                infoScript.AppendLine("\t\t");
                infoScript.AppendLine("\t\t/// <summary>");
                infoScript.AppendLine("\t\t/// Constructors");
                infoScript.AppendLine("\t\t/// </summary>		");
                infoScript.AppendLine("\t\tpublic " + tableName + "Info()");
                infoScript.AppendLine("\t\t{");
                infoScript.AppendLine("\t\t\tbase.DBInstanceName = \"" + drvConnID + "\"; //SEC Connection ID");
                infoScript.AppendLine("\t\t\tthis.Init();");
                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t#region Init");
                infoScript.AppendLine("\t\tprivate void Init()");
                infoScript.AppendLine("\t\t{");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("\t\t	this._" + String.Format("{0,-60}", dr["ColumnName"].ToString() + wording[0].ToString() + ";") + "//" + dr["Description"].ToString().Replace("\r\n", "\t"));
                }

                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("\t\t#endregion");
                infoScript.AppendLine("\t\t");
                infoScript.AppendLine("\t\t");
                infoScript.AppendLine("\t\t#region Private Properties");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("\t\t	private " + wording[1].ToString() + " _" + dr["ColumnName"].ToString() + ";");
                }

                infoScript.AppendLine("\t\t#endregion");
                infoScript.AppendLine("\t\t");
                infoScript.AppendLine("\t\t");
                infoScript.AppendLine("\t\t#region Public Properties");
                infoScript.AppendLine("\t\t");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("\t\t/// <summary>");
                    infoScript.AppendLine("\t\t/// " + dr["Description"].ToString().Replace("\r\n", "\t"));
                    infoScript.AppendLine("\t\t/// </summary>");
                    infoScript.AppendLine("\t\tpublic " + wording[1] + " " + dr["ColumnName"].ToString());
                    infoScript.AppendLine("\t\t{");
                    infoScript.AppendLine("\t\t	get { return _" + dr["ColumnName"].ToString() + "; }");
                    infoScript.AppendLine("\t\t	set { _" + dr["ColumnName"].ToString() + " = value; }");
                    infoScript.AppendLine("\t\t}");
                }
                infoScript.AppendLine("\t\t#endregion");
                infoScript.AppendLine("\t\t");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t#region Methods");
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t/// <summary>");
                infoScript.AppendLine("\t\t/// 依據PK載入一筆資料");
                infoScript.AppendLine("\t\t/// </summary>");
                infoScript.AppendLine("\t\t/// <returns>true代表成功載入，false代表找不到任何資料</returns>");
                infoScript.Append("\t\tpublic bool Load(");

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
                infoScript.AppendLine("\t\t{");
                infoScript.AppendLine("\t\t\tbool Result = false;");
                infoScript.AppendLine("");
                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("\t\t\tthis._" + pkColumn + " = i" + pkColumn + ";");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\tDatabase db = base.GetDatabase();");
                infoScript.AppendLine("\t\t\tStringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"   SELECT * FROM [" + tableName + "] WITH (Nolock) \");");
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"   WHERE(1 = 1) \");");
                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("\t\t\tsbCmd.Append(\"\t   AND " + pkColumn + " = @" + pkColumn + "\t  \");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\tDbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t#region Add In Parameter");
                infoScript.AppendLine("");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dr["isPK"].ToString() == "Y")
                        infoScript.AppendLine("\t\t\tdb.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t#endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\ttry");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\tDataTable dtTemp = db.ExecuteDataSet(dbCommand).Tables[0];");
                infoScript.AppendLine("\t\t\t\tif (dtTemp.Rows.Count == 0)");
                infoScript.AppendLine("\t\t\t\t{");
                infoScript.AppendLine("\t\t\t\t    base.EditMode = EditType.Insert;");
                infoScript.AppendLine("\t\t\t\t    Result = false;");
                infoScript.AppendLine("\t\t\t\t}");
                infoScript.AppendLine("\t\t\t\telse");
                infoScript.AppendLine("\t\t\t\t{");
                infoScript.AppendLine("\t\t\t\t    base.EditMode = EditType.Update;");
                infoScript.AppendLine("\t\t\t\t    Result = true;");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t    DataRow dr = dtTemp.Rows[0];");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("\t\t\t\t\t" + wording[3]);
                }
                infoScript.AppendLine("\t\t\t\t}");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("\t\t\t\tbase.IsSuccess = true;");
                else
                    infoScript.AppendLine("\t\t\t\tbase.ErrFlag = true;");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t\tcatch (Exception ex)");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\t#region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)");
                infoScript.AppendLine("\t\t\t\t//取得目前MethodName");
                infoScript.AppendLine("\t\t\t\tSystem.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();");
                infoScript.AppendLine("\t\t\t\tSystem.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\tResult = false;");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("\t\t\t\tbase.IsSuccess = false;");
                else
                    infoScript.AppendLine("\t\t\t\tbase.ErrFlag = false;");
                infoScript.AppendLine("\t\t\t\tbase.ErrMsg = ex.ToString();");
                infoScript.AppendLine("\t\t\t\tbase.ErrMethodName = myMethodBase.Name.ToString();");
                if (checkedButtonWriteExplog.Name == "rbWriteExplogY")
                {
                    infoScript.AppendLine("\t\t\t\tStringBuilder sbParameter = new StringBuilder();");
                    infoScript.AppendLine("\t\t\t\tforeach (DbParameter Value in dbCommand.Parameters)");
                    infoScript.AppendLine("\t\t\t\t{");
                    infoScript.AppendLine("\t\t\t\t    sbParameter.AppendLine(\"declare \" + Value.ParameterName + \" nvarchar(500) set \" + Value.ParameterName  + \" = '\" + Convert.ToString(Value.Value) + \"'\");");
                    infoScript.AppendLine("\t\t\t\t}");
                    infoScript.AppendLine("\t\t\t\tbase.ErrDbCommand = sbParameter.ToString() + dbCommand.CommandText;");
                }
                infoScript.AppendLine("\t\t\t\tbase.LogExpInf();");
                infoScript.AppendLine("\t\t\t\t#endregion");
                infoScript.AppendLine("");
                if (checkedButtonThrowException.Name == "rbThrowExceptionY")
                    infoScript.AppendLine("\t\t\t\tthrow; //將原來的 exception 再次的抛出去");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\treturn Result;");
                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t/// <summary>");
                infoScript.AppendLine("\t\t/// Insert");
                infoScript.AppendLine("\t\t/// </summary>");
                infoScript.AppendLine("\t\tpublic void Insert()");
                infoScript.AppendLine("\t\t{");
                infoScript.AppendLine("\t\t\tDatabase db = base.GetDatabase();");
                infoScript.AppendLine("\t\t\tStringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"	INSERT INTO [" + tableName + "]		\");");
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"		(				\");");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    if (ColumnsTable.Rows.IndexOf(dr) == 0)
                        infoScript.AppendLine("\t\t\tsbCmd.Append(\"		" + dr["ColumnName"].ToString() + "		\");");
                    else
                        infoScript.AppendLine("\t\t\tsbCmd.Append(\"		," + dr["ColumnName"].ToString() + "		\");");
                }
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"		)				\");");
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"	VALUES		\");");
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"		(				\");");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    if (ColumnsTable.Rows.IndexOf(dr) == 0)
                        infoScript.AppendLine("\t\t\tsbCmd.Append(\"		@" + dr["ColumnName"].ToString() + "		\");");
                    else
                        infoScript.AppendLine("\t\t\tsbCmd.Append(\"		,@" + dr["ColumnName"].ToString() + "		\");");
                }
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"		)				\");");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\tDbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t#region Add In Parameter");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (wording[1] == "XmlDocument")
                    {
                        infoScript.AppendLine("\t\t\tdb.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType.Xml, this._" + dr["ColumnName"].ToString() + ".OuterXml);");
                    }
                    else
                    {
                        infoScript.AppendLine("\t\t\tdb.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                    }
                }
                infoScript.AppendLine("\t\t\t#endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\ttry");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\tdb.ExecuteNonQuery(dbCommand);");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("\t\t\t\tbase.IsSuccess = true;");
                else
                    infoScript.AppendLine("\t\t\t\tbase.ErrFlag = false;");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t\tcatch (Exception ex)");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\t#region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)");
                infoScript.AppendLine("\t\t\t\t//取得目前MethodName");
                infoScript.AppendLine("\t\t\t\tSystem.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();");
                infoScript.AppendLine("\t\t\t\tSystem.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();");
                infoScript.AppendLine("");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("\t\t\t\tbase.IsSuccess = false;");
                else
                    infoScript.AppendLine("\t\t\t\tbase.ErrFlag = false;");
                infoScript.AppendLine("\t\t\t\tbase.ErrMsg = ex.ToString();");
                infoScript.AppendLine("\t\t\t\tbase.ErrMethodName = myMethodBase.Name.ToString();");
                if (checkedButtonWriteExplog.Name == "rbWriteExplogY")
                {
                    infoScript.AppendLine("\t\t\t\tStringBuilder sbParameter = new StringBuilder();");
                    infoScript.AppendLine("\t\t\t\tforeach (DbParameter Value in dbCommand.Parameters)");
                    infoScript.AppendLine("\t\t\t\t{");
                    infoScript.AppendLine("\t\t\t\t    sbParameter.AppendLine(\"declare \" + Value.ParameterName + \" nvarchar(500) set \" + Value.ParameterName  + \" = '\" + Convert.ToString(Value.Value) + \"'\");");
                    infoScript.AppendLine("\t\t\t\t}");
                    infoScript.AppendLine("\t\t\t\tbase.ErrDbCommand = sbParameter.ToString() + dbCommand.CommandText;");
                }
                infoScript.AppendLine("\t\t\t\tbase.LogExpInf();");
                infoScript.AppendLine("\t\t\t\t#endregion");
                infoScript.AppendLine("");
                if (checkedButtonThrowException.Name == "rbThrowExceptionY")
                    infoScript.AppendLine("\t\t\t\tthrow; //將原來的 exception 再次的抛出去");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t/// <summary>");
                infoScript.AppendLine("\t\t/// Update");
                infoScript.AppendLine("\t\t/// </summary>");
                infoScript.AppendLine("\t\tpublic void Update()");
                infoScript.AppendLine("\t\t{");
                infoScript.AppendLine("\t\t\tDatabase db = base.GetDatabase();");
                infoScript.AppendLine("\t\t\tStringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"	UPDATE [" + tableName + "] SET 		\");");
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
                        infoScript.AppendLine("\t\t\tsbCmd.Append(\"		" + dr["ColumnName"].ToString() + " = @" + dr["ColumnName"].ToString() + " 		\");");
                    else
                        infoScript.AppendLine("\t\t\tsbCmd.Append(\"		," + dr["ColumnName"].ToString() + " = @" + dr["ColumnName"].ToString() + " 		\");");
                }
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"	WHERE (1=1) \");");
                foreach (string ColumnName in pkColumns)
                {
                    infoScript.AppendLine("\t\t\tsbCmd.Append(\"		AND " + ColumnName + " = @" + ColumnName + " 		\");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\tDbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t#region Add In Parameter");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (wording[1] == "XmlDocument")
                    {
                        infoScript.AppendLine("\t\t\tdb.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType.Xml, this._" + dr["ColumnName"].ToString() + ".OuterXml);");
                    }
                    else
                    {
                        infoScript.AppendLine("\t\t\tdb.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                    }
                }
                infoScript.AppendLine("\t\t\t#endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\ttry");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\tdb.ExecuteNonQuery(dbCommand);");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("\t\t\t\tbase.IsSuccess = true;");
                else
                    infoScript.AppendLine("\t\t\t\tbase.ErrFlag = false;");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t\tcatch (Exception ex)");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\t#region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)");
                infoScript.AppendLine("\t\t\t\t//取得目前MethodName");
                infoScript.AppendLine("\t\t\t\tSystem.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();");
                infoScript.AppendLine("\t\t\t\tSystem.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();");
                infoScript.AppendLine("");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("\t\t\t\tbase.IsSuccess = false;");
                else
                    infoScript.AppendLine("\t\t\t\tbase.ErrFlag = false;");
                infoScript.AppendLine("\t\t\t\tbase.ErrMsg = ex.ToString();");
                infoScript.AppendLine("\t\t\t\tbase.ErrMethodName = myMethodBase.Name.ToString();");
                if (checkedButtonWriteExplog.Name == "rbWriteExplogY")
                {
                    infoScript.AppendLine("\t\t\t\tStringBuilder sbParameter = new StringBuilder();");
                    infoScript.AppendLine("\t\t\t\tforeach (DbParameter Value in dbCommand.Parameters)");
                    infoScript.AppendLine("\t\t\t\t{");
                    infoScript.AppendLine("\t\t\t\t    sbParameter.AppendLine(\"declare \" + Value.ParameterName + \" nvarchar(500) set \" + Value.ParameterName  + \" = '\" + Convert.ToString(Value.Value) + \"'\");");
                    infoScript.AppendLine("\t\t\t\t}");
                    infoScript.AppendLine("\t\t\t\tbase.ErrDbCommand = sbParameter.ToString() + dbCommand.CommandText;");
                }
                infoScript.AppendLine("\t\t\t\tbase.LogExpInf();");
                infoScript.AppendLine("\t\t\t\t#endregion");
                infoScript.AppendLine("");
                if (checkedButtonThrowException.Name == "rbThrowExceptionY")
                    infoScript.AppendLine("\t\t\t\tthrow; //將原來的 exception 再次的抛出去");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t/// <summary>");
                infoScript.AppendLine("\t\t/// Delete");
                infoScript.AppendLine("\t\t/// </summary>");
                infoScript.Append("\t\tpublic void Delete(");

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
                infoScript.AppendLine("\t\t{");
                infoScript.AppendLine("\t\t\tDatabase db = base.GetDatabase();");
                infoScript.AppendLine("\t\t\tStringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");

                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("\t\t\tthis._" + pkColumn + " = i" + pkColumn + ";");
                }

                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"	DELETE [" + tableName + "]		\");");
                infoScript.AppendLine("\t\t\tsbCmd.Append(\"	WHERE (1=1) 		\");");
                foreach (string ColumnName in pkColumns)
                {
                    infoScript.AppendLine("\t\t\tsbCmd.Append(\"		AND " + ColumnName + " = @" + ColumnName + " 		\");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\tDbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t#region Add In Parameter");
                infoScript.AppendLine("");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dr["isPK"].ToString() == "Y")
                        infoScript.AppendLine("\t\t\tdb.AddInParameter(dbCommand, \"@" + dr["ColumnName"].ToString() + "\", DbType." + wording[2] + ", this._" + dr["ColumnName"].ToString() + ");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t#endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\ttry");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\tdb.ExecuteNonQuery(dbCommand);");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("\t\t\t\tbase.IsSuccess = true;");
                else
                    infoScript.AppendLine("\t\t\t\tbase.ErrFlag = false;");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t\tcatch (Exception ex)");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\t#region 呼叫Base.LogExpInf 進行Exception Log 記錄 (固定寫法)");
                infoScript.AppendLine("\t\t\t\t//取得目前MethodName");
                infoScript.AppendLine("\t\t\t\tSystem.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();");
                infoScript.AppendLine("\t\t\t\tSystem.Reflection.MethodBase myMethodBase = stackFrame.GetMethod();");
                infoScript.AppendLine("");
                if (checkedButtonIsSuccess.Name == "rbIsSuccessY")
                    infoScript.AppendLine("\t\t\t\tbase.IsSuccess = false;");
                else
                    infoScript.AppendLine("\t\t\t\tbase.ErrFlag = false;");
                infoScript.AppendLine("\t\t\t\tbase.ErrMsg = ex.ToString();");
                infoScript.AppendLine("\t\t\t\tbase.ErrMethodName = myMethodBase.Name.ToString();");
                if (checkedButtonWriteExplog.Name == "rbWriteExplogY")
                {
                    infoScript.AppendLine("\t\t\t\tStringBuilder sbParameter = new StringBuilder();");
                    infoScript.AppendLine("\t\t\t\tforeach (DbParameter Value in dbCommand.Parameters)");
                    infoScript.AppendLine("\t\t\t\t{");
                    infoScript.AppendLine("\t\t\t\t    sbParameter.AppendLine(\"declare \" + Value.ParameterName + \" nvarchar(500) set \" + Value.ParameterName  + \" = '\" + Convert.ToString(Value.Value) + \"'\");");
                    infoScript.AppendLine("\t\t\t\t}");
                    infoScript.AppendLine("\t\t\t\tbase.ErrDbCommand = sbParameter.ToString() + dbCommand.CommandText;");
                }
                infoScript.AppendLine("\t\t\t\tbase.LogExpInf();");
                infoScript.AppendLine("\t\t\t\t#endregion");
                infoScript.AppendLine("");
                if (checkedButtonThrowException.Name == "rbThrowExceptionY")
                    infoScript.AppendLine("\t\t\t\tthrow; //將原來的 exception 再次的抛出去");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t/// <summary>");
                infoScript.AppendLine("\t\t/// Save用法： 1. Info.Load() 2. Set Value 3. Info.Save()");
                infoScript.AppendLine("\t\t/// </summary>	");
                infoScript.AppendLine("\t\tpublic void Save()");
                infoScript.AppendLine("\t\t{");
                infoScript.AppendLine("\t\t\tif (base.EditMode == EditType.Insert)");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\tthis.Insert();");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t\telse");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\tthis.Update();");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t#endregion");
                infoScript.AppendLine("\t}");
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

                infoScript.AppendLine("using System.Data;");
                infoScript.AppendLine("using System.Text;");
                infoScript.AppendLine("using System.Data.SqlClient;");
                infoScript.AppendLine("using System.Diagnostics;");
                infoScript.AppendLine("");
                infoScript.AppendLine("namespace " + tbNameSpace.Text);
                infoScript.AppendLine("{");
                infoScript.AppendLine("\tpublic partial class " + tableName + "Info");
                infoScript.AppendLine("\t{");
                infoScript.AppendLine("\t\t/// <summary>");
                infoScript.AppendLine("\t\t/// Constructors");
                infoScript.AppendLine("\t\t/// </summary>		");
                infoScript.AppendLine("\t\tpublic " + tableName + "Info(string connString)");
                infoScript.AppendLine("\t\t{");
                infoScript.AppendLine("\t\t\tthis.Init();");
                infoScript.AppendLine("\t\t\t_ConnectionString = connString;");
                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t#region Init");
                infoScript.AppendLine("\t\tprivate void Init()");
                infoScript.AppendLine("\t\t{");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("\t\t	this._" + String.Format("{0,-60}", dr["ColumnName"].ToString() + wording[0].ToString() + ";") + "//" + dr["Description"].ToString().Replace("\r\n", "\t"));
                }

                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("\t\t#endregion");
                infoScript.AppendLine("\t\t");
                infoScript.AppendLine("\t\t");
                infoScript.AppendLine("\t\t#region Private Properties");
                infoScript.AppendLine("\t\tprivate string _ConnectionString;");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("\t\tprivate " + wording[1].ToString() + " _" + dr["ColumnName"].ToString() + ";");
                }

                infoScript.AppendLine("\t\t#endregion");
                infoScript.AppendLine("\t\t");
                infoScript.AppendLine("\t\t");
                infoScript.AppendLine("\t\t#region Public Properties");
                infoScript.AppendLine("\t\t");

                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("\t\t/// <summary>");
                    infoScript.AppendLine("\t\t/// " + dr["Description"].ToString().Replace("\r\n", "\t"));
                    infoScript.AppendLine("\t\t/// </summary>");
                    infoScript.AppendLine("\t\tpublic " + wording[1] + " " + dr["ColumnName"].ToString());
                    infoScript.AppendLine("\t\t{");
                    infoScript.AppendLine("\t\t	get { return _" + dr["ColumnName"].ToString() + "; }");
                    infoScript.AppendLine("\t\t	set { _" + dr["ColumnName"].ToString() + " = value; }");
                    infoScript.AppendLine("\t\t}");
                }
                infoScript.AppendLine("\t\t#endregion");
                infoScript.AppendLine("\t\t");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t#region Methods");
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t/// <summary>");
                infoScript.AppendLine("\t\t/// 依據PK載入一筆資料");
                infoScript.AppendLine("\t\t/// </summary>");
                infoScript.AppendLine("\t\t/// <returns>true代表成功載入，false代表找不到任何資料</returns>");
                infoScript.Append("\t\tpublic bool Load(");

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
                infoScript.AppendLine("\t\t{");
                infoScript.AppendLine("\t\t\tbool Result = false;");
                infoScript.AppendLine("");
                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("\t\t\tthis._" + pkColumn + " = i" + pkColumn + ";");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\tusing (SqlCommand command = new SqlCommand())");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\tSqlConnection connection = new SqlConnection(_ConnectionString);");
                infoScript.AppendLine("\t\t\t\tconnection.Open();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\ttry");
                infoScript.AppendLine("\t\t\t\t{");
                infoScript.AppendLine("\t\t\t\t\tStringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"   SELECT * FROM [" + tableName + "] WITH (Nolock) \");");
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"   WHERE(1 = 1) \");");
                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"\t   AND " + pkColumn + " = @" + pkColumn + "\t  \");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tcommand.Connection = connection;");
                infoScript.AppendLine("\t\t\t\t\tcommand.CommandText = sbCmd.ToString();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\t#region Add In Parameter");
                infoScript.AppendLine("");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (dr["isPK"].ToString() == "Y")
                        infoScript.AppendLine("\t\t\t\t\tcommand.Parameters.Add(\"@" + dr["ColumnName"].ToString() + "\", SqlDbType." + SqlDbTypeToUpper[dr["DataType"].ToString()] + ").Value = this._" + dr["ColumnName"].ToString() + ";");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\t#endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tIDataReader dataReader = command.ExecuteReader();");
                infoScript.AppendLine("\t\t\t\t\tDataTable dtTemp = new DataTable();");
                infoScript.AppendLine("\t\t\t\t\tdtTemp.Load(dataReader);");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tif (dtTemp.Rows.Count == 0)");
                infoScript.AppendLine("\t\t\t\t\t{");
                infoScript.AppendLine("\t\t\t\t\t\tResult = false;");
                infoScript.AppendLine("\t\t\t\t\t}");
                infoScript.AppendLine("\t\t\t\t\telse");
                infoScript.AppendLine("\t\t\t\t\t{");
                infoScript.AppendLine("\t\t\t\t\t\tResult = true;");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\t\tDataRow dr = dtTemp.Rows[0];");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    infoScript.AppendLine("\t\t\t\t\t\t" + wording[3]);
                }
                infoScript.AppendLine("\t\t\t\t\t}");
                infoScript.AppendLine("\t\t\t\t}");
                infoScript.AppendLine("\t\t\t\tcatch (Exception ex)");
                infoScript.AppendLine("\t\t\t\t{");
                infoScript.AppendLine("\t\t\t\t\tStackTrace stack = new StackTrace();");
                infoScript.AppendLine("\t\t\t\t\tStackFrame frame = stack.GetFrame(0);");
                infoScript.AppendLine("\t\t\t\t\tstring className = frame.GetMethod().DeclaringType.FullName;");
                infoScript.AppendLine("\t\t\t\t\tstring methodName = frame.GetMethod().Name;");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tExceptionLogInfo exceptionLog = new ExceptionLogInfo(_ConnectionString);");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionClass = className;");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionMethod = methodName;");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionReason = ex.ToString();");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionDate = DateTime.Now;");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.Insert();");
                infoScript.AppendLine("\t\t\t\t}");
                infoScript.AppendLine("\t\t\t\tconnection.Close();");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t\treturn Result;");
                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t/// <summary>");
                infoScript.AppendLine("\t\t/// Insert");
                infoScript.AppendLine("\t\t/// </summary>");
                infoScript.AppendLine("\t\tpublic void Insert()");
                infoScript.AppendLine("\t\t{");
                infoScript.AppendLine("\t\t\tusing (SqlCommand command = new SqlCommand())");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\tSqlConnection connection = new SqlConnection(_ConnectionString);");
                infoScript.AppendLine("\t\t\t\tconnection.Open();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\ttry");
                infoScript.AppendLine("\t\t\t\t{");
                infoScript.AppendLine("\t\t\t\t\tStringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"	INSERT INTO [" + tableName + "]		\");");
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		(				\");");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    if (dr["isIdentity"].ToString() == "Y")
                        continue;
                    if (ColumnsTable.Rows.IndexOf(dr) == 0 || (ColumnsTable.Rows.IndexOf(dr) == 1 && ColumnsTable.Rows[0]["isIdentity"].ToString() == "Y"))
                        infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		" + dr["ColumnName"].ToString() + "		\");");
                    else
                        infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		," + dr["ColumnName"].ToString() + "		\");");
                }
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		)				\");");
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"	VALUES		\");");
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		(				\");");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    if (dr["isIdentity"].ToString() == "Y")
                        continue;
                    if (ColumnsTable.Rows.IndexOf(dr) == 0 || (ColumnsTable.Rows.IndexOf(dr) == 1 && ColumnsTable.Rows[0]["isIdentity"].ToString() == "Y"))
                        infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		@" + dr["ColumnName"].ToString() + "		\");");
                    else
                        infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		,@" + dr["ColumnName"].ToString() + "		\");");
                }
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		)				\");");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tcommand.Connection = connection;");
                infoScript.AppendLine("\t\t\t\t\tcommand.CommandText = sbCmd.ToString();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\t#region Add In Parameter");
                infoScript.AppendLine("");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    if (dr["isIdentity"].ToString() == "Y")
                        continue;
                    string[] wording = generateWording(dr).Split('@');
                    if (wording[1] == "XmlDocument")
                    {
                        infoScript.AppendLine("\t\t\t\t\tcommand.Parameters.Add(\"@" + dr["ColumnName"].ToString() + "\", SqlDbType.Xml).Value = this._" + dr["ColumnName"].ToString() + ".OuterXml == null ? DBNull.Value : this._" + dr["ColumnName"].ToString() + ".OuterXml;");
                    }
                    else
                    {
                        infoScript.AppendLine("\t\t\t\t\tcommand.Parameters.Add(\"@" + dr["ColumnName"].ToString() + "\", SqlDbType." + SqlDbTypeToUpper[dr["DataType"].ToString()] + ").Value = this._" + dr["ColumnName"].ToString() + " == null ? DBNull.Value : this._" + dr["ColumnName"].ToString() + ";");
                    }
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\t#endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tcommand.ExecuteNonQuery();");
                infoScript.AppendLine("\t\t\t\t}");
                infoScript.AppendLine("\t\t\t\tcatch (Exception ex)");
                infoScript.AppendLine("\t\t\t\t{");
                infoScript.AppendLine("\t\t\t\t\tStackTrace stack = new StackTrace();");
                infoScript.AppendLine("\t\t\t\t\tStackFrame frame = stack.GetFrame(0);");
                infoScript.AppendLine("\t\t\t\t\tstring className = frame.GetMethod().DeclaringType.FullName;");
                infoScript.AppendLine("\t\t\t\t\tstring methodName = frame.GetMethod().Name;");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tExceptionLogInfo exceptionLog = new ExceptionLogInfo(_ConnectionString);");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionClass = className;");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionMethod = methodName;");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionReason = ex.ToString();");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionDate = DateTime.Now;");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.Insert();");
                infoScript.AppendLine("\t\t\t\t}");
                infoScript.AppendLine("\t\t\t\tconnection.Close();");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t/// <summary>");
                infoScript.AppendLine("\t\t/// Update");
                infoScript.AppendLine("\t\t/// </summary>");
                infoScript.AppendLine("\t\tpublic void Update()");
                infoScript.AppendLine("\t\t{");
                infoScript.AppendLine("\t\t\tusing (SqlCommand command = new SqlCommand())");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\tSqlConnection connection = new SqlConnection(_ConnectionString);");
                infoScript.AppendLine("\t\t\t\tconnection.Open();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\ttry");
                infoScript.AppendLine("\t\t\t\t{");
                infoScript.AppendLine("\t\t\t\t\tStringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"	UPDATE [" + tableName + "] SET 		\");");
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
                    if (dt.Rows.IndexOf(dr) == 0)
                        infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		" + dr["ColumnName"].ToString() + " = @" + dr["ColumnName"].ToString() + " 		\");");
                    else
                        infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		," + dr["ColumnName"].ToString() + " = @" + dr["ColumnName"].ToString() + " 		\");");
                }
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"	WHERE (1=1) \");");
                foreach (string ColumnName in pkColumns)
                {
                    infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		AND " + ColumnName + " = @" + ColumnName + " 		\");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tcommand.Connection = connection;");
                infoScript.AppendLine("\t\t\t\t\tcommand.CommandText = sbCmd.ToString();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\t#region Add In Parameter");
                infoScript.AppendLine("");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    string[] wording = generateWording(dr).Split('@');
                    if (wording[1] == "XmlDocument")
                    {
                        infoScript.AppendLine("\t\t\t\t\tcommand.Parameters.Add(\"@" + dr["ColumnName"].ToString() + "\", SqlDbType.Xml).Value = this._" + dr["ColumnName"].ToString() + ".OuterXml;");
                    }
                    else
                    {
                        infoScript.AppendLine("\t\t\t\t\tcommand.Parameters.Add(\"@" + dr["ColumnName"].ToString() + "\", SqlDbType." + SqlDbTypeToUpper[dr["DataType"].ToString()] + ").Value = this._" + dr["ColumnName"].ToString() + ";");
                    }
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\t#endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tcommand.ExecuteNonQuery();");
                infoScript.AppendLine("\t\t\t\t}");
                infoScript.AppendLine("\t\t\t\tcatch (Exception ex)");
                infoScript.AppendLine("\t\t\t\t{");
                infoScript.AppendLine("\t\t\t\t\tStackTrace stack = new StackTrace();");
                infoScript.AppendLine("\t\t\t\t\tStackFrame frame = stack.GetFrame(0);");
                infoScript.AppendLine("\t\t\t\t\tstring className = frame.GetMethod().DeclaringType.FullName;");
                infoScript.AppendLine("\t\t\t\t\tstring methodName = frame.GetMethod().Name;");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tExceptionLogInfo exceptionLog = new ExceptionLogInfo(_ConnectionString);");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionClass = className;");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionMethod = methodName;");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionReason = ex.ToString();");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.ExceptionDate = DateTime.Now;");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\texceptionLog.Insert();");
                infoScript.AppendLine("\t\t\t\t}");
                infoScript.AppendLine("\t\t\t\tconnection.Close();");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t/// <summary>");
                infoScript.AppendLine("\t\t/// Delete");
                infoScript.AppendLine("\t\t/// </summary>");
                infoScript.Append("\t\tpublic void Delete(");

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
                infoScript.AppendLine("\t\t{");

                foreach (string pkColumn in pkColumns)
                {
                    infoScript.AppendLine("\t\t\tthis._" + pkColumn + " = i" + pkColumn + ";");
                }

                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\tusing (SqlCommand command = new SqlCommand())");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\tSqlConnection connection = new SqlConnection(_ConnectionString);");
                infoScript.AppendLine("\t\t\t\tconnection.Open();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\ttry");
                infoScript.AppendLine("\t\t\t\t{");
                infoScript.AppendLine("\t\t\t\t\tStringBuilder sbCmd = new StringBuilder();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"	DELETE [" + tableName + "]		\");");
                infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"	WHERE (1=1) 		\");");
                foreach (string ColumnName in pkColumns)
                {
                    infoScript.AppendLine("\t\t\t\t\tsbCmd.Append(\"		AND " + ColumnName + " = @" + ColumnName + " 		\");");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tcommand.Connection = connection;");
                infoScript.AppendLine("\t\t\t\t\tcommand.CommandText = sbCmd.ToString();");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\t#region Add In Parameter");
                infoScript.AppendLine("");
                foreach (DataRow dr in ColumnsTable.Rows)
                {
                    if (dr["isPK"].ToString() == "Y")
                        infoScript.AppendLine("\t\t\t\t\tcommand.Parameters.Add(\"@" + dr["ColumnName"].ToString() + "\", SqlDbType." + SqlDbTypeToUpper[dr["DataType"].ToString()] + ").Value = this._" + dr["ColumnName"].ToString() + ";");
                }
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\t#endregion");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\t\tcommand.ExecuteNonQuery();");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t\tcatch (Exception ex)");
                infoScript.AppendLine("\t\t\t{");
                infoScript.AppendLine("\t\t\t\tStackTrace stack = new StackTrace();");
                infoScript.AppendLine("\t\t\t\tStackFrame frame = stack.GetFrame(0);");
                infoScript.AppendLine("\t\t\t\tstring className = frame.GetMethod().DeclaringType.FullName;");
                infoScript.AppendLine("\t\t\t\tstring methodName = frame.GetMethod().Name;");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\tExceptionLogInfo exceptionLog = new ExceptionLogInfo(_ConnectionString);");
                infoScript.AppendLine("\t\t\t\texceptionLog.ExceptionClass = className;");
                infoScript.AppendLine("\t\t\t\texceptionLog.ExceptionMethod = methodName;");
                infoScript.AppendLine("\t\t\t\texceptionLog.ExceptionReason = ex.ToString();");
                infoScript.AppendLine("\t\t\t\texceptionLog.ExceptionDate = DateTime.Now;");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t\t\texceptionLog.Insert();");
                infoScript.AppendLine("\t\t\t\t}");
                infoScript.AppendLine("\t\t\t\tconnection.Close();");
                infoScript.AppendLine("\t\t\t}");
                infoScript.AppendLine("\t\t}");
                infoScript.AppendLine("");
                infoScript.AppendLine("\t\t#endregion");
                infoScript.AppendLine("\t}");
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
                    initialVariables = " = null";
                    declareType = "byte[]";
                    convertObject = "this._" + dr["ColumnName"].ToString() + " = (byte[])dr[\"" + dr["ColumnName"].ToString() + "\"];";
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
