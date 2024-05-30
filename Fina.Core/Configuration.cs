namespace Fina.Core
{
    public static class Configuration
    {
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 25;
        public const int DefaultStatusCodeResponse = 200;

        public static string ConnectionString { get; set; } = string.Empty;
        public static string BackEndURL { get;  set;} = string.Empty;
        public static string FrontEndURL { get; set; } = string.Empty ;
    }
}
