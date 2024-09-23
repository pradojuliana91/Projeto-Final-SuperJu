using SuperJU.API.Domain.Entity;
using System.Data.SqlClient;

namespace SuperJU.API.Domain.Repository
{
    public class PedidoItemRepository : IPedidoItemRepository
    {
        private readonly string connectionString;

        public PedidoItemRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<PedidoItem> BuscaPorPedidoId(int pedidoId)
        {
            string sql = @"SELECT pi.Id, pi.PedidoId, pi.ProdutoId, p.Nome as ProdutoNome, pi.Quantidade, pi.Valor, pi.ValorTotal
                           FROM PEDIDOS_ITEM pi
                                INNER JOIN PRODUTOS p on p.Id = pi.ProdutoId
                           WHERE pi.PedidoId = @PedidoId";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PedidoId", pedidoId));
           
            List<PedidoItem> pedidosItem = new List<PedidoItem>();
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
                        PedidoItem pedidoItem = new PedidoItem
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            PedidoId = dataReader.GetInt32(dataReader.GetOrdinal("PedidoId")),
                            ProdutoId = dataReader.GetInt32(dataReader.GetOrdinal("ProdutoId")),
                            ProdutoNome = dataReader.GetString(dataReader.GetOrdinal("ProdutoNome")),
                            Quantidade = dataReader.GetInt32(dataReader.GetOrdinal("Quantidade")),
                            Valor = dataReader.GetDecimal(dataReader.GetOrdinal("Valor")),
                            ValorTotal = dataReader.GetDecimal(dataReader.GetOrdinal("ValorTotal"))
                        };

                        pedidosItem.Add(pedidoItem);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar items de pedido", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return pedidosItem;
        }

        public void Inserir(PedidoItem pedidoItem)
        {
            string sql = @"INSERT INTO PEDIDOS_ITEM (PedidoId, ProdutoId, Quantidade, Valor, ValorTotal)
                         VALUES (@PedidoId, @ProdutoId, @Quantidade, @Valor, @ValorTotal)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@PedidoId", pedidoItem.PedidoId);
                    command.Parameters.AddWithValue("@ProdutoId", pedidoItem.ProdutoId);
                    command.Parameters.AddWithValue("@Quantidade", pedidoItem.Quantidade);
                    command.Parameters.AddWithValue("@Valor", pedidoItem.Valor);
                    command.Parameters.AddWithValue("@ValorTotal", pedidoItem.ValorTotal);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao inserir item do pedido", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
