using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Table("Palmares", Schema = "Equipes")]
    [Index("JoueurId", "TropheeId", Name = "IX_Joueur_PalmaresID_TropheeID")]
    public partial class Palmare
    {
        [Key]
        [Column("PalmaresID")]
        public int PalmaresId { get; set; }
        [Column("TropheeID")]
        public int TropheeId { get; set; }
        [Column("JoueurID")]
        public int JoueurId { get; set; }

        [ForeignKey("JoueurId")]
        [InverseProperty("Palmares")]
        public virtual Joueur Joueur { get; set; } = null!;
        [ForeignKey("TropheeId")]
        [InverseProperty("Palmares")]
        public virtual Trophee Trophee { get; set; } = null!;
    }
}
