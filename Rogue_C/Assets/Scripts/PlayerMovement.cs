using Unity.Netcode;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class PlayerMovement : NetworkBehaviour
{
    // Update is called once per frame
    public float moveSpeed = 3f;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            TestServerRpc();
        }

        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = +1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = -1f;

        Vector3 newPos = transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.position = newPos;

        SendNewPosServerRpc(newPos);
    }

    [ServerRpc]
    private void TestServerRpc()
    {
        Debug.Log("TestServerRpc " + OwnerClientId);
    }

    [ServerRpc]
    private void SendNewPosServerRpc(Vector3 pos)
    {
        //A client would call this
        //The client asks the server through this message to update it's position in 3D space
        transform.position = pos;

        UpdatePositionClientRpc(pos);

    }

    [ClientRpc]
    private void UpdatePositionClientRpc(Vector3 pos)
    {
        /*if (!IsOwner)
        {
            transform.position = pos;
        }*/
        if (!IsOwner)
        {
            StopAllCoroutines(); // Stop any ongoing smoothing coroutine
            StartCoroutine(SmoothMove(pos));
        }
    }
    private IEnumerator SmoothMove(Vector3 targetPosition)
    {
        float duration = 0.1f; // Duration of the smoothing
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            // Enable movement for the owner (the client)
            enabled = true;
        }
        else
        {
            // Disable movement for non-owners
            enabled = false;
        }
    }
}
