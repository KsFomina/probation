namespace Ex4
{
    partial class CombinationsParty
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CombinationsParty));
            comboBoxParties = new ComboBox();
            label1 = new Label();
            listBoxParties = new ListBox();
            StartButton = new Button();
            openFileDialog1 = new OpenFileDialog();
            toolStrip1 = new ToolStrip();
            UploadButton = new ToolStripButton();
            toolTip1 = new ToolTip(components);
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxParties
            // 
            comboBoxParties.FormattingEnabled = true;
            comboBoxParties.Location = new Point(12, 42);
            comboBoxParties.Name = "comboBoxParties";
            comboBoxParties.Size = new Size(237, 23);
            comboBoxParties.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 24);
            label1.Name = "label1";
            label1.Size = new Size(106, 15);
            label1.TabIndex = 1;
            label1.Text = "Выберите партию";
            // 
            // listBoxParties
            // 
            listBoxParties.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxParties.FormattingEnabled = true;
            listBoxParties.ItemHeight = 15;
            listBoxParties.Location = new Point(12, 71);
            listBoxParties.Name = "listBoxParties";
            listBoxParties.Size = new Size(387, 259);
            listBoxParties.TabIndex = 2;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(255, 42);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(75, 23);
            StartButton.TabIndex = 3;
            StartButton.Text = "Запустить";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { UploadButton });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(411, 25);
            toolStrip1.TabIndex = 4;
            toolStrip1.Text = "toolStrip1";
            // 
            // UploadButton
            // 
            UploadButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            UploadButton.Image = (Image)resources.GetObject("UploadButton.Image");
            UploadButton.ImageTransparentColor = Color.Magenta;
            UploadButton.Name = "UploadButton";
            UploadButton.Size = new Size(23, 22);
            UploadButton.Tag = "Выбрать файл";
            UploadButton.Text = "выбрать файл";
            UploadButton.Click += UploadButton_Click;
            // 
            // CombinationsParty
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(411, 337);
            Controls.Add(toolStrip1);
            Controls.Add(StartButton);
            Controls.Add(listBoxParties);
            Controls.Add(label1);
            Controls.Add(comboBoxParties);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CombinationsParty";
            Text = "Списки возможных партий";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxParties;

        private Label label1;
        private ListBox listBoxParties;
        private Button StartButton;
        private OpenFileDialog openFileDialog1;
        private ToolStrip toolStrip1;
        private ToolStripButton UploadButton;
        private ToolTip toolTip1;
    }
}
