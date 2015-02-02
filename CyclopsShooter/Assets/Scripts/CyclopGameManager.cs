using UnityEngine;
using System.Collections;

public class CyclopGameManager : MonoBehaviour
{
	public CyclopsMainPlayer Player;
	public RailSystem Rails;
	private CrouchMe _crouchScript;

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
}
