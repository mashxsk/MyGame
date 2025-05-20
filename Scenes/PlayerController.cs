using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float minJumpVelocity = 5f;     // ��������� �������� �����
    [SerializeField] private float maxJumpVelocity = 12f;    // ������������ �������� �����
    [SerializeField] private float holdDuration = 0.3f;       // ������� �������� ����� ����������
    [SerializeField] private ContactFilter2D platformFilter;

    private Rigidbody2D _rigidbody;
    private bool isHoldingJump = false;
    private float holdTimer = 0f;

    private bool IsOnPlatform => _rigidbody.IsTouching(platformFilter);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // ������ ������
        if (Input.GetKeyDown(KeyCode.Space) && IsOnPlatform)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, minJumpVelocity);
            isHoldingJump = true;
            holdTimer = 0f;
        }

        // ���������� � ����������� �������� �����
        if (Input.GetKey(KeyCode.Space) && isHoldingJump)
        {
            holdTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(holdTimer / holdDuration);
            float targetVelocity = Mathf.Lerp(minJumpVelocity, maxJumpVelocity, progress);
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, targetVelocity);
        }

        // ��������� � ���������� ���������� ������
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }

        // ��������� ����� ��������� � ��������� ����������
        if (holdTimer >= holdDuration)
        {
            isHoldingJump = false;
        }
    }
}


