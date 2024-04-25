using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Table("Match", Schema = "Equipes")]
    public partial class Match
    {
        [Key]
        [Column("MatchID")]
        public int MatchId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Column("StadeID")]
        public int? StadeId { get; set; }
        public int? EquipeDomicile { get; set; }
        public int? EquipeExterieure { get; set; }
        public int? EquipeGagnante { get; set; }
        [Column("SaisonID")]
        public int? SaisonId { get; set; }

        [ForeignKey("EquipeDomicile")]
        [InverseProperty("MatchEquipeDomicileNavigations")]
        public virtual Equipe? EquipeDomicileNavigation { get; set; }
        [ForeignKey("EquipeExterieure")]
        [InverseProperty("MatchEquipeExterieureNavigations")]
        public virtual Equipe? EquipeExterieureNavigation { get; set; }
        [ForeignKey("EquipeGagnante")]
        [InverseProperty("MatchEquipeGagnanteNavigations")]
        public virtual Equipe? EquipeGagnanteNavigation { get; set; }
        [ForeignKey("SaisonId")]
        [InverseProperty("Matches")]
        public virtual Saison? Saison { get; set; }
        [ForeignKey("StadeId")]
        [InverseProperty("Matches")]
        public virtual Stade? Stade { get; set; }
    }
}
