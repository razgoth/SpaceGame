using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceshipControls : MonoBehaviour
{

  public Rigidbody2D rb;
  public SpriteRenderer spriteRenderer;
  public Collider2D collider;
  public float thrust;
  public float turnThrust;
  private float thrustInput;
  private float turnInput;
  public float screenTop;
  public float screenButtom;
  public float screenLeft;
  public float screenRight;
  public GameObject bullet;
  public float bulletForce;
  public float deathForce;
  public int score;
  public int lives;
  public Text scoreText;
  public Text livesText;
  public AudioSource audio;
  public GameObject explosion;
  public Color inColor;
  public Color normalColor;
  public GameObject gameoverPanel;
  public GameObject newHighScorePanel;
  private bool specialEffect2;  // true = currently in special effect 2
  public GameManager gm;
  public InputField highScoreInput;
  public Text highScoreListText;
 // public static string publicName = PlayerPrefs.GetString("highscoreName") ;
  //public static int publicScore = PlayerPrefs.GetInt("highscore") ; 



  // Start is called before the first frame update
  void Start()
  {

    score = 0;

    specialEffect2 = false;
    scoreText.text = "Score " + score;
    livesText.text = "Lives " + lives;
    
    }

  // Update is called once per frame
  void Update()
  {

    // check for input from the fire key and make bullets
    if (Input.GetButtonDown("Fire1"))
    {
      GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
      newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);
      Destroy(newBullet, 9.0f);
    }



    // check for input from keyboard 
    thrustInput = Input.GetAxis("Vertical");
    turnInput = Input.GetAxis("Horizontal");

    // check for special effect 1
    if (Input.GetButtonDown("SpecialEffect1"))
    {

      spriteRenderer.color = inColor;
      collider.enabled = false;

      Invoke("SpecialEffect1", 2f);
    }

    // check for special effect 2
    if (Input.GetButtonDown("SpecialEffect2") && specialEffect2 == false)
    {
      specialEffect2 = true;
      spriteRenderer.enabled = false;
      collider.enabled = false;
      GetComponent<SpaceshipControls>().enabled = false;

      Invoke("SpecialEffect2", 1f);
    }

    // Rotate the ship
    transform.Rotate(Vector3.forward * turnInput * Time.deltaTime * -turnThrust);

    // screen wraping

    Vector2 newPos = transform.position;
    if (transform.position.y > screenTop)
    {
      newPos.y = screenButtom;
    }
    if (transform.position.y < screenButtom)
    {
      newPos.y = screenTop;
    }
    if (transform.position.x > screenRight)
    {
      newPos.x = screenLeft;
    }
    if (transform.position.x < screenLeft)
    {
      newPos.x = screenRight;
    }
    transform.position = newPos;
  }

  void FixedUpdate()
  {
    rb.AddRelativeForce(Vector2.up * thrustInput);
    //rb.AddTorque(-turnInput);
  }

  void ScorePoints(int pointsToAdd)
  {
    score += pointsToAdd;
    scoreText.text = "Score " + score;
  }

  void Respawn()
  {
    rb.velocity = Vector2.zero;
    transform.position = Vector2.zero;
    collider.enabled = false;

    spriteRenderer.enabled = true;
    spriteRenderer.color = inColor;
    Invoke("Invulnerable", 3f);
    GetComponent<SpaceshipControls>().enabled = true;

  }

  void Invulnerable()
  {
    collider.enabled = true;
    spriteRenderer.color = normalColor;

  }

  void SpecialEffect1()
  {

    // Turn on colliders and spriteRenderers 

    spriteRenderer.enabled = true;
    collider.enabled = true;
    spriteRenderer.color = normalColor;


  }

  void SpecialEffect2()
  {

    // move to new randome position 

    Vector2 newPosition = new Vector2(Random.Range(-19f, 19f), Random.Range(-13f, 13f));
    transform.position = newPosition;

    // Turn on colliders and spriteRenderers 

    spriteRenderer.enabled = true;
    collider.enabled = true;
    GetComponent<SpaceshipControls>().enabled = true;

    specialEffect2 = false; // stop people from using this button while the spcial effect is activated 
  }

  void LoseLife()
  {
    lives--;

    // Make Explosion when you lose
    GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
    Destroy(newExplosion, 3f);
    livesText.text = "Lives " + lives;

    // respawn - new life
    spriteRenderer.enabled = false;
    collider.enabled = false;
    GetComponent<SpaceshipControls>().enabled = false;

    Invoke("Respawn", 2f);

    if (lives <= 0)
    {
      //GameOver¨
      GameOver();
    }

  }
  void OnCollisionEnter2D(Collision2D col)
  {
    Debug.Log(col.relativeVelocity.magnitude);
    audio.Play();
    if (col.relativeVelocity.magnitude > deathForce)
    {
      LoseLife();
      // instead of creating one code for being hit by an astroid we create a function so that when we are being hit by ufo we get the same result 
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  { // whenever something with the trigger collider on it will collide with this object (spaceship)
    if (other.CompareTag("beam"))
    {
      LoseLife();
    }


  }


  void GameOver()
  {
    CancelInvoke();

    // tell the game manager to check for high scores
    if (gm.CheckForHighScore(score))
    {
      newHighScorePanel.SetActive(true);// when the user presses enter the info will close the input panel 
    }
    else
    {
      gameoverPanel.SetActive(true); // if there is no high score we need the gameover panel
      highScoreListText.text = "HIGH SCORE" + "\n" + "\n" + PlayerPrefs.GetString("highscoreName") + "   " + PlayerPrefs.GetInt("highscore");

    }
  }

  public void HighScoreInput()
  {
    string newInput = highScoreInput.text; // we need to store that to playerprefs
    Debug.Log(newInput);
    newHighScorePanel.SetActive(false); // the panel is closed when the user presses enter 
    gameoverPanel.SetActive(true);
    PlayerPrefs.SetString("highscoreName", newInput);
    PlayerPrefs.SetInt("highscore", score);
    highScoreListText.text = "HIGH SCORE" + "\n" + "\n" + PlayerPrefs.GetString("highscoreName") + "   " + PlayerPrefs.GetInt("highscore");
  }

  public void GoToMainMenu()
  {
    SceneManager.LoadScene("startMenu");
  }

  public void PlayAgain()
  {
    SceneManager.LoadScene("GameMode");
  }
}
