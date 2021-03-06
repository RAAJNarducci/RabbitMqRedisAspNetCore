﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _logger;
        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Estado>> Get()
        {
            _logger.LogError("Hakuna matata");
            return Ok();
            //var connection = @"Server=db;Database=ExemplosDapper;User=SA;Password=DockerSql2017;";
            //IEnumerable<Estado> estados;
            //using (SqlConnection conexao = new SqlConnection(connection))
            //{
            //    estados = conexao.Query<Estado>(
            //        "SELECT E.SiglaEstado, E.NomeEstado, E.NomeCapital, " +
            //               "R.NomeRegiao " +
            //        "FROM dbo.Estados E " +
            //        "INNER JOIN dbo.Regioes R ON R.IdRegiao = E.IdRegiao " +
            //        "ORDER BY E.NomeEstado");
            //}
            //return Ok(estados);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            _logger.LogError("Erro");
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
