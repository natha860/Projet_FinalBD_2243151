using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Table("Equipe", Schema = "Equipes")]
    [Index("Nom", Name = "UQ__Equipe__C7D1C61E03B55B90", IsUnique = true)]
    public partial class Equipe
    {
        public Equipe()
        {
            Joueurs = new HashSet<Joueur>();
            MatchEquipeDomicileNavigations = new HashSet<Match>();
            MatchEquipeExterieureNavigations = new HashSet<Match>();
            MatchEquipeGagnanteNavigations = new HashSet<Match>();
            Stades = new HashSet<Stade>();
        }

        [Key]
        [Column("EquipeID")]
        public int EquipeId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Nom { get; set; } = null!;
        public int AnneeDeFondation { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Ville { get; set; } = null!;
        [Column("ChampionnatID")]
        public int ChampionnatId { get; set; }

        [ForeignKey("ChampionnatId")]
        [InverseProperty("Equipes")]
        public virtual Championnat Championnat { get; set; } = null!;
        [InverseProperty("Equipe")]
        public virtual ICollection<Joueur> Joueurs { get; set; }
        [InverseProperty("EquipeDomicileNavigation")]
        public virtual ICollection<Match> MatchEquipeDomicileNavigations { get; set; }
        [InverseProperty("EquipeExterieureNavigation")]
        public virtual ICollection<Match> MatchEquipeExterieureNavigations { get; set; }
        [InverseProperty("EquipeGagnanteNavigation")]
        public virtual ICollection<Match> MatchEquipeGagnanteNavigations { get; set; }
        [InverseProperty("Equipe")]
        public virtual ICollection<Stade> Stades { get; set; }
    }
}
