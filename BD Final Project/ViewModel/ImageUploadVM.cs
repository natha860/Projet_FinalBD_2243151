using BD_Final_Project.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BD_Final_Project.ViewModel
{
    public class ImageUploadVM
    {

        public IFormFile ? FormFile {  get; set; }
        public Joueur Joueurs { get; set; } = null!;

        public string Nas {  get; set; }

      

    }
}
