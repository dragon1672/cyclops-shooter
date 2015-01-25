using UnityEngine;
using System.Collections;

public class VisualHealth : MonoBehaviour {

    private CanvasGroup healthCanvas;
    public float Alpha{get;set;}
	// Use this for initialization
	void Start () {
        healthCanvas = gameObject.GetComponent<CanvasGroup>();
        Alpha = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (healthCanvas.alpha != Alpha)
        {
            healthCanvas.alpha = Alpha;
        }
	}
}