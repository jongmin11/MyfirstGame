using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ItemScoreGiver : MonoBehaviour
{
    [Header("주는 점수 (예: 1, 2, 3)")]
    public int scoreValue = 1;

    [Header("코인에 보일 이미지 ")]
    public Sprite coinSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (coinSprite != null)
        {
            spriteRenderer.sprite = coinSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Application.isPlaying) return;

        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(scoreValue);
            other.GetComponent<Player>()?.OnScore();

            Destroy(gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        gameObject.name = $"Coin_{scoreValue}pt";
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (coinSprite != null && sr != null)
            sr.sprite = coinSprite;
    }

    private void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.yellow;
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = 14;
        Handles.Label(transform.position + Vector3.up * 1f, $"{scoreValue}점", style);
    }
#endif
}
