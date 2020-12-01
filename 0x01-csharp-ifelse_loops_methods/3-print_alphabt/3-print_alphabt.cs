using System;

class Program
{
    static void Main(string[] args)
    {
        for (int i = 'a'; i <= 'z'; i++)
        {
            if (!(i == 'q' || i == 'e')) {
            Console.Write((char)i);
            }
        }
    }
}