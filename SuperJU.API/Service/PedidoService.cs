using SuperJU.API.Controllers.Request;
using SuperJU.API.Controllers.Response;
using SuperJU.API.Domain.Entity;
using SuperJU.API.Domain.Repository;
using SuperJU.API.Exceptions;

namespace SuperJU.API.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository pedidoRepository;
        private readonly IPedidoItemRepository pedidoItemRepository;
        private readonly IProdutoRepository produtoRepository;
        private readonly IFormaPagamentoRepository formaPagamentoRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IPedidoItemRepository pedidoItemRepository,
            IProdutoRepository produtoRepository, IFormaPagamentoRepository formaPagamentoRepository)
        {
            this.pedidoRepository = pedidoRepository;
            this.pedidoItemRepository = pedidoItemRepository;
            this.produtoRepository = produtoRepository;
            this.formaPagamentoRepository = formaPagamentoRepository;
        }


        public List<PedidoResponse> Pesquisa(int? pedidoId, int? clienteId, int? formaPagamentoId, DateTime? dataInicio, DateTime? dataFim)
        {
            List<Pedido>? pedidos = pedidoRepository.Pesquisar(pedidoId, clienteId, formaPagamentoId, dataInicio, dataFim);

            if (pedidos == null || pedidos.Count == 0)
            {
                throw new NotFoundException("Nenhum pedido encontrado.");
            }

            List<PedidoResponse> pedidosResponse = new List<PedidoResponse>();
            foreach (var pedido in pedidos)
            {
                pedidosResponse.Add(new PedidoResponse
                {
                    Id = pedido.Id,
                    ClienteId = pedido.ClienteId,
                    ClienteNome = pedido.ClienteNome ?? string.Empty,
                    DataPedido = pedido.DataPedido,
                    FormaPagamentoId = pedido.FormaPagamentoId,
                    FormaPagamentoNome = pedido.FormaPagamentoNome ?? string.Empty,
                    ValorTotal = pedido.ValorTotal
                });
            }
            return pedidosResponse;
        }

        public PedidoResponse BuscaPorId(int id)
        {
            Pedido? pedido = pedidoRepository.BuscaPorId(id);

            if (pedido == null)
            {
                throw new NotFoundException("Pedido não encontrado.");
            }

            PedidoResponse pedidoResponse = new PedidoResponse
            {
                Id = pedido.Id,
                ClienteId = pedido.ClienteId,
                ClienteNome = pedido.ClienteNome ?? string.Empty,
                DataPedido = pedido.DataPedido,
                FormaPagamentoId = pedido.FormaPagamentoId,
                FormaPagamentoNome = pedido.FormaPagamentoNome ?? string.Empty,
                ValorTotal = pedido.ValorTotal
            };

            List<PedidoItem> pedidoItems = pedidoItemRepository.BuscaPorPedidoId(id);

            List<PedidoItemResponse> pedidoItemsResponse = new List<PedidoItemResponse>();
            if (pedidoItems != null && pedidoItems.Count > 0)
            {
                foreach (var entradaProdutoItem in pedidoItems)
                {
                    pedidoItemsResponse.Add(new PedidoItemResponse
                    {
                        Id = entradaProdutoItem.Id,
                        ProdutoId = entradaProdutoItem.ProdutoId,
                        ProdutoNome = entradaProdutoItem.ProdutoNome ?? string.Empty,
                        Quantidade = entradaProdutoItem.Quantidade,
                        Valor = entradaProdutoItem.Valor,
                        ValorTotal = entradaProdutoItem.ValorTotal
                    });
                }
            }

            pedidoResponse.Items = pedidoItemsResponse;

            return pedidoResponse;
        }

        public PedidoCadastroResponse Inserir(PedidoRequest pedidoRequest)
        {
            if (pedidoRequest == null || pedidoRequest.ClienteId == null || pedidoRequest.FormaPagamentoId == null || pedidoRequest.ValorTotal == null ||
                pedidoRequest.ValorTotal <= 0 || pedidoRequest.Items == null || pedidoRequest.Items.Count == 0 ||
                pedidoRequest.Items.Any(
                    s => s.ProdutoId == null || s.Quantidade == null || s.Quantidade <= 0 || s.Valor == null || s.Valor <= 0 || 
                    s.ValorTotal == null || s.ValorTotal <= 0 || (s.Quantidade * s.Valor) != s.ValorTotal
                    ) ||
                pedidoRequest.Items.Select(s => s.ValorTotal).Sum() != pedidoRequest.ValorTotal)
            {
                throw new BadRequestException("Dados inválidos.");
            }

            int idPedido = pedidoRepository.Inserir(new Pedido
            {
                ClienteId = pedidoRequest.ClienteId.Value,
                FormaPagamentoId = pedidoRequest.FormaPagamentoId.Value,
                DataPedido = DateTime.Now,
                ValorTotal = pedidoRequest.ValorTotal.Value
            });

            if (pedidoRequest.Items != null && pedidoRequest.Items.Count > 0)
            {
                foreach (var pedidoItemRequest in pedidoRequest.Items)
                {
                    pedidoItemRepository.Inserir(new PedidoItem
                    {
                        PedidoId = idPedido,
                        ProdutoId = pedidoItemRequest.ProdutoId!.Value,
                        Quantidade = pedidoItemRequest.Quantidade!.Value,
                        Valor = pedidoItemRequest.Valor!.Value,
                        ValorTotal = pedidoItemRequest.ValorTotal!.Value
                    });
                    produtoRepository.SaidaEstoque(pedidoItemRequest.ProdutoId.Value, pedidoItemRequest.Quantidade.Value);
                }
            }

            return new PedidoCadastroResponse
            {
                Id = idPedido
            };
        }

        public List<FormaPagamentoResponse> BuscaFormasPagamento()
        {
            List<FormaPagamento>? formasPagamento = formaPagamentoRepository.BuscaTodos();

            if (formasPagamento == null || formasPagamento.Count == 0)
            {
                throw new NotFoundException("Nenhum meio de pagamento encontrado.");
            }

            List<FormaPagamentoResponse> formasPagamentoResponse = new List<FormaPagamentoResponse>();
            foreach (var formaPagamento in formasPagamento)
            {
                formasPagamentoResponse.Add(new FormaPagamentoResponse
                {
                    Id = formaPagamento.Id,
                    Nome = formaPagamento.Nome,
                    Descricao = formaPagamento.Descricao
                });
            }
            return formasPagamentoResponse;
        }

        public List<RelVendaResponse> RelVenda(int? pedidoId, int? clienteId, int? formaPagamentoId, DateTime? dataInicio, DateTime? dataFim)
        {
            List<RelVendaResponse>? relVendas = pedidoRepository.RelVenda(pedidoId, clienteId, formaPagamentoId, dataInicio, dataFim);

            if (relVendas == null || relVendas.Count == 0)
            {
                throw new NotFoundException("Não foi encontrado registros para relatorio de venda");
            }

            return relVendas;
        }
    }
}

