using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelData : MonoBehaviour
{
    public static LevelData instance = null;

    //当前关卡
    public int currentLevel = 1;

    //已解锁关卡
    public int maxLevel = 1;

    private void Awake()
    {
        if(instance==null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        
        else
        {
            Destroy(gameObject);
        }
        
    }


}
