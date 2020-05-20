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

        public List<char> Alfabeto { get; set; }
        public List<Estado> Estados { get; set; }
        public List<Regra> Regras { get; set; }

        public void LerAlfabeto()
        {
            int op;
            do
            {
                Console.WriteLine("Insira o alfabeto: ");
                Alfabeto.Add(Convert.ToChar(Console.ReadLine() ?? throw new InvalidOperationException()));
                Console.WriteLine("Deseja inserir mais um caracter no alfabeto? 1- Sim 2- Não");
                op = Convert.ToInt32(Console.ReadLine());
            } while (op == 1);
        }

        public void LerEstados()
        {
            int op;
            do
            {
                var estado = new Estado();
                Console.WriteLine("Insira o estado: ");
                estado.Nome = Console.ReadLine();
                Console.WriteLine("Esse estado é inicial? 0- Não 1- Sim");
                estado.Inicial =
                    Convert.ToBoolean(int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()));
                Console.WriteLine("Esse estado é final? 0- Não 1- Sim");
                estado.Final =
                    Convert.ToBoolean(int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()));
                Estados.Add(estado);
                Console.WriteLine("Deseja continuar inserindo? 1- Sim 2- Não");
                op = Convert.ToInt32(Console.ReadLine());
            } while (op == 1);
        }

        public void LerRegras()
        {
            int op;
            do
            {
                var regra = new Regra();
                Console.WriteLine("Insira o estado atual: ");
                var estado = Console.ReadLine();
                regra.EstadoAtual = Estados.FirstOrDefault(e => e.Nome == estado);
                Console.WriteLine("Insira o identificador: ");
                regra.Identificador = Convert.ToChar(Console.ReadLine() ?? throw new InvalidOperationException());
                Console.WriteLine("Insira o próximo estado: ");
                estado = Console.ReadLine();
                regra.ProximoEstado = Estados.FirstOrDefault(e => e.Nome == estado);
                Regras.Add(regra);

                Console.WriteLine("Deseja realizar novamente? 1- Sim 2- Não");
                op = Convert.ToInt32(Console.ReadLine());
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
            Console.WriteLine("Alfabetos: ");
            foreach (var alfabetos in Alfabeto)
                Console.WriteLine(alfabetos);

            Console.WriteLine("Estados:");
            foreach (var estado in Estados)
                Console.WriteLine(estado.Nome);

            Console.WriteLine("Estados iniciais:");
            var estadosIniciais = Estados.Where(e => e.Inicial);
            foreach (var estadoInicial in estadosIniciais)
                Console.WriteLine(estadoInicial.Nome);
            
            Console.WriteLine("Estados finais:");
            var estadosFinais = Estados.Where(e => e.Final);
            foreach (var estadoFinal in estadosFinais)
                Console.WriteLine(estadoFinal.Nome);
            
        }

        public void RodarCadeia(char[] cadeia)
        {
            var estadoAtual = Estados.First(e => e.Inicial);
            var cadeiaAceita = false;
            try
            {
                foreach (var c in cadeia)
                {
                    var regra = Regras.FirstOrDefault(r => r.Identificador == c);
                    if (regra == null)
                        throw new CadeiaInvalidaException();

                    estadoAtual = regra.ProximoEstado;

                    Console.WriteLine(!estadoAtual.Final
                        ? $"Leu o Símbolo 1 o foi para o Estado {estadoAtual.Nome}"
                        : $"Leu o Símbolo 1 o foi para o Estado final {estadoAtual.Nome}");
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
                ? $"A cadeia {cadeia} foi ACEITA pelo Autômato!"
                : $"A cadeia {cadeia} foi REJEITADA pelo Autômato!");
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