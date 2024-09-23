using SuperJU.API.Domain.Entity;
using System.Data.SqlClient;

namespace SuperJU.API.Domain.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<Cliente> Pesquisar(int? id, string? nome)
        {
            string sql = " SELECT Id, Nome, CPF, DataNascimento, Telefone, Endereco, Complemento, CEP, Bairro, Cidade, Estado FROM CLIENTES WHERE 1=1 ";

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

            List<Cliente> clientes = new List<Cliente>();
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
                        clientes.Add(new Cliente
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            Nome = dataReader.GetString(dataReader.GetOrdinal("Nome")),
                            CPF = dataReader.GetString(dataReader.GetOrdinal("CPF")),
                            DataNascimento = dataReader.GetDateTime(dataReader.GetOrdinal("DataNascimento")),
                            Telefone = dataReader.GetString(dataReader.GetOrdinal("Telefone")),
                            Endereco = dataReader.GetString(dataReader.GetOrdinal("Endereco")),
                            Complemento = dataReader.IsDBNull(dataReader.GetOrdinal("Complemento")) ? null : dataReader.GetString(dataReader.GetOrdinal("Complemento")),
                            CEP = dataReader.GetString(dataReader.GetOrdinal("CEP")),
                            Bairro = dataReader.GetString(dataReader.GetOrdinal("Bairro")),
                            Cidade = dataReader.GetString(dataReader.GetOrdinal("Cidade")),
                            Estado = dataReader.GetString(dataReader.GetOrdinal("Estado"))
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar clientes", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return clientes;
        }

        public Cliente? BuscaPorId(int id)
        {
            string sql = @"SELECT Id, Nome, CPF, DataNascimento, Telefone, Endereco, Complemento, CEP, Bairro, Cidade, Estado FROM CLIENTES WHERE Id = @Id";

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
                        Cliente cliente = new Cliente()
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            Nome = dataReader.GetString(dataReader.GetOrdinal("Nome")),
                            CPF = dataReader.GetString(dataReader.GetOrdinal("CPF")),
                            DataNascimento = dataReader.GetDateTime(dataReader.GetOrdinal("DataNascimento")),
                            Telefone = dataReader.GetString(dataReader.GetOrdinal("Telefone")),
                            Endereco = dataReader.GetString(dataReader.GetOrdinal("Endereco")),
                            Complemento = dataReader.IsDBNull(dataReader.GetOrdinal("Complemento")) ? null : dataReader.GetString(dataReader.GetOrdinal("Complemento")),
                            CEP = dataReader.GetString(dataReader.GetOrdinal("CEP")),
                            Bairro = dataReader.GetString(dataReader.GetOrdinal("Bairro")),
                            Cidade = dataReader.GetString(dataReader.GetOrdinal("Cidade")),
                            Estado = dataReader.GetString(dataReader.GetOrdinal("Estado"))
                        };
                        return cliente;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar cliente", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return null;
        }

        public int Inserir(Cliente cliente)
        {
            string sql = @" INSERT INTO CLIENTES (Nome, CPF, DataNascimento, Telefone, Endereco, Complemento, CEP, Bairro, Cidade, Estado)
                            VALUES (@Nome, @CPF, @DataNascimento, @Telefone, @Endereco, @Complemento, @CEP, @Bairro, @Cidade, @Estado);
                            SELECT CONVERT(int,SCOPE_IDENTITY()) ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@CPF", cliente.CPF);
                    command.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                    command.Parameters.AddWithValue("@Complemento", (cliente.Complemento as object) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CEP", cliente.CEP);
                    command.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                    command.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                    command.Parameters.AddWithValue("@Estado", cliente.Estado);

                    int idCliente = (int)command.ExecuteScalar();
                    return idCliente;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao inserir cliente", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void Editar(int idCliente, Cliente cliente)
        {
            string sql = @"UPDATE CLIENTES SET Nome = @Nome, CPF = @CPF, DataNascimento = @DataNascimento, Telefone = @Telefone, Endereco = @Endereco, Complemento = @Complemento,
                          CEP = @CEP, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@CPF", cliente.CPF);
                    command.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                    command.Parameters.AddWithValue("@Complemento", (cliente.Complemento as object) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CEP", cliente.CEP);
                    command.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                    command.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                    command.Parameters.AddWithValue("@Estado", cliente.Estado);
                    command.Parameters.AddWithValue("@Id", idCliente);

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw new Exception("Erro ao atualizar cliente", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
