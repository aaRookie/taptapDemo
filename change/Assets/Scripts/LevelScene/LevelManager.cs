using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    private void Awake()
    {
        instance = this;
        
        
    }

    public void Start()
    {
        UnlockLevel();
        m_quit.onClick.AddListener(QuitFun);
    }


    public Button[] levelbutton = null;

    public Button m_quit;

    public void QuitFun()
    {
        Application.Quit();
    }


    //解锁关卡
    public void UnlockLevel()
    {

        for (int i = 0; i < levelbutton.Length; i++)
        {
            levelbutton[i].interactable = false;
        }

        for (int i = 0; i < LevelData.instance.maxLevel; i++)
        {
            levelbutton[i].interactable = true;
        }
        for (int i = LevelData.instance.maxLevel; i < levelbutton.Length; i++)
        {
            levelbutton[i].interactable = false;
        }
    }

    //切换场景
    public void ChangeScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    
}
