using System;

class Program
{
    static void Main(string[] args)
    {
        for ((int i, int max) = (0, 100); i < max; i++)
        {
            Console.Write("{0:D2}{1}", i, (i < max - 1 ? ", " : "\n"));
        }
    }
}
