using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class UIChangeName : MonoBehaviour {

    [MenuItem("GameObject/UI/ChangeUIName/1")]
    static void ChangeName1()
    {
        ChangeName("1");
    }
    [MenuItem("GameObject/UI/ChangeUIName/2")]
    static void ChangeName2()
    {
        ChangeName("2");
    }
    [MenuItem("GameObject/UI/ChangeUIName/3")]
    static void ChangeName3()
    {
        ChangeName("3");
    }
    [MenuItem("GameObject/UI/ChangeUIName/4")]
    static void ChangeName4()
    {
        ChangeName("4");
    }
    [MenuItem("GameObject/UI/ChangeUIName/5")]
    static void ChangeName5()
    {
        ChangeName("5");
    }
    [MenuItem("GameObject/UI/ChangeUIName/6")]
    static void ChangeName6()
    {
        ChangeName("6");
    }
    [MenuItem("GameObject/UI/ChangeUIName/7")]
    static void ChangeName7()
    {
        ChangeName("7");
    }
    [MenuItem("GameObject/UI/ChangeUIName/8")]
    static void ChangeName8()
    {
        ChangeName("8");
    }
    [MenuItem("GameObject/UI/ChangeUIName/9")]
    static void ChangeName9()
    {
        ChangeName("9");
    }
    [MenuItem("GameObject/UI/ChangeUIName/10")]
    static void ChangeName10()
    {
        ChangeName("10");
    }


    static void ChangeName(string index)
    {
        GameObject selectRoot = Selection.activeGameObject;
        if (selectRoot==null)
        {
            Debug.LogError("选择对象错误");
            return;
        }
        string selectParentName = selectRoot.name;
        selectParentName = selectParentName.Remove(selectParentName.Length-1);
        selectParentName += index;
        selectRoot.name = selectParentName;
        ChangeChildName(selectRoot, index);
    }
    static void ChangeChildName(GameObject parent,string index)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform t = parent.transform.GetChild(i);
            ChangeChildName(t.gameObject,index);

            string parentName = t.gameObject.name;
            parentName = parentName.Remove(parentName.Length - 1);
            parentName += index;
            t.name = parentName;
        }
    }
}
