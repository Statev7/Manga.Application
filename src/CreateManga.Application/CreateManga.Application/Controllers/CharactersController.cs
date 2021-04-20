namespace CreateManga.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Data;

    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CharactersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}
