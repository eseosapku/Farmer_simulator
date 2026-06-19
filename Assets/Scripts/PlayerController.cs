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
    public GameObject fertiliser;
    public Transform handPosition;
    public AudioSource dropAudio;
    public EconomyManager economy;
    public GameObject compost;
    public bool moveForward = false;
    public bool moveBack = false;
    public bool moveLeft = false;
    public bool moveRight = false;


    private float targetRotationX;
    private float targetRotationY;
    private Camera mainCamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnityEngine.InputSystem.EnhancedTouch.TouchSimulation.Enable();
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
            if (economy != null && !economy.TryUseFertilizer()) return;
            ThrowItem(fertiliser, "Fertilizer_Bag");
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ThrowItem(compost, "Compost_Bag");
        }
        if (isSimulating == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed || moveForward)
        {
            transform.Translate((-1f * speed * Time.deltaTime), 0f, 0f);
        }
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed || moveBack)
        {
            transform.Translate((1f * speed * Time.deltaTime), 0f, 0f);
        }
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed || moveLeft)
        {
            transform.Translate(0f, 0f, (-1f * speed * Time.deltaTime));
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed || moveRight)
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
    void DropFertiliser()
    {
        if (fertiliser == null || mainCamera == null) return;

        if (economy != null && !economy.TryUseFertilizer())
        {
            return; 
        }

        Vector3 spawnPos = (handPosition != null) ? handPosition.position : transform.position + transform.forward;
        GameObject bag = Instantiate(fertiliser, spawnPos, Quaternion.identity);
        bag.name = "Fertiliser_Bag";

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
        Debug.Log("Fertiliser bag thrown toward target!");
    }

    void ThrowItem(GameObject prefab, string itemName)  
    {
        if (prefab == null || mainCamera == null) return;

        Vector3 spawnPos = (handPosition != null) ? handPosition.position : transform.position + transform.forward;
        GameObject item = Instantiate(prefab, spawnPos, Quaternion.identity);
        item.name = itemName;

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            Vector3 target = Physics.Raycast(ray, out hit, 100f) ? hit.point : ray.origin + ray.direction * 15f;
            Vector3 dir = (target - spawnPos).normalized;
            dir.y += 0.3f;
            rb.linearVelocity = dir * 8f;
        }

        if (dropAudio != null) dropAudio.Play();
        Debug.Log(itemName + " thrown!");
    }

    public void ForwardDown() { moveForward = true; }
    public void ForwardUp() { moveForward = false; }
    public void BackDown() { moveBack = true; }
    public void BackUp() { moveBack = false; }
    public void LeftDown() { moveLeft = true; }
    public void LeftUp() { moveLeft = false; }
    public void RightDown() { moveRight = true; }
    public void RightUp() { moveRight = false; }

    public void ThrowFertilizerMobile()
    {
        if (economy != null && !economy.TryUseFertilizer()) return;
        ThrowItem(fertiliser, "Fertilizer_Bag");
    }

    public void ThrowCompostMobile()
    {
        ThrowItem(compost, "Compost_Bag");
    }

    public void StartWaterMobile()
    {
        if (waterAudio != null) waterAudio.Play();
    }

    public void StopWaterMobile()
    {
        EraseLine();
        if (waterAudio != null) waterAudio.Stop();
    }


}