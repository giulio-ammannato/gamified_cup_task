using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphPlotter : MonoBehaviour {

    public static GraphPlotter graphPlotter;
    private float thetaScale = 1f;
    private float radCircle = 1f;

    private static int numberOfLines = 12;
    public float lineWidth = 0.05f;

    //the game objects where the linerenderer are
    GameObject[] myLines;
    //the array of game renderers
    LineRenderer[] lineRenderers;

    //the width of the axis
    private static float axisLength = 3.5f;
    private static float diagonalAxisLength = axisLength / Mathf.Sqrt(2);

    //the number of axis
   // private int axis = 12;

    private void Awake()
    {
        GameObject clone = gameObject;

        //inizializa this game object
        if(graphPlotter == null)
        {
            graphPlotter = GameObject.FindGameObjectWithTag("GraphPlotter").GetComponent<GraphPlotter>();
        }

        //LineRenderer lr = transform.GetComponent<LineRenderer>().material;

        //inizialize the arrays
        myLines = new GameObject[numberOfLines];
        lineRenderers = new LineRenderer[numberOfLines];

        Debug.Log(numberOfLines);

        for (int x = 0; x<numberOfLines;x++ )
        {
            //create the game objects
            myLines[x] = new GameObject("linea" + (x + 1));
            //attach the linerenderer to each game object
            myLines[x].AddComponent<LineRenderer>();

            //initializa the linerenderers 
            lineRenderers[x] = myLines[x].GetComponent<LineRenderer>();
            //set the width of the line
            lineRenderers[x].startWidth = lineWidth;
            lineRenderers[x].endWidth = lineWidth;
            //set the material of the line
            lineRenderers[x].material = transform.GetComponent<LineRenderer>().material;

            lineRenderers[x].startColor = Color.white;
            lineRenderers[x].endColor = Color.white;





        }
       
    }

    // Use this for initialization
    void Start () {

        // Draw the circle using that LineRenderer
        // DrawCircle(lr);
        DrawAxis(lineRenderers);
        DrawLines(lineRenderers);
        // Set the position
        // lr.transform.position = lr.transform.parent.transform.position;

    }

    private void DrawAxis(LineRenderer[] _lineRenderers)
    {
        //north-south axis
        Vector3 axisStartPos = new Vector3(0, -axisLength, 1);
        Vector3 axisEndPos = new Vector3(0, axisLength, 1);

        _lineRenderers[0].SetPosition(0, axisStartPos);
        _lineRenderers[0].SetPosition(1, axisEndPos);

        //east-west axis
        axisStartPos = new Vector3(-axisLength, 0, 1);
        axisEndPos = new Vector3(axisLength, 0, 1);

        _lineRenderers[1].SetPosition(0, axisStartPos);
        _lineRenderers[1].SetPosition(1, axisEndPos);


        //north-west south-west axis
        axisStartPos = new Vector3(-diagonalAxisLength, -diagonalAxisLength, 1);
        axisEndPos = new Vector3(diagonalAxisLength, diagonalAxisLength, 1);

        _lineRenderers[2].SetPosition(0, axisStartPos);
        _lineRenderers[2].SetPosition(1, axisEndPos);

        //east-west axis
        axisStartPos = new Vector3(diagonalAxisLength, -diagonalAxisLength, 1);
        axisEndPos = new Vector3(-diagonalAxisLength, diagonalAxisLength, 1);

        _lineRenderers[3].SetPosition(0, axisStartPos);
        _lineRenderers[3].SetPosition(1, axisEndPos);
    }

    private void DrawLines(LineRenderer[] _lineRenderers)
    {
        int max = 10;

        int[] values = new int[8];
        for(int x =0;x < values.Length;x++ )
        {
            int r = UnityEngine.Random.Range(1,11);
            values[x] = r;

        }

        Debug.Log(values[0]);
        Debug.Log(values[1]);


        //north
        float percent = (float)values[0] / (float)max * 100f;
        Vector3 axisStartPos = new Vector3(0, (axisLength / 100) * percent, 1);
        //north-east
        percent = (float)values[1] / (float)max * 100f;
        Vector3 axisEndPos = new Vector3((diagonalAxisLength / 100) * percent, (diagonalAxisLength / 100) * percent, 1);
        _lineRenderers[4].SetPosition(0, axisStartPos);
        _lineRenderers[4].SetPosition(1, axisEndPos);

        //north-ease
        percent = (float)values[1] / (float)max * 100f;
        axisStartPos = new Vector3((diagonalAxisLength / 100) * percent, (diagonalAxisLength / 100) * percent, 1);
        //east
        percent = (float)values[2] / (float)max * 100f;
        axisEndPos = new Vector3((axisLength / 100) * percent, 0, 1);
        _lineRenderers[5].SetPosition(0, axisStartPos);
        _lineRenderers[5].SetPosition(1, axisEndPos);

        //east
        percent = (float)values[2] / (float)max * 100f;
        axisStartPos = new Vector3((axisLength / 100) * percent, 0, 1);
        //south-east
        percent = (float)values[3] / (float)max * 100f;
        axisEndPos = new Vector3((diagonalAxisLength / 100) * percent, -(diagonalAxisLength / 100) * percent, 1);
        _lineRenderers[6].SetPosition(0, axisStartPos);
        _lineRenderers[6].SetPosition(1, axisEndPos);

        ////south-east
        percent = (float)values[3] / (float)max * 100f;
        axisStartPos = new Vector3((diagonalAxisLength / 100) * percent, -(diagonalAxisLength / 100) * percent, 1);
        //south
        percent = (float)values[4] / (float)max * 100f;
        axisEndPos = new Vector3(0, -(axisLength / 100) * percent, 1);
        _lineRenderers[7].SetPosition(0, axisStartPos);
        _lineRenderers[7].SetPosition(1, axisEndPos);

        ////south
        percent = (float)values[4] / (float)max * 100f;
        axisStartPos = new Vector3(0, -(axisLength / 100) * percent, 1); 
        //south-west
        percent = (float)values[5] / (float)max * 100f;
        axisEndPos = new Vector3(-(diagonalAxisLength / 100) * percent, -(diagonalAxisLength / 100) * percent, 1);
        _lineRenderers[8].SetPosition(0, axisStartPos);
        _lineRenderers[8].SetPosition(1, axisEndPos);

        //south-west
        percent = (float)values[5] / (float)max * 100f;
        axisStartPos = new Vector3(-(diagonalAxisLength / 100) * percent, -(diagonalAxisLength / 100) * percent, 1);
        //west
        percent = (float)values[6] / (float)max * 100f;
        axisEndPos = new Vector3(-(axisLength / 100) * percent, 0, 1);
        _lineRenderers[9].SetPosition(0, axisStartPos);
        _lineRenderers[9].SetPosition(1, axisEndPos);

        //west
        percent = (float)values[6] / (float)max * 100f;
        axisStartPos = new Vector3(-(axisLength / 100) * percent, 0, 1);
        //north-west
        percent = (float)values[7] / (float)max * 100f;
        axisEndPos = new Vector3(-(diagonalAxisLength / 100) * percent, (diagonalAxisLength / 100) * percent, 1);
        _lineRenderers[10].SetPosition(0, axisStartPos);
        _lineRenderers[10].SetPosition(1, axisEndPos);

        //north-west
        percent = (float)values[7] / (float)max * 100f;
        axisStartPos = new Vector3(-(diagonalAxisLength / 100) * percent, (diagonalAxisLength / 100) * percent, 1);
        //north
        percent = (float)values[0] / (float)max * 100f;
        axisEndPos = new Vector3(0, (axisLength / 100) * percent, 1);
        _lineRenderers[11].SetPosition(0, axisStartPos);
        _lineRenderers[11].SetPosition(1, axisEndPos);

        Debug.Log(axisStartPos);
        Debug.Log(axisEndPos);



    }

    private void DrawCircle(LineRenderer lr)
    {
        // Calculate each point (theta) in the circle
        // And set its position in the LineRenderer
        int i = 0;
        for (float theta = 0f; theta < (2 * Mathf.PI); theta += thetaScale)
        {
            // Calculate position of point
            float x = (radCircle * 100) * Mathf.Cos(theta);
            float y = (radCircle * 100) * Mathf.Sin(theta);

            // Set the position of this point
            Vector3 pos = new Vector3(x, y, 1);
            lr.SetPosition(i, pos);
            i++;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
