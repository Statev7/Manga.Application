namespace CreateManga.Application.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.UsersMangas = new HashSet<MangaUser>();
        }

        public virtual ICollection<MangaUser> UsersMangas { get; set; }
    }
}
