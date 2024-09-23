using Moq;
using SuperJU.API.Domain.Entity;
using SuperJU.API.Domain.Repository;
using SuperJU.API.Service;
using SuperJU.API.Exceptions;
using SuperJU.API.Controllers.Request;

namespace SuperJU.API.Teste
{
    public class ClienteServiceTeste
    {
        [Fact]
        public void Retorna_Erro_Not_Found_Cliente_Pesquisa()
        {
            //Arrange
            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteRepositoryMock.Setup(repo => repo.Pesquisar(It.IsAny<int?>(), It.IsAny<string>())).Returns(value: null);
            ClienteService clienteService = new ClienteService(clienteRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => clienteService.Pesquisar(null, string.Empty));

            //Assert
            Assert.Equal("Nenhum cliente encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Cliente_Pesquisa()
        {
            //Arrange
            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            List<Cliente> clientes = new List<Cliente>();
            clientes.Add(new Cliente
            {
                Id = 1,
                Nome = "Teste 1",
                CPF = "11111111111",
                DataNascimento = DateTime.Now,
                Telefone = "34988334833",
                Endereco = "Rua Teste, 33",
                Complemento = null,
                CEP = "44333111",
                Bairro = "Bairro Teste",
                Cidade = "Cidteste",
                Estado = "MG"
            });
            clientes.Add(new Cliente
            {
                Id = 2,
                Nome = "Teste",
                CPF = "22222222222",
                DataNascimento = DateTime.Now,
                Telefone = "41988334833",
                Endereco = "Av B, 11",
                Complemento = "ap 23",
                CEP = "33333111",
                Bairro = "TTT Teste",
                Cidade = "Manquina",
                Estado = "SP"
            });

            clienteRepositoryMock.Setup(repo => repo.Pesquisar(It.IsAny<int?>(), It.IsAny<string>())).Returns(value: clientes);
            ClienteService clienteService = new ClienteService(clienteRepositoryMock.Object);

            //Act
            var response = clienteService.Pesquisar(null, string.Empty);

            //Assert
            Assert.NotEmpty(response);
            Assert.Equal(2, response.Count());
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Cliente_Busca_Por_Id()
        {
            //Arrange
            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: null);
            ClienteService clienteService = new ClienteService(clienteRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => clienteService.BuscarPorId(1));

            //Assert
            Assert.Equal("Nenhum cliente encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Cliente_Busca_Por_Id()
        {
            //Arrange
            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            Cliente cliente = new Cliente
            {
                Id = 1,
                Nome = "Teste 1",
                CPF = "11111111111",
                DataNascimento = DateTime.Now,
                Telefone = "34988334833",
                Endereco = "Rua Teste, 33",
                Complemento = null,
                CEP = "44333111",
                Bairro = "Bairro Teste",
                Cidade = "Cidteste",
                Estado = "MG"
            };
            clienteRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: cliente);
            ClienteService clienteService = new ClienteService(clienteRepositoryMock.Object);

            //Act
            var response = clienteService.BuscarPorId(1);

            //Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void Retorna_Erro_Bad_Request_Cliente_Cadastro()
        {
            //Arrange
            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            ClienteService clienteService = new ClienteService(clienteRepositoryMock.Object);
            ClienteCadstroEditarRequest clienteCadastro = new ClienteCadstroEditarRequest
            {
                Nome = null,
                CPF = "234",
                DataNascimento = DateTime.Now,
                Telefone = "833",
                Endereco = string.Empty,
                CEP = "44333",
                Bairro = string.Empty,
                Cidade = null,
                Estado = "MMMMMMG"
            };
            //Act
            var myException = Assert.Throws<BadRequestException>(() => clienteService.Cadastrar(clienteCadastro));

            //Assert
            Assert.Equal("Dados inválidos!", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Cliente_Cadastro()
        {
            //Arrange
            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteRepositoryMock.Setup(repo => repo.Inserir(It.IsAny<Cliente>())).Returns(value: 1);
            ClienteService clienteService = new ClienteService(clienteRepositoryMock.Object);
            ClienteCadstroEditarRequest clienteCadastro = new ClienteCadstroEditarRequest
            {
                Nome = "Teste 1",
                CPF = "11111111111",
                DataNascimento = DateTime.Now.AddYears(-6),
                Telefone = "34988334833",
                Endereco = "Rua Teste, 33",
                Complemento = null,
                CEP = "44333111",
                Bairro = "Bairro Teste",
                Cidade = "Cidteste",
                Estado = "MG"
            };

            //Act
            var response = clienteService.Cadastrar(clienteCadastro);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Id);
        }

        [Fact]
        public void Retorna_Erro_Bad_Request_Cliente_Atualiza()
        {
            //Arrange
            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            ClienteService clienteService = new ClienteService(clienteRepositoryMock.Object);
            ClienteCadstroEditarRequest clienteCadastro = new ClienteCadstroEditarRequest
            {
                Nome = null,
                CPF = "234",
                DataNascimento = DateTime.Now,
                Telefone = "833",
                Endereco = string.Empty,
                CEP = "44333",
                Bairro = string.Empty,
                Cidade = null,
                Estado = "MMMMMMG"
            };
            //Act
            var myException = Assert.Throws<BadRequestException>(() => clienteService.Atualizar(1, clienteCadastro));

            //Assert
            Assert.Equal("Dados inválidos.", myException.Message);
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Cliente_Atualiza()
        {
            //Arrange
            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: null);
            ClienteService clienteService = new ClienteService(clienteRepositoryMock.Object);
            ClienteCadstroEditarRequest clienteCadastro = new ClienteCadstroEditarRequest
            {
                Nome = "Teste 1",
                CPF = "11111111111",
                DataNascimento = DateTime.Now.AddYears(-6),
                Telefone = "34988334833",
                Endereco = "Rua Teste, 33",
                Complemento = null,
                CEP = "44333111",
                Bairro = "Bairro Teste",
                Cidade = "Cidteste",
                Estado = "MG"
            };
            //Act
            var myException = Assert.Throws<NotFoundException>(() => clienteService.Atualizar(1, clienteCadastro));

            //Assert
            Assert.Equal("Cliente não encontrado!", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Cliente_Atualiza()
        {
            //Arrange
            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            Cliente cliente = new Cliente
            {
                Id = 1,
                Nome = "Teste 1",
                CPF = "11111111111",
                DataNascimento = DateTime.Now.AddYears(-6),
                Telefone = "34988334833",
                Endereco = "Rua Teste, 33",
                Complemento = null,
                CEP = "44333111",
                Bairro = "Bairro Teste",
                Cidade = "Cidteste",
                Estado = "MG"
            };
            clienteRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: cliente);
            ClienteService clienteService = new ClienteService(clienteRepositoryMock.Object);
            ClienteCadstroEditarRequest clienteCadastro = new ClienteCadstroEditarRequest
            {
                Nome = "Teste 1",
                CPF = "11111111111",
                DataNascimento = DateTime.Now.AddYears(-6),
                Telefone = "34988334833",
                Endereco = "Rua Teste, 33",
                Complemento = null,
                CEP = "44333111",
                Bairro = "Bairro Teste",
                Cidade = "Cidteste",
                Estado = "MG"
            };

            //Act
            clienteService.Atualizar(1, clienteCadastro);

            //Assert
            clienteRepositoryMock.Verify(v => v.BuscaPorId(1), Times.Once());
            clienteRepositoryMock.Verify(v => v.Editar(1, cliente), Times.Once());
        }
    }
}