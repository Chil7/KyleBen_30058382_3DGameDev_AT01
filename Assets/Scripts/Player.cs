using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Define delegate types and events here

    public Node CurrentNode { get; private set; }
    public Node TargetNode { get; private set; }

    [SerializeField] private float speed = 4;
    private bool moving = false;

    private Vector3 currentDir;

    // Start is called before the first frame update
    void Start()
    {

        foreach (Node node in GameManager.Instance.Nodes)
        {
            if(node.Parents.Length > 2 && node.Children.Length == 0)
            {
                CurrentNode = node;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == false)
        {
            //Implement inputs and event-callbacks here
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                
                Debug.Log("Up");
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                Debug.Log("Down");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                Debug.Log("Left");
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
 
                Debug.Log("Right");
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, TargetNode.transform.position) > 0.25f)
            {
                transform.Translate(currentDir * speed * Time.deltaTime);
            }
            else
            {
                moving = false;
                CurrentNode = TargetNode;
            }
        }
    }

    //Implement mouse interaction method here
    //if object in UI which mouse is over is tagged 'button'
    //call the input (direction) method
    //invoke "change coulour" event
    public void MouseInteraction(int index)
    {
        Vector3 direction = new Vector3();

        RaycastHit hit;


        switch (index)
        {
            case 4:
                {
                    direction = - Vector3.right;
                    break;
                }
            case 3:
                {
                    direction = Vector3.right;
                    break;
                }
            case 2:
                {
                    direction = - Vector3.forward;
                    break;
                }
            case 1:
                {
                    direction = Vector3.forward;
                    break;
                }


        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, Mathf.Infinity))
        {
            Node _tempNode;


            if (hit.collider.TryGetComponent<Node>(out _tempNode))
            {
                MoveToNode(_tempNode);
            }
        }

    }

    /// <summary>
    /// Sets the players target node and current directon to the specified node.
    /// </summary>
    /// <param name="node"></param>
    public void MoveToNode(Node node)
    {
        if (moving == false)
        {
            TargetNode = node;
            currentDir = TargetNode.transform.position - transform.position;
            currentDir = currentDir.normalized;
            moving = true;
        }
    }
}
