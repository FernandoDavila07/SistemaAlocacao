using Domain.Entities; // Para usar a entidade PlanoAssistencia
using Domain.Interfaces; // Para usar o IPlanoAssistenciaRepository
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Rota base: api/PlanosAssistencia
    public class PlanosAssistenciaController : ControllerBase
    {
        private readonly IPlanoAssistenciaRepository _repository;

        public PlanosAssistenciaController(IPlanoAssistenciaRepository repository)
        {
            _repository = repository;
        }

        // GET: api/PlanosAssistencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanoAssistencia>>> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        // GET: api/PlanosAssistencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanoAssistencia>> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound("Plano de assistência não encontrado.");
            }
            return Ok(item);
        }

        // POST: api/PlanosAssistencia
        [HttpPost]
        public async Task<ActionResult<PlanoAssistencia>> Create(PlanoAssistencia plano)
        {
            await _repository.AddAsync(plano);

            return CreatedAtAction(nameof(GetById), new { id = plano.Id }, plano);
        }

        // PUT: api/PlanosAssistencia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PlanoAssistencia plano)
        {
            if (id != plano.Id)
            {
                return BadRequest("ID da rota não corresponde ao ID do objeto.");
            }

            var existingItem = await _repository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return NotFound("Plano de assistência não encontrado.");
            }

            await _repository.UpdateAsync(plano);

            return NoContent(); // 204
        }

        // DELETE: api/PlanosAssistencia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound("Plano de assistência não encontrado.");
            }

            await _repository.DeleteAsync(item);

            return NoContent(); // 204
        }
    }
}