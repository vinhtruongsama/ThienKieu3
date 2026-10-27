# Thiên Kiêu — Quy tắc dự án

## Quy tắc dự án

- Trước khi làm việc cho game, đọc `AI/README.md`, `AI/.MD/NenTangGame.md` và tài liệu liên quan trong `AI/.MD/`. Xem tài liệu trong `AI/` là nguồn thông tin chính; không suy diễn phần chưa chốt.
- Khi thiếu ngữ cảnh hoặc không chắc, tra mục lục trong `AI/.Log.md` rồi đọc đúng phần liên quan; đọc toàn bộ nếu cần.
- Trước khi báo hoàn tất bất kỳ công việc nào, dù lớn hay nhỏ, cập nhật `AI/.Log.md`: ghi việc đã làm, kết quả/trạng thái và file liên quan vào đúng chủ đề. Nếu là chủ đề mới, thêm mục và cập nhật mục lục. Ghi log xong rồi mới báo cáo.
- Trước mỗi lượt đọc hoặc ghi tệp dùng chung, yêu cầu Quản lý đăng ký lượt trong `AI/.Queue.md`. Các lượt trên cùng một tệp — kể cả đọc-đọc — chạy lần lượt theo thứ tự đăng ký; lượt sau chờ đến khi lượt trước hoàn tất và nhả tệp. Tệp khác nhau vẫn có thể xử lý song song. Chỉ Quản lý cập nhật hàng đợi; không khóa cả dự án.
- Mọi yêu cầu tạo hoặc chỉnh ảnh cho Thiên Kiêu phải tự động dùng nguyên văn STYLE MASTER trong `AI/.MD/NenTangGame.md`, kể cả khi người dùng không nhắc phong cách. Không dịch, rút gọn hoặc diễn đạt lại prompt này. Chỉ đổi phong cách khi người dùng yêu cầu rõ ràng.
- Tài liệu Markdown của game viết ngắn gọn, rõ ý, cụ thể và bằng tiếng Việt; chỉ prompt STYLE MASTER được giữ nguyên bằng tiếng Anh.
## Quy tắc tạo bản đồ

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
- Chỉ tích hợp phiên bản tài nguyên đã được duyệt. Ghi ảnh còn thiếu hoặc đang chờ duyệt vào `AI/.MD/img_Order.md`; tiếp tục những phần việc không phụ thuộc vào tài nguyên đó.
- Lưu ảnh do AI tạo theo nhóm trong `AI/.Generated/Map/`, `AI/.Generated/NhanVat/`, `AI/.Generated/NPC/`, `AI/.Generated/Quai/` hoặc `AI/.Generated/Item/`. Dùng tên tiếng Việt không dấu, viết liền như `ThucAn`, `BanhNep`, `DuaHau`; ảnh map có thư mục con theo tên map. Tránh tạo bản trùng hoặc đặt rải rác.
- Chụp màn hình từ góc nhìn khi chơi để kiểm thử bản đồ. Lưu ảnh trong `AI/.ScreenShorts/`, đánh giá như người chơi và cải thiện các lỗi hình ảnh hoặc hiển thị.
- Phối hợp theo vai trò: Thiết kế phụ trách hình ảnh, Lập trình phụ trách triển khai, Quản lý theo dõi tiến độ. Không khóa độc quyền toàn bộ dự án.
- Giữ các scene Unity hiển thị và có thể chỉnh sửa. Lưu scene trong `Assets/`; không thêm quy tắc Git ignore khiến các file scene `.unity` bị ẩn hoặc loại bỏ.
