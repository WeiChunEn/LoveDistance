using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Plane : MonoBehaviour
{

    public bool on;
    public float _fCloseTime = 0.0f;
    public float _fCloseEndTime = 5.0f;
    public float _fOpenTime = 0.0f;
    public float _fOpenEndTime = 2.0f;
    public float tmp = 0.5f;
    public Color Colud_color = new Color(1, 1, 1, 1);
    public Color MyColor;
    bool _bClose = false;
    GameObject _gCol = null;
    GameObject _gImg = null;

    // Use this for initialization
    void Start()
    {
        on = false;
        _gCol = GetComponentInChildren<BoxCollider>().gameObject;
        _gImg = GetComponentInChildren<SpriteRenderer>().gameObject;
        MyColor = _gImg.GetComponentInChildren<SpriteRenderer>().color;
        Colud_color.a = 1;
        MyColor.a = Colud_color.a;
    }

    // Update is called once per frame
    void Update()
    {
        //print(on);
        if (on == true && _fCloseTime < _fCloseEndTime)
        {
            _fOpenTime += Time.deltaTime;
            Colud_color.a -= tmp * Time.deltaTime;
            MyColor = _gImg.GetComponentInChildren<SpriteRenderer>().color;
            MyColor.a = Colud_color.a;

            if (_fOpenTime > _fOpenEndTime)
            {

                Colud_color.a = 0.0f;
                MyColor.a = Colud_color.a;
                if (!_bClose)
                {
                    _bClose = true;
                    _gCol.SetActive(false);
                    _gImg.SetActive(false);
                }
                _fCloseTime += Time.deltaTime;

            }
        }
        else
        {
            Colud_color.a = 1.0f;
            _fOpenTime = 0.0f;
            _fCloseTime = 0.0f;
            on = false;
            if (_bClose)
            {
                _bClose = false;
                _gCol.SetActive(true);
                _gImg.SetActive(true);
            }
        }
    }

}
