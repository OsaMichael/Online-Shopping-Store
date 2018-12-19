using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Core.Business
{
  public abstract  class BaseModel : IValidatableObject
    {
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public void Assign(object source)
        {
            if (source != null)
            {
                var destProperties = GetType().GetProperties();
                foreach (var sourceProperty in source.GetType().GetProperties())
                {
                    foreach (var desProperty in destProperties)
                    {
                        if (desProperty.Name == sourceProperty.Name && desProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                        {
                            desProperty.SetValue(this, sourceProperty.GetValue(source, new object[] { }), new object[] { });
                            break;
                        }
                    }
                }
            }
        }

        public Operation<ValidationResult[]> Validate()
        {
            var errors = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this, serviceProvider: null, items: null), errors, true);
            return new Operation<ValidationResult[]>
            {
                Result = errors.ToArray(),
                Succeeded = errors.Any() == false,
                Message = errors.Any() ? errors.Select(e => e.ErrorMessage).Aggregate((ag, e) => ag + ", " + e) : ""
            };

        }
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}
