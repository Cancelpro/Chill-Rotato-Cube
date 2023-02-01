using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonss : MonoBehaviour
{

    [SerializeField] Button cont;
    [SerializeField] Button levelSelect;
    [SerializeField] string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        cont.onClick.AddListener(Nextlevel);
        levelSelect.onClick.AddListener(LevelSelect);


    }

    void Nextlevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
