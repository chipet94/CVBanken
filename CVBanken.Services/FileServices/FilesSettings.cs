using System;

namespace CVBanken.Services.FileServices
{
    public static class FilesSettings
    {
        public static long MAX_SIZE = 10000000; //10 mb ish in bytes
        public static int MAX_USER_FILES = 5;
        public static string[] SUPPORTED_FILES = { ".jpg", ".jpeg", ".png", ".txt", ".docx", ".doc", ".pdf" };
        public static string[] SUPPORTED_IMAGES = { ".jpg", ".jpeg", ".png" };
        
    }
}