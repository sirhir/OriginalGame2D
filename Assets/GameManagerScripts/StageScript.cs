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

    public AudioClip startSound;
    public AudioClip gameOverSound;
    public AudioClip clearSound;

    private state gameState;

    void Start()
    {
        gameState = state.start;
    }

    void Update()
    {

        if ( Input.GetMouseButtonDown(0) )
        {
            if ( gameState == state.start )
            {
                StageStart();
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

    void StageStart()
    {
        GetComponent<AudioSource>().PlayOneShot(startSound, 0.2f);
        Destroy(this.gameObject.transform.Find("StartCanvas").gameObject);
        Instantiate(InputModulePrefab);
        gameState = state.ingame;
    }

    public void Missed()
    {
        if (gameState == state.ingame){
            GetComponent<AudioSource>().PlayOneShot(gameOverSound, 0.1f);
            Instantiate(GameOverCanvas);
            Destroy(GameObject.FindGameObjectWithTag("InputModule"));
            gameState = state.gameover;
        }
        else if (gameState == state.start)
        {
            GetComponent<AudioSource>().PlayOneShot(gameOverSound, 0.1f);
            Instantiate(GameOverCanvas);
            Destroy(this.gameObject.transform.Find("StartCanvas").gameObject);
            gameState = state.gameover;
        }
    }

    public void Cleared()
    {
        if (gameState == state.ingame)
        {
            GetComponent<AudioSource>().PlayOneShot(clearSound, 0.2f);
            Instantiate(ClearCanvas);
            Destroy(GameObject.FindGameObjectWithTag("InputModule"));
            gameState = state.clear;
        }
        else if (gameState == state.start)
        {
            GetComponent<AudioSource>().PlayOneShot(clearSound, 0.2f);
            Instantiate(ClearCanvas);
            Destroy(this.gameObject.transform.Find("StartCanvas").gameObject);
            gameState = state.clear;
        }
    }

}
