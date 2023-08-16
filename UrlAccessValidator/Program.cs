// See https://aka.ms/new-console-template for more information
using UrlAccessValidator.Helpers;

Console.WriteLine("========= Validation Started ============");

//Read Urls
var filePath = @"D:\MyProjects\WinForms\UrlAccessValidator\Urls.xlsx";
var urls = ExcelHelper.ExcelToList(filePath, "Url");

Console.WriteLine("Status:\tUrl: \t\t\t Message:");
Console.WriteLine("------:\t---------------\t\t --------");

foreach (var url in urls)
{
    var response = WebClientHelper.ValidateUrl(url);
    Console.WriteLine(String.Format("{0}:\t{1} => {2}", response.StatusCode, url, response.Message));
}
Console.WriteLine("======== Validation Completed ===========");

Console.ReadKey();
