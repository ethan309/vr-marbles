using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Resetable
{
    GameObject gameObject { get; }

    bool hit { get; }

    void ToggleReset(bool status);

    void TriggerRearrange(float newX, float newZ);
}
