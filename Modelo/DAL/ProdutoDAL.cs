﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelo;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ProdutoDAL
    {
        public void incluir(Produto produto)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Dados.stringDeConexao;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "dbo.prInserirProduto @nome, @preco, @estoque; select @@IDENTITY";
                cmd.Parameters.AddWithValue("@nome", produto.nome);
                cmd.Parameters.AddWithValue("@preco", produto.preco);
                cmd.Parameters.AddWithValue("@estoque", produto.estoque);
                cn.Open();
                produto.codigo = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw new Exception("Servidor SQL Erro:" + ex.Number + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        public void alterar(Produto produto)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Dados.stringDeConexao;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "dbo.prAlterarProduto @codigo, @nome, @preco, @estoque";
                cmd.Parameters.AddWithValue("@codigo", produto.codigo);
                cmd.Parameters.AddWithValue("@nome", produto.nome);
                cmd.Parameters.AddWithValue("@preco", produto.preco);
                cmd.Parameters.AddWithValue("@estoque", produto.estoque);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Servidor SQL Erro:" + ex.Number + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        public void excluir(int codigo)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Dados.stringDeConexao;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "dbo.prExcluirProduto @codigo";
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new Exception("Não foi possível excluir o produto " + codigo);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Servidor SQL Erro:" + ex.Number + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        public DataTable listagem()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.prSelectAllProdutos", Dados.stringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
    }
}
