# Danh sách ảnh và tài nguyên cần đặt

Ghi ảnh/tài nguyên còn thiếu, đang chờ tạo hoặc chờ duyệt. Dùng tên tiếng Việt không dấu. Chỉ tích hợp phiên bản đã duyệt; tiếp tục việc độc lập trong lúc chờ.

Mỗi tài nguyên có tên riêng, nơi sử dụng, mô tả và trạng thái. Danh sách được chia theo nhóm để dễ đọc và cập nhật.

## Phong cách ảnh

Áp dụng nguyên văn phong cách sau cho mọi ảnh tạo mới hoặc chỉnh sửa:

Whimsical hand-drawn storybook illustration style, stylized cartoon aesthetic, soft painterly textures with visible brush strokes, slightly rough sketchy ink linework, subtle shading, soft lighting, warm and harmonious color palette, playful but balanced proportions, simple yet charming shapes, magical and cozy atmosphere, expressive character design, crisp readable silhouettes, high detail, consistent artistic direction.

## Bản đồ gameplay

### `ThungLungHoang`

- **Dùng cho:** Thung Lũng Hoang.
- **Thiết kế theo:** [Kế hoạch MAP-001](KeHoachMAP001.md). Rừng dày, ánh sáng yếu, không thấy bầu trời; địa hình đi từ thấp lên cao. Đường thoát làm bằng bụi vàng sáng.
- **Lớp rừng xa — `RungXaThungLungHoang`:** Nhiều lớp cây ở độ cao và khoảng cách khác nhau; phủ kín khung camera, nối lặp được, không có trời.
- **Cây trung cảnh — `CayRungThungLungHoang`:** Thân cây cao, mảnh và tán dày, tách từng cây/cụm cây để sắp xếp riêng; cành lá có thể làm lay nhẹ.
- **Bộ địa hình — `TileDiaHinhThungLungHoang`:** Các miếng đất, đá, cỏ, mặt nền, vách, dốc, bậc và lớp đất/đá đỡ bên dưới. Tạo miếng chữ nhật có thể ghép liền, dùng lại ở nhiều đoạn và không hở mép.
- **Đường dẫn — `BuiVangDanDuong`:** Cụm bụi vàng phát sáng, nền trong suốt, có nhiều đoạn nối để rải liên tục trên đường chạy.
- **Đá văng — `HieuUngVoDa`:** Đất bật lên, đá vỡ thành mảnh và bụi đất trên nền trong suốt, dùng khi Khỉ đột nhảy vồ hoặc tiếp đất.
- **Nơi lưu:** `AI/.Generated/Map/ThungLungHoang/`.
- **Đã tạo:** `RungXaThungLungHoang.png`, `CayRungThungLungHoang.png`, `BeMatCoDat.png`, `BoNenDatDa.png`, `BuiVangDanDuong.png`, `HieuUngVoDa.png`, `HieuUngBangDong.png`.
- **Đã ghép thử:** [Scene Thung Lũng Hoang](../../Assets/Scenes/ThungLungHoang.unity); ảnh gốc ở thư mục trên, bản Unity tại `Assets/Art/Map/ThungLungHoang/`.
- **Trạng thái:** Đã dựng bản thử; chờ duyệt mỹ thuật và tiếp tục hoàn thiện địa hình/hoạt ảnh cảnh vật.

### `KhuDatSauNha`

- **Dùng cho:** Khu Đất Sau Nhà.
- **Cần tạo:** Các lớp cảnh, nền phẳng, cỏ dại, cây lớn, hồ và vật nuôi dưới nước.
- **Trạng thái:** Chờ tạo và duyệt.

### `NhaChinh`

- **Dùng cho:** Nhà Chính.
- **Cần tạo:** Các lớp cảnh thanh bình, nhà mái gỗ và cây xoài.
- **Trạng thái:** Chờ tạo và duyệt.

## Quái

### `BaQuaiLinhTruong`

