using UnityEngine;
using System.Collections;

public class GenerateGrid : MonoBehaviour
{
    public GameObject Cell;
    public GameObject Board;
    // Use this for initialization
    void Start()
    {
        for (float x = 5, i = 0; x > -4; x -= 0.5f, i++)
        {
            for (float y = -2.5f, j = 0; y < 3.5f; y += 0.5f, j++)
            {
                if (y == -2.5f || y==3|| x ==-3.5)
                {
                    var block = Instantiate(Board, new Vector3(y, x, 0), Quaternion.identity);
                }
                else
                {
                    var block = Instantiate(Cell, new Vector3(y, x, 0), Quaternion.identity);
                }

            }
        }
    }
}
