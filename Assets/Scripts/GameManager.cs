using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Runtime Board Data")]
    [SerializeField] private BoardSO board;

    private static GameManager instance;

    private void Awake()
    {
        // Ensure only one GameManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist between scenes
    }

    private void OnEnable()
    {
        Application.quitting += OnAppQuit;
    }

    private void OnDisable()
    {
        Application.quitting -= OnAppQuit;
    }

    private void OnAppQuit()
    {
        ClearBoard();
    }

    private void ClearBoard()
    {
        if (board != null)
        {
            board.ClearTiles();
            Debug.Log("Board cleared on application quit.");
        }
    }
}
