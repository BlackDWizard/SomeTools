
namespace TestForm
{
    partial class JsonForm
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
            this.tbXML = new System.Windows.Forms.TextBox();
            this.btnToJson = new System.Windows.Forms.Button();
            this.tbJSON = new System.Windows.Forms.TextBox();
            this.btnToXML = new System.Windows.Forms.Button();
            this.btnToJSONString = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbXML
            // 
            this.tbXML.Location = new System.Drawing.Point(9, 10);
            this.tbXML.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbXML.Multiline = true;
            this.tbXML.Name = "tbXML";
            this.tbXML.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbXML.Size = new System.Drawing.Size(722, 166);
            this.tbXML.TabIndex = 0;
            // 
            // btnToJson
            // 
            this.btnToJson.Location = new System.Drawing.Point(9, 349);
            this.btnToJson.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnToJson.Name = "btnToJson";
            this.btnToJson.Size = new System.Drawing.Size(722, 38);
            this.btnToJson.TabIndex = 2;
            this.btnToJson.Text = "XML轉JSON";
            this.btnToJson.UseVisualStyleBackColor = true;
            this.btnToJson.Click += new System.EventHandler(this.btnToJson_Click);
            // 
            // tbJSON
            // 
            this.tbJSON.Location = new System.Drawing.Point(9, 179);
            this.tbJSON.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbJSON.Multiline = true;
            this.tbJSON.Name = "tbJSON";
            this.tbJSON.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbJSON.Size = new System.Drawing.Size(722, 166);
            this.tbJSON.TabIndex = 3;
            // 
            // btnToXML
            // 
            this.btnToXML.Location = new System.Drawing.Point(9, 391);
            this.btnToXML.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnToXML.Name = "btnToXML";
            this.btnToXML.Size = new System.Drawing.Size(722, 38);
            this.btnToXML.TabIndex = 5;
            this.btnToXML.Text = "JSON轉XML";
            this.btnToXML.UseVisualStyleBackColor = true;
            this.btnToXML.Click += new System.EventHandler(this.btnToXML_Click);
            // 
            // btnToJSONString
            // 
            this.btnToJSONString.Location = new System.Drawing.Point(9, 434);
            this.btnToJSONString.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnToJSONString.Name = "btnToJSONString";
            this.btnToJSONString.Size = new System.Drawing.Size(722, 38);
            this.btnToJSONString.TabIndex = 6;
            this.btnToJSONString.Text = "JSONtoJSONString";
            this.btnToJSONString.UseVisualStyleBackColor = true;
            this.btnToJSONString.Click += new System.EventHandler(this.btnToJSONString_Click);
            // 
            // JsonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 478);
            this.Controls.Add(this.btnToJSONString);
            this.Controls.Add(this.btnToXML);
            this.Controls.Add(this.tbJSON);
            this.Controls.Add(this.btnToJson);
            this.Controls.Add(this.tbXML);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "JsonForm";
            this.Text = "JsonForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbXML;
        private System.Windows.Forms.Button btnToJson;
        private System.Windows.Forms.TextBox tbJSON;
        private System.Windows.Forms.Button btnToXML;
        private System.Windows.Forms.Button btnToJSONString;
    }
}

