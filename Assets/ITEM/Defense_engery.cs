using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Defense_engery : MonoBehaviour
{
    public Slider Orange_Defense;
    public Slider Purple_Defense;
    [SerializeField]
    private float Orange_Engery;
    [SerializeField]
    private float Purple_Engery;
    private float _fDecrease = 10.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Defense.flag1 == 1)
        {
            Orange_Engery = Defense._iOrange_Engery;

            Defense.flag1 = 0;
        }
        if (Defense.flag2 == 1)
        {

            Purple_Engery = Defense._iPurple_Engery;
            Defense.flag2 = 0;
        }



        Orange_Defense.value = Orange_Engery;
        Purple_Defense.value = Purple_Engery;
        if (Orange_Engery > 0)
        {
            Orange_Engery -= _fDecrease * Time.deltaTime;
            if (Orange_Engery < 0)
            {
                Orange_Engery = 0.0f;
            }
        }
        if (Purple_Engery > 0)
        {
            Purple_Engery -= _fDecrease * Time.deltaTime;
            if (Purple_Engery < 0)
            {
                Purple_Engery = 0.0f;
            }
        }



        // Orange_Defense.value -= _fDecrease * Time.deltaTime;
        //   Purple_Defense.value -= _fDecrease * Time.deltaTime;
    }
}
