using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace TestForm
{
    public partial class JsonForm : Form
    {
        public JsonForm()
        {
            InitializeComponent();
        }

        private void btnToJson_Click(object sender, EventArgs e)
        {
            if (tbXML.Text != "")
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(tbXML.Text);
                tbJSON.Text = JsonConvert.SerializeXmlNode(doc);
            }
        }

        private void btnToXML_Click(object sender, EventArgs e)
        {
            if (tbXML.Text != "")
            {
                XmlDocument doc = JsonConvert.DeserializeXmlNode(tbJSON.Text);
                tbXML.Text = doc.InnerXml;
            }
        }

        private void btnToJSONString_Click(object sender, EventArgs e)
        {
            string json = tbXML.Text;
            tbJSON.Text = "\"" + json.Replace("\"", "\\\"") + "\"";
        }
    }
}
