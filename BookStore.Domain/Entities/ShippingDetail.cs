namespace BookStore.Domain.Entities
{
    public class ShippingDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Zip { get; set; }

    }
}
