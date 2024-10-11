using UnityEngine;

public class PositionUpdater : MonoBehaviour
{
    [SerializeField] private Transform parent;
    private Transform _trasform;

    private void Start()
    {
        _trasform = transform;
    }


    void Update()
    {
        _trasform.position = parent.position;
    }
}