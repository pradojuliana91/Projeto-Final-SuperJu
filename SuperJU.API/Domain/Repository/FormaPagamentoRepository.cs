using SuperJU.API.Domain.Entity;
using System.Data.SqlClient;

namespace SuperJU.API.Domain.Repository
{
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private readonly string connectionString;

        public FormaPagamentoRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<FormaPagamento>? BuscaTodos()
        {
            string sql = @"SELECT Id, Nome, Descricao FROM FORMAS_PAGAMENTO";           

            List<FormaPagamento> formasPagamento = new List<FormaPagamento>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        FormaPagamento formaPagamento = new FormaPagamento
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            Nome = dataReader.GetString(dataReader.GetOrdinal("Nome")),
                            Descricao = dataReader.GetString(dataReader.GetOrdinal("Descricao")),                            
                        };

                        formasPagamento.Add(formaPagamento);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao busca formas de pagamento", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return formasPagamento;
        }
    }
}
