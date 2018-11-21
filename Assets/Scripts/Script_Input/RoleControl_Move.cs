using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(PolyNavAgent))]
public class RoleControl_Move : MonoBehaviour {
    /// <summary>
    /// 移动距离
    /// </summary>
	public float moveDistance;
    /// <summary>
    /// 起点位置
    /// </summary>
    public Vector3 startPos;
    public Vector3 endPos;
	public Camera Scene_Camera;
    
    public GameObject RoleTrans ;
    public UI2DSpriteAnimation RoleAnimation;
    Sprite[] RoleAnimation_All;
    //碰撞方向的BOX名称
    public GameObject ClickDirection_Up;
    public GameObject ClickDirection_Down;
    public GameObject ClickDirection_Left;
    public GameObject ClickDirection_Right;

    bool isStop;//角色是否为静止状态

    void Start () {
        moveDistance = 0;
        startPos = Vector3.zero;

        RoleAnimation_All = RoleAnimation.frames;
        isStop = true;
        ClickMoveEnd();
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void FixedUpdate()
    {
        if (isStop)
        {
            ClickMoveEnd();
        }
    }

	//鼠标点击移动
	public float RoleMove(float movespeed)
    {
        RoleTrans.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Vector3 nowVC = Vector3.zero;
        //按下鼠标右键只负责播放动画不停止
        if (Input.GetMouseButtonDown(1))
        {
            startPos = RoleTrans.transform.localPosition;
            isStop = false;
            moveDistance = 0;
            RoleAnimation.Play();
        }
		//持续按住鼠标右键只负责移动
		if (Input.GetMouseButton(1))
        {
            /*  ————————————————————————————————————————*/
            //直接移动位置
            //				Vector3 pos3 = rh.point;
            //				Vector2 pos2 = new Vector2 (pos3.x,pos3.y);
            //				if(rh.collider.gameObject.tag ==Ground_TagName){
            //					RoleTrans.position= Vector2.MoveTowards (RoleTrans.position,pos3,Click_Movespeed);
            //				}
            /*  ————————————————————————————————————————*/
            //判断移动方向
            Ray rays= Scene_Camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit rh;
			if(Physics.Raycast(rays,out rh)){
				if(rh.collider.gameObject == ClickDirection_Up)
                {
                    AnimationMove_Start(9,11);
                    nowVC = AnimationMove_NOW(Vector3.up * movespeed, movespeed);
                }
				else if(rh.collider.gameObject == ClickDirection_Down)
                {
                    AnimationMove_Start(0, 2);
                    nowVC = AnimationMove_NOW (Vector3.down * movespeed, movespeed);
                }
                else if (rh.collider.gameObject == ClickDirection_Left)
                {
                    AnimationMove_Start(3, 5);
                    nowVC = AnimationMove_NOW(Vector3.left * movespeed, movespeed);
                }
                else if (rh.collider.gameObject == ClickDirection_Right)
                {
                    AnimationMove_Start(6, 8);
                    nowVC = AnimationMove_NOW(Vector3.right * movespeed, movespeed);
                }
			}
        }
        //抬起鼠标右键为移动终止，停止播放动画
        if (Input.GetMouseButtonUp(1))
        {
            if (moveDistance % movespeed != 0)
            {
                moveDistance = Mathf.CeilToInt(moveDistance / movespeed) + 1;
            }
            isStop = true;
            ClickMoveEnd();
        }
        return moveDistance;
	}


    //处理点击角色静止的状态
    void ClickMoveEnd()
    {
        RoleTrans.GetComponent<SpriteRenderer>().sprite = RoleAnimation.frames[1];
        RoleAnimation.frames = RoleAnimation_All;
        RoleAnimation.Pause();
    }

	//移动开始处理
	void AnimationMove_Start(int start,int end){
        RoleAnimation.frames = new Sprite[] { RoleAnimation_All[start], RoleAnimation_All[start + 1], RoleAnimation_All[end], };
	}
	//角色移动进行时处理
	Vector3 AnimationMove_NOW(Vector3 vc,float movespeed)
    {
        //RoleTrans.transform.Translate(vc * movespeed * Time.deltaTime);
        Vector3.MoveTowards(RoleTrans.transform.localPosition, vc, movespeed * Time.deltaTime);
        moveDistance += movespeed * Time.deltaTime;
        return vc;
    }

    //切换处理
    void AnimationMove_Switch(){
        
	}
	//移动结束时处理
	void AnimationMove_Finish(UIAtlas ua,string sn){
        
	}
}