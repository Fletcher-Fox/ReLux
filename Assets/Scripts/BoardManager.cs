using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private static BoardManager _instance;

    [SerializeField] private BoardSO _board;
    public GameObject reticlePrefab;
    public GameObject reticleInstance;

    public GameObject pathLine;
    public LineRenderer lineRenderer;

    private Material _pathMaterial;

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
        _pathMaterial = Resources.Load<Material>("Materials/pathMaterial");
        _board.reticleEvent.AddListener(HandleReticle);
        _board.drawPath.AddListener(HandleDrawPath);
    }

    void OnDisable()
    {
        _board.reticleEvent.RemoveListener(HandleReticle);
        _board.drawPath.RemoveListener(HandleDrawPath);
    }

    void Start()
    {
        pathLine = new GameObject("pathLine");
        lineRenderer = pathLine.AddComponent<LineRenderer>();
        reticleInstance = Instantiate(reticlePrefab, Vector3.zero, Quaternion.identity);
        HandleReticle(false, Vector3.zero);
    }

    private void HandleReticle(bool visible, Vector3 position)
    {
        reticleInstance.SetActive(visible);
        reticleInstance.transform.position = position;
    }

    private void HandleDrawPath(bool visible, Vector3 start, Vector3 end)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.startWidth = 0.6f;
        lineRenderer.endWidth = 0.6f;

        lineRenderer.material = _pathMaterial;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        lineRenderer.gameObject.SetActive(visible);
    }
}
