namespace BookStoreApi.StaticClasses
{
    public static class ApplicationValues
    {
        public static Dictionary<string, string> secretValues = new Dictionary<string, string>();

        public static string? textLocalApiKey;

        public static string? stripeKeyPublicTest;

        public static string? stripeKeyPrivateTest;

        public static string? stripeWebHookTest;
    }
}
