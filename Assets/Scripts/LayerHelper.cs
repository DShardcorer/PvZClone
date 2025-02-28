using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerHelper
{
    public static readonly LayerMask Plants = LayerMask.GetMask("Plants");
    public static readonly LayerMask Enemies = LayerMask.GetMask("Enemies");
    public static readonly LayerMask Projectiles = LayerMask.GetMask("Projectiles");
    public static readonly LayerMask Suns = LayerMask.GetMask("Suns");
    public static readonly LayerMask GameOver = LayerMask.GetMask("GameOver");

}
