using Domain.Entities; // Para usar a entidade GrupoVeiculos
using Domain.Interfaces; // Para usar o IGrupoVeiculosRepository
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define a rota base como "api/GrupoVeiculos"
    public class GrupoVeiculosController : ControllerBase
    {
        // 1. Injeção de Dependência do Repositório
        private readonly IGrupoVeiculosRepository _repository;

        public GrupoVeiculosController(IGrupoVeiculosRepository repository)
        {
            _repository = repository;
        }

        // 2. GET: api/GrupoVeiculos (Listar Todos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrupoVeiculos>>> GetAll()
        {
            var grupos = await _repository.GetAllAsync();
            return Ok(grupos);
        }

        // 3. GET: api/GrupoVeiculos/5 (Buscar por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<GrupoVeiculos>> GetById(int id)
        {
            var grupo = await _repository.GetByIdAsync(id);
            if (grupo == null)
            {
                return NotFound("Grupo de veículo não encontrado.");
            }
            return Ok(grupo);
        }

        // 4. POST: api/GrupoVeiculos (Cadastrar Novo)
        [HttpPost]
        public async Task<ActionResult<GrupoVeiculos>> Create(GrupoVeiculos grupo)
        {
            // Nota: grupo vem do corpo (body) da requisição
            await _repository.AddAsync(grupo);

            // Retorna um 201 Created com o novo objeto
            return CreatedAtAction(nameof(GetById), new { id = grupo.Id }, grupo);
        }

        // 5. PUT: api/GrupoVeiculos/5 (Atualizar)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GrupoVeiculos grupo)
        {
            if (id != grupo.Id)
            {
                return BadRequest("O ID da rota não corresponde ao ID do objeto.");
            }

            var existingGrupo = await _repository.GetByIdAsync(id);
            if (existingGrupo == null)
            {
                return NotFound("Grupo de veículo não encontrado.");
            }

            // Atualiza o objeto
            await _repository.UpdateAsync(grupo);

            return NoContent(); // Retorna 204 No Content (sucesso, sem corpo)
        }

        // 6. DELETE: api/GrupoVeiculos/5 (Excluir)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var grupo = await _repository.GetByIdAsync(id);
            if (grupo == null)
            {
                return NotFound("Grupo de veículo não encontrado.");
            }

            await _repository.DeleteAsync(grupo);

            return NoContent(); // Retorna 204 No Content
        }
    }
}