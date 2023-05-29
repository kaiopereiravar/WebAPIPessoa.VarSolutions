using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPIPessoa.Application.Autenticacao;
using WebAPIPessoa.Application.Eventos;
using WebAPIPessoa.Repository;

namespace WebAPIPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly PessoaContext _context;

        public AutenticacaoController(PessoaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Login([FromBody] AutenticacaoRequest request)
        {
            var autenticacaoService = new AutenticacaoService(_context);//Ele esta pegando todos codigos da "AutenticacaoService", e jogando dentro da variavel
            var resposta = autenticacaoService.Autenticar(request);

            if (resposta == null) 
            {
                return Unauthorized();
            }

            else
            {
                return Ok(resposta);
            }

        }

        [HttpPost]
        [Route("esqueciSenha")]
        public IActionResult Esquecisenha()
        {
            var rabbit = new RabbitMQProducer();
            rabbit.EnviarMensagem();

            return NoContent();
        }

    }
}
