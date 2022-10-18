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
    public class VeiculoController : ControllerBase
    {
        private TopMobilContext _context;

        public VeiculoController(TopMobilContext context)
        {
            //construtor
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Veiculo>> GetAll() 
        {
            return _context.Veiculo.ToList();
        }
        
        [HttpGet("{VeiculoId}")]
        public ActionResult<List<Veiculo>> Get(int VeiculoId)
        {
            try
            {
                var result = _context.Veiculo.Find(VeiculoId);
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
        public async Task<ActionResult> post(Veiculo model)
        {
            try
            {
                _context.Veiculo.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    //return Ok();
                    return Created($"/api/veiculo/{model.placa}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();
        }

        [HttpDelete("{VeiculoId}")]
        public async Task<ActionResult> delete(int VeiculoId)
        {
            try
            {
                //verifica se existe veiculo a ser excluído
                var veiculo = await _context.Veiculo.FindAsync(VeiculoId);

                if(veiculo == null)
                {
                    return NotFound();
                }

                _context.Remove(veiculo);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha no acesso ao banco de dados.");
            }
        }

        [HttpPut("{VeiculoId}")]
        public async Task<IActionResult> put(int VeiculoId, Veiculo dadosVeiculoAlt)
        {
            try
            {
                //verifica se existe veiculo a ser alterado
                var result = await _context.Veiculo.FindAsync(VeiculoId);
                if(VeiculoId != result.id)
                {
                    return BadRequest();
                }

                result.placa = dadosVeiculoAlt.placa;
                result.modelo = dadosVeiculoAlt.modelo;
                result.ano = dadosVeiculoAlt.ano;
                result.cor = dadosVeiculoAlt.cor;

                await _context.SaveChangesAsync();
                return Created($"/api/veiculo/{dadosVeiculoAlt.placa}", dadosVeiculoAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha no acesso ao banco de dados.");
            }
        }
    }
}