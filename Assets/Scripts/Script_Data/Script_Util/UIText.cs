// ======================================================================================
// 文 件 名 称：UIText.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-28 10:51:51
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-28 10:51:51
// ======================================================================================
// 功 能 描 述：UI文字显示
// ======================================================================================

using UnityEngine;

public class UIText : MonoBehaviour
{
    [SerializeField]
    private string key;

    private void Start ()
    {
        UILabel uI = GetComponent<UILabel> ();

        if (!string.IsNullOrEmpty (key))
        {
            string value = LanguageMgr.GetInstance.GetText (key);
            if (!string.IsNullOrEmpty (value))
                uI.text = value;
            else
                Debug.LogError ("No Text Value!");
        }
    }

    /// <summary>
    /// 设置UI文字
    /// </summary>
    /// <param name="_useKey"></param>
    /// <param name="_keyOrValue"></param>
    public void SetText (bool _useKey, string _keyOrValue)
    {
        if (_useKey)
        {
            string value = LanguageMgr.GetInstance.GetText (_keyOrValue);
            if (!string.IsNullOrEmpty (value))
                GetComponent<UILabel> ().text = value;
            else
                Debug.LogError ("No Text Value!");
        }
        else
        {
            GetComponent<UILabel> ().text = _keyOrValue;
        }
    }

    public string GetText ()
    {
        return GetComponent<UILabel> ().text;
    }
}