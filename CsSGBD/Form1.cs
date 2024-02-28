using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsSGBD
{
    public partial class FormLocal : Form
    {
        public FormLocal()
        {
            InitializeComponent();
        }

        private void Limpar()
        {
            textMatricula.Text = "";
            textCpf.Text = "";
            textNome.Text = "";
            textEndereco.Text = "";
        }

        private void Pesquisar()
        {
            try
            {
                int matricula = 0;
                bool n = Int32.TryParse(textMatricula.Text, out matricula);

                Empregado empregado = new Empregado();
                empregado.Matricula = matricula;
                empregado.Cpf = textCpf.Text;
                empregado.Nome = textNome.Text;

                dataGridViewEmpregado.DataSource = empregado.Pesquisar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro);
            }
        }

        private void Salvar()
        {
            try
            {
                int matricula = 0;
                bool n = Int32.TryParse(textMatricula.Text.ToString(), out matricula);

                Empregado empregado = new Empregado();
                empregado.Matricula = matricula;
                empregado.Cpf = textCpf.Text.ToString();
                empregado.Nome = textNome.Text.ToString();
                empregado.Endereco = textEndereco.Text.ToString();

                if (matricula == 0)
                    empregado.Incluir();
                else
                    empregado.Alterar();

                Limpar();
                Pesquisar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
        }

        private void Excluir()
        {
            try
            {
                int matricula = 0;
                bool n = Int32.TryParse(textMatricula.Text.ToString(), out matricula);

                Empregado empregado = new Empregado();
                empregado.Matricula = matricula;
                empregado.Excluir();
                Limpar();
                Pesquisar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
        }


        private void FormLocal_Load(object sender, EventArgs e)
        {

        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        private void buttonLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
            Pesquisar();
        }

        private void dataGridViewEmpregado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textMatricula.Text = dataGridViewEmpregado.Rows[e.RowIndex].Cells[0].Value.ToString();
            textCpf.Text = dataGridViewEmpregado.Rows[e.RowIndex].Cells[1].Value.ToString();
            textNome.Text = dataGridViewEmpregado.Rows[e.RowIndex].Cells[2].Value.ToString();
            textEndereco.Text = dataGridViewEmpregado.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
    }
}
