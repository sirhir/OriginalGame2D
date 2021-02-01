using UnityEngine;
using UnityEngine.SceneManagement;

public class StageScript : MonoBehaviour
{
    enum state{
        start,
        ingame,
        gameover,
        clear,
    }

    public string currentScene;
    public string nextScene;
    public GameObject InputModulePrefab;
    public GameObject GameOverCanvas;
    public GameObject ClearCanvas;

    private state gameState;

    void Start()
    {
        gameState = state.start;
    }

    // Update is called once per frame
    void Update()
    {

        if ( Input.GetMouseButtonDown(0) )
        {
            if ( gameState == state.start )
            {
                Destroy(this.gameObject.transform.Find("StartCanvas").gameObject);
                Instantiate(InputModulePrefab);
                gameState = state.ingame;
            }

            if ( gameState == state.gameover)
            {
                SceneManager.LoadScene(currentScene);
            }

            if ( gameState == state.clear)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    public void Missed()
    {
        if (gameState == state.ingame)
        {
            Instantiate(GameOverCanvas);
            Destroy(GameObject.FindGameObjectWithTag("InputModule"));
            gameState = state.gameover;
        }
    }

    public void Cleared()
    {
        if (gameState == state.ingame)
        {
            Instantiate(ClearCanvas);
            Destroy(GameObject.FindGameObjectWithTag("InputModule"));
            gameState = state.clear;
        }
    }

}
