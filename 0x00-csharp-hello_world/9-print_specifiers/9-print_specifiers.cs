﻿using System;

class Program
{
	static void Main(string[] args)
	{
		double percent = .7553;
		double currency = 98765.4321;
		Console.WriteLine($"Percent: {percent.ToString("P2", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))}\nCurrency: {currency.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))}");
	}
}