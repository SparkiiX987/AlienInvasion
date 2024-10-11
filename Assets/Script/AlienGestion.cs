using System.Collections.Generic;
using UnityEngine;

public class AlienGestion : MonoBehaviour
{
    [SerializeField] private GameObject joinPrefab;

    private Transform _tranform;
    private SpringJoint2D[] springjoins;

    void Start()
    {
        _tranform = transform;
        springjoins = GetComponents<SpringJoint2D>();

        for (int i = 0; i < springjoins.Length; i++)
        {
            GameObject newJoin = Instantiate(joinPrefab);
            Transform otherTransform = springjoins[i].connectedBody.transform;
            Vector2 avancement = (otherTransform.position - _tranform.position);
            Vector2 pos = (Vector2)_tranform.position + avancement / 2;
            newJoin.transform.position = pos;

            float posNorm = Mathf.Sqrt(Mathf.Pow(avancement.x, 2) + Mathf.Pow(avancement.y, 2));
            float rotation = Mathf.Atan2(avancement.y, avancement.x) * Mathf.Rad2Deg;

            newJoin.transform.rotation = Quaternion.Euler(0, 0, rotation);
            float scale = Mathf.Sqrt(Mathf.Pow(avancement.x, 2) + Mathf.Pow(avancement.y, 2));
            print(scale);
            newJoin.transform.localScale = new Vector3(newJoin.transform.localScale.x, scale, 1);

            newJoin.transform.SetParent(_tranform, true);
            newJoin.GetComponent<PositionUpdater>().target = otherTransform;

        }
    }

    void Update()
    {

    }
}
