namespace CreateManga.Application.Data.Constants
{
    public static class MangasConstants
    {
        public const byte MIN_LENGHT_NAME = 2;
        public const string MIN_LENGHT_NAME_ERROR_MESSAGE = "The field Name cannot contain less that 2 characters!";
        public const byte MAX_LENGHT_NAME = 32;
        public const string MAX_LENGHT_NAME_ERROR_MESSAGE = "The field Name cannot contain more that 32 characters!";

        public const byte MAX_DESCRIPTION_LENGHT = 32;
        public const string DESCRIPTION_ERROR_MESSAGE = "The field Description cannot contain more than 32 characters!";
    }
}
