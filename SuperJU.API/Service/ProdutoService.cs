using SuperJU.API.Controllers.Request;
using SuperJU.API.Controllers.Response;
using SuperJU.API.Domain.Entity;
using SuperJU.API.Domain.Repository;
using SuperJU.API.Exceptions;

namespace SuperJU.API.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IEntradaProdutoRepository entradaProdutoRepository;
        private readonly IEntradaProdutoItemRepository entradaProdutoItemRepository;

        public ProdutoService(IProdutoRepository produtoRepository, IEntradaProdutoRepository entradaProdutoRepository, IEntradaProdutoItemRepository entradaProdutoItemRepository)
        {
            this.produtoRepository = produtoRepository;
            this.entradaProdutoRepository = entradaProdutoRepository;
            this.entradaProdutoItemRepository = entradaProdutoItemRepository;
        }

        public List<ProdutoResponse> Pesquisar(int? id, string? nome)
        {
            List<Produto>? produtos = produtoRepository.Pesquisar(id, nome);

            if (produtos == null || produtos.Count == 0)
            {
                throw new NotFoundException("Nenhum produto encontrado.");
            }

            List<ProdutoResponse> produtosResponse = new List<ProdutoResponse>();
            foreach (var produto in produtos)
            {
                produtosResponse.Add(new ProdutoResponse
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Quantidade = produto.Quantidade,
                    ValorCusto = produto.ValorCusto,
                    ValorVenda = produto.ValorVenda
                });
            }
            return produtosResponse;
        }

        public ProdutoResponse BuscarPorId(int id)
        {
            Produto? produto = produtoRepository.BuscaPorId(id);
            if (produto == null)
            {
                throw new NotFoundException("Produto não encontrado.");
            }

            return new ProdutoResponse
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Quantidade = produto.Quantidade,
                ValorCusto = produto.ValorCusto,
                ValorVenda = produto.ValorVenda
            };
        }

        public ProdutoCadastroResponse Cadastrar(ProdutoCadastroRequest produtoRequest)
        {
            if (produtoRequest == null || string.IsNullOrEmpty(produtoRequest.Nome) || string.IsNullOrEmpty(produtoRequest.Descricao) ||
                produtoRequest.ValorVenda == null || produtoRequest.ValorVenda <= 0)
            {
                throw new BadRequestException("Dados inválidos.");
            }

            int idProduto = produtoRepository.Inserir(new Produto
            {
                Nome = produtoRequest.Nome,
                Descricao = produtoRequest.Descricao,
                Quantidade = 0,
                ValorCusto = 0,
                ValorVenda = produtoRequest.ValorVenda.Value
            });
            return new ProdutoCadastroResponse
            {
                Id = idProduto
            };
        }

        public void Atualizar(int id, ProdutoEditarRequest produtoRequest)
        {

            if (produtoRequest == null || string.IsNullOrEmpty(produtoRequest.Nome) || string.IsNullOrEmpty(produtoRequest.Descricao) ||
                produtoRequest.Quantidade == null || !(produtoRequest.Quantidade >= 0) ||
                produtoRequest.ValorCusto == null || !(produtoRequest.ValorCusto >= 0) ||
                produtoRequest.ValorVenda == null || !(produtoRequest.ValorVenda > 0) ||
                produtoRequest.ValorCusto >= produtoRequest.ValorVenda)
            {
                throw new BadRequestException("Dados inválidos.");
            }

            Produto? produto = produtoRepository.BuscaPorId(id);
            if (produto == null)
            {
                throw new NotFoundException("Produto não encontrado.");
            }

            produto.Nome = produtoRequest.Nome;
            produto.Descricao = produtoRequest.Descricao;
            produto.Quantidade = produtoRequest.Quantidade.Value;
            produto.ValorCusto = produtoRequest.ValorCusto.Value;
            produto.ValorVenda = produtoRequest.ValorVenda.Value;

            produtoRepository.Editar(id, produto);
        }

        public List<EntradaProdutoResponse> PesquisarEntrada(string? numeroNota, DateTime? dataInicio, DateTime? dataFim)
        {
            List<EntradaProduto>? entradaProdutos = entradaProdutoRepository.Pesquisar(numeroNota, dataInicio, dataFim);

            if (entradaProdutos == null || entradaProdutos.Count == 0)
            {
                throw new NotFoundException("Nenhuma entrada de produtos encontrada.");
            }

            List<EntradaProdutoResponse> produtosResponse = new List<EntradaProdutoResponse>();
            foreach (var entrada in entradaProdutos)
            {
                produtosResponse.Add(new EntradaProdutoResponse
                {
                    Id = entrada.Id,
                    NumeroNota = entrada.NumeroNota,
                    DataEntrada = entrada.DataEntrada
                });
            }
            return produtosResponse;
        }

        public EntradaProdutoResponse BuscarEntradaPorId(int idEntrada)
        {
            EntradaProduto? entrada = entradaProdutoRepository.BuscaPorId(idEntrada);

            if (entrada == null)
            {
                throw new NotFoundException("Entrada de Produto não encontrado.");
            }

            EntradaProdutoResponse entradaProduto = new EntradaProdutoResponse
            {
                Id = entrada.Id,
                NumeroNota = entrada.NumeroNota,
                DataEntrada = entrada.DataEntrada,
            };

            List<EntradaProdutoItem> entradaProdutoItems = entradaProdutoItemRepository.BuscaPorEntradaProdutoId(entrada.Id);

            List<EntradaProdutoItemResponse> entradaProdutoItemsResponse = new List<EntradaProdutoItemResponse>();
            if (entradaProdutoItems != null && entradaProdutoItems.Count > 0)
            {

                foreach (var entradaProdutoItem in entradaProdutoItems)
                {
                    entradaProdutoItemsResponse.Add(new EntradaProdutoItemResponse
                    {
                        Id = entradaProdutoItem.Id,
                        ProdutoId = entradaProdutoItem.ProdutoId,
                        ProdutoNome = entradaProdutoItem.ProdutoNome ?? string.Empty,
                        Quantidade = entradaProdutoItem.Quantidade,
                        ValorCusto = entradaProdutoItem.ValorCusto
                    });
                }
            }

            entradaProduto.Produtos = entradaProdutoItemsResponse;

            return entradaProduto;
        }

        public EntradaProdutoCadastroResponse CadastrarEntrada(EntradaProdutoRequest entradaRequest)
        {
            if (entradaRequest == null || string.IsNullOrEmpty(entradaRequest.NumeroNota) || entradaRequest.Produtos == null || entradaRequest.Produtos.Count == 0 ||
                entradaRequest.Produtos.Any(
                    s => s.ProdutoId == null || s.Quantidade == null || s.Quantidade <= 0 ||
                    s.ValorCusto == null || s.ValorCusto < 0 ||
                    s.ValorVenda == null || s.ValorVenda <= 0 ||
                    s.ValorCusto >= s.ValorVenda)
                )
            {
                throw new BadRequestException("Dados inválidos.");
            }

            int idEntradaProduto = entradaProdutoRepository.Inserir(new EntradaProduto
            {
                NumeroNota = entradaRequest.NumeroNota,
                DataEntrada = DateTime.Now
            });

            foreach (var entradaProdutoItem in entradaRequest.Produtos)
            {
                entradaProdutoItemRepository.Inserir(new EntradaProdutoItem
                {
                    EntradaProdutoId = idEntradaProduto,
                    ProdutoId = entradaProdutoItem.ProdutoId!.Value,
                    Quantidade = entradaProdutoItem.Quantidade!.Value,
                    ValorCusto = entradaProdutoItem.ValorCusto!.Value
                });

                produtoRepository.EntradaEstoque(entradaProdutoItem.ProdutoId.Value, entradaProdutoItem.Quantidade.Value,
                    entradaProdutoItem.ValorCusto.Value, entradaProdutoItem.ValorVenda!.Value);
            }

            return new EntradaProdutoCadastroResponse
            {
                Id = idEntradaProduto
            };
        }

        public List<RelEstoqueResponse> RelEstoque(int? produtoId, string? produtoNome, int? qtdMaiorQue, int? qtdMenorQue)
        {
            List<RelEstoqueResponse>? relEstoque = produtoRepository.RelEstoque(produtoId, produtoNome, qtdMaiorQue, qtdMenorQue);
            if (relEstoque == null || relEstoque.Count == 0)
            {
                throw new NotFoundException("Não foi encontrado registros para relatorio de estoque");
            }
            return relEstoque;
        }
    }
}
