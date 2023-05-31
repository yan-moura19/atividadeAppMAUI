using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atividadeApp.Models
{
    internal class Atividade
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataFinalizacao { get; set; }

       
    }
}
