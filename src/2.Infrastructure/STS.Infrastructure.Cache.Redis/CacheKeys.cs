namespace STS.Infrastructure.Cache.Redis
{
    public static class CacheKeys
    {
        #region Repositories Cache

        public static string GetAllCities = nameof(GetAllCities);
        public static string GetAllMainCategories = nameof(GetAllMainCategories);
        public static string GetAllSubCategories = nameof(GetAllSubCategories);
        public static string GetAllTaskItems = nameof(GetAllTaskItems);

        #endregion
    }
}
