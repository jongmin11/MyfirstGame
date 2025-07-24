using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [Header("topObject ���� ��ġ ����Ʈ (����)")]
    public float[] fixedYTop = { 2f, 3f, 4f };

    [Header("bottomObject ���� ��ġ ����Ʈ (�Ʒ���)")]
    public float[] fixedYBottom = { -2f, -3f, -4f };

    [Header("���� ũ�� ����")]
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    [Header("���� ������Ʈ")]
    public Transform topObject;
    public Transform bottomObject;

    [Header("��ֹ� �� ����")]
    public float widthPadding = 4f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        // top/bottom Y ��ġ ���� ������ ����Ʈ �߿��� ����
        float topY = fixedYTop[Random.Range(0, fixedYTop.Length)];
        float bottomY = fixedYBottom[Random.Range(0, fixedYBottom.Length)];

        // top/bottom ������Ʈ ���� ���� ��ġ�� ��ġ
        if (topObject != null) topObject.localPosition = new Vector3(0, topY);
        if (bottomObject != null) bottomObject.localPosition = new Vector3(0, bottomY);

        // ������Ʈ ��ü ��ġ�� X�� �̵�
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        transform.position = placePosition;

        return placePosition;
    }
}
