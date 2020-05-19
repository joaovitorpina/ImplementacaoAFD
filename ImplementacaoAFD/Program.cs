using System;

namespace ImplementacaoAFD
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Automato automato = null;
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
                    break;
                case Menu.Sair:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    internal enum Menu
    {
        CriacaoAFD = 1,
        RodarCadeia,
        Sair
    }
}