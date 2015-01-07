using UnityEngine;
using System.Collections;

public class PlayAudioSources : MonoBehaviour {
	
	public AudioClip[] dialogue;
	private GameObject dummyAudioObject;
	private Vector3 newPosition;
	private int positionCounter;
	
	void Start () {
		dummyAudioObject = new GameObject ();
		dummyAudioObject.AddComponent<AudioSource> ();
		dummyAudioObject.AddComponent<ReadAmplitude> ();
		dummyAudioObject.audio.bypassReverbZones = true;
		dummyAudioObject.audio.panLevel = 0;

		newPosition = transform.position;

		InvokeRepeating ("playRandomDia", 1, 7);
		InvokeRepeating ("moveRobot", 1, 10);
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, newPosition, Time.deltaTime*0.5f);
	}

	void playRandomDia() {
		int i = Random.Range (0, 10);
		gameObject.GetComponent<TBE_3DCore.TBE_Source> ().PlayOneShot (dialogue [i]);
		dummyAudioObject.audio.PlayOneShot (dialogue[i]);
	}

	void moveRobot() {

		positionCounter++;
		float randomAddition = Random.Range (0.01f, 0.09f);
		switch (positionCounter) {
		case 1:
			newPosition = new Vector3(-2.32f, 4f, -14.387f);
			break;
		case 2:
			newPosition = new Vector3(1.704f, 1.78f, -14.387f);
			break;
		case 3:
			newPosition = new Vector3(3.765f, 0.634f, -14.38f);
			break;
		case 4:
			newPosition = new Vector3(-0.7374f, 1.7f, -14.647f);
			break;
		case 5:
			newPosition = new Vector3(-0.252f+randomAddition, 1.692f, -10.74f);
			break;
		}
		if (positionCounter == 4)
			positionCounter = 0;
	}
}
