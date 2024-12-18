using ClosedXML.Excel;

namespace CrossCutting.Utils.Excel;

public class ExcelUtils : IExcelUtils
{
    public byte[] GerarExcel<T>(List<T> dados)
    {
        if (dados == null || !dados.Any())
            throw new ArgumentException("A coleção de dados está vazia ou é nula.");

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add(typeof(T).Name);
            var propriedades = typeof(T).GetProperties();

            for (int col = 0; col < propriedades.Length; col++)
            {
                worksheet.Cell(1, col + 1).Value = propriedades[col].Name;
            }

            int row = 2;
            foreach (var item in dados)
            {
                for (int col = 0; col < propriedades.Length; col++)
                {
                    var valor = propriedades[col].GetValue(item);
                    worksheet.Cell(row, col + 1).Value = valor!.ToString();
                }
                row++;
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }
}