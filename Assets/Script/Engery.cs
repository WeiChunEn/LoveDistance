using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Engery : MonoBehaviour
{

    public delegate void AllPlayersInReady();
    public AllPlayersInReady allPlayersInReady = null;

    public int _iplayer1;
    public int _iplayer2;
    public int _iplayer3;
    public int _iplayer4;
    public Slider engery;
    public bool _bstart;

    void Start()
    {
        ResetValue();
    }

    void Update()
    {
        engery.value = _iplayer1 + _iplayer2 + _iplayer3 + _iplayer4;
        if (Input.GetKey(KeyCode.W) && _iplayer1 < 25)
        {
            _iplayer1++;

        }
        if (Input.GetKey(KeyCode.I) && _iplayer2 < 25)
        {
            _iplayer2++;

        }
        if (Input.GetKey(KeyCode.UpArrow) && _iplayer3 < 25)
        {
            _iplayer3++;

        }
        if (Input.GetKey(KeyCode.Keypad8) && _iplayer4 < 25)
        {
            _iplayer4++;
        }
        if (engery.value >= 100.0f)
        {
            allPlayersInReady();
        }
    }
    public void ResetValue()
    {
        _iplayer1 = 0;
        _iplayer2 = 0;
        _iplayer3 = 0;
        _iplayer4 = 0;
        engery.value = 0.0f;
    }
}
