using Conjugar.Core.Enum;
using Conjugar.Core.Services;
using Xunit;

namespace Conjugar.Test
{
    public class ValidadorDeVerbosTestes
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
        [InlineData("ação", TipoDeVerbo.NaoEhVerbo)]
        [InlineData("senhor", TipoDeVerbo.NaoEhVerbo)]
        public void DeveValidarQueEhVerbo(string verbo, TipoDeVerbo resultadoEsperado)
        {
            var conjugador = new ConjugarServices();
            var tipo = conjugador.DefinirTipoVerbo(verbo);

            Assert.Equal(resultadoEsperado, tipo);
        }

        [Theory(DisplayName = "Deve preparar o verbo para conjugação")]
        [InlineData(" caber", "caber")]
        [InlineData("ir ", "ir")]
        [InlineData(" saber ", "saber")]
        [InlineData("Cantar", "cantar")]
        [InlineData(" PESQUISAR ", "pesquisar")]
        public void DevePrepararOVerboParaConjugacao(string verbo, string resultadoEsperado)
        {
            var preparador = new ConjugarServices();
            var resultado = preparador.PrepararVerbo(verbo);

            Assert.Equal(resultado, resultadoEsperado);
        }

        [Theory(DisplayName = "Deve fazer validar se é um verbo")]
        [InlineData("saber cantar", false)]
        [InlineData("stron#g", false)]
        [InlineData(" PESQUISAR ", true)]
        [InlineData("senhor", false)]
        public void DeveValidarSeEhVerbo(string verbo, bool resultadoEsperado)
        {
            var validador = new ConjugarServices();
            var resultado = validador.ValidarVerbo(verbo);

            Assert.Equal(resultadoEsperado, resultado.EhValido);
        }
    }
}
