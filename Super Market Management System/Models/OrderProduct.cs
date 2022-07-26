using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Super_Market_Management_System.Models
{
    public class OrderProduct
    {
        [ForeignKey("orders")]
        public int orderid { get; set; }
        public virtual Order orders { get; set; }

        [ForeignKey("products")]
        public int ProdId { get; set; }
        public virtual Product products { get; set; }

        [Required]
        public int quantity { get; set; }

    }
}
