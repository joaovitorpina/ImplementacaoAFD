using System;

namespace ImplementacaoAFD
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Automato automato = null;
            var sair = false;
            do
            {
                Console.WriteLine("======MENU======");
                Console.WriteLine("1- Criação AFD");
                Console.WriteLine("2- Rodar Cadeia");
                Console.WriteLine("3- Sair");
                Console.WriteLine("================");
                Console.WriteLine("Escolha uma opcão do menu:");
                var menu = (Menu) Convert.ToInt32(Console.ReadLine());
                switch (menu)
                {
                    case Menu.CriacaoAFD:
                        automato = new Automato();
                        automato.CriarAutomato();
                        automato.MostrarAutomato();
                        break;
                    case Menu.RodarCadeia:
                        if (automato == null)
                        {
                            Console.WriteLine("Crie o automato antes de rodar uma cadeia");
                        }
                        else
                        {
                            Console.WriteLine("Digite a cadeia a ser rodada no automato: ");
                            var cadeia = Console.ReadLine()?.ToCharArray();
                            automato.RodarCadeia(cadeia);
                        }

                        break;
                    case Menu.Sair:
                        sair = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            } while (!sair);
        }
    }

    internal enum Menu
    {
        CriacaoAFD = 1,
        RodarCadeia,
        Sair
    }
}