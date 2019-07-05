using Domain.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DTO
{
    public class PaymentFormDTO : DefaultDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(800)]
        public string Describe { get; set; }

        public bool isUndefined
        {
            get { return Id == (int)PaymentFormEnum.Undefined; }
        }

        public bool isBillet
        {
            get { return Id == (int)PaymentFormEnum.Billet; }
        }

        public bool isCreditCard
        {
            get { return Id == (int)PaymentFormEnum.CreditCard; }
        }

        public bool isDeposit
        {
            get { return Id == (int)PaymentFormEnum.Deposit; }
        }

        public override void Validate()
        {
            ClearValidateMenssages();

            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                ValidationMessage.Add("Erro: O Nome da Forma de Pagamento está vazio.");
            }
        }
    }
}