using Conjugar.Core.Enum;
using Conjugar.Core.Model;
using Conjugar.Core.Utils;
using System.Linq;

namespace Conjugar.Core.Services
{
    public class ConjugarServices
    {
        public TipoDeVerbo DefinirTipoVerbo(string verbo)
        {
            // TODO: Verificar se é verbo
            if (Listas.VerbosIrregulares.Contains(verbo))
                return TipoDeVerbo.Irregular;

            return TipoDeVerbo.Regular;
        }

        public Verbo ConjugarVerbo(string verboASerConjugado)
        {
            return new Verbo();
        }
    }
}
