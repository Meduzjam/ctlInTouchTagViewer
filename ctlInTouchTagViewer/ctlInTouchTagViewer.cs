﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ctlInTouchTagViewer
{


    struct InTouchTag
    {
        public string name;
        public string comment;
        public string group;
    }


    public partial class ctlInTouchTagViewer : UserControl
    {

        private Microsoft.VisualBasic.FileIO.TextFieldParser parser;
        private List<InTouchTag> ltags;
        private String srcFile = @"D:\WORK\2017\ПСП Михайловская\export.csv";
        private bool simpleMode;



        public String SrcFile
        {
            // Retrieves the value of the private variable colBColor.
            get
            {
                return srcFile;
            }
            // Stores the selected value in the private variable colBColor, and
            // updates the background color of the label control lblDisplay.
            set
            {
                srcFile = value;
            }
        }

        public bool SimpleMode
        {
            // Retrieves the value of the private variable colBColor.
            get
            {
                return simpleMode;
            }
            // Stores the selected value in the private variable colBColor, and
            // updates the background color of the label control lblDisplay.
            set
            {
                simpleMode = value;
            }
        }

        public ctlInTouchTagViewer()
        {
            InitializeComponent();
            ltags = new List<InTouchTag>();


        }


        private void ctlInTouchTagViewer_Load(object sender, EventArgs e)
        {


            int ttype = 0;
            List<string> tlist = new List<string>();
            try
            {
                parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(srcFile, System.Text.Encoding.Default);
                parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                parser.SetDelimiters(new string[] { ";" });

                while (!parser.EndOfData)
                {
                    string[] row = parser.ReadFields();

                    if (row[0].StartsWith(":IODisc"))
                    { //18-1
                        ttype = 1;
                        continue;
                    }
                    else
                    if (row[0].StartsWith(":IOReal"))
                    { //47-1
                        ttype = 2;
                        continue;
                    }
                    else
                    if (row[0].StartsWith(":IOInt"))
                    { //46-1
                        ttype = 3;
                        continue;
                    }
                    else
                    if (row[0].StartsWith(":"))
                    {
                        ttype = 0;
                    }

                    if (ttype == 0)
                        continue;


                    string s = row[17 * Convert.ToInt32(ttype == 1) + 46 * Convert.ToInt32(ttype == 2) + 45 * Convert.ToInt32(ttype == 3)];

                    if (s.IndexOf(":") < 0)
                        continue;

                    ltags.Add(new InTouchTag { name = row[0], comment = s, group = s.Substring(0, s.IndexOf(":") + 1) });

                    

                    if (!tlist.Exists(delegate (String text) { return text == ltags[ltags.Count-1].group; }))
                        tlist.Add(ltags[ltags.Count - 1].group);



                }
            }

            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            lbGroup.BeginUpdate();
            try
            {
                foreach (string s in tlist)
                    lbGroup.Items.Add(s.Substring(0, s.Length - 1));
            }
            finally
            {
                lbGroup.EndUpdate();
            }

        }


        private void lbGroup_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            lvTags.Items.Clear();
            if (lbGroup.SelectedItems.Count == 0)
                return;

            lvTags.BeginUpdate();
            try
            {
                foreach (InTouchTag s in ltags.FindAll(delegate (InTouchTag item) { return item.group.StartsWith(lbGroup.SelectedItems[0] + ":"); }))
                {
                    lvTags.Items.Add(s.name);
                    lvTags.Items[lvTags.Items.Count - 1].SubItems.Add(s.comment);
                }
            }
            finally
            {
                lvTags.EndUpdate();
            }
        }

    }
}