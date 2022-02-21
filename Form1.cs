using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

                    var results2 = "found this " + textBox1.Text + line+"\n";
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

      
    }
    }

