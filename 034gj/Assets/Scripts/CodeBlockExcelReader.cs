using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using System.Text;

//�ı���ȡ����
public class CodeBlockExcelReader
{
    public struct ExcelData
    {
        public string codeBlockType;
        public string codeJudgmentTime;
        public string codeBlockID;
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
                        data.codeBlockType = reader.IsDBNull(0) ? string.Empty : reader.GetValue(0)?.ToString();
                        data.codeJudgmentTime = reader.IsDBNull(1) ? string.Empty : reader.GetValue(1)?.ToString();
                        data.codeBlockID = reader.IsDBNull(2) ? string.Empty : reader.GetValue(2)?.ToString();
                        excelData.Add(data);
                    }
                } while (reader.NextResult());//�������������һ�ű��ѭ��һ��
            }
        }
        return excelData;
    }
}