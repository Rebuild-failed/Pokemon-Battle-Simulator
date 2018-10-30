using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDUI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start ()
    {
        UIManager.instance.LoadPage(SceneManager.GetActiveScene().name);
        UIManager.instance.OpenPage(PageCollection.StartPage);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangePokemon()
    {

    }
    public int DamageCalculate()
    {
        return 0;
    }
    public void BattleOver()
    {

    }
}
