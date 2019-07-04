using System.Collections.Generic;
using System.Linq;

namespace Domain.DTO
{
    public abstract class DefaultDTO
    {
        private List<string> _validationMessage { get; set; }

        protected List<string> ValidationMessage
        {
            get { return _validationMessage ?? (_validationMessage = new List<string>()); }
        }

        protected void ClearValidateMenssages()
        {
            ValidationMessage.Clear();
        }

        protected void AddError(string message)
        {
            ValidationMessage.Add(message);
        }

        public abstract void Validate();

        protected bool isValid
        {
            get { return ValidationMessage.Any(); }
        }
    }
}