using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollReset : ResetRedirect
{
    public override void OnReset() 
    {
        SelectMenu.curSelected = Instantiate(Resources.Load<GameObject>("Objects/Ragdoll")).transform.Find(SelectMenu.curSelected.name).gameObject;
        Destroy(gameObject);
    }
}
