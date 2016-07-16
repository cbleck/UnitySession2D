using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DKController : MonoBehaviour {


    public float speed, jumpforce;
    public GameObject explosionPrefab;
    public Text itemsLabel;
    public Text livesLabel;

    private SpriteRenderer sprite;
    private Animator dkAnimator;
    private bool isgrounded;
    private int lives;
    private int items;

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        dkAnimator = GetComponent<Animator>();
        isgrounded = false;

        /*
        if (!PlayerPrefs.HasKey("Items"))
        {
            lives = 3;
            items = 0;
        }
        else {
            lives = PlayerPrefs.GetInt("Lives");
            items = PlayerPrefs.GetInt("Items");
        }
        */
        DataManager.instance.LoadData();
        items = DataManager.instance.items;
        lives = DataManager.instance.lives;
        //PlayerPrefs.DeleteAll();
    }
	
	// Update is called once per frame
	void Update () {
        dkAnimator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetAxis("Horizontal") >= 0)
            sprite.flipX = false;
        else
            sprite.flipX = true;
        transform.Translate(transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);


        if (Input.GetKey(KeyCode.Space) && isgrounded) {
            TriggerJump();
        }

        itemsLabel.text = "Items: " + items;
        livesLabel.text = "Lives: " + lives;
             
    }


    void OnTriggerEnter2D(Collider2D colliderTrigger) {

        Destroy(colliderTrigger.gameObject);
        items++;
        items = DataManager.instance.items;
        lives = DataManager.instance.lives;
        DataManager.instance.SaveData();
        /*
        PlayerPrefs.SetInt("Items", items);
        PlayerPrefs.Save();
        */
    }

    void OnCollisionEnter2D(Collision2D collisionObject) {

        if (collisionObject.transform.tag == "barrel") {
            lives--;
            items = DataManager.instance.items;
            lives = DataManager.instance.lives;
            DataManager.instance.SaveData();
            /*
            PlayerPrefs.SetInt("Lives", lives);
            PlayerPrefs.Save();
            */
            Destroy(Instantiate(explosionPrefab, collisionObject.contacts[0].point, Quaternion.identity), 0.9f);
            Destroy(collisionObject.gameObject);
        }
        Debug.Log(collisionObject.transform.name);
        Debug.Log(collisionObject.collider.bounds.max.y);
        Debug.Log(GetComponent<Collider2D>().bounds.min.y);

        if (collisionObject.transform.tag == "ground" && collisionObject.collider.bounds.max.y <= GetComponent<Collider2D>().bounds.min.y + 0.05f)
            isgrounded = true;

    }


    void TriggerJump() {
        dkAnimator.SetTrigger("jump");
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        isgrounded = false;
    }
}
