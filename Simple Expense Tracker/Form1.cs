using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using IOPath = System.IO.Path;

namespace Simple_Expense_Tracker
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            InisialisasiDataGridView(); // Setup awal kolom-kolom untuk DataGridView
            LoadDariCSV("alokasi.csv"); // Load data alokasi dari file CSV jika tersedia
            this.Load += Form1_Load; // Menambahkan event handler ketika Form dimuat
            this.Paint += new PaintEventHandler(Form1_Paint); // Menambahkan event handler untuk menggambar latar belakang (gradasi, dll.)
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadGrafikDariCSV("grafik.csv"); // Load data grafik dari file CSV
        }

        // Fungsi untuk mengatur struktur kolom-kolom di DataGridView
        private void InisialisasiDataGridView()
        {
            dataGridView1.Columns.Clear(); // Hapus semua kolom sebelumnya (jika ada)

            // Menambahkan kolom-kolom yang akan ditampilkan pada DataGridView
            dataGridView1.Columns.Add("Kebutuhann", "Kebutuhan");
            dataGridView1.Columns.Add("Prioritass", "Prioritas");
            dataGridView1.Columns.Add("Persentasee", "Persentase");
            dataGridView1.Columns.Add("Nominall", "Nominal");
            dataGridView1.Columns.Add("Pengeluarann", "Pengeluaran");

            dataGridView1.Columns.Add("NominalAsli", "NominalAsli"); // Kolom tersembunyi untuk menyimpan nilai asli
            dataGridView1.Columns["NominalAsli"].Visible = false; // kolom tidak ditampilkan

            // Set kolom selain persentase jadi ReadOnly
            dataGridView1.Columns["Kebutuhann"].ReadOnly = true;
            dataGridView1.Columns["Prioritass"].ReadOnly = true;
            dataGridView1.Columns["Nominall"].ReadOnly = true;
            dataGridView1.Columns["Pengeluarann"].ReadOnly = true;

            // Hanya kolom Persentase yang bisa diedit
            dataGridView1.Columns["Persentasee"].ReadOnly = false;
        }

        private void Pemasukan_TextChanged(object sender, EventArgs e)
        {

        }

        private void Nama_Kebutuhan_TextChanged(object sender, EventArgs e)
        {

        }

        private void TinggiPrioritas_CheckedChanged(object sender, EventArgs e)
        {
            //Jika prioritas tinggi, maka yang muncul adalah persentase berikut
            if (TinggiPrioritas.Checked)
            {
                PresentasePilihan.Items.Clear();
                PresentasePilihan.Items.Add("25%");
                PresentasePilihan.Items.Add("30%");
                PresentasePilihan.Items.Add("35%");
                PresentasePilihan.SelectedIndex = 0;
            }
        }

        private void SedangPrioritas_CheckedChanged(object sender, EventArgs e)
        {
            //Jika prioritas sedang, maka yang muncul adalah persentase berikut
            if (SedangPrioritas.Checked)
            {
                PresentasePilihan.Items.Clear();
                PresentasePilihan.Items.Add("15%");
                PresentasePilihan.Items.Add("20%");
                PresentasePilihan.Items.Add("25%");
                PresentasePilihan.SelectedIndex = 0;
            }
        }

        private void RendahPrioritas_CheckedChanged(object sender, EventArgs e)
        {
            //Jika prioritas rendah, maka yang muncul adalah persentase berikut
            if (RendahPrioritas.Checked)
            {
                PresentasePilihan.Items.Clear();
                PresentasePilihan.Items.Add("5%");
                PresentasePilihan.Items.Add("10%");
                PresentasePilihan.Items.Add("15%");
                PresentasePilihan.SelectedIndex = 0;
            }
        }

        private void Tambah_Click(object sender, EventArgs e)
        {
            // Validasi input: nama kebutuhan dan persentase harus diisi
            if (string.IsNullOrWhiteSpace(Nama_Kebutuhan.Text) || PresentasePilihan.SelectedItem == null)
            {
                MessageBox.Show("Silakan lengkapi nama kebutuhan dan pilih persentase.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string kebutuhan = Nama_Kebutuhan.Text; // Ambil input dari user

            // Tentukan level prioritas berdasarkan radio button yang dipilih
            string prioritas = TinggiPrioritas.Checked ? "Tinggi" :
                               SedangPrioritas.Checked ? "Sedang" :
                               RendahPrioritas.Checked ? "Rendah" : "";
            string jenisPengeluaran = comboBox1.Text;

            // Validasi: pastikan prioritas sudah dipilih
            if (string.IsNullOrEmpty(prioritas))
            {
                MessageBox.Show("Silakan pilih tingkat prioritas.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string persentaseStr = PresentasePilihan.SelectedItem.ToString().Replace("%", ""); // Ambil dan konversi persentase dari string ke double

            if (!double.TryParse(persentaseStr, out double persentaseInput))
            {
                MessageBox.Show("Persentase tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double persentaseInputDecimal = persentaseInput / 100; // Ubah persentase menjadi desimal (misal 30 → 0.3)

            if (!double.TryParse(Pemasukan.Text, out double pemasukan))
            {
                MessageBox.Show("Masukkan pemasukan yang valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hitung total persentase agar tidak melebihi 100%
            double totalPersentase = persentaseInput;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Persentasee"].Value != null)
                {
                    string existingPercentStr = row.Cells["Persentasee"].Value.ToString().Replace("%", "");
                    if (double.TryParse(existingPercentStr, out double existingPercent))
                    {
                        totalPersentase += existingPercent;
                    }
                }
            }

            // Hitung nominal alokasi berdasarkan persentase
            double nominal = pemasukan * persentaseInputDecimal;
            double pengeluaran = 0;
            string teks = "";

            // Hitung pengeluaran per jenis (harian, mingguan, bulanan)
            switch (jenisPengeluaran)
            {
                case "Harian":
                    pengeluaran = nominal / 30;
                    teks = "/Hari";
                    break;
                case "Mingguan":
                    pengeluaran = nominal / 4;
                    teks = "/Minggu";
                    break;
                case "Bulanan":
                    pengeluaran = nominal;
                    teks = "/Bulan";
                    break;
                default:
                    pengeluaran = 0;
                    break;
            }

            // Cek apakah total persentase melebihi 100%
            if (totalPersentase > 100)
            {
                MessageBox.Show("Total persentase melebihi 100%. Kebutuhan tidak bisa ditambahkan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tambahkan baris baru ke DataGridView
            int rowIndex = dataGridView1.Rows.Add();
            dataGridView1.Rows[rowIndex].Cells["Kebutuhann"].Value = kebutuhan;
            dataGridView1.Rows[rowIndex].Cells["Prioritass"].Value = prioritas;
            dataGridView1.Rows[rowIndex].Cells["Persentasee"].Value = persentaseStr + "%";
            dataGridView1.Rows[rowIndex].Cells["Nominall"].Value = nominal.ToString("C");

            // Format pengeluaran ke jutaan atau ribuan sesuai nilainya
            if (pengeluaran >= 1000000)
            {
                double juta = pengeluaran / 1000000;
                string formattedJuta = juta % 1 == 0 ? $"{juta:0}jt" : $"{juta:0.0}jt";
                dataGridView1.Rows[rowIndex].Cells["Pengeluarann"].Value = formattedJuta + teks;
            }
            else
            {
                dataGridView1.Rows[rowIndex].Cells["Pengeluarann"].Value = $"{Math.Round(pengeluaran / 1000)}rb" + teks;
            }

            dataGridView1.Rows[rowIndex].Cells["Pengeluarann"].Tag = jenisPengeluaran; // Simpan jenis pengeluaran di Tag (tidak terlihat)
            dataGridView1.Rows[rowIndex].Cells["NominalAsli"].Value = nominal; // Simpan nominal asli ke kolom tersembunyi

            // Perbarui label total dan grafik
            UpdateTotalAlokasi();
            TampilkanGrafik();
            SimpanGrafikKeCSV("grafik.csv");

            // Reset form input setelah tambah data
            Nama_Kebutuhan.Clear();
            PresentasePilihan.Items.Clear();
            PresentasePilihan.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            TinggiPrioritas.Checked = SedangPrioritas.Checked = RendahPrioritas.Checked = false;
        }

        private void UpdateTotalAlokasi()
        {
            double totalNominal = 0;

            // Iterasi setiap baris di DataGridView untuk menjumlahkan nilai nominal
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Nominall"].Value != null)
                {
                    // Ambil nilai dari kolom "Nominall" dan hapus format Rp serta titik pemisah ribuan
                    string nominalStr = row.Cells["Nominall"].Value.ToString()
                        .Replace("Rp", "")
                        .Replace(".", "")
                        .Trim();

                    double nominal = 0;

                    // Ubah string ke double dan tambahkan ke total jika berhasil
                    if (double.TryParse(nominalStr, out nominal))
                    {
                        totalNominal += nominal;
                    }
                }
            }

            // Ambil nilai pemasukan dari textbox, hapus titik/koma lalu parsing ke double
            double pemasukan = 0;
            double.TryParse(Pemasukan.Text.Replace(".", "").Replace(",", "").Trim(), out pemasukan);

            // Tampilkan hasil di label6 dengan format ribuan
            label6.Text = $"Total Alokasi: Rp {totalNominal:N0} / Rp {pemasukan:N0}";

        }

        private void PresentasePilihan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CetakDoc_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog(); // Dialog untuk memilih lokasi penyimpanan file
            sfd.Filter = "PDF Files (*.pdf)|*.pdf"; // Filter hanya file PDF
            sfd.FileName = "Laporan_Alokasi_Anggaran.pdf";

            // Jika pengguna menekan OK di dialog
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ExportToPdf(sfd.FileName); // Panggil fungsi export PDF
                MessageBox.Show("Berhasil disimpan ke PDF!");
            }
        }

        private void ExportToPdf(string filename)
        {
            // Inisialisasi dokumen A4 dengan margin
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 20f, 20f);
            PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
            doc.Open();

            // Judul
            Paragraph title = new Paragraph("Laporan Alokasi Anggaran\n\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14))
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(title);

            // Ambil kolom yang terlihat
            var visibleColumns = dataGridView1.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .ToList();

            PdfPTable table = new PdfPTable(visibleColumns.Count);
            table.WidthPercentage = 100;
            table.SpacingBefore = 10f;
            table.SpacingAfter = 10f;

            // Header dengan warna
            foreach (var column in visibleColumns)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE)));
                headerCell.BackgroundColor = new BaseColor(0, 102, 204); // Biru
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.Padding = 5;
                table.AddCell(headerCell);
            }

            // Isi tabel
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (var column in visibleColumns)
                    {
                        string text = row.Cells[column.Name].Value?.ToString() ?? "";
                        PdfPCell cell = new PdfPCell(new Phrase(text, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.Padding = 5;
                        table.AddCell(cell);
                    }
                }
            }

            doc.Add(table);

            // Tambahkan Total Alokasi & Sisa Anggaran
            doc.Add(new Paragraph("\n" + label6.Text, FontFactory.GetFont(FontFactory.HELVETICA, 10)));

            // Simpan dan masukkan grafik
            string chartPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "grafik.png");
            SimpanGrafikSebagaiGambar(chartPath);

            // Jika gambar chart berhasil dibuat, tambahkan ke PDF
            if (File.Exists(chartPath))
            {
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(chartPath);
                chartImage.ScaleToFit(400f, 300f);
                chartImage.Alignment = Element.ALIGN_CENTER;

                doc.Add(new Paragraph("Grafik Alokasi:\n\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11)));
                doc.Add(chartImage);
            }

            doc.Close();
        }

        private void SimpanGrafikSebagaiGambar(string path)
        {
            chartAlokasi.SaveImage(path, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
        }


        private void ResetTabel_Click(object sender, EventArgs e)
        {
            // Tampilkan dialog konfirmasi sebelum reset
            DialogResult result = MessageBox.Show(
                "Apakah Anda yakin ingin menghapus semua data alokasi?",
                "Konfirmasi Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Mereset semua data yang ada di form
                Pemasukan.Clear();
                dataGridView1.Rows.Clear();
                Nama_Kebutuhan.Clear();
                PresentasePilihan.Items.Clear();
                label6.Text = "Total Alokasi: Rp 0 / Rp 0";
                chartAlokasi.Series.Clear();
                chartAlokasi.Titles.Clear();

                // Kosongkan file grafik.csv
                File.WriteAllText("grafik.csv", "Kategori,Persentase\n");

                // Hapus juga data CSV utama jika diperlukan
                if (File.Exists("alokasi.csv"))
                    File.Delete("alokasi.csv");

                MessageBox.Show("Semua data berhasil direset.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Jika sel sedang diedit dan belum disimpan
            if (dataGridView1.IsCurrentCellDirty)
            {
                // Simpan nilai sel saat ini secara eksplisit agar memicu event CellValueChanged
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Pastikan perubahan terjadi pada baris valid dan kolom "Persentasee"
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Persentasee")
            {
                // Ambil nilai persentase dari sel dan konversi ke double
                string persentaseStr = dataGridView1.Rows[e.RowIndex].Cells["Persentasee"].Value?.ToString().Replace("%", "");
                if (!double.TryParse(persentaseStr, out double persentase))
                    return;

                // Ambil pemasukan dari TextBox dan konversi ke double
                double pemasukan;
                if (!double.TryParse(Pemasukan.Text, out pemasukan))
                    return;

                // Hitung nominal berdasarkan persentase
                double nominal = pemasukan * (persentase / 100);
                dataGridView1.Rows[e.RowIndex].Cells["Nominall"].Value = nominal.ToString("C");

                // Ambil jenis pengeluaran dari sel (contoh: "/Hari", "/Minggu", dll.)
                string jenisPengeluaran = dataGridView1.Rows[e.RowIndex].Cells["Pengeluarann"].Value?.ToString();
                double pengeluaran = 0;
                string teks = "";

                // Deteksi jenis pengeluaran berdasarkan teks yang ada
                string jenis = "";
                if (jenisPengeluaran != null)
                {
                    if (jenisPengeluaran.Contains("Hari"))
                        jenis = "Harian";
                    else if (jenisPengeluaran.Contains("Minggu"))
                        jenis = "Mingguan";
                    else if (jenisPengeluaran.Contains("Bulan"))
                        jenis = "Bulanan";
                }

                // Hitung nilai pengeluaran berdasarkan jenis
                switch (jenis)
                {
                    case "Harian":
                        pengeluaran = nominal / 30;
                        teks = "/Hari";
                        break;
                    case "Mingguan":
                        pengeluaran = nominal / 4;
                        teks = "/Minggu";
                        break;
                    case "Bulanan":
                        pengeluaran = nominal;
                        teks = "/Bulan";
                        break;
                    default:
                        pengeluaran = 0;
                        teks = "";
                        break;
                }

                // Format pengeluaran: jutaan jika >= 1 juta, atau ribuan jika di bawahnya
                if (pengeluaran >= 1000000)
                {
                    double juta = pengeluaran / 1000000;
                    string formattedJuta = juta % 1 == 0 ? $"{juta:0}jt" : $"{juta:0.0}jt";
                    dataGridView1.Rows[e.RowIndex].Cells["Pengeluarann"].Value = formattedJuta + teks;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Pengeluarann"].Value = $"{Math.Round(pengeluaran / 1000)}rb{teks}";
                }

                UpdateTotalAlokasi(); // Perbarui label total alokasi di bawah tabel
            }

        }

        private void SimpanKeCSV(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                // Baris pertama: pemasukan
                sw.WriteLine("Pemasukan," + Pemasukan.Text);

                // Header
                sw.WriteLine("Kebutuhan,Prioritas,Persentase,JenisPengeluaran");

                // Loop semua baris yang sudah diisi di DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow) // Abaikan baris kosong yang disediakan untuk input baru
                    {
                        // Ambil nilai dari tiap sel
                        string kebutuhan = row.Cells["Kebutuhann"].Value?.ToString();
                        string prioritas = row.Cells["Prioritass"].Value?.ToString();
                        string persentase = row.Cells["Persentasee"].Value?.ToString().Replace("%", "");
                        string jenisPengeluaran = row.Cells["Pengeluarann"].Tag?.ToString();

                        sw.WriteLine($"{kebutuhan},{prioritas},{persentase},{jenisPengeluaran}"); // Tulis satu baris data ke file CSV
                    }
                }
            }
        }

        private void LoadDariCSV(string filePath)
        {
            dataGridView1.Rows.Clear(); // Hapus semua baris sebelum load data baru

            double pemasukan = 0;

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;

                // Baris 1: baca pemasukan
                if ((line = sr.ReadLine()) != null && line.StartsWith("Pemasukan"))
                {
                    string[] split = line.Split(',');
                    if (split.Length > 1)
                        double.TryParse(split[1].Trim(), out pemasukan);
                    Pemasukan.Text = pemasukan.ToString();
                }

                // Baris 2: abaikan header
                sr.ReadLine();

                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 4)
                    {
                        // Ambil data per kolom
                        string kebutuhan = parts[0];
                        string prioritas = parts[1];
                        double persentase = double.Parse(parts[2]);
                        string jenisPengeluaran = parts[3];

                        // Hitung nominal dan pengeluaran berdasarkan jenis
                        double nominal = pemasukan * (persentase / 100);
                        string pengeluaranTeks = HitungPengeluaran(nominal, jenisPengeluaran);

                        // Tambahkan ke DataGridView
                        int rowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowIndex].Cells["Kebutuhann"].Value = kebutuhan;
                        dataGridView1.Rows[rowIndex].Cells["Prioritass"].Value = prioritas;
                        dataGridView1.Rows[rowIndex].Cells["Persentasee"].Value = persentase + "%";
                        dataGridView1.Rows[rowIndex].Cells["Nominall"].Value = nominal.ToString("C0");
                        dataGridView1.Rows[rowIndex].Cells["Pengeluarann"].Value = pengeluaranTeks;

                        dataGridView1.Rows[rowIndex].Cells["Pengeluarann"].Tag = jenisPengeluaran; // Simpan jenis pengeluaran ke dalam Tag untuk keperluan internal
                    }
                }
            }

            UpdateTotalAlokasi();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SimpanKeCSV("alokasi.csv"); // Simpan data alokasi ke file CSV saat aplikasi ditutup
        }

        private string HitungPengeluaran(double nominal, string jenis)
        {
            double hasil = 0;
            string teks = "";

            // Hitung pengeluaran per periode tergantung jenis pengeluaran
            if (jenis.Contains("Harian"))
            {
                hasil = nominal / 30;
                teks = "/Hari";
            }
            else if (jenis.Contains("Mingguan"))
            {
                hasil = nominal / 4;
                teks = "/Minggu";
            }
            else
            {
                hasil = nominal;
                teks = "/Bulan";
            }

            // Format hasil agar tampil ringkas: dalam juta jika besar, atau ribuan
            if (hasil >= 1000000)
            {
                double juta = hasil / 1000000;
                return (juta % 1 == 0 ? $"{juta:0}jt" : $"{juta:0.0}jt") + teks;
            }
            else
            {
                return $"{Math.Round(hasil / 1000)}rb{teks}";
            }
        }

        private void TampilkanGrafik()
        {
            chartAlokasi.Series.Clear();
            chartAlokasi.Titles.Clear();

            Series series = new Series("Alokasi");
            series.ChartType = SeriesChartType.Pie; // Atau Bar

            // Tambahkan data dari DataGridView ke grafik
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string label = row.Cells["Kebutuhann"].Value?.ToString();
                    string nominalStr = row.Cells["Nominall"].Value?.ToString()
                        .Replace("Rp", "").Replace(".", "").Replace(",", "").Trim();

                    if (double.TryParse(nominalStr, out double value))
                    {
                        series.Points.AddXY(label, value); // Tambahkan poin ke grafik
                    }
                }
            }

            chartAlokasi.Series.Add(series);
            chartAlokasi.Titles.Add("Grafik Alokasi Kebutuhan");
        }

        private void SimpanGrafikKeCSV(string path)
        {
            // Simpan data grafik (label dan nilai) ke file CSV
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (DataPoint point in chartAlokasi.Series[0].Points)
                {
                    string label = point.AxisLabel; // Ambil label dari sumbu
                    double value = point.YValues[0]; // Ambil nilai dari grafik
                    writer.WriteLine($"{label},{value}"); // Tulis ke file
                }
            }
        }

        private void LoadGrafikDariCSV(string path)
        {
            if (!File.Exists(path)) return;

            chartAlokasi.Series[0].ChartType = SeriesChartType.Pie;
            chartAlokasi.Series[0].Points.Clear();

            // Baca setiap baris CSV dan parsing jadi poin grafik
            foreach (string line in File.ReadAllLines(path))
            {
                string[] parts = line.Split(','); // Pisahkan label dan nilai
                if (parts.Length == 2 &&
                    double.TryParse(parts[1], out double value)) // Cek validasi nilai
                {
                    chartAlokasi.Series[0].Points.AddXY(parts[0], value); // Tambah ke grafik
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Buat gradasi warna latar belakang pada form
            System.Drawing.Rectangle rect = this.ClientRectangle;
            using (LinearGradientBrush brush = new LinearGradientBrush(
                new PointF(0, 0),
                new PointF(0, rect.Height),
                Color.White,
                Color.FromArgb(100, 181, 246))) // biru muda
            {
                e.Graphics.FillRectangle(brush, rect); // Isi latar dengan gradasi
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            // Pastikan ada baris yang dipilih
            if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
            {
                DialogResult result = MessageBox.Show(
                    "Apakah Anda yakin ingin menghapus baris ini?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    UpdateTotalAlokasi();       // Update alokasi setelah penghapusan
                    TampilkanGrafik();          // Perbarui grafik
                    SimpanGrafikKeCSV("grafik.csv"); // Simpan ulang grafik ke CSV
                }
            }
            else
            {
                MessageBox.Show("Pilih baris yang ingin dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
            {
                btnHapus_Click(sender, e); // Panggil ulang fungsi hapus
            }
        }

        private void chartAlokasi_Click(object sender, EventArgs e)
        {

        }
    }
}
