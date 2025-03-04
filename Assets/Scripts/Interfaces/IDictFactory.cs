
using UnityEngine;

public interface IDictFactory
{
    public IController GetObject(string key, Vector2 position);

    public void ReturnObject(IController controller);

}
