namespace CreateManga.Application.Data
{
    using Microsoft.AspNetCore.Identity;

    using CreateManga.Application.Data.Constants;

    public static class IdentityOptionsProvider
    {
        public static void GetIdentityOptions(IdentityOptions options)
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = PassowrdConstants.MIN_LENGHT_PASSWORD;
            options.User.RequireUniqueEmail = true;
        }
    }
}
