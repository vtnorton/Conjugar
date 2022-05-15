using Conjugar.Core.Enum;
using Conjugar.Core.Model;
using Conjugar.Core.Utils;
using System.Linq;
using System.Text.RegularExpressions;

namespace Conjugar.Core.Services
{
    public class ConjugarServices
    {
        public Validador<string> ValidarVerbo(string verbo)
        {
            var validador = new Validador<string>();

            verbo = PrepararVerbo(verbo);

            var tipo = DefinirTipoVerbo(verbo);
            if(tipo == TipoDeVerbo.NaoEhVerbo)
                validador.AdicionarValidacao("Erro: A palavra informada não é verbo.");

            if(verbo.Contains(" "))
                validador.AdicionarValidacao("Erro: Não é possível conjugar um verbo em uma frase. Coloque somente o verbo.");

            if (!Regex.IsMatch(verbo, "[A-Za-z]"))
                validador.AdicionarValidacao("Erro: Não é possível fazer essa conjugação.");

            return validador;
        }

        public string PrepararVerbo(string verbo)
        {
            return verbo.ToLower().Trim();
        }

        public TipoDeVerbo DefinirTipoVerbo(string verbo)
        {
            if (Listas.VerbosIrregulares.Contains(verbo))
                return TipoDeVerbo.Irregular;

            if(verbo.EndsWith("ar") || 
               verbo.EndsWith("er") || 
               verbo.EndsWith("ir"))
                return TipoDeVerbo.Regular;

            return TipoDeVerbo.NaoEhVerbo;
        }

        public Conjugacao DefinirConjugacao(string infinitivo)
        {
            if (infinitivo.EndsWith("ar"))
                return Conjugacao.PrimeiraConjugacao;
            if (infinitivo.EndsWith("er"))
                return Conjugacao.SegundaConjugacao;

            return Conjugacao.TerceriaConjugacao;
        }

        // TODO: Fazer testes
        public Verbo ConjugarVerbo(string verbo)
        {
            if(DefinirTipoVerbo(verbo) == TipoDeVerbo.Irregular)
                return ConjugacaoVerboIrregular(verbo);

            return ConjugacaoVerboRegular(verbo);
        }
        public Verbo ConjugacaoVerboIrregular(string infinitivo)
        {
            var verbo = new Verbo
            {
                TipoDeVerbo = TipoDeVerbo.Irregular,
                Infinitivo = infinitivo.FirstCharToUpper(),
            };

            return verbo;
        }

        // TODO: Fazer testes
        public Verbo ConjugacaoVerboRegular(string infinitivo)
        {
            var verbo = CriarPropriedades(infinitivo);

            switch (verbo.Conjugacao)
            {
                case Conjugacao.PrimeiraConjugacao:
                    return ConjugarPrimeiraConjugacao(verbo);
                case Conjugacao.SegundaConjugacao:
                    return ConjugarSegundaConjugacao(verbo);
                case Conjugacao.TerceriaConjugacao:
                    return ConjugarTerceiraConjugacao(verbo);
            }

            return verbo;
        }

        // TODO: Testes
        public Verbo CriarPropriedades(string infinitivo)
        {
            var tipoConjugacao = DefinirConjugacao(infinitivo);
            var vogalTematica = string.Empty;

            switch (tipoConjugacao) { 
                case Conjugacao.PrimeiraConjugacao:
                    vogalTematica = "a";
                    break;
                case Conjugacao.SegundaConjugacao:
                    vogalTematica = "e";
                    break;
                case Conjugacao.TerceriaConjugacao:
                    vogalTematica = "i";
                    break;
            }

            return new Verbo
            {
                TipoDeVerbo = TipoDeVerbo.Regular,
                Infinitivo = infinitivo.FirstCharToUpper(),
                Conjugacao = tipoConjugacao,
                VogalTematica = vogalTematica,
                Radical = infinitivo.Substring(0, infinitivo.Length - 2),
            };
        }
        
