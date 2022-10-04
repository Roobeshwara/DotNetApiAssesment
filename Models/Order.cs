namespace Order_Details.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string? customerName { get; set; }

        public string? productId { get; set; }

        public int? quantity { get; set; }

        public float? unitPrice { get; set; }

        public float? totalPrice { get; set; }

        public DateTime? placedDate { get; set; }

        public string? status { get; set; }


    }
}
