using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBombExplodable
{
    bool IsExplode { get; set; }
    void SetExplodeStatus(bool isExplode);

	Transform IsPlacedBy { get; set; }
    void SetPlacedBy(Transform player);

    float IsExplodeTimer { get; set; }
    void SetExplodeTimer(float explodeTimer);

    bool IsCounting { get; set; }
    void SetCounting(bool isCounting);
}