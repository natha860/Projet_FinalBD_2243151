using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Keyless]
    public partial class VwGestionDesEquipe
    {
        [StringLength(100)]
        [Unicode(false)]
        public string Equipe { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string? Joueur { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Championnat { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Niveau { get; set; }
        [Column("ChampionnatID")]
        public int? ChampionnatId { get; set; }
        [Column("JoueurID")]
        public int? JoueurId { get; set; }
        [Column("EquipeID")]
        public int EquipeId { get; set; }
        public int AnneeDeFondation { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Ville { get; set; } = null!;
    }
}
