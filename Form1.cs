using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeradorArquivoTexto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvResultado.ColumnCount = 2;
            dgvResultado.Columns[0].HeaderText = "Nome";
            dgvResultado.Columns[0].Width = 230;
            dgvResultado.Columns[1].HeaderText = "Salário";
            dgvResultado.Columns[1].Width = 67;
        }

        private void bntLinhasRegistro_Click(object sender, EventArgs e)
        {
            int numemeroFuncionarios = Convert.ToInt32(txtNFuncionarios.Text);
            if (numemeroFuncionarios < 1)
                numemeroFuncionarios = 1;
            int i = 0;

            do
            {
                var linhaTabela = new DataGridViewRow();
                linhaTabela.Cells.Add(new DataGridViewTextBoxCell { Value = string.Empty });
                linhaTabela.Cells.Add(new DataGridViewTextBoxCell { Value = 0 });
                dgvResultado.Rows.Add(linhaTabela);
            } while (++i < numemeroFuncionarios);

            txtNFuncionarios.Enabled = false;
            btnCriarArquivo.Enabled = true;
            btnReiniciar.Enabled = true;
            bntLinhasRegistro.Enabled = false;

        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            dgvResultado.Rows.Clear();
            txtNFuncionarios.Text = string.Empty;
            txtNFuncionarios.Enabled = true;
            btnCriarArquivo.Enabled = false;
            btnReiniciar.Enabled = false;
            bntLinhasRegistro.Enabled = true;
        }

        private void btnCriarArquivo_Click(object sender, EventArgs e)
        {
            if (!ValidarDados())
            {
                MessageBox.Show("Os dados possuem problemas. Verifique se não deixou nenhum nome em branco ou se existe um valor correto para os salários de cada um");
            }
            else if (sfdGravarArquivo.ShowDialog() == DialogResult.OK)
            {
                GerarArquivo();
                MessageBox.Show("Aquivo gerado com sucesso!");
            }
        }

        private bool ValidarDados()
        {
            int i = 0;
            bool dadosValidados = true;
            double stringToDouble;

            do
            {
                if (string.IsNullOrWhiteSpace(dgvResultado.Rows[i].Cells[0].Value.ToString()))
                    dadosValidados = false;
                if (!Double.TryParse(dgvResultado.Rows[i].Cells[1].Value.ToString(), out stringToDouble))
                    dadosValidados = false;
            } while (++i < dgvResultado.Rows.Count);

            return dadosValidados;
        }

        private void GerarArquivo()
        {
            StreamWriter wr = new StreamWriter(sfdGravarArquivo.FileName, true);
            
            for (int j = 0; j < dgvResultado.Rows.Count; j++)
            {
                wr.WriteLine(dgvResultado.Rows[j].Cells[0].Value.ToString() + ";" + dgvResultado.Rows[j].Cells[1].Value.ToString());

            }
            wr.Close();
        }

        
    }
}
