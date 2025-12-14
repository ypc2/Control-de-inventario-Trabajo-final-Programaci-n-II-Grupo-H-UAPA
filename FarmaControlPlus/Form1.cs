using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace FarmaciaMiPueblo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConfigurarEstilos();
            CargarDatosEjemplo();
        }

        private void ConfigurarEstilos()
        {
            // Añade estas líneas para asegurar que los labels sean blancos
            lblTotal.ForeColor = Color.White;
            lblBajosStock.ForeColor = Color.White;
            lblPorVencer.ForeColor = Color.White;

            // Añade esta línea para aumentar el interlineado
            dataGridView1.RowTemplate.Height = 40;

            // El resto de tu código existente...
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void CargarDatosEjemplo()
        {
            // Limpiar datos existentes primero
            dataGridView1.Rows.Clear();

            // Obtener fecha actual para referencia
            DateTime hoy = DateTime.Now;

            // Datos de ejemplo con diferentes estados de vencimiento
            dataGridView1.Rows.Add("Paracetamol 500mg", "PAR001", "Analgésico", 25, "$8.50", hoy.AddDays(15).ToString("dd/MM/yyyy")); // A punto de vencer (15 días)
            dataGridView1.Rows.Add("Amoxicilina 500mg", "AMX002", "Antibiótico", 8, "$18.50", hoy.AddDays(60).ToString("dd/MM/yyyy")); // Por vencer (60 días)
            dataGridView1.Rows.Add("Ibuprofeno 400mg", "IBU003", "Antiinflamatorio", 42, "$12.00", hoy.AddDays(120).ToString("dd/MM/yyyy")); // En stock normal
            dataGridView1.Rows.Add("Omeprazol 20mg", "OME004", "Gastrointestinal", 0, "$15.75", hoy.AddDays(200).ToString("dd/MM/yyyy")); // Sin stock
            dataGridView1.Rows.Add("Loratadina 10mg", "LOR005", "Antialérgico", 3, "$9.80", hoy.AddDays(180).ToString("dd/MM/yyyy")); // Stock bajo

            // AÑADIR 5 EJEMPLOS MÁS:
            dataGridView1.Rows.Add("Aspirina 100mg", "ASP006", "Analgésico", 15, "$5.20", hoy.AddDays(-10).ToString("dd/MM/yyyy")); // Ya vencido (hace 10 días)
            dataGridView1.Rows.Add("Ranitidina 150mg", "RAN007", "Gastrointestinal", 12, "$10.30", hoy.AddDays(45).ToString("dd/MM/yyyy")); // Por vencer
            dataGridView1.Rows.Add("Metformina 850mg", "MET008", "Diabetes", 0, "$22.90", hoy.AddDays(300).ToString("dd/MM/yyyy")); // Sin stock
            dataGridView1.Rows.Add("Atorvastatina 20mg", "ATO009", "Colesterol", 28, "$45.50", hoy.AddDays(5).ToString("dd/MM/yyyy")); // A punto de vencer (5 días)
            dataGridView1.Rows.Add("Losartán 50mg", "LOS010", "Hipertensión", 6, "$28.75", hoy.AddDays(250).ToString("dd/MM/yyyy")); // Stock bajo

            // Actualizar estadísticas
            ActualizarEstadisticas();
        }

        private void ActualizarEstadisticas()
        {
            int totalMedicamentos = 0;
            int bajosStock = 0;
            int porVencer = 0;
            int aPuntoDeVencer = 0;
            int vencidos = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue; // Saltar fila nueva
                totalMedicamentos++;

                if (row.Cells["Stock"].Value != null)
                {
                    int stock = 0;
                    if (int.TryParse(row.Cells["Stock"].Value.ToString(), out stock))
                    {
                        if (stock < 10 && stock > 0) bajosStock++;
                    }
                }

                if (row.Cells["FechaVencimiento"].Value != null)
                {
                    DateTime fechaVencimiento;
                    string fechaStr = row.Cells["FechaVencimiento"].Value.ToString();

                    if (DateTime.TryParseExact(fechaStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaVencimiento))
                    {
                        if (fechaVencimiento < DateTime.Now)
                            vencidos++;
                        else if (fechaVencimiento <= DateTime.Now.AddDays(30))
                            aPuntoDeVencer++;
                        else if (fechaVencimiento <= DateTime.Now.AddDays(90))
                            porVencer++;
                    }
                }
            }

            // Actualizar textos (SIN cambiar colores)
            lblTotal.Text = totalMedicamentos.ToString("N0"); // Formato con separadores de miles
            lblBajosStock.Text = bajosStock.ToString("N0");
            lblPorVencer.Text = (vencidos + aPuntoDeVencer + porVencer).ToString("N0");

            // QUITAR el cambio de color - mantener siempre blanco
            // lblBajosStock.ForeColor = bajosStock > 0 ? Color.Red : Color.Black; // ELIMINAR
            // lblPorVencer.ForeColor = (vencidos + aPuntoDeVencer + porVencer) > 0 ? Color.OrangeRed : Color.Black; // ELIMINAR

            // En su lugar, mantener siempre blanco
            lblTotal.ForeColor = Color.White;
            lblBajosStock.ForeColor = Color.White;
            lblPorVencer.ForeColor = Color.White;
        }
        private void btnNuevoMedicamento_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad para agregar nuevo medicamento",
                "Nuevo Medicamento",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(criterio))
            {
                // Mostrar todos si no hay criterio
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Visible = true;
                }
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool visible = false;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null &&
                        cell.Value.ToString().ToLower().Contains(criterio))
                    {
                        visible = true;
                        break;
                    }
                }

                row.Visible = visible;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            // Obtener valores
            int stock = 0;
            DateTime fechaVencimiento = DateTime.MaxValue;
            bool tieneStock = false;
            bool tieneFecha = false;

            // Obtener stock
            if (row.Cells["Stock"].Value != null)
            {
                tieneStock = int.TryParse(row.Cells["Stock"].Value.ToString(), out stock);
            }

            // Obtener fecha de vencimiento
            if (row.Cells["FechaVencimiento"].Value != null)
            {
                string fechaStr = row.Cells["FechaVencimiento"].Value.ToString();
                tieneFecha = DateTime.TryParseExact(fechaStr, "dd/MM/yyyy",
                                                   CultureInfo.InvariantCulture,
                                                   DateTimeStyles.None,
                                                   out fechaVencimiento);

                // Si no funciona con formato exacto, intentar parseo normal
                if (!tieneFecha)
                {
                    tieneFecha = DateTime.TryParse(fechaStr, out fechaVencimiento);
                }
            }

            // Crear fuentes
            Font fontBold = new Font(dataGridView1.Font, FontStyle.Bold);
            Font fontStrikeout = new Font(dataGridView1.Font, FontStyle.Strikeout);
            Font fontNormal = new Font(dataGridView1.Font, FontStyle.Regular);

            // **ORDEN DE PRIORIDAD CORREGIDO:**
            // 1. Ya vencido (MÁS CRÍTICO)
            // 2. Sin stock
            // 3. A punto de vencer (< 30 días)
            // 4. Por vencer (31-90 días)
            // 5. Stock bajo (última prioridad)

            // 1. YA VENCIDO - Fondo naranja, texto naranja bold
            if (tieneFecha && fechaVencimiento.Date < DateTime.Today)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 245, 230); // Naranja claro
                e.CellStyle.ForeColor = Color.FromArgb(255, 140, 0);   // Naranja
                e.CellStyle.Font = fontBold;
                e.CellStyle.SelectionBackColor = Color.FromArgb(255, 220, 180);
                e.CellStyle.SelectionForeColor = Color.DarkOrange;
                return; // ¡IMPORTANTE! Salir aquí para no aplicar otras reglas
            }

            // 2. SIN STOCK (stock = 0) - Fondo gris, texto tachado
            if (tieneStock && stock == 0)
            {
                e.CellStyle.BackColor = Color.FromArgb(245, 245, 245); // Gris claro
                e.CellStyle.ForeColor = Color.FromArgb(150, 150, 150); // Gris
                e.CellStyle.Font = fontStrikeout; // TACHADO
                e.CellStyle.SelectionBackColor = Color.FromArgb(220, 220, 220);
                e.CellStyle.SelectionForeColor = Color.FromArgb(100, 100, 100);
                return; // ¡IMPORTANTE! Salir aquí
            }

            // 3. A PUNTO DE VENCER (< 30 días) - Fondo amarillo, texto negro bold
            if (tieneFecha && fechaVencimiento.Date <= DateTime.Today.AddDays(30)
                           && fechaVencimiento.Date >= DateTime.Today)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 200); // Amarillo claro
                e.CellStyle.ForeColor = Color.Black; // Negro
                e.CellStyle.Font = fontBold;
                e.CellStyle.SelectionBackColor = Color.FromArgb(255, 255, 150);
                e.CellStyle.SelectionForeColor = Color.Black;
                return; // ¡IMPORTANTE! Salir aquí
            }

            // 4. POR VENCER (31-90 días) - Fondo naranja claro, texto naranja
            if (tieneFecha && fechaVencimiento.Date <= DateTime.Today.AddDays(90)
                           && fechaVencimiento.Date > DateTime.Today.AddDays(30))
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 240, 220); // Naranja claro
                e.CellStyle.ForeColor = Color.FromArgb(255, 140, 0);   // Naranja
                e.CellStyle.Font = fontBold;
                e.CellStyle.SelectionBackColor = Color.FromArgb(255, 220, 180);
                e.CellStyle.SelectionForeColor = Color.DarkOrange;
                return; // ¡IMPORTANTE! Salir aquí
            }

            // 5. STOCK BAJO (< 10 unidades) - ÚLTIMA PRIORIDAD
            if (tieneStock && stock < 10 && stock > 0)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 240, 245); // Rosa claro
                e.CellStyle.ForeColor = Color.FromArgb(200, 0, 0);     // Rojo
                e.CellStyle.Font = fontNormal; // NO bold
                e.CellStyle.SelectionBackColor = Color.FromArgb(255, 220, 225);
                e.CellStyle.SelectionForeColor = Color.DarkRed;
                return; // ¡IMPORTANTE! Salir aquí
            }

            // 6. NORMAL - Estilo por defecto (alternado)
            if (e.RowIndex % 2 == 0)
            {
                e.CellStyle.BackColor = Color.White;
            }
            else
            {
                e.CellStyle.BackColor = Color.FromArgb(240, 248, 255); // AliceBlue
            }
            e.CellStyle.ForeColor = Color.Black;
            e.CellStyle.Font = fontNormal;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Mostrar detalles del medicamento seleccionado
                string detalles = $"Medicamento: {row.Cells["Nombre"].Value}\n" +
                                 $"Código: {row.Cells["Codigo"].Value}\n" +
                                 $"Categoría: {row.Cells["Categoria"].Value}\n" +
                                 $"Stock: {row.Cells["Stock"].Value} unidades\n" +
                                 $"Precio: {row.Cells["Precio"].Value}\n" +
                                 $"Vencimiento: {row.Cells["FechaVencimiento"].Value}";

                lblDetalles.Text = detalles;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatosEjemplo();
            txtBuscar.Clear();
            MessageBox.Show("Datos actualizados correctamente",
                "Actualización",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            string reporte = $"REPORTE DE INVENTARIO - {DateTime.Now:dd/MM/yyyy}\n\n";
            reporte += $"Total medicamentos: {lblTotal.Text}\n";
            reporte += $"Bajos en stock: {lblBajosStock.Text}\n";
            reporte += $"Por vencer: {lblPorVencer.Text}\n\n";
            reporte += "Medicamentos críticos:\n";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Stock"].Value != null)
                {
                    int stock = Convert.ToInt32(row.Cells["Stock"].Value);
                    if (stock < 10)
                    {
                        reporte += $"• {row.Cells["Nombre"].Value} - Stock: {stock}\n";
                    }
                }
            }

            MessageBox.Show(reporte, "Reporte de Inventario",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuArchivoSalir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Está seguro que desea salir del sistema?",
                "Confirmar Salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void menuAyudaAcerca_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Sistema de Control de Inventario\n" +
                "Farmacia Mi Pueblo\n" +
                "Versión 1.0\n" +
                "© 2024 - Desarrollado en C# .NET",
                "Acerca del Sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
                e.Handled = true;
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = true;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}