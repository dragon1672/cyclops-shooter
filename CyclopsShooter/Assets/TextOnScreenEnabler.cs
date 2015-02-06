using UnityEngine;
using System.Collections;

public class TextOnScreenEnabler : MonoBehaviour {
    public GameObject winText;
    public GameObject gameoverText;
    public GameObject pleasewaitText;
    public bool startWinTextEnabled, startGameoverTextEnabled, startPleasewaitTextEnabled;
    public bool EnableWinText
    {
        set { winText.SetActive(value); }
        get { return winText.activeInHierarchy; }
    }
    public bool EnableGameOverText
    {
        set { gameoverText.SetActive(value); }
        get { return gameoverText.activeInHierarchy; }
    }
    public bool EnablePleaseWaitText
    {
        set { pleasewaitText.SetActive(value); }
        get { return pleasewaitText.activeInHierarchy; }
    }
        // Use this for initialization
	void Start () {
        EnableWinText = startWinTextEnabled;
        EnableGameOverText = startGameoverTextEnabled;
        EnablePleaseWaitText = startPleasewaitTextEnabled;
	}
	
	// Update is called once per frame
	void Update () {
        //DEBUG/////////////////////////////////////////

        //EnableWinText = startWinTextEnabled;
        //EnableGameoverText = startGameoverTextEnabled;
        //EnablePleasewaitText = startPleasewaitTextEnabled;

        ////////////////////////////////////////////////
	
	}
}
