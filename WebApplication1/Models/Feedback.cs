public class Feedback {
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int? AdminId { get; set; }
    public string ComplaintText { get; set; }
    public DateTime ComplaintDate { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Admin Admin { get; set; }
}