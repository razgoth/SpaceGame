using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

  public int numberOfAsteroid; // current number of asteroid in the scene
  public int levelNumber = 1;
  public GameObject asteroid;
  public Text levelText;
  public redAlien redalien; // the alien should be controlled by the game manager 



  void Start()
  {
    levelText.text = "Level " + levelNumber;
  }


  public void UpdateNumberOfAsteroids(int change)

  {
    numberOfAsteroid += change;

    // check to see if there are any asteroids left 
    if (numberOfAsteroid <= 0)
    {
      // start new level 
      Invoke("StartNewLevel", 3f);
    }
  }

  void StartNewLevel()
  {
    levelNumber++;

    levelText.text = "Level " + levelNumber;
    // spawn new asteroids 

    for (int i = 0; i < levelNumber * 2; i++)
    {
      Vector2 spawnPosition = new Vector2(Random.Range(-24, 24), 12f);
      Instantiate(asteroid, spawnPosition, Quaternion.identity);
      numberOfAsteroid = levelNumber * 2;
    }
    // the gameManager will take care of respawning the UFO because I want each level to have more difficulty 
    //set up the UFO 
    
    redalien.NewLevel();
  }

  public bool CheckForHighScore(int score)
  {
    int highScore = PlayerPrefs.GetInt("highscore");
    if (score > highScore)
    {
      return true;
    }
    return false;

  }

}
