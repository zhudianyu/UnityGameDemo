using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Grid : MonoBehaviour {

    private Node[,] grid;
    public Vector2 gridSize;
    public float nodeRadius;
    private float nodeDiameter;

    public LayerMask WhatLayer;

    public int gridCntX, gridCntY;

    public Transform player;

    public List<Node> nodePath = new List<Node>();
	// Use this for initialization
	void Start () {
        nodeDiameter = nodeRadius * 2;
        gridCntX =Mathf.RoundToInt( gridSize.x / nodeDiameter);
        gridCntY = Mathf.RoundToInt(gridSize.y / nodeDiameter);

        grid = new Node[gridCntX, gridCntY];
        CreatGrid();
	}

    private void CreatGrid()
    {
        Vector3 startPoint = transform.position - gridSize.x / 2 * Vector3.right-Vector3.forward*gridSize.y/2;
        for(int i = 0;i<gridCntX;i++)
        {
            for(int j = 0;j<gridCntY;j++)
            {
                Vector3 wordPoint = startPoint + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward *
                    (j * nodeDiameter + nodeRadius);
                bool wakable = !Physics.CheckSphere(wordPoint, nodeRadius, WhatLayer);
                grid[i, j] = new Node(wakable, wordPoint, i, j);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));

        if(grid == null)
        {
            return;
        }
        Node playerNode = GetFromPosition(player.position);
        foreach(var node in grid)
        {
            Gizmos.color = node._canWalk ? Color.white : Color.red;
            Gizmos.DrawCube(node._worldPos, Vector3.one * (nodeDiameter - .1f));
        }
        if(nodePath != null)
        {
            foreach(var node in nodePath)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(node._worldPos, Vector3.one * (nodeDiameter - .1f));
            }
        }
        if(playerNode != null && playerNode._canWalk)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(playerNode._worldPos, Vector3.one * (nodeDiameter - .1f));
        }
    }

    public Node GetFromPosition(Vector3 position)
    {
        float precentX = (position.x + gridSize.x / 2) / gridSize.x;
        float precentY = (position.z + gridSize.y / 2) / gridSize.y;
        precentY = Mathf.Clamp01(precentY);
        precentX = Mathf.Clamp01(precentX);

        int x = Mathf.RoundToInt((gridCntX - 1) * precentX);
        int y = Mathf.RoundToInt((gridCntY - 1) * precentY);

        return grid[x, y];


    }

    public List<Node> GetNeibourhood(Node node)
    {
        List<Node> neibourhood = new List<Node>();
        for(int i = -1;i<=1;i++)
        {
            for(int j = -1;j<= 1;j++)
            {
                if(i==0&&j==0)
                {
                    continue;
                }
                int tempX = node._gridX + i;
                int tempY = node._gridY + j;
                if(tempX<gridCntX&&tempX>0&&tempY>0&&tempY<gridCntY)
                {
                    neibourhood.Add(grid[tempX, tempY]);
                }
            }
        }
        return neibourhood;
    }
}
