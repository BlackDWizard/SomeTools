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
    public partial class APITest : Form
    {
        public APITest()
        {
            InitializeComponent();
            FormControl();
        }

        #region Properties
        #endregion

        #region ControlFunction

        private void rbBodyRaw_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBodyRaw.Checked)
                cbbRawType.Visible = true;
            else
            {
                cbbRawType.Visible = false;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string JsonResponse = CallAPIByRequest(cbbHTTPMethods.SelectedText, tbURL.Text, tecRequestBody.Text);

            tecResponseBody.Text = JsonResponse;
        }
        #endregion

        #region Function

        private void FormControl()
        {
            cbbRawType.Visible = false;
        }

        private string CallAPIByRequest(string HttpMethod, string Url, string Json)
        {
            string result = string.Empty;
            //設定webservice
            string APIURL = Url;
            var request = (HttpWebRequest)WebRequest.Create(APIURL);
            request.Method = HttpMethod;
            request.ContentType = "application/json; charset=utf-8";

            Dictionary<string, object> JsonDeserial = JsonConvert.DeserializeObject<Dictionary<string, object>>(Json);
            //send to webservice
            using (var streamwriter = new StreamWriter(request.GetRequestStream()))
            {
                streamwriter.Write(Json);
            }

            string Token = "";
            if ((JsonDeserial.ContainsKey("Token")))
                Token = JsonDeserial["Token"].ToString();

            //get webservice response
            var response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }
        #endregion
    }
}
