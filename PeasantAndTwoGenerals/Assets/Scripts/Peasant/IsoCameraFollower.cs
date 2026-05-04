using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target; // Персонаж, за которым следует камера
    [SerializeField] private float zoomSpeed = 5f; // Скорость приближения/удаления камеры
    [SerializeField] private float defaultDistance = 10f; // Базовая дистанция камеры от персонажа
    [SerializeField] private float minDistance = 5f; // Минимальная дистанция камеры
    [SerializeField] private float maxDistance = 20f; // Максимальная дистанция камеры
    [SerializeField] private float lerpRate = 0.1f; // Скорость, с которой камера следует за персонажем

    private float currentDistance; // Текущая дистанция камеры
    private bool isUsed = false;

    void Start()
    {
        currentDistance = defaultDistance;
    }

    public void SetUsed(bool value)
    {
        isUsed = value;
    }

    void LateUpdate()
    {
        if (!isUsed) return;
        // Получить вращение колеса мыши
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");

        // Обновить дистанцию камеры
        currentDistance -= scrollAmount * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Вычислить позицию камеры под углом 45 градусов
        Vector3 targetPosition = target.position + new Vector3(0, currentDistance, currentDistance) * 0.707f; // sin(45)/cos(45)=sqrt(2)/2 ≈ 0.707

        // Следовать за персонажем с плавным переходом
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpRate);

        // Постоянно смотрим на персонажа
        transform.LookAt(target.position);
    }
}
