using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance=null;

    private void Awake()
    {
        instance = this;
    }

    public List<Vector2> MapPos = new List<Vector2>();
    public List<GameObject> LevelMap = new List<GameObject>();
    public List<GameObject> MapGameObject = new List<GameObject>();

    public GameObject[] BoxWallGameObject=null;

    private bool currentswitch = true;

    public GameObject m_player;

    //int[] level1map = new int[9]{1,1,1,0,0,0,0,0,0};
    //int[] level2map = new int[9];
    //int[] level3map = new int[9];
    //int[] level4map = new int[9];
    //int[] level5map = new int[9];
    //int[] level6map = new int[9];

    public int CurrentPlayerIndex = 0;
    public int[] CurrentMap;

    void Start()
    {
        Init();
    }

    void Init()
    {
        LoadCurrentMap();
        LoadMapGameObject();

        ResetAllWall();
        ResetPlayerLineWall();
    }

    public void ChangeSwitch()
    {
        currentswitch =!currentswitch;

        //关闭障碍
        if (currentswitch==false)
        {
            for (int i = 0; i < BoxWallGameObject.Length; i++)
            {
                if(BoxWallGameObject[i])
                {
                    BoxWallGameObject[i].SetActive(false);
                }
            }
        }
        //开启障碍
        if(currentswitch==true)
        {
            for (int i = 0; i < BoxWallGameObject.Length; i++)
            {
                if (BoxWallGameObject[i])
                {
                    BoxWallGameObject[i].SetActive(true);
                }
            }
        }
    }

    //初始化地图数据
    void LoadCurrentMap()
    {
        switch(LevelData.instance.currentLevel)
        {
            case 1:
                LevelMap[0].SetActive(true);
                break;
            case 2:
                LevelMap[1].SetActive(true);
                break;
            case 3:
                LevelMap[2].SetActive(true);
                break;
            case 4:
                LevelMap[3].SetActive(true);
                break;
            case 5:
                LevelMap[4].SetActive(true);
                break;
            case 6:
                LevelMap[5].SetActive(true);
                break;
            case 7:
                LevelMap[6].SetActive(true);
                break;
            case 8:
                LevelMap[7].SetActive(true);
                break;
            case 9:
                LevelMap[8].SetActive(true);
                break;
            case 10:
                LevelMap[9].SetActive(true);
                break;
            case 11:
                LevelMap[10].SetActive(true);
                break;
            case 12:
                LevelMap[11].SetActive(true);
                break;
            default:
                LevelMap[0].SetActive(true);
                break;
        }
        
    }

    //初始化map的gameobject
    void LoadMapGameObject()
    {
        MapGameObject[0] = GameObject.Find("map0");
        MapGameObject[1] = GameObject.Find("map1");
        MapGameObject[2] = GameObject.Find("map2");
        MapGameObject[3] = GameObject.Find("map3");
        MapGameObject[4] = GameObject.Find("map4");
        MapGameObject[5] = GameObject.Find("map5");
        MapGameObject[6] = GameObject.Find("map6");
        MapGameObject[7] = GameObject.Find("map7");
        MapGameObject[8] = GameObject.Find("map8");

        BoxWallGameObject = GameObject.FindGameObjectsWithTag("boxwall");

        for (int i = 0; i < MapGameObject.Count; i++)
        {
            if (MapGameObject[i] && MapGameObject[i].tag=="playerpos")
            {
                CurrentPlayerIndex = i;
                break;
            }
                
        }

        m_player.SetActive(true);
        m_player.transform.position = MapGameObject[CurrentPlayerIndex].transform.position;

        Player.instance.transform.parent = MapGameObject[CurrentPlayerIndex].transform;
        Player.instance.oldParent = MapGameObject[CurrentPlayerIndex].transform;
        Player.instance.newParent = MapGameObject[CurrentPlayerIndex].transform;
    }   

    //判断能有移动地图
    public bool ChangeCanMove(int temp)
    {
        if (MapGameObject[temp] == null)
            return true;
        else return false;
    }

    //更改角色父物体
    public void ChangePlayerParent(int temp)
    {
        Player.instance.transform.parent = MapGameObject[temp].transform;

        Player.instance.oldParent = Player.instance.newParent;
        Player.instance.newParent= MapGameObject[temp].transform;
        //ResetPlayerLineWall();
    }

    //重置map左右墙
    public void ResetAllWall()
    {
        for (int i = 0; i < MapGameObject.Count; i++)
        {
            if(MapGameObject[i])
            {
                MapGameObject[i].transform.Find("mapright").GetComponent<BoxCollider2D>().enabled = true;
                MapGameObject[i].transform.Find("mapleft").GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }


    //移动时，控制map左右墙
    public void ResetPlayerLineWall()
    {
        int tempindex = CurrentPlayerIndex % 3;

        //左行
        if (tempindex!=0)
        {
            if(MapGameObject[CurrentPlayerIndex-1]!=null)
            {           
                if(CanMoveIn(CurrentPlayerIndex, CurrentPlayerIndex - 1))
                {
                    MapGameObject[CurrentPlayerIndex - 1].transform.Find("mapright").GetComponent<BoxCollider2D>().enabled = false;
                    MapGameObject[CurrentPlayerIndex].transform.Find("mapleft").GetComponent<BoxCollider2D>().enabled = false;
                }
                                          
            }

            if(tempindex==2)
            {
                if(MapGameObject[CurrentPlayerIndex - 2]!=null&& MapGameObject[CurrentPlayerIndex - 1] != null)
                {
                    if(CanMoveIn(CurrentPlayerIndex-1, CurrentPlayerIndex - 2))
                    {
                        MapGameObject[CurrentPlayerIndex - 2].transform.Find("mapright").GetComponent<BoxCollider2D>().enabled = false;
                        MapGameObject[CurrentPlayerIndex - 1].transform.Find("mapleft").GetComponent<BoxCollider2D>().enabled = false;
                    }
                    
                }               
            }
        }

        //右行
        if(tempindex!=2)
        {
            if (MapGameObject[CurrentPlayerIndex + 1] != null)
            {
                if (CanMoveIn(CurrentPlayerIndex, CurrentPlayerIndex + 1))
                {
                    //Debug.Log("can move");
                    MapGameObject[CurrentPlayerIndex + 1].transform.Find("mapleft").GetComponent<BoxCollider2D>().enabled = false;
                    MapGameObject[CurrentPlayerIndex].transform.Find("mapright").GetComponent<BoxCollider2D>().enabled = false;
                }
                //else
                    
                
            }

            if (tempindex == 0)
            {
                if (MapGameObject[CurrentPlayerIndex + 2] != null&& MapGameObject[CurrentPlayerIndex + 1] != null)
                {
                    if(CanMoveIn(CurrentPlayerIndex+1, CurrentPlayerIndex + 2))
                    {
                        MapGameObject[CurrentPlayerIndex + 2].transform.Find("mapleft").GetComponent<BoxCollider2D>().enabled = false;
                        MapGameObject[CurrentPlayerIndex + 1].transform.Find("mapright").GetComponent<BoxCollider2D>().enabled = false;
                    }                   
                }                    
            }
        }
    }

    //从a到b的高度差判定
    private bool CanMoveIn(int a,int b)
    {

        //高度差判定
        float aleft = MapGameObject[a].GetComponent<MapObject>().height+ MapGameObject[a].GetComponent<MapObject>().LeftHeight;
        float aright = MapGameObject[a].GetComponent<MapObject>().height + MapGameObject[a].GetComponent<MapObject>().RightHeight;

        float bleft = MapGameObject[b].GetComponent<MapObject>().height + MapGameObject[b].GetComponent<MapObject>().LeftHeight;
        float bright = MapGameObject[b].GetComponent<MapObject>().height + MapGameObject[b].GetComponent<MapObject>().RightHeight;

        //Debug.Log(a+" "+aleft + " " + aright);
        //Debug.Log(b + " " + bleft + " " + bleft);

        if (a > b)
        {
            if (aleft == bright)
                return true;
            else
                return false;
        }
        else if (a < b)
        {
            if (aright == bleft)
                return true;
            else
                return false;
        }
        else return false;

    }
}
