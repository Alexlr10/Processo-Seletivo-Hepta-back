using Microsoft.AspNetCore.Mvc;
using processo_seletivo.Interfaces;
using processo_seletivo.Messages;
using processo_seletivo.Models;

namespace processo_seletivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionariosController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Funcionario> Get()
        {
            try
            {
                var funcionarios = _funcionarioService.GetTodosFuncionarios();
                return Ok(funcionarios);
            }
            catch (MessageException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Funcionario> GetById([FromRoute] int id)
        {
            try
            {
                var funcionario = _funcionarioService.GetFuncionarioPorId(id);

                return Ok(funcionario);
            }
            catch (MessageException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Funcionario> Post([FromBody] Funcionario novoFuncionario)
        {
            try
            {
                var funcionarioCriado = _funcionarioService.AddFuncionario(novoFuncionario);
                return CreatedAtAction(nameof(GetById), new { id = funcionarioCriado.Id }, funcionarioCriado);
            }
            catch (MessageException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Funcionario> Put([FromRoute] int id, [FromBody] Funcionario funcionarioAlterado)
        {
            try
            {
                return Ok(_funcionarioService.UpdateFuncionario(id, funcionarioAlterado));
            }
            catch (MessageException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                _funcionarioService.DeleteFuncionario(id);
                return Ok("Funcionário excluído com sucesso.");
            }
            catch (MessageException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}
