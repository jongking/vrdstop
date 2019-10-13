using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Video.Model;
using Color = UnityEngine.Color;

namespace Assets.Scripts
{
    class DesktopUVs : MonoBehaviour
    {
        private Material m;
        private int desktopwidth = 0;
        private int desktopheight = 0;
        private int orgindesktopwidth = 0;
        private int orgindesktopheight = 0;
        //public UnityEngine.UI.Image PlaneImage;

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
            //UnityEngine.UI.Image PlaneImage = GetComponent<UnityEngine.UI.Image>();
            m = gameObject.GetComponent<Renderer>().material;
            Screen.fullScreen = false;
            //桌面赋值
            sc = new GdiScreenCapture();
            Image desktopImage = sc.CaptureWindowUV(0, 0);
            desktopwidth = desktopImage.Width;
            desktopheight = desktopImage.Height;
            orgindesktopwidth = desktopImage.Width;
            orgindesktopheight = desktopImage.Height;
            desktopImage.Dispose();
            desktopImage = null;
            Debug.Log("Start");

            //新建线程
            ct1 = new Thread(CaptureWindowThread);
            ct1.Start();
            ct2 = new Thread(CaptureWindowThread);
            ct2.Start();
            ct3 = new Thread(CaptureWindowThread);
            ct3.Start();
        }

        private Texture2D texture;
        private GdiScreenCapture sc;
        void Update()
        {
            //UnityEngine.UI.Image PlaneImage = GetComponent<UnityEngine.UI.Image>();
            Debug.Log("Update");
            if (m)
            {
                KeyDownEvent();
                //Debug.Log("Update2 " + desktopwidth + "|" + desktopheight);
                //if(sc == null)
                //    sc = new GdiScreenCapture();
                //Image img1 = null;
                //img1 = sc.CaptureWindowUV(desktopwidth, desktopheight);

                if (texture == null)
                    texture = new Texture2D(desktopwidth, desktopheight);

                //var bimage = sc.PhotoImageInsert(img1);
                //img1.Dispose();
                //img1 = null;
                //texture.LoadImage(bimage);
                //m.SetTexture("_MainTex", texture);
                if (bytelist.Count > 0)
                {
                    Debug.Log("Update" + bytelist.Count);
                    var bimage = bytelist[0];
                    texture.LoadImage(bimage);
                    m.SetTexture("_MainTex", texture);
                    bytelist.RemoveAt(0);
                    bimage = null;
                }
            }
        }

        List<byte[]> bytelist = new List<byte[]>();
        void CaptureWindowThread()
        {
            while (true)
            {
                var gsc = new GdiScreenCapture();
                Image img1 = gsc.CaptureWindowSBS(desktopwidth, desktopheight);
                var bimage = gsc.PhotoImageInsert(img1);
                bytelist.Add(bimage);
                img1.Dispose();
                img1 = null;
                Thread.Sleep(20);
            }
        }

        void KeyDownEvent()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                desktopwidth += 200;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                desktopwidth -= 200;
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                if (desktopwidth != orgindesktopwidth)
                {
                    desktopwidth = orgindesktopwidth;
                }
                else
                {
                    desktopwidth = orgindesktopwidth + orgindesktopwidth;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                desktopheight += 200;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                desktopheight -= 200;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                if (desktopheight != orgindesktopheight)
                {
                    desktopheight = orgindesktopheight;
                }
                else
                {
                    desktopheight = orgindesktopheight + orgindesktopheight;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x - 15, gameObject.transform.localEulerAngles.y);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x + 15, gameObject.transform.localEulerAngles.y);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y - 15);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y + 15);
            }
            //else if (Input.GetKeyDown(KeyCode.F4))
            //{
            //    Debug.Log("F4");
            //    var left = GameObject.Find("Left Camera");
            //    gameObject.transform.localEulerAngles = left.transform.localEulerAngles;
            //    var right = GameObject.Find("Right Camera");
            //    gameObject.transform.localEulerAngles = right.transform.localEulerAngles;
            //}
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                Debug.Log("F2");
                SceneManager.LoadScene("PlaneSBSScene");
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                Debug.Log("F3");
                SceneManager.LoadScene("SampleScene");
            }
        }
        void End()
        {
        }
    }
}
