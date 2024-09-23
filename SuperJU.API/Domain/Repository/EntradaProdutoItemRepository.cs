using SuperJU.API.Domain.Entity;
using System.Data.SqlClient;

namespace SuperJU.API.Domain.Repository
{
    public class EntradaProdutoItemRepository : IEntradaProdutoItemRepository
    {
        private readonly string connectionString;

        public EntradaProdutoItemRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<EntradaProdutoItem> BuscaPorEntradaProdutoId(int entradaProdutoId)
        {
            string sql = @"SELECT epi.Id, epi.EntradaProdutoId, epi.ProdutoId, p.Nome as ProdutoNome, epi.Quantidade, epi.ValorCusto 
                           FROM ENTRADAS_PRODUTO_ITEM epi 
                                INNER JOIN PRODUTOS p ON p.ID = epi.ProdutoId
                           WHERE epi.EntradaProdutoId = @EntradaProdutoId";

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@EntradaProdutoId", entradaProdutoId));

            List<EntradaProdutoItem> entradaProdutoItem = new List<EntradaProdutoItem>();
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
                        EntradaProdutoItem entradasProdutoItem = new EntradaProdutoItem
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            EntradaProdutoId = dataReader.GetInt32(dataReader.GetOrdinal("EntradaProdutoId")),
                            ProdutoId = dataReader.GetInt32(dataReader.GetOrdinal("ProdutoId")),
                            ProdutoNome = dataReader.GetString(dataReader.GetOrdinal("ProdutoNome")),
                            Quantidade = dataReader.GetInt32(dataReader.GetOrdinal("Quantidade")),
                            ValorCusto = dataReader.GetDecimal(dataReader.GetOrdinal("ValorCusto"))
                        };

                        entradaProdutoItem.Add(entradasProdutoItem);
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Erro ao buscar items de entrada do produtos", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return entradaProdutoItem;
        }

        public void Inserir(EntradaProdutoItem entradaProdutoItem)
        {
            string sql = @"INSERT INTO ENTRADAS_PRODUTO_ITEM (EntradaProdutoId, ProdutoId, Quantidade, ValorCusto) 
                           VALUES (@EntradaProdutoId, @ProdutoId, @Quantidade, @ValorCusto)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@EntradaProdutoId", entradaProdutoItem.EntradaProdutoId);
                    command.Parameters.AddWithValue("@ProdutoId", entradaProdutoItem.ProdutoId);
                    command.Parameters.AddWithValue("@Quantidade", entradaProdutoItem.Quantidade);
                    command.Parameters.AddWithValue("@ValorCusto", entradaProdutoItem.ValorCusto);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao inserir entrada de produto item", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
