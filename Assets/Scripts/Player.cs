using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //Define delegate types and events here

    public Node CurrentNode { get; private set; }
    public Node TargetNode { get; private set; }

    [SerializeField] private float speed = 4;
    private bool moving = false;

    private Vector3 currentDir;

    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;

    [SerializeField] Node northNode;
    [SerializeField] Node southNode;
    [SerializeField] Node eastNode;
    [SerializeField] Node westNode;

    private NavButton currentButton;
    // Start is called before the first frame update
    void Start()
    {

        foreach (Node node in GameManager.Instance.Nodes)
        {
            if(node.Parents.Length > 2 && node.Children.Length == 0)
            {
                CurrentNode = node;
                TargetNode = node;
                break;
            }
        }
        CheckAvailableNodes();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == false)
        {
            //Implement inputs and event-callbacks here
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                FindNode(1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                FindNode(2);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                FindNode(4);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                FindNode(3);
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
                CheckAvailableNodes();
                CurrentNode = TargetNode;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            MouseInteraction();
        }
    }

    public void CheckAvailableNodes()
    {
        RaycastHit hit;
        Node _tempNode;


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {

            if (hit.collider.TryGetComponent<Node>(out _tempNode))
            {
                northNode = _tempNode;
            }
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(- Vector3.forward), out hit, Mathf.Infinity))
        {

            if (hit.collider.TryGetComponent<Node>(out _tempNode))
            {
                southNode = _tempNode;
            }
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity))
        {

            if (hit.collider.TryGetComponent<Node>(out _tempNode))
            {
                eastNode = _tempNode;
            }
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(- Vector3.right), out hit, Mathf.Infinity))
        {

            if (hit.collider.TryGetComponent<Node>(out _tempNode))
            {
                westNode = _tempNode;
            }
        }
    }

    //Implement mouse interaction method here
    //if object in UI which mouse is over is tagged 'button'
    //call the input (direction) method
    //invoke "change coulour" event
    public void MouseInteraction()
    {
        pointerEventData = new PointerEventData(eventSystem);

        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results) 
        {
            if(result.gameObject.TryGetComponent<NavButton>(out currentButton))
            {
                Debug.Log(currentButton.direction);
                FindNode(currentButton.direction);
            }
        }
    }

    public void FindNode(int _targetNode)
    {
        switch (_targetNode)
        {
            case 4:
                {
                    if (westNode != null) 
                    {
                        MoveToNode(westNode);
                    }
                    break;
                }
            case 3:
                {

                    if (eastNode != null)
                    {
                        MoveToNode(eastNode);
                    }
                    break;
                }
            case 2:
                {
                    if (southNode != null)
                    {
                        MoveToNode(southNode);
                    }
                    break;
                }
            case 1:
                {
                    if (northNode != null)
                    {
                        MoveToNode(northNode);
                    }
                    break;
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
