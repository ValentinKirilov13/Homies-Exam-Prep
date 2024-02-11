﻿namespace Homies.Models
{
    public class EventDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Start { get; set; } = string.Empty;

        public string End { get; set; } = string.Empty;

        public string Organiser { get; set; }  = string.Empty;

        public string CreatedOn { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}
