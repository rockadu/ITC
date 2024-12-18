namespace CrossCutting.Utils.Excel;

public interface IExcelUtils
{
    public byte[] GerarExcel<T>(List<T> dados);
}