using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance = null;

    public Button m_replay;
    public Button m_return;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        m_replay.onClick.AddListener(replayFun);
        m_return.onClick.AddListener(returnFun);
    }
    
    void Update()
    {
        
    }

    //到达终点
    public void EndSuccess()
    {
        Debug.Log("win");
        UnlockNextLevel();
    }

    //解锁关卡
    public void UnlockNextLevel()
    {
        LevelData.instance.maxLevel ++;
        StartCoroutine(ChangeScene());
    }

    //返回关卡界面
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    //重载当前关卡
    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    //返回关卡界面
    public void returnFun()
    {
        StartCoroutine(ChangeScene());
    }

    //重玩本关
    public void replayFun()
    {
        StartCoroutine(ReloadScene());
    }
}
