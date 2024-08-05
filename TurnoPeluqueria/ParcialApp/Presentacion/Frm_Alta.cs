
using ParcialApp.Dominio;
using ParcialApp.Servicios;
using ParcialApp.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParcialApp.Presentacion
{
    public partial class Frm_Alta : Form
    {
        FabricaServicio fabrica = null;
        IServicio service = null;
        Turno nuevo = null;

        public Frm_Alta(FabricaServicio fabrica)
        {
            InitializeComponent();
            this.fabrica = fabrica;
            service = fabrica.CrearServicio();
            nuevo = new Turno();

        }




        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCliente.Text))
            {
                MessageBox.Show("Debe ingresar un cliente....",
                   "Control",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation);
                return;
            }
            if(dtpFecha.Value <= DateTime.Now.AddDays(0))
            {
                MessageBox.Show("Debe ingresar una fecha valida....",
                  "Control",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Exclamation);
                return;
            }
            if (dtpFecha.Value >= DateTime.Now.AddDays(45))
            {
                MessageBox.Show("La fecha de reserva no puede ser superior a 45 dias....",
                 "Control",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Exclamation);
                return;
            }

            string fecha = dtpFecha.Text;
            string hora = cboHora.Text;
            if (dgvDetalles.Rows.Count == 0)//debe ingresar al menos un detalle ==0
                                            //en este caso pedia 3 ingredientes
            {
                MessageBox.Show("Debe ingresar al menos un turno"
                   , "Control"
                   , MessageBoxButtons.OK
                   , MessageBoxIcon.Exclamation);
                return;
            }
            if (service.ExisteTurno(fecha, hora) == true)
            {
                MessageBox.Show("El turno ya existe"
                   , "Control"
                   , MessageBoxButtons.OK
                   , MessageBoxIcon.Exclamation);
                return;
            }
            GrabarTurno();

        }

        private void GrabarTurno()
        {
            nuevo.cliente = txtCliente.Text;
            nuevo.fecha = dtpFecha.Text;
            nuevo.hora = cboHora.Text;
            if (service.SaveTurno(nuevo))
            {
                MessageBox.Show("Se registro con exito el Turno..."
                  , "Informe"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Exclamation);

                this.Dispose();
            }
            else
            {
                MessageBox.Show("No se pudo registrar el turno...."
                 , "Error"
                 , MessageBoxButtons.OK
                 , MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();

            }
            else
            {
                return;
            }
        }

        private void Frm_Alta_Presupuesto_Load(object sender, EventArgs e)
        {
            cboServicios.SelectedIndex = -1;
            dtpFecha.Value = DateTime.Now;
            cboHora.SelectedIndex = -1;
            CargarCombo();
        }

        private void CargarCombo()
        {
            cboServicios.DataSource = service.GetServicios();
            cboServicios.ValueMember = "id";
            cboServicios.DisplayMember = "nombre";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
           if(cboServicios.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Servicio...."
                   , "Control"
                   , MessageBoxButtons.OK
                   , MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtObservaciones.Text))
            {
                MessageBox.Show("Debe ingresar una observacion...."
                                  , "Control"
                                  , MessageBoxButtons.OK
                                  , MessageBoxIcon.Exclamation);
                return;
            }

            foreach(DataGridViewRow fila in dgvDetalles.Rows)
            {
                Servicio oServicio = (Servicio)cboServicios.SelectedItem;
                if (fila.Cells["servicio"].Value.ToString().Equals(cboServicios.Text))
                {
                    MessageBox.Show("El servicio ya esta cargado...."
                  , "Control"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Exclamation);
                    return;
                } 
            }
            Servicio s = (Servicio)cboServicios.SelectedItem;
            string observaciones = txtObservaciones.Text;

            DetalleTurno detalle = new DetalleTurno(s, observaciones);
            nuevo.AgregarDetalle(detalle);
            dgvDetalles.Rows.Add(new object[] { s, observaciones, "Quitar" });
        }

       

        private void dgvDetalles_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 2)
            {
                //completar...
                nuevo.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);
            }
        }
    }
}
