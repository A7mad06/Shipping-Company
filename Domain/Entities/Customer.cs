using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; } = Guid.NewGuid();
        [Required]
        public string CustomerName { get; set; } = null!;
        [Required,Phone]
        public string PhoneNumber { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
