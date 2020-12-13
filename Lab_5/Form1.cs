using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json.Linq;
// dotnet add package RestSharp
// Install-Package Newtonsoft.Json

namespace Lab_5
{
    public partial class Form1 : Form
    {
        static string user_code;
        static string code;
        public static string access_token;
        public static RestClient client = new RestClient();
        private static System.Timers.Timer aTimer;
        public Form1()
        {
            InitializeComponent();
        }

        private static void setAuthTimer()
        {
            aTimer = new System.Timers.Timer(6000);
            aTimer.Elapsed += onTimer;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void onTimer(object source, ElapsedEventArgs e)
        {
            var request = new RestRequest("https://graph.facebook.com/v2.6/device/login_status");
            request.AddParameter("access_token", "152175889581621|069f4c9082317e8f541fa12d8bf292c3");
            request.AddParameter("code", code);
            var response = client.Post(request);
            JObject JSONresponse = JObject.Parse(response.Content);
            Console.WriteLine("Go");
            if (JSONresponse["error"] == null)
            {
                access_token = (string)JSONresponse.GetValue("access_token");
                aTimer.Stop();
                aTimer.Dispose();
                MessageBox.Show("You logged in!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form2 form2 = new Form2();
                form2.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var request = new RestRequest("https://graph.facebook.com/v2.6/device/login");
            request.AddParameter("access_token", "152175889581621|069f4c9082317e8f541fa12d8bf292c3");
            request.AddParameter("scope", "email, user_gender, user_birthday, user_friends, user_hometown, user_likes, user_posts, user_photos");
            var response = client.Post(request);
            JObject JSONresponse = JObject.Parse(response.Content);
            user_code = (string)JSONresponse.GetValue("user_code");
            code = (string)JSONresponse.GetValue("code");
            textBox2.AppendText(user_code);
            setAuthTimer();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
