using System;

class Line
{
    public static void PrintLine(int lenght)
    {
        Console.WriteLine("{0}$", lenght > 0 ? new String('_', lenght) : "" );
    }
}