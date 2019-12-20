using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ctlInTouchTagViewer
{


    struct InTouchTag
    {
        public string name;
        public string comment;
        public string group;
        public string EU;
        public float MinEU;
        public float MaxEU;
    }


    public partial class ctlInTouchTagViewer : UserControl
    {


        private Microsoft.VisualBasic.FileIO.TextFieldParser parser;
        private List<InTouchTag> ltags;
        private List<string> lgroups;
        private String srcFile = @"D:\WORK\2017\ПСП Михайловская\export.csv";
        private bool _expertMode = false;
        private string _expertStr = "_man _man_en _chnsignal _deactiv _invert _delayfront _filter_t _elect _filter_on";
        private bool _simple = true;

        private bool checkExpertStr(string item)
        {
            if (_expertMode)
                return false;

            foreach (string s in _expertStr.Split(' '))
            {
                if (item.ToLower().EndsWith(s))
                    return true;
            }
            return false;
        }

        public String SelectedTag_Name
        {
            get
            {
                return lvTags.SelectedItems.Count > 0 ? lvTags.SelectedItems[0].Text : "";
            }
        }

        public String SelectedTag_Comment
        {
            get
            {
                return lvTags.SelectedItems.Count > 0 ? lvTags.SelectedItems[0].SubItems[1].Text : "";
            }
        }

        public String SelectedTag_EU
        {
            get
            {
                //return lvTags.SelectedItems.Count > 0 ? ltags.Where(item=>item.name == lvTags.SelectedItems[0].Text).ToList()[0].EU : "";
                return lvTags.SelectedItems.Count > 0 ? lvTags.SelectedItems[0].SubItems[2].Text : "";
            }
        }

        public float SelectedTag_Min_EU
        {
            get
            {
                //return lvTags.SelectedItems.Count > 0 ? ltags.Where(item => item.name == lvTags.SelectedItems[0].Text).ToList()[0].MinEU : 0;
                return lvTags.SelectedItems.Count > 0 ? Convert.ToSingle(lvTags.SelectedItems[0].SubItems[3].Text) : 0;
            }
        }

        public float SelectedTag_Max_EU
        {
            get
            {
                //return lvTags.SelectedItems.Count > 0 ? ltags.Where(item => item.name == lvTags.SelectedItems[0].Text).ToList()[0].MaxEU : 100;
                return lvTags.SelectedItems.Count > 0 ? Convert.ToSingle(lvTags.SelectedItems[0].SubItems[4].Text) : 100;
            }
        }




        public String SrcFile
        {
            get
            {
                return srcFile;
            }
            set
            {
                srcFile = value;
            }
        }

        public String ExpertStr
        {
            get
            {
                return _expertStr;
            }

            set
            {
                _expertStr = value;
            }
        }

        public bool ExpertMode
        {
            // Retrieves the value of the private variable colBColor.
            get
            {
                return _expertMode;
            }
            // Stores the selected value in the private variable colBColor, and
            // updates the background color of the label control lblDisplay.
            set
            {
                _expertMode = value;
                if (tbSearch.Text.Length == 0)
                {
                    lbGroupSelect();
                }
                else
                {
                    tbSearchChange();
                }

                lvTags.Columns[0].Width = Convert.ToInt32(_expertMode) * 150;
            }
        }

        public ctlInTouchTagViewer()
        {
            InitializeComponent();

            ltags = new List<InTouchTag>();
            lgroups = new List<string>();
            
        }


        private void lbGroupSelect()
        {
            lvTags.Items.Clear();
            if (lbGroup.SelectedItems.Count == 0)
                return;

            lvTags.BeginUpdate();
            try
            {
                //foreach (InTouchTag s in ltags.FindAll(delegate (InTouchTag item) { return item.group.StartsWith(lbGroup.SelectedItems[0] + ":"); }))
                foreach (InTouchTag s in ltags.Where(item => (item.group == lbGroup.SelectedItems[0] + ":")
                                                                            && !checkExpertStr(item.name)
                                                                            ).OrderBy(item => item.comment))
                {

                    ListViewItem nitem;
                    nitem = lvTags.Items.Add(s.name);
                    nitem.SubItems.Add(s.comment);
                    nitem.SubItems.Add(s.EU);
                    nitem.SubItems.Add(s.MinEU.ToString());
                    nitem.SubItems.Add(s.MaxEU.ToString());

                }
            }
            finally
            {
                lvTags.EndUpdate();
            }
        }

        private void tbSearchChange()
        {
            //if (e.KeyCode != Keys.Enter)
            //    return;

            lvTags.Items.Clear();
            if (tbSearch.Text.Length > 2)
            {
                lvTags.BeginUpdate();




                try
                {
                    //foreach (InTouchTag s in ltags.FindAll(delegate (InTouchTag item) { return item.comment.ToLower().Contains(textBox1.Text.ToLower()); }))

                    //foreach (InTouchTag s in ltags.Where(item => item.comment.ToLower().Contains(textBox1.Text.ToLower())).Take(20))

                    foreach (InTouchTag s in ltags.Where(item => item.comment.ToLower().Contains(tbSearch.Text.ToLower())
                                                                            && !checkExpertStr(item.name)
                                                                            ).OrderBy(item => item.comment).Take(20))



                    {

                        ListViewItem nitem;
                        nitem = lvTags.Items.Add(s.name);
                        nitem.SubItems.Add(s.comment);
                        nitem.SubItems.Add(s.EU);
                        nitem.SubItems.Add(s.MinEU.ToString());
                        nitem.SubItems.Add(s.MaxEU.ToString());
                    }
                }
                finally
                {
                    lvTags.EndUpdate();
                }
            }
        }


        public void LoadData()
        {


            int ttype = 0;
            lgroups.Clear();
            ltags.Clear();
            lbGroup.Items.Clear();
            lvTags.Items.Clear();

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
                    { //47-1  13-1
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

                    ltags.Add(new InTouchTag
                    {
                        name = row[0],
                        comment = s,
                        group = s.Substring(0, s.IndexOf(":") + 1),
                        EU = ttype > 1 ? row[10] : "",
                        MaxEU = (ttype == 1) ? 2 : Convert.ToSingle(row[13]),
                        MinEU = (ttype == 1) ? -1 : Convert.ToSingle(row[12])
                    }); ;

                    if (!lgroups.Exists(delegate (String text) { return text == ltags[ltags.Count - 1].group; }))
                        lgroups.Add(ltags[ltags.Count - 1].group);


                }
                
            }

            catch (System.Exception ex)
            {
                
                #if DEBUG

                    MessageBox.Show(ex.Message);
                #endif
            }


            lbGroup.BeginUpdate();
            try
            {
                foreach (string s in lgroups.OrderBy(item=>item))
                    lbGroup.Items.Add(s.Substring(0, s.Length - 1));
            }
            finally
            {
                lbGroup.EndUpdate();
            }

        }

        private void ctlInTouchTagViewer_Load(object sender, EventArgs e)
        {



        }


        private void lbGroup_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lbGroup.SelectedIndex == -1)
                return;

            tbSearch.Clear();
            lbGroupSelect();

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.TextLength == 0)
                return;

            lbGroup.SelectedIndex = -1;
            tbSearchChange();
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            ExpertMode = cbExpertMode.Checked;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ctlInTouchTagViewer_KeyDown(object sender, KeyEventArgs e)
        {
  

        }

        private void ctlInTouchTagViewer_Load_1(object sender, EventArgs e)
        {
            #if DEBUG
                LoadData();
            #endif
            label1.Text = "v."+Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
