using Conjugar.Core.Enum;
using System.Collections.Generic;

namespace Conjugar.Core.Model
{
    public class Verbo
    {
        public string Radical { get; set; }
        public string Infinitivo { get; set; }
        public string VogalTematica { get; set; }
        public string Gerundio { get; set; }
        public string ParticipioPassado { get; set; }
        public TipoDeVerbo TipoDeVerbo { get; set; }
        public Conjugacao Conjugacao { get; set; }

        public List<Conjugacoes> Conjugacoes { get; set; }
    }
}
