using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {

    public string UnityGameName;
	// Use this for initialization
    public void LoadTheGame()
    {
        Application.LoadLevel(UnityGameName); 
    }
}
