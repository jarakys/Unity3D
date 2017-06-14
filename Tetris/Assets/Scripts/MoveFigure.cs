using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveFigure : MonoBehaviour
{
    public GameObject[] Figure;
    private GameObject currentFigure;
    private int[] Angle = { 0, 90, 180, 270 };
    private static float Timer = 0.15f;
    public float speed;
    Controller control;
    float angle;
    // Use this for initialization
    void Start()
    {
        control = new Controller();
        SpawnFigure();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Time.fixedTime > Timer)
        {
            currentFigure.transform.position = new Vector3(currentFigure.transform.position.x, currentFigure.transform.position.y - 0.5f, -2);
            if (!control.CheckPosition(currentFigure.transform))
            {
                currentFigure.transform.position = new Vector3(currentFigure.transform.position.x, currentFigure.transform.position.y + 0.5f, -2);
                control.SetFigure(currentFigure);
                Destroy(currentFigure);
                SpawnFigure();
            }
            Timer += speed;
        }
    }

    void SpawnFigure()
    {
        int numbFigure = Random.Range(0, Figure.Length);
        int angleFigure = Random.Range(0, Angle.Length - 1);
        if (numbFigure == 2)
        {
            angleFigure = 0;
        }
        currentFigure = (Instantiate(Figure[numbFigure], new Vector3(0f, 4f, -2f), Quaternion.Euler(0, 0, Angle[angleFigure]))) as GameObject;
        if (!control.CheckPosition(currentFigure.transform))
        {
            SceneManager.LoadScene(0);
        }

    }
    public void Left()
    {
        currentFigure.transform.position = new Vector3(currentFigure.transform.position.x - 0.5f, currentFigure.transform.position.y, -2);
        if (!control.CheckPosition(currentFigure.transform))
        {
            currentFigure.transform.position = new Vector3(currentFigure.transform.position.x + 0.5f, currentFigure.transform.position.y, -2);
        }

    }
    public void Right()
    {
        currentFigure.transform.position = new Vector3(currentFigure.transform.position.x + 0.5f, currentFigure.transform.position.y, -2);
        if (!control.CheckPosition(currentFigure.transform))
        {
            currentFigure.transform.position = new Vector3(currentFigure.transform.position.x - 0.5f, currentFigure.transform.position.y, -2);
        }
    }
    public void Rotate()
    {
        if (currentFigure.name != "Cube(Clone)")
        {
            float angle = currentFigure.transform.eulerAngles.z + 90;
            currentFigure.transform.eulerAngles = new Vector3(0, 0, angle);
            if (!control.CheckPosition(currentFigure.transform))
            {
                angle -= 90;
                currentFigure.transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }
    public void Down()
    {
        while (control.CheckPosition(currentFigure.transform))
        {
            currentFigure.transform.position = new Vector3(currentFigure.transform.position.x, currentFigure.transform.position.y - 0.5f, -2);
        }
        currentFigure.transform.position = new Vector3(currentFigure.transform.position.x, currentFigure.transform.position.y + 0.5f, -2);
        control.SetFigure(currentFigure);
        Destroy(currentFigure);
        SpawnFigure();
    }
}
