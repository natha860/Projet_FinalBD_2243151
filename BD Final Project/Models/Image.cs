using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD_Final_Project.Models
{
    [Table("Images", Schema = "Equipes")]
    [Index("Identifiant", Name = "UC_Joueur_Identifiant", IsUnique = true)]
    public partial class Image
    {
        [Key]
        [Column("ImagesID")]
        public int ImagesId { get; set; }
        public Guid Identifiant { get; set; }
        [Column("JoueurID")]
        public int? JoueurId { get; set; }
        public byte[]? Photo { get; set; }

        [ForeignKey("JoueurId")]
        [InverseProperty("Images")]
        public virtual Joueur? Joueur { get; set; }
    }
}
