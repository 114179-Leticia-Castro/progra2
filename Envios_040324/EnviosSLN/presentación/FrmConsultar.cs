using ExamenSLN.datos;
using ExamenSLN.datos.DTOs;
using ExamenSLN.dominio;
using RecetasSLN.Servicios;
using RecetasSLN.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenSLN.presentación
{
    public partial class FrmConsultar : Form
    {
        FabricaServicio fabrica = null;
        IServicio servicio = null;
        Envio update = null;
       
        public FrmConsultar(FabricaServicio fabrica)
        {
            InitializeComponent();
            this.fabrica = fabrica;
            servicio = fabrica.CrearServicio();
            update = new Envio();
            
        }

        private void FrmConsultar_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Now.AddDays(-30);
            lblTotal.Text = "Total de pedidos : " + "0";
            CargarCombo();
        }

        private void CargarCombo()
        {
            //Completar...
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //Completar...
            List<Envio> lPedido = servicio.ListaEnviosFiltrados(txtDni.Text, dtpDesde.Value, dtpHasta.Value);
            dgvPedidos.Rows.Clear();
            foreach (Envio en in lPedido)//cargo desde la lista presupuesto la grilla
            {
                //creamos la grilla con los datos del presupuesto
                dgvPedidos.Rows.Add(new object[] {en.Codigo,
                                                  en.DniCliente,
                                                  en.FechaEnvio,
                                                  en.Estado,
                                                  "eliminar"});//tiene la coma de mas xq tiene una columna de mas de acciones
            }
            lblTotal.Text = "Total de pedidos: " + dgvPedidos.Rows.Count.ToString();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Completar...
            if (MessageBox.Show("Seguro que desea salir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgvPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Completar...
            if (dgvPedidos.CurrentCell.ColumnIndex == 3)
            {
                string Entregado = dgvPedidos.CurrentRow.Cells["colEntrega"].Value.ToString();
                Envio oEnvio = (Envio)dgvPedidos.CurrentRow.Cells["colCodigo"].Value;

                if (Entregado == "S")
                {
                    bool registrar = servicio.CargarEnvio(oEnvio);
                    if (registrar)
                    {
                        dgvPedidos.CurrentRow.Cells["colEntrega"].Value = "Para Enviar";
                        MessageBox.Show("Se registro el envio ", "Entregado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

            }
        }
    }
}
