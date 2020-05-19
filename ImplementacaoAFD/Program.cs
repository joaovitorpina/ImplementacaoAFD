using System;
using System.Collections.Generic;

namespace ImplementacaoAFD
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Automato automato = null;
            Console.WriteLine("======MENU======");
            
            Console.WriteLine("Escolha uma opcão do menu:");
            var menu = (Menu) Convert.ToInt32(Console.ReadLine());
            switch (menu)
            {
                case Menu.CriacaoAFD:
                    automato = new Automato();
                    automato.CriarAutomato();
                    break;
                case Menu.RodarCadeia:
                    if (automato == null)
                    {
                        Console.WriteLine("asubdaiusbnda");
                    }
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