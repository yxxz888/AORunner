using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Player player;
    public Camera followCamera;
    public GameObject ui;
    public Platform[] platforms;
    public Text TxtScore;

    public int ScorePerMoneyBag = 100;

    private ArrayList platformList;
    private bool isStart = false;

    private float Score;

	// Use this for initialization
	void Start () {
        followCamera.transform.SetParent(player.transform);

        platformList = new ArrayList();
        foreach (Platform obj in platforms)
        {
            platformList.Add(obj);
            obj.reset();
        }

        EventDispatcher.Instance.AddEventListener(EventDispatcher.OnPickMoneyBag, onPickMoneyBag);
    }
	
	// Update is called once per frame
	void Update () {

        if (checkStartGame())
            return;

        if(Input.GetButtonUp("Jump"))
        {
            checkJump();
        }
        if (Input.GetButtonUp("Slide"))
        {
            checkSlide();
        }
        if (Input.GetButtonUp("TurnLeft"))
        {
            checkTurnLeft();
        }
        if (Input.GetButtonUp("TurnRight"))
        {
            checkTurnRight();
        }
        player.forward();

        checkPlatformReuse();

        Score += Time.deltaTime * 10;
        setTextScore();
    }


    public void checkJump()
    {
        if(player.canAction())
            player.jump();
    }


    public void checkSlide()
    {
        if (player.canAction())
            player.slide();
    }


    public void checkTurnLeft()
    {
        if (player.canAction())
            player.turnLeft();
    }


    public void checkTurnRight()
    {
        if (player.canAction())
            player.turnRight();
    }


    private bool checkStartGame()
    {
        if (isStart)
            return false;

        if (Input.GetMouseButtonUp(0))
            startGame();

        return true;
    }   


    void startGame()
    {
        isStart = true;
        player.startGame();
    }


    private void checkPlatformReuse()
    {
        Platform platform = platformList[0] as Platform;
        if(platform.isPlayerPass())
        {
            int diretion = Random.value < 0.5 ? -1 : 1;
            Platform last = platformList[platformList.Count - 1] as Platform;
            platform.transform.localPosition = getNextPlatformPosition(last, diretion);        
            platform.reset();
            platform.SetDiretion(last.GetDiretion() + diretion);
            platformList.Remove(platform);
            platformList.Add(platform);
        }
    }

    private Vector3 getNextPlatformPosition(Platform last,int diretionMark)
    {
        Vector3 size = last.getSize();
        float width = size.x;
        float length = size.z;
        float d = Mathf.Abs(length / 2 - width / 2);
        Vector3 result = new Vector3();
        if (last.GetDiretion() == 0)
            result = new Vector3(last.transform.position.x + diretionMark * d, 0, last.transform.position.z + d);
        else if (last.GetDiretion() == 1)
            result = new Vector3(last.transform.position.x + d, 0, last.transform.position.z - diretionMark * d);
        else if (last.GetDiretion() == 2)
            result = new Vector3(last.transform.position.x - diretionMark * d, 0, last.transform.position.z - d);
        else if (last.GetDiretion() == 3)
            result = new Vector3(last.transform.position.x - d, 0, last.transform.position.z + diretionMark * d);
        return result;
    }


    private void onPickMoneyBag(Object param)
    {
        Debug.Log("hehe");
        Score += ScorePerMoneyBag;
        setTextScore();
    }


    private void setTextScore()
    {
        TxtScore.text = (int)Score + "";
    }
}
