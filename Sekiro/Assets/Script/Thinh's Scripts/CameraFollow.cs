using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform player = null;
    private Vector3 offset;
    [SerializeField] private Transform lookAtPoint =null;
    [SerializeField] private Transform pivot = null;
    //[SerializeField] private Transform cameraSpot = null;
    [SerializeField] private float maxViewAngle = 0f;
    [SerializeField] private float minViewAngle = 0f;
    private Vector2 deltamouse = new Vector2(0, 0);
    private Transform defaultPivotPos;    
   

    //Input System Methods
    public void OnLook(InputAction.CallbackContext context)
    {
        deltamouse = context.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        offset = player.position - transform.position;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraLook();
       
    }

    
    private void CameraLook()
    {
        //Rotate the player horizontally
        float horizontal = deltamouse.x;
        if (player.GetComponent<PlayerStat>().alive)
        {
            player.Rotate(0, horizontal, 0);
        }
        //Rotate the pivot vertically
        float vertical = deltamouse.y;
        pivot.Rotate(-vertical, 0, 0);

        //Limit the up/ down rotation
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f+minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        //Move the camera based on the player rotation
        float rotateYAngle = player.eulerAngles.y;
        float rotateXAngle = pivot.eulerAngles.x;

        Quaternion rotateAngle = Quaternion.Euler(rotateXAngle, rotateYAngle, 0);

        transform.position = player.position - (rotateAngle * offset);

        if(transform.position.y < player.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y + 1f, transform.position.z);
        }

        transform.LookAt(lookAtPoint);
        
    }
    
}
