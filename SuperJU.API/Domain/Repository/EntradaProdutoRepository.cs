using SuperJU.API.Domain.Entity;
using System.Data.SqlClient;

namespace SuperJU.API.Domain.Repository
{
    public class EntradaProdutoRepository : IEntradaProdutoRepository
    {
        private readonly string connectionString;

        public EntradaProdutoRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<EntradaProduto>? Pesquisar(string? numeroNota, DateTime? dataInicio, DateTime? dataFim)
        {
            string sql = @"SELECT Id, NumeroNota, DataEntrada FROM ENTRADAS_PRODUTO WHERE 1=1 ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(numeroNota))
            {
                sql += " AND NumeroNota = @NumeroNota";
                parameters.Add(new SqlParameter("@NumeroNota", numeroNota));
            }

            if (dataInicio != null)
            {
                sql += " AND DataEntrada >= @DataEntradaInicio";
                parameters.Add(new SqlParameter("@DataEntradaInicio", dataInicio?.Date));
            }

            if (dataFim != null)
            {
                sql += " AND DataEntrada <= @DataEntradaFim";
                parameters.Add(new SqlParameter("@DataEntradaFim", dataFim?.Date.AddHours(23).AddMinutes(59).AddSeconds(59)));
            }

            List<EntradaProduto> entradas = new List<EntradaProduto>();
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
                        EntradaProduto produto = new EntradaProduto
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            NumeroNota = dataReader.GetString(dataReader.GetOrdinal("NumeroNota")),
                            DataEntrada = dataReader.GetDateTime(dataReader.GetOrdinal("DataEntrada"))
                        };

                        entradas.Add(produto);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar entrada de produtos", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return entradas;
        }

        public EntradaProduto? BuscaPorId(int id)
        {
            string sql = @"SELECT Id, NumeroNota, DataEntrada FROM ENTRADAS_PRODUTO WHERE Id = @Id";

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
                        EntradaProduto entrada = new EntradaProduto()
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            NumeroNota = dataReader.GetString(dataReader.GetOrdinal("NumeroNota")),
                            DataEntrada = dataReader.GetDateTime(dataReader.GetOrdinal("DataEntrada"))
                        };
                        return entrada;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar entrada de produto", ex);
                }
                finally
                {
                    connection.Close();
                }
                return null;
            }
        }

        public int Inserir(EntradaProduto entradaProduto)
        {
            string sql = @"INSERT INTO ENTRADAS_PRODUTO (NumeroNota, DataEntrada) VALUES (@NumeroNota, @DataEntrada);
                           SELECT CONVERT(int,SCOPE_IDENTITY()) ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@NumeroNota", entradaProduto.NumeroNota);
                    command.Parameters.AddWithValue("@DataEntrada", entradaProduto.DataEntrada);

                    int idEntradaProduto = (int)command.ExecuteScalar();
                    return idEntradaProduto;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao inserir entrada de produto", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}

