using UnityEngine;
using System.Collections;

public class EpicThemeInstance : MonoBehaviour {

    private GameObject themeMusic;
	// Use this for initialization
	void Start () {

        if (themeMusic == null)
        {
            themeMusic = this.gameObject;
            DontDestroyOnLoad(themeMusic);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
	}
}
