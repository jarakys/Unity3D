using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    int[,] Grid = new int[18, 12];
    int top = 17;
    public Controller()
    {
        Grid.Initialize();
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            Grid[i, 0] = 1;
            Grid[i, 11] = 1;
        }
        for (int i = 0; i < Grid.GetLength(1); i++)
        {
            Grid[17, i] = 1;
        }
    }
    public bool CheckPosition(Transform figure)
    {
        for (int i = 0; i < figure.transform.childCount; i++)
        {
            var child = figure.transform.GetChild(i);
            int x = Mathf.Abs(Mathf.RoundToInt((child.transform.position.x + 2.5f) / 0.5f));
            int y = Mathf.Abs(Mathf.RoundToInt((child.transform.position.y - 5f) / 0.5f));
            if (Grid[y, x] == 1)
            {
                return false;
            }
        }
        return true;
    }
    public void SetFigure(GameObject figure)
    {
        for (int i = figure.transform.childCount - 1; i >= 0; i--)
        {
            var child = figure.transform.GetChild(i);
            int x = Mathf.Abs(Mathf.RoundToInt((child.transform.position.x + 2.5f) / 0.5f));
            int y = Mathf.Abs(Mathf.RoundToInt((child.transform.position.y - 5f) / 0.5f));
            Grid[y, x] = 1;
            child.name = y + " " + x;
            if (top > y)
            {
                top = y;
            }
        }
        figure.transform.DetachChildren();
        DeleteFullLines();
    }
    public void DeleteFullLines()
    {
        List<int> list = new List<int>();
        for (int i = Grid.GetLength(0) - 2; i >= top; i--)
        {
            bool fullLines = true;
            for (int j = 1; j < Grid.GetLength(1) - 1; j++)
            {
                if (Grid[i, j] == 0)
                {
                    fullLines = false;
                    break;
                }
            }
            if (fullLines)
            {
                for (int j = 1; j < Grid.GetLength(1) - 1; j++)
                {
                    Grid[i, j] = 0;
                    var obj = GameObject.Find(i + " " + j);
                    Destroy(obj.gameObject);
                }
                list.Add(i);
            }
        }
        int counter = 0;
        foreach (var line in list)
        {
            if (list.Count > 1)
            {
                counter++;
                if (list.Count > counter)
                {
                    for (int i = line - 1; i >= top - 1; i--)
                    {
                        for (int j = 1; j < Grid.GetLength(1) - 1; j++)
                        {
                            if (Grid[i, j] == 1)
                            {
                                if (i > list[counter])
                                {
                                    var block = GameObject.Find(i + " " + j);
                                    block.name = i + counter + " " + j;
                                    block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y + counter * -0.5f, -2);
                                    Grid[i, j] = 0;
                                    Grid[i + counter, j] = 1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = line - 1; i >= top - 1; i--)
                    {
                        for (int j = 1; j < Grid.GetLength(1) - 1; j++)
                        {
                            if (Grid[i, j] == 1)
                            {
                                var block = GameObject.Find(i + " " + j);
                                block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y + counter * -0.5f, -2);
                                block.name = i + counter + " " + j;
                                Grid[i, j] = 0;
                                Grid[i + counter, j] = 1;

                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = line - 1; i >= top - 1; i--)
                {
                    for (int j = 1; j < Grid.GetLength(1) - 1; j++)
                    {
                        if (Grid[i, j] == 1)
                        {
                            var block = GameObject.Find(i + " " + j);
                            block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y + 1 * -0.5f, -2);
                            block.name = i + 1 + " " + j;
                            Grid[i, j] = 0;
                            Grid[i + 1, j] = 1;
                        }
                    }
                }
            }
        }
    }
}
