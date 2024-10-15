using UnityEngine;

public class PositionUpdater : MonoBehaviour
{
    [SerializeField] private Transform parent;
    private Transform _trasform;
    public bool draging;

    private void Start()
    {
        _trasform = transform;
    }


    void Update()
    {
        if (draging)
            return;
        _trasform.position = parent.position;
    }
}