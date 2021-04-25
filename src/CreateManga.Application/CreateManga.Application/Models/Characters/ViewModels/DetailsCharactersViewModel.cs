namespace CreateManga.Application.Models.Characters.ViewModels
{
    using System.ComponentModel;

    public class DetailsCharactersViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public char Gender { get; set; }

        public string Ability { get; set; }

        [DisplayName("Manga")]
        public string MangaName { get; set; }
    }
}
