using UnityEngine;
using System.Collections;

public class BarrelGenerator : MonoBehaviour {

    public GameObject barrelPrefab;
    public float waitTime;


	// Use this for initialization
	void Start () {
        InvokeRepeating("CreateBarrel", 0, waitTime);
	}

    void CreateBarrel() {
        Instantiate(barrelPrefab, transform.position, Quaternion.identity);
    }
}
