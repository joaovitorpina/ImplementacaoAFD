using System;
using System.Collections.Generic;
using System.Linq;

namespace ImplementacaoAFD
{
    public class Automato
    {
        public Automato()
        {
            Alfabeto = new List<char>();
            Estados = new List<Estado>();
            Regras = new List<Regra>();
        }

        private List<char> Alfabeto { get; }
        private List<Estado> Estados { get; }
        private List<Regra> Regras { get; }

        private void LerAlfabeto()
        {
            int op;
            Console.WriteLine("Insira o alfabeto");
            do
            {
                Console.WriteLine("Escreva um simbolo para ser inserido: ");
                var simbolo = Convert.ToChar(Console.ReadLine() ?? throw new InvalidOperationException());

                if (Alfabeto.Any(a => a == simbolo))
                    Console.WriteLine($"Simbolo {simbolo} ja foi inserido no alfabeto");
                else
                    Alfabeto.Add(simbolo);

                Console.WriteLine("Deseja inserir mais um caracter no alfabeto? 1- Sim 0- Não");
                op = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\n\n" + MostrarAlfabeto());
            } while (op == 1);
        }

        private void LerEstados()
        {
            int op;
            Console.WriteLine("Insira o(s) estado(s): ");
            do
            {
                var estado = new Estado();
                Console.WriteLine("Escreva o nome do estado:");
                estado.Nome = Console.ReadLine();
                Console.WriteLine("Esse estado é inicial? 1- Sim 0- Não");
                estado.Inicial =
                    Convert.ToBoolean(int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()));
                Console.WriteLine("Esse estado é final? 1- Sim 0- Não");
                estado.Final =
                    Convert.ToBoolean(int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()));
                Estados.Add(estado);

                var e = Estados.FirstOrDefault(e => e.Nome == estado.Nome);
                if (e != default)
                    Console.WriteLine($"O estado {e.Nome} ja existe");

                Console.WriteLine("Deseja continuar inserindo? 1- Sim 0- Não");
                op = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\n\n" + MostrarEstados());
            } while (op == 1);
        }

        private void LerRegras()
        {
            int op;
            Console.WriteLine("Insira o estado atual: ");
            do
            {
                var regra = new Regra();
                Console.WriteLine("Escreva o estado inicial da regra:");
                var estado = Console.ReadLine();
                regra.EstadoAtual = Estados.FirstOrDefault(e => e.Nome == estado);
                Console.WriteLine("Escreva o identificador da regra:");
                regra.Identificador = Convert.ToChar(Console.ReadLine() ?? throw new InvalidOperationException());
                Console.WriteLine("Escreva o proximo estado da regra:");
                estado = Console.ReadLine();
                regra.ProximoEstado = Estados.FirstOrDefault(e => e.Nome == estado);
                Regras.Add(regra);

                if (Regras.Any(r => r == regra))
                    Console.WriteLine("A regra ja existe");

                Console.WriteLine("Deseja realizar novamente? 1- Sim 0- Não");
                op = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\n" + MostrarRegras());
            } while (op == 1);
        }

        public void CriarAutomato()
        {
            LerAlfabeto();
            LerEstados();
            LerRegras();
        }

        public void MostrarAutomato()
        {
            Console.WriteLine("DESCRIÇÃO DO AF:");

            Console.WriteLine(MostrarAlfabeto());

            Console.WriteLine(MostrarEstados());

            Console.WriteLine(MostrarEstadosInicais());

            Console.WriteLine(MostrarEstadosFinais());

            Console.WriteLine(MostrarRegras());

            Console.WriteLine(Environment.NewLine + Environment.NewLine + "TIPO de AF: Determinístico");
        }

        private string MostrarAlfabeto()
        {
            var alfabeto = "Alfabeto: ";

            foreach (var simbolo in Alfabeto)
                if (simbolo == Alfabeto.Last())
                    alfabeto += simbolo;
                else
                    alfabeto += simbolo + ", ";

            return alfabeto;
        }

        private string MostrarEstados()
        {
            var estados = "Estados: ";

            foreach (var estado in Estados)
                if (estado == Estados.Last())
                    estados += estado.Nome;
                else
                    estados += estado.Nome + ", ";

            return estados;
        }

        private string MostrarEstadosInicais()
        {
            var estadosIniciais = "Estados Iniciais: ";
            var listaEstadosIniciais = Estados.Where(e => e.Inicial).ToList();

            foreach (var estadoInicial in listaEstadosIniciais)
                if (estadoInicial == listaEstadosIniciais.Last())
                    estadosIniciais += estadoInicial.Nome;
                else
                    estadosIniciais += estadoInicial.Nome + ", ";

            return estadosIniciais;
        }

        private string MostrarEstadosFinais()
        {
            var estadosFinais = "Estados Finais: ";
            var listaEstadosFinais = Estados.Where(e => e.Final).ToList();

            foreach (var estadoFinal in listaEstadosFinais)
                if (estadoFinal == listaEstadosFinais.Last())
                    estadosFinais += estadoFinal.Nome;
                else
                    estadosFinais += estadoFinal.Nome + ", ";

            return estadosFinais;
        }

        private string MostrarRegras()
        {
            return Regras.Aggregate("Funcões de transicão: ",
                (current, regra) =>
                    current + $"{regra.EstadoAtual.Nome} + {regra.Identificador} -> {regra.ProximoEstado.Nome}" +
                    Environment.NewLine);
        }

        public void RodarCadeia(char[] cadeia)
        {
            if (!Regras.Any(r => r.EstadoAtual.Inicial))
            {
                Console.WriteLine(
                    "O automato deve ter uma regra inicial para rodar uma Cadeia, crie o automato novamente com no minimo 1 regra inicial");
                return;
            }

            Console.WriteLine($"Cadeia a ser analisada: {new string(cadeia)}");

            var estadoAtual = Estados.First(e => e.Inicial);
            var cadeiaAceita = false;
            try
            {
                foreach (var c in cadeia)
                {
                    var regra = Regras.FirstOrDefault(r => r.Identificador == c && r.EstadoAtual == estadoAtual);
                    if (regra == null)
                        throw new CadeiaInvalidaException();

                    estadoAtual = regra.ProximoEstado;

                    Console.WriteLine(!estadoAtual.Final
                        ? $"Leu o Símbolo {c} o foi para o Estado {estadoAtual.Nome}"
                        : $"Leu o Símbolo {c} o foi para o Estado final {estadoAtual.Nome}");
                }

                if (estadoAtual.Final)
                    cadeiaAceita = true;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case CadeiaInvalidaException _:
                        break;
                    default: throw;
                }
            }

            Console.WriteLine(cadeiaAceita
                ? $"A cadeia {new string(cadeia)} foi ACEITA pelo Autômato!"
                : $"A cadeia {new string(cadeia)} foi REJEITADA pelo Autômato!");
        }
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

    public class CadeiaInvalidaException : Exception
    {
    }
}