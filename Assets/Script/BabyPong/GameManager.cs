using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;
    private int i;

    public GUISkin layout;

    GameObject theBall;

	void Start () {
        i = PlayerPrefs.GetInt("recompense");
        theBall = GameObject.FindGameObjectWithTag("Ball");
	}

    public static void Score (string wallID)
    {
        if (wallID == "RightWall")
        {
            PlayerScore1++;
        } else
        {
            PlayerScore2++;
        }
    }

    void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + PlayerScore1);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + PlayerScore2);

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            PlayerScore1 = 0;
            PlayerScore2 = 0;
            theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }
        if (PlayerScore1 == 3)
        {
            GUI.Label(new Rect(0 , 200, 2000, 1000), "Mouais t'as eu chaud, que de la chance !");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            PlayerPrefs.SetInt("babypong", 1);
            if(i==0)
              PlayerPrefs.SetInt("recompense", 1);
            PlayerScore1 = 0;
            PlayerScore2 = 0;
            SceneManager.LoadScene("Pic");
        } else if(PlayerScore2 == 3) {
            GUI.Label(new Rect(0 , 200, 2000, 1000), "T'as perdu... T'es mauvais Jaaaaaack!");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            PlayerPrefs.SetInt("babypong", 2);
            PlayerPrefs.SetInt("recompense", 0);
            PlayerScore1 = 0;
            PlayerScore2 = 0;
            SceneManager.LoadScene("Pic");
        }
    }
}
