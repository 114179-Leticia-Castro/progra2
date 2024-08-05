using RECETASPRACTICA.Entidades;
using RECETASPRACTICA.Servicios;
using RECETASPRACTICA.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RECETASPRACTICA
{
    public partial class FrmAltaRecetas : Form
    {
        FabricaServicio fabrica = null;
        IServicio servicio = null;
        Receta nuevo = null;

        public FrmAltaRecetas(FabricaServicio fabrica)
        {
            InitializeComponent();
            this.fabrica = fabrica;
            servicio=fabrica.CrearServicio();
            nuevo=new Receta();

        }

        private void FrmAltaRecetas_Load(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtCheff.Text = "";
            cboTipo.SelectedIndex = -1;
            lblRecetaNro.Text = lblRecetaNro.Text + " " + servicio.TraerProximaReceta().ToString();
            cargarIngrediente();

        }

        private void cargarIngrediente()
        {
            cboProducto.DataSource = servicio.TraerIngrediente();
            cboProducto.ValueMember = "ingredienteId";
            cboProducto.DisplayMember = "nombre";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboProducto.SelectedIndex == -1)//valido lo que cargo en la grilla
            {
                MessageBox.Show("Debe seleccionar un Ingrediente...."
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad valida...."
                   , "Control"
                   , MessageBoxButtons.OK
                   , MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow fila in dgvDetalles.Rows)//recorre la grilla y ve si el producto existe,
                                                              //si ya esta me llama la atencion
            {
                Ingrediente oIngrediente = (Ingrediente)cboProducto.SelectedItem;
                if (fila.Cells["ColIngrediente"].Value.ToString().Equals(cboProducto.Text))//validacion de que NO SE PUEDE
                                                                                              //agregar el mismo producto
                {
                    MessageBox.Show("El ingrediente ya esta cargado...."
                   , "Control"
                   , MessageBoxButtons.OK
                   , MessageBoxIcon.Exclamation);
                    return;
                }
                
            }
            Ingrediente i = (Ingrediente)cboProducto.SelectedItem;//xq el dao me da productos directamente y no la datatable

            int cant = Convert.ToInt32(txtCantidad.Text);
            DetalleReceta detalle = new DetalleReceta(i, cant);//esto es lo que hay de la clase detalle orden

            nuevo.AgregarDetalle(detalle);
            dgvDetalles.Rows.Add(new object[] { i,  cant, "Quitar" });//este cant es el que se
            lblTotalIngredientes.Text = "Total de Ingredientes: " + dgvDetalles.Rows.Count.ToString();
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 2)//es el boton quitar
            {
                nuevo.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea cancelar la orden?", "Cancelar?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)

                this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un Nombre de receta...."
                  , "Control"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtCheff.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del cheff...."
                  , "Control"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvDetalles.Rows.Count < 3)//debe ingresar al menos un detalle ==0
                                            //en este caso pedia 3 ingredientes
            {
                MessageBox.Show("Debe ingresar al menos tres ingredientes...."
                   , "Control"
                   , MessageBoxButtons.OK
                   , MessageBoxIcon.Exclamation);
                return;
            }
            GrabarReceta();

        }

        private void GrabarReceta()
        {
            nuevo.Nombre = txtNombre.Text;
            nuevo.Cheff = txtCheff.Text;
            nuevo.TipoReceta = cboTipo.SelectedIndex + 1;

            if (servicio.CrearReceta(nuevo))
            {
                MessageBox.Show("Se registro con exito la Receta..."
                  , "Informe"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Exclamation);

                this.Dispose();

            }
            else
            {
                MessageBox.Show("No se pudo registrar la Receta...."
                  , "Error"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Exclamation);
            }
        }
    }
}
