using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int currentBullets;
    public int capacity;

    public int Reload(int _totalBullets)
    {
        if (_totalBullets > 0 && _totalBullets >= capacity)
        {
            int bullet = capacity - currentBullets;

            currentBullets += bullet;

            _totalBullets -= bullet;
        }
        else if (_totalBullets > 0)
        {
            currentBullets = _totalBullets + currentBullets;

            _totalBullets = 0;
        }

        return _totalBullets;
    }
}
