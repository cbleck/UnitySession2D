using UnityEngine;
using System.Collections;

public class BarrelObject : MonoBehaviour
{

    void OnEnable(){
        StartCoroutine("WaitToReturn");
    }

    IEnumerator WaitToReturn(){
        yield return new WaitForSeconds(5.0f);
        ObjectPool.instance.PoolGameObject(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collisionObject){
        if (collisionObject.transform.tag == "Player"){
            ObjectPool.instance.PoolGameObject(this.gameObject);
        }
    }
}
