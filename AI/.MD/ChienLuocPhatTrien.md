# Chiến lược phát triển

## Hướng đi

- Phát triển client và server song song theo từng chức năng.
- Làm một phần chơi nhỏ, hoàn chỉnh và chơi được trực tuyến trước khi mở rộng số lượng bản đồ.
- Kiểm chứng trên máy cục bộ trước; chưa cần thuê hạ tầng lớn hoặc mở công khai sớm.
- Đo tiến độ bằng chức năng chạy xuyên suốt và điều kiện qua mốc, không bằng số lượng bản đồ.

## Lộ trình

| Bước | Việc cần làm | Điều kiện hoàn thành |
|---|---|---|
| 0. Chuẩn hóa nền tảng | Rà soát dự án Unity, map, mã định danh, dữ liệu game và phương án mạng; thống nhất thiết kế kỹ thuật. | Có một thiết kế kỹ thuật thống nhất và được xác nhận. |
| 1. Kết nối cơ bản | Chạy một server cục bộ và hai client; đăng nhập, nhìn thấy nhau và di chuyển trong cùng map. | Hai người chơi cùng di chuyển được. |
| 2. Gameplay trực tuyến | Đưa quái, chiến đấu, sát thương, vật phẩm và nhiệm vụ qua kết nối mạng. | Server xác nhận các kết quả quan trọng. |
| 3. Lưu và chuyển map | Tạo nhân vật, lưu dữ liệu, chuyển map, thoát và đăng nhập lại. | Không mất hoặc nhân đôi dữ liệu. |
| 4. Vertical Slice | Hoàn thiện một đoạn chơi nhỏ có giao diện, nhiệm vụ, quái, chiến đấu, server và lưu dữ liệu. | Người thử chơi trọn một vòng trải nghiệm. |
| 5. Closed Alpha | Đưa server thử nghiệm lên mạng và mời nhóm nhỏ vào chơi. | Có theo dõi lỗi, ổn định và cách khôi phục. |
| 6. Mở rộng | Dùng quy trình đã kiểm chứng để làm thêm map, quái, nhiệm vụ và tính năng. | Nội dung mới vượt qua bộ kiểm tra chung. |

Map cụ thể cho Vertical Slice chưa chốt; bước 0 cần chọn một đoạn chơi có chiến đấu để kiểm chứng trọn vòng trải nghiệm.

## Nguyên tắc kỹ thuật

- Client hiển thị hình ảnh, âm thanh, hoạt ảnh và nhận điều khiển; server kiểm chứng vị trí, chiến đấu, quái, phần thưởng, nhiệm vụ và dữ liệu lưu.
- Máy chủ/thế giới có dữ liệu người chơi riêng nhưng dùng cùng bộ map chuẩn. Map và khu/instance là các khái niệm riêng, không phải mỗi map là một server.
- Chọn một hệ thống mạng/backend chính sau khi rà soát bước 0; chưa chốt công nghệ cụ thể. Unity Dedicated Server cùng Netcode for GameObjects là phương án cần khảo sát.
- Client và server dùng chung mã định danh, sự kiện và cấu trúc dữ liệu game. Chỉ đồng bộ trạng thái cần thiết cho người chơi trong khu vực.
- Hình ảnh và giao diện có thể tiếp tục làm song song nếu không làm chậm việc kiểm chứng online; tài nguyên giao diện không thay thế mốc gameplay.

## Hiện trạng

Toàn dự án đang ở **bước 0**, và bước này chưa hoàn tất. Chưa có bản chơi được, map gameplay, gameplay trực tuyến, server, đăng nhập thật hoặc lưu tiến trình.

Scene `Assets/Scenes/DangNhap.unity` là mẫu giao diện/video trong Unity. Scene có các lớp UI và trường nhập, nhưng chưa kết nối dịch vụ đăng nhập hay danh sách server thật; nó không được tính là prototype online hoặc hoàn thành bước 0. `SampleScene` vẫn chỉ có Main Camera.

## Việc kế tiếp

Kiểm tra cấu trúc Unity và các hệ thống hiện có; liệt kê phần đang chạy trên client, rủi ro mạng và dữ liệu cần thống nhất. Từ kết quả đó, chốt thiết kế client-server, mã định danh, sự kiện và phạm vi prototype bước 1.