        public Verbo ConjugarPrimeiraConjugacao(Verbo verbo)
        {
            /*
                Principal.Text = "infinitivo: " + verbo + "\ngerúndio: " + Radical + "ando\nparticípio: " + Radical + "ado";
                Presente.Text = "eu " + Radical + "o \ntu " + Radical + "as \nele " + Radical + "a \nnós " + Radical + "amos \nvós " + Radical + "ais \neles " + Radical + "am";
                PreteritoImperfeito.Text = "eu " + Radical + "ava \ntu " + Radical + "avas \nele " + Radical + "ava \nnós " + Radical + "ávamos \nvós " + Radical + "áveis \neles " + Radical + "avam";
                PreteritoPerfeito.Text = "eu " + Radical + "ei \ntu " + Radical + "aste \nele " + Radical + "ou \nnós " + Radical + "amos \nvós " + Radical + "astes \neles " + Radical + "aram";
                PreteritoMaisQuePerfeito.Text = "eu " + Radical + "ara \ntu " + Radical + "aras \nele " + Radical + "ara \nnós " + Radical + "áramos \nvós " + Radical + "áreis \neles " + Radical + "aram";
                FuturoDoPresente.Text = "eu " + Radical + "arei \ntu " + Radical + "arás \nele " + Radical + "ará \nnós " + Radical + "aremos \nvós " + Radical + "areis \neles " + Radical + "arão";
                FuturoDoPreterito.Text = "eu " + Radical + "aria \ntu " + Radical + "arias \nele " + Radical + "aria \nnós " + Radical + "aríamos \nvós " + Radical + "aríeis \neles " + Radical + "ariam";
                PresenteDoSubjuntivo.Text = "eu " + Radical + "e \ntu " + Radical + "es \nele " + Radical + "e \nnós " + Radical + "emos \nvós " + Radical + "eis \neles " + Radical + "em";
                PreteritoImperfeitoDoSubjuntivo.Text = "eu " + Radical + "asse \ntu " + Radical + "assses \nele " + Radical + "asse \nnós " + Radical + "ássemos \nvós " + Radical + "ásseis \neles " + Radical + "assem";
                FuturoDoSubjuntivo.Text = "eu " + Radical + "ar \ntu " + Radical + "ares \nele " + Radical + "ar \nnós " + Radical + "armos \nvós " + Radical + "ardes \neles " + Radical + "arem";
                Imperativo.Text = "eu --- \ntu " + Radical + "a \nele " + Radical + "e \nnós " + Radical + "emos \nvós " + Radical + "ai \neles " + Radical + "em";
            */
            return new Verbo();
        }

