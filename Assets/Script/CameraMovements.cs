using UnityEngine;


public class CameraMovements : MonoBehaviour
{
    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private float speed;
    private Transform _tranform;

    void Start()
    {
        _tranform = transform;
    }


    void Update()
    {
        if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            _tranform.position = new Vector3(_tranform.position.x, transform.position.y + speed, -10f);
            if (_tranform.position.y > maxY)
                _tranform.position = new Vector3(_tranform.position.x, maxY, -10f);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _tranform.position = new Vector3(_tranform.position.x, transform.position.y - speed, -10f);
            if (_tranform.position.y < minY)
                _tranform.position = new Vector3(_tranform.position.x, minY, -10f);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _tranform.position = new Vector3(_tranform.position.x + speed, transform.position.y, -10f);
            if (_tranform.position.x > maxX)
                _tranform.position = new Vector3(maxX, _tranform.position.y, -10f);
        }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _tranform.position = new Vector3(_tranform.position.x - speed, transform.position.y, -10f);
            if (_tranform.position.x < minX)
                _tranform.position = new Vector3(minX, _tranform.position.y, -10f);
        }
    }
}
