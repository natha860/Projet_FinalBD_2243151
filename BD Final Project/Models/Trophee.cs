using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Table("Trophee", Schema = "Equipes")]
    [Index("Nom", Name = "UQ__Trophee__C7D1C61EF6B610DB", IsUnique = true)]
    public partial class Trophee
    {
        public Trophee()
        {
            Palmares = new HashSet<Palmare>();
        }

        [Key]
        [Column("TropheeID")]
        public int TropheeId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Nom { get; set; } = null!;
        public int Annee { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Categorie { get; set; } = null!;

        [InverseProperty("Trophee")]
        public virtual ICollection<Palmare> Palmares { get; set; }
    }
}
