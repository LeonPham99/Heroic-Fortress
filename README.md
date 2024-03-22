Tiến độ dự án

Phần II: Enemy

1. Tạo Animations cho các enemy trong tag Animation và Animator

2. Tạo chức năng di chuyển cho Enemy

  Tạo class Enemy
- Đảm nhận tất cả các cơ chế di chuyển theo các đường point đã tạo ra trước đó

  Tạo class LevelManager
- Thực hiện trừ đi số mạng của người chơi khi enemy đi đến point cuối cùng trong play scene

  Cập nhật class Spawner
- Điều chỉnh Function SpawnEnemy()
  Thêm vào logic để Enemy di chuyển theo các Waypoint khi được spawn ra trong play scene
- Thêm hai function OnEnable và OnDisable để thực hiện sự kiện kết thúc vòng chơi khi enemy cuối cùng bị tiêu diệt hoặc đi đến point cuối
- Thêm chức năng IEnumerator NextWave để thực hiện delay mỗi khi kết thúc 1 vòng và khởi động lại thời gian spawn quái và số lượng enemy được spawn

3. Tạo thanh máu cho Enemy
  Tạo một thanh máu bằng Canvas + Image trong Hierachy cho enemy và tạo thêm 1 object trống để chứa thanh máu của enemy

  Tạo class EnemyHealth
  - Đảm nhận tất cả giá trị và các chức năng liên quan đến thanh máu

  Tạo class EnemyHealthContainer
  - Thay đổi UI thanh máu của enemy khi nhận sát thương

  Điều chỉnh class EnemyHealth
  - Chỉnh function Die(). Khi enemy hết máu sẽ trả về pooling thay và tắt đi thay vì destroy
  - Thêm function ResetHealth() để set giá trị thanh máu của enemy quay lại trạng thái ban đầu nếu Enemy đi đến điểm cuối mà vẫn chưa chết

  Điều chỉnh lại class Enemy
  - Chỉnh function EndPointReached(). Reset lại máu của enemy khi đến điểm cuối

4. Thêm vào các Animation khác cho Enemy
   Tạo class EnemyAnimations
   - Kích hoạt các animation của enemy bằng trigger đã tạo trong Animator của Unity
   - Thêm hai chức năng EnemyHit() và EnemyDie() để thực hiện logic kích hoạt animation
   - Thêm vào chức năng OnEnable() và OnDisable() để kích hoạt sự kiện khi enemy bị gây sát thương + chết
   - Thêm chức năng GetCurrentAnimationLenght() để xác định số giây animation sẽ chạy
   - Thêm vào chức năng IEnumerator PlayHit để thực hiện logic kích hoạt animation. Khi animation kết thúc sẽ gọi chức năng ResumeMovement() đã tạo trong class Enemy
   - IEnumerator PlayDead sẽ tương tự như PlayHit nhưng sẽ có thêm 2 chức năng ResetHealth() của enemy và IEnumerator PlayDead
  
     Note:
     - ResetHealth() của enemy trong IEnumerator PlayDead để chắc chắn set thanh máu của enemy lại lúc đầu vì sẽ tái sử dụng của enemy này.
     - ObjecctPooler.ReturnToPool(gameObject) lúc này sẽ nằm trong PlayDead IEnumerator nhằm ẩn enemy đi khi play animation dead của enemy

   Chỉnh sửa lại class EnemyHealth
   - Thêm code vào hàm DealDamage() để thực hiện kích hoạt animation của enemy khi bị gây sát thương
   - Xóa ResetHealth() và ObjecctPooler.ReturnToPool(gameObject)
     
   Chỉnh sửa lại class Enemy
   - Tạo một thuộc tính MoveSpeed tĩnh để giá trị tốc độ của enemy không thay đổi khi tiếp tục di chuyển sau khi bị gây sát thương
   - Tạo thêm hai chức năng StopMovement() và ResumeMoveMent() để enemy có thời gian chạy animation

5. Điều chỉnh hướng của Enemy khi di chuyển đến các point (Quay trái, quay phải)
   Thêm vào class Enemy
   - Tạo một hàm lastpointPosition để kiểm tra vị trí cuối cùng của enemy và thêm vào chức năng CurentPointPositionReached()
   - Lấy thành phần của SpriteRenderer
   - Tạo chức năng Rotate và thêm vào logic để flip sprite của enemy
     
  Note: Thanh máu của enemy bị thay đổi khi nhân vật flip sang trái (Đang sửa)
