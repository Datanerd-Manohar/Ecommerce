public class Product {
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public float Price { get; set; }
    public float Rating { get; set; }
    public int ProductCategoryId { get; set; }
    public virtual ProductCategory ProductCategory { get; set; }
}