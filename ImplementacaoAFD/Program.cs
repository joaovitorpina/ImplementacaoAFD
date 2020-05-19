using System;

namespace ImplementacaoAFD
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Escolha uma opcão do menu:");
            var menu = (Menu) Convert.ToInt32(Console.ReadLine());
            switch (menu)
            {
                case Menu.CriacaoAFD:
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