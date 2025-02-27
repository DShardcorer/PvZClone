using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Sprite image;

    public void HandleMove(Vector2 target)
    {
        transform.position = target;
    }

    public void HandleShowEffectShoot(){
        
    }
}
