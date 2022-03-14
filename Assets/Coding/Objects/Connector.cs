using System;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public enum Direction
    {
        Y,
        X,
        Z
    }

    public Guid Guid;
    public Connector connectedConnector;
    public Vector3 raycastSize = Vector3.one;
    public bool isStatic = false;
    public bool autoConnect;
    public bool connected = false;
    public Transform originalParent;
    public Func<Connector, bool> connectCondition = (Connector connector) => { return true; };
    public Rigidbody rb;
    public Rigidbody originalRb;
    public GameObject originalRbParent;
    public CarryableObject carryable;
    public Transform movementTransform;
    public ConnectorOffset offset;
    public Direction direction;
    public Vector3 connectionOffset;
    public List<int> hitLayers;
    public List<Connector> ignoredConnectors;
    public List<Connector> originalIgnoredConnectors;
    public AudioSource connectSound;
    public ConfigurableJoint Joint;
    public bool disconnectable = true;
    public bool cascadeRigidbody;

    bool massSaved;
    float mass;
    float angularDrag;
    private int layerMask;

    bool initialized;

    void Start()
    {
        Guid = Guid.NewGuid();

        // If there's no transform marked for re-parenting, use the current object's transform
        if (movementTransform == null) movementTransform = transform;

        // Store the original parent of the transform to be re-parented
        originalParent = movementTransform.parent;

        // Look for a rigidbody on the reparenting object and its parent
        if (rb == null) rb = movementTransform.GetComponent<Rigidbody>();
        if (rb == null && movementTransform.parent != null) rb = movementTransform.parent.GetComponent<Rigidbody>();

        // Store the original rigidbody's parent
        originalRbParent = rb.gameObject;
        originalRb = rb;

        // Try to get a CarryableObject component
        if (carryable == null) carryable = GetComponent<CarryableObject>();

        // Set up raycast layermask
        foreach (int i in hitLayers)
        {
            int x = 1 << i;
            layerMask = layerMask | x;
        }

        // Store ignored connectors set from the editor/prefab
        originalIgnoredConnectors = new List<Connector>(ignoredConnectors);

        // Mark Initialized
        initialized = true;
    }

    void Update()
    {
        // Handle connectors that should be connected automatically
        if (autoConnect)
        {
            // Wait for target connector to be initialized
            if (!connectedConnector.initialized)
            {
                return;
            }

            // Connect to target connector
            Connect(connectedConnector);

            // Mark auto-connected
            autoConnect = false;
        }
    }

    public void CheckForConnection()
    {
        if (connected || isStatic) return;

        // Do a raycast sphere to check if we're overlapping something
        Collider[] hits = Physics.OverlapBox(transform.position, raycastSize / 2, transform.rotation, layerMask);
        if (hits.Length > 0)
        {
            // We are, so itterate over all of the colliders
            foreach (Collider collider in hits)
            {
                // Check if it's a vaild, non-connected socket
                Connector socket = collider.transform.gameObject.GetComponent<Connector>();
                if (socket != null && !socket.connected && socket != this)
                {
                    // Check if we should be ignoring this socket
                    if (ignoredConnectors.Contains(socket)) return;
                    // This probably isn't useful now
                    if (socket.originalParent == originalParent) return;
                    // Check if connection condition is met
                    if (!connectCondition(socket)) return;
                    // Connect the socket
                    Connect(socket);
                    // Only connect one socket
                    return;
                }
            }
        }
    }

    public virtual void Connect(Connector socket)
    {
        // Disconnect from carry system
        if (carryable.trigger.connected) carryable.trigger.Disconnect(!disconnectable);

        // Tell each other who we're connected to
        socket.connectedConnector = this;
        connectedConnector = socket;

        // Set relative positioning
        Vector3 pos = connectedConnector.movementTransform.position;
        if (offset == null)
        {
            // Offset is null - we're using scale of the object here
            //pos += (connectedConnector.transform.up * (connectedConnector.transform.lossyScale.y / 2));
            pos += (connectedConnector.transform.up * (transform.lossyScale.y / 2));
        }
        else
        {
            // Offset is not null - a possibly-non-uniform mesh with multiple connectors
            pos += (GetDirectionOffset() / 2) * connectedConnector.transform.up;
            //pos += (connectedConnector.GetDirectionOffset() / 2) * connectedConnector.transform.up;
        }

        if (connectedConnector.offset == null)
        {
            // Offset is null - we're using scale of the object here
            pos += (connectedConnector.transform.up * (connectedConnector.transform.lossyScale.y / 2));
        }
        else
        {
            // Offset is not null - a possibly-non-uniform mesh with multiple connectors
            pos += (connectedConnector.GetDirectionOffset() / 2) * connectedConnector.transform.up;
        }

        pos += connectedConnector.transform.up * connectedConnector.connectionOffset.y;
        pos += connectedConnector.transform.right * connectedConnector.connectionOffset.x;
        pos += connectedConnector.transform.forward * connectedConnector.connectionOffset.z;

        pos += transform.up * connectionOffset.y;
        pos += transform.right * connectionOffset.x;
        pos += transform.forward * connectionOffset.z;

        // Set the position
        movementTransform.position = pos;

        // Prevent Gimbal Locking
        if (transform.forward == connectedConnector.transform.forward)
            movementTransform.Rotate(connectedConnector.transform.right, 10);

        // Match Forward Axis
        Quaternion rotZ = Quaternion.FromToRotation(-transform.forward, connectedConnector.transform.forward);
        movementTransform.transform.rotation = rotZ * movementTransform.transform.rotation;

        // Prevent Gimbal Locking
        if (transform.right == -connectedConnector.transform.right)
            movementTransform.Rotate(connectedConnector.transform.forward, 10);

        // Match Right Axis
        Quaternion rotX = Quaternion.FromToRotation(transform.right, connectedConnector.transform.right);
        movementTransform.transform.rotation = rotX * movementTransform.transform.rotation;

        // We connected to another object, so we don't need to be animated
        RemoveRigidbody();
        ChangeRigidbodies(connectedConnector.rb);

        // Rebuild the collider so it doesn't be stupid
        RecreateCollider();

        // Ignore every neightbor connector so weird things don't happen
        connectedConnector.ignoredConnectors.AddRange(originalIgnoredConnectors);
        ignoredConnectors.AddRange(connectedConnector.originalIgnoredConnectors);

        // Change parent to actually connect the two objects
        movementTransform.parent = connectedConnector.movementTransform;

        // Mark as connected
        connectedConnector.connected = true;
        connected = true;

        // Play connection sound
        if (connectSound != null) connectSound.Play();
    }

    public void ChangeRigidbodies(Rigidbody rbdy)
    {
        List<Guid> changedConnectors = new List<Guid>();
        ChangeRigidbodies(rbdy, ref changedConnectors);
        //connectedConnector.ChangeRigidbodies(rbdy, ref changedConnectors);
    }

    public void ChangeRigidbodies(Rigidbody rbdy, ref List<Guid> changedConnectors)
    {
        if (changedConnectors.Contains(Guid)) return;
        changedConnectors.Add(Guid);

        try
        {
            rb = rbdy;
            carryable.rb = rb;
            carryable.trigger.rb = rb;
            if (Joint != null) Joint.connectedBody = rb;
        }
        catch
        {

        }

        if (cascadeRigidbody)
        {
            foreach (Connector connector in originalIgnoredConnectors)
            {
                connector.rb = rbdy;
                if (connector.connected)
                {
                    connector.connectedConnector.ChangeRigidbodies(rbdy, ref changedConnectors);
                }
            }
        }
    }

    void RemoveRigidbody()
    {
        // What are we doing here?
        if (massSaved) return;

        mass = rb.mass;
        angularDrag = rb.angularDrag;

        if (Joint != null) Joint.connectedBody = null;

        DestroyImmediate(rb);

        massSaved = true;
    }

    void AddRigidbody()
    {
        // We haven't saved the rigidbody
        if (!massSaved) return;

        originalRb = originalRbParent.AddComponent<Rigidbody>();
        originalRb.mass = mass;
        originalRb.angularDrag = angularDrag;

        massSaved = false;
    }

    void RecreateCollider()
    {
        DestroyImmediate(GetComponent<BoxCollider>());
        gameObject.AddComponent<BoxCollider>();
    }

    void NonstaticDisconnect()
    {
        // Return non-static connectors to pre-connected state
        if (!isStatic)
        {
            if (cascadeRigidbody)
            {
                // EhAh
                foreach (Connector connector in originalIgnoredConnectors)
                {
                    if (connector == this) continue;
                    connector.Disconnect();
                }
            }

            movementTransform.parent = originalParent;

            AddRigidbody();
            ChangeRigidbodies(originalRb);
            RecreateCollider();

            rb.ResetCenterOfMass();
        }
    }

    public virtual void Disconnect()
    {
        // Shortcut
        if (!connected) return;

        // Mark as disconnected
        connected = false;
        connectedConnector.connected = false;

        NonstaticDisconnect();
        connectedConnector.NonstaticDisconnect();

        // Remove connected object's ignored connectors so we can connect to this object again
        foreach (Connector connector in originalIgnoredConnectors)
        {
            connectedConnector.ignoredConnectors.Remove(connector);
        }
        // Remove ignored connectors
        foreach (Connector connector in connectedConnector.originalIgnoredConnectors)
        {
            ignoredConnectors.Remove(connector);
        }

        // Mark as null
        connectedConnector.connectedConnector = null;
        // We're completely disconnected now
        connectedConnector = null;

        // Play disconnect sound
        //if (connectSound != null) connectSound.Play();
    }

    public float GetDirectionOffset()
    {
        if (direction == Direction.Y)
        {
            return offset.Offset.y;
        }
        else if (direction == Direction.X)
        {
            return offset.Offset.x;
        }
        return offset.Offset.z;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform's position with size of the connection raycast
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, raycastSize);

        //rotationMatrix = Matrix4x4.TRS(rb.position + rb.centerOfMass, transform.rotation, Vector3.one);
        //Gizmos.matrix = rotationMatrix;
        //Gizmos.color = Color.blue;
        //Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
}