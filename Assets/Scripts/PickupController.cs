using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask pickableLayer;
    [SerializeField] Transform holdArea;

    Vector3 raycastPos;

    GameObject pickedItem;

    Rigidbody pickedItemRB;

    HighlightItem highlightItemScript;

    Collider objectCollider;

    float spherecastRadius = 0.1f;
    float maxDistance = 10.0f;
    float pickupForce = 150.0f;
    float throwForce = 600.0f;
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(pickedItem == null)
            {
                raycastPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;

                if (Physics.SphereCast(raycastPos, spherecastRadius, mainCamera.transform.forward, out hit, maxDistance, pickableLayer))
                {
                    Pickup(hit.transform.gameObject);
                }
            }
            else if (pickedItem != null)
            {
                Drop();
            }
        }

        if(pickedItem != null)
        {
            MoveItem();
        }
    }

    void Pickup (GameObject pickedObj)
    {
        highlightItemScript = pickedObj.GetComponent<HighlightItem>();
        objectCollider = pickedObj.GetComponent<Collider>();
        objectCollider.enabled = false;
        highlightItemScript.shouldHighlight = false;
        pickedItem = pickedObj;
        pickedItem.transform.position = holdArea.transform.position;
        pickedItemRB = pickedObj.GetComponent<Rigidbody>();
        pickedItemRB.useGravity = false;
        pickedItemRB.drag = 10;
        pickedItemRB.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Drop()
    {
        objectCollider.enabled = true;
        highlightItemScript.shouldHighlight = true;
        highlightItemScript = null;
        objectCollider = null;
        pickedItemRB.AddForce(mainCamera.transform.forward * throwForce);
        pickedItemRB.useGravity = true;
        pickedItemRB.drag = 1;
        pickedItemRB.constraints = RigidbodyConstraints.None;
        pickedItem = null;
    }

    void MoveItem()
    {
        if(Vector3.Distance(pickedItem.transform.position, holdArea.transform.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.transform.position - pickedItem.transform.position);
            pickedItemRB.AddForce(moveDirection * pickupForce);
        }
    }
}
