using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour {

    public int moneyPerPlatform = 10;

    private bool isStepped = false;
    private bool isOut = false;

    private int diretion = 0;

    private ArrayList bags = new ArrayList();

    //private int row = 3;
    //private int col = 10;

	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    internal int GetDiretion()
    {
        return diretion;
    }


    internal void SetDiretion(int value)
    {
        if (value < 0)
            value += 4;
        else if (value > 3)
            value -= 4;
        diretion = value;
        this.transform.localEulerAngles = new Vector3(0, diretion * 90, 0);
    }


    internal bool isPlayerPass()
    {
        return isStepped && isOut;
    }


    internal void reset()
    {
        isStepped = false;
        isOut = false;

        clearMoney();
        createMoney();
    }


    internal Vector3 getSize()
    {
        Transform ground = transform.Find("Ground");
        Vector3 size = ground.GetComponent<MeshFilter>().mesh.bounds.size;
        float width = size.x * ground.localScale.x;
        float length = size.z * ground.localScale.z;
        float height = size.y * ground.localScale.y;
        return new Vector3(width, height, length);
        //Vector3 size = transform.Find("Ground").GetComponent<MeshRenderer>().bounds.size;
        //return size;
    }


    private void clearMoney()
    {
        for (int i = bags.Count - 1; i >= 0; i--)
        {
            GameObject bag = bags[i] as GameObject;
            Destroy(bag);
        }
        bags = new ArrayList();
    }


    private void createMoney()
    {
        //for(int i = 0;i < moneyPerPlatform;i++)
        //{
        //    GameObject money = Instantiate(Resources.Load("Dollar_bag")) as GameObject;
        //    float x = Random.Range(0, getSize().x);
        //    float z = Random.Range(0, getSize().z);

        //    money.transform.SetParent(transform);
        //    money.transform.localPosition = new Vector3(x - getSize().x / 2, 1, z - getSize().z / 2);

        //    bags.Add(money);
        //}

        Vector3 size = getSize();
        float dx = size.x / PatternConfig.patternCount;
        float dz = size.z / PatternConfig.patternLength;
        PatternConfig pattern = PatternConfig.getRandomPattern();
        for(int i = 0;i < PatternConfig.patternCount;i++)
        {
            for (int j = 0; j < PatternConfig.patternLength; j++)
            {
                int type = pattern.GetPatternDetail(i, j);
                GameObject obj = getObjByType(type);

                if (obj == null)
                    continue;

                float px = -size.x / 2 + dx / 2 + i * dx;
                float pz = (-size.z / 2 + dz / 2 + j * dz) * -1;//前后倒置
                obj.transform.SetParent(transform);
                obj.transform.localPosition = new Vector3(px,1,pz);
            }
        }
    }


    private GameObject getObjByType(int type)
    {
        GameObject obj = null;
        if(type == 1)
            obj = Instantiate(Resources.Load("Dollar_bag")) as GameObject;
        return obj;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isStepped = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isOut = true;
        }
    }
}
