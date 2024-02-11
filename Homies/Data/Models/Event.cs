using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Homies.Common.Constants.DataConstants;

namespace Homies.Data.Models
{
    [Comment("Table of Events")]
    public class Event
    {
        [Key]
        [Comment("Event Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventNameMaxLenght)]
        [Comment("Event Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(EventDescriptionMaxLenght)]
        [Comment("Event Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Event Organiser Identifier")]
        public string OrganiserId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(OrganiserId))]
        public IdentityUser Organiser { get; set; } = null!;

        [Required]
        [Comment("Create Date of Event")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Comment("Start Date of Event")]
        public DateTime Start { get; set; }

        [Required]
        [Comment("End Date of Event")]
        public DateTime End { get; set; }

        [Required]
        [Comment("Event Type Identifier")]
        public int TypeId { get; set; }

        [Required]
        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; } = null!;

        public ICollection<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
    }
}

