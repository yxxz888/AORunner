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
        Debug.Log(getSize());
        for(int i = 0;i < moneyPerPlatform;i++)
        {
            GameObject money = Instantiate(Resources.Load("Dollar_bag")) as GameObject;
            float x = Random.Range(0, getSize().x);
            float z = Random.Range(0, getSize().z);

            money.transform.SetParent(transform);
            money.transform.localPosition = new Vector3(x - getSize().x / 2, 1, z - getSize().z / 2);

            bags.Add(money);
        }
    }


    //把一个platform分成N个区域，然后随机出M个放Money
    //private int[] GetMoneyIndexes()
    //{
    //    int[] result = new int[moneyPerPlatform];
    //    ArrayList indexes = new ArrayList();
    //    for (int i = 0; i < row * col; i++)
    //        indexes.Add(i);
    //    for (int i = 0; i < moneyPerPlatform; i++)
    //    {
    //        int index = Random.Range(0, indexes.Count);
    //        result[i] = (int)indexes[index];
    //        indexes.RemoveAt(index);
    //    }    
    //    return result;
    //}

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
