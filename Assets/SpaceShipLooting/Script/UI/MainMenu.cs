using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //public SceneFader fader;
    [SerializeField] private string loadToScene = "SceneName";

   

    private void Start()
    {
        
    }

    public void StartGame(Button button)
    {
        Debug.Log("Start Game");
    }

    public void Option(Button button)
    {
        Debug.Log("Option Game");
    }

    public void QuitGame(Button button)
    {
        Debug.Log("Quit Game");
    }
}
