using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindPath : MonoBehaviour {

	// Use this for initialization
    private Grid _grid;
    public Transform walkpalyer, endPoint;
	void Start () {
        _grid = GetComponent<Grid>();
	}
	void FindingPath(Vector3 StartPos,Vector3 endPos)
    {
        Node startNode = _grid.GetFromPosition(StartPos);
        Node endNode = _grid.GetFromPosition(endPos);
        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);

        while(openSet.Count>0)
        {
            Node curNode = openSet[0];
            for (int i = 0; i < openSet.Count;i++ )
            {
                if(openSet[i].fCost<curNode.fCost||openSet[i].fCost == curNode.fCost
                    &&openSet[i].hCost<curNode.hCost)
                {
                    curNode = openSet[i];
                }
            }
             openSet.Remove(curNode);

            closeSet.Add(curNode);
            if(curNode == endNode)
            {
                GeneratePath(startNode, endNode);
                return;
            }

            foreach(var node in _grid.GetNeibourhood(curNode))
            {
                if (!node._canWalk || closeSet.Contains(node))
                    continue;
                int newCost = curNode.gCost + GetDistanceNodes(curNode, node);
                if(newCost<node.gCost||!openSet.Contains(node))
                {
                    node.gCost = newCost;
                    node.hCost = GetDistanceNodes(node, endNode);
                    node.parent = curNode;
                    if(!openSet.Contains(node))
                    {
                        openSet.Add(node);
                    }
                }
            }
        }
    }
    private void GeneratePath(Node StartNode,Node EndNode)
    {
        List<Node> path = new List<Node>();
        Node temp = EndNode;
        while(temp != StartNode)
        {
            path.Add(temp);
            temp = temp.parent;

        }
        path.Reverse();
        _grid.nodePath = path;

    }
    int GetDistanceNodes(Node a,Node b)
    {
        int cntX = Mathf.Abs(a._gridX - b._gridX);
        int cntY = Mathf.Abs(a._gridY - b._gridY);

        if(cntX>cntY)
        {
            return 14 * cntY + 10 * (cntX - cntY);
        }
        else
        {
            return 14 * cntX + 10 * (cntY - cntX);
        }

    }
	// Update is called once per frame
	void Update () {
        FindingPath(walkpalyer.position, endPoint.position);
	}
}
