using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
using DG.Tweening;

public class MapObject : MonoBehaviour
{
    //是否可滑动
    public bool IsMouseInMap = false;

    //滑动cd
    private float cd = 0.5f;
    private float temptime = 0;
    public bool canMove = true;

    public bool isLock = false;

    //当前map的位置索引
    public int CurrentMapIndex = 0;

    //当前map的高度
    public float height = 1;
    //当前map高度变化
    public float LeftHeight = 0;
    public float RightHeight = 0;
    
    private void Awake()
    {
        InitCurrentMapIndex();
        FreshHeight();
        FreshLock();
    }

    private void Update()
    {
        if(canMove==false)
        temptime += Time.deltaTime;
        if (temptime > cd)
            canMove = true;

        Gesture currentgesture = EasyTouch.current;

            if (IsMouseInMap && currentgesture != null)
            {
                if (currentgesture.type == EasyTouch.EvtType.On_Swipe)
                {
                //Debug.Log(currentgesture.type);
                if (isLock == true)
                {
                    //Debug.Log("lock");
                    return;
                }
                    

                    if (canMove)
                    {
                        if (Player.instance.isInMap == false)
                        {
                            //Debug.Log("can't move");
                            return;
                        }


                        temptime = 0;

                        if (currentgesture.swipe == EasyTouch.SwipeDirection.Up)
                        {
                            MoveUp();
                        }
                        else if (currentgesture.swipe == EasyTouch.SwipeDirection.Down)
                        {
                            MoveDown();
                        }
                        else if (currentgesture.swipe == EasyTouch.SwipeDirection.Left)
                        {
                            MoveLeft();
                        }
                        else if (currentgesture.swipe == EasyTouch.SwipeDirection.Right)
                        {
                            MoveRight();
                        }
                    }
                }
        }
        
    }

    //初始化高度数据
    private void FreshHeight()
    {
        if (gameObject.transform.Find("stairup"))
        {
            LeftHeight = 0;
            RightHeight = 1;
        }

        if (gameObject.transform.Find("stairdown"))
        {
            LeftHeight = 1;
            RightHeight = 0;
        }

        if(gameObject.transform.Find("ground2"))
        {
            height = 2;
        }
    }

