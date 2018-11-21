using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSceneChange : MonoBehaviour {
    public Transform World_Root;
    public Transform Map_World;
    public Transform Map_Building;

    void Start () {
        //CommonFunc.GetInstance.Map_Instantiate(Data_Static.Map_City_beijing, Map_World);
    }
	

	void Update () {
		
	}

    public void SceneChange(string changeTo)
    {
        CommonFunc.GetInstance.Map_Instantiate(changeTo, Map_World);
    }
}
