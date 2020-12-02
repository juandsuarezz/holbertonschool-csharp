using System;
using System.Collections.Generic;

class List
{
    public static int MaxInteger(List<int> mylist)
    {
        if (mylist == null || mylist.Count == 0)
        {
            Console.WriteLine("List is empty");
            return -1;
        }
        int max = mylist[0];
        foreach (int val in myList)
        {
            if (val > max)
            {
                max = val;
            }
        }
        return max;
    }
}