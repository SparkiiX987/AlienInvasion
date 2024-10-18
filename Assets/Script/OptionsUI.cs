using UnityEngine;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private LevelSelector levelSelector;

    public void ResetProgression()
    {
        PlayerPrefs.DeleteAll();
        levelSelector.ResetAllStars();
    }
}
