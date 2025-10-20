using Domain.Entities; // Para usar a entidade VeiculoAssistencia
using Domain.Interfaces; // Para usar o IVeiculoAssistenciaRepository
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Rota base: api/VeiculosAssistencia
    public class VeiculosAssistenciaController : ControllerBase
    {
        private readonly IVeiculoAssistenciaRepository _repository;

        public VeiculosAssistenciaController(IVeiculoAssistenciaRepository repository)
        {
            _repository = repository;
        }

        // GET: api/VeiculosAssistencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoAssistencia>>> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        // GET: api/VeiculosAssistencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VeiculoAssistencia>> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound("Associação não encontrada.");
            }
            return Ok(item);
        }

        // POST: api/VeiculosAssistencia (Associar veículo a um plano)
        [HttpPost]
        public async Task<ActionResult<VeiculoAssistencia>> Create(VeiculoAssistencia associacao)
        {
            await _repository.AddAsync(associacao);

            return CreatedAtAction(nameof(GetById), new { id = associacao.Id }, associacao);
        }

        // PUT: api/VeiculosAssistencia/5 (Atualizar - raramente usado, mas bom ter)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VeiculoAssistencia associacao)
        {
            if (id != associacao.Id)
            {
                return BadRequest("ID da rota não corresponde ao ID do objeto.");
            }

            var existingItem = await _repository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return NotFound("Associação não encontrada.");
            }

            await _repository.UpdateAsync(associacao);

            return NoContent(); // 204
        }

        // DELETE: api/VeiculosAssistencia/5 (Remover associação)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound("Associação não encontrada.");
            }

            await _repository.DeleteAsync(item);

            return NoContent(); // 204
        }
    }
}