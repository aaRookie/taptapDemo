using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutMap : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.name=="player")
        {
            Player.instance.isInMap = false;
            //Debug.Log(Player.instance.isInMap+" "+Time.time);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            Player.instance.isInMap = true;
            //Debug.Log(Player.instance.isInMap + " " + Time.time);
        }
    }
}
