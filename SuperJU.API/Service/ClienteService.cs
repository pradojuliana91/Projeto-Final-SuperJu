using SuperJU.API.Controllers.Request;
using SuperJU.API.Controllers.Response;
using SuperJU.API.Domain.Entity;
using SuperJU.API.Domain.Repository;
using SuperJU.API.Exceptions;

namespace SuperJU.API.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public List<ClienteResponse> Pesquisar(int? id, string? nome)
        {
            List<Cliente>? clientes = clienteRepository.Pesquisar(id, nome);

            if (clientes == null || clientes.Count == 0)
            {
                throw new NotFoundException("Nenhum cliente encontrado.");
            }

            List<ClienteResponse> clientesResponse = new List<ClienteResponse>();
            foreach (var cliente in clientes)
            {
                clientesResponse.Add(new ClienteResponse
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    CPF = cliente.CPF,
                    DataNascimento = cliente.DataNascimento,
                    Telefone = cliente.Telefone,
                    CEP = cliente.CEP,
                    Endereco = cliente.Endereco,
                    Complemento = cliente.Complemento,
                    Bairro = cliente.Bairro,
                    Cidade = cliente.Cidade,
                    Estado = cliente.Estado
                });
            }

            return clientesResponse;
        }

        public ClienteResponse BuscarPorId(int id)
        {
            Cliente? cliente = clienteRepository.BuscaPorId(id);

            if (cliente == null)
            {
                throw new NotFoundException("Nenhum cliente encontrado.");
            }

            return new ClienteResponse
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                DataNascimento = cliente.DataNascimento,
                Telefone = cliente.Telefone,
                CEP = cliente.CEP,
                Endereco = cliente.Endereco,
                Complemento = cliente.Complemento,
                Bairro = cliente.Bairro,
                Cidade = cliente.Cidade,
                Estado = cliente.Estado
            };
        }

        public ClienteCadastroResponse Cadastrar(ClienteCadstroEditarRequest clienteRequest)
        {
            if (clienteRequest == null || string.IsNullOrEmpty(clienteRequest.Nome) || string.IsNullOrEmpty(clienteRequest.CPF) || clienteRequest.CPF.Length != 11 ||
                clienteRequest.DataNascimento == null || clienteRequest.DataNascimento == default || clienteRequest.DataNascimento?.Date > new DateTime(2018, 12, 31).Date ||
                string.IsNullOrEmpty(clienteRequest.Telefone) || clienteRequest.Telefone.Length != 11 || string.IsNullOrEmpty(clienteRequest.CEP) ||
                clienteRequest.CEP.Length != 8 || string.IsNullOrEmpty(clienteRequest.Endereco) || string.IsNullOrEmpty(clienteRequest.Bairro) ||
                string.IsNullOrEmpty(clienteRequest.Cidade) || string.IsNullOrEmpty(clienteRequest.Estado) || clienteRequest.Estado.Length != 2)
            {
                throw new BadRequestException("Dados inválidos!");
            }

            int idCliente = clienteRepository.Inserir(new Cliente
            {
                Nome = clienteRequest.Nome,
                CPF = clienteRequest.CPF,
                DataNascimento = clienteRequest.DataNascimento!.Value,
                Telefone = clienteRequest.Telefone,
                CEP = clienteRequest.CEP,
                Endereco = clienteRequest.Endereco,
                Complemento = clienteRequest.Complemento,
                Bairro = clienteRequest.Bairro,
                Cidade = clienteRequest.Cidade,
                Estado = clienteRequest.Estado
            });
            return new ClienteCadastroResponse
            {
                Id = idCliente
            };
        }

        public void Atualizar(int id, ClienteCadstroEditarRequest clienteRequest)
        {
            if (clienteRequest == null || string.IsNullOrEmpty(clienteRequest.Nome) || string.IsNullOrEmpty(clienteRequest.CPF) || clienteRequest.CPF.Length != 11 ||
                clienteRequest.DataNascimento == null || clienteRequest.DataNascimento == default || clienteRequest.DataNascimento?.Date > new DateTime(2018, 12, 31).Date ||
                string.IsNullOrEmpty(clienteRequest.Telefone) || clienteRequest.Telefone.Length != 11 || string.IsNullOrEmpty(clienteRequest.CEP) ||
                clienteRequest.CEP.Length != 8 || string.IsNullOrEmpty(clienteRequest.Endereco) || string.IsNullOrEmpty(clienteRequest.Bairro) ||
                string.IsNullOrEmpty(clienteRequest.Cidade) || string.IsNullOrEmpty(clienteRequest.Estado) || clienteRequest.Estado.Length != 2)
            {
                throw new BadRequestException("Dados inválidos.");
            }

            Cliente? cliente = clienteRepository.BuscaPorId(id);
            if (cliente == null)
            {
                throw new NotFoundException("Cliente não encontrado!");
            }

            cliente.Nome = clienteRequest.Nome;
            cliente.CPF = clienteRequest.CPF;
            cliente.DataNascimento = clienteRequest.DataNascimento!.Value;
            cliente.Telefone = clienteRequest.Telefone;
            cliente.CEP = clienteRequest.CEP;
            cliente.Endereco = clienteRequest.Endereco;
            cliente.Complemento = clienteRequest.Complemento;
            cliente.Bairro = clienteRequest.Bairro;
            cliente.Cidade = clienteRequest.Cidade;
            cliente.Estado = clienteRequest.Estado;

            clienteRepository.Editar(id, cliente);
        }
    }
}
