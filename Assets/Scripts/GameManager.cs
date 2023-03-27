using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text gameOverText;

    //Stores all the nodes
    [SerializeField] private Node[] nodes;
    //Stores reference to the player
    [SerializeField] private Player player;

    public Node[] Nodes { get { return nodes; } }
    public Player Player { get { return player; } }

    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Awake is called before Start is executed for the first time.
    /// </summary>
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        FindObjectOfType<Enemy>().GameOverEvent += GameOver;
    }

    private void Start()
    {
        if (gameOverText.gameObject.activeSelf == true)
        {
            GameOverTextPopup();
        }
    }

    /// <summary>
    /// Triggers the Restart Game coroutine.
    /// </summary>
    private void GameOver()
    {
        GameOverEvent();
    }

    /// <summary>
    /// Disables the player. Re-loads the active scene after 2 second delay.
    /// </summary>
    /// <returns></returns>
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GameOverEvent()
    {
        GameOverTextPopup();
        player.enabled = false;
    }

    private void GameOverTextPopup()
    {
        gameOverText.gameObject.SetActive(!gameOverText.gameObject.activeSelf);
    }

    public void RestartGameEvent()
    {
        StartCoroutine(RestartGame());
    }

    public void SwitchMainMenu(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

}
