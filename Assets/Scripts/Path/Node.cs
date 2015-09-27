using UnityEngine;
using System.Collections;

public class Node 
{

    public bool _canWalk;
    public Vector3 _worldPos;
    public int _gridX, _gridY;

    public int gCost;
    public int hCost;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public Node parent;

    public Node(bool canWalk,Vector3 pos,int x,int y)
    {
        _canWalk = canWalk;
        _worldPos = pos;
        _gridX = x;
        _gridY = y;
    }
}
