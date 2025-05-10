using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class DiceAreaController : MonoBehaviour, IInitializable
{
    [SerializeField] private Image _trayImage;
    [SerializeField] private Vector2 _size;
    [SerializeField] private Vector2 _cell;

    private IDiceController _diceManager;
    public void Initialize()
    {
        _diceManager = ServiceLocator.Resolve<IDiceController>();
        _diceManager.DiceRolled += OnDiceRolled;
    }

    public void Click()
    {
        int count = Random.Range(1, 5);
        List<int> list = new() { 4, 6, 8, 10, 12, 16, 20 };
        List<int> dice = new();
        for (int i = 0; i < count; i++)
        {
            dice.Add(list[Random.Range(0, list.Count)]);
        }

        _diceManager.RollDice(dice.ToArray());
    }
    private void OnDiceRolled(List<(int sides, int value)> list)
    {
        foreach (Transform child in transform)
        {
            if (child == _trayImage.transform)
                continue;
            Destroy(child.gameObject);
        }

        int cellsX = Mathf.FloorToInt(_size.x / _cell.x) / 2;
        int cellsY = Mathf.FloorToInt(_size.y / _cell.y) / 2;
        Vector2Int sizeInt = new Vector2Int(cellsX, cellsY);

        List<Vector2Int> availableCells = new List<Vector2Int>();

        for (int x = -sizeInt.x; x <= sizeInt.x; x++)
        {
            for (int y = -sizeInt.y; y <= sizeInt.y; y++)
            {
                availableCells.Add(new Vector2Int(x, y));
            }
        }

        for (int i = 0; i < availableCells.Count; i++)
        {
            int randomIndex = Random.Range(i, availableCells.Count);
            (availableCells[i], availableCells[randomIndex]) = (availableCells[randomIndex], availableCells[i]);
        }

        for (int i = 0; i < list.Count; i++)
        {
            DiceView dice = _diceManager.GetDice(list[i].sides, list[i].value);
            dice.transform.SetParent(transform);
            Vector2Int posInt = availableCells[i];
            dice.transform.localPosition = new Vector3(posInt.x * _cell.x, posInt.y * _cell.y, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 halfSize = _size * 0.5f;

        Vector2 topLeft = new Vector2(-halfSize.x, halfSize.y);
        Vector2 topRight = new Vector2(halfSize.x, halfSize.y);
        Vector2 bottomLeft = new Vector2(-halfSize.x, -halfSize.y);
        Vector2 bottomRight = new Vector2(halfSize.x, -halfSize.y);

        Vector3 worldTopLeft = transform.TransformPoint(topLeft);
        Vector3 worldTopRight = transform.TransformPoint(topRight);
        Vector3 worldBottomLeft = transform.TransformPoint(bottomLeft);
        Vector3 worldBottomRight = transform.TransformPoint(bottomRight);

        Debug.DrawLine(worldTopLeft, worldTopRight, Color.yellow);
        Debug.DrawLine(worldTopRight, worldBottomRight, Color.yellow);
        Debug.DrawLine(worldBottomRight, worldBottomLeft, Color.yellow);
        Debug.DrawLine(worldBottomLeft, worldTopLeft, Color.yellow);

        if (_cell.x > 0 && _cell.y > 0)
        {
            Gizmos.color = new Color(1, 1, 0, 0.3f); 
            for (float x = -halfSize.x + _cell.x; x < halfSize.x; x += _cell.x)
            {
                Vector3 start = transform.TransformPoint(new Vector2(x, -halfSize.y));
                Vector3 end = transform.TransformPoint(new Vector2(x, halfSize.y));
                Gizmos.DrawLine(start, end);
            }
            for (float y = -halfSize.y + _cell.y; y < halfSize.y; y += _cell.y)
            {
                Vector3 start = transform.TransformPoint(new Vector2(-halfSize.x, y));
                Vector3 end = transform.TransformPoint(new Vector2(halfSize.x, y));
                Gizmos.DrawLine(start, end);
            }
        }
    }
}
