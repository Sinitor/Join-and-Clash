using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; 
    
    private void Start()
    {
        scoreText.text = "Score LVL: " + PlayerPrefs.GetInt("score");
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
