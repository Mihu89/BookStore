using BookStore.Domain.Models;

namespace BookStore.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}