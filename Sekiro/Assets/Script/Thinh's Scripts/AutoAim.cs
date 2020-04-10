using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoAim : MonoBehaviour
{
    private GameObject target = null;
    [SerializeField] private GameObject fovStartPoint = null;
    [SerializeField] private float lookSpeed = 500;
    [SerializeField] private float maxAngle = 90;

    private Quaternion targetRotation;
    private Quaternion lookAt;
    private bool isAiming = false;

    public void OnAim(InputAction.CallbackContext context)
    {
        isAiming = context.performed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTarget(GameObject tar)
    {
        target = tar;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            if (FieldofView(fovStartPoint))
            {
                Vector3 direction = target.transform.position - transform.position;
                targetRotation = Quaternion.LookRotation(direction);
                lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
                transform.rotation = lookAt;
            }
            else
            {
                targetRotation = Quaternion.Euler(0, 0, 0);
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
            }
        }
        else if (!isAiming)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
        }
        if (FieldofView(fovStartPoint))
        {
            GetComponentInParent<AimManager>().ActiveAim();
        }
        else
        {
            GetComponentInParent<AimManager>().DeactiveAim();
        }
    }

    private bool FieldofView(GameObject looker)
    {
        if (target != null)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, looker.transform.forward);
            if (angle < maxAngle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
