using Microsoft.AspNetCore.Mvc;
using SuperJU.API.Controllers.Request;
using SuperJU.API.Controllers.Response;
using SuperJU.API.Exceptions;
using SuperJU.API.Service;
using System.Net;

namespace SuperJU.API.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService clienteService;

        public ClienteController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }


        // GET /clientes?id={id}&nome={nome}
        [HttpGet]
        public ActionResult<List<ClienteResponse>> Pesquisar([FromQuery] int? id, [FromQuery] string? nome)
        {
            try
            {
                List<ClienteResponse> clientes = clienteService.Pesquisar(id, nome);
                return Ok(clientes);
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

        // GET /clientes/{id}
        [HttpGet("{id}")]
        public ActionResult<ClienteResponse> BuscarPorId(int id)
        {
            try
            {
                ClienteResponse cliente = clienteService.BuscarPorId(id);
                return Ok(cliente);
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

        // POST /clientes
        [HttpPost]
        public ActionResult<ClienteCadastroResponse> Cadastrar([FromBody] ClienteCadstroEditarRequest cliente)
        {
            try
            {
                ClienteCadastroResponse clienteCadastro = clienteService.Cadastrar(cliente);
                return CreatedAtAction(nameof(BuscarPorId), new { id = clienteCadastro.Id }, clienteCadastro);
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

        // PUT /clientes/{id}
        [HttpPut("{id}")]
        public ActionResult Atualizar(int id, [FromBody] ClienteCadstroEditarRequest cliente)
        {
            try
            {
                clienteService.Atualizar(id, cliente);
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
    }
}
