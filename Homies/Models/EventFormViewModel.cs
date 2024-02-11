using System.ComponentModel.DataAnnotations;
using static Homies.Common.Constants.DataConstants;
using static Homies.Common.Messages.ErrorMessages;

namespace Homies.Models
{
    public class EventFormViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(EventNameMaxLenght, MinimumLength = EventNameMinLenght, ErrorMessage = LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(EventDescriptionMaxLenght, MinimumLength = EventDescriptionMinLenght, ErrorMessage = LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Start { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string End { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int TypeId { get; set; }

        public IEnumerable<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();
    }
}
