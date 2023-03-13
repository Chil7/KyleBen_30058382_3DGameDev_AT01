using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //list variable for stack
    private List<Node> unsearchedNode = new List<Node>();

    //var for starting, current, destination node
    [Tooltip("Movement speed modifier.")]
    [SerializeField] private float speed = 3;
    private Node currentNode;

    private Vector3 currentDir;
    private bool playerCaught = false;

    public delegate void GameEndDelegate();
    public event GameEndDelegate GameOverEvent  = delegate { };


    // Start is called before the first frame update
    void Start()
    {

        InitializeAgent();

        //find all available nodes and add thme to the node list
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCaught == false)
        {
            if (currentNode != null)
            {
                //If within 0.25 units of the current node.
                if (Vector3.Distance(transform.position, currentNode.transform.position) > 0.25f)
                {
                    transform.Translate(currentDir * speed * Time.deltaTime);
                }
                //Implement path finding here
                else
                {
                    DepthFirstSearch();
                }
            }
            else
            {
                Debug.LogWarning($"{name} - No current node");
            }

            Debug.DrawRay(transform.position, currentDir, Color.cyan);
        }
    }

    //Called when a collider enters this object's trigger collider.
    //Player or enemy must have rigidbody for this to function correctly.
    private void OnTriggerEnter(Collider other)
    {
        if (playerCaught == false)
        {
            if (other.tag == "Player")
            {
                playerCaught = true;
                GameOverEvent.Invoke(); //invoke the game over event
            }
        }
    }

    /// <summary>
    /// Sets the current node to the first in the Game Managers node list.
    /// Sets the current movement direction to the direction of the current node.
    /// </summary>
    void InitializeAgent()
    {
        currentNode = GameManager.Instance.Nodes[0];
        currentDir = currentNode.transform.position - transform.position;
        currentDir = currentDir.normalized;
    }

    //Implement DFS algorithm method here

        //List of nodes were searching (stack
        //Variable of variable currentlys earched (use for or while loop)
        //Boolean if target found
        


        //access the nodes on gamemanager
        //add gamemanager.instance.nodes[0] to a list of unseached nodes 9root node0
        //check if root node is the same as Gamemanager.instance.player.targernode/currentnode
        //if its the same : return that as the new destination for this enemy
        //add the children of the node being searched to the list of unsearcherd nodes
        //remove the node being searched from lsit of unsearched nodes
        //assign the node at the top (last position of the unsearched list) as the node being searched
        //go back to line 164

    //}

    private void DepthFirstSearch()
    {
        bool _isFound = false;
        var _currentNode = GameManager.Instance.Nodes[0];
        var _playerTargetNode = GameManager.Instance.Player.TargetNode;

        unsearchedNode.Clear();

        unsearchedNode.Add(_currentNode);

        while (_isFound == false && unsearchedNode.Count > -1)
        {
            if (_currentNode == _playerTargetNode)
            {
                _isFound = true;
                currentNode = _currentNode;
                break;
            }
            else
            {
                foreach (Node children in _currentNode.Children)
                {
                    unsearchedNode.Add(children);
                }
                unsearchedNode.Remove(_currentNode);
                _currentNode = unsearchedNode[unsearchedNode.Count - 1];
            }
        }
    }
}
