using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private List<string> texts = new();
    private int step = 0;


    private void Start()
    {
        text.text = texts[step];
    }

    public void Next()
    {
        step++;
        if(step == texts.Count)
        {
            gameManager.started = true;
            gameObject.SetActive(false);
            return;
        }
        text.text = texts[step];
    }
}
