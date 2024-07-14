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

namespace CaseItau.API.Controllers
{
    [Route("api/fundos")]
    [ApiController]
    public class FundController : ControllerBase
    {
        private readonly IFundService _fundService;

        public FundController(IFundService fundService)
        {
            _fundService = fundService;
        }

        /// <summary>
        /// Lista todos os fundos da base
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            //var lista = new List<Fund>();
            //var con = new SQLiteConnection("Data Source=dbCaseItau.s3db");
            //con.Open();
            //var cmd = con.CreateCommand();
            //cmd.CommandText = "SELECT F.*, T.NOME AS NOME_TIPO FROM FUNDO F INNER JOIN TIPO_FUNDO T ON T.CODIGO = F.CODIGO_TIPO";
            //cmd.CommandType = System.Data.CommandType.Text;
            //var reader = cmd.ExecuteReader();
            //while(reader.Read())
            //{
            //    var f = new Fund();
            //    f.Codigo = reader[0].ToString();
            //    f.Nome = reader[1].ToString();
            //    f.Cnpj = reader[2].ToString();
            //    f.CodigoTipo = int.Parse(reader[3].ToString());
            //    f.Patrimonio = decimal.Parse(reader[4].ToString());
            //    f.NomeTipo = reader[5].ToString();                
            //    lista.Add(f);
            //}
            
            return Ok(new ResponseDTO());
        }

        // GET: api/Fundo/ITAUTESTE01
        [HttpGet("{codigo}", Name = "Get")]
        public IActionResult Get(string codigo)
        {
            //var con = new SQLiteConnection("Data Source=dbCaseItau.s3db");
            //con.Open();
            //var cmd = con.CreateCommand();
            //cmd.CommandText = "SELECT F.*, T.NOME AS NOME_TIPO FROM FUNDO F INNER JOIN TIPO_FUNDO T ON T.CODIGO = F.CODIGO_TIPO WHERE F.CODIGO = '" + codigo + "'";
            //cmd.CommandType = System.Data.CommandType.Text;
            //var reader = cmd.ExecuteReader();
            //if (reader.Read())
            //{
            //    var f = new Fundo();
            //    f.Codigo = reader[0].ToString();
            //    f.Nome = reader[1].ToString();
            //    f.Cnpj = reader[2].ToString();
            //    f.CodigoTipo = int.Parse(reader[3].ToString());
            //    f.Patrimonio = decimal.Parse(reader[4].ToString());
            //    f.NomeTipo = reader[5].ToString();
            //    return f;
            //}
            return Ok(new ResponseDTO());
        }

        // POST: api/Fundo
        [HttpPost]
        public IActionResult Post([FromBody] Fund value)
        {
            //var con = new SQLiteConnection("Data Source=dbCaseItau.s3db");
            //con.Open();
            //var cmd = con.CreateCommand();
            //cmd.CommandText = "INSERT INTO FUNDO VALUES('" + value.Codigo + "','" + value.Nome + "','" + value.Cnpj + "',"+value.CodigoTipo.ToString() + ",NULL)";
            //cmd.CommandType = System.Data.CommandType.Text;
            //var resultado = cmd.ExecuteNonQuery();
            return Ok(new ResponseDTO());
        }

        // PUT: api/Fundo/ITAUTESTE01
        [HttpPut("{codigo}")]
        public IActionResult Put(string codigo, [FromBody] Fund value)
        {
            //var con = new SQLiteConnection("Data Source=dbCaseItau.s3db");
            //con.Open();
            //var cmd = con.CreateCommand();
            //cmd.CommandText = "UPDATE FUNDO SET Nome = '" + value.Nome + "', CNPJ = '" + value.Cnpj + "', CODIGO_TIPO = " + value.CodigoTipo + " WHERE CODIGO = '" + codigo + "'";
            //cmd.CommandType = System.Data.CommandType.Text;
            //var resultado = cmd.ExecuteNonQuery();
            return Ok(new ResponseDTO());
        }

        // DELETE: api/Fundo/ITAUTESTE01
        [HttpDelete("{codigo}")]
        public IActionResult Delete(string codigo)
        {
            //var con = new SQLiteConnection("Data Source=dbCaseItau.s3db");
            //con.Open();
            //var cmd = con.CreateCommand();
            //cmd.CommandText = "DELETE FROM FUNDO WHERE CODIGO = '" + codigo + "'";
            //cmd.CommandType = System.Data.CommandType.Text;
            //var resultado = cmd.ExecuteNonQuery();
            return Ok(new ResponseDTO());
        }

        [HttpPut("{codigo}/patrimonio")]
        public IActionResult MovimentarPatrimonio(string codigo, [FromBody] decimal value)
        {
            //var con = new SQLiteConnection("Data Source=dbCaseItau.s3db");
            //con.Open();
            //var cmd = con.CreateCommand();
            //cmd.CommandText = "UPDATE FUNDO SET PATRIMONIO = IFNULL(PATRIMONIO,0) + " + value.ToString() + " WHERE CODIGO = '" + codigo + "'";
            //cmd.CommandType = System.Data.CommandType.Text;
            //var resultado = cmd.ExecuteNonQuery();
            return Ok(new ResponseDTO());
        }
    }
}
