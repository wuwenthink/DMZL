using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControl : MonoBehaviour {

    public KeyCode Move_Up;   //向上移动按键
    public KeyCode Move_Down;   //向下移动按键
    public KeyCode Move_Left;   //向左移动按键
    public KeyCode Move_Right;   //向右移动按键
    
    public KeyCode Key_SystemTip;   //打开系统菜单

    public KeyCode Key_RoleInfo;   //打开人物信息
    public KeyCode Key_Bag;   //打开背包
    public KeyCode Key_Task;   //打开任务
    public KeyCode Key_News;   //打开消息
    public KeyCode Key_Map;   //打开地图

    public KeyCode Key_NearTip;   //触发交互按键

    public bool isAllow; //按键是否生效

    //各个界面打开时的数字编号
    public string windowIndex;

    void Awake()
    {
        windowIndex = "begin";

        isAllow = true;

        //默认按键
        Move_Up = KeyCode.W;
        Move_Down = KeyCode.S;
        Move_Left = KeyCode.A;
        Move_Right = KeyCode.D;

        Key_SystemTip = KeyCode.Escape;

        Key_RoleInfo = KeyCode.C;
        Key_Bag = KeyCode.B;
        Key_Task = KeyCode.T;
        Key_News = KeyCode.N;
        Key_Task = KeyCode.M;

        Key_NearTip = KeyCode.E;
    }

    void Start () {
		
	}

	void Update () {
        if (CommonFunc.GetInstance.OpenWindow != null)
        {
            if (Input.GetKey(Key_SystemTip))
            {
                DestroyImmediate(CommonFunc.GetInstance.OpenWindow);
            }
        }
	}

    //在更改按键状态下改变按键的方法
    KeyCode ChangeKey(KeyCode kc)
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                return e.keyCode;
            }
            return kc;
        }
        return kc;
    }

    //返回键值keyCode
    KeyCode getKeyCode(string keyName)
    {
        KeyCode key=KeyCode.A;
        switch (keyName)
        {
            case "Q":
                key = KeyCode.Q;
                break;
            case "W":
                key = KeyCode.W;
                break;
            case "E":
                key = KeyCode.E;
                break;
            case "R":
                key = KeyCode.R;
                break;
            case "T":
                key = KeyCode.T;
                break;
            case "Y":
                key = KeyCode.Y;
                break;
            case "U":
                key = KeyCode.U;
                break;
            case "I":
                key = KeyCode.I;
                break;
            case "O":
                key = KeyCode.O;
                break;
            case "P":
                key = KeyCode.P;
                break;
            case "A":
                key = KeyCode.A;
                break;
            case "S":
                key = KeyCode.S;
                break;
            case "D":
                key = KeyCode.D;
                break;
            case "F":
                key = KeyCode.F;
                break;
            case "G":
                key = KeyCode.G;
                break;
            case "H":
                key = KeyCode.H;
                break;
            case "J":
                key = KeyCode.J;
                break;
            case "K":
                key = KeyCode.K;
                break;
            case "L":
                key = KeyCode.L;
                break;
            case "Z":
                key = KeyCode.Z;
                break;
            case "X":
                key = KeyCode.X;
                break;
            case "C":
                key = KeyCode.C;
                break;
            case "V":
                key = KeyCode.V;
                break;
            case "B":
                key = KeyCode.B;
                break;
            case "N":
                key = KeyCode.N;
                break;
            case "M":
                key = KeyCode.M;
                break;
            case "TAP":
                key = KeyCode.Tab;
                break;
            case "LeftShift":
                key = KeyCode.LeftShift;
                break;
            case "LeftAlt":
                key = KeyCode.LeftAlt;
                break;
            case "F1":
                key = KeyCode.F1;
                break;
            case "F2":
                key = KeyCode.F2;
                break;
            case "F3":
                key = KeyCode.F3;
                break;
            case "F4":
                key = KeyCode.F4;
                break;
            case "F5":
                key = KeyCode.F5;
                break;
            case "F6":
                key = KeyCode.F6;
                break;
            case "F7":
                key = KeyCode.F7;
                break;
            case "F8":
                key = KeyCode.F8;
                break;
            case "F9":
                key = KeyCode.F9;
                break;
            case "F10":
                key = KeyCode.F10;
                break;
            case "F11":
                key = KeyCode.F11;
                break;
            case "F12":
                key = KeyCode.F12;
                break;
            case "Esc":
                key = KeyCode.Escape;
                break;
            case "Space":
                key = KeyCode.Space;
                break;
            case "Enter":
                key = KeyCode.Return;
                break;
            case "Delete":
                key = KeyCode.Delete;
                break;
            case "Backspace":
                key = KeyCode.Backspace;
                break;
            case "End":
                key = KeyCode.End;
                break;
            case "Home":
                key = KeyCode.Home;
                break;
            case "PageUp":
                key = KeyCode.PageUp;
                break;
            case "PageDown":
                key = KeyCode.PageDown;
                break;
            case "Little_0":
                key = KeyCode.Keypad0;
                break;
            case "Little_1":
                key = KeyCode.Keypad1;
                break;
            case "Little_2":
                key = KeyCode.Keypad2;
                break;
            case "Little_3":
                key = KeyCode.Keypad3;
                break;
            case "Little_4":
                key = KeyCode.Keypad4;
                break;
            case "Little_5":
                key = KeyCode.Keypad5;
                break;
            case "Little_6":
                key = KeyCode.Keypad6;
                break;
            case "Little_7":
                key = KeyCode.Keypad7;
                break;
            case "Little_8":
                key = KeyCode.Keypad8;
                break;
            case "Little_9":
                key = KeyCode.Keypad9;
                break;
            case "0":
                key = KeyCode.Alpha0;
                break;
            case "1":
                key = KeyCode.Alpha1;
                break;
            case "2":
                key = KeyCode.Alpha2;
                break;
            case "3":
                key = KeyCode.Alpha3;
                break;
            case "4":
                key = KeyCode.Alpha4;
                break;
            case "5":
                key = KeyCode.Alpha5;
                break;
            case "6":
                key = KeyCode.Alpha6;
                break;
            case "7":
                key = KeyCode.Alpha7;
                break;
            case "8":
                key = KeyCode.Alpha8;
                break;
            case "9":
                key = KeyCode.Alpha9;
                break;
            case "":
                key = KeyCode.None;
                break;
        }
        return key;
    }
    //返回键值描述keyName
    string getKey(KeyCode key)
    {
        string keyName = "";
        switch (key)
        {
            case KeyCode.Q:
                keyName = "Q";
                break;
            case KeyCode.W:
                keyName = "W";
                break;
            case KeyCode.E:
                keyName = "E";
                break;
            case KeyCode.R:
                keyName = "R";
                break;
            case KeyCode.T:
                keyName = "T";
                break;
            case KeyCode.Y:
                keyName = "Y";
                break;
            case KeyCode.U:
                keyName = "U";
                break;
            case KeyCode.I:
                keyName = "I";
                break;
            case KeyCode.O:
                keyName = "O";
                break;
            case KeyCode.P:
                keyName = "P";
                break;
            case KeyCode.A:
                keyName = "A";
                break;
            case KeyCode.S:
                keyName = "S";
                break;
            case KeyCode.D:
                keyName = "D";
                break;
            case KeyCode.F:
                keyName = "F";
                break;
            case KeyCode.G:
                keyName = "G";
                break;
            case KeyCode.H:
                keyName = "H";
                break;
            case KeyCode.J:
                keyName = "J";
                break;
            case KeyCode.K:
                keyName = "K";
                break;
            case KeyCode.L:
                keyName = "L";
                break;
            case KeyCode.Z:
                keyName = "Z";
                break;
            case KeyCode.X:
                keyName = "X";
                break;
            case KeyCode.C:
                keyName = "C";
                break;
            case KeyCode.V:
                keyName = "V";
                break;
            case KeyCode.B:
                keyName = "B";
                break;
            case KeyCode.N:
                keyName = "N";
                break;
            case KeyCode.M:
                keyName = "M";
                break;
            case KeyCode.Tab:
                keyName = "Tab";
                break;
            case KeyCode.LeftShift:
                keyName = "LeftShift";
                break;
            case KeyCode.LeftAlt:
                keyName = "LeftAlt";
                break;
            case KeyCode.F1:
                keyName = "F1";
                break;
            case KeyCode.F2:
                keyName = "F2";
                break;
            case KeyCode.F3:
                keyName = "F3";
                break;
            case KeyCode.F4:
                keyName = "F4";
                break;
            case KeyCode.F5:
                keyName = "F5";
                break;
            case KeyCode.F6:
                keyName = "F6";
                break;
            case KeyCode.F7:
                keyName = "F7";
                break;
            case KeyCode.F8:
                keyName = "F8";
                break;
            case KeyCode.F9:
                keyName = "F9";
                break;
            case KeyCode.F10:
                keyName = "F10";
                break;
            case KeyCode.F11:
                keyName = "F11";
                break;
            case KeyCode.F12:
                keyName = "F12";
                break;
            case KeyCode.Escape:
                keyName = "Escape";
                break;
            case KeyCode.Space:
                keyName = "Space";
                break;
            case KeyCode.Return:
                keyName = "Return";
                break;
            case KeyCode.Delete:
                keyName = "Delete";
                break;
            case KeyCode.Backspace:
                keyName = "Backspace";
                break;
            case KeyCode.End:
                keyName = "End";
                break;
            case KeyCode.Home:
                keyName = "Home";
                break;
            case KeyCode.PageUp:
                keyName = "PageUp";
                break;
            case KeyCode.PageDown:
                keyName = "PageDown";
                break;
            case KeyCode.Keypad0:
                keyName = "Little_0";
                break;
            case KeyCode.Keypad1:
                keyName = "Little_1";
                break;
            case KeyCode.Keypad2:
                keyName = "Little_2";
                break;
            case KeyCode.Keypad3:
                keyName = "Little_3";
                break;
            case KeyCode.Keypad4:
                keyName = "Little_4";
                break;
            case KeyCode.Keypad5:
                keyName = "Little_5";
                break;
            case KeyCode.Keypad6:
                keyName = "Little_6";
                break;
            case KeyCode.Keypad7:
                keyName = "Little_7";
                break;
            case KeyCode.Keypad8:
                keyName = "Little_8";
                break;
            case KeyCode.Keypad9:
                keyName = "Little_9";
                break;
            case KeyCode.Alpha0:
                keyName = "0";
                break;
            case KeyCode.Alpha1:
                keyName = "1";
                break;
            case KeyCode.Alpha2:
                keyName = "2";
                break;
            case KeyCode.Alpha3:
                keyName = "3";
                break;
            case KeyCode.Alpha4:
                keyName = "4";
                break;
            case KeyCode.Alpha5:
                keyName = "5";
                break;
            case KeyCode.Alpha6:
                keyName = "6";
                break;
            case KeyCode.Alpha7:
                keyName = "7";
                break;
            case KeyCode.Alpha8:
                keyName = "8";
                break;
            case KeyCode.Alpha9:
                keyName = "9";
                break;
            case KeyCode.None:
                keyName = "";
                break;
        }
        return keyName;
    }
}
