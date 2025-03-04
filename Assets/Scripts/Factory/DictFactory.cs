
using UnityEngine;

public interface IDictFactory
{
    public IController GetProduct(string key, Vector2 position);
}
