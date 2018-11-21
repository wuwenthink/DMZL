using UnityEngine;
using System.Collections;

//用鼠标、键盘控制球体转动。
public class WorldMapControl : MonoBehaviour {
    Camera MainC;//摄像机
    float cameraMSize=5.0f;//摄像机的
    Transform WorldMapObj;
    float mspeed=0;//转动速度
	Vector3 StartPosition;  
	Vector3 previousPosition;  
	Vector3 offset;  
	Vector3 finalOffset;  
	Vector3 eulerAngle;  

	bool isSlide;  
	float angle; 

	float MCPosX;
	float MCPosY;
	float MCPosZ;
    

    void Start () {
		MainC = Camera.main;
        WorldMapObj = Camera.main.transform;
		mspeed = 5.0f;
		MCPosZ = -10.0f;
    }
	

	void Update () {
		cameraMSize = Mathf.Clamp (cameraMSize,0.94f,3.49f);
		MainC.orthographicSize = cameraMSize;

		MCPosX = Mathf.Clamp (MCPosX,-5.0f,5.0f);
		MCPosY = Mathf.Clamp (MCPosY,-5.0f,5.0f);
		MCPosZ = Mathf.Clamp (MCPosZ,-50.0f,-5.0f);
		MainC.transform.position = new Vector3(MCPosX,MCPosY,MCPosZ);


        KeyInput();
        //Raytest ();
        LargeMap();
    }

    public void Raytest() {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            Debug.Log(hit.collider.gameObject);
        }
    }
    public void KeyInput()
    {
        //用键盘移动
        if (Input.GetKey(KeyCode.W))//按W往上
        {
            WorldMapObj.transform.Translate(Vector3.down * Time.deltaTime * mspeed);
        }
        if (Input.GetKey(KeyCode.S))//按S往下
        {
            WorldMapObj.transform.Translate(Vector3.up * Time.deltaTime * mspeed);
        }
        if (Input.GetKey(KeyCode.A))//按A往左
        {
            WorldMapObj.transform.Translate(Vector3.right * Time.deltaTime * mspeed);
        }
         if (Input.GetKey(KeyCode.D))//按A往右
        {
            WorldMapObj.transform.Translate(Vector3.left * Time.deltaTime * mspeed);

        }


                //快速移动视角
                if (Input.GetKey(KeyCode.UpArrow))//按向上箭头移动镜头
                {
                    MCPosY -= 0.5f;
                }
                if (Input.GetKey(KeyCode.DownArrow))//按向下箭头移动镜头
                {
                    MCPosY += 0.5f;
                }
                if (Input.GetKey(KeyCode.LeftArrow))//按向左箭头移动镜头
                {
                    MCPosX += 0.5f;
                }
                if (Input.GetKey(KeyCode.RightArrow))//按向右箭头移动镜头
                {
                    MCPosX -= 0.5f;
                }
            }
  
	////用鼠标控制
	//public void MouseInput(){
	//	if (Input.GetMouseButtonDown(0))  
	//	{  
	//		StartPosition = Input.mousePosition;  
	//		previousPosition = Input.mousePosition;  
	//	}  
	//	if (Input.GetMouseButton(0))  
	//	{  
	//		offset = Input.mousePosition - previousPosition;  
	//		previousPosition = Input.mousePosition;  
	//		transform.position= Vector3.Cross (offset, Vector3.forward).normalized;  
	//	}  
	//	if (Input.GetMouseButtonUp(0))  
	//	{  
	//		finalOffset = Input.mousePosition - StartPosition;  
	//		isSlide = true;  
	//		angle = finalOffset.magnitude;
 //       }
 //   }

    //放大缩小视角
    public void LargeMap() {
       // 使用滚轮控制摄像机的视野
            cameraMSize -= Input.GetAxis("Mouse ScrollWheel") * 1.5f;
        if (Input.GetMouseButton(1)) {

            if (Input.GetKey(KeyCode.W))//按向上箭头移动镜头
            {
                cameraMSize -= 0.1f*Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))//按向下箭头移动镜头
            {
                cameraMSize += 0.1f*Time.deltaTime;
            }
            
        }

    }

}

