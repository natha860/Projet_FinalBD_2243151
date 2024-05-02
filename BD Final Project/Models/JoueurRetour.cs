using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Keyless]
    [Table("JoueurRetour", Schema = "Equipes")]
    public partial class JoueurRetour
    {
        [Column("JoueurID")]
        public int JoueurId { get; set; }
        [StringLength(9)]
        [Unicode(false)]
        public string Nas { get; set; } = null!;

        [ForeignKey("JoueurId")]
        public virtual Joueur Joueur { get; set; } = null!;
    }
}
