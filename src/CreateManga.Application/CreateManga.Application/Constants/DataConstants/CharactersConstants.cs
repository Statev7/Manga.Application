namespace CreateManga.Application.Data.Constants
{
    public static class CharactersConstants
    {
        public const byte MIN_LENGHT_NAME = 2;
        public const string MIN_LENGHT_NAME_ERROR_MESSAGE = "The field Name cannot contain less that 2 characters!";
        public const byte MAX_LENGHT_NAME = 64;
        public const string MAX_LENGHT_NAME_ERROR_MESSAGE = "The field Name cannot contain more that 64 characters!";

        public const int MIN_AGE_VALUE = 1;
        public const int MAX_AGE_VALUE = 100000;
    }
}
