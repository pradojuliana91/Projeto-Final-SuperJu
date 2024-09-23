using SuperJU.API.Controllers.Request;
using SuperJU.API.Controllers.Response;
using SuperJU.API.Domain.Entity;
using SuperJU.API.Domain.Repository;
using SuperJU.API.Exceptions;

namespace SuperJU.API.Service
{
    public interface IProdutoService
    {
        List<ProdutoResponse> Pesquisar(int? id, string? nome);
        ProdutoResponse BuscarPorId(int id);
        ProdutoCadastroResponse Cadastrar(ProdutoCadastroRequest produtoRequest);
        void Atualizar(int id, ProdutoEditarRequest produtoRequest);
        List<EntradaProdutoResponse> PesquisarEntrada(string? numeroNota, DateTime? dataInicio, DateTime? dataFim);
        EntradaProdutoResponse BuscarEntradaPorId(int idEntrada);
        EntradaProdutoCadastroResponse CadastrarEntrada(EntradaProdutoRequest entradaRequest);
        List<RelEstoqueResponse> RelEstoque(int? produtoId, string? produtoNome, int? qtdMaiorQue, int? qtdMenorQue);
    }
}
