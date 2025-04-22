namespace STS.Infrastructure.Repositories.Dapper.Queries;

public static class Query
{
    public static string GetAllCities => "SELECT * FROM Cities";
    public static string GetAllMainCategories => "SELECT Id, Name, ImgPath FROM MainCategories";
    public static string GetAllSubCategories => "SELECT s.Id, s.Name, m.Name AS MainCategoryName FROM SubCategories s JOIN MainCategories m ON s.Id = m.Id";
    public static string GetAllTaskItems => "SELECT t.Id, t.Name, t.ImgPath, t.BasePrice, t.SubCategoryId, s.Name AS SubCategoryName FROM TaskItems t JOIN SubCategories s ON t.SubCategoryId = s.Id";
}
