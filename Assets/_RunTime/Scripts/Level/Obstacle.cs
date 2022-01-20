using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private DecorationSpawner[] _decorationSpawners;
    private List<ObstacleDecoration> _obstacleDecorations = new List<ObstacleDecoration>();
    public void SpawnDecorations()
    {
        foreach (var decorationSpawner in _decorationSpawners)
        {
            decorationSpawner.gameObject.SetActive(true);
            ObstacleDecoration obstacleDecoration = decorationSpawner.CurrentDecoration.GetComponent<ObstacleDecoration>();
            if (obstacleDecoration != null)
            {
                _obstacleDecorations.Add(obstacleDecoration);
            }
        }
    }
    public void PlayCollisionFeedBack(Collider _collider)
    {
        ObstacleDecoration decoration = _FindDecorationForCollider(_collider);
        if (decoration != null)
        {
            decoration.PlayCollisionFeedBack();
        }
    }

    private ObstacleDecoration _FindDecorationForCollider(Collider _collider)
    {
        float minDistX = Mathf.Infinity;
        ObstacleDecoration minDistDecoration = null;
        foreach (ObstacleDecoration decoration in _obstacleDecorations)
        {
            float decorationPosX = decoration.transform.position.x;
            float colliderPosX = _collider.bounds.center.x;

            float distX = Mathf.Abs(decorationPosX - colliderPosX);
            if (distX < minDistX)
            {
                minDistX = distX;
                minDistDecoration = decoration;
            }
        }
        return minDistDecoration;
    }
    private void OnDisable()
    {
        _InitializeOnDizable();
    }
    private void _InitializeOnDizable()
    {
        PoolingSystem.Instance.ReturnGameObject(gameObject);
    }
}
