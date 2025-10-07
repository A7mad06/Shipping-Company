using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum OrderStatus
    {
        Preparing,Shipped,Delivered
    };
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Preparing;
        #region Driver Relationship
        [ForeignKey(nameof(Driver))]
        public string NationalId { get; set; } = null!;
        public Driver Driver { get; set; } = null!;
        #endregion
        #region Vehicle Relationship
        [ForeignKey(nameof(Vehicle))]
        public int VehicleNumber { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        #endregion
        #region Customer Relationship
        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        #endregion
        public Location? DriverLocation { get; set; }
        public Location OrderLocation { get; set; } = null!;
        public Location DeliveryLocation { get; set; } = null!;
    }
}
