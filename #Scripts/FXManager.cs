using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public GameObject[] fxList;

    public KeyCode[] keys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < keys.Length && i < fxList.Length; i++)
        {
            try
            {
                Bind(keys[i], fxList[i]);
            }
            catch (System.Exception)
            {
                Debug.Log("FXManager : Warning, item number " + i.ToString() + " has not succesfully be bound. Chek both lists in inspector.");
            }
        }

    }

    void Bind(KeyCode key, GameObject fx)
    {
        if (Input.GetKeyDown(key))
        {
            fx.SetActive(!fx.activeInHierarchy);
        }
    }
}
