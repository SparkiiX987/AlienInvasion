using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> Stars = new List<GameObject>();
    [SerializeField] private string levelName;
    [SerializeField] private Sprite starOwned;
    [SerializeField] private Sprite starUnowned;

    public void UpdateVisual()
    {
        if (PlayerPrefs.GetInt(levelName) == 1)
        {
            // change look
            for(int i = 0; i < Stars.Count; i++)
            {
                Stars[i].SetActive(true);
                if(PlayerPrefs.GetInt(levelName + "Star" + i) == 1)
                {
                    Stars[i].GetComponent<Image>().sprite = starOwned;
                }
                else
                {
                    Stars[i].GetComponent<Image>().sprite = starUnowned;
                    return;
                }
            }
        }
    }
}
