using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private List<LevelButton> Levels = new();

    private void Start()
    {
        if(PlayerPrefs.GetInt("Level1") != 1)
        {
            PlayerPrefs.SetInt("level1", 1);
        }
        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].UpdateVisual();
        }
    }
}
