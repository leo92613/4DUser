using UnityEngine;
using System.Collections;
using System;
using FRL.IO;


namespace FRL.IO.FourD
{
    public class Test3Manager : MonoBehaviour
    {

        public KeyBoardRotation HyperCube;
        public RotateByDegree SampleCube;
        public int[] srcDegrees;
        public int[] targetDegrees;
        public int[] difficulty;
        public int level = -2;
        public bool toggle = true;
        System.Random random;
        public float timer = 0;
        public GameObject GUI;

        public float value;
        // Use this for initialization
        void Start()
        {
            //difficulty = new int[4] { 0, 2, 3, 5 };
            difficulty = new int[6] { 0,1,2,3,4,5 };
            random = new System.Random();
            targetDegrees = new int[6];
            //Medium ();
            setSample();
            SampleCube.setDegrees(targetDegrees);
            while (getDifferent() < 2.0f)
            {
                setSample();
                SampleCube.setDegrees(targetDegrees);
            }
            SampleCube.set(false);
            // SampleCube.gameObject.SetActive(false);
            // HyperCube.gameObject.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            //SampleCube.gameObject.SetActive (false);
            //HyperCube.gameObject.SetActive (true);
            if (Input.GetKeyDown(KeyCode.F12))
            {
                reset();
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                HyperCube.reset();
            }
            //if (Time.time - timer > 600f)
            //{

            //    Debug.Log("Used up to 10 mins!");
            //    Debug.Log(Time.time - timer);
            //    timer = Time.time;
            //    GUI.SetActive(true);
            //    HyperCube.set(false);
            //}
        }

        public void reset()
        {
            value = float.MaxValue;
            level++;
            if (level > 3)
                Application.LoadLevel(1);
            SampleCube.reset();
            setSample();
            SampleCube.setDegrees(targetDegrees);
            HyperCube.reset();
            timer = 0f;
            SampleCube.set(false);
            HyperCube.set(true);
            GUI.SetActive(false);
        }

        public void CheckTarget()
        {

            SampleCube.gameObject.SetActive(toggle);
            HyperCube.gameObject.SetActive(!toggle);
            toggle = !toggle;
        }

        void setSample()
        {
            switch (level)
            {
                case -2:
                    for (int i = 0; i < difficulty.Length; i++)
                        targetDegrees[difficulty[i]] = random.Next(360);
                    break;

                case -1:
                    break;

                case 0:
                    targetDegrees = new int[6] { 60, 352, 0, 194, 0, 0 };
                    break;
                case 1:
                    targetDegrees = new int[6] { 116, 29, 237, 0, 0, 0 };
                    break;
                case 2:
                    targetDegrees = new int[6] { 7, 319, 83, 0, 58, 0 };
                    break;
                case 3:
                    targetDegrees = new int[6] { 3, 89, 71, 205, 9, 101 };
                    break;
            }
        }

        void Easy()
        {
            difficulty = new int[1];
            for (int i = 0; i < 1; i++)
            {
                difficulty[i] = random.Next(6);
            }
            setSample();
        }

        void Medium()
        {
            difficulty = new int[3];
            for (int i = 0; i < 3; i++)
            {
                difficulty[i] = random.Next(6);
            }
            setSample();
        }
        void Hard()
        {
            difficulty = new int[4];
            for (int i = 0; i < 4; i++)
            {
                difficulty[i] = random.Next(6);
            }
            setSample();
        }


        float getDifferent()
        {
            Vector3[] Current = HyperCube.returnVertices();
            Vector3[] Target = SampleCube.returnVertices();
            float value = 0.0f;
            for (int i = 0; i < Current.Length; i++)
            {
                float different = float.MaxValue;
                for (int j = 0; j < Target.Length; j++)
                {
                    if (different > Vector4.Distance(Current[i], Target[j]))
                    {
                        different = Vector4.Distance(Current[i], Target[j]);
                    }
                }
                value += different;
            }
            return value;
        }

        public void showTarget()
        {
            bool b = SampleCube.gameObject.activeSelf;
            SampleCube.gameObject.SetActive(!b);
        }
        public void CompareVertex()
        {
            if (timer == 0)
            {
                timer = Time.time;
            }
            value = getDifferent();
            //Debug.Log(value);
                if (value < 0.5f)
            {

                timer = Time.time - timer;
                Debug.Log(timer);
                //Debug.Break();
                GUI.SetActive(true);
                HyperCube.set(false);
               // reset();

            }

        }
    }
}
