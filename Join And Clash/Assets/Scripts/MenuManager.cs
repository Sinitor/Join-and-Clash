using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image nextImage;
    [SerializeField] private int score;
    private void Update()
    {
        if (!Boss.boss.boosIsAlive)
        {
            StartCoroutine(NextLVL());
        }
    } 
    IEnumerator NextLVL()
    {
        yield return new WaitForSeconds(2);
        nextImage.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
        }
    } 
    public void NewLVL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
