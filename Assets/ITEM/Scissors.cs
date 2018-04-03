using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : MonoBehaviour
{
    
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void OnTriggerEnter(Collider col)
    {
        print("789");
        if (col.tag == "OrangeTeam" || col.tag == "PurpleTeam" )
        {
            transform.parent.gameObject.SetActive(false);
                Player get_team = col.GetComponent<Player>();
            switch (get_team._myTeam)
            {
                case Player.AllTeam.none:
                    break;
                case Player.AllTeam.OrangeTeam:
                    if (Defense._iPurple_Engery <= 0)
                    {
                        get_team._gOtherTeam.StampSuccessful(true);
                        get_team._gOtherTeam._gTeamMate.StampSuccessful(true);
                    }
                    break;
                case Player.AllTeam.PurpleTeam:
                    if (Defense._iOrange_Engery <= 0)
                    {
                        get_team._gOtherTeam.StampSuccessful(true);
                        get_team._gOtherTeam._gTeamMate.StampSuccessful(true);
                    }
                    break;
            }
            //if (Defense._iOrange_Engery <= 0 || Defense._iPurple_Engery <= 0)
            //{
            //    print("852");
            //    get_team._gOtherTeam.StampSuccessful(true);
            //    get_team._gOtherTeam._gTeamMate.StampSuccessful(true);
            //}
           

        }
        


    }

}
