using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startBtn : MonoBehaviour
{

    public Text levelTxt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gameStart()
    {
        int levelNum = 0;
        string level = levelTxt.text.Substring(6);
        bool isLevelNum = int.TryParse(level, out levelNum);
        if (dataTransfer.D != null)
        {
            if (isLevelNum)
            {
                dataTransfer.D.dataToSend = levelNum;
            }
        }

        SceneManager.LoadScene("MainScene");
    }
}
