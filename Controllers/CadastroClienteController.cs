using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TopMobil_API.Data;
using TopMobil_API.Models;

namespace TopMobil_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroClienteController : ControllerBase
    {
        private TopMobilContext _context;

        public CadastroClienteController(TopMobilContext context)
        {
            //construtor
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<CadastroCliente>> GetAll() 
        {
            return _context.CadastroCliente.ToList();
        } 

        [HttpGet("{ClienteId}")]
        public ActionResult<List<CadastroCliente>> Get(int ClienteId)
        {
            try
            {
                var result = _context.CadastroCliente.Find(ClienteId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> post(CadastroCliente model)
        {
            try
            {
                _context.CadastroCliente.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    //return Ok();
                    return Created($"/api/cadastrocliente/{model.email}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();
        }

        [HttpDelete("{ClienteId}")]
        public async Task<ActionResult> delete(int ClienteId)
        {
            try
            {
                //verifica se existe cliente a ser excluído
                var cliente = await _context.CadastroCliente.FindAsync(ClienteId);

                if(cliente == null)
                {
                    return NotFound();
                }

                _context.Remove(cliente);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha no acesso ao banco de dados.");
            }
        }

        [HttpPut("{ClienteId}")]
        public async Task<IActionResult> put(int ClienteId, CadastroCliente dadosClienteAlt)
        {
            try
            {
                //verifica se existe cliente a ser alterado
                var result = await _context.CadastroCliente.FindAsync(ClienteId);
                if(ClienteId != result.id)
                {
                    return BadRequest();
                }

                result.nome = dadosClienteAlt.nome;
                result.telefone = dadosClienteAlt.telefone;
                result.email = dadosClienteAlt.email;
                result.senha = dadosClienteAlt.senha;

                await _context.SaveChangesAsync();
                return Created($"/api/cadastrocliente/{dadosClienteAlt.email}", dadosClienteAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha no acesso ao banco de dados.");
            }
        }
    }
}