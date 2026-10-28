# Màn hình đăng nhập

## Khung cảnh

- Góc nhìn từ phía sau bên trong thuyền, hướng về mũi thuyền. Bảng đăng nhập ở giữa và là điểm nhìn chính.
- Thuyền làm bằng gỗ lâu năm, thấy rõ vân gỗ; thùng đồ được xếp gọn, không làm rối khung hình.
- Hai bên mạn thuyền có khách đứng trò chuyện và ngắm cảnh. Gió nhẹ lay động cảnh vật.
- Trời xanh buổi sớm, ánh sáng dịu, vài dải mây và đàn chim bay xa.

## Bảng đăng nhập

- Tiêu đề: **Thiên Kiêu**. Dòng mời lên thuyền: **Lên thuyền**.
- **Quý Danh:** ô nhập tên tài khoản, làm bằng gỗ lâu năm.
- **Mật Hiệu:** ô nhập mật khẩu cùng chất liệu. Chữ nhập màu trắng đậm, viền đen. Có nút mắt ẩn/hiện; mặc định ẩn.
- **Nơi quý khách muốn đến:** danh sách máy chủ để chọn. Các máy chủ là những phiên riêng có cùng bản đồ, có thể tinh chỉnh riêng; chọn máy chủ không phải chọn bản đồ. Mặc định chọn máy chủ đăng nhập lần cuối, lưu trên máy người chơi. Khi chọn máy chủ khác, lấy tình trạng hiện tại từ máy đó.
- Mỗi máy chủ hiển thị trong ô vuông bo góc, nền vàng cam, chữ xanh lá có viền đen. Chấm báo tình trạng: đỏ là rất đông, cam là đông, xanh lá là bình thường.
- Nút **Xuất Phát** để đăng nhập: hình chữ nhật có nét thuyền gỗ, đơn giản và nổi bật hơn ô nhập. Chữ đỏ hồng, lớn hơn các nhãn khác.
- **Quên Mật Hiệu?** kèm biểu tượng dễ thương, mở luồng lấy lại mật khẩu.
- **Quý khách chưa có tài khoản?** mở luồng tạo tài khoản và chọn nhân vật.

## Các lớp thành phần

Ghép các lớp theo thứ tự từ sau ra trước; mỗi món có thể thay hoặc chỉnh riêng:

1. `KhungGoBang.png` — khung bảng gỗ lớn.
2. `LogoThienKieu.png` — logo chữ **Thiên Kiêu**.
3. `OQuyDanh.png` và `OMatHieu.png` — nền hai ô nhập; nhãn và nội dung nhập nằm trên lớp chữ riêng.
4. Lớp chữ `Quý Danh`, `Mật Hiệu`, `Lên thuyền` và `Nơi quý khách muốn đến`.
5. `IconMatMo.png` / `IconMatDong.png` — hai trạng thái nút hiện/ẩn mật khẩu.
6. `OChonMayChu.png` — thanh chọn máy chủ; `DongMayChu.png` — dòng máy chủ có thể lặp. Tên máy chủ và chấm màu tình trạng được hiển thị riêng.
7. `NutXuatPhat.png` — mặt nút; chữ `Xuất Phát` là lớp riêng.
8. `BieuTuongQuenMatHieu.png` và chữ `Quên Mật Hiệu?` — biểu tượng và liên kết lấy lại mật khẩu.
9. Chân bảng gồm hai lớp chữ riêng: `Quý khách chưa có tài khoản?` và `Đăng kí mới`.

Tài nguyên PNG nằm trong `AI/.Generated/Map/DangNhap/ThanhPhan/`. Chữ và dữ liệu máy chủ không ghép vào ảnh để có thể chỉnh trong Unity. Bộ lớp đã được tích hợp theo yêu cầu ngày 2026-10-29; dữ liệu máy chủ thật chưa có.

## Chuyển cảnh

- Đăng nhập thành công: thuyền tăng tốc về phía trước rồi chuyển sang màn hình đang tải.
- Màn hình đang tải có nền đen; thuyền nhỏ lướt ở giữa cùng hiệu ứng báo đang tải.

## Tạo tài khoản

- Từ lời mời chưa có tài khoản, mở bảng tạo tài khoản trong bối cảnh thuyền.
- Các ô gồm **Quý Danh**, **Mật Hiệu**, **Nhập lại Mật Hiệu** và **Số điện thoại**.
- Người chơi yêu cầu mã xác nhận 6 số, nhập mã rồi chọn **Đăng Ký**. Cách gửi mã chưa chốt.
- Đăng ký thành công sẽ mở màn hình chọn/tạo nhân vật, là màn hình đầu tiên sau khi tạo tài khoản.

## Tiến độ

Đặc tả đã ghi; video nền và bộ 10 PNG thành phần được tích hợp theo yêu cầu ngày 2026-10-29. Scene [DangNhap.unity](../../Assets/Scenes/DangNhap.unity) dùng video lặp, hiển thị ảnh tĩnh đã duyệt trong lúc video nạp, và ghép riêng khung, logo, ô nhập, biểu tượng, nút cùng các lớp chữ. Trường Quý Danh/Mật Hiệu dùng được; nút mắt ẩn/hiện Mật Hiệu đã có xử lý. Dữ liệu máy chủ chưa có nên ô chọn dùng lời nhắc và dòng mẫu ẩn; đăng nhập, quên mật hiệu và đăng kí mới chưa nối luồng thật.
