using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDUI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
    // Use this for initialization
    void Start ()
    {
        UIManager.instance.LoadPage(SceneManager.GetActiveScene().name);
        UIManager.instance.OpenPage(PageCollection.StartPage);
    }

}
