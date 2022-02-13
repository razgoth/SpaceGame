using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    public float maxthrust; 
    public float maxtorque; 
    public Rigidbody2D rb;
    public float screenTop; 
    public float screenButtom;
    public float screenLeft; 
    public float screenRight;   
    public int asteroidSize;    // 3 large, 2 medium, 1 small
    public GameObject ateroidMedium;
    public GameObject asteroidSmall; 
    public int points; 
    public GameObject player;
    public GameObject explosion;
    public GameManager gm; // link to game manager script

    // Start is called before the first frame update
    void Start()
    {
        Vector2 thrust = new Vector2(Random.Range(-maxthrust,maxthrust),(Random.Range(-maxthrust,maxthrust)));
        float torque = Random.Range(-maxtorque,maxtorque); 

        rb.AddForce(thrust); 
        rb.AddTorque(torque); 

        // Find the Player
         player = GameObject.FindWithTag("Player"); 

         // Find the game manager 

        gm = GameManager.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
              // screen wraping

        Vector2 newPos = transform.position; 
        if(transform.position.y > screenTop){
            newPos.y = screenButtom;
        }
        if(transform.position.y < screenButtom){
            newPos.y = screenTop;
        }
          if(transform.position.x > screenRight){
            newPos.x = screenLeft;
        }
        if(transform.position.x < screenLeft){
            newPos.x = screenRight;
        }
        transform.position = newPos; 
    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // check to see if its a bullet
        if(other.CompareTag("bullet")){
                    
            // destroy the bullet
            Destroy(other.gameObject); 
            // check the size of the asteroid and spawn in the next smaller size 
            if(asteroidSize == 3){
                // spawn two medium asteroids
                Instantiate(ateroidMedium, transform.position, transform.rotation);
                Instantiate(ateroidMedium, transform.position, transform.rotation);
                
                gm.UpdateNumberOfAsteroids(1);  // we tell the gamemanager will still have 1 more after destroying 1

            }
            else if ( asteroidSize == 2){
                // spawn two small asteroids 
                Instantiate(asteroidSmall, transform.position, transform.rotation);
                Instantiate(asteroidSmall, transform.position, transform.rotation);

                gm.UpdateNumberOfAsteroids(1);
            }
            else if ( asteroidSize == 1){
                // Remove the asteroid
                gm.UpdateNumberOfAsteroids(-1);
            }
                // Tell the player to score some points 

                player.SendMessage("ScorePoints", points); 
                
                // Make an explosion 
                GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation); 
                Destroy (newExplosion, 3f);
                
                // Remove the asteroid
                Destroy (gameObject); 

        }
    }


}
