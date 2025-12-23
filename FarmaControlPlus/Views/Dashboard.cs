using System;
using System.Drawing;
using System.Windows.Forms;

namespace TuProyecto.Views
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            SetupDashboard();
            WireEvents();
            // Colors and static visual properties moved to Designer -> keep runtime out to make UI static.
            // ApplyCardStyles();
            // SetupButtonHoverEffects();
        }

        private void SetupDashboard()
        {
            // Configurar textos básicos
            subtitleLabel.Text = "Vista rápida del inventario de la farmacia";

            // Tarjeta 1 - Estado del Inventario
            card1Title.Text = "Estado del Inventario";
            card1Button.Text = "Ver Reporte Detallado";

            // Tarjeta 2 - Ingresos
            card2Title.Text = "Rs. 8,55,875 Ingresos : Ene 2022";
            card2Button.Text = "Ver Reporte Detallado";

            // Tarjeta 3 - Descargar Reporte
            card3Title.Text = "Descargar Reporte";
            card3Stat1Value.Text = "298";
            card3Stat1Label.Text = "Medicinas Disponibles";
            card3Link1.Text = "Visitar Inventario";

            card3Stat2Value.Text = "01";
            card3Stat2Label.Text = "Escasez de Medicina";
            card3Link2.Text = "Resolver Ahora";

            // Tarjeta 4 - Inventario
            card4Title.Text = "Inventario";
            card4Button.Text = "Ir a Configuración";
            card4Stat1Value.Text = "298";
            card4Stat1Label.Text = "Total de Medicinas";
            card4Stat2Value.Text = "24";
            card4Stat2Label.Text = "Grupos de Medicina";

            // Tarjeta 5 - Mi Farmacia
            card5Title.Text = "Mi Farmacia";
            card5Button.Text = "Ir a Gestión de Usuarios";
            card5Stat1Value.Text = "04";
            card5Stat1Label.Text = "Total de Proveedores";
            card5Stat2Value.Text = "05";
            card5Stat2Label.Text = "Total de Usuarios";

            // Tarjeta 6 - Reporte Rápido
            card6Title.Text = "Reporte Rápido";
            card6Month.Text = "Enero 2022";
            card6Stat1Value.Text = "70,856";
            card6Stat1Label.Text = "Cantidad de Medicinas Vendidas";
            card6Stat2Value.Text = "5,288";
            card6Stat2Label.Text = "Facturas Generadas";
        }

        private void WireEvents()
        {
            // Conectar eventos de botones
            card1Button.Click += (s, e) => MessageBox.Show("Navegando a: Estado del Inventario");
            card2Button.Click += (s, e) => MessageBox.Show("Navegando a: Reporte de Ingresos");

            // Conectar eventos de links
            card3Link1.Click += (s, e) => MessageBox.Show("Navegando a: Inventario");
            card3Link2.Click += (s, e) => MessageBox.Show("Resolviendo escasez de medicina");

            // Conectar eventos de configuración
            card4Button.Click += (s, e) => MessageBox.Show("Navegando a: Configuración");
            card5Button.Click += (s, e) => MessageBox.Show("Navegando a: Gestión de Usuarios");

            // Do not set hover/color changes at runtime to keep UI static.
            // SetupButtonHoverEffects();
        }

        private void SetupButtonHoverEffects()
        {
            // This method now unused when UI is static; kept for optional dynamic mode.
            card1Button.BackColor = Color.FromArgb(41, 128, 185);
            card2Button.BackColor = Color.FromArgb(46, 204, 113);
            card4Button.BackColor = Color.FromArgb(41, 128, 185);
            card5Button.BackColor = Color.FromArgb(41, 128, 185);

            card1Button.ForeColor = Color.White;
            card2Button.ForeColor = Color.White;
            card4Button.ForeColor = Color.White;
            card5Button.ForeColor = Color.White;

            card1Button.FlatAppearance.BorderSize = 0;
            card2Button.FlatAppearance.BorderSize = 0;
            card4Button.FlatAppearance.BorderSize = 0;
            card5Button.FlatAppearance.BorderSize = 0;

            SetupButtonHover(card1Button, Color.FromArgb(41, 128, 185));
            SetupButtonHover(card2Button, Color.FromArgb(46, 204, 113));
            SetupButtonHover(card4Button, Color.FromArgb(41, 128, 185));
            SetupButtonHover(card5Button, Color.FromArgb(41, 128, 185));

            card3Link1.LinkColor = Color.FromArgb(52, 152, 219);
            card3Link1.ActiveLinkColor = Color.FromArgb(41, 128, 185);
            card3Link2.LinkColor = Color.FromArgb(230, 126, 34);
            card3Link2.ActiveLinkColor = Color.FromArgb(211, 84, 0);
        }

        private void SetupButtonHover(Button button, Color baseColor)
        {
            button.MouseEnter += (s, e) => button.BackColor = DarkenColor(baseColor);
            button.MouseLeave += (s, e) => button.BackColor = baseColor;
        }

        private void ApplyCardStyles()
        {
            // kept for reference; when UI is static the equivalent styles are applied in Designer.cs
            card1ColorBar.BackColor = Color.FromArgb(41, 128, 185);
            card2ColorBar.BackColor = Color.FromArgb(46, 204, 113);
            card3ColorBar.BackColor = Color.FromArgb(52, 152, 219);
            card4ColorBar.BackColor = Color.FromArgb(44, 62, 80);
            card5ColorBar.BackColor = Color.FromArgb(44, 62, 80);
            card6ColorBar.BackColor = Color.FromArgb(44, 62, 80);

            card3Stat1Value.ForeColor = Color.FromArgb(52, 152, 219);
            card3Stat2Value.ForeColor = Color.FromArgb(230, 126, 34);
            card4Stat1Value.ForeColor = Color.FromArgb(44, 62, 80);
            card4Stat2Value.ForeColor = Color.FromArgb(44, 62, 80);
            card5Stat1Value.ForeColor = Color.FromArgb(44, 62, 80);
            card5Stat2Value.ForeColor = Color.FromArgb(44, 62, 80);
            card6Stat1Value.ForeColor = Color.FromArgb(46, 204, 113);
            card6Stat2Value.ForeColor = Color.FromArgb(52, 152, 219);

            card6Month.Font = new Font("Segoe UI", 10, FontStyle.Italic);
            card6Month.ForeColor = Color.Gray;
        }

        private Color DarkenColor(Color color)
        {
            int r = Math.Max(color.R - 20, 0);
            int g = Math.Max(color.G - 20, 0);
            int b = Math.Max(color.B - 20, 0);
            return Color.FromArgb(r, g, b);
        }

        public void UpdateDashboardData(
            int totalMedicines = 298,
            int medicineGroups = 24,
            int totalSuppliers = 4,
            int totalUsers = 5,
            string revenue = "Rs. 8,55,875",
            int medicinesSold = 70856,
            int invoicesGenerated = 5288,
            int medicinesAvailable = 298,
            int medicineShortage = 1)
        {
            // Actualizar tarjeta 2
            card2Title.Text = $"{revenue} Ingresos : Ene 2022";

            // Actualizar tarjeta 3
            card3Stat1Value.Text = medicinesAvailable.ToString();
            card3Stat2Value.Text = medicineShortage.ToString("00");

            // Actualizar tarjeta 4
            card4Stat1Value.Text = totalMedicines.ToString();
            card4Stat2Value.Text = medicineGroups.ToString();

            // Actualizar tarjeta 5
            card5Stat1Value.Text = totalSuppliers.ToString("00");
            card5Stat2Value.Text = totalUsers.ToString("00");

            // Actualizar tarjeta 6
            card6Stat1Value.Text = medicinesSold.ToString("N0");
            card6Stat2Value.Text = invoicesGenerated.ToString("N0");
        }
    }
}