using System.Collections;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;

    public void Exit()
    {
        Application.Quit();
    }

    public void RightToLeft()
    {
        StartCoroutine(Decale(true));
    }

    public void LeftToRight()
    {
        StartCoroutine(Decale(false));
    }

    private IEnumerator Decale(bool goingRight)
    {
        float value = 0;
        if (goingRight)
            value = 19.2f;
        else
            value = -19.2f;

        for (int i = 0; i < 100; i++)
        {
            _transform.anchoredPosition = new Vector2(_transform.anchoredPosition.x + value, _transform.anchoredPosition.y);
            yield return new WaitForSeconds(0.005f);
        }
    }
}
