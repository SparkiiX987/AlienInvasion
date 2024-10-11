using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string alienTag;
    [SerializeField] private string alienStructTag;
    [SerializeField] private Controls playerControls;
    [SerializeField] private GameObject StructurePrefab;
    [SerializeField] private float placingRange;
    [SerializeField] private float minRange;
    [SerializeField] private Transform structureParent;

    private bool isDraging;
    private Transform dragingObject;

    private InputAction mouse;

    private void Awake()
    {
        playerControls = new Controls();
    }

    private void OnEnable()
    {
        mouse = playerControls.Inputs.Mouse;
        mouse.Enable();
        mouse.started += OnClick;
        mouse.performed += OnMaintain;
        mouse.canceled += OnReleased;


    }

    private void OnDisable()
    {
        mouse.Disable();
    }

    void Update()
    {
        if (isDraging)
        {
            Drag();
        }
    }

    private void OnClick(InputAction.CallbackContext ctx)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector2.zero);
        if (hit.Length == 0)
            return;

        for(int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null && hit[i].collider.tag == alienTag)
            {
                dragingObject = hit[i].transform;
                isDraging = true;

            }
        }
    }

    private void OnMaintain(InputAction.CallbackContext ctx)
    {
        if (isDraging)
        {
            Drag();
        }
    }

    private void OnReleased(InputAction.CallbackContext ctx)
    {
        if (isDraging)
        {
            isDraging = false;
            PlaceStructure();
        }
    }

    private void Drag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragingObject.position = new Vector2(mousePos.x, mousePos.y);
    }

    private void PlaceStructure()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(dragingObject.position, placingRange, Vector2.zero, placingRange);
        List<Rigidbody2D> rigidbodys = new();
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                //calcule la distance entre l'objet placer et la boule structure
                float distance = Mathf.Sqrt(Mathf.Pow((dragingObject.position.x - hits[i].transform.position.x), 2) + Mathf.Pow((dragingObject.position.y - hits[i].transform.position.x), 2));
                if(hits[i].transform.tag == alienStructTag && distance >minRange)
                {
                    rigidbodys.Add(hits[i].transform.GetComponent<Rigidbody2D>());
                }
            }
            if (rigidbodys.Count == 0)
                return;

            GameObject newStructurePoint = Instantiate(StructurePrefab);
            newStructurePoint.transform.position = dragingObject.position;
            newStructurePoint.transform.SetParent(structureParent, true);
            for(int i = 0; i < rigidbodys.Count; i++)
            {
                SetupStructurePoint(newStructurePoint);
            }
            SpringJoint2D[] springJoins = newStructurePoint.GetComponents<SpringJoint2D>();
            for (int i = 0;i < springJoins.Length; i++)
            {
                InitSpringJoin(springJoins[i], rigidbodys[i]);
            }

            Destroy(dragingObject.gameObject);
            dragingObject = null;
        }
    }

    private void SetupStructurePoint(GameObject newStructurePoint)
    {
        newStructurePoint.AddComponent<SpringJoint2D>();
    }

    private void InitSpringJoin(SpringJoint2D springJoin, Rigidbody2D rigidbody)
    {
        springJoin.connectedBody = rigidbody;
        springJoin.enableCollision = true;
        springJoin.frequency = 25;
        springJoin.connectedAnchor = Vector2.zero;
        springJoin.enableCollision = true;
    }

}
