using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // 플레이어의 스텟 값을 조절하기 편하도록 게임 매니저에 등록
    [SerializeField] private PlayerStatsData playerStatsData;
    public PlayerStatsData PlayerStatsData => playerStatsData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}