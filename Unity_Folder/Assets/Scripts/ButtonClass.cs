using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void On_Click_Start(string str)
    {
        SceneManager.LoadSceneAsync(str);
    }

    public void ActiveObj(GameObject obj)
    {
        if (obj.activeInHierarchy == false)
        {
            obj.SetActive(true);
        }
        else
            obj.SetActive(false);
    }

    public void On_Click_Exit()
    {
        Application.Quit();
    }

    public void On_Click_Replay()
    {
        SceneManager.LoadScene(1);
    }
}
