using Domain.Entities; // Para usar a entidade EmpresaAssistencia
using Domain.Interfaces; // Para usar o IEmpresaAssistenciaRepository
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Rota base: api/EmpresasAssistencia
    public class EmpresasAssistenciaController : ControllerBase
    {
        private readonly IEmpresaAssistenciaRepository _repository;

        public EmpresasAssistenciaController(IEmpresaAssistenciaRepository repository)
        {
            _repository = repository;
        }

        // GET: api/EmpresasAssistencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaAssistencia>>> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        // GET: api/EmpresasAssistencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaAssistencia>> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound("Empresa de assistência não encontrada.");
            }
            return Ok(item);
        }

        // POST: api/EmpresasAssistencia
        [HttpPost]
        public async Task<ActionResult<EmpresaAssistencia>> Create(EmpresaAssistencia empresa)
        {
            await _repository.AddAsync(empresa);

            return CreatedAtAction(nameof(GetById), new { id = empresa.Id }, empresa);
        }

        // PUT: api/EmpresasAssistencia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmpresaAssistencia empresa)
        {
            if (id != empresa.Id)
            {
                return BadRequest("ID da rota não corresponde ao ID do objeto.");
            }

            var existingItem = await _repository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return NotFound("Empresa de assistência não encontrada.");
            }

            await _repository.UpdateAsync(empresa);

            return NoContent(); // 204
        }

        // DELETE: api/EmpresasAssistencia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound("Empresa de assistência não encontrada.");
            }

            await _repository.DeleteAsync(item);

            return NoContent(); // 204
        }
    }
}