using System;

class Number 
{
    public static int PrintLastDigit(int number) 
    {
        int n = Math.Abs(number % 10);
        Console.Write(n);
        return n;
    }
}