- **Dùng cho:** Thung Lũng Hoang.
- **Cần tạo:** Một mẫu Khỉ đột dùng cho ba nhân vật cùng loài, kèm hoạt ảnh xuất hiện, gào, vỗ ngực, nhảy vồ và choáng. Ba con xuất hiện ở cuối map sau khi người chơi bắt đầu di chuyển 1 giây; sau đó đuổi theo và nhảy vồ khi người chơi chạy được một đoạn. Tạo hình phải đọc rõ ở góc nhìn đi cảnh cuộn ngang.
- **Mẫu được dùng:** Ảnh `KhiDotNghiengPhai.png` đã duyệt; tạo mẫu dùng trong scene tại `BaQuaiLinhTruongMaster.png`.
- **Hoạt ảnh đã có:** `idle`, `jump`, `hurt`; controller Unity tại `Assets/Art/Quai/Animations/Gorilla.controller`.
- **Còn thiếu:** Hoạt ảnh xuất hiện, gào, vỗ ngực và đòn đánh riêng. Sáu lần tạo clip `attack` bằng HeyGen đều lỗi trước khi trả video; chưa có clip để ghép.
- **Lưu ý:** Quái không thể bị hạ gục. Khi đòn trúng, người chơi chỉ bị đẩy lùi ngắn và choáng, không chết.
- **Nơi lưu:** `AI/.Generated/Quai/`.
- **Trạng thái:** Đã ghép ba hoạt ảnh hiện có vào bản scene thử; bộ hoạt ảnh vẫn còn thiếu như ghi ở trên.

### `KhiDot`

- **Dùng cho:** Thung Lũng Hoang.
- **Mô tả:** Bảng concept Khỉ đột với bốn góc nhìn và sáu tư thế, dựa trên ảnh người dùng cung cấp. Chưa xác nhận đây là một trong ba quái linh trưởng.
- **Tệp:** [KhiDot.png](../.Generated/Quai/KhiDot.png).
- **Trạng thái:** Chờ người dùng duyệt.

### `KhiDotNghiengPhai`

- **Dùng cho:** Thung Lũng Hoang.
- **Mô tả:** Mẫu tạo hình Thú Khỉ Đột toàn thân, góc nghiêng quay sang phải.
- **Tệp:** [KhiDotNghiengPhai.png](../.Generated/Quai/KhiDotNghiengPhai.png).
- **Trạng thái:** Đã duyệt.

## Vật phẩm

### `NamHien`

- **Dùng cho:** Thung Lũng Hoang.
- **Mô tả:** Có thể xuất hiện; loài, vị trí và tạo hình chưa chốt.
- **Trạng thái:** Chờ chốt nội dung.

### `ThaoDuocQuy`

- **Dùng cho:** Thung Lũng Hoang.
- **Mô tả:** Có thể xuất hiện; loài, vị trí và tạo hình chưa chốt.
- **Trạng thái:** Chờ chốt nội dung.

### `BanhCuaMe`

- **Dùng cho:** Nhà Chính.
- **Mô tả:** Bánh mẹ vừa làm, dùng từ túi đồ để hồi đầy máu (HP) và thể lực. Loại bánh và tạo hình chưa chốt.
- **Trạng thái:** Chờ tạo và duyệt.

## Nhân vật và NPC

### `NguoiChoiNam`

- **Dùng cho:** Nhân vật người chơi tại MAP-001.
- **Mô tả:** Nam, gương mặt anh tú, dáng cao và ốm theo yêu cầu người dùng.
- **Tệp:** [NguoiChoiNam.png](../.Generated/NhanVat/NguoiChoiNam.png); hoạt ảnh gồm `idle`, `run`, `jump`, `hurt`.
- **Đã tích hợp:** `Assets/Art/NhanVat/Animations/Player.controller` và các clip/atlas cùng thư mục.
- **Trạng thái:** Đã ghép vào scene thử; chờ duyệt tạo hình cuối.

### `AnhHung`

- **Dùng cho:** Thung Lũng Hoang.
- **Đã tạo:** Tạo hình Thiên Kiêu bay lơ lửng; hoạt ảnh `idle` và `cast` trong `Assets/Art/NPC/Animations/Hero.controller`.
- **Còn thiếu:** Hoạt ảnh quơ tay/thi triển riêng nếu cần thay cho clip `cast` hiện có.
- **Vị trí dự kiến:** Xuất hiện khi người chơi chạy đến mốc 50% chiều ngang map.
- **Lời thoại:** “Nhọc con hãy mau chạy đi”. Ghép chữ thành thành phần Unity riêng, không vẽ chữ vào ảnh nhân vật.
- **Hiệu ứng kỹ năng — `HieuUngBangDong`:** Đã tạo lớp hiệu ứng băng riêng, nền trong suốt; lưu cùng nhóm ảnh map Thung Lũng Hoang.
- **Nơi lưu:** Tạo hình tại `AI/.Generated/NPC/`; hiệu ứng map tại `AI/.Generated/Map/ThungLungHoang/`.
- **Trạng thái:** Đã ghép hoạt ảnh hiện có vào scene thử; chờ duyệt tạo hình cuối.

