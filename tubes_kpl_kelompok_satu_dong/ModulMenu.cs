using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using tubes_kpl_kelompok_satu_dong;

namespace tubes_kpl_kelompok_satu_dong
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }

    public class MenuService
    {
        private readonly Dictionary<string, string> _categoryMap = new Dictionary<string, string>
        {
            { "M", "Makanan Utama" }, { "D", "Minuman" }, { "S", "Snack" }
        };

        public void AddMenu(string name, string catCode, string priceText)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Nama tidak boleh kosong.");
            if (!double.TryParse(priceText, out double price) || price <= 0) throw new Exception("Harga harus angka valid > 0.");

            string category = _categoryMap.ContainsKey(catCode) ? _categoryMap[catCode] : "Lainnya";

            AppSession.Instance.MenuList.Add(new MenuItem { Name = name, Category = category, Price = price });
        }
    }
    public class FormMenuMakanan : Form
    {
        private TextBox txtName, txtCategory, txtPrice;
        private Button btnAdd;
        private ListBox listMenu;
        private MenuService _service;

        public FormMenuMakanan()
        {
            _service = new MenuService();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Manajemen Menu Makanan (GUI by Kamu)";
            this.Size = new Size(400, 350);

            Controls.Add(new Label { Text = "Nama Menu:", Location = new Point(20, 20) });
            txtName = new TextBox { Location = new Point(120, 20), Width = 200 };
            Controls.Add(txtName);

            Controls.Add(new Label { Text = "Kode Kategori (M/D/S):", Location = new Point(20, 60), Width = 100 });
            txtCategory = new TextBox { Location = new Point(120, 60), Width = 200 };
            Controls.Add(txtCategory);

            Controls.Add(new Label { Text = "Harga:", Location = new Point(20, 100) });
            txtPrice = new TextBox { Location = new Point(120, 100), Width = 200 };
            Controls.Add(txtPrice);

            btnAdd = new Button { Text = "Tambah Menu", Location = new Point(120, 140), Width = 100 };
            btnAdd.Click += BtnAdd_Click;
            Controls.Add(btnAdd);

            listMenu = new ListBox { Location = new Point(20, 180), Width = 300, Height = 100 };
            Controls.Add(listMenu);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _service.AddMenu(txtName.Text, txtCategory.Text.ToUpper(), txtPrice.Text);
                MessageBox.Show("Menu berhasil ditambahkan!");
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshList()
        {
            listMenu.Items.Clear();
            foreach (var item in AppSession.Instance.MenuList)
            {
                listMenu.Items.Add($"{item.Name} - {item.Category} - Rp{item.Price}");
            }
        }
    }
}