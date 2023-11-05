 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [Header("Приследуемый обьект")]
    [SerializeField] private Transform target; // Цель за которой следует камера
    [SerializeField] private float sensition; // Чуствительность мыши
    [SerializeField] private float smoothMouseMoveTime;
    [Header("Смещение камеры")]
    [SerializeField] private Vector2 offsetPositon;
    [SerializeField] private Vector3 startOffsetRotation;
    private Vector3 euler; // Углы поворота
    private float mouseY = 0f; // Положение мыши по Y
    private float mouseX = 0f; // Положение мыши по X
    [Header("Настройка приближения камеры")]
    [SerializeField] private float maxZoom = 8f; // Максимальное увелечение
    [SerializeField] private float smoothZoomTime = 0.35f;
    private float zoom; // Увелечение
    private float zoomVelocity = 0f; // Ось Увелечения
    private float mouseScrollDeltaY = 0f;
    [Header("ObstracleReact")]
    [SerializeField] private float minDistance = 1.5f;
    [SerializeField] private float hideDistance = 2f;
    [SerializeField] private LayerMask obstracles;
    [SerializeField] private LayerMask notPlayer;
    private LayerMask cameraOrigin;
    private float maxDistance;

    private void Start()
    {
        cameraOrigin = gameObject.GetComponent<Camera>().cullingMask;
        maxDistance = Vector3.Distance(transform.position, target.position);
        euler = new Vector3
            (startOffsetRotation.x, 
            startOffsetRotation.y, 
            startOffsetRotation.z) ;

        transform.rotation = Quaternion.Euler(euler);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);

        mouseScrollDeltaY = maxZoom;
        zoom = mouseScrollDeltaY;
    }
    private void LateUpdate()
    {
        MouseScrollWheel();
        MouseRotate();
        ObstracleReact();
        PlayerReact();
    }
    private void MouseScrollWheel()
    {
        mouseScrollDeltaY -= Input.mouseScrollDelta.y;
        mouseScrollDeltaY = Mathf.Clamp(mouseScrollDeltaY, 0, maxZoom);

        zoom = Mathf.SmoothDamp(zoom, mouseScrollDeltaY, ref zoomVelocity, smoothZoomTime);
        zoom = Mathf.Clamp(zoom, 0, maxZoom);

        transform.position = target.position;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime);

        transform.Translate(new Vector3(offsetPositon.x, offsetPositon.y, -zoom));
    }
    private void MouseRotate()
    {
        if (Input.GetMouseButton(1))
        {
            mouseX = Input.GetAxis("Mouse X") * sensition;
            mouseY = Input.GetAxis("Mouse Y") * sensition;

        euler.x -= mouseY;

        euler.x = Mathf.Clamp(euler.x, 0f, 90.0f);

        euler.y += mouseX;

        euler.z = startOffsetRotation.z;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euler), Time.fixedDeltaTime * sensition);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
        }
    }

    private void ObstracleReact()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        RaycastHit hit;
        if (Physics.Raycast(target.position, transform.position - target.position, out hit, maxDistance, obstracles))
            transform.position = hit.point;
        else if (distance < maxDistance && !Physics.Raycast(transform.position, -transform.forward, .1f, obstracles))
            transform.position -= transform.forward * .05f;
    }
    private void PlayerReact()
    {
       float distance = Vector3.Distance(transform.position, target.position);
        if (distance < hideDistance)
            gameObject.GetComponent<Camera>().cullingMask = notPlayer;
        else
            gameObject.GetComponent<Camera>().cullingMask = cameraOrigin;
    }

    private void OnDrawGizmosSelected()
    {
        //euler = new Vector3
        //   (startOffsetRotation.x,
        //   startOffsetRotation.y,
        //   startOffsetRotation.z);

        //transform.rotation = Quaternion.Euler(euler);
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

}


