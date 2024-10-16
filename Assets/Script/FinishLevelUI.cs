using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevelUI : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject buttonsParent;
    [SerializeField] private List<GameObject> stars = new List<GameObject>();
    [SerializeField] private Sprite ownedStarSprite;

    private void OnEnable()
    {
        StartCoroutine(EndAnimation());
    }

    private IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < stars.Count; i++)
        {
            if (!(PlayerPrefs.GetInt(levelName + "Star" + i) != 1))
                break;
            stars[i].GetComponent<Image>().sprite = ownedStarSprite;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        buttonsParent.SetActive(true);
    }

    public void NextLevel()
    {
        //SceneManager.LoadScene();
    }

    public void Restart()
    {
        SceneManager.LoadScene(levelName);
    }

    public void SelectLevel()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        GameObject UI = GameObject.Find("Menu");
        UI.GetComponent<RectTransform>().anchoredPosition = new Vector2(1920, 0);
        //SceneManager.UnloadSceneAsync(levelName);
    }

}
