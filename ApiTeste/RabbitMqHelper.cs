using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste
{
    public class Contador
    {
        private int _valorAtual = 0;

        public int ValorAtual { get => _valorAtual; }

        public void Incrementar()
        {
            _valorAtual++;
        }
    }

    public class Conteudo
    {
        [Required]
        public string Mensagem { get; set; }
    }

    public class Resultado
    {
    }
}
