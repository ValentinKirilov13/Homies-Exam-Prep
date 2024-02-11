using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Homies.Common.Constants.DataConstants;

namespace Homies.Data.Models
{
    [Comment("Tables of Types")]
    public class Type
    {
        [Key]
        [Comment("Type Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TypeNameMaxLenght)]
        [Comment("Type Name")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}

