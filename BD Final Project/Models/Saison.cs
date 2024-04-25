using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Table("Saison", Schema = "Championnats")]
    public partial class Saison
    {
        public Saison()
        {
            Matches = new HashSet<Match>();
        }

        [Key]
        [Column("SaisonID")]
        public int SaisonId { get; set; }
        public int Annee { get; set; }
        public int Duree { get; set; }
        [Column("ChampionnatID")]
        public int? ChampionnatId { get; set; }

        [ForeignKey("ChampionnatId")]
        [InverseProperty("Saisons")]
        public virtual Championnat? Championnat { get; set; }
        [InverseProperty("Saison")]
        public virtual ICollection<Match> Matches { get; set; }
    }
}
