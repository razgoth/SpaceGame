using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redAlien : MonoBehaviour
{

  public Rigidbody2D rb;
  public Vector2 direction;
  public float speed;
  public Transform player;
  public GameObject bullet;
  public float bulletSpeed;
  public float shootingDelay;
  public float lastTimeShot = 0;
  public GameObject explosion;
  public SpriteRenderer spriteRenderer;
  public Collider2D collider;
  public bool disabled;
  public int currentLevel = 0;
  public int points;




  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.FindWithTag("Player").transform;

  }

  // Update is called once per frame
  void Update()
  {
    if (disabled)
    {
      return;
    }
    if (Time.time > lastTimeShot + shootingDelay)
    {
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
      Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
      GameObject newBullet = Instantiate(bullet, transform.position, q);
      newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, bulletSpeed));
      lastTimeShot = Time.time;

    }
  }


  // to reach into this from outside make it outside
  // all we need is for the game manager starts
  public void NewLevel()
  {
   
    Disable(); // so that we dont have anything alive after the level ends 
    currentLevel++;
    //timeBeforeSpawning = Random.Range(5f, 10f);
    Invoke("Enabling", 10f);

    speed = 2f + (currentLevel + .1f);
    bulletSpeed = 500 + (currentLevel * 100);
    points = 500 * currentLevel;
  }

  void Enabling()
  {

     Vector2 newPosition = new Vector2(Random.Range (-19f, 19f), Random.Range(-13f, 13f)); 
        transform.position = newPosition;
    // turn on colliders and sprite 

    collider.enabled = true;
    spriteRenderer.enabled = true;
    disabled = false;

  }


  void FixedUpdate()
  {
    direction = (player.position - transform.position).normalized; // FINDING THE SPACESHIP must be minus 
    rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime); // THE DIRECTION OF Shots against THE ufo must be plus

  }

  void Disable()
  {
   
    collider.enabled = false;
    spriteRenderer.enabled = false;
    disabled = true;
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("bullet"))
    {
         GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
    Destroy(newExplosion, 3f);
    Disable();
    Destroy(other.gameObject); 

    }
  }
}
