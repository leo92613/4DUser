using UnityEngine;
using System.Collections;
using System;
using FRL.IO;

namespace FRL.IO.FourD
{
    public class KeyBoardRotation : MonoBehaviour
    {

        //public SphereCollider trackballArea;
        //public BoxCollider interactionArea;

        private Transform box;
        private HyperCube cube;
        private Trackball trackball = new Trackball(4);

        public Vector3 A_ = new Vector3();
        private Vector3 B_ = new Vector3();
        public Vector4 A = new Vector4();
        public Vector4 B = new Vector4();

        private float radius;
        public bool isTriggerPressed = false;

        public int tag = 0;
        public float eps = 0.01f;
        public int type = 0;
        public TestManager testmanager;
        void Awake()
        {
            box = this.GetComponent<Transform>();
            cube = new HyperCube(box, tag);
            UpdateRotation(cube, trackball, A, B);
        }


        public Vector4[] returnVertices()
        {
            int size = cube.size / 2;
            Vector4[] rst = new Vector4[size];
            //Debug.Log(size);
            for (int i = 0; i < size; i++)
            {
                rst[i] = new Vector4(cube.vertices[i].x, cube.vertices[i].y, cube.vertices[i].z, cube.vertices[i].w);
            }
            return rst;
        }

        Vector4 ReturnVector(Vector3 vector, int type)
        {
            Vector4 result = new Vector4();
            switch (type) {
                case 0:
                result = new Vector4(vector.x, vector.y, vector.z, 0.0f);
                    break;
                case 1:
                result = new Vector4(vector.x, vector.y, 0.0f, vector.z);
                    break;
                case 2:
                    result = new Vector4(vector.x, vector.y, 0.0f, vector.z);
                    break;
                case 3:
                    result = new Vector4(0f,0f,  vector.z,vector.y);
                    break;
        }
            return result;
        }

        // Update is called once per frame
        void Update()
        {
            isTriggerPressed = false;
           
            //if (Input.GetKey(KeyCode.LeftArrow))
            //{
            //    type = true;
            //    isTriggerPressed = true;
            //    Vector2 tmp = new Vector2(-eps, 0f);
            //    float mag = Mathf.Clamp01(tmp.SqrMagnitude());
            //    B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);
            //}
            //if (Input.GetKey(KeyCode.RightArrow))
            //{
            //    type = true;
            //    isTriggerPressed = true;
            //    Vector2 tmp = new Vector2(eps, 0f);
            //    float mag = Mathf.Clamp01(tmp.SqrMagnitude());
            //    B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            //}
            //if (Input.GetKey(KeyCode.UpArrow))
            //{
            //    type = true;
            //    isTriggerPressed = true;
            //    Vector2 tmp = new Vector2(0f, -eps);
            //    float mag = Mathf.Clamp01(tmp.SqrMagnitude());
            //    B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            //}
            //if (Input.GetKey(KeyCode.DownArrow))
            //{
            //   // Debug.Log("Key down");
            //    type = true;
            //    isTriggerPressed = true;
            //    Vector2 tmp = new Vector2(0f, eps);
            //    float mag = Mathf.Clamp01(tmp.SqrMagnitude());
            //    B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            //}
            //if (Input.GetKey(KeyCode.D))
            //{
            //    type = false;
            //    isTriggerPressed = true;
            //    Vector2 tmp = new Vector2(eps, 0f);
            //    float mag = Mathf.Clamp01(tmp.SqrMagnitude());
            //    B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            //}
            //if (Input.GetKey(KeyCode.A))
            //{
            //    type = false;
            //    isTriggerPressed = true;
            //    Vector2 tmp = new Vector2(-eps, 0f);
            //    float mag = Mathf.Clamp01(tmp.SqrMagnitude());
            //    B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            //}
            //if (Input.GetKey(KeyCode.S))
            //{
            //    type = false;
            //    isTriggerPressed = true;
            //    Vector2 tmp = new Vector2(0f, eps);
            //    float mag = Mathf.Clamp01(tmp.SqrMagnitude());
            //    B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            //}
            //if (Input.GetKey(KeyCode.W))
            //{
            //    type = false;
            //    isTriggerPressed = true;
            //    Vector2 tmp = new Vector2(0f, -eps);
            //    float mag = Mathf.Clamp01(tmp.SqrMagnitude());
            //    B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            //}

            if (Input.GetKey(KeyCode.W))
            {
                type = 2;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(0f, -eps);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(1.0f - mag, tmp.y, 0f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                type = 2;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(0f, eps);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(1.0f - mag, tmp.y, 0f);

            }
            if (Input.GetKey(KeyCode.E))
            {
                type = 0;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(0f, -eps);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            }
            if (Input.GetKey(KeyCode.D))
            {
                // Debug.Log("Key down");
                type = 0;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(0f, eps);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            }
            if (Input.GetKey(KeyCode.R))
            {
                type = 0;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(eps, 0f);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            }
            if (Input.GetKey(KeyCode.F))
            {
                type = 0;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(-eps, 0f);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            }
            if (Input.GetKey(KeyCode.U))
            {
                type = 1;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(0f, eps);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            }
            if (Input.GetKey(KeyCode.J))
            {
                type = 1;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(0f, -eps);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            }
            if (Input.GetKey(KeyCode.I))
            {
                type = 1;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(eps, 0f);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            }
            if (Input.GetKey(KeyCode.K))
            {
                type = 1;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(-eps, 0f);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            }
            if (Input.GetKey(KeyCode.O))
            {
                type = 3;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(0f, eps);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            }
            if (Input.GetKey(KeyCode.L))
            {
                type = 3;
                isTriggerPressed = true;
                Vector2 tmp = new Vector2(0f, -eps);
                float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(-tmp.x, tmp.y, 1.0f - mag);

            }

            if (isTriggerPressed)
            {
                A_ = new Vector3(0f, 0f, 1.0f);
                A = ReturnVector(A_, type);
                if (type == 2)
                    A = new Vector4(1f, 0f, 0f,0f);
                if (type == 3)
                    A = new Vector4(0f, 0f, 1f, 0f);
                //A = new Vector4();
                B = ReturnVector(B_, type);
                UpdateRotation(cube, trackball, A, B);
                //testmanager.CompareVertex();
                //A_ = B_;
            }
        }

        void UpdateRotation(HyperCube cube, Trackball trackball, Vector4 A_, Vector4 B_)
        {
            float[] A = new float[4] { A_.x, A_.y, A_.z, A_.w };
            float[] B = new float[4] { B_.x, B_.y, B_.z, B_.w };
            trackball.rotate(A, B);
            for (int i = 0; i < 16; i++)
            {
                float[] src = new float[4];
                src[0] = cube.srcVertices[i].x;
                src[1] = cube.srcVertices[i].y;
                src[2] = cube.srcVertices[i].z;
                src[3] = cube.srcVertices[i].w;
                float[] dst = new float[4];
                trackball.transform(src, dst);
                //Debug.Log(dst[1]);
                //Debug.Log(trackball.toString());
                cube.updatepoint4(dst, i);
                cube.update_edges();
            }
        }

        public HyperCube returnCube()
        {
            return cube;
        }


    }
}
