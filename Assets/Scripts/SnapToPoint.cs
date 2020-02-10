using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample
{
    public class SnapToPoint : MonoBehaviour, Resetable
    {
        private bool shouldReset;
        private GameObject centerpoint;
        private Vector3 snapTo;
        private Quaternion snapAngle;
        private Rigidbody body;
        public float snapTime = 2;

        private float dropTimer;
        private Interactable interactable;

        bool Resetable.hit => !transform.gameObject.activeSelf;

        private void RecordSnapPosition()
        {
            RecordSnapPosition(transform.position, transform.rotation);
        }

        private void RecordSnapPosition(Vector3 position, Quaternion rotation)
        {
            snapTo = position;
            snapAngle = rotation;
        }

        public void ToggleReset(bool status)
        {
            shouldReset = status;
            if(status)
            {
                transform.gameObject.SetActive(true);
            }
        }

        public void TriggerRearrange(float newX, float newZ)
        {
            Vector3 newPosition = new Vector3(newX, 0.3F, newZ);
            // transform.SetPositionAndRotation(newPosition, transform.rotation);
            RecordSnapPosition(newPosition, transform.rotation);
            ToggleReset(true);
        }

        private void Start()
        {
            RecordSnapPosition();
            centerpoint = GameObject.FindGameObjectsWithTag("Centerpoint")[0];
            shouldReset = false;
            interactable = GetComponent<Interactable>();
            body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            bool used = false;

            if (interactable != null)
                used = interactable.attachedToHand;

            if (used || transform.position == snapTo && transform.rotation == snapAngle)
            {
                body.velocity = Vector3.zero;
                body.angularVelocity = Vector3.zero;
                body.isKinematic = false;
                dropTimer = -1;
                ToggleReset(false);
            }
            else if (shouldReset)
            {
                dropTimer += Time.deltaTime / (snapTime / 2);

                body.isKinematic = dropTimer > 1;

                // Already back in target position
                if (dropTimer > 1)
                {
                    //transform.parent = snapTo;
                    transform.position = snapTo;
                    transform.rotation = snapAngle;
                    ToggleReset(false);
                    dropTimer = -1;
                }
                // Still returning to target position
                else
                {
                    float t = Mathf.Pow(35, dropTimer);

                    body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, Time.fixedDeltaTime * 4);
                    if (body.useGravity)
                        body.AddForce(-Physics.gravity);

                    transform.position = Vector3.Lerp(transform.position, snapTo, Time.fixedDeltaTime * t * 3);
                    transform.rotation = Quaternion.Slerp(transform.rotation, snapAngle, Time.fixedDeltaTime * t * 2);
                }
            }
            else
            {
                float separation = Vector3.Distance(centerpoint.transform.position, transform.position);
                if(separation > 6.1)
                {
                    transform.gameObject.SetActive(false);
                }
            }
        }
    }
}