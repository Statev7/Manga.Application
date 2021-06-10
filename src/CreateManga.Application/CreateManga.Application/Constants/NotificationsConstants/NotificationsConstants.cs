namespace CreateManga.Application.Constants.NotificationsConstants
{
    public static class NotificationsConstants
    {
        public const string SUCCESS_NOTIFICATION = "Success";
        public const string ERROR_NOTIFICATION = "Error";
        public const string WARNING_NOTIFICATION = "Warning";
        public const string INFO_NNOTIFICATION = "Info";

        // Manga notifications constants

        public const string SUCCESSFUL_CREATED_A_MANGA = "Successfully created a {0}!";
        public const string SUCCESSFUL_UPDATE_A_MANGA = "Successfully update a {0}!";

        public const string MANGA_ALREADY_EXIST = "This manga already exist!";

        public const string CANNOT_UPDATE_EMPTY_MANGA = "Invalid manga";
        public const string CANNOT_GET_A_DETAILS_ABOUT_EMPTY_MANGA = "Invalid manga";
        public const string FAILED_TO_SET_DATE = "The start date cannot be later than the end date";

        // Characters notifications constants 

        public const string SUCCESSFUL_CREATED_A_CHARACTER = "Successfully created a {0}!";
        public const string SUCCESSFUL_UPDATE_A_CHARACTER = "Successfully update a {0}!";

        public const string CHARACTER_ALREADY_EXIST = "This character already exist!";

        public const string CANNOT_CRAETE_CHARACTER_WITHOUT_MANGA = "A character cannot be created before a manga is created!";
        public const string CANNOT_UPDATE_EMPTY_CHARACTER = "Invalid character";
        public const string CANNOT_GET_A_DETAILS_ABOUT_EMPTY_CHARACTER = "Invalid character";

        // Chapters notifications constants

        public const string SUCCESSFUL_CREATED_A_CHAPTER = "Successfully created a {0}!";
        public const string SUCCESSFUL_UPDATE_A_CHAPTER = "Successfully update a {0}!";

        public const string CHAPTER_ALREADY_EXIST = "This chapter already exist!";

        public const string CANNOT_CRAETE_CHAPTER_WITHOUT_MANGA = "A chapter cannot be created before a manga is created!";
        public const string CANNOT_UPDATE_EMPTY_CHAPTER = "Invalid chapter";
        public const string CANNOT_GET_A_DETAILS_ABOUT_EMPTY_CHAPTER = "Invalid chapter";

        // Votes notifications constants

        public const string SUCCESSFUL_VOTING = "Successfully voted for this manga!";
        public const string ALREADY_VOTED = "You have already voted for this manga!";
        public const string SUCCESSFUL_UNVOTED = "Successfully unvoted from the manga!";
        public const string ALREADY_UNVOTED = "Already unvoted for this manga!";
    }
}
