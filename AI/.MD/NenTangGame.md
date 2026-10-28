# Nền tảng game

## Game là gì?

Định hướng hiện tại là game Unity đi cảnh cuộn ngang: khám phá bản đồ, chạy và nhảy, chiến đấu với quái, gặp NPC, làm nhiệm vụ và thu thập vật phẩm/thảo dược.

Máy chủ là các phiên riêng có cùng nội dung và bộ bản đồ; có thể tinh chỉnh từng máy chủ theo yêu cầu. Máy chủ không phải bản đồ.

Tài liệu đã mô tả phần mở đầu cốt truyện; chưa triển khai trong game. Diễn biến về sau, các nhân vật/NPC khác, hệ thống chiến đấu chi tiết và nền tảng phát hành chưa chốt. Không tự đặt tên nhân vật ngoài những tên người dùng đã cung cấp.

## Mở đầu trải nghiệm

1. Sau khi tạo tài khoản và vào thế giới, người chơi xuất hiện ở **Thung Lũng Hoang**. Ba quái thú linh trưởng hung tợn rượt đuổi; đường vàng hướng dẫn người chơi chạy thoát khỏi bản đồ.
2. Một kiếm sĩ Thiên Kiêu trong làng, chỉ được gọi là **Anh Hùng**, chặn quái và giúp người chơi chạy thoát. Danh tính của người này là bí ẩn. Ba quái không thể bị hạ gục; nếu người chơi chết, hồi sinh gần đó và chạy lại.
3. Ở Khu Đất Sau Nhà, bạn đồng hành **Sờ Lam** xuất hiện, nói “Phù, may mắn,…” và giải thích chuyện vừa xảy ra. Sờ Lam hướng dẫn người chơi đến gặp mẹ đang chờ ở Nhà Chính.
4. Mẹ **Mai** cho người chơi ăn bánh vừa làm và hướng dẫn mở túi đồ, dùng bánh để hồi đầy thể trạng: máu (HP) và thể lực.

Mỗi đầu hoặc cuối bản đồ, khi người chơi đến gần lối chuyển, hiện tên bản đồ kế tiếp.

## Chia khu trong bản đồ

- Mỗi bản đồ có ba **khu tân thủ**. Ghép người chơi có sức mạnh và chặng nhiệm vụ gần nhau vào cùng khu để tránh lệch sức và làm hỏng mạch truyện.
- Các **khu chung** còn lại mở cho mọi người chơi.
- Khu tân thủ và khu chung là các khu bên trong cùng bản đồ, không phải bản đồ hay máy chủ mới.
- Nhiệm vụ và mốc truyện được lưu theo từng người chơi; hành động của người khác không được làm người chơi bỏ lỡ hoặc vượt trước phần truyện của mình.
- Mốc sức mạnh và cách chia nhóm cụ thể: chưa chốt.

## Tiến độ

- Unity `6000.3.23f1`.
- Có `SampleScene` và tài nguyên điều khiển `InputSystem_Actions.inputactions`.
- Toàn dự án đang ở **bước 0**; thiết kế kỹ thuật thống nhất chưa hoàn tất. Lộ trình và điều kiện qua mốc ghi tại [ChienLuocPhatTrien.md](ChienLuocPhatTrien.md).
- `SampleScene` chỉ có Main Camera; chưa có map gameplay hoặc vòng chơi hoàn chỉnh.
- Chưa có gameplay, server, kết nối mạng, đăng nhập thật, lưu tiến trình, NPC, quái hoặc nhiệm vụ chạy trong game.
- Scene đăng nhập hiện có là mẫu giao diện/video; chưa kết nối tài khoản hay server thật, không được tính là prototype online hoặc hoàn thành bước 0. Scene này đứng đầu Build Settings.
- Đã có đặc tả về map, giao diện đăng nhập, tạo tài khoản và chọn/tạo nhân vật; các phần này chưa thành hệ thống chơi hoàn chỉnh.
- Đã mô tả ba bản đồ mở đầu và diễn biến gặp Anh Hùng, Sờ Lam, mẹ Mai; chưa dựng map trong Unity. Chi tiết tại [BanDo.md](BanDo.md).

## Cách dùng từ

Ưu tiên từ thuần Việt trong lời thoại, giao diện và tài liệu. Dùng từ Hán-Việt vừa phải, khi hợp sắc thái hoặc đã chốt tên riêng.

## STYLE MASTER

Whimsical hand-drawn storybook illustration style, stylized cartoon aesthetic, soft painterly textures with visible brush strokes, slightly rough sketchy ink linework, subtle shading, soft lighting, warm and harmonious color palette, playful but balanced proportions, simple yet charming shapes, magical and cozy atmosphere, expressive character design, crisp readable silhouettes, high detail, consistent artistic direction.
