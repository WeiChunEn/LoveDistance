using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleRole : MonoBehaviour
{

    public delegate void LostTeamMate();
    public LostTeamMate lostTeamMate = null;

    [SerializeField]
    private LineRenderer _lineRender = null;
    [SerializeField]
    private Transform _pPlayerMySelf = null;
    [SerializeField]
    private Transform _pPlayerTeamMate = null;
    [SerializeField]
    private Vector3 _v3TeamDistance = new Vector3(0, 0, 0);

    [SerializeField]
    private float _fDepth = -2.0f;
    public float _fTeamDistance = 0.0f;
    [SerializeField]
    private float _fMaxTeamDistance = 7.0f;

    void Start()
    {
    }

    void Update()
    {
        _lineRender.SetPosition(0, new Vector3(_pPlayerMySelf.position.x, _pPlayerMySelf.position.y, _fDepth));
        _lineRender.SetPosition(1, new Vector3(_pPlayerTeamMate.position.x, _pPlayerTeamMate.position.y, _fDepth));
        _v3TeamDistance = _pPlayerMySelf.position - _pPlayerTeamMate.position;
        _fTeamDistance = Mathf.Sqrt(Mathf.Pow(_v3TeamDistance.x, 2) + Mathf.Pow(_v3TeamDistance.y, 2));
        if (_fTeamDistance > _fMaxTeamDistance)
        {
            if (lostTeamMate != null)
            {
                lostTeamMate();
            }
        }
    }
}
