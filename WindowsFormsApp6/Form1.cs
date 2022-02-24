using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace WindowsFormsApp6
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("adminlink.txt"))
            {
                File.WriteAllText("adminlink.txt", AdminPanelFinder.Properties.Resources.adminlink);
            }
        }

        public void check()


        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))

            {
                MessageBox.Show("i don t see any url", "youhacker panel finder");
                return;
            }
            if (!textBox1.Text.EndsWith("/")) textBox1.Text += "/";
            if (!textBox1.Text.ToLower().StartsWith("http")) textBox1.Text = "http://" + textBox1.Text;

            var results = "";
            foreach (string line in File.ReadAllLines("adminlink.txt"))
            {
                richTextBox1.ReadOnly = true;
                richTextBox1.AppendText($"\nURL:{textBox1.Text + line}    Checking:\n");
                if (UrlIsValid(textBox1.Text + line))
                {

                    var results2 = "found this " + textBox1.Text + line + "\n";
                    richTextBox1.AppendText(results2);
                    using (StreamWriter file = new StreamWriter("results.txt"))
                    {
                        results = results + results2;
                        file.Write(results);

                    }






                }
                else
                {
                    var fail = "[-]there is no admin Panel here " + textBox1.Text + line + "\n";
                    richTextBox1.AppendText(fail);
                }




            }
        }

        private static bool UrlIsValid(string url)
        {
            bool urlExists = false;
            try
            {
                var httpRequest = new HttpRequest();
                var response = httpRequest.Get(url);
                if (response.StatusCode == xNet.HttpStatusCode.OK)
                {
                    urlExists = true;

                }
            }
            catch { }
            return urlExists;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(check);
            thread.Start();




        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (WebClient clone = new WebClient())
            {
                string cloned = clone.DownloadString(textBox1.Text);
                string email = cloned;
                string emails = "";
                Regex reg = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}", RegexOptions.IgnoreCase);
                Match match;
                for (match = reg.Match(email); match.Success; match = match.NextMatch())
                {
                    if (!(emails.Contains(match.Value)))
                        emails = emails + match.Value + "\n";
                    richTextBox2.Text = emails;

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

         

            string wordpress = textBox1.Text + "/license.txt";
            string wordpress2 = textBox1.Text + "/xmlrpc.php?rsd";
            string joomla = textBox1.Text + "/README.txt";
            string joomla2 = textBox1.Text + "/robots.txt";
            string joomla3 = textBox1.Text + "/language/en-GB/en-GB.xml";
            string drupal = textBox1.Text + "/CHANGELOG.txt";
            if (UrlIsValid(wordpress) == true || UrlIsValid(wordpress2) == true)
            {
                using (WebClient clone = new WebClient())
                {
                    string cloned = clone.DownloadString(wordpress);
                    string cloned2 = clone.DownloadString(wordpress2);
                    if (cloned.Contains("WordPress") || cloned2.Contains("WordPress"))
                    {
                        MessageBox.Show("cms is wordpress", "youhacker panel finder");
                    }


                }
            }


            else if (UrlIsValid(joomla) == true || UrlIsValid(joomla2) == true || UrlIsValid(joomla3) == true)
            {
                using (WebClient clone = new WebClient())
                {
                    string cloned = clone.DownloadString(joomla);
                    string cloned2 = clone.DownloadString(joomla2);
                    string cloned3 = clone.DownloadString(joomla3);
                    if (cloned.Contains("Joomla") || cloned2.Contains("Joomla") || cloned3.Contains("Joomla"))
                    {
                        MessageBox.Show("joomla", "youhacker panel finder");
                    }



                }

            }
            else if (UrlIsValid(drupal) == true)
            {
                using (WebClient clone = new WebClient())
                {
                    string cloned = clone.DownloadString(drupal);
                    if (cloned.Contains("Drupal"))
                    {
                        MessageBox.Show("Drupal", "youhacker panel finder");

                    }

                }
           
               

            }
            else
            {
                MessageBox.Show("Can t Detect CMS", "youhacker panel finder");
            }
        }
    }
}
    

