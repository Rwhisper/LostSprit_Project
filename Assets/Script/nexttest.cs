using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class nexttest : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onMouseDown()
    {
        Debug.Log("??ư ????");
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
