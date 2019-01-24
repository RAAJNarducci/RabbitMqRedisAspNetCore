using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ApiTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private static object syncObject = Guid.NewGuid();
        private static string[] Siglas = new string[]
        {
            "SP", "RJ", "SC"
        };
        [HttpGet]
        public Estado Get([FromServices]IConfiguration config, [FromServices]IDistributedCache cache)
        {
            Estado estado = null;
            string sigla = Siglas[new Random().Next(0, 3)];
            string valorJSON = cache.GetString(sigla);
            if (valorJSON == null)
            {
                // Exemplo de implementação do pattern Double-checked locking
                lock (syncObject)
                {
                    valorJSON = cache.GetString(sigla);
                    if (valorJSON == null)
                    {
                        estado = ObterEstado(config, sigla);
                        estado.IsCache = false;
                        DistributedCacheEntryOptions opcoesCache =
                            new DistributedCacheEntryOptions();
                        opcoesCache.SetAbsoluteExpiration(
                            TimeSpan.FromMinutes(2));

                        valorJSON = JsonConvert.SerializeObject(estado);
                        cache.SetString(sigla, valorJSON, opcoesCache);
                    }
                }
            }

            if (estado == null && valorJSON != null)
            {
                estado = JsonConvert
                    .DeserializeObject<Estado>(valorJSON);
                estado.IsCache = true;
            }

            return estado;
        }

        private Estado ObterEstado(IConfiguration config, string sigla)
        {
            var connection = @"Server=db;Database=ExemplosDapper;User=SA;Password=DockerSql2017;";
            using (SqlConnection conexao = new SqlConnection(connection))
            {
                return conexao.QueryFirst<Estado>(
                    "SELECT E.SiglaEstado, E.NomeEstado, E.NomeCapital, " +
                           "R.NomeRegiao " +
                    "FROM dbo.Estados E " +
                    "INNER JOIN dbo.Regioes R ON R.IdRegiao = E.IdRegiao " +
                    $"WHERE E.SiglaEstado = '{sigla}' " +
                    "ORDER BY E.NomeEstado");
            }
        }
    }
}