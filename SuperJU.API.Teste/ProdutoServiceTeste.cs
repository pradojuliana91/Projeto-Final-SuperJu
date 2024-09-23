using Moq;
using SuperJU.API.Domain.Entity;
using SuperJU.API.Domain.Repository;
using SuperJU.API.Service;
using SuperJU.API.Exceptions;
using SuperJU.API.Controllers.Request;
using SuperJU.API.Controllers.Response;

namespace SuperJU.API.Teste
{
    public class ProdutoServiceTeste
    {
        [Fact]
        public void Retorna_Erro_Not_Found_Produto_Pesquisa()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();
            produtoRepositoryMock.Setup(repo => repo.Pesquisar(It.IsAny<int?>(), It.IsAny<string>())).Returns(value: null);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => produtoService.Pesquisar(null, string.Empty));

            //Assert
            Assert.Equal("Nenhum produto encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Produto_Pesquisa()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();

            List<Produto> produtos = new List<Produto>();
            produtos.Add(new Produto
            {
                Id = 1,
                Nome = "Produto 1",
                Descricao = "Produto Descricao 1",
                Quantidade = 10,
                ValorCusto = 20.09m,
                ValorVenda = 30.99m
            });
            produtos.Add(new Produto
            {
                Id = 2,
                Nome = "Produto 2",
                Descricao = "Produto Descricao 2",
                Quantidade = 99,
                ValorCusto = 0.99m,
                ValorVenda = 2.99m
            });

            produtoRepositoryMock.Setup(repo => repo.Pesquisar(It.IsAny<int?>(), It.IsAny<string>())).Returns(value: produtos);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            //Act
            var response = produtoService.Pesquisar(null, string.Empty);

            //Assert
            Assert.NotEmpty(response);
            Assert.Equal(2, response.Count());
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Produto_Busca_Por_Id()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();
            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: null);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => produtoService.BuscarPorId(1));

            //Assert
            Assert.Equal("Produto não encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Produto_Busca_Por_Id()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();

            Produto produto = new Produto
            {
                Id = 1,
                Nome = "Produto 1",
                Descricao = "Produto Descricao 1",
                Quantidade = 10,
                ValorCusto = 20.09m,
                ValorVenda = 30.99m
            };

            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: produto);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            //Act
            var response = produtoService.BuscarPorId(1);

            //Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void Retorna_Erro_Bad_Request_Produto_Cadastro()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();

            ProdutoCadastroRequest produto = new ProdutoCadastroRequest
            {
                Nome = null,
                Descricao = null,
                ValorVenda = null
            };

            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<BadRequestException>(() => produtoService.Cadastrar(produto));

            //Assert
            Assert.Equal("Dados inválidos.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Produto_Cadastro()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();

            ProdutoCadastroRequest produto = new ProdutoCadastroRequest
            {
                Nome = "Produto 1",
                Descricao = "Descricao Produto 1",
                ValorVenda = 33.99m
            };

            produtoRepositoryMock.Setup(repo => repo.Inserir(It.IsAny<Produto>())).Returns(value: 1);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            //Act
            var response = produtoService.Cadastrar(produto);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Id);
        }

        [Fact]
        public void Retorna_Erro_Bad_Request_Produto_Atualiza()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();

            ProdutoEditarRequest produto = new ProdutoEditarRequest
            {
                Nome = null,
                Descricao = null,
                Quantidade = null,
                ValorCusto = null,
                ValorVenda = null,
            };

            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<BadRequestException>(() => produtoService.Atualizar(1, produto));

            //Assert
            Assert.Equal("Dados inválidos.", myException.Message);
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Produto_Atualiza()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();

            ProdutoEditarRequest produto = new ProdutoEditarRequest
            {
                Nome = "Produto 1",
                Descricao = "Descricao Produto 1",
                Quantidade = 1,
                ValorCusto = 33.99m,
                ValorVenda = 69.50m,
            };
            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: null);

            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => produtoService.Atualizar(1, produto));

            //Assert
            Assert.Equal("Produto não encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Produto_Atualiza()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();

            Produto produto = new Produto
            {
                Nome = "Produto 1",
                Descricao = "Descricao Produto 1",
                Quantidade = 1,
                ValorCusto = 33.99m,
                ValorVenda = 69.50m,
            };

            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: produto);

            ProdutoEditarRequest produtoRequest = new ProdutoEditarRequest
            {
                Nome = "Produto 1 XXXXXXXX",
                Descricao = "Descricao Produto 1 XXXXXXX",
                Quantidade = 2,
                ValorCusto = 23.39m,
                ValorVenda = 109.90m,
            };

            produtoRepositoryMock.Setup(repo => repo.Editar(It.IsAny<int>(), It.IsAny<Produto>()));
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            //Act
            produtoService.Atualizar(1, produtoRequest);

            //Assert
            produtoRepositoryMock.Verify(v => v.BuscaPorId(1), Times.Once());
            produtoRepositoryMock.Verify(v => v.Editar(1, produto), Times.Once());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact]
        public void Retorna_Erro_Not_Found_Entrada_Produto_Pesquisa()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            entradaProdutoRepositoryMock.Setup(repo => repo.Pesquisar(It.IsAny<string?>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).Returns(value: null);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => produtoService.PesquisarEntrada(null, null, null));

            //Assert
            Assert.Equal("Nenhuma entrada de produtos encontrada.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Entrada_Produto_Pesquisa()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            List<EntradaProduto> entradaProdutos = new List<EntradaProduto>();
            entradaProdutos.Add(new EntradaProduto
            {
                Id = 1,
                NumeroNota = "0001",
                DataEntrada = DateTime.Now
            });
            entradaProdutos.Add(new EntradaProduto
            {
                Id = 2,
                NumeroNota = "0002",
                DataEntrada = DateTime.Now
            });

            entradaProdutoRepositoryMock.Setup(repo => repo.Pesquisar(It.IsAny<string?>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).Returns(value: entradaProdutos);

            //Act
            var response = produtoService.PesquisarEntrada(null, null, null);

            //Assert
            Assert.NotEmpty(response);
            Assert.Equal(2, response.Count());
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Entrada_Produto_Busca_Por_Id()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            entradaProdutoRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: null);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => produtoService.BuscarEntradaPorId(1));

            //Assert
            Assert.Equal("Entrada de Produto não encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Entrada_Produto_Busca_Por_Id()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            EntradaProduto entrada = new EntradaProduto
            {
                Id = 1,
                NumeroNota = "0001",
                DataEntrada = DateTime.Now
            };

            entradaProdutoRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: entrada);

            List<EntradaProdutoItem> entradaProdutos = new List<EntradaProdutoItem>();
            entradaProdutos.Add(new EntradaProdutoItem
            {
                Id = 1,
                EntradaProdutoId = 1,
                ProdutoId = 1,
                ProdutoNome = "Produto 1",
                Quantidade = 54,
                ValorCusto = 11.50m
            });
            entradaProdutos.Add(new EntradaProdutoItem
            {
                Id = 2,
                EntradaProdutoId = 1,
                ProdutoId = 2,
                ProdutoNome = "Produto 2",
                Quantidade = 3,
                ValorCusto = 561.19m
            });

            entradaProdutoItemRepositoryMock.Setup(repo => repo.BuscaPorEntradaProdutoId(It.IsAny<int>())).Returns(value: entradaProdutos);

            //Act
            var response = produtoService.BuscarEntradaPorId(1);

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Produtos);
            Assert.Equal(2, response.Produtos.Count);
        }

        [Fact]
        public void Retorna_Erro_Bad_Request_Entrada_Produto_Inserir()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            EntradaProdutoRequest entrada = new EntradaProdutoRequest
            {
                NumeroNota = null
            };

            List<EntradaProdutoItemRequest> entradaItems = new List<EntradaProdutoItemRequest>();
            entradaItems.Add(new EntradaProdutoItemRequest
            {
                ProdutoId = null,
                Quantidade = null,
                ValorCusto = null,
                ValorVenda = null
            });
            entradaItems.Add(new EntradaProdutoItemRequest
            {
                ProdutoId = 1,
                Quantidade = 1,
                ValorCusto = 9.95m,
                ValorVenda = 35.90m
            });

            entrada.Produtos = entradaItems;

            //Act
            var myException = Assert.Throws<BadRequestException>(() => produtoService.CadastrarEntrada(entrada));

            //Assert
            Assert.Equal("Dados inválidos.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Entrada_Produto_Inserir()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            EntradaProdutoRequest entrada = new EntradaProdutoRequest
            {
                NumeroNota = "34"
            };

            entradaProdutoRepositoryMock.Setup(repo => repo.Inserir(It.IsAny<EntradaProduto>())).Returns(value: 1);

            List<EntradaProdutoItemRequest> entradaItems = new List<EntradaProdutoItemRequest>();
            entradaItems.Add(new EntradaProdutoItemRequest
            {
                ProdutoId = 1,
                Quantidade = 2,
                ValorCusto = 30,
                ValorVenda = 60
            });
            entradaItems.Add(new EntradaProdutoItemRequest
            {
                ProdutoId = 2,
                Quantidade = 33,
                ValorCusto = 9.95m,
                ValorVenda = 35.90m
            });

            entrada.Produtos = entradaItems;

            entradaProdutoItemRepositoryMock.Setup(repo => repo.Inserir(It.IsAny<EntradaProdutoItem>()));
            produtoRepositoryMock.Setup(repo => repo.EntradaEstoque(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<decimal>()));

            //Act
            var response = produtoService.CadastrarEntrada(entrada);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Id);
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Produto_Rel_Estoque()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            produtoRepositoryMock.Setup(repo => repo.RelEstoque(It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(value: null);
         
            //Act
            var myException = Assert.Throws<NotFoundException>(() => produtoService.RelEstoque(null, null, null, null));

            //Assert
            Assert.Equal("Não foi encontrado registros para relatorio de estoque", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Produto_Rel_Estoque()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IEntradaProdutoRepository> entradaProdutoRepositoryMock = new Mock<IEntradaProdutoRepository>();
            Mock<IEntradaProdutoItemRepository> entradaProdutoItemRepositoryMock = new Mock<IEntradaProdutoItemRepository>();
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, entradaProdutoRepositoryMock.Object, entradaProdutoItemRepositoryMock.Object);

            List<RelEstoqueResponse> relEstoque = new List<RelEstoqueResponse>();
            relEstoque.Add(new RelEstoqueResponse
            {
                ProdutoId = 1,
                ProdutoNome = "Produto 1",
                ValorCusto = 44.99m,
                ValorVenda = 60.59m,
                Quantidade = 23,
                QtdSaidaU30Dia = 0,
                DataUltimaVenda = null
            });
            relEstoque.Add(new RelEstoqueResponse
            {
                ProdutoId = 2,
                ProdutoNome = "Produto 2",
                ValorCusto = 1,
                ValorVenda = 2.99m,
                Quantidade = 1590,
                QtdSaidaU30Dia = 300,
                DataUltimaVenda = DateTime.Now
            });
            relEstoque.Add(new RelEstoqueResponse
            {
                ProdutoId = 3,
                ProdutoNome = "Produto 3",
                ValorCusto = 23.31m,
                ValorVenda = 122.99m,
                Quantidade = 3,
                QtdSaidaU30Dia = 1,
                DataUltimaVenda = DateTime.Now.AddDays(-20)
            });

            produtoRepositoryMock.Setup(repo => repo.RelEstoque(It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(value: relEstoque);

            //Act
            var response = produtoService.RelEstoque(null, null, null, null);

            //Assert
            Assert.NotEmpty(response);
            Assert.Equal(3, response.Count);
        }
    }
}