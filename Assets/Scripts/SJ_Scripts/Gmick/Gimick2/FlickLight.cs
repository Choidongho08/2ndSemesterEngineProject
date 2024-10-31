using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickLight : PuzzleManager
{
    [SerializeField] private Button BulbSwitch;
    [SerializeField] private Button LEDSwitch;

    [SerializeField] private string puzzleAns;
    [SerializeField] int a;

    public void FlickEbulb()
    {
        Debug.Log("Àü±¸ µþ±ï");
        puzzleAns += "0";
        FlickChecker();
        EndChecking();
    }

    public void FlickLED()
    {
        Debug.Log("LED µþ±ï");
        puzzleAns += "1";
        FlickChecker();
        EndChecking();
    }

    private void FlickChecker()
    {

        if (_gimick.numberCheck[a] == puzzleAns[a])
            a++;
        else
        {
            a = 0;
            puzzleAns = null;
            Debug.Log("ÃÊ±âÈ­");
        }
    }

    private void EndChecking()
    {
        if (_gimick.numberCheck.Length == a)
        {
            _gimick.puzzleEnd = true;
            Debug.Log("ÆÛÁñ ²ý");
        }
    }
}
