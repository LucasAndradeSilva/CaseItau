 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using CaseItau.Data.Entities;
using CaseItau.Domain.Interfaces;
using CaseItau.Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CaseItau.API.Controllers
{
    [Route("api/fundos")]
    [ApiController]
    public class FundController : ControllerBase
    {
        private readonly IFundService _fundService;
        private readonly ILogger<FundController> _logger;
        public FundController(IFundService fundService, ILogger<FundController> logger)
        {
            _fundService = fundService;
            _logger = logger;
        }

        /// <summary>
        /// Lista todos os fundos da base
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 20)
        {
            _logger.LogInformation($"Buscando fundos - Page: {pageNumber}, Size: {pageSize} - [FundController]");

            var funds = await _fundService.GetPaginationFundsAsync(pageNumber, pageSize);

            var response = new ResponseDTO(funds);

            if (!funds.Any())
            {
                response.Success = false;
                response.Message = $"Nenhum fundo encontrado";

                _logger.LogInformation($"{response.Message} - [FundController]");

                return NotFound(response);
            }

            _logger.LogInformation($"{funds.Count()} fundos encontrados - [FundController]");

            return Ok(response);
        }


        /// <summary>
        /// Obtem um fundo espeficido - GET: api/Fundo/ITAUTESTE01
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpGet("{codigo}", Name = "Get")]
        public async Task<IActionResult> GetAsync(string codigo)
        {
            _logger.LogInformation("Buscando um fundo - [FundController]");

            var fund = await _fundService.GetAsync(x => x.Code.ToLower() == codigo.ToLower());

            var response = new ResponseDTO(fund);

            if (fund is null)
            {
                response.Success = false;
                response.Message = $"Nenhum fundo com o código {codigo} encontrado";

                _logger.LogInformation($"{response.Message} - [FundController]");

                return NotFound(response);
            }

            _logger.LogInformation($"Fundo com o código {codigo} encontrado - [FundController]");

            return Ok(response);
        }
        
        /// <summary>
        /// Insere um novo fundo - POST: api/Fundo
        /// </summary>
        /// <param name="fund"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Fund fund)
        {
            _logger.LogInformation("Inserindo fundo - [FundController]");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Erro nas validações - [FundController]", ModelState);
                return BadRequest(new ResponseDTO(ModelState, false, "Erro nas validações"));
            }                
            
            await _fundService.AddAsync(fund);

            _logger.LogInformation($"Fundo com o código {fund.Code} inserido com sucesso! - [FundController]");

            return CreatedAtAction(nameof(PostAsync) ,new ResponseDTO(fund));                        
        }
        
        /// <summary>
        /// Alterar dados do fundo - api/Fundo/ITAUTESTE01
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{codigo}")]
        public IActionResult Put(string codigo, [FromBody] Fund fund)
        {            
            _logger.LogInformation($"Alterando fundo código {codigo} - [FundController]");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Erro nas validações - [FundController]", ModelState);
                return BadRequest(new ResponseDTO(ModelState, false, "Erro nas validações"));
            }

            fund.Code = codigo;

            _fundService.Update(fund);

            _logger.LogInformation($"Fundo Código {codigo} Atualizado com Sucesso! - [FundController]");

            return Ok(new ResponseDTO(fund, message: "Fundo Atualizado com Sucesso!"));
        }
        
        /// <summary>
        /// Deletar fundo - api/Fundo/ITAUTESTE01
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Delete(string codigo)
        {
            _logger.LogInformation($"Deletando fundo código {codigo} - [FundController]");

            var fund = await _fundService.GetAsync(x => x.Code.ToLower() == codigo.ToLower());

            var response = new ResponseDTO(fund);

            if (fund is null)
            {
                response.Success = false;
                response.Message = $"Nenhum fundo código {codigo} encontrado";

                _logger.LogInformation($"{response.Message} - [FundController]");

                return NotFound(response);
            }

            _fundService.Remove(fund);

            response = new ResponseDTO(fund, message: $"Fundo código {codigo} removido com sucesso!");

            _logger.LogInformation($"{response.Message} - [FundController]");

            return Ok(response);
        }

        /// <summary>
        /// Atualiza patrimonio do fundo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="equity"></param>
        /// <returns></returns>
        [HttpPut("{codigo}/patrimonio")]        
        public async Task<IActionResult> MovimentarPatrimonioAsync(string codigo, [FromBody] decimal equity)
        {
            _logger.LogInformation($"Atualizando Patrimonio do fundo código {codigo} - [FundController]");

            var fund = await _fundService.GetAsync(x => x.Code.ToLower() == codigo.ToLower());

            var response = new ResponseDTO(fund);

            if (fund is null)
            {
                response.Success = false;
                response.Message = $"Nenhum fundo código {codigo} encontrado";

                _logger.LogInformation($"{response.Message} - [FundController]");

                return NotFound(response);
            }

            fund.Equity = equity;

            _fundService.Update(fund);

            response = new ResponseDTO(fund, message: $"O Patrimonio do fundo código {codigo} foi atualizado!");

            _logger.LogInformation($"{response.Message} - [FundController]");

            return Ok(response);
        }
    }
}
