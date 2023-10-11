using UnityEngine;
using UnityEngine.Timeline;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도


    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // FixedUpdate는 물리 갱신 주기(0.02초)에 맞춰 실행됨 
    private void FixedUpdate() {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
        Rotate();

        Move();

        float inputMove = playerInput.move + Mathf.Abs(playerInput.rotate);
        if (playerInput.move < 0) inputMove = playerInput.move;
        playerAnimator.SetFloat("Move" , inputMove);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {
        // 카메라의 z축방향과 x축방향을 구함
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        // 카메라가 기울어져 있기 때문에 y를 0으로
        cameraForward.y = 0;
        cameraRight.y = 0;
        // 카메라의 Vector를 정규화
        cameraForward.Normalize();
        cameraRight.Normalize();

        // 움직이는 Vector를 입력에 맞게 변화
        Vector3 movement = cameraForward * playerInput.move + cameraRight * playerInput.rotate;
        movement.Normalize();
        playerRigidbody.MovePosition(playerRigidbody.position + (movement * moveSpeed * Time.deltaTime));
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate() {
        //float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        //playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0,turn,0f);
        LookMousePosition();
    }

    /// <summary>
    /// 캐릭터가 마우스가 있는 방향을 바라보게 함
    /// </summary>
    private void LookMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePos = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
            transform.forward = mousePos;
        }
    }
}