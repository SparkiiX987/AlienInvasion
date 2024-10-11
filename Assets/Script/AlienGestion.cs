using System.Collections.Generic;
using UnityEngine;

public class AlienGestion : MonoBehaviour
{
    [SerializeField] private GameObject joinPrefab;

    private Transform _tranform;
    private SpringJoint2D[] springjoins;
    List<LineRenderer> lineRenderers = new();


    void Start()
    {
        _tranform = transform;
        springjoins = GetComponents<SpringJoint2D>();

        for (int i = 0; i < springjoins.Length; i++)
        {
            GameObject newJoin = Instantiate(joinPrefab, _tranform);
            newJoin.transform.position = Vector2.zero;
            lineRenderers.Add(newJoin.GetComponent<LineRenderer>());
            newJoin.GetComponent<LineRenderer>().widthMultiplier = 0.4f;
        }
    }

    void Update()
    {
        for (int i = 0; i < lineRenderers.Count; i++)
        {
            lineRenderers[i].SetPosition(0, _tranform.position);
            lineRenderers[i].SetPosition(1, springjoins[i].connectedBody.position);
        }
    }
}
