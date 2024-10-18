using System.Collections;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private string structureTag;
    [SerializeField] private GameObject LevelEndUI;
    [SerializeField] private string levelName;
    [SerializeField] private string nextLevelName;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float stars2Timer;
    [SerializeField] private float stars3Timer;
    [SerializeField] private GameObject nextLevelButton;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(structureTag))
        {
            StartCoroutine(WaitAndDisplay(0.5f));
        }
    }

    private IEnumerator WaitAndDisplay(float seconds)
    {
        gameManager.started = false;
        yield return new WaitForSeconds(seconds);
        LevelEndUI.SetActive(true);
        LevelEndUI.GetComponent<FinishLevelUI>().nextLevel = nextLevelName;
        PlayerPrefs.SetInt(levelName + "Star" + 1, 1);
        if(nextLevelName != "No")
            PlayerPrefs.SetInt(nextLevelName, 1);
        else
            nextLevelButton.SetActive(false);

        if (gameManager.timer <= stars3Timer)
        {
            PlayerPrefs.SetInt(levelName + "Star" + 2, 1);
            PlayerPrefs.SetInt(levelName + "Star" + 3, 1);
        }
        else if (gameManager.timer <= stars2Timer)
        {
            PlayerPrefs.SetInt(levelName + "Star" + 2, 1);
        }
    }
}
