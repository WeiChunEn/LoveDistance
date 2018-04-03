using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootAndHand : MonoBehaviour
{
    public Player playerMySelf = null;

    //public delegate void StampSomeone(bool _bSameTeam);
    //public StampSomeone stampSomeone = null;
    public delegate void OnTheGround(bool _bOnTheGround);
    public OnTheGround onTheGround = null;

    public enum AllLimbs
    {
        none,
        Foot,
        Hand
    }
    public AllLimbs allLimbs = AllLimbs.none;
    public Player.AllTeam _myTeam = Player.AllTeam.none;

    private BoxCollider _tHand = null;

    [SerializeField]
    private bool _bTellFinishAreaMyTeam = false;

    [SerializeField]
    private float _fHighFiveTiming = 0.0f;
    [SerializeField]
    private float _fHighFiveTime = 1.5f;
    
    //void OnTriggerEnter(Collider col)
    //{
    //    switch (allLimbs)
    //    {
    //        case AllLimbs.Foot:
    //            switch (col.tag)
    //            {
    //                case "Plane":
    //                    if (onTheGround != null)
    //                    {
    //                        onTheGround(true);
    //                    }
    //                    break;
    //                case "Finish":
    //                    Finish_area finishArea = col.GetComponentInParent<Finish_area>();
    //                    finishArea.finish = true;
    //                    break;
    //                case "OrangeTeam":
    //                    Player otherPlayer = col.GetComponent<Player>();
    //                    //同隊
    //                    if (_myTeam == otherPlayer._myTeam)
    //                    {
    //                        playerMySelf.StampSuccessful(true);
    //                    }
    //                    //不同隊
    //                    else
    //                    {
    //                        otherPlayer.StampSuccessful(false);
    //                    }
    //                    break;
    //                case "PurpleTeam":
    //                    Player theOtherPlayer = col.GetComponent<Player>();
    //                    //同隊
    //                    if (_myTeam == theOtherPlayer._myTeam)
    //                    {
    //                        playerMySelf.StampSuccessful(true);
    //                    }
    //                    //不同隊
    //                    else
    //                    {
    //                        theOtherPlayer.StampSuccessful(false);
    //                    }
    //                    break;
    //            }
    //            break;
    //    }
    //}
    void OnTriggerStay(Collider col)
    {
        switch (allLimbs)
        {
            case AllLimbs.Foot:
                switch (col.tag)
                {
                    case "Plane":
                        if (onTheGround != null)
                        {
                            onTheGround(true);
                        }
                        break;
                    case "Finish":
                        Finish_area finishArea = col.GetComponentInParent<Finish_area>();
                        finishArea.finish = true;
                        if (!_bTellFinishAreaMyTeam)
                        {
                            switch (_myTeam)
                            {
                                case Player.AllTeam.OrangeTeam:
                                    finishArea._iOrangeNum += 1;
                                    break;
                                case Player.AllTeam.PurpleTeam:
                                    finishArea._iPurpleNum += 1;
                                    break;
                            }
                            _bTellFinishAreaMyTeam = true;
                        }
                        break;
                    case "OrangeTeam":
                        Player otherPlayer = col.GetComponent<Player>();
                        //同隊
                        if (_myTeam == otherPlayer._myTeam)
                        {
                            playerMySelf.StampSuccessful(true);
                        }
                        //不同隊
                        else
                        {
                            otherPlayer.StampSuccessful(false);
                            playerMySelf.StampSuccessful(true);
                        }
                        break;
                    case "PurpleTeam":
                        Player theOtherPlayer = col.GetComponent<Player>();
                        //同隊
                        if (_myTeam == theOtherPlayer._myTeam)
                        {
                            playerMySelf.StampSuccessful(true);
                        }
                        //不同隊
                        else
                        {
                            theOtherPlayer.StampSuccessful(false);
                            playerMySelf.StampSuccessful(true);
                        }
                        break;
                    case "Trap":
                        if (onTheGround != null)
                        {
                            onTheGround(true);
                        }
                        //Trap_Plane.on = true;
                        Trap_Plane trapPlane = col.GetComponentInParent<Trap_Plane>();
                        trapPlane.on = true;
                        break;
                }
                break;
            case AllLimbs.Hand:
                if (_tHand == null)
                {
                    _tHand = gameObject.GetComponent<BoxCollider>();
                }
                switch (col.tag)
                {
                    case "OrangeTeam":
                        FootAndHand otherPlayer = col.GetComponent<FootAndHand>();
                        //同隊
                        if (_myTeam == otherPlayer._myTeam)
                        {
                            _fHighFiveTiming += Time.deltaTime;
                            if (_fHighFiveTiming >= _fHighFiveTime)
                            {
                                _fHighFiveTiming = 0.0f;
                                _tHand.isTrigger = false;
                                playerMySelf.StampSuccessful(true);
                            }
                        }
                        break;
                    case "PurpleTeam":
                        FootAndHand theOtherPlayer = col.GetComponent<FootAndHand>();
                        //同隊
                        if (_myTeam == theOtherPlayer._myTeam)
                        {
                            _fHighFiveTiming += Time.deltaTime;
                            if (_fHighFiveTiming >= _fHighFiveTime)
                            {
                                _fHighFiveTiming = 0.0f;
                                _tHand.isTrigger = false;
                                playerMySelf.StampSuccessful(true);
                            }
                        }
                        break;
                }
                break;
        }
    }
    void OnTriggerExit(Collider col)
    {
        switch (allLimbs)
        {
            case AllLimbs.Foot:
                switch (col.tag)
                {
                    case "Plane":
                        if (onTheGround != null)
                        {
                            onTheGround(false);
                        }
                        break;
                    case "Finish":
                        Finish_area finishArea = col.GetComponentInParent<Finish_area>();
                        finishArea.finish = false;
                        if (_bTellFinishAreaMyTeam)
                        {
                            switch (_myTeam)
                            {
                                case Player.AllTeam.OrangeTeam:
                                    finishArea._iOrangeNum -= 1;
                                    break;
                                case Player.AllTeam.PurpleTeam:
                                    finishArea._iPurpleNum -= 1;
                                    break;
                            }
                            _bTellFinishAreaMyTeam = false;
                        }
                        break;
                }
                break;
            case AllLimbs.Hand:
                if (_tHand == null)
                {
                    _tHand = gameObject.GetComponent<BoxCollider>();
                }
                switch (col.tag)
                {
                    case "OrangeTeam":
                        FootAndHand otherPlayer = col.GetComponent<FootAndHand>();
                        //同隊
                        if (_myTeam == otherPlayer._myTeam)
                        {
                            _fHighFiveTiming = 0.0f;
                            _tHand.isTrigger = true;
                        }
                        break;
                    case "PurpleTeam":
                        FootAndHand theOtherPlayer = col.GetComponent<FootAndHand>();
                        //同隊
                        if (_myTeam == theOtherPlayer._myTeam)
                        {
                            _fHighFiveTiming = 0.0f;
                            _tHand.isTrigger = true;
                        }
                        break;
                }
                break;
        }
    }
}
