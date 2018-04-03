using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Team_Win : MonoBehaviour
{
    public Slider win_engery_purple;
    public Slider win_engery_orange;
    public GameObject finish_area;
    public float win_finish = 5.0f;
    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	public void TeamWinUpdate ()
    {
        if (finish_area.GetComponent<Finish_area>()._iOrangeNum == 2)
        {
            
            win_engery_orange.value += win_finish*Time.deltaTime;
        }
        if (win_engery_orange.value >= 100)
        {
            finish_area.GetComponent<Finish_area>()._bWin = true;
            finish_area.GetComponent<Finish_area>().winTeam = Player.AllTeam.OrangeTeam;
        }


        if(finish_area.GetComponent<Finish_area>()._iPurpleNum == 2)
        {
            //print(1);
            win_engery_purple.value += win_finish * Time.deltaTime;
        }
        if (win_engery_purple.value >= 100)
        {
            finish_area.GetComponent<Finish_area>()._bWin = true;
            finish_area.GetComponent<Finish_area>().winTeam = Player.AllTeam.PurpleTeam;
        }
    }
}
