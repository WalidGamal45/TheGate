namespace Domain.Domains
{
    public class Basket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int  ProductId { get; set; }
        public int  Amount { get; set; }
    }
}
