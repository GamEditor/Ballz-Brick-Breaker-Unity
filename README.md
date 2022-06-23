# Ballz (Brick Breaker) Unity
как найти точку на прямой, если есть 2 точки и одна координата (x / y)
x = ((y-y1)/(y2-y1))*(x2-x1) + x1
y = ((x-x1)/(x2-x1))*(y2-y1) + y1

 public Transform originalObject;
    public Transform reflectedObject;
    void Update() {
        reflectedObject.position = Vector3.Reflect(originalObject.position, Vector3.right);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("ApplyDamage", 10);
        }
    }
