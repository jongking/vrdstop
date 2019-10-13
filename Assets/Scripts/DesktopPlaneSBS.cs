using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Video.Model;

namespace Assets.Scripts
{
    class DesktopPlaneSBS : MonoBehaviour
    {
        private Material m;
        Mesh mesh;
        private int desktopwidth = 0;
        private int desktopheight = 0;
        float g_threshold = 0.4f;//初始曲率
        float g_perfoot = 0.2f;//步幅
        private int autosleeptime = 2;
        private int Milliseconds = 22;

        void Awake()
        {
            Application.targetFrameRate = -1;
        }

        void OnDestroy()
        {
            if (ct1 != null)
            {
                ct1.Abort();
            }
            if (ct2 != null)
            {
                ct2.Abort();
            }
            if (ct3 != null)
            {
                ct3.Abort();
            }
            if (bytelist.Count > 0)
            {
                for (int i = 0; i < bytelist.Count; i++)
                {
                    bytelist.RemoveAt(0);
                }
            }
        }

        private Thread ct1;
        private Thread ct2;
        private Thread ct3;
        void Start()
        {
            gameObject.AddComponent<MeshFilter>();
            mesh = CreateArcSurface(g_threshold);
            this.GetComponent<MeshFilter>().mesh = mesh;
            m = gameObject.GetComponent<Renderer>().material;
            m.shader = Shader.Find("Sprites/Default");

            Screen.fullScreen = false;
            //桌面赋值
            sc = new GdiScreenCapture();
            Image desktopImage = sc.CaptureWindowUV(0, 0);
            desktopwidth = desktopImage.Width;
            desktopheight = desktopImage.Height;
            desktopImage.Dispose();
            desktopImage = null;

            //新建线程
            ct1 = new Thread(CaptureWindowThread);
            ct1.Start();
            //ct2 = new Thread(CaptureWindowThread);
            //ct2.Start();
            //ct3 = new Thread(CaptureWindowThread);
            //ct3.Start();
        }

        private Texture2D texture;
        private GdiScreenCapture sc;
        void Update()
        {
            if (m)
            {
                Debug.Log("Update " + bytelist.Count + " " + autosleeptime);
                KeyDownEvent();
                //if (sc == null)
                //    sc = new GdiScreenCapture();
                //Image img1 = null;
                //img1 = sc.CaptureWindowSBS(desktopwidth, desktopheight);

                if (texture == null)
                    texture = new Texture2D(desktopwidth, desktopheight);

                //var bimage = sc.PhotoImageInsert(img1);
                if (bytelist.Count > 0)
                {
                    var bimage = bytelist[0];
                    texture.LoadImage(bimage);
                    m.mainTexture = texture;
                    bytelist.RemoveAt(0);
                    bimage = null;
                }
                //img1.Dispose();
                //img1 = null;
                //PlaneImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                //PlaneImage.gameObject.SetActive(true);
            }
        }

        DateTime CaptureTime = DateTime.Now;
        List<byte[]> bytelist = new List<byte[]>();
        void CaptureWindowThread()
        {
            var gsc = new GdiScreenCapture();
            while (true)
            {
                //if (bytelist.Count == 0)
                //{
                //    autosleeptime -= 2;
                //}
                //else
                //{
                //    autosleeptime += 2;
                //}
                //用于控制每间隔n毫秒执行一次截图
                if (CaptureTime.AddMilliseconds(Milliseconds) <= DateTime.Now)
                {
                    CaptureTime = DateTime.Now;
                    Image img1 = gsc.CaptureWindowSBS(desktopwidth, desktopheight);
                    //imagelist.Add(img1);
                    var bimage = gsc.PhotoImageInsert(img1);
                    bytelist.Add(bimage);
                    img1.Dispose();
                    img1 = null;
                    Thread.Sleep(autosleeptime);
                }
                //else
                //{
                //    Thread.Sleep(autosleeptime);
                //}
            }
        }

