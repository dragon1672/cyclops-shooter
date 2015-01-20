using UnityEngine;
using System.Collections;

public class PlayAudioSources : MonoBehaviour {

	private static System.Random Rand = new System.Random();
	private static readonly object syncLock = new object();

	private static float RanInRange(float min, float max)
	{
		lock (syncLock) { return (float) (Rand.NextDouble()*(max - min) + min); }
	}

	private static int RanInRange(int min, int max)
	{
		lock (syncLock) { return (Rand.Next(max - min) + min); }
	}

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
		int i = RanInRange(0, 10);
		gameObject.GetComponent<TBE_3DCore.TBE_Source> ().PlayOneShot (dialogue [i]);
		dummyAudioObject.audio.PlayOneShot (dialogue[i]);
	}

	void moveRobot() {

		positionCounter += RanInRange(1,4);
		float randomAddition = RanInRange(0.01f, 0.09f);
		positionCounter %= 4;
		positionCounter++;
		switch (positionCounter) {
		case 0:
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
	}
}
