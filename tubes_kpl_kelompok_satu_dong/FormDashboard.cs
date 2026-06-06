using System;
using System.Drawing;
using System.Windows.Forms;
using tubes_kpl_kelompok_satu_dong;

namespace tubes_kpl_kelompok_satu_dong
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            this.Text = "Aplikasi Tukar Voucher Makanan - Dashboard";
            this.Size = new Size(400, 200);
            this.StartPosition = FormStartPosition.CenterScreen;

            Button btnMenu = new Button { Text = "Buka Menu Makanan", Location = new Point(50, 50), Width = 130, Height = 50 };
            btnMenu.Click += (s, e) => new FormMenuMakanan().ShowDialog();
            Controls.Add(btnMenu);

            Button btnVoucher = new Button { Text = "Buka Klaim Voucher", Location = new Point(200, 50), Width = 130, Height = 50 };
            btnVoucher.Click += (s, e) => new FormKlaimVoucher().ShowDialog();
            Controls.Add(btnVoucher);


        }
    }
}