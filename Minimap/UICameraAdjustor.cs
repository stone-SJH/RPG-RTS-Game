﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof(UICamera))]
public class UICameraAdjustor : MonoBehaviour {

	float standard_width = 1024f;
	float standard_height = 600f;
	float device_width = 0f;
	float device_height = 0f;

	private void SetCameraSize(){
		float adjustor = 0f;
		float standard_aspect = standard_width / standard_height;
		float device_aspect = device_width / device_height;

		if (device_aspect < standard_aspect){
				adjustor = standard_aspect / device_aspect;
				GetComponent<Camera>().orthographicSize = adjustor;
		}
	}

	void Awake(){
		device_width = Screen.width;
		device_height = Screen.height;

		SetCameraSize();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
