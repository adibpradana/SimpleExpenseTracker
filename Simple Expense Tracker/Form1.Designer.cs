namespace Simple_Expense_Tracker
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            Pemasukan = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            Nama_Kebutuhan = new TextBox();
            label4 = new Label();
            dataGridView1 = new DataGridView();
            Kebutuhann = new DataGridViewTextBoxColumn();
            Prioritass = new DataGridViewTextBoxColumn();
            Persentasee = new DataGridViewTextBoxColumn();
            Nominall = new DataGridViewTextBoxColumn();
            Pengeluarann = new DataGridViewTextBoxColumn();
            Tambah = new Button();
            TinggiPrioritas = new RadioButton();
            SedangPrioritas = new RadioButton();
            RendahPrioritas = new RadioButton();
            PresentasePilihan = new ComboBox();
            label5 = new Label();
            CetakDoc = new Button();
            ResetTabel = new Button();
            label6 = new Label();
            label7 = new Label();
            comboBox1 = new ComboBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            chartAlokasi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label8 = new Label();
            btnHapus = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartAlokasi).BeginInit();
            SuspendLayout();
            // 
            // Pemasukan
            // 
            Pemasukan.Location = new Point(207, 147);
            Pemasukan.Name = "Pemasukan";
            Pemasukan.Size = new Size(247, 27);
            Pemasukan.TabIndex = 0;
            Pemasukan.TextChanged += Pemasukan_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(119, 154);
            label1.Name = "label1";
            label1.Size = new Size(82, 20);
            label1.TabIndex = 1;
            label1.Text = "Pemasukan";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Wide Latin", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.LightSeaGreen;
            label2.Location = new Point(90, 49);
            label2.Name = "label2";
            label2.Size = new Size(816, 34);
            label2.TabIndex = 2;
            label2.Text = "SIMPLE EXPENSE TRACKER";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(119, 213);
            label3.Name = "label3";
            label3.Size = new Size(136, 20);
            label3.TabIndex = 3;
            label3.Text = "Tambah Kebutuhan";
            label3.Click += label3_Click;
            // 
            // Nama_Kebutuhan
            // 
            Nama_Kebutuhan.Location = new Point(119, 236);
            Nama_Kebutuhan.Name = "Nama_Kebutuhan";
            Nama_Kebutuhan.Size = new Size(263, 27);
            Nama_Kebutuhan.TabIndex = 4;
            Nama_Kebutuhan.TextChanged += Nama_Kebutuhan_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(119, 364);
            label4.Name = "label4";
            label4.Size = new Size(126, 20);
            label4.TabIndex = 5;
            label4.Text = "Daftar Kebutuhan";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Kebutuhann, Prioritass, Persentasee, Nominall, Pengeluarann });
            dataGridView1.Location = new Point(119, 387);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(674, 174);
            dataGridView1.TabIndex = 6;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // Kebutuhann
            // 
            Kebutuhann.HeaderText = "Kebutuhan";
            Kebutuhann.MinimumWidth = 6;
            Kebutuhann.Name = "Kebutuhann";
            Kebutuhann.ReadOnly = true;
            Kebutuhann.Width = 125;
            // 
            // Prioritass
            // 
            Prioritass.HeaderText = "Prioritas";
            Prioritass.MinimumWidth = 6;
            Prioritass.Name = "Prioritass";
            Prioritass.ReadOnly = true;
            Prioritass.Width = 125;
            // 
            // Persentasee
            // 
            Persentasee.HeaderText = "Persentase";
            Persentasee.MinimumWidth = 6;
            Persentasee.Name = "Persentasee";
            Persentasee.ReadOnly = true;
            Persentasee.Width = 125;
            // 
            // Nominall
            // 
            Nominall.HeaderText = "Nominal";
            Nominall.MinimumWidth = 6;
            Nominall.Name = "Nominall";
            Nominall.ReadOnly = true;
            Nominall.Width = 125;
            // 
            // Pengeluarann
            // 
            Pengeluarann.HeaderText = "Pengeluaran";
            Pengeluarann.MinimumWidth = 6;
            Pengeluarann.Name = "Pengeluarann";
            Pengeluarann.ReadOnly = true;
            Pengeluarann.Width = 125;
            // 
            // Tambah
            // 
            Tambah.BackColor = Color.Turquoise;
            Tambah.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            Tambah.ForeColor = Color.Black;
            Tambah.Location = new Point(786, 230);
            Tambah.Name = "Tambah";
            Tambah.Size = new Size(79, 38);
            Tambah.TabIndex = 7;
            Tambah.Text = "Tambah";
            Tambah.UseVisualStyleBackColor = false;
            Tambah.Click += Tambah_Click;
            // 
            // TinggiPrioritas
            // 
            TinggiPrioritas.AutoSize = true;
            TinggiPrioritas.BackColor = Color.Transparent;
            TinggiPrioritas.Location = new Point(119, 269);
            TinggiPrioritas.Name = "TinggiPrioritas";
            TinggiPrioritas.Size = new Size(72, 24);
            TinggiPrioritas.TabIndex = 8;
            TinggiPrioritas.TabStop = true;
            TinggiPrioritas.Text = "Tinggi";
            TinggiPrioritas.UseVisualStyleBackColor = false;
            TinggiPrioritas.CheckedChanged += TinggiPrioritas_CheckedChanged;
            // 
            // SedangPrioritas
            // 
            SedangPrioritas.AutoSize = true;
            SedangPrioritas.BackColor = Color.Transparent;
            SedangPrioritas.Location = new Point(207, 269);
            SedangPrioritas.Name = "SedangPrioritas";
            SedangPrioritas.Size = new Size(80, 24);
            SedangPrioritas.TabIndex = 9;
            SedangPrioritas.TabStop = true;
            SedangPrioritas.Text = "Sedang";
            SedangPrioritas.UseVisualStyleBackColor = false;
            SedangPrioritas.CheckedChanged += SedangPrioritas_CheckedChanged;
            // 
            // RendahPrioritas
            // 
            RendahPrioritas.AutoSize = true;
            RendahPrioritas.BackColor = Color.Transparent;
            RendahPrioritas.Location = new Point(302, 269);
            RendahPrioritas.Name = "RendahPrioritas";
            RendahPrioritas.Size = new Size(80, 24);
            RendahPrioritas.TabIndex = 10;
            RendahPrioritas.TabStop = true;
            RendahPrioritas.Text = "Rendah";
            RendahPrioritas.UseVisualStyleBackColor = false;
            RendahPrioritas.CheckedChanged += RendahPrioritas_CheckedChanged;
            // 
            // PresentasePilihan
            // 
            PresentasePilihan.FormattingEnabled = true;
            PresentasePilihan.Location = new Point(416, 235);
            PresentasePilihan.Name = "PresentasePilihan";
            PresentasePilihan.Size = new Size(151, 28);
            PresentasePilihan.TabIndex = 11;
            PresentasePilihan.SelectedIndexChanged += PresentasePilihan_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Location = new Point(416, 214);
            label5.Name = "label5";
            label5.Size = new Size(78, 20);
            label5.TabIndex = 12;
            label5.Text = "Persentase";
            // 
            // CetakDoc
            // 
            CetakDoc.BackColor = Color.Turquoise;
            CetakDoc.BackgroundImageLayout = ImageLayout.Stretch;
            CetakDoc.FlatAppearance.BorderSize = 0;
            CetakDoc.FlatStyle = FlatStyle.Flat;
            CetakDoc.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            CetakDoc.Location = new Point(799, 387);
            CetakDoc.Name = "CetakDoc";
            CetakDoc.Size = new Size(120, 38);
            CetakDoc.TabIndex = 13;
            CetakDoc.Text = "Simpan ke pdf";
            CetakDoc.UseVisualStyleBackColor = false;
            CetakDoc.Click += CetakDoc_Click;
            // 
            // ResetTabel
            // 
            ResetTabel.BackColor = Color.Turquoise;
            ResetTabel.BackgroundImageLayout = ImageLayout.Stretch;
            ResetTabel.FlatAppearance.BorderSize = 0;
            ResetTabel.FlatStyle = FlatStyle.Flat;
            ResetTabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ResetTabel.Location = new Point(799, 448);
            ResetTabel.Name = "ResetTabel";
            ResetTabel.Size = new Size(120, 34);
            ResetTabel.TabIndex = 14;
            ResetTabel.Text = "Reset";
            ResetTabel.UseVisualStyleBackColor = false;
            ResetTabel.Click += ResetTabel_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Location = new Point(119, 569);
            label6.Name = "label6";
            label6.Size = new Size(105, 20);
            label6.TabIndex = 15;
            label6.Text = "Total Alokasi : ";
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Location = new Point(602, 214);
            label7.Name = "label7";
            label7.Size = new Size(125, 20);
            label7.TabIndex = 16;
            label7.Text = "Jenis Pengeluaran";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Harian", "Mingguan", "Bulanan" });
            comboBox1.Location = new Point(602, 235);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 17;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // chartAlokasi
            // 
            chartArea1.Name = "ChartArea1";
            chartAlokasi.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartAlokasi.Legends.Add(legend1);
            chartAlokasi.Location = new Point(964, 186);
            chartAlokasi.Name = "chartAlokasi";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartAlokasi.Series.Add(series1);
            chartAlokasi.Size = new Size(375, 375);
            chartAlokasi.TabIndex = 18;
            chartAlokasi.Text = "chart1";
            chartAlokasi.Click += chartAlokasi_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Location = new Point(964, 163);
            label8.Name = "label8";
            label8.Size = new Size(100, 20);
            label8.TabIndex = 19;
            label8.Text = "Grafik Alokasi";
            // 
            // btnHapus
            // 
            btnHapus.BackColor = Color.Turquoise;
            btnHapus.BackgroundImageLayout = ImageLayout.Stretch;
            btnHapus.FlatAppearance.BorderSize = 0;
            btnHapus.FlatStyle = FlatStyle.Flat;
            btnHapus.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnHapus.Location = new Point(799, 506);
            btnHapus.Name = "btnHapus";
            btnHapus.Size = new Size(120, 34);
            btnHapus.TabIndex = 20;
            btnHapus.Text = "Hapus Baris";
            btnHapus.UseVisualStyleBackColor = false;
            btnHapus.Click += btnHapus_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1371, 609);
            Controls.Add(btnHapus);
            Controls.Add(label8);
            Controls.Add(chartAlokasi);
            Controls.Add(comboBox1);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(ResetTabel);
            Controls.Add(CetakDoc);
            Controls.Add(label5);
            Controls.Add(PresentasePilihan);
            Controls.Add(RendahPrioritas);
            Controls.Add(SedangPrioritas);
            Controls.Add(TinggiPrioritas);
            Controls.Add(Tambah);
            Controls.Add(dataGridView1);
            Controls.Add(label4);
            Controls.Add(Nama_Kebutuhan);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Pemasukan);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Paint += Form1_Paint;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartAlokasi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Pemasukan;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox Nama_Kebutuhan;
        private Label label4;
        private DataGridView dataGridView1;
        private Button Tambah;
        private RadioButton TinggiPrioritas;
        private RadioButton SedangPrioritas;
        private RadioButton RendahPrioritas;
        private ComboBox PresentasePilihan;
        private Label label5;
        private Button CetakDoc;
        private Button ResetTabel;
        private Label label6;
        private Label label7;
        private ComboBox comboBox1;
        private DataGridViewTextBoxColumn Kebutuhann;
        private DataGridViewTextBoxColumn Prioritass;
        private DataGridViewTextBoxColumn Persentasee;
        private DataGridViewTextBoxColumn Nominall;
        private DataGridViewTextBoxColumn Pengeluarann;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAlokasi;
        private Label label8;
        private Button btnHapus;
    }
}
