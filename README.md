Tiến độ dự án

Phần I:

1. Tạo dự án và các tập tin cần thiết (20240315)
   Dùng Unity Version: 2022.3.20f1
   
2. Tạo chức năng spawn enemy
   Tạo class Waypoint (20240316)
   - Chức năng lớp này tạo con đường các vị trí trong một không gian, tham chiếu để di chuyển các đối tượng trò chơi theo tham chiếu này

   Tạo class WaypointEditor
   - Chức năng của lớp này cho phép điều chỉnh và cập nhật các vị trí point đã vẽ trong unity trực tiếp bằng chuột thông qua scene thay vì phải chỉnh thủ công trong Inspector
  
   Tạo class SpawnSystem
   - Đảm nhận tất cả các chức năng spawn enemy. Ví dụ như: Thời gian spawn enemy giữa các vòng, tracking số lượng enemy đã spawn, spawn enemy theo thời gian ngẫu nhiên...
  
   Tạo class ObjectPooling (Áp dụng Object Pooling Pattern)
   - Đảm nhận việc sinh ra các objects cần sử dụng và deactive toàn bộ objects enemy nếu chưa sử dụng.
   - Tạo vùng chứa cho các đối tượng được tải sẵn.  
     Object Pooling Pattern
     Điểm mạnh:
   - Tái sử dụng được các object enemy trong game tower defense và giảm thiểu memory fragmentation do Garbage Collector
     
     Điểm yếu:
   - Làm chậm quá trình khởi động của game (Vì sẽ sinh ra một danh sách objects cần sử dụng trong game)
3. Tạo các Enemy di chuyển theo vị trí point đã định sẵn
   - Tạo Enemies kẻ thù hoạt động di chuyển, bị thương và bỏ chạy
   - Tạo Animation cho enemies bị die.
   - Tạo Animation cho enemies đang chạy.
