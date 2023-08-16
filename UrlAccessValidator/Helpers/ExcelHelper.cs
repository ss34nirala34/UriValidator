using System.Data;
using UrlAccessValidator.Utilities;

namespace UrlAccessValidator.Helpers
{
    public static class ExcelHelper
    {
        public static DataTable ExcelToDataTable(string filePath)
        {
            var dt = new DataTable();
            using (var reader = new ExcelDataReader(filePath))
            {
                dt.Load(reader);
            }
            return dt ?? new DataTable();
        }

        public static List<string> ExcelToList(string filePath, string columnName)
        {
            List<string> records = new List<string>();
            var dt = ExcelToDataTable(filePath);
            if (dt.Rows.Count > 0)
                records = dt.AsEnumerable().Select(item => (string)item[columnName]).ToList();
            return records ?? new List<string>();
        }
    }
}
