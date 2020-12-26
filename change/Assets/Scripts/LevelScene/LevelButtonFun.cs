using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonFun : MonoBehaviour
{
    
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonFun);
    }

    void ButtonFun()
    {
        switch(gameObject.name)
        {
            case "level1":
                LevelData.instance.currentLevel = 1;
                break;
            case "level2":
                LevelData.instance.currentLevel = 2;
                break;
            case "level3":
                LevelData.instance.currentLevel = 3;
                break;
            case "level4":
                LevelData.instance.currentLevel = 4;
                break;
            case "level5":
                LevelData.instance.currentLevel = 5;
                break;
            case "level6":
                LevelData.instance.currentLevel = 6;
                break;
            case "level7":
                LevelData.instance.currentLevel = 7;
                break;
            case "level8":
                LevelData.instance.currentLevel = 8;
                break;
            case "level9":
                LevelData.instance.currentLevel = 9;
                break;
            case "level10":
                LevelData.instance.currentLevel = 10;
                break;
            case "level11":
                LevelData.instance.currentLevel = 11;
                break;
            case "level12":
                LevelData.instance.currentLevel = 12;
                break;
        }

        LevelManager.instance.ChangeScene();
    }
}
