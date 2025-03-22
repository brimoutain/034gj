using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using System.Text;

//文本读取方法
public class PatchExcelReader
{
    public struct ExcelData
    {
        public string patchType;
        public string patchJudgmentTime;
        public string patchID;
    }
    public static List<ExcelData> ReadExcel(string filepath)
    {
        List<ExcelData> excelData = new List<ExcelData>();
        //支持中文和日文编码
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //流式读取（第三方方法）
        using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    while (reader.Read())//如果这一行有内容就读取表格中的一行
                    {
                        ExcelData data = new ExcelData();
                        data.patchType = reader.IsDBNull(0) ? string.Empty : reader.GetValue(3)?.ToString();
                        data.patchJudgmentTime = reader.IsDBNull(1) ? string.Empty : reader.GetValue(4)?.ToString();
                        data.patchID = reader.IsDBNull(2) ? string.Empty : reader.GetValue(5)?.ToString();
                        excelData.Add(data);
                    }
                } while (reader.NextResult());//如果后续还有下一张表就循环一次
            }
        }
        return excelData;
    }
}