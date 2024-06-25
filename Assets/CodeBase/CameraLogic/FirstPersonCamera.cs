using UnityEngine;

namespace CodeBase.CameraLogic
{
  public class FirstPersonCamera : MonoBehaviour
  {
    // Чувствительность мыши
    public float mouseSensitivity = 100f;

    // Скорость движения
    public float speed = 10f;

    // Горизонтальный и вертикальный углы поворота камеры
    private float xRotation = 0f;
    private float yRotation = 0f;

    // Ссылка на Transform игрока (Player)
    private Transform playerTransform;

    private void Start()
    {
      // Закрепить курсор в центре экрана
      Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
      // Если игрок найден, то обновляем камеру
      if (playerTransform != null)
      {
        // Получить входные данные от мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Изменить вертикальный угол поворота
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Применить поворот к камере
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Изменить горизонтальный угол поворота
        yRotation += mouseX;

        // Повернуть игрока вокруг оси Y
        playerTransform.Rotate(Vector3.up * mouseX);

        // Получить входные данные от клавиатуры
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Рассчитать движение относительно поворота камеры
        Vector3 movement = transform.right * horizontalInput + transform.forward * verticalInput;

        // Переместить игрока
        playerTransform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Обновить позицию камеры, чтобы следовать за игроком
        transform.position = playerTransform.position;
      }
    }

    public void Follow(GameObject player)
    {
      playerTransform = player.transform;
    }
  }
}