using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsEF6Demo.Data;
using WinFormsEF6Demo.Models;

namespace WinFormsEF6Demo.Forms
{
    public partial class FormProducto : Form
    {
        private int? _idSeleccionado = null;
        public FormProducto()
        {
            InitializeComponent();
        }
        private void FormProducto_Load(object sender, EventArgs e)
        {
            var estados = new[]
            {
                new { Valor = "S", Texto = "Stock" },
                new { Valor = "A", Texto = "Agotado" }
            };
            cmbEstado.DataSource = estados;
            cmbEstado.ValueMember = "Valor";
            cmbEstado.DisplayMember = "Texto";
            var iva = new[]
            {
                new { Valor = 15, Texto = "15%" },
                new { Valor = 0, Texto = "0%" },
            };
            cmbIva.DataSource = iva;
            cmbIva.ValueMember = "Valor";
            cmbIva.DisplayMember = "Texto";
            numPvp.Minimum = 0;
            numPvp.Maximum = 9999;
            numPvp.Value = 0.00m;
            numPvp.DecimalPlaces = 2;
            CargarDatos();
        }
        private void CargarDatos()
        {
            using (var db = new AppDb())
            {
                dgvProducto.DataSource = db.Productos
                    .OrderBy(p => p.ProductoId)
                    .Select(p => new { p.ProductoId, p.Descripcion, p.Estado, p.Pvp, p.Iva })
                    .ToList();
            }
            dgvProducto.ClearSelection();
            _idSeleccionado = null;
        }
        private void LimpiarFormulario()
        {
            txtDescripcion.Text = "";
            cmbEstado.SelectedIndex = 0;
            numPvp.Value = 0.00m;
            cmbIva.SelectedIndex = 0;
            errorProvider1.SetError(txtDescripcion, "");
            _idSeleccionado = null;
        }
        private bool ValidarFormulario()
        {
            bool ok = true;
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                errorProvider1.SetError(txtDescripcion, "Descripción es obligatoria");
                ok = false;
            }
            else errorProvider1.SetError(txtDescripcion, "");
            return ok;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtDescripcion.Focus();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;
            using (var db = new AppDb())
            {
                if (_idSeleccionado == null)
                {
                    var pro = new Producto
                    {
                        Descripcion = txtDescripcion.Text.Trim(),
                        Estado = cmbEstado.SelectedValue.ToString(),
                        Pvp = numPvp.Value,
                        Iva = (int)cmbIva.SelectedValue
                    };
                    db.Productos.Add(pro);
                }
                else
                {
                    var pro = db.Productos.Find(_idSeleccionado.Value);
                    if (pro == null) return;
                    pro.Descripcion = txtDescripcion.Text.Trim();
                    pro.Estado = cmbEstado.SelectedValue.ToString();
                    pro.Pvp = numPvp.Value;
                    pro.Iva = (int)cmbIva.SelectedValue;
                }
                db.SaveChanges();
            }
            CargarDatos();
            LimpiarFormulario();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccione un registro para editar.", "Anteción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtDescripcion.Focus();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(_idSeleccionado == null)
            {
                MessageBox.Show("Seleccione un registro para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var r = MessageBox.Show("¿Eliminar el registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;
            using (var db = new AppDb())
            {
                var pro = db.Productos.Find(_idSeleccionado.Value);
                if (pro != null)
                {
                    db.Productos.Remove(pro);
                    db.SaveChanges();
                }
            }
            CargarDatos();
            LimpiarFormulario();
        }
        private void dgvProducto_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducto.CurrentRow == null || dgvProducto.CurrentRow.Index < 0) return;

            var row = dgvProducto.CurrentRow;
            if (row.Cells["ProductoId"] == null) return;
            _idSeleccionado = (int?)row.Cells["ProductoId"].Value;

            txtDescripcion.Text = row.Cells["Descripcion"].Value?.ToString() ?? "";
            var estado = row.Cells["Estado"].Value?.ToString() ?? "S";
            cmbEstado.SelectedValue = estado;
            numPvp.Value = row.Cells["Pvp"].Value != null ? Convert.ToDecimal(row.Cells["Pvp"].Value) : 0.00m;
            var iva = row.Cells["Iva"].Value != null ? Convert.ToInt32(row.Cells["Iva"].Value) : 15;
            cmbIva.SelectedValue = iva;
        }
    }
}
