using UnityEngine;

public class Transaction
{
    public Order Order { get; }
    public string Mop { get; }
    
    public Transaction(Order order, string mop)
    {
        Order = order;
        Mop = mop;
    }
}
