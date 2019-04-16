using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BUTTON_MODE
{
    HIDE_TEXT, //隐藏 字体
    OTHER //其他

}
/// <summary>
/// 射线 交互Button  通过 射线进行 物理 探测 检测
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class RayButton : Button
{

    public UnityEngine.Events.UnityEvent MenuUIEvent;

    private RectTransform rectTransform;
    bool isPlayMusic=true;
 
    [SerializeField]
    private Text childText;
    private BoxCollider _boxCollider;

    /// <summary>
    ///  按钮交互 模式
    /// </summary>  
    [SerializeField]
    public BUTTON_MODE currentMode=BUTTON_MODE.OTHER;

    protected override void Awake()
    {
        base.Awake();

       

        _boxCollider = GetComponent<BoxCollider>();

        if (_boxCollider==null)
            _boxCollider = gameObject.AddComponent<BoxCollider>();
       
        rectTransform = GetComponent<RectTransform>();

        _boxCollider.size = new Vector3(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y, .001f);

        transform.tag = Tag.Button;
         
        HandleInterateMode(false);
    }   

    
    void HandleInterateMode(bool isEnter)
    {
        switch (currentMode)
        {
            case BUTTON_MODE.HIDE_TEXT:
               
                if (childText == null)
                {
                    childText = transform.Find("Text").GetComponent<Text>();
                }
                childText.gameObject.SetActive(isEnter);

                break;
            case BUTTON_MODE.OTHER:
                break;
        
        }
    }

    void AddChildText()
    {
        if (childText == null)
        { 
            GameObject text = new GameObject("Text");
            RectTransform textRec = text.AddComponent<RectTransform>();

            childText = text.AddComponent<Text>();
            text.transform.SetParent(this.transform);
            text.transform.localPosition =
                new Vector3(transform.GetComponent<RectTransform>().sizeDelta.x, 0, 0);
            childText.text = "Text";
            childText.alignment = TextAnchor.MiddleCenter;
            childText.fontSize = 24;
            text.transform.localScale = Vector3.one;
        }
    }

    /// <summary>
    ///  视线进入 
    /// </summary>
    public virtual void RayEneter()
    {
        if (isPlayMusic)
        {
            AudioManager.Instance.BtnHover();
            isPlayMusic = false;
        }

        HandleInterateMode(true);
 
        DoStateTransition(SelectionState.Highlighted, false);
    }

    /// <summary>
    /// 视线停留
    /// </summary>
    public virtual void RayStay()
    {
        ReSetButtonState();
    }

    /// <summary>
    ///  视线离开
    /// </summary>
    public virtual void RayExit()
    {
        DoStateTransition(SelectionState.Normal, false);
        isPlayMusic = true;
        HandleInterateMode(false);
    }
    /// <summary>
    /// 在点击 按钮 触发
    /// </summary>
    public virtual void TriggerEvent()
    {
        DoStateTransition(SelectionState.Pressed, false);

    }

    /// <summary>
    /// 在 射线进入交互后 按键 抬起 的时候调用
    /// </summary>
    public void ClickInvoke()
    {
        AudioManager.Instance.BtnClick();
        onClick.Invoke();//调用下 Click事件
           
    }
    /// <summary>
    /// 事件完成
    /// </summary>
    public virtual void ReSetButtonState()
    {
        DoStateTransition(SelectionState.Highlighted, false);
    }

}
