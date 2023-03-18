using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Dropdown difficultyDropDown;
    private int difficultyIndex = 0;
    
    void Start()
    {
        difficultyDropDown = GetComponent<Dropdown>();
    }

    public void DifficultySelect(int _difficultyIndex)
    {
        if (_difficultyIndex == 0)
        {
            difficultyIndex = _difficultyIndex;
        }
        else if (_difficultyIndex == 1)
        {
            difficultyIndex = _difficultyIndex;
        }
        else if (_difficultyIndex == 2)
        {
            difficultyIndex = _difficultyIndex;
        }
        else
        {
            Debug.LogWarning(_difficultyIndex + "Not valid");
        }
    }

    public void PreSwitchScene()
    {
        if(difficultyIndex == 0)
        {
            SwitchScene("Easy");
        }
        else if (difficultyIndex == 1)
        {
            SwitchScene("Medium");
        }
        else if (difficultyIndex == 2)
        {
            SwitchScene("Hard");
        }
        else
        {
            Debug.LogWarning(difficultyIndex + "Not valid");
        }
    }

    public void SwitchScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void QuitTiDesktop()
    {
        Application.Quit();
    }
 
}
