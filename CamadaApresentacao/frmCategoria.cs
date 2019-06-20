using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamadaNegocio;

namespace CamadaApresentacao
{
    public partial class frmCategoria : Form
    {
        private bool eNovo = false;
        private bool eEditar = false;

        public frmCategoria()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.txtNome, "Insira o nome da Categoria");
        }
        // Mostrar Mensagem de confirmaçao

        private void MensagemOK(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comercio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Mensagem de Erro

        private void MensagemErro(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comercio", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Limpar campos
        private void Limpar()
        {
            this.txtNome.Text = string.Empty;
            this.txtIdCategoria.Text = string.Empty;
            this.txtDescricao.Text = string.Empty;
        }

        //Habilitar os text box
        private void Habilitar(bool valor)
        {
            this.txtNome.ReadOnly = !valor;
            this.txtDescricao.ReadOnly = !valor;
            this.txtIdCategoria.ReadOnly = !valor;

        }

        //Habilitar os Botões
        private void botoes()
        {
            if (this.eNovo || this.eEditar)
            {
                this.Habilitar(true);
                this.btnNovo.Enabled = false;
                this.btnSalvar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNovo.Enabled = true;
                this.btnSalvar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;

            }

        }

        // Ocultar as colunas do Grid

        private void OcultarColunas()
        {
            this.dataLista.Columns[0].Visible = false;
            this.dataLista.Columns[1].Visible = false;

        }

        //Metodo Mostrar no DataGrid

        private void Mostrar()
        {
            this.dataLista.DataSource = NCategoria.Mostrar();
            this.OcultarColunas();
            lblTotal.Text = "Total de registros: " + Convert.ToString( dataLista.Rows.Count);

        }

        //Metodo Buscar pelo nome

        private void BuscarNome()
        {
            this.dataLista.DataSource = NCategoria.BuscarNome(this.txtBuscar.Text);
            this.OcultarColunas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataLista.Rows.Count);

        }
        private void frmCategoria_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.botoes();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.eEditar = false;
            this.botoes();
            this.Limpar();
            this.Habilitar(true);
            this.txtNome.Focus();
            this.txtIdCategoria.Enabled = false;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if(txtNome.Text == string.Empty)
                {
                    MensagemErro("Preencha todos os campos");
                    errorIcone.SetError (txtNome, "Insira o nome");
                }
                else
                {
                    if(this.eNovo)
                    {
                        resp = NCategoria.Inserir(this.txtNome.Text.Trim().ToUpper(), this.txtDescricao.Text.Trim());
                    }
                    else
                    {
                        resp = NCategoria.Editar(Convert.ToInt32(this.txtIdCategoria.Text),
                            this.txtNome.Text.Trim().ToUpper(),
                            this.txtDescricao.Text.Trim());
                    }
                    if(resp.Equals("OK"))
                    {
                        if(this.eNovo)
                        {
                            this.MensagemOK("Registro salvo com sucesso!");
                        }
                        else
                        {
                            this.MensagemOK("Registro editado com sucesso!");
                        }
                    }
                    else
                    {
                        this.MensagemErro(resp);
                    }

                    this.eNovo = false;
                    this.eEditar = false;
                    this.botoes();
                    this.Limpar();
                    this.Mostrar();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
