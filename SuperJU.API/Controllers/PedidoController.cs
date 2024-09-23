using Microsoft.AspNetCore.Mvc;
using SuperJU.API.Controllers.Request;
using SuperJU.API.Controllers.Response;
using SuperJU.API.Exceptions;
using SuperJU.API.Service;
using System.Net;

namespace SuperJU.API.Controllers
{
    [ApiController]
    [Route("pedidos")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            this.pedidoService = pedidoService;
        }

        // GET /pedidos?pedidoId={pedidoId}&clienteId={clienteId}&clienteId={clienteId}&dataInicio={dataInicio}&dataFim={dataFim}
        [HttpGet]
        public ActionResult<List<PedidoResponse>> Pesquisar([FromQuery] int? pedidoId, [FromQuery] int? clienteId, [FromQuery] int? formaPagamentoId,
            [FromQuery] DateTime? dataInicio, [FromQuery] DateTime? dataFim)
        {
            try
            {
                List<PedidoResponse> pedidos = pedidoService.Pesquisa(pedidoId, clienteId, formaPagamentoId, dataInicio, dataFim);
                return Ok(pedidos);
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

        // GET /pedidos/{id}
        [HttpGet("{id}")]
        public ActionResult<PedidoResponse> BuscarPorId(int id)
        {
            try
            {
                PedidoResponse pedido = pedidoService.BuscaPorId(id);
                return Ok(pedido);
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

        // POST /pedidos
        [HttpPost]
        public ActionResult<PedidoCadastroResponse> Cadastrar([FromBody] PedidoRequest reuqest)
        {
            try
            {
                PedidoCadastroResponse response = pedidoService.Inserir(reuqest);
                return CreatedAtAction(nameof(BuscarPorId), new { id = response.Id }, response);
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

        // GET /pedidos/forma-pagamento
        [HttpGet("forma-pagamento")]
        public ActionResult<List<FormaPagamentoResponse>> BuscaFormasPagamento()
        {
            try
            {
                List<FormaPagamentoResponse> entradas = pedidoService.BuscaFormasPagamento();
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

        // GET /pedidos/relatorio?pedidoId={pedidoId}&clienteId={clienteId}&clienteId={clienteId}&dataInicio={dataInicio}&dataFim={dataFim}
        [HttpGet("relatorio")]
        public ActionResult<List<RelVendaResponse>> RelatorioVenda([FromQuery] int? pedidoId, [FromQuery] int? clienteId, [FromQuery] int? formaPagamentoId,
            [FromQuery] DateTime? dataInicio, [FromQuery] DateTime? dataFim)
        {
            try
            {
                List<RelVendaResponse> relVendas = pedidoService.RelVenda(pedidoId, clienteId, formaPagamentoId, dataInicio, dataFim);
                return Ok(relVendas);
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

