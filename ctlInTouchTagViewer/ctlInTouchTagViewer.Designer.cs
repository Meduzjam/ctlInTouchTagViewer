namespace ctlInTouchTagViewer
{
    partial class ctlInTouchTagViewer
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.gbGroup = new System.Windows.Forms.GroupBox();
            this.lbGroup = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvTags = new System.Windows.Forms.ListView();
            this.cTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbExpertMode = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.gbGroup.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Location = new System.Drawing.Point(3, 69);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.gbGroup);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.groupBox3);
            this.scMain.Size = new System.Drawing.Size(762, 366);
            this.scMain.SplitterDistance = 189;
            this.scMain.TabIndex = 2;
            // 
            // gbGroup
            // 
            this.gbGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGroup.Controls.Add(this.lbGroup);
            this.gbGroup.Location = new System.Drawing.Point(3, 3);
            this.gbGroup.Name = "gbGroup";
            this.gbGroup.Size = new System.Drawing.Size(183, 360);
            this.gbGroup.TabIndex = 1;
            this.gbGroup.TabStop = false;
            this.gbGroup.Text = "Группы";
            // 
            // lbGroup
            // 
            this.lbGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGroup.FormattingEnabled = true;
            this.lbGroup.Location = new System.Drawing.Point(9, 19);
            this.lbGroup.Name = "lbGroup";
            this.lbGroup.Size = new System.Drawing.Size(168, 329);
            this.lbGroup.TabIndex = 0;
            this.lbGroup.SelectedIndexChanged += new System.EventHandler(this.lbGroup_SelectedIndexChanged_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lvTags);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(563, 360);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Параметры";
            // 
            // lvTags
            // 
            this.lvTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cTag,
            this.cName});
            this.lvTags.FullRowSelect = true;
            this.lvTags.GridLines = true;
            this.lvTags.HideSelection = false;
            this.lvTags.Location = new System.Drawing.Point(6, 19);
            this.lvTags.MultiSelect = false;
            this.lvTags.Name = "lvTags";
            this.lvTags.ShowGroups = false;
            this.lvTags.Size = new System.Drawing.Size(551, 329);
            this.lvTags.TabIndex = 0;
            this.lvTags.UseCompatibleStateImageBehavior = false;
            this.lvTags.View = System.Windows.Forms.View.Details;
            // 
            // cTag
            // 
            this.cTag.Text = "Тэг";
            this.cTag.Width = 0;
            // 
            // cName
            // 
            this.cName.Text = "Наименование";
            this.cName.Width = 500;
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(6, 19);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(652, 20);
            this.tbSearch.TabIndex = 3;
            this.tbSearch.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbSearch);
            this.groupBox1.Location = new System.Drawing.Point(6, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(664, 49);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поиск";
            // 
            // cbExpertMode
            // 
            this.cbExpertMode.AutoSize = true;
            this.cbExpertMode.Location = new System.Drawing.Point(688, 29);
            this.cbExpertMode.Name = "cbExpertMode";
            this.cbExpertMode.Size = new System.Drawing.Size(68, 17);
            this.cbExpertMode.TabIndex = 6;
            this.cbExpertMode.Text = "Эксперт";
            this.cbExpertMode.UseVisualStyleBackColor = true;
            this.cbExpertMode.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(703, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ctlInTouchTagViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbExpertMode);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.scMain);
            this.Name = "ctlInTouchTagViewer";
            this.Size = new System.Drawing.Size(771, 438);
            this.Load += new System.EventHandler(this.ctlInTouchTagViewer_Load_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlInTouchTagViewer_KeyDown);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.gbGroup.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.ListBox lbGroup;
        private System.Windows.Forms.ListView lvTags;
        private System.Windows.Forms.ColumnHeader cTag;
        private System.Windows.Forms.ColumnHeader cName;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.GroupBox gbGroup;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbExpertMode;
        private System.Windows.Forms.Label label1;
    }
}
