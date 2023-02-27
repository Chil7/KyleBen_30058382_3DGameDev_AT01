using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //list variable for stack
    private List<Node> nodeStack;

    //list v for path
    private List<Node> nodePath;

    //list of all nodes in the graph
    private List<Node> nodeGraph;

    //var for starting, current, destination node

    [Tooltip("Movement speed modifier.")]
    [SerializeField] private float speed = 3;
    private Node currentNode;

    private Node startNode;
    private Node targetNode;

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
    public List<Node> FindPath(Node startNode, Node targetNode)
    {
        int addLocation = 0;
        //set current node as start node
        currentNode = startNode;

        //Add current to the Stack
        nodeStack.Insert(addLocation, currentNode);
        //nodeStack.Insert(0, Node );

        //set local variable found to false
        bool isFound = false;

        //Initiate while Loop to continue so long as "found" is false
        //Check if currentNode is the target node
        //if it isn't, continue the loop
        //otherwise set 'found' to true and break the loop
        while (isFound == false)
        {
            if (currentNode == targetNode)
            {
                isFound = true;
                break;
            }
            else
                return null;
        }


        //for each neighbour of current node
        //check if its on the stack
        //Check if its already searched
        //if neither is true, add neighour to stack and set currentNode as parent

        foreach (Node neighbours in currentNode.Children) 
        {
            if (nodeStack.Contains(neighbours) == true) 
            {
                currentNode = neighbours;
            }
        }
        


        //set current node to 'searched'
        //remove currentNode from stack

        //check if the stack is empty 
        //yes: break loop and return null with erroe message
        //no: set last node in stack as currentNode and return to start of loop

        //Initiate while Loop to continue so long as 'found' is true
        //Add 'currentNode' to path
        //check if curretnnode has parent
        //if does, set parent as cuurent Node and continue loop
        //otherwise retrun path value

        return null;
    }
}
