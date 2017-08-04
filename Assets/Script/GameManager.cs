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
            platformList.Add(obj);
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
            Debug.Log("hehe");
            int diretion = Random.value < 0.5 ? -1 : 1;
            Platform next = platformList[1] as Platform;
            platform.transform.position = getNextPlatformPosition(next, diretion);
            platform.reset();
            platform.transform.localEulerAngles = new Vector3(0, platform.transform.localEulerAngles.y + diretion * 90, 0);
            platformList.Remove(platform);
            platformList.Add(platform);
        }
    }

    private Vector3 getNextPlatformPosition(Platform next,int diretionMark)
    {
        Vector3 size = next.transform.Find("Ground").GetComponent<MeshFilter>().mesh.bounds.size;
        Debug.Log(size);
        Debug.Log(next.transform.localScale);
        float width = size.x * next.transform.localScale.x;
        float length = size.z * next.transform.localScale.z;
        float d = length / 2 - width / 2;
        Debug.Log(d);
        Vector3 result = new Vector3();
        if(next.transform.localEulerAngles.y == 0)
            result = new Vector3(next.transform.position.x + diretionMark * d, 0, next.transform.position.z + d);
        else if (next.transform.localEulerAngles.y == 90)
            result = new Vector3(next.transform.position.x + d, 0, next.transform.position.z - diretionMark * d);
        else if (next.transform.localEulerAngles.y == 180)
            result = new Vector3(next.transform.position.x - diretionMark * d, 0, next.transform.position.z - d);
        else if (next.transform.localEulerAngles.y == 270)
            result = new Vector3(next.transform.position.x - d, 0, next.transform.position.z + diretionMark * d);
        return result;
    }
}
