using Common;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_DragBar :UIBehaviour, IDragHandler, IPooler
    {
        RectTransform windowRect;
        RectTransform dragBarRect;
        WindowUI window;

        public WindowUI Window
        {
            get => window;
            set
            {
                window = value;
                //调整拖拽栏大小
                windowRect = window.GetComponent<RectTransform>();
             
                dragBarRect.sizeDelta = new Vector2(windowRect.sizeDelta.x,dragBarRect.sizeDelta.y);
                dragBarRect.anchoredPosition = Vector3.zero;

 
            }
        }

        public Text ObjectName;
        public Text WindowName;

        Button MinButton;
        Button ExitButton;

        protected override void Awake ()
        {
            dragBarRect = GetComponent<RectTransform>();

            ObjectName = transform.FindChildByName("ObjectName").GetComponent<Text>();
            WindowName = transform.FindChildByName("WindowName").GetComponent<Text>();

            MinButton = transform.FindChildByName("MinButton").GetComponent<Button>();
            ExitButton = transform.FindChildByName("ExitButton").GetComponent<Button>();

            this.OnMouseClick_Left(() => window.transform.SetAsLastSibling());
            this.OnMouseClickSecond_Left(() => window.Minimize());

            MinButton.onClick.AddListener(()=>window.Minimize());
            ExitButton.onClick.AddListener(() => window.Exit());

        }

        public void OnDrag ( PointerEventData eventData )
        {
            if ( Input.mousePosition.x < Camera.main.scaledPixelWidth && Input.mousePosition.x > 0
              && Input.mousePosition.y < Camera.main.scaledPixelHeight  && Input.mousePosition.y > 0 )
            {
                window.transform.position = Input.mousePosition - transform.localPosition;
                transform.position = Input.mousePosition;
            }
        }

        public void OnReset ()
        { 
        }

        public void Recover ()
        {   
            window = null;
        }
    }
}
