using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using tubes_kpl_kelompok_satu_dong;
namespace tubes_kpl_kelompok_satu_dong
{
    public class Voucher
    {
        public string Code { get; set; }
        public decimal Discount { get; set; }
    }

    public class VoucherService
    {
        private readonly Dictionary<string, decimal> _promoRules = new Dictionary<string, decimal>
        {
            { "PROMO10", 10000m }, { "DISKON20", 20000m }
        };

        private readonly Regex _validFormat = new Regex(@"^[A-Z0-9]{5,10}$");

        public void Claim(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new Exception("Kode tidak boleh kosong.");
            if (!_validFormat.IsMatch(code)) throw new Exception("Format kode harus huruf kapital & angka (5-10 digit).");
            if (!_promoRules.ContainsKey(code)) throw new Exception("Kode voucher tidak ditemukan/tidak valid.");

            AppSession.Instance.ClaimedVouchers.Add(new Voucher { Code = code, Discount = _promoRules[code] });
        }
    }

    public class FormKlaimVoucher : Form
    {
        private TextBox txtVoucher;
        private Button btnClaim;
        private ListBox listVoucher;
        private VoucherService _service;

        public FormKlaimVoucher()
        {
            _service = new VoucherService();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Klaim Voucher (GUI by Zek)";
            this.Size = new Size(350, 300);

            Controls.Add(new Label { Text = "Masukkan Kode Voucher:", Location = new Point(20, 20), Width = 150 });
            txtVoucher = new TextBox { Location = new Point(20, 45), Width = 200 };
            Controls.Add(txtVoucher);

            btnClaim = new Button { Text = "Klaim", Location = new Point(230, 43), Width = 80 };
            btnClaim.Click += BtnClaim_Click;
            Controls.Add(btnClaim);

            Controls.Add(new Label { Text = "Voucher Aktif Anda:", Location = new Point(20, 90), Width = 150 });
            listVoucher = new ListBox { Location = new Point(20, 115), Width = 290, Height = 100 };
            Controls.Add(listVoucher);
        }

        private void BtnClaim_Click(object sender, EventArgs e)
        {
            try
            {
                _service.Claim(txtVoucher.Text.ToUpper());
                MessageBox.Show("Voucher berhasil diklaim!");
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Klaim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshList()
        {
            listVoucher.Items.Clear();
            foreach (var v in AppSession.Instance.ClaimedVouchers)
            {
                listVoucher.Items.Add($"Kode: {v.Code} | Diskon: Rp{v.Discount}");
            }
        }
    }
}