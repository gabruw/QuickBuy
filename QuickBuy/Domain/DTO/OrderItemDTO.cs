using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DTO
{
    public class OrderItemDTO : DefaultDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdProduct { get; set; }

        [ForeignKey("IdProduct")]
        public virtual ProductDTO ProductOrderItem { get; set; }

        public int IdAddress { get; set; }

        [ForeignKey("IdAddress")]
        public virtual AddressDTO AddressOrderItem { get; set; }

        //For Track
        public virtual OrderDTO OrderOrderItem { get; set; }

        [Required]
        public int Amount { get; set; }

        public override void Validate()
        {
            ClearValidateMenssages();

            if (Amount < 1)
            {
                ValidationMessage.Add("Erro: A Quantidade de Items do Pedido está vazia.");
            }
        }
    }
}