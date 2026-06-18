using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 0.5f;
    public LineRenderer waterLine;
    public Transform waterTank;
    public bool isSimulating = false;
    public AudioSource waterAudio;
    public GameObject fertilizer;
    public Transform handPosition;
    public AudioSource dropAudio;
    public EconomyManager economy;



    private float targetRotationX;
    private float targetRotationY;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (waterAudio != null)
        {
            waterAudio.Stop();
        }
        mainCamera = Camera.main;

        targetRotationY = transform.eulerAngles.y;
        if (mainCamera != null)
        {
            targetRotationX = mainCamera.transform.localEulerAngles.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            DropFertilizer();
        }
        if (isSimulating == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            transform.Translate((-1f * speed * Time.deltaTime), 0f, 0f);
        }
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            transform.Translate((1f * speed * Time.deltaTime), 0f, 0f);
        }
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            transform.Translate(0f, 0f, (-1f * speed * Time.deltaTime));
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            transform.Translate(0f, 0f, (1f * speed * Time.deltaTime));
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (waterAudio != null)
            {
                waterAudio.Play();
            }
        }

        if (Mouse.current.leftButton.isPressed)
        {
            ShootWater();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            EraseLine();

            if (waterAudio != null)
            {
                waterAudio.Stop(); // Kills the audio source instantly on release frame!
            }
        }

        HandleRotation();
    }

    void HandleRotation()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Vector2 mouseMovement = Mouse.current.delta.ReadValue();
        transform.Rotate(Vector3.up * mouseMovement.x * rotationSpeed);
        if (mainCamera != null)
        {
            mainCamera.transform.Rotate(Vector3.left * mouseMovement.y * rotationSpeed);
        }
    }

    void ShootWater()
    {
        if (waterLine == null || waterTank == null || mainCamera == null) return;
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        waterLine.SetPosition(0, waterTank.position);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            waterLine.SetPosition(1, hit.point);
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);   // ← ADD THIS LINE
            CarrotSim carrot = hit.collider.GetComponentInParent<CarrotSim>();
            if (carrot != null)
            {
                carrot.moisture = Mathf.Min(carrot.moisture + (25f * Time.deltaTime), 100f);
                Debug.Log("Streaming water onto carrot safely via camera view tracking!");
                if (carrot.moisture >= 100f)
                {
                    carrot.TestingForceGrowth();
                    carrot.moisture = 30f;
                    Debug.Log("Moisture maxed out! Forcing carrot patch to advance growth stage.");
                }
            }
        }
        else
        {
            waterLine.SetPosition(1, ray.origin + (ray.direction * 30f));
        }
    }
    void EraseLine()
    {
        if (waterLine == null) return;
        waterLine.SetPosition(0, Vector3.zero);
        waterLine.SetPosition(1, Vector3.zero);
    }
    void DropFertilizer()
    {
        if (fertilizer == null || mainCamera == null) return;

        if (economy != null && !economy.TryUseFertilizer())
        {
            return; 
        }

        Vector3 spawnPos = (handPosition != null) ? handPosition.position : transform.position + transform.forward;
        GameObject bag = Instantiate(fertilizer, spawnPos, Quaternion.identity);
        bag.name = "Fertilizer_Bag";

        Rigidbody rb = bag.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.origin + ray.direction * 15f;
            }

            Vector3 throwDirection = (targetPoint - spawnPos).normalized;
            throwDirection.y += 0.3f; 

            float throwForce = 8f;
            rb.linearVelocity = throwDirection * throwForce;
        }

        if (dropAudio != null) dropAudio.Play();
        Debug.Log("Fertilizer bag thrown toward target!");
    }
}