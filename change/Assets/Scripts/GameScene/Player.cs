using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance=null;

    public Transform m_player;

    public Transform oldParent;
    public Transform newParent;

    public bool isInMap = true;

    public bool canChange = true;

    public float temptime = 0;
    public float changeCd = 0.2f;

    //0.5f
    public float speed = 3f;

    public int SpeedPause = 1;

    public bool isFire = false;
    public bool isWind = false;
    public bool isThun = false;

    private void Awake()
    {
        instance = this;
    }

    //变向
    public void ChangeSpeed()
    {
        if(canChange)
        {
            speed = -speed;
            canChange = false;
            //Debug.Log("change");
        }
              
    }

    //速度暂停
    public void ChangePauseSpeed()
    {
        if (SpeedPause == 1)
        {
            SpeedPause = 0;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }          
        else
        {
            SpeedPause = 1;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }          
    }

    public void PauseWhenMoveMap()
    {
        ChangePauseSpeed();
        StartCoroutine(EndPause());
    }

    IEnumerator EndPause()
    {
        yield return new WaitForSeconds(0.7f);
        ChangePauseSpeed();
    }


    //角色移动
    public void PlayerMove()
    {
        m_player.transform.position += new Vector3(speed * Time.deltaTime* SpeedPause, 0, 0);
    }

    //角色死亡
    public void PlayerDead()
    {
        ChangePauseSpeed();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("dead");
    }

    
    void Update()
    {
        PlayerMove();

        if(canChange==false)
        {
            temptime += Time.deltaTime;
            if(temptime>changeCd)
            {
                temptime = 0;
                canChange = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //开关
        if (collision.transform.name == "switch")
        {
            //Debug.Log("switch");
            MapManager.instance.ChangeSwitch();
        }

        else if(collision.transform.name=="Fire")
        {
            isFire = true;
            Destroy(collision.gameObject);
        }
        else if(collision.transform.name == "Thun")
        {
            isThun = true;
            Destroy(collision.gameObject);
        }
        else if (collision.transform.name == "Wind")
        {
            isWind = true;
            Destroy(collision.gameObject);
        }
    }

    //碰撞检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //碰到网格左右边界
        if (collision.transform.name == "mapleft")
        {
            //Debug.Log("mapleft");
            ChangeSpeed();
        }
        else if (collision.transform.name == "mapright")
        {
            //Debug.Log("mapright");
            ChangeSpeed();
        }

        //碰到墙壁
        else if(collision.transform.name=="wall")
        {
            //Debug.Log("wall");
            ChangeSpeed();
        }

        //碰到楼梯
        else if (collision.transform.name == "stairwall")
        {
            Debug.Log("stair");
            ChangeSpeed();
        }

        //碰到box
        else if (collision.transform.name == "boxwall")
        {
            Debug.Log("boxwall");
            ChangeSpeed();
        }    
        
        //碰到刺
        else if (collision.transform.name == "ci")
        {
            if(isFire==true)
            {
                Destroy(collision.gameObject);
                isFire = false;
            }
            else
            {
                PlayerDead();
            }          
        }

        //终点
        else if(collision.transform.name=="end")
        {
            Destroy(collision.gameObject);
            ChangePauseSpeed();
            GameUIManager.instance.EndSuccess();            
        }
    }


}
