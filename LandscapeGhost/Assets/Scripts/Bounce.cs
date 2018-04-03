using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {
	public Renderer rend;

	public int band;
	public int scale;
	public int start;
	public Color temp = Color.white;

	public float rVal;
	public float gVal;
	public float bVal;

	public float gravity;
	public float maxScale = 0;
	public float realMax = 0;


	public GameObject cube;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		rend.enabled = true;

		/* Spawn another cube next to itself */
		//var go = Instantiate (cube, Vector3.zero, Quaternion.identity) as GameObject;
		//go.AddComponent<TestCube>();

		//var pos = go.transform.position;
		//pos.x = 2;
		//pos.y = this.transform.position.y;
		//pos.z = this.transform.position.z;
		//go.transform.position = pos;
	}
	
	// Update is called once per frame
	//float i = 255;

	void Update () {

		if (realMax < maxScale)
			realMax = maxScale;

		if (maxScale < AudioObj.freqBand[band] * scale) {
			maxScale = AudioObj.freqBand[band] * scale;

		} else {
			maxScale -= gravity;
		}

		/* Scale the y value with the frequency, and move the position up by half the y value so that it stays in the same spot */
		//transform.localScale = new Vector3 (transform.localScale.x, maxScale + start, transform.localScale.z);
		transform.localScale = new Vector3 (maxScale + start, maxScale + start, maxScale + start);
		//var pos = transform.position;
		//pos.y = 0 + transform.localScale.y / 2;
		//transform.position = pos;


		/* Change color, depending on which band */
		/*
		R: 255, 0, 0
		O: 255, 127, 0
		Y: 255, 255, 0
		G: 0, 255, 0
		B: 0, 0, 255
		I: 75, 0, 130
		V: 148, 0, 211

		*/
		if (realMax < 1.2f)
			realMax = 1.2f;
		float maxScale1 = maxScale / realMax;

		temp.r = 0;
		temp.g = 0;
		temp.b = 0;

		switch (band) {
		case 0:
			temp.r = maxScale1;
			break;
		case 1:
			temp.r = maxScale1;
			temp.g = maxScale1 * 127 / 256;

			break;
		case 2:
			temp.r = maxScale1;
			temp.g = maxScale1;
			break;
		case 3:
			temp.g = maxScale1;
			break;
		case 4:
			temp.b = maxScale1;
			break;
		case 5:
			temp.r = maxScale1 * 75 / 256;
			temp.b = maxScale1 * 130 / 256;
			break;
		case 6:
			temp.r = maxScale1 * 148 / 256;
			temp.b = maxScale1 * 211 / 256;
			break;
		case 7:
			temp.r = maxScale1;
			break;
		}

		rVal = temp.r;
		gVal = temp.g;
		bVal = temp.b;

		//rend.material.color = temp;
	}
}
