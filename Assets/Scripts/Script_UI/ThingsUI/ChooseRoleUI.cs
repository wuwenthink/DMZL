// ======================================================================================
// 文 件 名 称：ChooseRoleUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-31 19:54:02
// 最 后 修 改：xic
// 更 新 时 间：2017-11-14 14:20
// ======================================================================================
// 功 能 描 述：UI：选择角色
// ======================================================================================
using System.Collections.Generic;
using UnityEngine;

public class ChooseRoleUI : MonoBehaviour
{
    public Transform GameObject_Pos_Role;
    public Transform Button_RoleEX;

    private Transform UI_Root;

    void Start ()
    {
        UI_Root = FindObjectOfType<UIRoot> ().transform;
        CommonFunc.GetInstance.SetUIPanel (gameObject);
    }

    public void SetAll (List<int> roleIds)
    {
        var comm = CommonFunc.GetInstance;
        var roleDic = RunTime_Data.RolePool;

        int num = -1;
        foreach (int id in roleIds)
        {
            Transform role = comm.UI_Instantiate (Button_RoleEX, GameObject_Pos_Role, new Vector3 (0, 70 - 50 * ++num), Vector3.one);
            role.name = id.ToString ();

            role.Find ("Sprite_RoleIcon").GetComponent<UISprite> ().spriteName = roleDic [id].headIcon;
            role.Find ("Label_RoleName").GetComponent<UIText> ().SetText (false, roleDic [id].commonName);

            UIEventListener.Get (role.gameObject).onClick = Choose;
        }
        Button_RoleEX.gameObject.SetActive (false);
    }

    // 选择这个人并返回他的ID
    private void Choose (GameObject btn)
    {
        UI_Root.BroadcastMessage ("ReceiveButtonState", int.Parse (btn.name));

        Destroy (gameObject);
    }
}
