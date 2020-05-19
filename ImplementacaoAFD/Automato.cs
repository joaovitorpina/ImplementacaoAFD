using System.Collections.Generic;

namespace ImplementacaoAFD
{
    public class Automato
    {
        public List<char> Alfabeto { get; set; }
        public List<Estado> Estados { get; set; }
        public List<Regra> Regras { get; set; }
    }

    public class Estado
    {
        public string Nome { get; set; }
        public bool Inicial { get; set; }
        public bool Final { get; set; }
    }

    public class Regra
    {
        public Estado EstadoAtual { get; set; }
        public char Identificador { get; set; }
        public Estado ProximoEstado { get; set; }
    }
}