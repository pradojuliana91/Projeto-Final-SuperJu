using SuperJU.API.Controllers.Response;
using SuperJU.API.Domain.Entity;

namespace SuperJU.API.Domain.Repository
{
    public interface IProdutoRepository
    {
        List<Produto>? Pesquisar(int? id, string? nome);
        Produto? BuscaPorId(int id);
        int Inserir(Produto produto);
        void Editar(int idProduto, Produto produto);
        void EntradaEstoque(int idProduto, int quantidade, decimal valorCusto, decimal valorVenda);
        void SaidaEstoque(int idProduto, int quantidade);
        List<RelEstoqueResponse>? RelEstoque(int? produtoId, string? produtoNome, int? qtdMaiorQue, int? qtdMenorQue);
    }
}
