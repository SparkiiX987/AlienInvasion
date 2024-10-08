using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string alienTag;
    [SerializeField] private Controls playerControls;

    private InputAction mouse;

    private Vector2 oldPosition;

    private void Awake()
    {
        playerControls = new Controls();
    }

    private void OnEnable()
    {
        mouse = playerControls.Inputs.Mouse;
        mouse.Enable();
        mouse.performed += OnClic;

    }

    private void OnDisable()
    {
        mouse.Disable();
    }

    void Update()
    {

    }

    private void OnClic(InputAction.CallbackContext ctx)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.tag == alienTag)
        {
            oldPosition = hit.collider.transform.position;
        }
    }

    private void Drag()
    {

    }

}
