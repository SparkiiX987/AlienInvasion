using System.Collections.Generic;
using UnityEngine;

public class AlienGestion : MonoBehaviour
{
    private Transform _tranform;
    private LineRenderer[] linesRenderers;
    public List<Transform> otherStructuresposition = new List<Transform>();

    void Start()
    {
        _tranform = transform;
        linesRenderers = GetComponents<LineRenderer>();
        print(linesRenderers.Length);
    }

    void Update()
    {
        UpdateLinesRenderers();
    }


    private void UpdateLinesRenderers()
    {
        for(int i = 0; i < linesRenderers.Length; i++)
        {
            linesRenderers[i].SetPosition(0, _tranform.position);
            linesRenderers[i].SetPosition(1, otherStructuresposition[i].position);
        }
    }

}
