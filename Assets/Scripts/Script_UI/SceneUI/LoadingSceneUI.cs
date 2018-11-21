using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneUI : MonoBehaviour {
    public UILabel Label_Tips;
    public UISprite Sprite_Animation;
    public UILabel Label_Ming;
    public UILabel Label_Pro;

    float mPro=0.15f;
    float delay = 3f;

    void Start ()
    {
        int id = Random.Range(100, 102);
        //Label_Tips.text =SD_WordTranslate.Class_Dic["9"].chinese + SD_WordTranslate.Class_Dic[id.ToString()].chinese;

        Sprite_Animation.fillAmount = 0.15f;
        Label_Ming.gameObject.SetActive(false);
    }
	

	void Update () {
        mPro += Time.deltaTime;
        if (mPro > delay)
        {
            mPro = 0.15f;
            Sprite_Animation.fillAmount =0.15f + mPro/ delay;
            Destroy(Label_Ming.gameObject.GetComponent<TweenAlpha>());
            Label_Ming.gameObject.SetActive(false);
        }
        else if(mPro < delay)
        {
            if (!Label_Ming.gameObject.GetComponent<TweenAlpha>())
            {
                Label_Ming.gameObject.SetActive(true);
                TweenAlpha ta = Label_Ming.gameObject.AddComponent<TweenAlpha>();
                ta.PlayForward();
                ta.duration = delay;
                ta.style = UITweener.Style.Once;
                ta.from = 0.2f;
                ta.to = 1;
            }
            Sprite_Animation.fillAmount =0.15f + mPro / delay;
        }
	}

    public void setAnimation(float progress)
    {
        if (progress > 100)
        {
            progress = 100;
        }
        Label_Pro.text = progress.ToString()+"%";
    }
    

}
