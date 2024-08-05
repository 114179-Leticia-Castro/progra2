using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurnoMedico.Entidades;
using TurnoMedico.Servicios;
using TurnoMedico.Servicios.Interfaz;

namespace TurnoMedico
{
    public partial class FrmAlta : Form
    {
        FabricaServicio fabrica = null;
        IService service = null;
        Turno nuevo = null;

        public FrmAlta(FabricaServicio fabrica)
        {
            InitializeComponent();
            this.fabrica = fabrica;
            service = fabrica.CrearServicio();
            nuevo = new Turno();
        }

        private void FrmAlta_Load(object sender, EventArgs e)
        {
            txtPaciente.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            txtMotivo.Text = "";
            cboMedico.SelectedIndex = -1;
            dtpfecha.Value = DateTime.Now;
            cargarCombo();
        }

        private void cargarCombo()
        {
            cboMedico.DataSource = service.TraerMedico();
            cboMedico.ValueMember = "matricula";
            cboMedico.DisplayMember = "especialidad";
        }

       

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(cboMedico.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Medico....",
                    "Control",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            if(string.IsNullOrEmpty(txtMotivo.Text))
            {
                MessageBox.Show("Debe ingresar un motivo de consulta....",
                    "Control",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            if(dtpfecha.Value<=DateTime.Now.AddDays(0))
            {
                MessageBox.Show("Debe ingresar una fecha valida....",
                    "Control",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            if (cboHora.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una hora....",
                    "Control",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                Medico oMedico = (Medico)cboMedico.SelectedItem;
                if (fila.Cells["colMedico"].Value.ToString().Equals(cboMedico.Text) && fila.Cells["colFecha"].Value.ToString().Equals(dtpfecha.Text))
                {
                    MessageBox.Show("El medico ya esta cargado para esa fecha",
                    "Control",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                    return;
                }
            }



            Medico medico = (Medico)cboMedico.SelectedItem;
            string motivoConsulta = txtMotivo.Text;
            string fecha = dtpfecha.Value.ToShortDateString();
            string hora = cboHora.Text;

            DetalleTurno detalle = new DetalleTurno(medico, motivoConsulta, fecha, hora);
                   
            
            nuevo.AgregarDetalle(detalle);
            dgvDetalles.Rows.Add(new object[] { medico, motivoConsulta, fecha, hora, "Quitar" });//para q aparezca el nombre del medico hago medico.apellido
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 4)//es el boton quitar
            {
                nuevo.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea cancelar el turno?", "Cancelar?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)

                this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtPaciente.Text))
            {
                MessageBox.Show("Debe ingresar un paciente....",
                   "Control",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation);
                return;
            }
            Medico oMedico = (Medico)cboMedico.SelectedItem;
            string fecha = dtpfecha.Text;
            string hora = cboHora.Text;
            if (dgvDetalles.Rows.Count == 0 )//debe ingresar al menos un detalle ==0
                                            //en este caso pedia 3 ingredientes
            {
                MessageBox.Show("Debe ingresar al menos un turno"
                   , "Control"
                   , MessageBoxButtons.OK
                   , MessageBoxIcon.Exclamation);
                return;
            }
            if(service.ExisteTurno(fecha, hora, oMedico.Matricula) == true)
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
            nuevo.Paciente=txtPaciente.Text;
            if (service.CrearTurno(nuevo))
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
    }
}
