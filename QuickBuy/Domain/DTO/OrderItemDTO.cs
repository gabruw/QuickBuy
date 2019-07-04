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
        public ProductDTO UserOrder { get; set; }

        public int IdEndereco { get; set; }

        [ForeignKey("IdEndereco")]
        public AddressDTO EnderecoOrderItem { get; set; }

        [Required]
        [MinLength(1)]
        public int Amount { get; set; }

        public override void Validate()
        {
            ClearValidateMenssages();

            if (Amount < 0)
            {
                AddError("Erro: A Quantidade de Items do Pedido está vazia.");
            }
        }
    }
}