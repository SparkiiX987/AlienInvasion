using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> Levels = new();

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        if (PlayerPrefs.GetInt("Level1") == 0)
        {
            PlayerPrefs.SetInt("Level1", 1);
        }
        for (int i = 0; i < Levels.Count; i++)
        {
            if (PlayerPrefs.GetInt("Level" + (i + 1)) == 1)
            {
                Levels[i].SetActive(true);
                Levels[i].GetComponent<LevelButton>().UpdateVisual();
            }
            else
                return;
        }
    }

    public void ResetAllStars()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].SetActive(false);
            Levels[i].GetComponent<LevelButton>().ResetVisual();
        }
        SetUp();
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene("Level" +  level);
    }

}
