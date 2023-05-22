using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using processo_seletivo.Models;
using System.Net;

namespace processo_seletivo.Controllers
{
    /// <summary>
    /// Endpoints para testes da infraestrutura da solução
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        /// <summary>
        /// Usado para verificar se a API está rodando
        /// </summary>
        /// <returns></returns>
        [HttpGet("servidorweb")]
        public ActionResult GetServidorWeb()
        {
            return Ok("API operacional");
        }
        
        /// <summary>
        /// Usado para verificar se o acesso ao BD está ok
        /// </summary>
        /// <returns></returns>
        [HttpGet("bancodados")]
        public ActionResult GetBancoDados()
        {
            using (var bd = new ProcessoseletivoContext())
            {
                try
                {
                    var dados = bd.Lotacaos.Count();
                    return Ok("BD operacional");
                }
                catch(Exception e)
                {
                    while (e.InnerException != null) e = e.InnerException;
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Erro no acesso ao BD: " + e.Message);
                }
            }
        }
    }
}
