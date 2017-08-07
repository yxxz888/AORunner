using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Player player;
    public Camera followCamera;
    public GameObject ui;
    public Platform[] platforms;

    private ArrayList platformList;
    private bool isStart = false;

	// Use this for initialization
	void Start () {
        followCamera.transform.parent = player.transform;

        platformList = new ArrayList();
        foreach (Platform obj in platforms)
        {
            platformList.Add(obj);
            obj.reset();
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (checkStartGame())
            return;

        if(Input.GetButtonUp("Jump") && player.canAction())
        {
            player.jump();
        }
        if (Input.GetButtonUp("Slide") && player.canAction())
        {
            player.slide();
        }
        if (Input.GetButtonUp("TurnLeft") && player.canAction())
        {
            player.turnLeft();
        }
        if (Input.GetButtonUp("TurnRight") && player.canAction())
        {
            player.turnRight();
        }
        player.forward();

        checkPlatformReuse();
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
        Vector3 size = last.transform.Find("Ground").GetComponent<MeshRenderer>().bounds.size;
        float width = size.x * last.transform.localScale.x;
        float length = size.z * last.transform.localScale.z;
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
}
