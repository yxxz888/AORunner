using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject obj = Instantiate(Resources.Load("Coin_Burst")) as GameObject;
            obj.transform.SetParent(transform.parent);
            obj.transform.localPosition = transform.localPosition;

            Destroy(gameObject);

            EventDispatcher.Instance.DispatcherEvent(EventDispatcher.OnPickMoneyBag);
        }
    }
}
