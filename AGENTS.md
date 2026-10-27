# Thiên Kiêu — Quy tắc dự án

## Quy tắc tạo bản đồ

- Mọi yêu cầu tạo mới hoặc chỉnh sửa hình ảnh cho dự án Thiên Kiêu đều phải tự động áp dụng STYLE MASTER dưới đây, kể cả khi người dùng không nhắc đến phong cách. Trước khi tạo hoặc chỉnh ảnh, đọc phần này và chèn nguyên văn STYLE MASTER tiếng Anh vào prompt; không dịch, rút gọn hoặc diễn đạt lại. Áp dụng thống nhất cho bản đồ, NPC, quái vật, vật phẩm và giao diện. Các hướng dẫn khác viết bằng tiếng Việt. Chỉ đổi phong cách khi người dùng yêu cầu rõ ràng.
- Dựng bản đồ từ nhiều tài nguyên riêng biệt, có thể tái sử dụng và chỉnh sửa độc lập; không dùng một ảnh tĩnh duy nhất.
- Tách các lớp hậu cảnh như bầu trời, mây, núi xa và rừng xa để tạo chiều sâu.
- Hậu cảnh không cần kéo dài theo toàn bộ bản đồ. Mỗi lớp chỉ cần phủ kín khung camera và dịch chuyển theo camera khi camera chạy theo người chơi; dùng parallax và các mảnh nền lặp phù hợp để luôn phủ kín khung, không lộ mép hoặc khoảng trống. Địa hình chơi vẫn được bố trí theo chiều dài màn chơi.
- Đặt cây cối, công trình, đá và cảnh vật phía sau nhân vật ở lớp trung cảnh.
- Tạo địa hình chơi được với nhiều cao độ, dốc, bậc, vách đá, hố và mép nền để người chơi chạy, nhảy.
- Tách bụi cây, cỏ, cành lá và vật thể tiền cảnh để có thể tạo hoạt ảnh độc lập.
- Ghép đất, đá, cỏ và các bề mặt Tilemap liền mạch, có lớp nền nâng đỡ, kích thước và tỷ lệ thống nhất.
- Thiết lập va chạm chính xác cho mặt đất, vật cản, vách đá và các bề mặt đứng.
- Mỗi bản đồ cần có lối vào, lối ra và điểm xuất hiện, cùng một vị trí chuyển tiếp dễ nhận biết như cổng, lối đi hoặc ranh giới khu vực. Khi người chơi đến gần vị trí đó, hiển thị tên khu vực sắp đến trong bảng nổi dễ đọc phía trên nhân vật, giống cách trình bày trong ảnh tham chiếu. Ẩn tên khi người chơi rời khỏi vùng tiếp cận; chỉ chuyển bản đồ khi người chơi đi vào hoặc kích hoạt lối chuyển tiếp.
- Mỗi NPC cần có mã riêng, vị trí, hoạt ảnh, hội thoại và nhiệm vụ phù hợp.
- Mỗi loài quái cần có khu vực xuất hiện, chỉ số, hành vi, hoạt ảnh, cách tấn công và vật phẩm rơi riêng.
- Mỗi bản đồ cần có mục đích, diễn biến, điều kiện hoàn thành và liên kết với cốt truyện.
- Tạo hoạt ảnh phù hợp cho cây, cỏ, nước, NPC, quái vật và các vật thể cần chuyển động.
- Chỉ tích hợp phiên bản tài nguyên đã được duyệt. Ghi tài nguyên còn thiếu hoặc đang chờ duyệt vào `AI/.MD/order.md`; tiếp tục những phần việc không phụ thuộc vào tài nguyên đó.
- Lưu tài nguyên do AI tạo trong `AI/.Generated/`. Ưu tiên tái sử dụng tài nguyên sẵn có; tránh tạo bản trùng hoặc đặt rải rác.
- Chụp màn hình từ góc nhìn khi chơi để kiểm thử bản đồ. Lưu ảnh trong `AI/.ScreenShorts/`, đánh giá như người chơi và cải thiện các lỗi hình ảnh hoặc hiển thị.
- Phối hợp theo vai trò: Thiết kế phụ trách hình ảnh, Lập trình phụ trách triển khai, Quản lý theo dõi tiến độ. Không khóa độc quyền toàn bộ dự án.
- Giữ các scene Unity hiển thị và có thể chỉnh sửa. Lưu scene trong `Assets/`; không thêm quy tắc Git ignore khiến các file scene `.unity` bị ẩn hoặc loại bỏ.

### Mẫu phong cách (STYLE MASTER)

Whimsical hand-drawn storybook illustration style, stylized cartoon aesthetic, soft painterly textures with visible brush strokes, slightly rough sketchy ink linework, subtle shading, soft lighting, warm and harmonious color palette, playful but balanced proportions, simple yet charming shapes, magical and cozy atmosphere, expressive character design, crisp readable silhouettes, high detail, consistent artistic direction.
