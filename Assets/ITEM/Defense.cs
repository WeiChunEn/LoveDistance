using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Defense : MonoBehaviour
{
    
    
    static public float _iOrange_Engery;
    
    static public float _iPurple_Engery;
    static public int flag1= 0;
    static public int flag2 = 0;
   
    // Use this for initializaton
    void Start()
    {
        _iOrange_Engery = 0.0f;
        _iPurple_Engery = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "OrangeTeam" || col.tag == "PurpleTeam")
        {
            
            transform.parent.gameObject.SetActive(false);
            if(col.tag == "OrangeTeam")
            {
               _iOrange_Engery = 100;
                flag1++;
            }
            if (col.tag == "PurpleTeam")
            {
                _iPurple_Engery = 100;
                flag2++;
            }
            


        }
        


    }

}
