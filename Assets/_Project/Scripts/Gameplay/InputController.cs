using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject selectedObject;
    Vector3 offset;

    // Update is called once per frame
    private void Update()
    {
        MoveStickWithMouse();
    }

    private void MoveStickWithMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;

                if (selectedObject.TryGetComponent(out StickController stickController))
                {
                    stickController.OnMovementStart(mousePosition);
                }
            }
        }
        if (selectedObject)
        {
            selectedObject.transform.position = new Vector3(
                selectedObject.transform.position.x,
                mousePosition.y + offset.y,
                selectedObject.transform.position.z
                );

            if (selectedObject.TryGetComponent(out StickController stickController))
            {
                stickController.OnMovement(mousePosition);
            }
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            if (selectedObject.TryGetComponent(out StickController stickController))
            {
                stickController.OnMovementEnd(mousePosition);
            }

            selectedObject = null;
        }
    }
}