        public Verbo ConjugarSegundaConjugacao(Verbo verbo)
        {
            /* 
                Principal.Text = "infinitivo: " + verbo + "\ngerúndio: " + Radical + "endo\nparticípio: " + Radical + "ito";
                Presente.Text = "eu " + Radical + "o \ntu " + Radical + "es \nele " + Radical + "e \nnós " + Radical + "emos \nvós " + Radical + "eis \neles " + Radical + "em";
                PreteritoImperfeito.Text = "eu " + Radical + "ia \ntu " + Radical + "ias \nele " + Radical + "ia \nnós " + Radical + "iamos \nvós " + Radical + "ieis \neles " + Radical + "iam";
                PreteritoPerfeito.Text = "eu " + Radical + "i \ntu " + Radical + "este \nele " + Radical + "eu \nnós " + Radical + "emos \nvós " + Radical + "estes \neles " + Radical + "eram";
                PreteritoMaisQuePerfeito.Text = "eu " + Radical + "era \ntu " + Radical + "eras \nele " + Radical + "era \nnós " + Radical + "êramos \nvós " + Radical + "êreis \neles " + Radical + "eram";
                FuturoDoPresente.Text = "eu " + Radical + "erei \ntu " + Radical + "erás \nele " + Radical + "erá \nnós " + Radical + "eremos \nvós " + Radical + "ereis \neles " + Radical + "erão";
                FuturoDoPreterito.Text = "eu " + Radical + "eria \ntu " + Radical + "erias \nele " + Radical + "eria \nnós " + Radical + "eríamos \nvós " + Radical + "eríeis \neles " + Radical + "eriam";
                PresenteDoSubjuntivo.Text = "eu " + Radical + "a \ntu " + Radical + "as \nele " + Radical + "a \nnós " + Radical + "amos \nvós " + Radical + "ais \neles " + Radical + "am";
                PreteritoImperfeitoDoSubjuntivo.Text = "eu " + Radical + "esse \ntu " + Radical + "essses \nele " + Radical + "esse \nnós " + Radical + "êssemos \nvós " + Radical + "êsseis \neles " + Radical + "essem";
                FuturoDoSubjuntivo.Text = "eu " + Radical + "er \ntu " + Radical + "eres \nele " + Radical + "er \nnós " + Radical + "ermos \nvós " + Radical + "erdes \neles " + Radical + "erem";
                Imperativo.Text = "eu --- \ntu " + Radical + "e \nele " + Radical + "a \nnós " + Radical + "amos \nvós " + Radical + "ei \neles " + Radical + "am";
            */
            return new Verbo();
        }

        public Verbo ConjugarTerceiraConjugacao(Verbo verbo)
        {
            /* 
                Principal.Text = "infinitivo: " + verbo + "\ngerúndio: " + Radical + "indo\nparticípio: " + Radical + "ido";
                Presente.Text = "eu " + Radical + "o \ntu " + Radical + "es \nele " + Radical + "e \nnós " + Radical + "imos \nvós " + Radical + "is \neles " + Radical + "em";
                PreteritoImperfeito.Text = "eu " + Radical + "ia \ntu " + Radical + "ias \nele " + Radical + "ia \nnós " + Radical + "iamos \nvós " + Radical + "ieis \neles " + Radical + "iam";
                PreteritoPerfeito.Text = "eu " + Radical + "i \ntu " + Radical + "iste \nele " + Radical + "iu \nnós " + Radical + "imos \nvós " + Radical + "istes \neles " + Radical + "iram";
                PreteritoMaisQuePerfeito.Text = "eu " + Radical + "ira \ntu " + Radical + "iras \nele " + Radical + "ira \nnós " + Radical + "íramos \nvós " + Radical + "íreis \neles " + Radical + "iram";
                FuturoDoPresente.Text = "eu " + Radical + "irei \ntu " + Radical + "irás \nele " + Radical + "irá \nnós " + Radical + "iremos \nvós " + Radical + "ireis \neles " + Radical + "irão";
                FuturoDoPreterito.Text = "eu " + Radical + "iria \ntu " + Radical + "irias \nele " + Radical + "iria \nnós " + Radical + "iríamos \nvós " + Radical + "iríeis \neles " + Radical + "iriam";
                PresenteDoSubjuntivo.Text = "eu " + Radical + "a \ntu " + Radical + "as \nele " + Radical + "a \nnós " + Radical + "amos \nvós " + Radical + "ais \neles " + Radical + "am";
                PreteritoImperfeitoDoSubjuntivo.Text = "eu " + Radical + "isse \ntu " + Radical + "issses \nele " + Radical + "isse \nnós " + Radical + "íssemos \nvós " + Radical + "ísseis \neles " + Radical + "issem";
                FuturoDoSubjuntivo.Text = "eu " + Radical + "ir \ntu " + Radical + "ires \nele " + Radical + "ir \nnós " + Radical + "irmos \nvós " + Radical + "irdes \neles " + Radical + "irem";
                Imperativo.Text = "eu --- \ntu " + Radical + "e \nele " + Radical + "a \nnós " + Radical + "amos \nvós " + Radical + "i \neles " + Radical + "am";
            */
            return new Verbo();
        }
    }
}
