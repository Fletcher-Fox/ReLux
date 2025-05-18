using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private static BoardManager _instance;

    [SerializeField] private BoardSO _board;
    public GameObject reticlePrefab;
    public GameObject reticleInstance;

    private void Awake()
    {
        // Ensure only one BoardManager exists
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject); // Persist between scenes
    }
    void OnEnable()
    {
        _board = Resources.Load<BoardSO>("SOInstance/Core/Board");
        _board.reticleEvent.AddListener(HandleReticle);
    }

    void OnDisable()
    {
        _board.reticleEvent.AddListener(HandleReticle);
    }

    void Start()
    {
        reticleInstance = Instantiate(reticlePrefab, Vector3.zero, Quaternion.identity);
        HandleReticle(false, Vector3.zero);
    }

    void HandleReticle(bool visible, Vector3 position)
    {
        reticleInstance.SetActive(visible);
        reticleInstance.transform.position = position;
    }
}
