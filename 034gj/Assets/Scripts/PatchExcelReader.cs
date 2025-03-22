using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using System.Text;

//�ı���ȡ����
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
        //֧�����ĺ����ı���
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //��ʽ��ȡ��������������
        using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    while (reader.Read())//�����һ�������ݾͶ�ȡ����е�һ��
                    {
                        ExcelData data = new ExcelData();
                        data.patchType = reader.IsDBNull(0) ? string.Empty : reader.GetValue(3)?.ToString();
                        data.patchJudgmentTime = reader.IsDBNull(1) ? string.Empty : reader.GetValue(4)?.ToString();
                        data.patchID = reader.IsDBNull(2) ? string.Empty : reader.GetValue(5)?.ToString();
                        excelData.Add(data);
                    }
                } while (reader.NextResult());//�������������һ�ű��ѭ��һ��
            }
        }
        return excelData;
    }
}