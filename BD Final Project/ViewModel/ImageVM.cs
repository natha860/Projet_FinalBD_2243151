using BD_Final_Project.Models;

namespace BD_Final_Project.ViewModel
{
    public class ImageVM
    {
        public Joueur Joueurs { get; set; } = null!;
        public Image Image { get; set; } = null!;
        public string? fichierImage { get; set; }
        public int JoueurId { get; set; }
        public byte[] Nas { get; set; }
    }
}
