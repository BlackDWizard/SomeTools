using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TestForm
{
    public partial class APITestForm : Form
    {
        public APITestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                string result = string.Empty;
                //設定webservice
                var request = (HttpWebRequest)WebRequest.Create(tbURL.Text);
                request.Method = "POST";
                request.ContentType = "application/json";

                //String authHeaer = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(tbUserName.Text + ":" + tbPassword.Text));
                //request.Headers.Add("Authorization", "Basic " + authHeaer);

                //send to webservice
                using (var streamwriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamwriter.Write(tbForwardContent.Text);
                }

                //get webservice response
                var response = request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                tbReplyContent.Text = result;
            }
            catch(Exception ex)
            {
                tbException.Text = ex.ToString();
            }
        }
    }
}
