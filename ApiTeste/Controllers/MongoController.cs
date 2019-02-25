using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ApiTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoController : ControllerBase
    {
        private IConfiguration _configuration;
        public MongoController(IConfiguration config)
        {
            _configuration = config;
        }

        [Route("createDb")]
        [HttpGet]
        public void CreateDb()
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile($"appsettings.json");
            //var configuration = builder.Build();

            MongoClient client = new MongoClient(
                _configuration.GetConnectionString("ConexaoMongo"));
            var dbNames = client.ListDatabaseNames();
            IMongoDatabase db = client.GetDatabase("dbEstado");

            var estado = db.GetCollection<Estado>("Estado");
            Estado estadoSP = new Estado
            {
                IdRegiao = 4,
                NomeCapital = "São Paulo",
                NomeEstado = "São Paulo",
                SiglaEstado = "SP"
            };
            estado.InsertOne(estadoSP);
        }

        [HttpGet("state/{codigo}")]
        public IActionResult GetStado(string codigo)
        {
            Estado est = null;
                est = ObterItem<Estado>(codigo);

            return Ok(est);
        }

        private T ObterItem<T>(string codigo)
        {
            MongoClient client = new MongoClient(
                _configuration.GetConnectionString("ConexaoCatalogo"));
            IMongoDatabase db = client.GetDatabase("DBCatalogo");

            var filter = Builders<T>.Filter.Eq("Codigo", codigo);

            return db.GetCollection<T>("Catalogo")
                .Find(filter).FirstOrDefault();
        }
    }
}