﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace Dota_Dupes_GUI
{
    public partial class Form1 : Form
    {
        string steamKey = "26AF57E923E0D8E5E63C006BA68D78FE";
        string steamID = "76561198022346122";
        public Form1()
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Resourses\");
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            InitializeComponent();
            //textBox1.Text = "76561198022346122";
            //textBox2.Text = "26AF57E923E0D8E5E63C006BA68D78FE";
            //pictureBox1.Load("http://media.steampowered.com/apps/570/icons/econ/testitem_slot_empty.57f48103eed57e2cfa3d041f979f96a6869c1d22.png");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {            
            steamID = textBox1.Text;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var inventory = serializer.Deserialize<dynamic>(new WebClient().DownloadString("http://api.steampowered.com/IEconItems_570/GetPlayerItems/v0001/?key=" + steamKey + "&steamid=" + steamID))["result"]["items"];
            var store = serializer.Deserialize<dynamic>(new WebClient().DownloadString("http://api.steampowered.com/IEconItems_570/GetSchema/v1/?key=2916AE1EA8E0247B2E2EB44ADF2A4ADC"))["result"]["items"];
            List<int> items = new List<int>();
            List<int> dupes = new List<int>();

            foreach (var item in inventory)
            {
                if (items.Contains(item["defindex"]) && !dupes.Contains(item["defindex"]))
                {
                    dupes.Add(item["defindex"]);
                }
                else
                {
                    items.Add(item["defindex"]);
                }
            }

            #region Console Stuff
            foreach (var defindex in dupes)
            {
                foreach (var item in store)
                {
                    if (item["defindex"] == defindex && item["item_class"] != "supply_crate")
                    {
                        Console.WriteLine(item["name"]);
                    }
                }
            } 
            #endregion
            imageList1.ImageSize = new Size(256, 170);
            List<Image> images = new List<Image>();
            List<String> imageURL = new List<String>();
            foreach (var defindex in dupes)
            {
                foreach (var item in store)
                {
                    if (item["defindex"] == defindex && item["item_class"] != "supply_crate")
                    {
                        string url = item["image_url"];
                        // Create an instance of WebClient
                        WebClient client = new WebClient();
                        // Hookup DownloadFileCompleted Event
                        //client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);

                        // Start the download and copy the file to c:\temp
                        client.DownloadFileAsync(new Uri(url), Directory.GetCurrentDirectory() + @"\Resourses\" + item["name"]);
                    }
                }
            }
            Completed();
        }

        private void Completed()
        {
            string path = Directory.GetCurrentDirectory() + @"\Resourses";

            var query = from f in Directory.GetFiles(path, "*.*")
                        select new { Path = f, FileName = Path.GetFileName(f) };

            var files = query.ToList();
            pictureBox1.DataBindings.Add("ImageLocation", files, "Path");
            label3.DataBindings.Add("Text", files, "FileName");
            dataRepeater1.ItemHeaderVisible = false; // to hide headers
            dataRepeater1.DataSource = files;
        }

        #region Useless
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataRepeater1_CurrentItemIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        } 
        #endregion

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

    }
}