### `SoLam`

- **Dùng cho:** Khu Đất Sau Nhà.
- **Mô tả:** Bạn đồng hành tròn, bán lỏng bán đặc, có mắt và biểu cảm; nhảy ra, trò chuyện và đồng hành. Tạo hình cụ thể chưa chốt.
- **Trạng thái:** Chờ tạo và duyệt.

### `MeMai`

- **Dùng cho:** Nhà Chính.
- **Mô tả:** Mẹ Mai chờ người chơi ở nhà, cho ăn và hướng dẫn dùng bánh. Tạo hình chưa chốt.
- **Trạng thái:** Chờ tạo và duyệt.

## Giao diện HUB MAP-001

### `HubMap001`

- **Gồm:** Thanh sinh lực, mục tiêu chạy thoát, tên bản đồ và các nút Túi đồ, Nhân vật, Nhiệm vụ.
- **Đã tích hợp:** Canvas trong scene [ThungLungHoang](../../Assets/Scenes/ThungLungHoang.unity).
- **Ảnh kiểm tra:** [Map001-Final-HUD.png](../.ScreenShorts/Map001-Final-HUD.png).
- **Trạng thái:** Đã dựng giao diện thử; chờ duyệt bố cục và nhãn chính thức.

## Giao diện đăng nhập

### `NenDangNhapThuyenBay`

- **Dùng cho:** Màn hình đăng nhập.
- **Mô tả:** Bản mới canh cột buồm theo trục boong và dùng nắng dịu từ phía sau góc nhìn, không thấy mặt trời.
- **Tệp:** [ThuyenBayDangNhapV2.png](../.Generated/Map/DangNhap/ThuyenBayDangNhapV2.png).
- **Trạng thái:** Đã duyệt.

### `VideoNenDangNhapThuyenBay`

- **Dùng cho:** Màn hình đăng nhập.
- **Mô tả:** Clip nền 12 giây, 480p; mây, buồm và dây chuyển động nhẹ, có tiếng gió và dây thừng nhỏ.
- **Tệp:** [Video nền](../.Generated/Map/DangNhap/ThuyenBayDangNhapVideo.mp4); đã tích hợp tại [`Assets/Video/DangNhap/`](../../Assets/Video/DangNhap/).
- **Trạng thái:** Đã duyệt theo yêu cầu tích hợp.

### `BangDangNhap`

- **Dùng cho:** Màn hình đăng nhập.
- **Mô tả:** Bản bảng ghép tham khảo tạo trước; scene dùng bộ lớp riêng và không dùng ảnh ghép này.
- **Tệp:** [BangDangNhap.png](../.Generated/Map/DangNhap/BangDangNhap.png).
- **Trạng thái:** Chờ người dùng duyệt.

### `ThanhPhanBangDangNhap`

- **Dùng cho:** Màn hình đăng nhập.
- **Mô tả:** Bộ ảnh PNG tách lớp. Chữ và dữ liệu máy chủ ghép riêng trong Unity.
- **Ảnh gốc:** [Thư mục Thành phần](../.Generated/Map/DangNhap/ThanhPhan/).
- **Gồm:** `KhungGoBang.png`, `LogoThienKieu.png`, `OQuyDanh.png`, `OMatHieu.png`, `IconMatMo.png`, `IconMatDong.png`, `OChonMayChu.png`, `DongMayChu.png`, `NutXuatPhat.png`, `BieuTuongQuenMatHieu.png`.
- **Đã tích hợp tại:** [`Assets/Art/DangNhap/ThanhPhan/`](../../Assets/Art/DangNhap/ThanhPhan/).
- **Trạng thái:** Đã duyệt theo yêu cầu tích hợp.
