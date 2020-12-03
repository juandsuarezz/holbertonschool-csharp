﻿using System;
using System.Collections.Generic;

class LList
{
    public static LinkedListNode<int> Insert(LinkedList<int> myLList, int n)
    {
        LinkedListNode<int> node = myLList.First;

        if (n < node.Value)
        {
            return myLList.AddBefore(node, n);
        }

        while (node != null)
        {
            if (n >= node.Value && (node.Next == null || n < node.Next.Value))
            {
                return myLList.AddAfter(node, n);
            }
            node = node.Next;
        }
        return null;
    }
}