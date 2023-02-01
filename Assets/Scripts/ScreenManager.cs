using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SwitchScene);
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
