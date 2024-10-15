using System.Collections;
using System.Collections.Generic;
using System.Transactions;
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
    [SerializeField] private GameObject joinPrefab;
    [SerializeField] private Gradient preLinkGradient;

    [SerializeField] private List<GameObject> preLinks = new();

    private bool isDraging;
    private Transform dragingObject;

    public bool started;

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
        if (!started)
            return;
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
                dragingObject.GetComponent<PositionUpdater>().draging = true;
                StartCoroutine(PreLink());
                return;
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
            dragingObject.GetComponent<PositionUpdater>().draging = false;
            PlaceStructure();
        }
    }

    private void Drag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragingObject.position = new Vector2(mousePos.x, mousePos.y);
    }

    private IEnumerator PreLink()
    {
        while(isDraging)
        {
            for(int i = 0; i < preLinks.Count; i++)
            {

                Destroy(preLinks[i]);
            }
            preLinks.Clear();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.CircleCastAll(mousePos, placingRange, Vector2.zero, placingRange);

            RaycastHit2D hit = Physics2D.CircleCast(mousePos, .7f, Vector2.zero, .7f);

            if (hits.Length > 0 && !(hit.collider != null && hit.collider.gameObject != dragingObject.gameObject))
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.tag == alienStructTag)
                    {
                        GameObject newJoin = Instantiate(joinPrefab);
                        preLinks.Add(newJoin);
                        newJoin.transform.position = mousePos;
                        newJoin.transform.SetParent(transform, true);
                        LineRenderer lineRenderer = newJoin.GetComponent<LineRenderer>();
                        lineRenderer.sortingOrder = -20;
                        lineRenderer.colorGradient = preLinkGradient;
                        lineRenderer.SetPosition(0, mousePos);
                        lineRenderer.SetPosition(1, hits[i].transform.position);
                    }
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < preLinks.Count; i++)
        {

            Destroy(preLinks[i]);
        }
        preLinks.Clear();
        yield return null;
    }

    private void PlaceStructure()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.CircleCastAll(mousePos, placingRange, Vector2.zero, placingRange);
        List<Rigidbody2D> rigidbodys = new();
        if (hits.Length > 0)
        {
            RaycastHit2D hit = Physics2D.CircleCast(mousePos, .7f, Vector2.zero, .7f);
            if (hit.collider != null && hit.collider.gameObject != dragingObject.gameObject)
                return;
            for (int i = 0; i < hits.Length; i++)
            {
                //calcule la distance entre l'objet placer et la boule structure
                float distance = Mathf.Sqrt(Mathf.Pow((mousePos.x - hits[i].transform.position.x), 2) + Mathf.Pow((mousePos.y - hits[i].transform.position.y), 2));
                if(hits[i].transform.tag == alienStructTag && distance > minRange)
                {
                    rigidbodys.Add(hits[i].transform.GetComponent<Rigidbody2D>());
                }
            }
            if (rigidbodys.Count == 0)
                return;

            GameObject newStructurePoint = Instantiate(StructurePrefab);
            newStructurePoint.transform.position = new Vector3 (mousePos.x, mousePos.y, -.1f);
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
