using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Es.Models
{
    public class TTrComModel
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("customer_id")]
        public int CustomerId { get; set; }

        [JsonPropertyName("customer_first_name")]
        public string CustomerFirstName { get; set; }

        [JsonPropertyName("customer_last_name")]
        public string CustomerLastName { get; set; }

        [JsonPropertyName("customer_full_name")]
        public string CustomerFullName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("taxful_total_price")]
        public double TaxfulTotalPrice { get; set; }

        [JsonPropertyName("category")]
        public string[] Category { get; set; }

        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }

        [JsonPropertyName("order_date")]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("products")]
        public Product[] Products { get; set; }
    }

    public class Product

    {
        [JsonPropertyName("product_id")]
        public long ProductId { get; set; }

        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }
    }
}