using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("생성할 코인 프리팹들")]
    public GameObject[] coinPrefabs;

    [Header("생성 범위 (카메라 위치 기준 상대값)")]
    public Vector2 offsetMin = new Vector2(-4f, -3f);
    public Vector2 offsetMax = new Vector2(4f, 3f);

    [Header("겹침 검사")]
    public float checkRadius = 0.5f;
    public LayerMask collisionLayer;

    [Header("시간마다 생성")]
    public float spawnInterval = 2f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            TrySpawnCoin();
            timer = 0f;
        }
    }

    void TrySpawnCoin()
    {
        // 기준 위치 = 카메라(또는 자기 위치)
        Vector2 basePos = transform.position;

        // 상대 범위 안에서 랜덤 위치 생성
        Vector2 randomPos = new Vector2(
            Random.Range(basePos.x + offsetMin.x, basePos.x + offsetMax.x),
            Random.Range(basePos.y + offsetMin.y, basePos.y + offsetMax.y)
        );

        // 겹침 검사
        Collider2D hit = Physics2D.OverlapCircle(randomPos, checkRadius, collisionLayer);
        if (hit != null)
        {
            Debug.Log($"[스킵] 겹침 감지: {randomPos}");
            return;
        }

        // 프리팹 랜덤 선택
        GameObject prefab = coinPrefabs[Random.Range(0, coinPrefabs.Length)];

        if (prefab == null)
        {
            Debug.LogWarning("[오류] coinPrefabs에 null이 있음!");
            return;
        }

        Instantiate(prefab, randomPos, Quaternion.identity);
        Debug.Log($"[코인 생성] {prefab.name} at {randomPos}");
    }
}