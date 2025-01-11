using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _mainCamera;

    private Vector3 _prevCameraPos;

    public Vector3 normalizedCamera; // The normalized direction towards the camera

    [SerializeField]
    private int cameraQuadernt = 1;

    // Gizmo color and length
    public Color gizmoColor = Color.red;
    public float gizmoLength = 2.0f;


    [SerializeField]
    private SpriteRenderer spriteRender;

    private Dictionary<string, Sprite> spriteDictionary;

    void Start() {

        _mainCamera = FindFirstObjectByType<Camera>();
        _prevCameraPos = _mainCamera.transform.position;

        if (_mainCamera == null)
        {
            Debug.LogError("No camera found in the scene!");
        }

        spriteDictionary = new Dictionary<string, Sprite>();

        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Elf");

        // Populate the dictionary
        foreach (Sprite sprite in sprites)
        {
            // Debug.Log(sprite.name);
            spriteDictionary[sprite.name] = sprite;
        }

    }

    public Sprite DetermineSprite(Vector2 point)
    {
        // Special case: The origin is not in any quadrant.
        if (point == Vector2.zero)
            return spriteDictionary["front_right"];

        // Calculate the angle in radians using Mathf.Atan2 (returns value in [-π, π])
        float angle = Mathf.Atan2(point.y, point.x);

        // Normalize the angle to [0, 2π]
        if (angle < 0)
            angle += 2 * Mathf.PI;

        // Determine the quadrant based on the angle
        if (angle >= 0 && angle < Mathf.PI / 2)
            // return 1; // Quadrant 1 [+x, +z] Front-Right
            return spriteDictionary["front_right"];

        else if (angle >= Mathf.PI / 2 && angle < Mathf.PI)
            // return 2; // Quadrant 2 [+x, -z] From-Left
            return spriteDictionary["front_left"];

        else if (angle >= Mathf.PI && angle < 3 * Mathf.PI / 2)
            // return 3; // Quadrant 3 [-x, -z] Back-Left
            return spriteDictionary["back_left"];

        else
            // return 4; // Quadrant 4 [-x, +z] Back-Right
            return spriteDictionary["back_right"];
    }

    void Update()
    {
        if (_mainCamera != null)
        {
            if (_prevCameraPos != _mainCamera.transform.position) {

                // Calculate the direction vector from the character to the camera
                Vector3 direction = _mainCamera.transform.position - transform.position;

                spriteRender.sprite = DetermineSprite(new Vector2(direction.x,direction.z));

                // Flatten both vectors to the XZ plane
                Vector3 flattenedDirection = new Vector3(direction.x, 0f, direction.z).normalized;

                // Optionally: make the sprite face the camera (as you had before)
                transform.rotation = Quaternion.LookRotation(-flattenedDirection);
                
                _prevCameraPos = _mainCamera.transform.position;
            }
        }
    }
}
