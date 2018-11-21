using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour {
	Object[] initsObject;
	GameObject _start;
	GameStart gs;
    void Awake(){
        //永久使用_GameStart的Scence内所有控件
        initsObject = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (Object go in initsObject)
        {
            DontDestroyOnLoad(go);
        }
    }

	void Start () {
		Add ();
	}


	void Update () {
	}
		
	void Add(){	//添加Gamestart脚本
		_start = this.gameObject;
		if(_start!=null){
			_start.AddComponent<GameStart> ();
            _start.AddComponent<KeyControl>();
            _start.AddComponent<Save_Game>();
            _start.AddComponent<Load_Game>();
            _start.AddComponent<TimeManager>();
        }
	}
}
