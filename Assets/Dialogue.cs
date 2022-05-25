using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogue;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(true);
        StartCoroutine(display());
    }

    IEnumerator display()
    {
        yield return new WaitForSeconds(10f);
        dialogue.SetActive(false);
    }
}
