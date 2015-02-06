using UnityEngine;
using System.Collections;

public class CyclopGameManager : MonoBehaviour
{
	public CyclopsMainPlayer Player;
	public RailSystem Rails;
	private CrouchMe _crouchScript;
    private static TextOnScreenEnabler TextEnabler;
    public static int mainMenuLoadIndex = 0;

    void Start()
    {
        TextEnabler = Player.GetComponentInChildren<TextOnScreenEnabler>();
	    _crouchScript = Player.GetComponent<CrouchMe>();
	    Rails.CompletedSection += () =>
	    {
		    if (_crouchScript != null)
		    {
                _crouchScript.enabled = false;
                TextEnabler.EnablePleaseWaitText = true;
			    _crouchScript.UnCrouch();
		    }
	    };
	    Rails.CompletedAll += () => {
			Debug.Log("You win");
            StartCoroutine(WonGame(3.0f));
	    };
	    Rails.EnterSection += () =>
	    {
            _crouchScript.enabled = true;
            TextEnabler.EnablePleaseWaitText = false;
	    };
    }

	public void Continue()
	{
		Rails.Continue();
	}

    public static IEnumerator WonGame(float secToWait)
    {
        TextEnabler.EnableWinText = true;
        return ReloadMainMenu(secToWait);
    }

    public static IEnumerator GameOver(float secToWait)
    {
        TextEnabler.EnableGameOverText = true;
        return ReloadMainMenu(secToWait);
    }

    private static IEnumerator ReloadMainMenu(float secToWait)
    {
        yield return new WaitForSeconds(secToWait);
        AudioManager.resetPool();
        Application.LoadLevel(mainMenuLoadIndex);
    }
}