# Ballz (Brick Breaker) Unity
��� ����� ����� �� ������, ���� ���� 2 ����� � ���� ���������� (x / y)
x = ((y-y1)/(y2-y1))*(x2-x1) + x1
y = ((x-x1)/(x2-x1))*(y2-y1) + y1

 public Transform originalObject;
    public Transform reflectedObject;
    void Update() {
        reflectedObject.position = Vector3.Reflect(originalObject.position, Vector3.right);
    }
