using System.Collections;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private string structureTag;
    [SerializeField] private GameObject LevelEndUI;
    [SerializeField] private string levelName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(structureTag))
        {
            StartCoroutine(WaitAndDisplay(0.5f));
        }
    }

    private IEnumerator WaitAndDisplay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        LevelEndUI.SetActive(true);
        PlayerPrefs.SetInt(levelName + "Star" + 1, 1);
    }

}
