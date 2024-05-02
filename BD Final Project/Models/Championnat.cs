using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Table("Championnat", Schema = "Championnats")]
    [Index("Nom", Name = "UQ__Champion__C7D1C61EA84B5741", IsUnique = true)]
    public partial class Championnat
    {
        public Championnat()
        {
            Equipes = new HashSet<Equipe>();
            Saisons = new HashSet<Saison>();
        }

        [Key]
        [Column("ChampionnatID")]
        public int ChampionnatId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Nom { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Niveau { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Format { get; set; } = null!;

        [InverseProperty("Championnat")]
        public virtual ICollection<Equipe> Equipes { get; set; }
        [InverseProperty("Championnat")]
        public virtual ICollection<Saison> Saisons { get; set; }
    }
}
