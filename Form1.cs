using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Agendamento
{
    public partial class Form1 : Form
    {
        MySqlConnection conexao = new MySqlConnection("server=localhost; port=3306; user id=root; password=admin123; database=consulta; SSL Mode=None");
        MySqlCommand command;
        MySqlDataReader rs;
        MySqlDataAdapter data_adapter;

        public Form1()
        {
            InitializeComponent();
            try
            {
                conexao.Open();
                populaDataGrid();
            }
            catch(Exception ex0) {
                MessageBox.Show(null, "Erro na conexão" + ex0.ToString(), "Erro");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                command = new MySqlCommand("DELETE FROM agendamentos " +
                            "WHERE cpf_id = @cpf_id", conexao);

                command.Parameters.AddWithValue("@cpf_id", txtcpf.Text);

                command.Prepare();
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show(null, "CPF " + txtcpf.Text + " excluído com sucesso", "Agendamento");
                    limparCampos();
                    populaDataGrid();
                }
                else
                {
                    MessageBox.Show(null, "O CPF não foi encontrado!", "Agendamento");
                }

                command.Dispose();
            }
            catch (Exception ex3)
            {
                MessageBox.Show(null, "A eliminação não foi efetuada. Verifique os dados.", "Eliminação do Agendamento");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                command = new MySqlCommand("INSERT INTO agendamentos (cpf_id,nome_completo,data_nascimento,data_consulta)" +
                            " VALUES (@cpf_id,@nome_completo,@data_nascimento,@data_consulta)", conexao);

                command.Parameters.AddWithValue("@cpf_id", txtcpf.Text);
                command.Parameters.AddWithValue("@nome_completo", txtnome.Text);
                command.Parameters.AddWithValue("@data_nascimento", txtnascimento.Text);
                command.Parameters.AddWithValue("@data_consulta", txtconsulta.Text);

                command.Prepare();
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show(null, "Cadastrado concluído.", "Cadastro do Agendamento");
                    limparCampos();
                    populaDataGrid();
                }
                else
                {
                    MessageBox.Show(null, "O cadastro não foi efetuado. Verifique os dados.", "Cadastro Agendamento");
                }

                command.Dispose();
            }
            catch (Exception ex1) { MessageBox.Show(null, "Não foi possível cadastrar. Verifique se o CPF está cadastrado.", "Cadastro Agendamento"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                command = new MySqlCommand("UPDATE agendamentos SET " +
                            "nome_completo=@nome_completo, data_nascimento=@data_nascimento, data_consulta=@data_consulta " +
                            "WHERE cpf_id = @cpf_id", conexao);

                command.Parameters.AddWithValue("@cpf_id", txtcpf.Text);
                command.Parameters.AddWithValue("@nome_completo", txtnome.Text);
                command.Parameters.AddWithValue("@data_nascimento", txtnascimento.Text);
                command.Parameters.AddWithValue("@data_consulta", txtconsulta.Text);

                command.Prepare();
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show(null, "Atualização concluída.", "Cadastro Agendamento");
                    limparCampos();
                    populaDataGrid();
                }
                else
                {
                    MessageBox.Show(null, "A alteração não foi efetuada. Verifique os dados.", "Alteração Agendamento");
                }

                command.Dispose();
            }
            catch (Exception ex2)
            {
                MessageBox.Show(null, "A alteração não foi efetuada. Verifique os dados.", "Cadastro Agendamento");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                command = new MySqlCommand("SELECT * FROM agendamentos WHERE cpf_id = @cpf_id", conexao);

                command.Parameters.AddWithValue("@cpf_id", txtcpf.Text);

                command.Prepare();
                rs = command.ExecuteReader();
                if (rs.Read())
                {
                    txtnome.Text = rs.GetString("nome_completo");
                    txtnascimento.Text = rs.GetString("data_nascimento");
                    txtconsulta.Text = rs.GetString("data_consulta");
                }
                else
                {
                    MessageBox.Show(null, "CPF não cadastrado", "Cad Agendamento");
                }
                rs.Close();
                command.Dispose();
            }
            catch (Exception ex4)
            {
                MessageBox.Show(null, "Não foi possível efetuar a consulta.", "Consulta dados do Paciente Agendado");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void limparCampos()
        {
            txtcpf.Text = "";
            txtnome.Text = "";
            txtnascimento.Text = "";
            txtconsulta.Text = "";
        }

        public void populaDataGrid()
        {
            dataGridView1.DataSource = null;
            data_adapter = new MySqlDataAdapter("SELECT * FROM agendamentos", conexao);
            DataSet DS = new DataSet();
            data_adapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0]; //posição 0 dos adaptadores inseridos
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int id = DadosCompartilhados.getId();
            MessageBox.Show("Exemplo de dado compartilhado. Id: " + id, "Exemplo");
        }
    
    }
}
