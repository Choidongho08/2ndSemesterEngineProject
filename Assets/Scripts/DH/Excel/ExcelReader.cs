using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExcelReader : MonoSingleton<ExcelReader>
{
    [SerializeField] private string csvFileName = "ErrorCodeExcel";

    public Dictionary<string, ErrorCode> dictionaryErrorCode = new Dictionary<string, ErrorCode>(); 

    [System.Serializable]
    public class ErrorCode
    {
        public string name;
        public string errorCode;
    }
    void Start()
    {
        
        ReadCSV();
    }

    private void ReadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');

                if (fields[0] == string.Empty)
                    break;

                ErrorCode menu = new ErrorCode();

                menu.name = fields[0];
                Debug.Log(menu.name);
                menu.errorCode = fields[1];
                Debug.Log(menu.errorCode);

                dictionaryErrorCode.Add(menu.name, menu);
            }
        }
        else
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다.");
        }
    }
}