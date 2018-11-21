using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TheCityUI : MonoBehaviour {
	 
    GameObject Texture_Map;
    Transform TM_Transform;
	public Camera MainC;
	public UILabel test;
    
    bool IsFar;
    float mapX;
    float mapY;
    float widthCheck;
    float heightCheck;
    public float LittleMapW;
    public float LittleMapH;
    public float BigMapW;
    public float BigMapH;

    Vector3 mousePos1;
    Vector3 mousePos2;
    Vector3 offset;

    float mspeed;

    void Awake()
    {
		 
        MainC = Camera.main;
        Texture_Map = GameObject.Find("Texture_Map");
        TM_Transform = Texture_Map.transform;

        mspeed = 0.0005f * Time.deltaTime;
        mousePos1 = Camera.main.WorldToScreenPoint(Input.mousePosition);

        IsFar = true;
    }

    void Start() {
		


    }


    void Update() {
        //用鼠标移动地图
        if (Input.GetMouseButton(0))
        {

            mousePos2 = Camera.main.WorldToScreenPoint(Input.mousePosition);
            offset = mousePos2 - mousePos1;
            Texture_Map.transform.Translate(offset.x * mspeed, offset.y * mspeed, 0);
            check();
        }
//        mousePos1 = Camera.main.WorldToScreenPoint(Input.mousePosition);



        //用鼠标移动摄像机
        if (Input.GetMouseButton(0))
        {
            mousePos2 = Camera.main.WorldToScreenPoint(Input.mousePosition);
            offset = mousePos2 - mousePos1;
            MainC.transform.Translate(offset.x * mspeed, offset.y * mspeed, 0);
            check();
        }
        mousePos1 = Camera.main.WorldToScreenPoint(Input.mousePosition);


		/*
        //用键盘移动地图
        if (Input.GetKey(KeyCode.W))
        {
            Texture_Map.transform.Translate(Vector3.down * mspeed * 1000);
            check();
        }
        if (Input.GetKey(KeyCode.S))
        {
            Texture_Map.transform.Translate(Vector3.up * mspeed * 1000);
            check();
        }
        if (Input.GetKey(KeyCode.A))
        {
            Texture_Map.transform.Translate(Vector3.right * mspeed * 1000);
            check();
        }
        if (Input.GetKey(KeyCode.D))
        {
            Texture_Map.transform.Translate(Vector3.left * mspeed * 1000);
            check();
        }
//        mousePos1 = Camera.main.WorldToScreenPoint(Input.mousePosition);

		*/

     //鼠标滚轮缩放2档
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.orthographicSize<1.13f)
            {
                Camera.main.orthographicSize += 0.5f;
                IsFar = true;

                widthCheck = BigMapW ;
                heightCheck = BigMapH;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.orthographicSize >0.6f)
            {
                Camera.main.orthographicSize -= 0.5f;
                IsFar = false;
                widthCheck = LittleMapW;
                heightCheck = LittleMapH;
            }
        }
    }


    void check()
    {
        mapX = Texture_Map.transform.localPosition.x;
        mapY = Texture_Map.transform.localPosition.y;
        //当超过地图范围时，重新计算坐标
        if (mapX >= widthCheck)
        {
            mapX = widthCheck;
        }
        if (mapX <= -widthCheck)
        {
            mapX = -widthCheck;
        }
        if (mapY >= heightCheck)
        {
            mapY = heightCheck;
        }
        if (mapY <= -heightCheck)
        {
            mapY = -heightCheck;
        }

        Texture_Map.transform.localPosition = new Vector3(mapX, mapY, 0);
    }




}
