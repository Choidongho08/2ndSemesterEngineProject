using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExcelReader : MonoSingleton<ExcelReader>
{
    public string csvFileName = "ErrorCodeExcel";

    public Dictionary<string, ErrorCode> dictionaryErrorCode = new Dictionary<string, ErrorCode>(); // 상품명 : menu(상품 이름, 가격, 정보)

    [System.Serializable]
    public class ErrorCode
    {
        public string name;
        public string errorCode;
    }

    private void Start()
    {
        ReadCSV();
    }

    private void ReadCSV()
    {
        string path = csvFileName + ".csv";

        List<ErrorCode> errorCodeList = new List<ErrorCode>();

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
            Debug.Log(dictionaryErrorCode.Count);
            Debug.Log(menu.name);
        }
        Debug.Log(ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.ManyLobbyRequests));
        Debug.Log(dictionaryErrorCode[ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.QuickJoinLobby)].errorCode);
    }
}