        void KeyDownEvent()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                g_threshold += 0.2f;
                mesh = CreateArcSurface(g_threshold);
                this.GetComponent<MeshFilter>().mesh = mesh;
            }
            else
            if (Input.GetKeyDown(KeyCode.A))
            {
                g_threshold -= 0.2f;
                mesh = CreateArcSurface(g_threshold);
                this.GetComponent<MeshFilter>().mesh = mesh;
            }
            else
            if (Input.GetKeyDown(KeyCode.W))
            {
                var playerPostion = this.GetComponent<Transform>().position;
                this.GetComponent<Transform>().position = new Vector3(playerPostion.x, playerPostion.y + g_perfoot, playerPostion.z);
            }
            else
            if (Input.GetKeyDown(KeyCode.S))
            {
                var playerPostion = this.GetComponent<Transform>().position;
                this.GetComponent<Transform>().position = new Vector3(playerPostion.x, playerPostion.y - g_perfoot, playerPostion.z);
            }
            else
            if (Input.GetKeyDown(KeyCode.E))
            {
                var playerPostion = this.GetComponent<Transform>().position;
                this.GetComponent<Transform>().position = new Vector3(playerPostion.x, playerPostion.y, playerPostion.z - g_perfoot);
            }
            else
            if (Input.GetKeyDown(KeyCode.D))
            {
                var playerPostion = this.GetComponent<Transform>().position;
                this.GetComponent<Transform>().position = new Vector3(playerPostion.x, playerPostion.y, playerPostion.z + g_perfoot);
            }
            else
            if (Input.GetKeyDown(KeyCode.R))
            {
                var playerPostion = this.GetComponent<Transform>().position;
                this.GetComponent<Transform>().position = new Vector3(playerPostion.x - g_perfoot, playerPostion.y, playerPostion.z);
            }
            else
            if (Input.GetKeyDown(KeyCode.F))
            {
                var playerPostion = this.GetComponent<Transform>().position;
                this.GetComponent<Transform>().position = new Vector3(playerPostion.x + g_perfoot, playerPostion.y, playerPostion.z);
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                Debug.Log("F3");
                SceneManager.LoadScene("SampleScene");
            }
            //else if (Input.GetKeyDown(KeyCode.F4))
            //{
            //    Debug.Log("F4");
            //    SceneManager.LoadScene("UVScene");
            //}
        }
        private Mesh CreateArcSurface(float threshold)
        {
            float planeWidth = 16;
            float planeHeight = 9;
            int planesSeg = 64;
            int threshold2 = 1;

            Mesh mesh = new Mesh();

            int segments_count = planesSeg;
            int vertex_count = (segments_count + 1) * 2;

            Vector3[] vertices = new Vector3[vertex_count];

            int vi = 0;

            // 普通平面步
            float widthSetup = planeWidth * 1.0f / segments_count;

            // 半径
            float r = planeWidth * 1.0f / (Mathf.Sin(threshold / 2) * 2);
            float r2 = planeWidth * 1.0f / (Mathf.Sin(threshold2 / 2) * 2);

            // 弧度步
            float angleSetup = threshold / planesSeg;
            float angleSetup2 = threshold2 / planesSeg;

            // 余角
            float coangle = (Mathf.PI - threshold) / 2;
            float coangle2 = (Mathf.PI - threshold2) / 2;

            // 弓形的高度
            // https://zh.wikipedia.org/wiki/%E5%BC%93%E5%BD%A2
            float h = r - (r * Mathf.Cos(threshold / 2));
            float h2 = r - (r * Mathf.Cos(threshold2 / 2));

            // 弓形高度差值（半径-高度）
            float diff = r - h;

            for (int si = 0; si <= segments_count; si++)
            {
                float x = 0;

                float z = 0;

                if (threshold == 0)
                {
                    // 阈值为0时,按照普通平面设置顶点
                    x = widthSetup * si;

                    vertices[vi++] = new Vector3(-planeWidth / 2 + x, planeHeight / 2, z);

                    vertices[vi++] = new Vector3(-planeWidth / 2 + x, -planeHeight / 2, z);
                }
                else
                {
                    // 阈值不为0时,根据圆的几何性质计算弧上一点
                    // https://zh.wikipedia.org/wiki/%E5%9C%86
                    x = r * Mathf.Cos(coangle + angleSetup * si);
                    z = r * Mathf.Sin(coangle + angleSetup * si);

                    vertices[vi++] = new Vector3(-x, planeHeight / 2, z - diff);
                    vertices[vi++] = new Vector3(-x, -planeHeight / 2, z - diff);
                }
            }

            int indices_count = segments_count * 3 * 2;
            int[] indices = new int[indices_count];

            int vert = 0;
            int idx = 0;
            for (int si = 0; si < segments_count; si++)
            {
                indices[idx++] = vert + 1;
                indices[idx++] = vert;
                indices[idx++] = vert + 3;

                indices[idx++] = vert;
                indices[idx++] = vert + 2;
                indices[idx++] = vert + 3;

                vert += 2;
            }

            mesh.vertices = vertices;
            mesh.triangles = indices;

            // // https://answers.unity.com/questions/154324/how-do-uvs-work.html
            Vector2[] uv = new Vector2[vertices.Length];

            float uvSetup = 1.0f / segments_count;

            int iduv = 0;
            for (int i = 0; i < uv.Length; i = i + 2)
            {
                uv[i] = new Vector2(uvSetup * iduv, 1);
                uv[i + 1] = new Vector2(uvSetup * iduv, 0);
                iduv++;
            }

            mesh.uv = uv;

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();

            return mesh;
        }
    }
}
