///<summary>描述:
///作者:Tang
///创建时间: 2019/03/21 09:15 
///版本:v1.0
///公司：上海域圆信息科技有限公司
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 音频管理单例。
/// </summary>
public class AudioManager : MonoBehaviour
{


    static AudioManager _instance;
    public static string ManagerName = "AudioManger";
    private static  GameObject _self;

    
    private static AudioSource _audioSource;

    private AudioClip _btnHoverClip;
    private AudioClip _btnClickClip;


    public static AudioManager Instance
    {
        get
        {
            if (_instance==null)
            {
                if (_self==null)
                {
                    _self = GameObject.Find(ManagerName);
                    if (_self==null)
                    {
                        _self = new GameObject(ManagerName);
                        
                    }

                    _instance = _self.GetComponent<AudioManager>();
                    _audioSource = _self.GetComponent<AudioSource>();
                    if (_audioSource==null)
                    {
                        _audioSource = _self.AddComponent<AudioSource>();
                    }
                    if (_instance==null)
                    {
                        _instance = _self.AddComponent<AudioManager>();
                    }
                    
                }
                
            }

            return _instance;
        }
        
    }

     void PlayMuSic(AudioClip clip)
    {

        _audioSource.clip = clip;
        // 延迟一下  播放
       _audioSource.PlayDelayed(0.15f);
    }

     public void BtnHover( AudioClip clip=null)
     {
         PlayMuSic(clip != null ? clip : _btnHoverClip);
     }

     public void BtnClick(AudioClip clip = null)
     {
         PlayMuSic(clip != null ? clip : _btnClickClip);
     }


     private void Start()
     {

         _btnHoverClip = Resources.Load<AudioClip>("");
        _btnClickClip = Resources.Load<AudioClip>("");
    }
}
