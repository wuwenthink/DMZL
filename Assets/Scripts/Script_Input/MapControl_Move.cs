// ======================================================================================
// 文 件 名 称：MapControl_Move.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：Administrator
// 创 建 时 间：2018-03-25 11:30:29
// 最 后 修 改：Administrator
// 更 新 时 间：2018-03-25 11:30:29
// ======================================================================================
// 功 能 描 述：使用键盘或者鼠标移动地图
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl_Move : MonoBehaviour {

    public float limit_Top;//上边界
    public float limit_Bottom;//下边界
    public float limit_Left;//左边界
    public float limit_Right;//右边界
    public bool isOpenMouse;

    float key_speed = 20;//键盘移动速度
    public Camera Scene_Camera;
    public Camera UI_Camera;

    Vector3 StartPosition;
    Vector3 previousPosition;
    Vector3 offset;
    float PosScale;//镜头远近


    void Start () {
        limit_Top = 0;
        limit_Bottom = 0;
        limit_Left = 0;
        limit_Right = 0;

        isOpenMouse = false;
    }
	

	void Update ()
    {
       
    }
    void FixedUpdate()
    {
        if (UICamera.isOverUI)
        {
            return;
        }
        else
        {
            MapLimit(isOpenMouse);
        }
    }
    void MapLimit(bool isMouse)
    {
        //边界限制
        if (limit_Top != 0 && limit_Bottom != 0 && limit_Left != 0 && limit_Right != 0)
        {
            Scene_Camera.transform.localPosition = new Vector3(Mathf.Clamp(Scene_Camera.transform.localPosition.x, limit_Left, limit_Right),
                Mathf.Clamp(Scene_Camera.transform.localPosition.y, limit_Bottom, limit_Top), Scene_Camera.transform.localPosition.z);
        }

        //鼠标到屏幕边缘或者按下对应按键时时相机平移移动（后期增加限制）
        if (isMouse)
        {
            Vector3 v1 = Scene_Camera.ScreenToViewportPoint(Input.mousePosition);
            if ((v1.x < 0.05f) || Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * key_speed * Time.deltaTime, Space.World);
            }
            if ((v1.x > 1 - 0.05f) || Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * key_speed * Time.deltaTime, Space.World);
            }
            if ((v1.y < 0.05f) || Input.GetKey(KeyCode.S))
            {
                transform.Translate(-Vector3.up * key_speed * Time.deltaTime, Space.World);
            }
            if ((v1.y > 1 - 0.05f) || Input.GetKey(KeyCode.W))
            {
                transform.Translate(-Vector3.down * key_speed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * key_speed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * key_speed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(-Vector3.up * key_speed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(-Vector3.down * key_speed * Time.deltaTime, Space.World);
            }
        }

        //按空格键镜头回到角色身上
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    transform.localPosition = FindObjectOfType<RoleControl_Move>().transform.localPosition;
        //}

        PosScale = Scene_Camera.orthographicSize;
        ////鼠标移动
        //if (Input.GetMouseButtonDown(0))
        //{
        //    StartPosition = Input.mousePosition;
        //    previousPosition = Input.mousePosition;
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    offset = Input.mousePosition - previousPosition;
        //    previousPosition = Input.mousePosition;
        //    SceneCamera.transform.Translate(-offset * mouse_speed * Time.deltaTime);
        //}

        //鼠标滚轮放大缩小
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            PosScale += 0.5f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            PosScale -= 0.5f;
        }

        Scene_Camera.orthographicSize = Mathf.Clamp(PosScale, 2, 20);
    }
}
