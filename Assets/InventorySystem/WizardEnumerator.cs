///<summary>描述:
///作者:
///创建时间: 2019/02/17 17:57 
///版本:v1.0
///</summary>using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnumerator : IEnumerator
{
  
    object IEnumerator.Current
    {
        get
        {
            return currentObj;
        }
    }

    private Wizards currentObj=null;
     

    public void Dispose()
    { currentObj = null;
    }

    public bool MoveNext()
    {
        currentObj = (currentObj == null) ? Wizards.FirstCreated : currentObj.NextWizards;
        return (currentObj != null);
    }

    public void Reset()
    {
        currentObj = null;
    }
 
}
