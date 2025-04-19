using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Runtime Board Data")]
    [SerializeField] private BoardSO _board;

    private static GameManager _instance;

    private void Awake()
    {
        // Ensure only one GameManager exists
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject); // Persist between scenes
    }

    private void OnEnable()
    {
        _board = Resources.Load<BoardSO>("SOInstance/Core/Board");
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
        if (_board != null)
        {
            _board.ClearBoardTokens();
            Debug.Log("Board cleared on application quit.");
        }
    }
}
