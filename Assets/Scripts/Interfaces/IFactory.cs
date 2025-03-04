
using UnityEngine;

public interface IFactory
{
    public IController GetObject(Vector2 position);
}
