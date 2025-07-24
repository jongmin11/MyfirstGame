using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;
    public int obestacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obestacleCount = obstacles.Length;

        for (int i = 0; i < obestacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obestacleCount);
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("BackGround"))
        {
            BoxCollider2D boxCol = collision.GetComponent<BoxCollider2D>();
            if (boxCol == null)
            {

                return;
            }

            float width = boxCol.size.x * collision.transform.localScale.x;
            Vector3 pos = collision.transform.position;
            pos.x += width * numBgCount;
            collision.transform.position = pos;
            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obestacleCount);
        }
    }
}