    //初始化锁数据
   private void FreshLock()
    {
        if (gameObject.transform.Find("lock"))
        {
            isLock = true;
        }
        else
            isLock = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            collision.transform.parent = transform;
            Player.instance.oldParent = Player.instance.newParent;
            Player.instance.newParent = transform;
            MapManager.instance.CurrentPlayerIndex = CurrentMapIndex;

            MapManager.instance.ResetPlayerLineWall();

            if(Player.instance.isThun==true)
            {
                isLock = false;
                Player.instance.isThun = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player.instance.isInMap = true;

            //Player.instance.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Player.instance.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player.instance.isInMap = false;

        //Player.instance.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        Player.instance.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void OnMouseEnter()
    {
        IsMouseInMap = true;
        //Debug.Log("true" + gameObject.name);
        //IsMouseInMap = false;
    }

    //private void OnMouseDrag()
    //{
    //    IsMouseInMap = true;
    //    Debug.Log("true" + gameObject.name);        
    //}

    private void OnMouseExit()
    {
        StartCoroutine(ChangeIsMouseInMap());
        //Debug.Log("false" + gameObject.name);
    }

    IEnumerator ChangeIsMouseInMap()
    {
        yield return new WaitForSeconds(0.05f);
        IsMouseInMap = false;
    }

    //初始化当前地图的索引
    private void InitCurrentMapIndex()
    {
        if (gameObject.name == "map0")
            CurrentMapIndex = 0;
        else if (gameObject.name == "map1")
            CurrentMapIndex = 1;
        else if (gameObject.name == "map2")
            CurrentMapIndex = 2;
        else if (gameObject.name == "map3")
            CurrentMapIndex = 3;
        else if (gameObject.name == "map4")
            CurrentMapIndex = 4;
        else if (gameObject.name == "map5")
            CurrentMapIndex = 5;
        else if (gameObject.name == "map6")
            CurrentMapIndex = 6;
        else if (gameObject.name == "map7")
            CurrentMapIndex = 7;
        else if (gameObject.name == "map8")
            CurrentMapIndex = 8;
    }

    public void MoveUp()
    {
        int tempInindex = CurrentMapIndex;

        if(tempInindex>2)
        {
            tempInindex = CurrentMapIndex - 3;

            if (MapManager.instance.MapGameObject[tempInindex] == null)
            {
                //Debug.Log("from "+ CurrentMapIndex+" to "+tempInindex);

                MapManager.instance.MapGameObject[CurrentMapIndex] = null;
                //Debug.Log(MapManager.instance.MapGameObject[CurrentMapIndex]);

                MapManager.instance.MapGameObject[tempInindex] = this.gameObject;
                CurrentMapIndex = tempInindex;

                MapManager.instance.CurrentPlayerIndex = Player.instance.transform.parent.GetComponent<MapObject>().CurrentMapIndex;
                //if(Player.instance.oldParent==Player.instance.newParent)
                //    MapManager.instance.CurrentPlayerIndex = CurrentMapIndex;

                

                gameObject.transform.DOMove(MapManager.instance.MapPos[CurrentMapIndex], 1f, false);
                canMove = false;

                Player.instance.PauseWhenMoveMap();

                MapManager.instance.ResetAllWall();
                MapManager.instance.ResetPlayerLineWall();               
            }
        }                    
        
    }

    public void MoveDown()
    {
        int tempInindex = CurrentMapIndex;

        if (tempInindex <6)
        {
            tempInindex = CurrentMapIndex + 3;

            if (MapManager.instance.MapGameObject[tempInindex] == null)
            {
                MapManager.instance.MapGameObject[CurrentMapIndex] = null;
                MapManager.instance.MapGameObject[tempInindex] = this.gameObject;
                CurrentMapIndex = tempInindex;

                MapManager.instance.CurrentPlayerIndex = Player.instance.transform.parent.GetComponent<MapObject>().CurrentMapIndex;
                //if (Player.instance.oldParent == Player.instance.newParent)
                //    MapManager.instance.CurrentPlayerIndex = CurrentMapIndex;

                //Debug.Log("movedown");

                gameObject.transform.DOMove(MapManager.instance.MapPos[CurrentMapIndex], 1f, false);
                canMove = false;

                Player.instance.PauseWhenMoveMap();

                MapManager.instance.ResetAllWall();
                MapManager.instance.ResetPlayerLineWall();

                
            }
        }      
    }

    public void MoveLeft()
    {
        int tempInindex = CurrentMapIndex;

        if (tempInindex!=0&& tempInindex != 3&& tempInindex != 6)
        {
            tempInindex = CurrentMapIndex - 1;

            if (MapManager.instance.MapGameObject[tempInindex] == null)
            {
                MapManager.instance.MapGameObject[CurrentMapIndex] = null;
                MapManager.instance.MapGameObject[tempInindex] = this.gameObject;
                CurrentMapIndex = tempInindex;

                MapManager.instance.CurrentPlayerIndex = Player.instance.transform.parent.GetComponent<MapObject>().CurrentMapIndex;
                //if (Player.instance.oldParent == Player.instance.newParent)
                //    MapManager.instance.CurrentPlayerIndex = CurrentMapIndex;

                //Debug.Log("moveup");

                gameObject.transform.DOMove(MapManager.instance.MapPos[CurrentMapIndex], 1f, false);
                canMove = false;

                Player.instance.PauseWhenMoveMap();

                MapManager.instance.ResetAllWall();
                MapManager.instance.ResetPlayerLineWall();

                
            }
        }
    }

    public void MoveRight()
    {
        int tempInindex = CurrentMapIndex;

        if (tempInindex != 2 && tempInindex != 5 && tempInindex != 8)
        {
            tempInindex = CurrentMapIndex + 1;

            if (MapManager.instance.MapGameObject[tempInindex] == null)
            {
                MapManager.instance.MapGameObject[CurrentMapIndex] = null;
                MapManager.instance.MapGameObject[tempInindex] = this.gameObject;
                CurrentMapIndex = tempInindex;

                MapManager.instance.CurrentPlayerIndex = Player.instance.transform.parent.GetComponent<MapObject>().CurrentMapIndex;
                //if (Player.instance.oldParent == Player.instance.newParent)
                //    MapManager.instance.CurrentPlayerIndex = CurrentMapIndex;

                //Debug.Log("moveup");

                gameObject.transform.DOMove(MapManager.instance.MapPos[CurrentMapIndex], 1f, false);
                canMove = false;

                Player.instance.PauseWhenMoveMap();

                MapManager.instance.ResetAllWall();
                MapManager.instance.ResetPlayerLineWall();

                
            }
        }
    }


}
