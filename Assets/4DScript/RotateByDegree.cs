using UnityEngine;
using System.Collections;
using System;
using FRL.IO;

namespace FRL.IO.FourD
{

    public class RotateByDegree : MonoBehaviour
    {
        private rotation rot;
        private HyperCube cube;
        private int degreeXY,degreeXZ,degreeXW,degreeYZ,degreeYW, degreeZW = 0;
        private int[] degrees;
        // Use this for initialization

        void UpdateRotation()
        {
            rot.createMat(degrees);
            for (int i = 0; i < 16; i++)
            {
                float[] src = new float[4];
                src[0] = cube.srcVertices[i].x;
                src[1] = cube.srcVertices[i].y;
                src[2] = cube.srcVertices[i].z;
                src[3] = cube.srcVertices[i].w;
                float[] dst = new float[4];
                rot.transform(src, dst);
                //Debug.Log(dst[0]);
                //Debug.Log(dst[1]);
                //Debug.Log(dst[2]);
                //Debug.Log(dst[3]);
                cube.updatepoint4(dst, i);
                cube.update_edges();
                //Debug.Log(rot.toString());
            }
        }

        void Start()
        {
            degrees = new int[6];
            cube = this.GetComponent<HyperCubeOBJ>().returnCube();
            rot = new rotation();
        }
        void showDegree()
        {
            string s = "{";
            for(int i = 0; i < 6; i++)
            {
                s += degrees[i];
                s += ",";
            }
            s += "}";
            Debug.Log(s);
        }

        // Update is called once per frame
        void Update()
        {
            showDegree();
            if(Input.GetKey(KeyCode.Space))
            {
                //Debug.Log("Key Pressed");
                //degreeXW += 6;
                degrees[2] = (degrees[2] + 6) % 360;
                //rot.createRot("xw",degreeXW);
                UpdateRotation();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //Debug.Log("Key Pressed");
                degrees[0] = (degrees[0]+6)% 2880;
                //rot.createRot("xy", degreeXY);
                UpdateRotation();
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //Debug.Log("Key Pressed");
                //degreeXY -= 6;
                degrees[0] = (degrees[0] - 6) % 360;
                //rot.createRot("xy", degreeXY);
                UpdateRotation();
            }
            if (Input.GetKey(KeyCode.A))
            {
                //Debug.Log("Key Pressed");
                //degreeXZ += 6;
                degrees[1] = (degrees[1] + 6) % 360;
                //rot.createRot("xz", degreeXZ);
                UpdateRotation();
            }
            if (Input.GetKey(KeyCode.D))
            {
                //Debug.Log("Key Pressed");
                //degreeXZ -= 6;
                degrees[1] = (degrees[1] - 6) % 360;
                //rot.createRot("xz", degreeXZ);
                UpdateRotation();
            }
           
        }
    }

    internal class rotation
    {
        float[,] mat, rot, tmp;
        const int size = 4;

        enum axis
        {
            xy = 1,
            xz = 2,
            xw = 3,
            yz = 4,
            yw = 5,
            zw = 6
        }

        public rotation()
        {
            mat = new float[size, size];
            rot = new float[size, size];
            tmp = new float[size, size];
            identity();
        }

        public void createMat(int[] degrees)
        {
            identity(mat);
            int degree = 3;
            bool end = false;
            while (!end)
            {
                end = true;
                for (int i = 0; i < 6; i++)
                {
                    if (degrees[i] > degree)
                    {
                        end = false;
                        createRot(i + 1, 3.0f);
                    }
                }
                degree += 3;
            }
        }

        public void createRot(string type,float degree)
        {
            int t = (int)Enum.Parse(typeof(axis), type);
            Debug.Log(t);
            createRot(t,degree);
        }

        public void createRot(int type,float degree)
        {
            float cos = Mathf.Cos(degree / (180.0f * 3.14f));
            float sin = Mathf.Sin(degree / (180.0f * 3.14f));
            switch (type)
            {
                case 1:
                    identity(rot);
                    rot[0, 0] = cos;
                    rot[0, 1] = -sin;
                    rot[1, 0] = sin;
                    rot[1, 1] = cos;
                    break;
                case 2:
                    identity(rot);
                    rot[0, 0] = cos;
                    rot[0, 2] = -sin;
                    rot[2, 0] = sin;
                    rot[2, 2] = cos;
                    break;
                case 3:
                    identity(rot);
                    rot[0, 0] = cos;
                    rot[0, 3] = -sin;
                    rot[3, 0] = sin;
                    rot[3, 3] = cos;
                    break;
                case 4:
                    identity(rot);
                    rot[1, 1] = cos;
                    rot[1, 2] = -sin;
                    rot[2, 1] = sin;
                    rot[2, 2] = cos;
                    break;
                case 5:
                    identity(rot);
                    rot[2, 2] = cos;
                    rot[2, 3] = -sin;
                    rot[3, 2] = sin;
                    rot[3, 3] = cos;
                    break;
                case 6:
                    identity(rot);
                    rot[2, 2] =cos;
                    rot[2, 3] = - sin;
                    rot[3, 2] = sin;
                    rot[3, 3] = cos;
                    break;
                default:
                    identity(rot);
                    break;
            }
            multiply(rot);
        }

        public string toString()
        {
            return toString(mat);
        }

        public string toString(float[,] mat)
        {
            string s = "{ ";
            for (int row = 0; row < size; row++)
            {
                s += "{";
                for (int col = 0; col < size; col++)
                    s += round(mat[row, col]) + ",";
                s += "},";
            }
            s += " }";
            return s;
        }

        public void identity()
        {
            identity(mat);
        }

        public void identity(float[,] mat)
        {
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                    mat[row, col] = row == col ? 1.0f : 0.0f;
        }
     
        public void multiply(float[,] src)
        {
            multiply(src, mat, tmp);
            copy(tmp, mat);
        }

        public void multiply(float[,] a, float[,] b, float[,] dst)
        {
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                {
                    dst[row, col] = 0.0f;
                    for (int k = 0; k < size; k++)
                        dst[row, col] += a[row, k] * b[k, col];
                }
        }

        public void transform(float[] src, float[] dst)
        {
            transform(mat, src, dst);
        }

        public void transform(float[,] mat, float[] src, float[] dst)
        {
            for (int row = 0; row < size; row++)
            {
                dst[row] = 0.0f;
                for (int col = 0; col < size; col++)
                    dst[row] += mat[row, col] * src[col];
            }
        }

        public void copy(float[,] src, float[,] dst)
        {
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                    dst[row, col] = src[row, col];
        }

        public float dot(float[] a, float[] b)
        {
            float t = 0.0f;
            for (int k = 0; k < size; k++)
                t += a[k] * b[k];
            return t;
        }

        public void normalize(float[,] a, int i, float[] b)
        {
            float s = (float)Math.Sqrt(dot(b, b));
            for (int k = 0; k < size; k++)
                a[i, k] /= s;
        }

        public string round(float t)
        {
            return "" + ((int)(t * 1000) / 1000.0f);
        }

        public void transpose(float[,] src, float[,] dst)
        {
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                    dst[col, row] = src[row, col];
        }
    }
}
