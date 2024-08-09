using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _patrolRoute;
    [SerializeField] private float _speed;

    private Transform[] _places;
    private int _placePointIndex = 0;
    private Transform _nextPlace;

    private void Start()
    {
        _places = new Transform[_patrolRoute.childCount];

        for (int i = 0; i < _patrolRoute.childCount; i++)
        {
            _places[i] = _patrolRoute.GetChild(i);
        }

        _nextPlace = _places[_placePointIndex];
    }

    private void Update()
    {
        if (transform.position == _nextPlace.position)
            NextPoint();

        transform.position = Vector3.MoveTowards(transform.position, _nextPlace.position, _speed * Time.deltaTime);
    }

    private void NextPoint()
    {
        _placePointIndex = ++_placePointIndex % _places.Length;
        _nextPlace = _places[_placePointIndex];
    }
}
