using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Back : MonoBehaviour
{

    private float _fMaxDistance;
    [SerializeField]
    private float _fGetbacking = 0.0f;
    [SerializeField]
    private float _fGetbacktime = 1.0f;
    [SerializeField]
    private bool _bflag = false;
    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        _fMaxDistance = gameObject.GetComponent<GrappleRole>()._fTeamDistance;

        if (_fMaxDistance > gameObject.GetComponent<SpringJoint>().maxDistance && _fGetbacking < _fGetbacktime)
        {


            gameObject.GetComponent<SpringJoint>().maxDistance = 1;
            gameObject.GetComponent<SpringJoint>().spring = 200;


            _fGetbacking += 1 * Time.deltaTime;





        }
        else
        {
            _fGetbacking = 0.0f;
            gameObject.GetComponent<SpringJoint>().maxDistance = 5;
            gameObject.GetComponent<SpringJoint>().spring = 10;
            _bflag = false;


            //_fGetbacking = 0.0f;
        }

    }
}
