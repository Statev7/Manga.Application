namespace CreateManga.Application.Constants.RolesConstants
{
    public static class RolesConstants
    {
        public const string USER_ROLE_NAME = "User";
        public const string EDITOR_ROLE_NAME = "Editor";
        public const string AUTHOR_ROLE_NAME = "Author";
        public const string ADMIN_ROLE_NAME = "Admin";

        public const string ADMIN_OR_AUTHOR_OR_EDITOR = ADMIN_ROLE_NAME + "," + AUTHOR_ROLE_NAME + "," + EDITOR_ROLE_NAME;
        public const string ADMIN_OR_AUTHOR = ADMIN_ROLE_NAME + "," + AUTHOR_ROLE_NAME;
    }
}
