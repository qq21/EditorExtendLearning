///<summary>描述: 巫师的 迭代器
///作者:
///创建时间: 2019/02/17 17:26 
///版本:v1.0
///</summary>using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;
using System.Diagnostics;

[System.Serializable]
public class Wizards : MonoBehaviour, IEnumerable {
    public static Wizards LastCreated = null;
    public static Wizards FirstCreated = null;
    public Wizards NextWizards = null;
    public Wizards PreWizards = null;
    public string wizardName = "";
    // Use this for initialization
    void Start() {

    }
    private void Awake()
    {

#if  Property_Test
#region  test
        string abc = "123456123456123456123456";
        string dfg = "123456123456123456123456";
        int i=0;
        TimeSpan timeStart;
        TimeSpan timeEnd;

        timeStart = Process.GetCurrentProcess().TotalProcessorTime;
        while (i<30000)
        {
            //Animator.StringToHash(abc)== Animator.StringToHash(dfg)
            //abc.GetHashCode()==dfg.GetHashCode()
            //string.Equals(abc, dfg, System.StringComparison.CurrentCultureIgnoreCase)
            UnityEngine.Debug.Log(abc==dfg);           
            i++;
        }
        timeEnd = Process.GetCurrentProcess().TotalProcessorTime.Subtract(timeStart);
        UnityEngine.Debug.Log(timeEnd);
#endregion
#endif

    }
    public bool isSame(string s1,string s2)
    {
        
        return string.Equals(s1, s2, System.StringComparison.CurrentCultureIgnoreCase);
    }
    
    // Update is called once per frame
    void Update () {
		
	}

    private void OnDestroy()
    {
        if (PreWizards!=null)
        {
            PreWizards.NextWizards = NextWizards;
        }
        if (NextWizards!=null)
        {
            NextWizards.PreWizards = PreWizards;
        }
    }

    public IEnumerator  GetEnumerator()
    {
        return new WizardEnumerator();
    }
  
}
