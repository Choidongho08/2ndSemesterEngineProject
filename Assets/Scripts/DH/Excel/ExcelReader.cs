using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExcelReader : MonoSingleton<ExcelReader>
{
    // �о� �� ���� �̸�
    public string csvFileName = "ErrorCodeExcel";

    // key:value ���·� ����
    // key(�޴���)�� value�� �̾ƿ��� ����
    // ���ϴ� ���·� �����ص� ����
    public Dictionary<string, ErrorCode> dictionaryErrorCode = new Dictionary<string, ErrorCode>(); // ��ǰ�� : menu(��ǰ �̸�, ����, ����)

    // �о� �� �����͸� ���� ����ü
    // ���� Ŭ������ �����߽��ϴ�! struct�� �����ص� �����ؿ�.
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

    // ������ �о� ���� �޼���
    private void ReadCSV()
    {
        // ���� �̸�.Ȯ����
        string path = csvFileName+".csv";

        // �����͸� �����ϴ� ����Ʈ
        // ���ϰ� �����ϱ� ���� List�� ����
        // ���ϴ� ���·� �����Ͻø� �˴ϴ�!
        List<ErrorCode> errorCodeList = new List<ErrorCode>();

        // stream reader
        // UTF-8�� ���ڵ� �Ϸ��� �ش� StreamReader�� �ʿ���!!
        // Application.dataPath�� Unity�� Assets������ ������
        // �ڿ� �������� ������ �ִ� ��θ� �ۼ�
        // ex) Assets > Files�� menu.csv�� ��������? "/" + "Files/menu.csv"�߰�
        StreamReader reader = new StreamReader(Application.dataPath + "/" + path);

        // ������ ���� �Ǻ��ϱ� ���� bool Ÿ�� ����
        bool isFinish = false;


        while (isFinish == false)
        {
            // ReadLine�� ���پ� �о string���� ��ȯ�ϴ� �޼���
            // ���پ� �о data������ ������
            string data = reader.ReadLine(); // �� �� �б�

            // data ������ ������� Ȯ��
            if (data == null)
            {
                // ���� ����ٸ�? ������ �� == ������ �����̴�
                // isFinish�� true�� ����� �ݺ��� Ż��
                isFinish = true;
                break;
            }

            // .csv�� ,(�޸�)�� �������� �����Ͱ� ���еǾ� �����Ƿ�
            // ,(�޸�)�� �������� �����͸� ������ list�� ����
            // ex) ������ġ,200��,���־��! => [������ġ][200��][���־��!]
            var splitData = data.Split(','); // �޸��� ������ ����

            // ���� �����ߴ� �޴� ��ü�� �������ְ�
            ErrorCode menu = new ErrorCode();

            // �޴��� ����Ʈ�� �ִ� �����ͷ� �ʱ�ȭ
            // menu.name�� splitData[0]��° �ִ� �����͸� ��´ٴ� �ǹ�
            // ��, menu ��ü name�������� splitData[0]�� ��� "������ġ"�� ���ϴ�.
            menu.name = splitData[0];
            menu.errorCode = splitData[1];

            // menu ��ü�� �� ��Ҵٸ� dictionary�� key�� value������ ����
            // �̷��� �صθ� dicMenu.Add("������ġ");�� menu.name, menu.price .. ���� ����
            dictionaryErrorCode.Add(menu.name, menu);
            Debug.Log(menu.name);
            Debug.Log(dictionaryErrorCode.Count); // �� ������ üũ
        }
        Debug.Log(ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.QuickJoinLobby));
        Debug.Log(dictionaryErrorCode[ErrorEnum.instance.GetErrorCode(ErrorCodeEnum.QuickJoinLobby)].errorCode);
    }
}
