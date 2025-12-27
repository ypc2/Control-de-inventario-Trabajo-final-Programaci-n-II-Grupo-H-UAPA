using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmaControlPlus
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            ApplyStyling();
        }

        private void ApplyStyling()
        {
            // Center align title labels
            lblWelcome.Left = (panelHeader.Width - lblWelcome.Width) / 2;
            lblSystem.Left = (panelHeader.Width - lblSystem.Width) / 2;

            // Center other controls
            lblLogin.Left = (this.ClientSize.Width - lblLogin.Width) / 2;
            lnkForgot.Left = (this.ClientSize.Width - lnkForgot.Width) / 2;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validación de campos
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña", "Error de Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Aquí va tu lógica de autenticación real
            // Ejemplo simple:
            if (txtUsername.Text == "admin" && txtPassword.Text == "1234")
            {
                // Hide login and open main form. Form1 will show the green modal itself (on Shown).
                this.Hide();

                Form1 formPrincipal = new Form1();

                // Optional: Pass data to main form
                // formPrincipal.Usuario = txtUsername.Text;

                // Show the main form; its OnShown will display the green modal for the configured duration.
                formPrincipal.Show();

                // Close the login form when the main form closes
                formPrincipal.FormClosed += (s, args) => this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Login Fallido",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please contact system administrator for password reset.",
                "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(52, 152, 219);
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(41, 128, 185);
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }
    }

    // Partial class to add startup modal behavior without modifying the main Form1 designer file.
    public partial class Form1
    {
        private bool _startupModalShown = false;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Ensure the modal is shown only once when the form first appears
            if (!_startupModalShown)
            {
                _startupModalShown = true;

                using (var modal = new SuccessModal("¡Login exitoso!", 2000))
                {
                    // ShowDialog with 'this' centers the modal over Form1 and blocks until it closes.
                    modal.ShowDialog(this);
                }
            }
        }
    }

    // Self-contained, reusable green success modal. Adjust colors/sizes if needed.
    internal class SuccessModal : Form
    {
        private readonly Timer _closeTimer;
        private readonly Label _messageLabel;

        public SuccessModal(string message, int milliseconds = 7000)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            TopMost = true;
            BackColor = Color.FromArgb(46, 204, 113); // green
            Width = 360;
            Height = 120;
            Opacity = 0.98;

            _messageLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.White,
                Text = message,
                Padding = new Padding(12)
            };

            Controls.Add(_messageLabel);

            _closeTimer = new Timer { Interval = Math.Max(1000, milliseconds) };
            _closeTimer.Tick += (s, e) =>
            {
                _closeTimer.Stop();
                Close();
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Center over owner if provided, otherwise center on current screen
            if (Owner != null)
            {
                var ownerRect = Owner.Bounds;
                var ownerScreenPoint = Owner.PointToScreen(Point.Empty);
                int x = ownerScreenPoint.X + (ownerRect.Width - Width) / 2;
                int y = ownerScreenPoint.Y + (ownerRect.Height - Height) / 2;
                Location = new Point(Math.Max(0, x), Math.Max(0, y));
            }
            else
            {
                var screen = Screen.FromPoint(Cursor.Position).WorkingArea;
                Location = new Point(screen.Left + (screen.Width - Width) / 2,
                                     screen.Top + (screen.Height - Height) / 2);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _closeTimer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _closeTimer?.Dispose();
                _messageLabel?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}