using UnityEngine;
using System.Collections;

public class CyclopGameManager : MonoBehaviour
{
	public CyclopsMainPlayer Player;
	public RailSystem Rails;
	private CrouchMe _crouchScript;
    private TextOnScreenEnabler TextEnabler;
    public int mainMenuLoadIndex = 0;

    void Start()
    {
	    _crouchScript = Player.GetComponent<CrouchMe>();
	    Rails.CompletedSection += () =>
	    {
		    if (_crouchScript != null)
		    {
			    _crouchScript.enabled = false;
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
	    };
    }

	public void Continue()
	{
		Rails.Continue();
	}

    IEnumerator WonGame(float secToWait)
    {
        TextEnabler.EnableWinText = true;
        yield return new WaitForSeconds(secToWait);
        Application.LoadLevel(mainMenuLoadIndex);
    }
}