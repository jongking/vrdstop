using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Video.Model;
using Color = UnityEngine.Color;

namespace Assets.Scripts
{
    class Test_ScreenCapture : MonoBehaviour
    {
        private Material m;
        private int desktopwidth = 0;
        private int desktopheight = 0;
        //public UnityEngine.UI.Image PlaneImage;
        void Start()
        {
            //UnityEngine.UI.Image PlaneImage = GetComponent<UnityEngine.UI.Image>();
            m = gameObject.GetComponent<Renderer>().material;
            Screen.fullScreen = false;
            //桌面赋值
            sc = new GdiScreenCapture();
            Image desktopImage = sc.CaptureWindowSBS();
            desktopwidth = desktopImage.Width;
            desktopheight = desktopImage.Height;
            Debug.Log("Start");
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
                Debug.Log("Update2 " + desktopwidth + "|" + desktopheight);
                if(sc == null)
                    sc = new GdiScreenCapture();
                Image img1 = null;
                img1 = sc.CaptureWindowSBS(desktopwidth, desktopheight);

                if (texture == null)
                    texture = new Texture2D(img1.Width, img1.Height);

                var bimage = sc.PhotoImageInsert(img1);
                //Debug.Log(img1.Width + " " + img1.Height + " " + bimage.Length);
                img1.Dispose();
                img1 = null;
                texture.LoadImage(bimage);
                m.SetTexture("_MainTex", texture);
                //Debug.Log(" " + bimage.Length);
                //m.mainTexture = texture;
                //PlaneImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                //PlaneImage.gameObject.SetActive(true);
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
            else if (Input.GetKeyDown(KeyCode.W))
            {
                desktopheight += 200;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                desktopheight -= 200;
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
            //    gameObject.transform.localEulerAngles = new Vector3(left.transform.localEulerAngles.x, left.transform.localEulerAngles.y - 45);
            //}
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                Debug.Log("F2");
                SceneManager.LoadScene("PlaneSBSScene");
            }
            else if (Input.GetKeyDown(KeyCode.F4))
            {
                Debug.Log("F4");
                SceneManager.LoadScene("UVScene");
            }
        }
        void End()
        {
        }
    }
}
