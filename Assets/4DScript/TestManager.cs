using UnityEngine;
using System.Collections;
using System;
using FRL.IO;


namespace FRL.IO.FourD
{
	public class TestManager : MonoBehaviour
	{
	
		public RotateByDegree HyperCube;
		public RotateByDegree SampleCube;
		public int[] srcDegrees;
		public int[] targetDegrees;
		public int[] difficulty;
		public int level = 0;
		System.Random random;
		// Use this for initialization
		void Start ()
		{
			random = new System.Random ();
			targetDegrees = new int[6];
			Medium ();
			SampleCube.setDegrees (targetDegrees);

		}
	
		// Update is called once per frame
		void Update ()
		{
			SampleCube.gameObject.SetActive (false);
			HyperCube.gameObject.SetActive (true);
			if (Input.GetKey(KeyCode.Space)){
				SampleCube.gameObject.SetActive (true);
				HyperCube.gameObject.SetActive (false);
			}
		}



		void setSample(){
			for (int i = 0; i < difficulty.Length; i++)
				targetDegrees [difficulty[i]] = random.Next (360);
		}

		void Easy(){
			difficulty = new int[1];
			for (int i = 0; i< 1; i++) {
				difficulty[i] = random.Next (6);
			}
			setSample ();
		}

		void Medium(){
			difficulty = new int[3];
			for (int i = 0; i< 3; i++) {
				difficulty[i] = random.Next (6);
			}
			setSample ();
		}
		void Hard(){
			difficulty = new int[4];
			for (int i = 0; i< 4; i++) {
				difficulty[i] = random.Next (6);
			}
			setSample ();
		}
	}
}
