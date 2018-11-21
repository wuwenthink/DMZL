using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public class MapEditor : MonoBehaviour {
    public static int partNum_X = 66;//当前场景横向6000*6000的块数


    void Start () {
		
	}
	

	void Update () {
    }

    [MenuItem("Tools/MapEditor/Open_MapEditor")]
    static void Map_Editor()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Editor/MapEditorFolder/MapEditor.unity", OpenSceneMode.Single);
    }

    // 循环获取选中物体的子（孙）物体名称和坐标，写入表格  by xic ；2017-10-10 11:35:52
    [MenuItem ("Tools/MapEditor/GetChildPosCircle")]
    static void GetChildPosCircle ()
    {
        if (File.Exists (Application.dataPath + "/Editor/MapEditorFolder/POS" + "/" + "BuildingPos" + ".xls"))
        {
            File.Delete (Application.dataPath + "/Editor/MapEditorFolder/POS" + "/" + "BuildingPos" + ".xls");
        }

        string result = "";

        Transform select = Selection.activeGameObject.transform;
        foreach(Transform child in select.GetComponentsInChildren<Transform>())
        {
            if(child.name != "Pic_Building" && child.name != "AnswerArea" && !child.name.Contains("wall") && !child.name.Contains("Wall") && !child.name.StartsWith("BuildingModel"))
                if(Regex.Matches (child.name, "[a-zA-Z]").Count > 0)
                    result += child.name + "," + (int)(child.position.x*100) / 100f + ";" + (int) (child.position.y * 100) / 100f + ","+child.name.Split('_')[1]+ "\n";
        }

        FileStream fs = new FileStream (Application.dataPath + "/Editor/MapEditorFolder/POS" + "/" + "BuildingPos" + ".xls", FileMode.Append, FileAccess.Write);
        StreamWriter sw = new StreamWriter (fs, Encoding.UTF8);
        sw.Write (result);
        sw.Close ();
        fs.Close ();

    }


    [MenuItem("Tools/MapEditor/GetChildPos")]
    static void GetChildPos()
    {
        if (File.Exists(Application.dataPath + "/Editor/MapEditorFolder/POS" + "/" + "BuildingPos" + ".xls"))
        {
            File.Delete(Application.dataPath + "/Editor/MapEditorFolder/POS" + "/" + "BuildingPos" + ".xls");
        }

        List<string> names = new List<string>();
        GameObject[] selectRoot = Selection.gameObjects;
        for (int index = 0; index < selectRoot.Length; index++)
        {
            names.Clear();
            if (selectRoot[index].transform.childCount > 0)
            {
                for (int i = 0; i < selectRoot[index].transform.childCount; i++)
                {
                    Transform t = selectRoot[index].transform.GetChild(i);
                    string go_name = "";
                    if (t.gameObject.name.Contains("("))
                    {
                        go_name = t.gameObject.name.Replace("BuildingModel_", "");
                        int indexWord = go_name.IndexOf("(");
                        go_name = go_name.Remove(indexWord, go_name.Length - indexWord);
                    }
                    else
                    {
                        go_name = t.gameObject.name.Replace("BuildingModel_", "");
                    }
                    int scenePartId = int.Parse (selectRoot [index].name);
                    float PosX = 60 * (scenePartId % partNum_X) + 30 + t.transform.localPosition.x;
                    float PosY = -60 * (scenePartId / partNum_X) - 30 + t.transform.localPosition.y;
                    names.Add (scenePartId + "\t" + go_name + "\t" + PosX + ";" + PosY + "\r\n");
                }
                string pos = "";
                for (int i = 0; i < names.Count; i++)
                {
                    pos += names[i];
                }
                FileStream fs = new FileStream(Application.dataPath + "/Editor/MapEditorFolder/POS" + "/" + "BuildingPos" + ".xls", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(pos);
                sw.Close(); fs.Close();
            }
        }
#if UNITY_EDITOR
    AssetDatabase.Refresh();
#endif
       
    }



    [MenuItem("Tools/MapEditor/Childs_ChangeName")]
    static void Childs_ChangeName()
    {
        List<string> names = new List<string>();
        GameObject[] selectRoot = Selection.gameObjects;
        for (int index = 0; index < selectRoot.Length; index++)
        {
            names.Clear();
            string ParentName = selectRoot[index].gameObject.name.Replace("BuildingModel_", "");
            if (selectRoot[index].transform.childCount > 0)
            {
                for (int i = 0; i < selectRoot[index].transform.childCount; i++)
                {
                    Transform t = selectRoot[index].transform.GetChild(i);
                    string ChildName = t.gameObject.name;

                    t.gameObject.name = ParentName +"_"+ ChildName;


                }
            }
        }
    }


    [MenuItem("Tools/MapEditor/ChangeName_Now")]
    static void ChangeName_Now()
    {
        List<string> names = new List<string>();
        GameObject[] selectRoot = Selection.gameObjects;
        for (int index = 0; index < selectRoot.Length; index++)
        {
            names.Clear();
            string ParentName = selectRoot[index].gameObject.name.Replace("BuildingModel_", "");
            if (selectRoot[index].transform.childCount > 0)
            {
                for (int i = 0; i < selectRoot[index].transform.childCount; i++)
                {
                    Transform t = selectRoot[index].transform.GetChild(i);
                    string ChildName = t.gameObject.name.Remove(0,4);

                    t.gameObject.name = ParentName + ChildName;

                }
            }
        }
    }

    [MenuItem("Tools/MapEditor/AddChild")]
    static void AddChild()
    {

        GameObject[] selectRoot = Selection.gameObjects;
        for (int index = 0; index < selectRoot.Length; index++)
        {
            GameObject go = Instantiate<GameObject>(selectRoot[index]);
            go.name = selectRoot[index].name;
            if (go.GetComponent<PolygonCollider2D>())
            {
                DestroyImmediate(go.GetComponent<PolygonCollider2D>());
            }
            go.transform.parent = selectRoot[index].transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.AddComponent<BoxCollider2D>().isTrigger = true;
            go.layer = 0;

            if (go.GetComponent<SpriteRenderer>())
            {
                DestroyImmediate(go.GetComponent<SpriteRenderer>());
            }
        }
    }

}
