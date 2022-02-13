using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TopScore : MonoBehaviour
{
    public SpaceshipControls spaceship;

        public void GoToMainMenu()
        {
        SceneManager.LoadScene("startMenu");

        }

        
    
                  public void OnGUI()
    {
        string stringFirst = PlayerPrefs.GetString("highscoreName") ;
        int nextInfo = PlayerPrefs.GetInt("highscore") ; 
        string stringSecond = nextInfo.ToString();

        //int result= SpaceshipControls.publicScore;
        //string stringOutput = result.ToString(); 
               GUI.Label(new Rect(Screen.width/2.5f,Screen.height/3, 300, 50),"<color=yellow><size=40>" + stringFirst + "    " + stringSecond+ "</size></color>");

       //GUI.Label(new Rect(Screen.width/2.5f,Screen.height/3, 300, 50),"<color=yellow><size=40>" + SpaceshipControls.publicName + "    " + stringOutput+ "</size></color>");
        // GUI.Label(new Rect(Screen.width/3,Screen.height/3, 900, 50),SpaceshipControls.publicName);
       //GUI.Label(new Rect(Screen.width/2.5f,Screen.height/3, 300, 50),"<color=yellow><size=40>" + SpaceshipControls.highScoreListText + "    " + stringOutput+ "</size></color>");

    }   
}
