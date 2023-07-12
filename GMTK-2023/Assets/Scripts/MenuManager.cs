using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private string gameScene = "Game";
    [SerializeField] private GameObject initialMenuPanel;
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private TMP_Text LevelText;
    [SerializeField] private TMP_Text GoldText;

    public void Play ()
    {
        Env.Instance.Level = Env.Instance.FirstLevel;
        SceneManager.LoadScene(gameScene);
    }

    public void Exit ()
    {
        Application.Quit();
    }

    void Start () {
        if (TimerText && Env.Instance) {
            TimerText.text = Mathf.FloorToInt(Env.Instance.Timer / 60).ToString() + "m " + Mathf.FloorToInt(Env.Instance.Timer % 60).ToString() + "s";
            LevelText.text = Env.Instance.PrincessLevel.ToString();
            GoldText.text = Env.Instance.Coins.ToString();
        }
    }
}
