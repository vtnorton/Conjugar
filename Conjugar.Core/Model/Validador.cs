using System.Collections.Generic;

namespace Conjugar.Core.Model
{
    public class Validador<T> where T : class
    {
        public Validador(){
            EhValido = true;
            Erros = new List<string>();
        }

        public bool EhValido { get; set; }
        public T Verbo { get; set; }
        public List<string> Erros { get; set; }

        public void AdicionarValidacao(string erro)
        {
            EhValido = false;
            Erros.Add(erro);
        } 
    }
}
