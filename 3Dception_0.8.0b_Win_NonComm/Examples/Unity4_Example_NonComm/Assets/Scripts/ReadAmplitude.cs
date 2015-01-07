using UnityEngine;
using System.Collections;

public class ReadAmplitude : MonoBehaviour {

	private GameObject Talker;
	private Color talkerColor;
	private float amplitude;
	private float[] smooth = new float[2];

	// Use this for initialization
	void Start () {

		talkerColor = new Color ();
		Talker = GameObject.Find ("Robot/default");

	}
	
	// Update is called once per frame
	void Update () {
		talkerColor.r = Mathf.Clamp(amplitude*100, 0, 0.92f);
		talkerColor.b = Mathf.Clamp(amplitude*100, 0, 0.04f);
		talkerColor.g = Mathf.Clamp(amplitude*100, 0, 0.04f);
		talkerColor.a = 1;
		Talker.renderer.materials[2].color = Talker.renderer.materials[0].color = talkerColor;
	}

	void OnAudioFilterRead (float[] data, int channels)
	{		
		for (var i = 0; i < data.Length; i++) {
			float absInput = Mathf.Abs(data[i]);
			smooth[0] = ((0.01f * absInput) + (0.99f * smooth[1]));
			amplitude = smooth[0];
			smooth[1] = smooth[0];
			data[i] = 0;
		}
	}
}
