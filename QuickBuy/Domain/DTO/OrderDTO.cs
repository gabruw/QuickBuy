using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.DTO
{
    public class OrderDTO : DefaultDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public virtual UserDTO UserOrder { get; set; }

        public int IdPaymentForm { get; set; }

        [ForeignKey("IdPaymentForm")]
        public virtual PaymentFormDTO PaymentFormOrder { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime? DeliveryForecastDate { get; set; }

        public virtual ICollection<OrderItemDTO> OrderItems { get; set; }

        public override void Validate()
        {
            ClearValidateMenssages();

            if(OrderDate == null)
            {
                ValidationMessage.Add("Erro: A Data do Pedido está vazia.");
            }

            if (DeliveryForecastDate == null)
            {
                ValidationMessage.Add("Erro: A Data de Previsão da Entrega do Pedido está vazia.");
            }

            if(!OrderItems.Any())
            {
                ValidationMessage.Add("Erro: O Pedido não possuí items.");
            }
        }
    }
}
