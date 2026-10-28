# AI — Nguồn thông tin Thiên Kiêu

Đọc tài liệu phù hợp trước khi làm việc. Dữ liệu chưa xác nhận phải ghi là **chưa chốt**, không tự thêm chi tiết cốt truyện.

| Tài liệu | Nội dung |
|---|---|
| `../AGENTS.md` | Quy tắc thực hiện và phong cách |
| `.MD/NenTangGame.md` | Game là gì, tiến độ và STYLE MASTER |
| `.MD/ChienLuocPhatTrien.md` | Chiến lược, lộ trình và điều kiện qua từng bước |
| `.MD/BanDo.md` | Mô tả từng bản đồ và liên kết tài nguyên |
| `.MD/KeHoachMAP001.md` | Bố cục và kế hoạch phát triển Thung Lũng Hoang |
| `.MD/img_Order.md` | Ảnh/tài nguyên còn thiếu hoặc chờ duyệt |
| `.MD/DangNhap.md` | Đặc tả màn hình đăng nhập và chuyển cảnh |
| `.MD/TaoNhanVat.md` | Đặc tả màn hình chọn và tạo nhân vật |
| `.Log.md` | Lịch sử theo chủ đề; tra cứu khi thiếu ngữ cảnh |
| `.Queue.md` | Hàng đợi các lượt ghi trùng tệp khi đã có lượt ghi đang diễn ra |

## Ảnh do AI tạo

- `.Generated/Map/<TenMap>/`, `.Generated/NhanVat/`, `.Generated/NPC/`, `.Generated/Quai/`, `.Generated/Item/`.
- Đặt tên tiếng Việt không dấu, viết liền theo kiểu `ThucAn`, `BanhNep`, `DuaHau`.
- Chỉ đưa bản đã duyệt vào Unity. Bản đồ phải liên kết trực tiếp tới ảnh và hoạt ảnh dùng trong map.
- Lưu ảnh chụp kiểm thử khi chơi trong `.ScreenShorts/`.

## Kỹ năng và tài liệu tham khảo

- `../agent-sprite-forge-main/`: bộ công cụ tạo tài nguyên 2D. Khi làm hoạt ảnh nhân vật/quái từ ảnh mẫu hoặc video, đọc `skills/video2dsprite/SKILL.md` trước; chỉ tích hợp tài nguyên đã duyệt.
- `../Tham Khảo/`: lưu ảnh giao diện, ảnh nhân vật, tư liệu và nguồn tham khảo để xem lại, dùng lại khi cần.

## Thử video bằng HeyGen

- Khi gửi yêu cầu tạo video để thử HeyGen, dùng model `heygen-video-1` ở độ phân giải `480p`.
- Đây là cấu hình thử nghiệm để giảm chi phí; không tự áp dụng thay cho độ phân giải của video dùng chính thức.

Các file Markdown viết ngắn gọn, rõ và bằng tiếng Việt. Ưu tiên từ thuần Việt; dùng từ Hán-Việt vừa phải. Chỉ prompt STYLE MASTER giữ nguyên tiếng Anh.
Trước khi báo hoàn tất công việc, cập nhật `.Log.md`.
