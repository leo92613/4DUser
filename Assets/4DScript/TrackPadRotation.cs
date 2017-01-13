using UnityEngine;
using System.Collections;
using System;
using FRL.IO;

namespace FRL.IO.FourD
{
    public class TrackPadRotation : GlobalReceiver, IGlobalTouchpadTouchSetHandler,IGlobalTouchpadPressDownHandler
    {

        //public SphereCollider trackballArea;
        //public BoxCollider interactionArea;

        private Transform box;
        private HyperCube cube;
        private Trackball trackball = new Trackball(4);

        private Vector3 A_ = new Vector3();
        private Vector3 B_ = new Vector3();
        private Vector4 A = new Vector4();
        private Vector4 B = new Vector4();

        private float radius;
        private bool isTriggerPressed = false;

        public int tag = 0;
        public bool type = true;
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

        Vector4 ReturnVector(Vector3 vector, bool type)
        {
            radius = 1f;
            Vector4 result;
            //Vector3 relapos = new Vector3();
            //relapos = (vector - box.position) * 8f / 3f;
            //float r = (float)Math.Sqrt(relapos.x * relapos.x + relapos.y * relapos.y + relapos.z * relapos.z);
            //if (r < radius)
            //{
            //    result = new Vector4(relapos.x, relapos.y, relapos.z, (float)Math.Sqrt(radius * radius - relapos.x * relapos.x - relapos.y * relapos.y - relapos.z * relapos.z));
            //}
            //else
            //{
            //    Vector3 Q = (radius / r) * relapos;
            //    relapos = Q + box.position;
            //    result = new Vector4(Q.x, Q.y, Q.z, 0f);
            //}
            if (type)
                result = new Vector4(vector.x, vector.y, vector.z, 0.0f);
            else
                result = new Vector4(vector.x, vector.y, 0.0f, vector.z);
            return result;
        }

        // Update is called once per frame
        void Update()
        {
            if (isTriggerPressed)
            {
                A = ReturnVector(A_,type);
                B = ReturnVector(B_,type);
                UpdateRotation(cube, trackball, A, B);
                testmanager.CompareVertex();
                A_ = B_;
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
                cube.updatepoint4(dst, i);
                cube.update_edges();
            }
        }

        public HyperCube returnCube()
        {
            return cube;
        }
       

        void IGlobalTouchpadTouchDownHandler.OnGlobalTouchpadTouchDown(ViveControllerModule.EventData eventData)
        {
            isTriggerPressed = true;
            Vector2 tmp = eventData.touchpadAxis;
            float mag = Mathf.Clamp01(tmp.SqrMagnitude());
            B_ = new Vector3(tmp.x, tmp.y, mag);
            A_ = new Vector3(tmp.x, tmp.y, mag);
        }

        void IGlobalTouchpadTouchHandler.OnGlobalTouchpadTouch(ViveControllerModule.EventData eventData)
        {
                Vector2 tmp = eventData.touchpadAxis;
            float mag = Mathf.Clamp01(tmp.SqrMagnitude());
                B_ = new Vector3(tmp.x, tmp.y, mag);
        }

        void IGlobalTouchpadTouchUpHandler.OnGlobalTouchpadTouchUp(ViveControllerModule.EventData eventData)
        {
            isTriggerPressed = false;
        }

        void IGlobalTouchpadPressDownHandler.OnGlobalTouchpadPressDown(ViveControllerModule.EventData eventData)
        {
            type = !type;
        }
    }
}