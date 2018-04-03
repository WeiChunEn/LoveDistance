using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Finish_area : MonoBehaviour
{
    public int num;
    public float starttime = 0.0f;
    public float appeartime = 10.0f;
    int tmp;
    public int _iOrangeNum;
    public int _iPurpleNum;
    public bool finish;
    
    public bool _bWin = false;
    public Player.AllTeam winTeam = Player.AllTeam.none;
    // Use this for initialization
    void Start ()
    {
        _iOrangeNum = 0;
        _iPurpleNum = 0;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(finish)
        {
            starttime = 0.0f;
        }
        else
        {
            addtime();
        }
	}
    void appear_area(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == index )
            {
                transform.GetChild(i).gameObject.SetActive(true);
                tmp = num;

            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void addtime()
    {
        starttime += Time.deltaTime;
        if (starttime >= appeartime)
        {
            num = Random.Range(0, 4);
            if (num == tmp)
            {
                num = num + 1;
                appear_area(num);
            }
            else
            {
                appear_area(num);
            }

            starttime = 0.0f;
        }
    }


}
