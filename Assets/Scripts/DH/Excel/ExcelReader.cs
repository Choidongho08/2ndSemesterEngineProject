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
        string path = csvFileName + ".csv";

        List<ErrorCode> errorCodeList = new List<ErrorCode>();

        TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);

        StreamReader reader = new StreamReader(Application.dataPath + "/" + path);

        bool isFinish = false;

        while (isFinish == false)
        {
            string data = reader.ReadLine();
            if (data == null)
            {
                isFinish = true;
                break;
            }

            var splitData = data.Split(',');

            ErrorCode menu = new ErrorCode();

            menu.name = splitData[0];
            menu.errorCode = splitData[1];

            dictionaryErrorCode.Add(menu.name, menu);
        }

    }
}
