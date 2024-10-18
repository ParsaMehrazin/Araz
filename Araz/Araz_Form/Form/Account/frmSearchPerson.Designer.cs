namespace Araz_Form.Form
{
    partial class frmSearchPerson
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcPersonList = new DevExpress.XtraGrid.GridControl();
            this.gvPersonList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnGridEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnPrintGridList = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnGridDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPersonList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPersonList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGridEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintGridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGridDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcPersonList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(915, 521);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcPersonList
            // 
            this.gcPersonList.Location = new System.Drawing.Point(12, 12);
            this.gcPersonList.MainView = this.gvPersonList;
            this.gcPersonList.Name = "gcPersonList";
            this.gcPersonList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnPrintGridList,
            this.btnGridDelete,
            this.btnGridEdit});
            this.gcPersonList.Size = new System.Drawing.Size(891, 497);
            this.gcPersonList.TabIndex = 8;
            this.gcPersonList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPersonList});
            // 
            // gvPersonList
            // 
            this.gvPersonList.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gvPersonList.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvPersonList.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvPersonList.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvPersonList.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvPersonList.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvPersonList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvPersonList.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvPersonList.Appearance.OddRow.BackColor = System.Drawing.Color.LightCyan;
            this.gvPersonList.Appearance.OddRow.Options.UseBackColor = true;
            this.gvPersonList.Appearance.OddRow.Options.UseTextOptions = true;
            this.gvPersonList.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvPersonList.Appearance.OddRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvPersonList.Appearance.Row.Options.UseTextOptions = true;
            this.gvPersonList.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvPersonList.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvPersonList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn14,
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn6,
            this.gridColumn5,
            this.gridColumn18,
            this.gridColumn4,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn13,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.gvPersonList.GridControl = this.gcPersonList;
            this.gvPersonList.IndicatorWidth = 35;
            this.gvPersonList.Name = "gvPersonList";
            this.gvPersonList.OptionsView.EnableAppearanceOddRow = true;
            this.gvPersonList.OptionsView.ShowAutoFilterRow = true;
            this.gvPersonList.OptionsView.ShowFooter = true;
            this.gvPersonList.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvPersonList_CustomDrawRowIndicator);
            this.gvPersonList.DoubleClick += new System.EventHandler(this.gvPersonList_DoubleClick);
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "سمت";
            this.gridColumn14.FieldName = "RoleName";
            this.gridColumn14.MaxWidth = 300;
            this.gridColumn14.MinWidth = 150;
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsColumn.AllowFocus = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 3;
            this.gridColumn14.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "جنسیت";
            this.gridColumn3.FieldName = "Sex";
            this.gridColumn3.MaxWidth = 65;
            this.gridColumn3.MinWidth = 65;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Sex", "تعداد : {0}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 65;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "نام";
            this.gridColumn1.FieldName = "PersonName";
            this.gridColumn1.MaxWidth = 300;
            this.gridColumn1.MinWidth = 150;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 150;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "نام خانوادگی";
            this.gridColumn2.FieldName = "PersonLastName";
            this.gridColumn2.MaxWidth = 300;
            this.gridColumn2.MinWidth = 150;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "تصحیلات";
            this.gridColumn6.FieldName = "Education";
            this.gridColumn6.MaxWidth = 300;
            this.gridColumn6.MinWidth = 150;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 150;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "کد ملی";
            this.gridColumn5.FieldName = "NationalCode";
            this.gridColumn5.MaxWidth = 300;
            this.gridColumn5.MinWidth = 100;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 100;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "تاریخ تولد";
            this.gridColumn18.DisplayFormat.FormatString = "yyyy-mm-dd";
            this.gridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn18.FieldName = "AgeDate";
            this.gridColumn18.MaxWidth = 200;
            this.gridColumn18.MinWidth = 100;
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.AllowEdit = false;
            this.gridColumn18.OptionsColumn.AllowFocus = false;
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 6;
            this.gridColumn18.Width = 100;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "سن";
            this.gridColumn4.FieldName = "PersonAge";
            this.gridColumn4.MaxWidth = 80;
            this.gridColumn4.MinWidth = 60;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 7;
            this.gridColumn4.Width = 60;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "موبایل";
            this.gridColumn7.FieldName = "Mobile";
            this.gridColumn7.MaxWidth = 200;
            this.gridColumn7.MinWidth = 100;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 8;
            this.gridColumn7.Width = 100;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "تلفن";
            this.gridColumn8.FieldName = "Tel";
            this.gridColumn8.MaxWidth = 200;
            this.gridColumn8.MinWidth = 100;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 9;
            this.gridColumn8.Width = 100;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "ایمیل";
            this.gridColumn13.FieldName = "Email";
            this.gridColumn13.MaxWidth = 400;
            this.gridColumn13.MinWidth = 150;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 10;
            this.gridColumn13.Width = 150;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "استان";
            this.gridColumn9.FieldName = "Province";
            this.gridColumn9.MaxWidth = 300;
            this.gridColumn9.MinWidth = 150;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 11;
            this.gridColumn9.Width = 150;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "شهر";
            this.gridColumn10.FieldName = "CityName";
            this.gridColumn10.MaxWidth = 300;
            this.gridColumn10.MinWidth = 150;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 12;
            this.gridColumn10.Width = 150;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "آدرس";
            this.gridColumn11.FieldName = "Address";
            this.gridColumn11.MaxWidth = 600;
            this.gridColumn11.MinWidth = 150;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 13;
            this.gridColumn11.Width = 150;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "کدپستی";
            this.gridColumn12.FieldName = "PostalCode";
            this.gridColumn12.MaxWidth = 200;
            this.gridColumn12.MinWidth = 100;
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 14;
            this.gridColumn12.Width = 100;
            // 
            // btnGridEdit
            // 
            this.btnGridEdit.AutoHeight = false;
            editorButtonImageOptions3.Image = global::Araz_Form.Properties.Resources.editdatasource_16x16;
            this.btnGridEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnGridEdit.Name = "btnGridEdit";
            this.btnGridEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // btnPrintGridList
            // 
            this.btnPrintGridList.AutoHeight = false;
            editorButtonImageOptions1.Image = global::Araz_Form.Properties.Resources.printer_16x16;
            this.btnPrintGridList.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnPrintGridList.Name = "btnPrintGridList";
            this.btnPrintGridList.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // btnGridDelete
            // 
            this.btnGridDelete.AutoHeight = false;
            editorButtonImageOptions2.Image = global::Araz_Form.Properties.Resources.cancel_16x16;
            this.btnGridDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnGridDelete.Name = "btnGridDelete";
            this.btnGridDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(915, 521);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcPersonList;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(895, 501);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmSearchPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 521);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchPerson";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "جستجوی شخص";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSearchPerson_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPersonList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPersonList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGridEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintGridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGridDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcPersonList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPersonList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnGridEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnPrintGridList;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnGridDelete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}