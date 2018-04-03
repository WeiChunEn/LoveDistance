using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FootAndHand foot = null;
    public FootAndHand hand = null;

    public enum AllPlayer
    {
        player0,
        player1,
        player2,
        player3,
        player4
    }
    public enum AllTeam
    {
        none,
        OrangeTeam,
        PurpleTeam
    }
    public AllPlayer _allPlayer = AllPlayer.player0;
    public AllTeam _myTeam = AllTeam.none;

    public Player _gTeamMate = null;
    public Player _gOtherTeam = null;

    private Rigidbody _rigi;

    private string _sPlayer = "";
    private string _sJump = "";

    public bool _bFacingRight = false;
    [SerializeField]
    private bool _bHeadache = false;
    [SerializeField]
    private bool _bKnockBack = false;
    [SerializeField]
    private bool _bCanJump = false;

    [SerializeField]
    private float _fHeadaching = 0.0f;
    [SerializeField]
    private float _fHeadacheTime = 3.0f;
    [SerializeField]
    private float _fKnocking = 0.0f;
    [SerializeField]
    private float _fKnockBackTime = 0.3f;
    [SerializeField]
    private float _fAxisX;
    [SerializeField]
    private float _fMoveforcex = 5.0f;
    [SerializeField]
    private float _fJumpforce = 8.0f;
    [SerializeField]
    private float _fKnockBackforce = 3.0f;

    void Awake()
    {
        //玩家設定
        switch (_allPlayer)
        {
            case AllPlayer.player0:
                break;
            case AllPlayer.player1:
                _sPlayer = "Player1";
                _sJump = "Jump1";
                _bFacingRight = true;
                break;
            case AllPlayer.player2:
                _sPlayer = "Player2";
                _sJump = "Jump2";
                _bFacingRight = true;
                break;
            case AllPlayer.player3:
                _sPlayer = "Player3";
                _sJump = "Jump3";
                gameObject.transform.Rotate(0, -180, 0);
                _bFacingRight = false;
                break;
            case AllPlayer.player4:
                _sPlayer = "Player4";
                _sJump = "Jump4";
                gameObject.transform.Rotate(0, -180, 0);
                _bFacingRight = false;
                break;
        }
        //設定手腳
        if (foot == null)
        {
            foot = gameObject.GetComponentsInChildren<FootAndHand>()[0];
        }
        if (hand == null)
        {
            hand = gameObject.GetComponentsInChildren<FootAndHand>()[1];
        }
        foot._myTeam = _myTeam;
        hand._myTeam = _myTeam;
        foot.onTheGround = CanJump;
        hand.onTheGround = CanJump;
        if (foot.playerMySelf == null)
        {
            foot.playerMySelf = this;
        }
        if (hand.playerMySelf == null)
        {
            hand.playerMySelf = this;
        }
        //foot.stampSomeone = StampSuccessful;
        //hand.stampSomeone = StampSuccessful;

        _rigi = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //撞到頭
        if (_bHeadache)
        {
            Headache();
        }
        //震飛
        if (_bKnockBack)
        {
            KnockBack();
        }
    }
    void FixedUpdate()
    {
        _fAxisX = Input.GetAxis(_sPlayer);
        if (!_bHeadache && !_bKnockBack)
        {
            //移動
            if (_fAxisX != 0.0f)
            {
                Move(true);
            }
            else
            {
                Move(false);
            }
            //跳躍
            if (_bCanJump && Input.GetButtonDown(_sJump))
            {
                Jump();
                _bCanJump = false;
            }
        }
    }
    //移動
    void Move(bool _bMoving)
    {
        if (_bMoving)
        {
            //方向控制
            if (!_bFacingRight && _fAxisX > 0.0f)
            {
                gameObject.transform.Rotate(0, -180, 0);
                //print("right");
                _bFacingRight = true;
            }
            else if (_bFacingRight && _fAxisX < 0.0f)
            {
                gameObject.transform.Rotate(0, -180, 0);
                //print("left");
                _bFacingRight = false;
            }
            //移動
            _rigi.velocity = new Vector3(_fAxisX * _fMoveforcex, _rigi.velocity.y, 0);
        }
        else
        {
            //if (_rigi.velocity.x != 0.0)
            _rigi.velocity = new Vector3(0.0f, _rigi.velocity.y, 0.0f);
        }
    }
    //跳躍
    void Jump()
    {
        _rigi.velocity = new Vector3(_rigi.velocity.x, _fJumpforce, 0);
    }
    //在地板
    void CanJump(bool _bOnTheGround)
    {
        if (_bOnTheGround)
        {
            _bCanJump = true;
        }
        else
        {
            _bCanJump = false;
        }
    }
    //踩中角色
    public void StampSuccessful(bool _bSameTeam)
    {
        //不同隊
        if (!_bSameTeam)
        {
            print("Headache");
            _bHeadache = true;
        }
        //同隊
        else
        {
            _bKnockBack = true;
        }
        //StartCoroutine(KnockBack(0.05f, 500, this.gameObject.transform.position));
    }
    //暈眩時間
    bool Headache()
    {
        bool _bHeadaching = false;
        if (_fHeadaching <= _fHeadacheTime)
        {
            _bHeadaching = true;
            _fHeadaching += Time.deltaTime;
        }
        //時間到
        else
        {
            _fHeadaching = 0.0f;
            _bHeadache = false;
            _bHeadaching = false;
        }
        return _bHeadaching;
    }
    //震飛時間
    bool KnockBack()
    {
        bool _bKnocking = false;
        if (_fKnocking <= _fKnockBackTime)
        {
            _bKnocking = true;
            _fKnocking += Time.deltaTime;
            if (_bFacingRight)
            {
                _rigi.velocity = new Vector3(-_fKnockBackforce, _fKnockBackforce, 0);
                //print("123  " + _rigi.velocity.x);
                //_rigi.AddForce(new Vector3(_v3KnockBackDir.x * -300, _v3KnockBackDir.y * _fKnockBackPwr, 0));
            }
            else
            {
                _rigi.velocity = new Vector3(_fKnockBackforce, _fKnockBackforce, 0);
                //print("456  " + _rigi.velocity.x);
                //_rigi.AddForce(new Vector3(_v3KnockBackDir.x * 300, _v3KnockBackDir.y * _fKnockBackPwr, 0));
            }
        }
        //時間到
        else
        {
            _fKnocking = 0.0f;
            _bKnockBack = false;
            _bKnocking = false;
        }
        return _bKnocking;
    }
    //震飛
    //IEnumerator KnockBack(float _fKnocking, float _fKnockBackPwr, Vector3 _v3KnockBackDir)
    //{
    //    float _fTimer = 0.0f;
    //    _rigi.velocity = new Vector3(0, _rigi.velocity.y, 0);
    //    while (_fKnocking > _fTimer)
    //    {
    //        _fTimer += Time.deltaTime;
    //        if (_bFacingRight)
    //        {
    //            //_rigi.velocity = new Vector3(_rigi.velocity.x * -(_fMoveforcex), 5, 0);
    //            _rigi.AddForce(new Vector3(_v3KnockBackDir.x * -300, _v3KnockBackDir.y * _fKnockBackPwr, 0));
    //        }
    //        else
    //        {
    //            //_rigi.velocity = new Vector3(_rigi.velocity.x * _fMoveforcex, 5, 0);
    //            _rigi.AddForce(new Vector3(_v3KnockBackDir.x * 300, _v3KnockBackDir.y * _fKnockBackPwr, 0));
    //        }
    //    }
    //    yield return 0;
    //}
}
