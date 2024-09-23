using SuperJU.API.Controllers.Response;
using SuperJU.API.Domain.Entity;
using System.Data.SqlClient;

namespace SuperJU.API.Domain.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string connectionString;

        public ProdutoRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<Produto>? Pesquisar(int? id, string? nome)
        {
            string sql = @"SELECT Id, Nome, Descricao, Quantidade, ValorCusto, ValorVenda FROM PRODUTOS WHERE 1=1 ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (id != null)
            {
                sql += " AND Id = @Id";
                parameters.Add(new SqlParameter("@Id", id));
            }

            if (!string.IsNullOrEmpty(nome))
            {
                sql += " AND Nome LIKE @Nome";
                parameters.Add(new SqlParameter("@nome", "%" + nome + "%"));
            }

            List<Produto> produtos = new List<Produto>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddRange(parameters.ToArray());

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Produto produto = new Produto
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            Nome = dataReader.GetString(dataReader.GetOrdinal("Nome")),                            
                            Descricao = dataReader.GetString(dataReader.GetOrdinal("Descricao")),
                            Quantidade = dataReader.GetInt32(dataReader.GetOrdinal("Quantidade")),
                            ValorCusto = dataReader.GetDecimal(dataReader.GetOrdinal("ValorCusto")),
                            ValorVenda = dataReader.GetDecimal(dataReader.GetOrdinal("ValorVenda"))                            
                        };

                        produtos.Add(produto);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar produtos", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return produtos;
        }

        public Produto? BuscaPorId(int id)
        {
            string sql = @"SELECT Id, Nome, Descricao, Quantidade, ValorCusto, ValorVenda FROM PRODUTOS WHERE Id = @Id";

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@Id", id));

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddRange(parameters.ToArray());

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Produto produto = new Produto()
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            Nome = dataReader.GetString(dataReader.GetOrdinal("Nome")),
                            Descricao = dataReader.GetString(dataReader.GetOrdinal("Descricao")),
                            Quantidade = dataReader.GetInt32(dataReader.GetOrdinal("Quantidade")),
                            ValorCusto = dataReader.GetDecimal(dataReader.GetOrdinal("ValorCusto")),
                            ValorVenda = dataReader.GetDecimal(dataReader.GetOrdinal("ValorVenda"))
                        };
                        return produto;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar produto", ex);
                }
                finally
                {
                    connection.Close();
                }
                return null;
            }
        }

        public int Inserir(Produto produto)
        {
            string sql = @"INSERT INTO PRODUTOS (Nome, Descricao, Quantidade, ValorCusto, ValorVenda)
                         VALUES (@Nome, @Descricao, @Quantidade, @ValorCusto, @ValorVenda);
                         SELECT CONVERT(int,SCOPE_IDENTITY()) ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Nome", produto.Nome);
                    command.Parameters.AddWithValue("@Descricao", produto.Descricao);
                    command.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                    command.Parameters.AddWithValue("@ValorCusto", produto.ValorCusto);
                    command.Parameters.AddWithValue("@ValorVenda", produto.ValorVenda);

                    int idProduto = (int)command.ExecuteScalar();
                    return idProduto;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao inserir produto", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void Editar(int idProduto, Produto produto)
        {
            string sql = @"UPDATE PRODUTOS SET Nome = @Nome, Descricao = @Descricao, Quantidade = @Quantidade, ValorCusto = @ValorCusto, ValorVenda = @ValorVenda WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Nome", produto.Nome);
                    command.Parameters.AddWithValue("@Descricao", produto.Descricao);
                    command.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                    command.Parameters.AddWithValue("@ValorCusto", produto.ValorCusto);
                    command.Parameters.AddWithValue("@ValorVenda", produto.ValorVenda);
                    command.Parameters.AddWithValue("@Id", idProduto);

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw new Exception("Erro ao atualizar produto", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void EntradaEstoque(int idProduto, int quantidade, decimal valorCusto, decimal valorVenda)
        {
            string sql = @"UPDATE PRODUTOS SET Quantidade = Quantidade + @Quantidade, ValorCusto = @ValorCusto, ValorVenda = @ValorVenda WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Quantidade", quantidade);
                    command.Parameters.AddWithValue("@ValorCusto", valorCusto);
                    command.Parameters.AddWithValue("@ValorVenda", valorVenda);
                    command.Parameters.AddWithValue("@Id", idProduto);

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw new Exception("Erro ao dar entrada de estoque no produto", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void SaidaEstoque(int idProduto, int quantidade)
        {
            string sql = @"UPDATE PRODUTOS SET Quantidade = Quantidade - @Quantidade WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Quantidade", quantidade);
                    command.Parameters.AddWithValue("@Id", idProduto);

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw new Exception("Erro ao dar saida de estoque no produto", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<RelEstoqueResponse>? RelEstoque(int? produtoId, string? produtoNome, int? qtdMaiorQue, int? qtdMenorQue)
        {
            string sql = @" SELECT 
	                            p.Id as ProdutoId,
	                            p.Nome as ProdutoNome,
	                            p.ValorCusto,
	                            p.ValorVenda,
	                            p.Quantidade,
	                            (	
		                            SELECT 
			                            COALESCE(SUM(pedI.Quantidade),0)
		                            FROM
			                            PEDIDOS_ITEM pedI
			                            INNER JOIN PEDIDOS ped ON pedI.PedidoId = ped.Id
		                            WHERE 
			                            pedI.ProdutoId = p.id
			                            AND ped.DataPedido >= (GETDATE() - 30)  
	                            ) as QtdSaidaU30Dia,
	                            (	
		                            SELECT 
			                            MAX(ped.DataPedido)
		                            FROM
			                            PEDIDOS_ITEM pedI
			                            INNER JOIN PEDIDOS ped ON pedI.PedidoId = ped.Id
		                            WHERE 
			                            pedI.ProdutoId = p.id
	                            ) as DataUltimaVenda
                            FROM  
	                            PRODUTOS p
                            WHERE
	                            1 = 1 ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (produtoId != null)
            {
                sql += " AND p.Id = @ProdutoId ";
                parameters.Add(new SqlParameter("@ProdutoId", produtoId));
            }

            if (!string.IsNullOrEmpty(produtoNome))
            {
                sql += " AND p.Nome like @ProdutoNome ";
                parameters.Add(new SqlParameter("@ProdutoNome", "%" + produtoNome + "%"));
            }

            if (qtdMaiorQue != null)
            {
                sql += " AND p.Quantidade >= @QtdMaiorQue ";
                parameters.Add(new SqlParameter("@QtdMaiorQue", qtdMaiorQue));
            }

            if (qtdMenorQue != null)
            {
                sql += " AND p.Quantidade <= @QtdMenorQue ";
                parameters.Add(new SqlParameter("@QtdMenorQue", qtdMenorQue));
            }

            sql += " ORDER BY p.Nome ";

            List<RelEstoqueResponse> relatorio = new List<RelEstoqueResponse>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddRange(parameters.ToArray());

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        RelEstoqueResponse relEstoqueResponse = new RelEstoqueResponse
                        {
                            ProdutoId = dataReader.GetInt32(dataReader.GetOrdinal("ProdutoId")),
                            ProdutoNome = dataReader.GetString(dataReader.GetOrdinal("ProdutoNome")),
                            ValorCusto = dataReader.GetDecimal(dataReader.GetOrdinal("ValorCusto")),
                            ValorVenda = dataReader.GetDecimal(dataReader.GetOrdinal("ValorVenda")),
                            Quantidade = dataReader.GetInt32(dataReader.GetOrdinal("Quantidade")),
                            QtdSaidaU30Dia = dataReader.GetInt32(dataReader.GetOrdinal("QtdSaidaU30Dia")),
                            DataUltimaVenda = 
                            (dataReader.IsDBNull(dataReader.GetOrdinal("DataUltimaVenda"))) ? null : dataReader.GetDateTime(dataReader.GetOrdinal("DataUltimaVenda"))
                        };

                        relatorio.Add(relEstoqueResponse);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar relatorio de estqoue", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return relatorio;
        }
    }
}
