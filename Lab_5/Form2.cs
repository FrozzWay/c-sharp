using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Lab_5
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var request = new RestRequest("https://graph.facebook.com/me");
            request.AddParameter("access_token", Form1.access_token);
            request.AddParameter("fields", "name,birthday,id");
            var response = Form1.client.Get(request);
            JObject JSONresponse = JObject.Parse(response.Content);
            string id = (string)JSONresponse.GetValue("id");
            string name = (string)JSONresponse.GetValue("name");
            string birthday = (string)JSONresponse.GetValue("birthday");
            MessageBox.Show($"{id}\n{name}\n{birthday}", "Info", MessageBoxButtons.OK);

        }
    }
}
