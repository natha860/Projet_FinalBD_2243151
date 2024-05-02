using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Table("Stade", Schema = "Championnats")]
    [Index("Nom", Name = "UQ__Stade__C7D1C61E7440D60E", IsUnique = true)]
    public partial class Stade
    {
        public Stade()
        {
            Matches = new HashSet<Match>();
        }

        [Key]
        [Column("StadeID")]
        public int StadeId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Nom { get; set; } = null!;
        public int Capacite { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Ville { get; set; }
        public int AnneeInauguration { get; set; }
        [Column("EquipeID")]
        public int? EquipeId { get; set; }

        [ForeignKey("EquipeId")]
        [InverseProperty("Stades")]
        public virtual Equipe? Equipe { get; set; }
        [InverseProperty("Stade")]
        public virtual ICollection<Match> Matches { get; set; }
    }
}
