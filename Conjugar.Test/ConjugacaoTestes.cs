using Conjugar.Core.Enum;
using Conjugar.Core.Services;
using Xunit;

namespace Conjugar.Test
{
    public class ConjugacaoTestes
    {
        [Theory(DisplayName = "Deve definir corretamente o tipo de conjugação")]
        [InlineData("cantar", Conjugacao.PrimeiraConjugacao)]
        [InlineData("varrer", Conjugacao.SegundaConjugacao)]
        [InlineData("parir", Conjugacao.TerceriaConjugacao)]
        public void DeveDefinirTipoDeConjugacao(string verbo, Conjugacao resultadoEsperado)
        {
            var conjugador = new ConjugarServices();
            var resultado = conjugador.DefinirConjugacao(verbo);

            Assert.Equal(resultadoEsperado, resultado);
        }
    }
}
