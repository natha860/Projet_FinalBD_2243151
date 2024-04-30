using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Table("ChangementClub", Schema = "Equipes")]
    public partial class ChangementClub
    {
        [Key]
        [Column("ChangementClubID")]
        public int ChangementClubId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Joueur { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? AncienClub { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? NouveauClub { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateRecutement { get; set; }
    }
}
