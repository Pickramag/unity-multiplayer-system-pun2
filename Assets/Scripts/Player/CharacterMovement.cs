using UnityEngine;
using Photon.Pun;

public class CharacterMovement : MonoBehaviour
{
    [Header("Основные переменные")]

    [Space]

    [SerializeField, Tooltip("Скорость игрока")] private float speed;

    private float gravityForce;
    private Vector3 moveVector;

    private CharacterController controller;
    private Animator animator;
    private JoystickController mobile;
    private PhotonView photonView;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mobile = GetComponent<JoystickController>();
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
            SmoothlyCameraFollow.instance.target = transform;
    }

    private void Update()
    {
        MovePlayer();
        GravityPlayer();
    }

    private void MovePlayer()
    {
        if (photonView.IsMine)
        {
            moveVector = Vector3.zero;

            moveVector.x = mobile.Horizontal() * speed;
            moveVector.z = mobile.Vertical() * speed;

            if (moveVector.x != 0 || moveVector.z != 0)
                animator.SetBool("IsRun", true);
            else
                animator.SetBool("IsRun", false);

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, speed, 0.0f);

                transform.rotation = Quaternion.LookRotation(direction);
            }

            moveVector.y = gravityForce;
            controller.Move(moveVector * Time.deltaTime);
        }
    }

    private void GravityPlayer()
    {
        if (controller.isGrounded)
            gravityForce -= 20f * Time.deltaTime;
        else
            gravityForce = -1f;
    }
}
