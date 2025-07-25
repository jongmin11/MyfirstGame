using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [Header("topObject 고정 위치 리스트 (위쪽)")]
    public float[] fixedYTop = { 2f, 3f, 4f };

    [Header("bottomObject 고정 위치 리스트 (아래쪽)")]
    public float[] fixedYBottom = { -2f, -3f, -4f };

    [Header("구멍 크기 범위")]
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    [Header("참조 오브젝트")]
    public Transform topObject;
    public Transform bottomObject;

    [Header("장애물 간 간격")]
    public float widthPadding = 4f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        // top/bottom Y 위치 각각 고정된 리스트 중에서 선택
        float topY = fixedYTop[Random.Range(0, fixedYTop.Length)];
        float bottomY = fixedYBottom[Random.Range(0, fixedYBottom.Length)];

        // top/bottom 오브젝트 각각 고정 위치에 배치
        if (topObject != null) topObject.localPosition = new Vector3(0, topY);
        if (bottomObject != null) bottomObject.localPosition = new Vector3(0, bottomY);

        // 오브젝트 자체 위치는 X만 이동
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        transform.position = placePosition;

        return placePosition;
    }
}
