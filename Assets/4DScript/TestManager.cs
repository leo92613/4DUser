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
        public bool toggle = true;
		System.Random random;
		// Use this for initialization
		void Start ()
		{
            difficulty = new int[4] { 0, 2, 3, 5 };
			random = new System.Random ();
			targetDegrees = new int[6];
            //Medium ();
            setSample();
            SampleCube.setDegrees (targetDegrees);
            SampleCube.gameObject.SetActive(false);
            HyperCube.gameObject.SetActive(true);
        }

        // Update is called once per frame
        void Update ()
		{
			//SampleCube.gameObject.SetActive (false);
			//HyperCube.gameObject.SetActive (true);
			if (Input.GetKey(KeyCode.Space)){
				SampleCube.gameObject.SetActive (true);
				HyperCube.gameObject.SetActive (false);
			}
		}

        public void CheckTarget()
        {

            SampleCube.gameObject.SetActive(toggle);
            HyperCube.gameObject.SetActive(!toggle);
            toggle = !toggle;
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

        public void CompareVertex()
        {
            Vector4[] Current = HyperCube.returnVertices();
            Vector4[] Target = SampleCube.returnVertices();
            float value = 0.0f;
            for (int i = 0; i < Current.Length; i ++)
            {
                float different = float.MaxValue;
                for (int j = 0; j< Target.Length; j++)
                {
                    if (different > Vector4.Distance(Current[i],Target[j]))
                    {
                        different = Vector4.Distance(Current[i], Target[j]);
                    }
                }
                value += different;
            }
            Debug.Log(value);
        }
	}
}
