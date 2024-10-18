using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> Stars = new List<GameObject>();
    [SerializeField] private string levelName;

    public void UpdateVisual()
    {
        if (PlayerPrefs.GetInt(levelName) == 1)
        {
            for(int i = 0; i < Stars.Count; i++)
            {
                if(PlayerPrefs.GetInt(levelName + "Star" + (i + 1)) == 1)
                {
                    Stars[i].SetActive(true);
                }
                else
                {
                    Stars[i].SetActive(false);
                }
            }
        }
    }

    public void ResetVisual()
    {
        for (int i = 0; i < Stars.Count; i++)
        {
            Stars[i].SetActive(false);
        }
    }
}
