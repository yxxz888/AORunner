using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour {

    public int moneyPerPlatform = 10;

    private bool isStepped = false;
    private bool isOut = false;

    private int diretion = 0;

    private int row = 3;
    private int col = 10;

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

        createMoney();
    }


    private void createMoney()
    {
        int[] indexes = GetMoneyIndexes();
        
        //GameObject money = Instantiate(Resources.Load("Prefabs/Dollar_bag")) as GameObject;

    }


    //把一个platform分成N个区域，然后随机出M个放Money
    private int[] GetMoneyIndexes()
    {
        int[] result = new int[moneyPerPlatform];
        ArrayList indexes = new ArrayList();
        for (int i = 0; i < row * col; i++)
            indexes.Add(i);
        for (int i = 0; i < moneyPerPlatform; i++)
        {
            int index = Random.Range(0, indexes.Count);
            result[i] = (int)indexes[index];
            indexes.RemoveAt(index);
        }    
        return result;
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
