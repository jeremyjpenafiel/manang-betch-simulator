public class NPC : Entity
{
    public Transaction CurrentTransaction { get; private set; }
    
    
    public void StartTransaction(Order order)
    {
        string mop = "Mop"; // Replace with actual mop logic
        CurrentTransaction = new Transaction(order, mop);
    }
}
