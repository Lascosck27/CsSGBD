using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CsSGBD
{
    class Empregado
    {
        private const string stringConexao = "SERVER=localhost; PORT=3306; DATABASE=cssgbd_db; UID=root; PWD=L4sc0sck!";

        public int Matricula {  get; set; }
        public string Cpf { get; set;}
        public string Nome { get; set;}
        public string Endereco { get; set;}

        public DataTable Pesquisar()
        {
            try
            {
                StringBuilder stringSql = new StringBuilder();  //cria uma instância da classe StringBuilder, chamada stringSql
                stringSql.Append("select matricula, cpf, nome, endereco "); //seleciona os as colunas matricula, cpf, nome e endereco
                stringSql.Append("from empregado "); //da tabela empregado
                stringSql.Append("where 1");    //sempre verdadeiro

                if (Matricula != 0)     //se Matricula for diferente de zero: adiciona a clausula AND, selecionando somente registros com a matricula especificada
                    stringSql.Append(" and matricula=@matricula");
                if (!Cpf.Equals(""))    //se Cpf não estiver vazio: seleciona apenas registros com o CPF especificado
                    stringSql.Append(" and cpf=@cpf");
                if (!Nome.Equals("")) 
                    stringSql.Append(" and nome like @nome"); /* o operador LIKE permite correspondências parciais nos nomes dos registros,
                                                               * o que permite que registros com nomes que contenham a sequência de caracteres 
                                                               * especificada serão selecionados. Ex: se o nome for 'João', a parte resultante será:
                                                               * "AND nome like 'João'", onde % é um caractere curinga que corresponde à qualquer sequência
                                                               * de caracteres antes ou depois do nome especificado.*/

                MySqlConnection conexao = new MySqlConnection(stringConexao); /* a classe MySqlConnection cria uma conexão com o DB. O construtor recebe como parâmetro uma sring de 
                                                                               * conexão, que neste caso é a stringConexao, que contém as informações necessárias para fazer a conexão*/

                MySqlCommand comando = new MySqlCommand(stringSql.ToString(), conexao); /* a classe MySqlCommand cria um novo comando SQL. O construtor recebeu dois parâmtros:
                                                                                         * a consulta SQL em forma de string (através do ToString do StringBuilder stringSql)
                                                                                         * e a conexão com o banco de dados (conexao)*/

                comando.Parameters.AddWithValue("@matricula", Matricula);   //adiciona um parâmetro chamado @matricula ao comando, com o valor de Matricula
                comando.Parameters.AddWithValue("@cpf", Cpf);   //adiciona um parâmetro chamado @cpf ao comando, com o valor de Cpf
                comando.Parameters.AddWithValue("@nome", "%"+Nome+"%"); //adiciona um parâmetro chamado @nome ao comando, com o valor de Nome, mas modificado com o %%.

                MySqlDataAdapter adaptador = new MySqlDataAdapter(); //cria um adaptador de dados usando a classe MySqlDataAdapter
                adaptador.SelectCommand = comando; //define como comando o 'comando' (criado anteriormente)

                DataTable dados = new DataTable(); //cria um objeto DataTable para armazenar os resultados da consulta
                adaptador.Fill(dados); //executa a consulta usando o adaptador e preenche o DataTable com os resultados
                return dados; //como resultado da função, o DT contendo os dados é retornado

            }

            catch (Exception e) //captura uma exceção, se houver, e lança ao usuário.
            {
                throw e;
            }
        }

        public void Incluir()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(stringConexao);   /* a classe MySqlConnection cria uma conexão com o DB, utilizando stringConexao como
                                                                                 * a string de conexão especificada*/
                MySqlCommand comando = new MySqlCommand();  //cria um novo comando SQL
                comando.Connection = conexao;   //define a conexão que será usada pelo comando
                comando.CommandText = "insert into empregado (cpf, nome, endereco) " + 
                                                    "values (@cpf, @nome, @endereco)"; /* Define a instrução SQL de inserção, especificando as colunas e
                                                                                        * os parâmetros que serão inseridos na tabela "empregado"*/
                comando.Parameters.AddWithValue("@cpf", Cpf); // atribui o valor da variável Cpf ao parâmetro @cpf no comando SQL
                comando.Parameters.AddWithValue("@nome", Nome); // atribui o valor da variável Nome ao parâmetro @nome 
                comando.Parameters.AddWithValue("@endereco", Endereco); // atribui o valor da variável Endereco ao parâmetro @endereco

                conexao.Open(); // abre a conexão com o banco de dados
                comando.Prepare();  // prepara o comando SQL para execução. Importante para otimizar o desempenho e evitar ataques de injeção de SQL
                comando.ExecuteNonQuery(); // executa o comando para inserir os dados na tabela. Como se trata de um comando de inserção, não são esperados resultados retornados
                conexao.Close(); // fecha a conexão com o DB
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void Alterar()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(stringConexao);
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "update empregado SET " +
                                              "cpf=@cpf, " +
                                              "nome=@nome, " +
                                              "endereco=@endereco, " +
                                              "where matricula=@matricula";

                comando.Parameters.AddWithValue("@matricula", Matricula);
                comando.Parameters.AddWithValue("@cpf", Cpf);
                comando.Parameters.AddWithValue("@nome", Nome);
                comando.Parameters.AddWithValue("@endereco", Endereco);

                conexao.Open();
                comando.Prepare();
                comando.ExecuteNonQuery();
                conexao.Close();

            }
            catch (Exception e) 
            { 
                throw e;
            }
        }

        public void Excluir()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(stringConexao);
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "delete from empregado " +
                                       "where matricula=@matricula";

                comando.Parameters.AddWithValue("@matricula", Matricula);

                conexao.Open();
                comando.Prepare();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
