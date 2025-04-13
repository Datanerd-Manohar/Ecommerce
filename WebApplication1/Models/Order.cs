public class Order {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public int ProductId {get; set;}
    public string ProductName { get; set; }
    public float TotalAmount { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Product Product { get; set; }
}
