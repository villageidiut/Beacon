﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatebymouseY : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

	void Update () {

         float mouseInput = Input.GetAxis("Mouse Y");
         Vector3 lookhere = new Vector3(0,mouseInput,0);
         transform.Rotate(lookhere);
	}
	
}
