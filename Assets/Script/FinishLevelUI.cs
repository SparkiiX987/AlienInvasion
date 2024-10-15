using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelUI : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject buttonsParent;
    [SerializeField] private List<GameObject> stars = new List<GameObject>();
    [SerializeField] private Sprite ownedStarSprite;

    private void OnEnable()
    {
        
    }

    private IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < stars.Count; i++)
        {
            if (!(PlayerPrefs.GetInt(levelName + "Star" + i) == 1))
                yield return null;
            stars[i].GetComponent<SpriteRenderer>().sprite = ownedStarSprite;
            yield return new WaitForSeconds(0.5f);
        }
    }

}
