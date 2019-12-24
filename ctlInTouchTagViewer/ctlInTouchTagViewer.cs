using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

namespace ctlInTouchTagViewer
{
    internal struct InTouchTag
    {
        public string name;
        public string comment;
        public string group;
        public string EU;
        public float minEU;
        public float maxEU;
        public bool favorite;
    }

    public partial class ctlInTouchTagViewer : UserControl
    {
        private const string ALL_TAGS = "ВСЕ ПАРАМЕТРЫ";
        private const string FAV_TAGS = "ИЗБРАННЫЕ ПАРАМЕТРЫ";

        private List<InTouchTag> ltags;
        private List<string> lgroups;
        private List<string> lfav;
        private String srcFile = @"D:\WORK\2017\ПСП Михайловская\export.csv";
        private String favFile = "favtags.csv";
        private bool _expertMode = false;
        private string _expertStr = "_man _man_en _chnsignal _deactiv _invert _delayfront _filter_t _elect _filter_on";
        private bool _grouping = true;
        private int _search_result_count = 20;

        [Description("Макс количество результатов поиска")]
        public int Search_result_count
        {
            get
            {
                return _search_result_count;
            }
            set
            {
                _search_result_count = value;
            }
        }

        [Description("Будет ли разбиение на группы по шаблону [ИМЯ АГРЕГАТА: Название параметра]")]
        public bool Groupring
        {
            get
            {
                return _grouping;
            }
            set
            {
                _grouping = value;

                //scMain.Panel1Collapsed = !value;
                if (lbGroup.Items.Count > 0)
                    lbGroup.SelectedIndex = 0;
            }
        }

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

        [Description("Имя выделенного тэга")]
        public String SelectedTag_Name
        {
            get
            {
                return lvTags.SelectedItems.Count > 0 ? lvTags.SelectedItems[0].SubItems[1].Text : "";
            }
        }

        [Description("Коммент тэга")]
        public String SelectedTag_Comment
        {
            get
            {
                return lvTags.SelectedItems.Count > 0 ? lvTags.SelectedItems[0].Text : "";
            }
        }

        [Description("Ед.изм тэга")]
        public String SelectedTag_EU
        {
            get
            {
                //return lvTags.SelectedItems.Count > 0 ? ltags.Where(item=>item.name == lvTags.SelectedItems[0].Text).ToList()[0].EU : "";
                return lvTags.SelectedItems.Count > 0 ? lvTags.SelectedItems[0].SubItems[2].Text : "";
            }
        }

        [Description("Мин шкалы тэга")]
        public float SelectedTag_Min_EU
        {
            get
            {
                //return lvTags.SelectedItems.Count > 0 ? ltags.Where(item => item.name == lvTags.SelectedItems[0].Text).ToList()[0].MinEU : 0;
                return lvTags.SelectedItems.Count > 0 ? Convert.ToSingle(lvTags.SelectedItems[0].SubItems[3].Text) : 0;
            }
        }

        [Description("Макс шкалы тэга")]
        public float SelectedTag_Max_EU
        {
            get
            {
                //return lvTags.SelectedItems.Count > 0 ? ltags.Where(item => item.name == lvTags.SelectedItems[0].Text).ToList()[0].MaxEU : 100;
                return lvTags.SelectedItems.Count > 0 ? Convert.ToSingle(lvTags.SelectedItems[0].SubItems[4].Text) : 100;
            }
        }

        [Description("Путь до файла с выгрузкой базы тэгов InTouch.")]
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

        [Description("Тэги относящиеся к эспертному режиму (резделенные пробелом)")]
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

        [Description("Режим эксперт позволяет оботразить служебные тэги")]
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

