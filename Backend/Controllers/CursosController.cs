using System;
using System.Threading.Tasks;
using System.Web.Http;
using web_api.Models;
using web_api.Repositories;
using System.Web.Http.Cors;

namespace web_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cursos")]
    public class CursosController : ApiController
    {
        private readonly ICursoRepository _repository;

        public CursosController()
        {
            _repository = new CursoRepository();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var cursos = await _repository.ObterTodosAsync();
                return Ok(cursos);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var curso = await _repository.ObterPorIdAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            return Ok(curso);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Curso curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            curso.Id = 0;
            var novoId = await _repository.InserirAsync(curso);
            curso.Id = novoId;

            return Created($"api/cursos/{novoId}", curso);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] Curso curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != curso.Id)
            {
                return BadRequest();
            }

            var existente = await _repository.ObterPorIdAsync(id);
            if (existente == null)
            {
                return NotFound();
            }

            await _repository.AtualizarAsync(curso);
            return Ok(curso);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var curso = await _repository.ObterPorIdAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            if (curso.Ativo)
            {
                return BadRequest("Não é possível excluir um curso ativo.");
            }

            await _repository.ExcluirAsync(id);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}