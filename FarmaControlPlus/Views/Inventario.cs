using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace TuProyecto.Views
{
    public partial class Inventario: UserControl
    {
        public Inventario()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            CargarDatosEjemplo();
        }

        private void ConfigurarDataGridView()
        {
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void CargarDatosEjemplo()
        {
            dataGridView1.Rows.Clear();

            // Cargar combobox de categorías
            cmbCategoria.Items.Clear();
            cmbCategoria.Items.Add("Todas las categorías");
            cmbCategoria.Items.Add("Analgésico");
            cmbCategoria.Items.Add("Antibiótico");
            cmbCategoria.Items.Add("Antiinflamatorio");
            cmbCategoria.SelectedIndex = 0;

            DateTime hoy = DateTime.Now;
            dataGridView1.Rows.Add("Paracetamol 500mg", "PAR001", "Analgésico", 25, "$8.50", hoy.AddDays(15).ToString("dd/MM/yyyy"));
            dataGridView1.Rows.Add("Amoxicilina 500mg", "AMX002", "Antibiótico", 8, "$18.50", hoy.AddDays(60).ToString("dd/MM/yyyy"));
            dataGridView1.Rows.Add("Ibuprofeno 400mg", "IBU003", "Antiinflamatorio", 42, "$12.00", hoy.AddDays(120).ToString("dd/MM/yyyy"));
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();
            string categoria = cmbCategoria.SelectedItem.ToString();

            // Lógica de búsqueda
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;

            // Tu lógica de formateo condicional
        }

        public void ActualizarDatos()
        {
            CargarDatosEjemplo();
        }
    }
}