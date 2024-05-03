using BD_Final_Project.Models;

namespace BD_Final_Project.ViewModel
{
    public class JoueurPalmaresVM
    {
        public int EquipeId { get; set; }

        public string Nom { get; set; } = null!;
        public List<Trophee>? trophees { get; set; }


    }
}
