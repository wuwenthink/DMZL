using UnityEngine;

public class LableTipsUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public UIText Label_TipsDesc;
    public UIText Label_TipsTitle;

    public UISprite Sprite_BGTable;
    public UISprite Sprite_BGTable2;
    Vector3 offset;
    private bool isHover = false;

    private Camera ui_camera;

    private void Start ()
    {
        //ui_camera = FindObjectOfType<UIRoot> ().transform.Find ("Camera").GetComponent<Camera> ();
    }

    void LateUpdate ()
    {
        //if (isHover)
        //{
        //    Vector3 pos = ui_camera.ScreenToWorldPoint (Input.mousePosition);
        //    this.gameObject.transform.position = pos + offset;
        //}
    }

    void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = back;
        GetComponent<UIFollow>().enabled = false;
    }

    void back (GameObject btn)
    {
        Destroy (this.gameObject);
    }

    /// <summary>
    /// 设置内容
    /// </summary>
    /// <param name="_isClick">是点击还是跟随出现</param>
    /// <param name="_textLable">内容文字</param>
    public void SetAll (bool _isClick, string _textTitle, string _textLable)
    {
        Label_TipsDesc.SetText(false, _textLable);
        Label_TipsTitle.SetText(false, _textTitle);
        if (_isClick)
        {
            ClickControl ();
        }
        else
        {
            GetComponent<UIFollow>().enabled = true;
            //offset = new Vector3 (0.02f, 0.003f, 0);
            Sprite_Back.SetActive (false);
            isHover = true;

            var word = _textLable.Length;
            var hang = Mathf.CeilToInt (word / 19f);
            Sprite_BGTable.height = 40 + hang * 20;
            Sprite_BGTable2.height = 40 + hang * 20;
            //Label_TipsDesc.gameObject.GetComponent<UILabel>().SetRect (0, 0, 10 + word % 17 * 12, 30 * hang);
            //Label_TipsDesc.transform.localPosition = new Vector3 (20 + word * 6, 10 * hang + 10);
        }
    }
}
