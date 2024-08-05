using RecetasSLN.datos;
using RecetasSLN.datos.DTOs;
using RecetasSLN.dominio;
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

namespace RecetasSLN.presentación
{
    public partial class FrmConsultar : Form
    {
        FabricaServicio fabrica = null;
        IServicio servicio = null;
        Pedido update = null;
       
        public FrmConsultar(FabricaServicio fabrica)
        {
            InitializeComponent();
            this.fabrica = fabrica;
            servicio = fabrica.CrearServicio();
            update = new Pedido();
            
        }

        private void FrmConsultar_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Now.AddDays(-30);
            dtpHasta.Value = DateTime.Now;
            lblTotal.Text = "Total de pedidos: " + "0";
            CargarCombo();
        }

        private void CargarCombo()
        {
            cboClientes.DataSource = servicio.TraerCliente();
            cboClientes.ValueMember = "ID";
            cboClientes.DisplayMember = "apellido";
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //Completar...
            if(dtpDesde.Value < DateTime.Now.AddDays(-30) && dtpHasta.Value>DateTime.Now)
            {
                MessageBox.Show("Debe ingresar una fecha valida!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<PedidoDTO> lPedido = servicio.TraerPedido((int)cboClientes.SelectedValue, dtpDesde.Value, dtpHasta.Value);//tendriamos un servicio con estos parametros, son los datos que yo tengo en la pantalla y que va a cargar el boton consultar
            dgvPedidos.Rows.Clear();
            foreach (PedidoDTO p in lPedido)//cargo desde la lista presupuesto la grilla
            {
                //creamos la grilla con los datos del presupuesto
                dgvPedidos.Rows.Add(new object[] {p.Codigo,
                                                  p.Cliente,
                                                  p.FechaEntrega,
                                                  p.Entregado,
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
                string Entregado = dgvPedidos.CurrentRow.Cells["colEntregado"].Value.ToString();
                int codigo = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["colCodigo"].Value);

                if(Entregado == "S")
                {
                    bool actualizar = servicio.RegistrarPedido(codigo);
                    if (actualizar)
                    {
                        dgvPedidos.CurrentRow.Cells["colEntregado"].Value = "N";
                        MessageBox.Show("Se actualizo el estado de la entrega ", "Entregado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
               
            }
            if (dgvPedidos.CurrentCell.ColumnIndex == 4)
            {
               
                if (MessageBox.Show("Esta seguro que desea borrar la orden? Esta accion no se puede deshacer", "Borrar?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    int codigo = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["colCodigo"].Value.ToString());
                    servicio.RegistrarBaja(codigo);
                    dgvPedidos.Rows.Clear();
                }

            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
