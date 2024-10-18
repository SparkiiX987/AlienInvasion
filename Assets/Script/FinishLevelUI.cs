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
    public string nextLevel;

    private void OnEnable()
    {
        if(tag == "lose")
            StartCoroutine(LoseAnime());
        else
            StartCoroutine(EndAnimation());
    }

    private IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < stars.Count; i++)
        {
            if ((PlayerPrefs.GetInt(levelName + "Star" + (i + 1)) != 1))
                break;
            stars[i].GetComponent<Image>().sprite = ownedStarSprite;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        buttonsParent.SetActive(true);
    }

    private IEnumerator LoseAnime()
    {
        yield return new WaitForSeconds(0.5f);
        buttonsParent.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void Restart()
    {
        SceneManager.LoadScene(levelName);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
