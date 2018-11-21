using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceAlpha : MonoBehaviour {
    UISprite Role_Player;
    void Start () {
		
	}

	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        Role_Player = GameObject.FindObjectOfType<RoleControl_Move>().GetComponent<UISprite>();
        if (other.gameObject.tag == "Player")
        {
            Color half=new Color(1,1,1,0.5f);
            Role_Player.color = half;
            this.gameObject.GetComponent<UITexture>().color = half;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Role_Player = GameObject.FindObjectOfType<RoleControl_Move>().GetComponent<UISprite>();
        if (other.gameObject.tag == "Player")
        {
            Color nomal = new Color(1, 1, 1, 1f);
            Role_Player.color = nomal;
            this.gameObject.GetComponent<UITexture>().color = nomal;
        }
        }

}
