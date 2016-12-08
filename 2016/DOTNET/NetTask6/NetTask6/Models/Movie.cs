﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetTask6.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public uint Year { get; set; }
        public string Image { get; set; }

        public virtual Director Director { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }
    }
}
