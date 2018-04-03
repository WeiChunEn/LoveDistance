using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Engery engery = null;
    public Finish_area finishArea = null;
    public Team_Win teamWin = null;
    public Defense_engery defenseEngery = null;

    public GameObject _gStart = null;
    public GameObject _gTutorial = null;
    public GameObject _gGame = null;

    public Camera _gCamera = null;

    [SerializeField]
    private bool _bWait = false;
    [SerializeField]
    private bool _bGameCompleteWait = false;

    [SerializeField]
    private float _fCameraSpeed = 5.0f;
    [SerializeField]
    private float _fWaiting = 0.0f;
    [SerializeField]
    private float _fWaitTime = 0.5f;
    [SerializeField]
    private float _fGameCompleteWaiting = 0.0f;
    [SerializeField]
    private float _fGameCompleteWaitTime = 5.0f;

    [System.Serializable]
    public struct AAudio
    {
        public AudioClip _aStartMenu;
        public AudioClip _aGameMenu;
    }
    public AAudio _aAudio;
    [System.Serializable]
    public struct GPlayer
    {
        public Player _gPlayer1;
        public Player _gPlayer2;
        public Player _gPlayer3;
        public Player _gPlayer4;
    }
    public GPlayer _gPlayer;
    [System.Serializable]
    public struct GGameComplete
    {
        public GameObject _gWinBackGround;
        public GameObject _gOrange;
        public GameObject _gPurple;
    }
    public GGameComplete _gGameComplete;

    public enum AllMenu
    {
        StartMenu,
        TutorialMenu,
        GoToGame,
        GameMenu,
        GameComplete
    }
    public AllMenu nowMenu = AllMenu.StartMenu;
    void Awake()
    {
        if (engery == null)
        {
            print("MISS Engery");
        }
        else
        {
            engery.allPlayersInReady = AllReady;
        }
        _gCamera.GetComponent<AudioSource>().clip = _aAudio._aStartMenu;
        _gCamera.GetComponent<AudioSource>().Play();
    }
    void Update()
    {
        switch (nowMenu)
        {
            case AllMenu.StartMenu:
                StartMenu();
                break;
            case AllMenu.TutorialMenu:
                TutorialMenu();
                break;
            case AllMenu.GoToGame:
                GoToGame();
                break;
            case AllMenu.GameMenu:
                GameMenu();
                break;
            case AllMenu.GameComplete:
                GameComplete();
                break;
        }
    }
    void StartMenu()
    {
        _gStart.SetActive(true);
        _gTutorial.SetActive(false);
        _gGame.SetActive(false);
    }
    void TutorialMenu()
    {
        _gStart.SetActive(false);
        _gTutorial.SetActive(true);
        _gGame.SetActive(false);
        if (_bWait)
        {
            Wait();
        }
    }
    void GoToGame()
    {
        _gStart.SetActive(false);
        _gTutorial.SetActive(true);
        _gGame.SetActive(true);
        if (_gCamera != null)
        {
            _gCamera.transform.position = new Vector3(_gCamera.transform.position.x, Mathf.Lerp(_gCamera.transform.position.y, _gGame.transform.position.y + 6, _fCameraSpeed * Time.deltaTime), _gCamera.transform.position.z);
            if (_gCamera.transform.position.y >= (_gGame.transform.position.y - 5.99f))
            {
                //_gCamera.transform.position = _gGame.transform.position;
                _gCamera.transform.position = new Vector3(_gCamera.transform.position.x, _gGame.transform.position.y + 6, _gCamera.transform.position.z);
                nowMenu = AllMenu.GameMenu;
                _gCamera.GetComponent<AudioSource>().clip = _aAudio._aGameMenu;
                _gCamera.GetComponent<AudioSource>().Play();
            }
        }
    }
    void GameMenu()
    {
        _gStart.SetActive(false);
        _gTutorial.SetActive(false);
        _gGame.SetActive(true);
        if (finishArea._bWin)
        {
            finishArea._bWin = false;
            _bGameCompleteWait = true;
            nowMenu = AllMenu.GameComplete;
        }
        else
        {
            teamWin.TeamWinUpdate();
        }
    }
    void GameComplete()
    {
        _gStart.SetActive(false);
        _gTutorial.SetActive(false);
        _gGame.SetActive(true);
        _gGameComplete._gWinBackGround.SetActive(true);
        switch (finishArea.winTeam)
        {
            case Player.AllTeam.OrangeTeam:
                _gGameComplete._gOrange.SetActive(true);
                break;
            case Player.AllTeam.PurpleTeam:
                _gGameComplete._gPurple.SetActive(true);
                break;
        }
        if (_bGameCompleteWait)
        {
            GameCompleteWait();
        }
    }

    public void PressBtnStart()
    {
        print("Start");
        nowMenu = AllMenu.TutorialMenu;
    }
    public void PressBtnExit()
    {
        print("Exit");
        Application.Quit();
    }
    void AllReady()
    {
        if (!_bWait)
        {
            _bWait = true;
        }
    }
    //時間
    bool Wait()
    {
        bool _bWaiting = false;
        if (_fWaiting <= _fWaitTime)
        {
            _bWaiting = true;
            _fWaiting += Time.deltaTime;
        }
        //時間到
        else
        {
            _fWaiting = 0.0f;
            _bWait = false;
            _bWaiting = false;
            print("GoToGame");
            nowMenu = AllMenu.GoToGame;
        }
        return _bWaiting;
    }
    //完成遊戲後的等待時間
    bool GameCompleteWait()
    {
        bool _bGameCompleteWaiting = false;
        if (_fGameCompleteWaiting <= _fGameCompleteWaitTime)
        {
            _bGameCompleteWaiting = true;
            _fGameCompleteWaiting += Time.deltaTime;
        }
        //時間到
        else
        {
            _fGameCompleteWaiting = 0.0f;
            _bGameCompleteWait = false;
            _bGameCompleteWaiting = false;
            Reset();
            nowMenu = AllMenu.StartMenu;
        }
        return _bGameCompleteWaiting;
    }
    //歸零
    void Reset()
    {
        finishArea.winTeam = Player.AllTeam.none;
        _gCamera.transform.position = new Vector3(_gCamera.transform.position.x, _gTutorial.transform.position.y, _gCamera.transform.position.z);
        _gCamera.GetComponent<AudioSource>().clip = _aAudio._aStartMenu;
        _gCamera.GetComponent<AudioSource>().Play();

        engery.ResetValue();
        _bWait = false;
        _bGameCompleteWait = false;


        _gGameComplete._gWinBackGround.SetActive(false);
        _gGameComplete._gOrange.SetActive(false);
        _gGameComplete._gPurple.SetActive(false);
        _gPlayer._gPlayer1.gameObject.transform.position = new Vector3(-5.21f, 25.42f, 0);
        _gPlayer._gPlayer2.gameObject.transform.position = new Vector3(-3.38f, 25.42f, 0);
        _gPlayer._gPlayer3.gameObject.transform.position = new Vector3(3.36f, 25.42f, 0);
        _gPlayer._gPlayer4.gameObject.transform.position = new Vector3(5.07f, 25.42f, 0);
        if (!_gPlayer._gPlayer1._bFacingRight)
        {
            _gPlayer._gPlayer1.transform.Rotate(0, -180, 0);
            _gPlayer._gPlayer1._bFacingRight = true;
        }
        if (!_gPlayer._gPlayer2._bFacingRight)
        {
            _gPlayer._gPlayer2.transform.Rotate(0, -180, 0);
            _gPlayer._gPlayer2._bFacingRight = true;
        }
        if (_gPlayer._gPlayer3._bFacingRight)
        {
            _gPlayer._gPlayer3.transform.Rotate(0, -180, 0);
            _gPlayer._gPlayer3._bFacingRight = false;
        }
        if (_gPlayer._gPlayer4._bFacingRight)
        {
            _gPlayer._gPlayer4.transform.Rotate(0, -180, 0);
            _gPlayer._gPlayer4._bFacingRight = false;
        }
        teamWin.win_engery_orange.value = 0.0f;
        teamWin.win_engery_purple.value = 0.0f;
        defenseEngery.Orange_Defense.value = 0.0f;
        defenseEngery.Purple_Defense.value = 0.0f;
    }
}
