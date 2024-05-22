using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestForm;

namespace TestForm
{
    public partial class mainForm : Form
    {
        bool subWindowOpened = false;
        public mainForm()
        {
            InitializeComponent();
        }

        private void btnAPITestForm_Click(object sender, EventArgs e)
        {
            APITestForm APItestForm = new APITestForm();
            APItestForm.Show();
            APItestForm.FormClosed += new FormClosedEventHandler(this.childrenFormClosed);

            this.Enabled = false;
        }

        private void btnGenerateID_Click(object sender, EventArgs e)
        {
            GenerateIDForm generateIDForm = new GenerateIDForm();
            generateIDForm.Show();
            generateIDForm.FormClosed += new FormClosedEventHandler(this.childrenFormClosed);

            this.Enabled = false;
        }

        private void btnJsonTransform_Click(object sender, EventArgs e)
        {
            JsonForm jsonForm = new JsonForm();
            jsonForm.Show();
            jsonForm.FormClosed += new FormClosedEventHandler(this.childrenFormClosed);

            this.Enabled = false;
        }

        private void btnPandaman_Click(object sender, EventArgs e)
        {
            PandamanForm pandamanForm = new PandamanForm();
            pandamanForm.Show();
            pandamanForm.FormClosed += new FormClosedEventHandler(this.childrenFormClosed);

            this.Enabled = false;
            subWindowOpened = true;
        }

        private void childrenFormClosed(object sender,EventArgs e)
        {
            this.Enabled = true;
            subWindowOpened = false;
        }

        private void btnAPITest_Click(object sender, EventArgs e)
        {
            APITest apiTest = new APITest();
            apiTest.Show();
            apiTest.FormClosed += new FormClosedEventHandler(this.childrenFormClosed);

            this.Enabled = false;
            subWindowOpened = true;
        }
    }
}
