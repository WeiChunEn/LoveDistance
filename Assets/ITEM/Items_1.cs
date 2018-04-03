using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items_1 : MonoBehaviour
{
    bool _bTouch;
    public int num;
    public int tmp;
    public float starttime = 0.0f;
    public float appeartime = 10.0f;
    // Use this for initialization
    void Start()
    {
        _bTouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        addtime();
    }

    void appear(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == index)
            {

                transform.GetChild(i).gameObject.SetActive(true);


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

            appear(num);

            starttime = 0.0f;
        }
    }
}
