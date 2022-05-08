using Conjugar.Core.Enum;
using Conjugar.Core.Services;
using Xunit;

namespace Conjugar.Test
{
    public class ValidadorDeVerbos
    {

        [Theory(DisplayName = "Deve validar que é verbo")]
        [InlineData("andar", TipoDeVerbo.Regular)]
        [InlineData("esquecer", TipoDeVerbo.Regular)]
        [InlineData("garantir", TipoDeVerbo.Regular)]
        [InlineData("ansiar", TipoDeVerbo.Irregular)]
        [InlineData("caber", TipoDeVerbo.Irregular)]
        [InlineData("coibir", TipoDeVerbo.Irregular)]
        [InlineData("compor", TipoDeVerbo.Irregular)]
        [InlineData("palavra", TipoDeVerbo.NaoEhVerbo)]
        [InlineData("verbos", TipoDeVerbo.NaoEhVerbo)]
        public void DeveValidarQueEhVerbo(string verbo, TipoDeVerbo resultado)
        {
            var conjugador = new ConjugarServices();
            var tipo = conjugador.DefinirTipoVerbo(verbo);

            Assert.Equal(resultado, tipo);
        }
    }
}
