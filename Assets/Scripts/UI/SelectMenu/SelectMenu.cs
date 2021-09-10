using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectMenu : MonoBehaviour
{
    public static GameObject curSelected;
    public bool menuClosed = false;
    private static SelectMenu instance = null;
    //TODO the object should be created when an object is selected and destroyed(by itself) when an object is deselected
    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (!curSelected)
        {
            CloseMenu();
        }
        if (menuClosed)
        {
            Destroy(gameObject);
        }
    }

    private void CloseMenu()
    {
        if (instance == this)
        {
            instance = null;
            GetComponent<Animator>().Play("Close");
        }
    }

    public static void CloseCurMenu()
    {
        if (instance)
            instance.CloseMenu();
    }

    private static GameObject GetSelected() 
    {
        if (curSelected.GetComponent<SelectRedirect>())
        {
            return curSelected.GetComponent<SelectRedirect>().redirectTo;
        }
        return curSelected;
    }

    public static void OnResetSelected()
    {
        GameObject resetTarget = GetSelected();
        if (resetTarget.GetComponent<ResetRedirect>())
        {
            resetTarget.GetComponent<ResetRedirect>().OnReset();
            return;
        }
        curSelected = Instantiate(curSelected.GetComponent<PrefabLoader>().Prefab);
        Destroy(resetTarget);
    }

    public static void OnDeleteSelected() 
    {
        GameObject deleteTarget = GetSelected();
        if (!deleteTarget.GetComponent<NoSelectDelete>()) 
        {
            if (deleteTarget.GetComponent<DeleteRedirect>())
            {
                deleteTarget.GetComponent<DeleteRedirect>().OnDelete();
            }
            else 
            {
                Destroy(deleteTarget);
            }
        }
    }

    public static void OnExplodeSelected() 
    {
        GameObject explodeTarget = GetSelected();
        if (explodeTarget.GetComponent<ExplodeRedirect>())
        {
            explodeTarget.GetComponent<ExplodeRedirect>().OnExplode();
        }
        else
        {
            //use curSelected to get true location of selected object
            foreach (Collider2D c in Physics2D.OverlapCircleAll(curSelected.transform.position, 2))
            {
                if(c.attachedRigidbody)
                    c.attachedRigidbody.AddForce((c.attachedRigidbody.position - (Vector2)curSelected.transform.position).normalized * 10, ForceMode2D.Impulse);
            }
            GameObject explosionFab = Resources.Load<GameObject>("Effects/Explosion");
            for (int i = 0; i < 5; i++)
            {
                Instantiate(explosionFab).transform.position = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0).normalized * Random.Range(0f, 1f) + curSelected.transform.position;
            }
        }
        OnDeleteSelected();
    }



}
