using SuperJU.API.Controllers.Response;
using SuperJU.API.Domain.Entity;
using System.Data.SqlClient;

namespace SuperJU.API.Domain.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string connectionString;

        public PedidoRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<Pedido>? Pesquisar(int? pedidoId, int? clienteId, int? formaPagamentoId, DateTime? dataInicio, DateTime? dataFim)
        {
            string sql = @" SELECT p.Id, p.ClienteId, c.Nome as ClienteNome, p.FormaPagamentoId, fp.Nome as FormaPagamentoNome, p.DataPedido, p.ValorTotal
                            FROM PEDIDOS p 
                                 INNER JOIN CLIENTES c ON c.Id = p.ClienteId
                                 INNER JOIN FORMAS_PAGAMENTO fp ON fp.Id = p.FormaPagamentoId
                            WHERE 1=1 ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (pedidoId != null)
            {
                sql += " AND p.Id = @PedidoId";
                parameters.Add(new SqlParameter("@PedidoId", pedidoId));
            }

            if (clienteId != null)
            {
                sql += " AND p.ClienteId = @ClienteId";
                parameters.Add(new SqlParameter("@ClienteId", clienteId));
            }

            if (formaPagamentoId != null)
            {
                sql += " AND p.FormaPagamentoId = @FormaPagamentoId";
                parameters.Add(new SqlParameter("@FormaPagamentoId", formaPagamentoId));
            }

            if (dataInicio != null)
            {
                sql += " AND p.DataPedido >= @DataPedidoInicio";
                parameters.Add(new SqlParameter("@DataPedidoInicio", dataInicio?.Date));
            }

            if (dataFim != null)
            {
                sql += " AND p.DataPedido <= @DataPedidoFim";
                parameters.Add(new SqlParameter("@DataPedidoFim", dataFim?.Date.AddHours(23).AddMinutes(59).AddSeconds(59)));
            }

            List<Pedido> pedidos = new List<Pedido>();
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
                        Pedido pedido = new Pedido
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            ClienteId = dataReader.GetInt32(dataReader.GetOrdinal("ClienteId")),
                            ClienteNome = dataReader.GetString(dataReader.GetOrdinal("ClienteNome")),
                            FormaPagamentoId = dataReader.GetInt32(dataReader.GetOrdinal("FormaPagamentoId")),
                            FormaPagamentoNome = dataReader.GetString(dataReader.GetOrdinal("FormaPagamentoNome")),
                            DataPedido = dataReader.GetDateTime(dataReader.GetOrdinal("DataPedido")),
                            ValorTotal = dataReader.GetDecimal(dataReader.GetOrdinal("ValorTotal"))
                        };

                        pedidos.Add(pedido);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar pedidos", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return pedidos;
        }

        public Pedido? BuscaPorId(int id)
        {
            string sql = @" SELECT p.Id, p.ClienteId, c.Nome as ClienteNome, p.FormaPagamentoId, fp.Nome as FormaPagamentoNome, p.DataPedido, p.ValorTotal
                            FROM PEDIDOS p 
                                 INNER JOIN CLIENTES c ON c.Id = p.ClienteId
                                 INNER JOIN FORMAS_PAGAMENTO fp ON fp.Id = p.FormaPagamentoId
                            WHERE p.Id = @Id ";

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
                        Pedido pedido = new Pedido()
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            ClienteId = dataReader.GetInt32(dataReader.GetOrdinal("ClienteId")),
                            ClienteNome = dataReader.GetString(dataReader.GetOrdinal("ClienteNome")),
                            FormaPagamentoId = dataReader.GetInt32(dataReader.GetOrdinal("FormaPagamentoId")),
                            FormaPagamentoNome = dataReader.GetString(dataReader.GetOrdinal("FormaPagamentoNome")),
                            DataPedido = dataReader.GetDateTime(dataReader.GetOrdinal("DataPedido")),
                            ValorTotal = dataReader.GetDecimal(dataReader.GetOrdinal("ValorTotal"))
                        };

                        return pedido;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar pedido", ex);
                }
                finally
                {
                    connection.Close();
                }
                return null;
            }
        }

        public int Inserir(Pedido pedido)
        {
            string sql = @"INSERT INTO PEDIDOS (ClienteId, FormaPagamentoId, DataPedido, ValorTotal)
                         VALUES (@ClienteId, @FormaPagamentoId, @DataPedido, @ValorTotal);
                         SELECT CONVERT(int,SCOPE_IDENTITY()) ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@ClienteId", pedido.ClienteId);
                    command.Parameters.AddWithValue("@FormaPagamentoId", pedido.FormaPagamentoId);
                    command.Parameters.AddWithValue("@DataPedido", pedido.DataPedido);
                    command.Parameters.AddWithValue("@ValorTotal", pedido.ValorTotal);

                    int idPedido = (int)command.ExecuteScalar();
                    return idPedido;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao inserir pedido", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<RelVendaResponse>? RelVenda(int? pedidoId, int? clienteId, int? formaPagamentoId, DateTime? dataInicio, DateTime? dataFim)
        {
            string sql = @" SELECT 
	                            p.Id as PedidoId, 
   	                            c.Nome as ClienteNome,
   	                            fp.Nome as FormaPagamentoNome,
   	                            p.DataPedido,
   	                            p.ValorTotal,
   	                            (
		                            SELECT  
			                            COALESCE(SUM((pedI.Quantidade * prod.ValorCusto)),0)
		                            FROM 
			                            PEDIDOS_ITEM pedI
			                            INNER JOIN PRODUTOS prod ON pedI.ProdutoId = prod.Id
		                            WHERE
			                            pedI.PedidoId = p.Id
	                            ) as ValorCustoTotal
                            FROM 
	                            PEDIDOS p 
	                            INNER JOIN CLIENTES c ON c.Id = p.ClienteId
	                            INNER JOIN FORMAS_PAGAMENTO fp ON fp.Id = p.FormaPagamentoId
                            WHERE 
	                            1=1 ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (pedidoId != null)
            {
                sql += " AND p.Id = @PedidoId";
                parameters.Add(new SqlParameter("@PedidoId", pedidoId));
            }

            if (clienteId != null)
            {
                sql += " AND p.ClienteId = @ClienteId";
                parameters.Add(new SqlParameter("@ClienteId", clienteId));
            }

            if (formaPagamentoId != null)
            {
                sql += " AND p.FormaPagamentoId = @FormaPagamentoId";
                parameters.Add(new SqlParameter("@FormaPagamentoId", formaPagamentoId));
            }

            if (dataInicio != null)
            {
                sql += " AND p.DataPedido >= @DataPedidoInicio";
                parameters.Add(new SqlParameter("@DataPedidoInicio", dataInicio?.Date));
            }

            if (dataFim != null)
            {
                sql += " AND p.DataPedido <= @DataPedidoFim";
                parameters.Add(new SqlParameter("@DataPedidoFim", dataFim?.Date.AddHours(23).AddMinutes(59).AddSeconds(59)));
            }

            List<RelVendaResponse> rel = new List<RelVendaResponse>();
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
                        RelVendaResponse relVendaResponse = new RelVendaResponse
                        {
                            PedidoId = dataReader.GetInt32(dataReader.GetOrdinal("PedidoId")),
                            ClienteNome = dataReader.GetString(dataReader.GetOrdinal("ClienteNome")),                            
                            FormaPagamentoNome = dataReader.GetString(dataReader.GetOrdinal("FormaPagamentoNome")),
                            DataPedido = dataReader.GetDateTime(dataReader.GetOrdinal("DataPedido")),
                            ValorTotal = dataReader.GetDecimal(dataReader.GetOrdinal("ValorTotal")),
                            ValorCustoTotal = dataReader.GetDecimal(dataReader.GetOrdinal("ValorCustoTotal")),
                            ValorLucro = dataReader.GetDecimal(dataReader.GetOrdinal("ValorTotal")) - dataReader.GetDecimal(dataReader.GetOrdinal("ValorCustoTotal"))
                        };

                        rel.Add(relVendaResponse);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar relatorio de vendas", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return rel;
        }
    }
}