using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class OrderDTO : DefaultDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string IdUser { get; set; }

        [ForeignKey("IdUser")]
        public UserDTO UserOrder { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime DeliveryForecastDate { get; set; }

        public ICollection<OrderItemDTO> OrderItems { get; set; }

        public override void Validate()
        {
            ClearValidateMenssages();

            if(OrderDate == null)
            {
                AddError("Erro: A Data do Pedido está vazia.");
            }
        }
    }
}
