using UnityEngine;
using System.Collections;

public class DKController : MonoBehaviour {


    public float speed;
    private SpriteRenderer sprite;
    private Animator dkAnimator;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        dkAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        dkAnimator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetAxis("Horizontal") >= 0)
            sprite.flipX = false;
        else
            sprite.flipX = true;
        transform.Translate(transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);

    }


    void OnTriggerEnter2D(Collider2D colliderTrigger) {

        Destroy(colliderTrigger.gameObject);
    }
}
