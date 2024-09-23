using Microsoft.AspNetCore.Mvc;
using SuperJU.API.Controllers.Request;
using SuperJU.API.Controllers.Response;
using SuperJU.API.Exceptions;
using SuperJU.API.Service;
using System.Net;

namespace SuperJU.API.Controllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            this.produtoService = produtoService;
        }

        // GET /produtos?id={id}&nome={nome}
        [HttpGet]
        public ActionResult<List<ProdutoResponse>> Pesquisar([FromQuery] int? id, [FromQuery] string? nome)
        {
            try
            {
                List<ProdutoResponse> produtos = produtoService.Pesquisar(id, nome);
                return Ok(produtos);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET /produtos/{id}
        [HttpGet("{id}")]
        public ActionResult<ProdutoResponse> BuscarPorId(int id)
        {
            try
            {
                ProdutoResponse produto = produtoService.BuscarPorId(id);
                return Ok(produto);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST /produtos
        [HttpPost]
        public ActionResult<ProdutoCadastroResponse> Cadastrar([FromBody] ProdutoCadastroRequest produto)
        {
            try
            {
                ProdutoCadastroResponse produtoCadastro = produtoService.Cadastrar(produto);
                return CreatedAtAction(nameof(BuscarPorId), new { id = produtoCadastro.Id }, produtoCadastro);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT /produtos/{id}
        [HttpPut("{id}")]
        public ActionResult Atualizar(int id, [FromBody] ProdutoEditarRequest produto)
        {
            try
            {
                produtoService.Atualizar(id, produto);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        // GET /produtos/entrada?numeroNota={id}&dataInicio={dataInicio}&dataFim={dataFim}
        [HttpGet("entrada")]
        public ActionResult<List<EntradaProdutoResponse>> PesquisarEntrada([FromQuery] string? numeroNota, [FromQuery] DateTime? dataInicio, [FromQuery] DateTime? dataFim)
        {
            try
            {
                List<EntradaProdutoResponse> entradas = produtoService.PesquisarEntrada(numeroNota, dataInicio, dataFim);
                return Ok(entradas);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET /produtos/entrada/{id}
        [HttpGet("entrada/{id}")]
        public ActionResult<EntradaProdutoResponse> BuscarEntradaPorId(int id)
        {
            try
            {
                EntradaProdutoResponse entrada = produtoService.BuscarEntradaPorId(id);
                return Ok(entrada);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST /produtos/entrada
        [HttpPost("entrada")]
        public ActionResult<EntradaProdutoCadastroResponse> CadastrarEntrada([FromBody] EntradaProdutoRequest entrada)
        {
            try
            {
                EntradaProdutoCadastroResponse entradaProdutoCadastroResponse = produtoService.CadastrarEntrada(entrada);
                return CreatedAtAction(nameof(BuscarEntradaPorId), new { id = entradaProdutoCadastroResponse.Id }, entradaProdutoCadastroResponse);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET /produtos?produtoId={produtoId}&produtoNome={produtoNome}&qtdMaiorQue={qtdMaiorQue}&qtdMenorQue={qtdMenorQue}
        [HttpGet("relatorio")]
        public ActionResult<List<ProdutoResponse>> RelatorioEstoque([FromQuery] int? produtoId, [FromQuery] string? produtoNome, 
            [FromQuery] int? qtdMaiorQue, [FromQuery] int? qtdMenorQue)
        {
            try
            {
                List<RelEstoqueResponse> relEstoque = produtoService.RelEstoque(produtoId, produtoNome, qtdMaiorQue, qtdMenorQue);
                return Ok(relEstoque);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
