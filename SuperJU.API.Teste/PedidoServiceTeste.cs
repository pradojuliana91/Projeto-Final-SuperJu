using Moq;
using SuperJU.API.Domain.Entity;
using SuperJU.API.Domain.Repository;
using SuperJU.API.Service;
using SuperJU.API.Exceptions;
using SuperJU.API.Controllers.Request;
using SuperJU.API.Controllers.Response;

namespace SuperJU.API.Teste
{
    public class PedidoServiceTeste
    {
        [Fact]
        public void Retorna_Erro_Not_Found_Pedido_Pesquisa()
        {
            //Arrange
            Mock<IPedidoRepository> pedidoRepositoryMock = new Mock<IPedidoRepository>();
            Mock<IPedidoItemRepository> pedidoItemRepositoryMock = new Mock<IPedidoItemRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IFormaPagamentoRepository> formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();

            pedidoRepositoryMock.Setup(repo => repo.Pesquisar(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).Returns(value: null);

            PedidoService pedidoService = new PedidoService(pedidoRepositoryMock.Object, pedidoItemRepositoryMock.Object, produtoRepositoryMock.Object, formaPagamentoRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => pedidoService.Pesquisa(null, null, null, null, null));

            //Assert
            Assert.Equal("Nenhum pedido encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Pedido_Pesquisa()
        {
            //Arrange
            Mock<IPedidoRepository> pedidoRepositoryMock = new Mock<IPedidoRepository>();
            Mock<IPedidoItemRepository> pedidoItemRepositoryMock = new Mock<IPedidoItemRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IFormaPagamentoRepository> formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();

            List<Pedido> pedidos = new List<Pedido>();
            pedidos.Add(new Pedido
            {
                Id = 1,
                ClienteId = 1,
                ClienteNome = "Teste Cliente 1",
                FormaPagamentoId = 1,
                FormaPagamentoNome = "Dinheiro",
                DataPedido = DateTime.Now,
                ValorTotal = 100.40m
            });
            pedidos.Add(new Pedido
            {
                Id = 2,
                ClienteId = 2,
                ClienteNome = "Teste Cliente 2",
                FormaPagamentoId = 2,
                FormaPagamentoNome = "Cartao",
                DataPedido = DateTime.Now,
                ValorTotal = 200
            });

            pedidoRepositoryMock.Setup(repo => repo.Pesquisar(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).Returns(value: pedidos);

            PedidoService pedidoService = new PedidoService(pedidoRepositoryMock.Object, pedidoItemRepositoryMock.Object, produtoRepositoryMock.Object, formaPagamentoRepositoryMock.Object);


            //Act
            var response = pedidoService.Pesquisa(null, null, null, null, null);

            //Assert
            Assert.NotEmpty(response);
            Assert.Equal(2, response.Count());
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Pedido_Busca_Por_Id()
        {
            //Arrange
            Mock<IPedidoRepository> pedidoRepositoryMock = new Mock<IPedidoRepository>();
            Mock<IPedidoItemRepository> pedidoItemRepositoryMock = new Mock<IPedidoItemRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IFormaPagamentoRepository> formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();

            pedidoRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: null);

            PedidoService pedidoService = new PedidoService(pedidoRepositoryMock.Object, pedidoItemRepositoryMock.Object, produtoRepositoryMock.Object, formaPagamentoRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => pedidoService.BuscaPorId(1));

            //Assert
            Assert.Equal("Pedido não encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Pedido_Busca_Por_Id()
        {
            //Arrange
            Mock<IPedidoRepository> pedidoRepositoryMock = new Mock<IPedidoRepository>();
            Mock<IPedidoItemRepository> pedidoItemRepositoryMock = new Mock<IPedidoItemRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IFormaPagamentoRepository> formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();

            Pedido pedido = new Pedido
            {
                Id = 1,
                ClienteId = 1,
                ClienteNome = "Teste Cliente 1",
                FormaPagamentoId = 1,
                FormaPagamentoNome = "Dinheiro",
                DataPedido = DateTime.Now,
                ValorTotal = 100.40m
            };

            pedidoRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: pedido);

            List<PedidoItem> pedidoItems = new List<PedidoItem>();
            pedidoItems.Add(new PedidoItem
            {
                Id = 1,
                PedidoId = 1,
                ProdutoId = 1,
                ProdutoNome = "Produto 1",
                Quantidade = 3,
                Valor = 21.50m,
                ValorTotal = 64.5m
            });
            pedidoItems.Add(new PedidoItem
            {
                Id = 2,
                PedidoId = 1,
                ProdutoId = 2,
                ProdutoNome = "Produto 2",
                Quantidade = 2,
                Valor = 17.95m,
                ValorTotal = 35.90m
            });

            pedidoItemRepositoryMock.Setup(repo => repo.BuscaPorPedidoId(It.IsAny<int>())).Returns(value: pedidoItems);

            PedidoService pedidoService = new PedidoService(pedidoRepositoryMock.Object, pedidoItemRepositoryMock.Object, produtoRepositoryMock.Object, formaPagamentoRepositoryMock.Object);


            //Act
            var response = pedidoService.BuscaPorId(1);

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Items);
            Assert.Equal(2, response.Items.Count);
        }

        [Fact]
        public void Retorna_Erro_Bad_Request_Pedido_Inserir()
        {
            //Arrange
            Mock<IPedidoRepository> pedidoRepositoryMock = new Mock<IPedidoRepository>();
            Mock<IPedidoItemRepository> pedidoItemRepositoryMock = new Mock<IPedidoItemRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IFormaPagamentoRepository> formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();

            PedidoRequest pedido = new PedidoRequest
            {
                ClienteId = null,
                FormaPagamentoId = null,
                ValorTotal = null
            };

            List<PedidoItemRequest> pedidoItems = new List<PedidoItemRequest>();
            pedidoItems.Add(new PedidoItemRequest
            {
                ProdutoId = null,
                Quantidade = null,
                Valor = null,
                ValorTotal = null
            });
            pedidoItems.Add(new PedidoItemRequest
            {
                ProdutoId = 1,
                Quantidade = 1,
                Valor = 9.95m,
                ValorTotal = 35.90m
            });

            pedido.Items = pedidoItems;

            PedidoService pedidoService = new PedidoService(pedidoRepositoryMock.Object, pedidoItemRepositoryMock.Object, produtoRepositoryMock.Object, formaPagamentoRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<BadRequestException>(() => pedidoService.Inserir(pedido));

            //Assert
            Assert.Equal("Dados inválidos.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Pedido_Inserir()
        {
            //Arrange
            Mock<IPedidoRepository> pedidoRepositoryMock = new Mock<IPedidoRepository>();
            Mock<IPedidoItemRepository> pedidoItemRepositoryMock = new Mock<IPedidoItemRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IFormaPagamentoRepository> formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();

            PedidoRequest pedido = new PedidoRequest
            {
                ClienteId = 1,
                FormaPagamentoId = 1,
                ValorTotal = 100.40m
            };

            pedidoRepositoryMock.Setup(repo => repo.Inserir(It.IsAny<Pedido>())).Returns(value: 1);

            List<PedidoItemRequest> pedidoItems = new List<PedidoItemRequest>();
            pedidoItems.Add(new PedidoItemRequest
            {
                ProdutoId = 1,
                Quantidade = 3,
                Valor = 21.50m,
                ValorTotal = 64.5m
            });
            pedidoItems.Add(new PedidoItemRequest
            {
                ProdutoId = 2,
                Quantidade = 2,
                Valor = 17.95m,
                ValorTotal = 35.90m
            });

            pedido.Items = pedidoItems;

            pedidoItemRepositoryMock.Setup(repo => repo.Inserir(It.IsAny<PedidoItem>()));
            produtoRepositoryMock.Setup(repo => repo.SaidaEstoque(It.IsAny<int>(), It.IsAny<int>()));

            PedidoService pedidoService = new PedidoService(pedidoRepositoryMock.Object, pedidoItemRepositoryMock.Object, produtoRepositoryMock.Object, formaPagamentoRepositoryMock.Object);

            //Act
            var response = pedidoService.Inserir(pedido);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Id);
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Pedido_Formas_Pagamento()
        {
            //Arrange
            Mock<IPedidoRepository> pedidoRepositoryMock = new Mock<IPedidoRepository>();
            Mock<IPedidoItemRepository> pedidoItemRepositoryMock = new Mock<IPedidoItemRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IFormaPagamentoRepository> formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();

            formaPagamentoRepositoryMock.Setup(repo => repo.BuscaTodos()).Returns(value: null);

            PedidoService pedidoService = new PedidoService(pedidoRepositoryMock.Object, pedidoItemRepositoryMock.Object, produtoRepositoryMock.Object, formaPagamentoRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => pedidoService.BuscaFormasPagamento());

            //Assert
            Assert.Equal("Nenhum meio de pagamento encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Pedido_Formas_Pagamento()
        {
            //Arrange
            Mock<IPedidoRepository> pedidoRepositoryMock = new Mock<IPedidoRepository>();
            Mock<IPedidoItemRepository> pedidoItemRepositoryMock = new Mock<IPedidoItemRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IFormaPagamentoRepository> formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();

            List<FormaPagamento> formasPagamento = new List<FormaPagamento>();
            formasPagamento.Add(new FormaPagamento
            {
                Id = 1,
                Nome = "Dinheiro",
                Descricao = "Pagamento Com Dinheiro"
            });
            formasPagamento.Add(new FormaPagamento
            {
                Id = 2,
                Nome = "Cartao",
                Descricao = "Pagamento com Cartao Avista ou Debito"
            });
            formasPagamento.Add(new FormaPagamento
            {
                Id = 3,
                Nome = "Pix",
                Descricao = "Pagamento com Pix"
            });

            formaPagamentoRepositoryMock.Setup(repo => repo.BuscaTodos()).Returns(value: formasPagamento);

            PedidoService pedidoService = new PedidoService(pedidoRepositoryMock.Object, pedidoItemRepositoryMock.Object, produtoRepositoryMock.Object, formaPagamentoRepositoryMock.Object);


            //Act
            var response = pedidoService.BuscaFormasPagamento();

            //Assert
            Assert.NotEmpty(response);
            Assert.Equal(3, response.Count);
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Pedido_Rel_Venda()
        {
            //Arrange
            Mock<IPedidoRepository> pedidoRepositoryMock = new Mock<IPedidoRepository>();
            Mock<IPedidoItemRepository> pedidoItemRepositoryMock = new Mock<IPedidoItemRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IFormaPagamentoRepository> formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();

            pedidoRepositoryMock.Setup(repo => repo.RelVenda(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).Returns(value: null);

            PedidoService pedidoService = new PedidoService(pedidoRepositoryMock.Object, pedidoItemRepositoryMock.Object, produtoRepositoryMock.Object, formaPagamentoRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() =>
                pedidoService.RelVenda(null, null, null, null, null)
            );

            //Assert
            Assert.Equal("Não foi encontrado registros para relatorio de venda", myException.Message);
        }

        [Fact]
        public void Retorna_Sucesso_Pedido_Rel_Venda()
        {
            //Arrange
            Mock<IPedidoRepository> pedidoRepositoryMock = new Mock<IPedidoRepository>();
            Mock<IPedidoItemRepository> pedidoItemRepositoryMock = new Mock<IPedidoItemRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<IFormaPagamentoRepository> formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();

            List<RelVendaResponse> relVendas = new List<RelVendaResponse>();
            relVendas.Add(new RelVendaResponse
            {
                PedidoId = 1,
                ClienteNome = "Cliente Teste 1",
                FormaPagamentoNome = "Dinheiro",
                DataPedido = DateTime.Now,
                ValorTotal = 200,
                ValorCustoTotal = 40,
                ValorLucro = 160
            });
            relVendas.Add(new RelVendaResponse
            {
                PedidoId = 2,
                ClienteNome = "Cliente Teste 2",
                FormaPagamentoNome = "Cartão",
                DataPedido = DateTime.Now,
                ValorTotal = 10.99m,
                ValorCustoTotal = 4.99m,
                ValorLucro = 6
            });
            relVendas.Add(new RelVendaResponse
            {
                PedidoId = 1,
                ClienteNome = "Cliente Teste 3",
                FormaPagamentoNome = "Pix",
                DataPedido = DateTime.Now,
                ValorTotal = 1000,
                ValorCustoTotal = 700,
                ValorLucro = 300
            });

            pedidoRepositoryMock.Setup(repo => repo.RelVenda(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).Returns(value: relVendas);

            PedidoService pedidoService = new PedidoService(pedidoRepositoryMock.Object, pedidoItemRepositoryMock.Object, produtoRepositoryMock.Object, formaPagamentoRepositoryMock.Object);


            //Act
            var response = pedidoService.RelVenda(null, null, null, null, null);

            //Assert
            Assert.NotEmpty(response);
            Assert.Equal(3, response.Count);
        }
    }
}