                lvTags.Columns[1].Width = Convert.ToInt32(_expertMode) * 150;
            }
        }

        public ctlInTouchTagViewer()
        {
            InitializeComponent();

            ltags = new List<InTouchTag>();
            lgroups = new List<string>();
            lfav = new List<string>();
            label1.Text = "v." + Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
                foreach (InTouchTag s in ltags.Where(item => ((item.group == lbGroup.SelectedItems[0].ToString() + ":")
                                                                    || (lbGroup.SelectedItems[0].ToString() == ALL_TAGS) // Все тэгт
                                                                    || ((lbGroup.SelectedItems[0].ToString() == FAV_TAGS) && item.favorite)) // Избранные
                                                                && !checkExpertStr(item.name)
                                                             ).OrderBy(item => item.comment))
                {
                    ListViewItem nitem;
                    nitem = lvTags.Items.Add(s.comment);
                    nitem.ForeColor = s.favorite ? System.Drawing.Color.Blue : System.Drawing.Color.Black;
                    nitem.SubItems.Add(s.name);
                    nitem.SubItems.Add(s.EU);
                    nitem.SubItems.Add(s.minEU.ToString());
                    nitem.SubItems.Add(s.maxEU.ToString());
                    nitem.Checked = s.favorite;
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
                                                                            ).OrderBy(item => item.comment).Take(Search_result_count))

                    {
                        ListViewItem nitem;
                        nitem = lvTags.Items.Add(s.comment);
                        nitem.Font = new System.Drawing.Font(
                                        nitem.Font.FontFamily, nitem.Font.Size, s.favorite ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular);
                        nitem.SubItems.Add(s.name);
                        nitem.SubItems.Add(s.EU);
                        nitem.SubItems.Add(s.minEU.ToString());
                        nitem.SubItems.Add(s.maxEU.ToString());
                        nitem.Checked = s.favorite;
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
            lfav.Clear();
            lbGroup.Items.Clear();
            lvTags.Items.Clear();

            try
            {
                using (StreamReader sr = new StreamReader(new DirectoryInfo(srcFile).Parent.FullName + @"\" + favFile, System.Text.Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        lfav.Add(line.Trim());
                    }
                }
            }
            catch (System.Exception ex)
            {
#if DEBUG

                MessageBox.Show(ex.Message);
#endif
            }

            try
            {
                Microsoft.VisualBasic.FileIO.TextFieldParser parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(srcFile, System.Text.Encoding.Default);
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
                    if (row[0].StartsWith(":MemoryDisc"))
                    { //13-1
                        ttype = 4;
                        continue;
                    }
                    else
                    if (row[0].StartsWith(":"))
                    {
                        ttype = 0;
                    }

                    if (ttype == 0)
                        continue;

                    string s = row[17 * Convert.ToInt32(ttype == 1) + 46 * Convert.ToInt32(ttype == 2 || ttype == 3) + 12 * Convert.ToInt32(ttype == 4)];

                    if (s.IndexOf(":") < 0 && Groupring)
                        continue;

                    ltags.Add(new InTouchTag
                    {
                        name = row[0],
                        comment = s,
                        group = Groupring ? s.Substring(0, s.IndexOf(":") + 1) : "",
                        EU = ttype > 1 ? row[10] : "",
                        maxEU = (ttype == 1) ? 2 : Convert.ToSingle(row[13]),
                        minEU = (ttype == 1) ? -1 : Convert.ToSingle(row[12]),
                        favorite = lfav.Contains(row[0])
                    });

                    if (Groupring && !lgroups.Exists(delegate (String text) { return text == ltags[ltags.Count - 1].group; }))
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
            lbGroup.Items.Add(ALL_TAGS);
            lbGroup.Items.Add(FAV_TAGS);
            try
            {
                foreach (string s in lgroups.OrderBy(item => item))
                    lbGroup.Items.Add(s.Substring(0, s.Length - 1));
            }
            finally
            {
                lbGroup.EndUpdate();
            }
        }

        public void SaveFav()
        {
            if (ltags.Count > 0)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(new DirectoryInfo(srcFile).Parent.FullName + @"\" + favFile, false))
                    {
                        foreach (InTouchTag tag in ltags.Where(item => item.favorite).ToList())
                        {
                            sw.WriteLine(tag.name);
                        }
                    }
                }
                catch (System.Exception ex)
                {
#if DEBUG

                    MessageBox.Show(ex.Message);
#endif
                }
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
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void lvTags_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.SubItems.Count != 5)
                return;
            int i = ltags.FindIndex(item => item.name == e.Item.SubItems[1].Text);
            if (i != -1)
            {
                e.Item.ForeColor = e.Item.Checked ? System.Drawing.Color.Blue : System.Drawing.Color.Black;

                ltags[i] = new InTouchTag
                {
                    name = ltags[i].name,
                    comment = ltags[i].comment,
                    group = ltags[i].group,
                    maxEU = ltags[i].maxEU,
                    minEU = ltags[i].minEU,
                    EU = ltags[i].EU,
                    favorite = e.Item.Checked
                };
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }
    }
}