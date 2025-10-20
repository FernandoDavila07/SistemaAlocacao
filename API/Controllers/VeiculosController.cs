using Domain.Entities; // Para usar a entidade Veiculo
using Domain.Interfaces; // Para usar o IVeiculoRepository
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Rota base: api/Veiculos
    public class VeiculosController : ControllerBase
    {
        private readonly IVeiculoRepository _repository;

        public VeiculosController(IVeiculoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Veiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetAll()
        {
            var veiculos = await _repository.GetAllAsync();
            return Ok(veiculos);
        }

        // GET: api/Veiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetById(int id)
        {
            var veiculo = await _repository.GetByIdAsync(id);
            if (veiculo == null)
            {
                return NotFound("Veículo não encontrado.");
            }
            return Ok(veiculo);
        }

        // POST: api/Veiculos
        [HttpPost]
        public async Task<ActionResult<Veiculo>> Create(Veiculo veiculo)
        {
            await _repository.AddAsync(veiculo);

            return CreatedAtAction(nameof(GetById), new { id = veiculo.Id }, veiculo);
        }

        // PUT: api/Veiculos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Veiculo veiculo)
        {
            if (id != veiculo.Id)
            {
                return BadRequest("ID da rota não corresponde ao ID do objeto.");
            }

            var existingVeiculo = await _repository.GetByIdAsync(id);
            if (existingVeiculo == null)
            {
                return NotFound("Veículo não encontrado.");
            }

            await _repository.UpdateAsync(veiculo);

            return NoContent(); // 204
        }

        // DELETE: api/Veiculos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var veiculo = await _repository.GetByIdAsync(id);
            if (veiculo == null)
            {
                return NotFound("Veículo não encontrado.");
            }

            await _repository.DeleteAsync(veiculo);

            return NoContent(); // 204
        }
    }
}