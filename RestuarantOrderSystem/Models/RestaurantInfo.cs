using System;
using System.ComponentModel.DataAnnotations;

namespace RestuarantOrderSystem.Models
{
    public class RestaurantInfo
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string RestaurantName { get; set; } = "Gourmet Bistro RROMS";

        [Required, MaxLength(250)]
        public string Address { get; set; } = "123 Culinary Avenue, Suite 100";

        [Phone, MaxLength(20)]
        public string ContactNumber { get; set; } = "+1-800-555-RROMS";

        public TimeSpan OpeningTime { get; set; } = new TimeSpan(11, 0, 0);
        public TimeSpan ClosingTime { get; set; } = new TimeSpan(23, 0, 0);

        [MaxLength(500)]
        public string DiningAreas { get; set; } = "Main Hall, Outdoor Terrace, VIP Lounge";
    }
}