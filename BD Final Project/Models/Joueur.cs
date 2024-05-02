using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Table("Joueur", Schema = "Equipes")]
    [Index("Nom", Name = "UQ__Joueur__C7D1C61E5A6044F7", IsUnique = true)]
    public partial class Joueur
    {
        public Joueur()
        {
            Images = new HashSet<Image>();
            Palmares = new HashSet<Palmare>();
        }

        [Key]
        [Column("JoueurID")]
        public int JoueurId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Nom { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string DateNaissance { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Position { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string? Nationalite { get; set; }
        [Column("EquipeID")]
        public int? EquipeId { get; set; }
        [Column("NAS")]
        public byte[]? Nas { get; set; }

        [ForeignKey("EquipeId")]
        [InverseProperty("Joueurs")]
        public virtual Equipe? Equipe { get; set; }
        [InverseProperty("Joueur")]
        public virtual ICollection<Image> Images { get; set; }
        [InverseProperty("Joueur")]
        public virtual ICollection<Palmare> Palmares { get; set; }
    }
}
