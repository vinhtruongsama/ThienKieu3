# Kế hoạch MAP-001 — Thung Lũng Hoang

## Phạm vi và trạng thái

Tài liệu này là khung thiết kế và kế hoạch phát triển. MAP-001 hiện có scene thử tại `Assets/Scenes/ThungLungHoang.unity`, kèm mã chạy map, ảnh môi trường và hoạt ảnh đã ghép để kiểm tra trong Unity. Đây chưa phải bản hoàn chỉnh: bộ hoạt ảnh Khỉ đột còn thiếu một số động tác, mỹ thuật và nhịp chơi vẫn cần duyệt. Dự án vẫn cần chốt nền tảng kỹ thuật trước khi mở rộng sản phẩm.

Giả định người chơi đã chọn xong nhân vật. Khi vào thế giới, nhân vật được đưa tới điểm xuất hiện của Thung Lũng Hoang. Đăng nhập, tạo nhân vật và máy chủ nằm ngoài phạm vi bản thiết kế map này.

## Nội dung đã chốt

- Đây là bản đồ mở đầu, mã `MAP-001`, tên dữ liệu `ThungLungHoang`.
- Người chơi bị ba quái thú Khỉ đột; không thể hạ gục chúng. Đường vàng chỉ lối thoát.
- Địa hình thung lũng đi từ thấp lên cao, hướng tới Khu Đất Sau Nhà. Cảnh có nhiều cây thân nhỏ, cao, tán dày và ánh sáng yếu.
- Anh Hùng, một Thiên Kiêu, dáng ngầu chưa rõ danh tính, chặn quái bằng kĩ năng quơ tay đóng băng quái và giúp người chơi thoát.
- Ba Khỉ Đột xuất hiện cùng lúc, nhảy ra và gầm gừ rồi mới rượt đuổi. Chúng không cản đường và giữ khoảng đứng riêng; đội hình đổi thứ tự theo thời gian. Chỉ cú nhảy vồ đáp sát người chơi mới gây đẩy lùi và choáng; người chơi không chết.
- Hoàn thành map khi đến lối ra; gần lối ra hiện tên Khu Đất Sau Nhà.
- Map riêng này chỉ có 1 mình Người chơi vào được nên không cần chia Khu.

## Bố cục màn chơi đề xuất

Đây là bố cục để dựng bản xám đầu tiên; vị trí, kích thước và chướng ngại cụ thể chưa chốt.

| Đoạn                       | Bố trí và mục đích                                                                                                                                                                                                                 |
| -------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 1. Điểm xuất hiện          | Đặt nhân vật ở phần thấp của thung lũng. Đường vàng phải đọc được ngay. Đề xuất bắt đầu cuộc rượt đuổi khi quyền điều khiển được trao cho người chơi; cách báo hiệu quái đang đuổi cần chốt khi dựng.                              |
| 2. Lối thung lũng          | Hẹp dần giữa các thân cây cao, đưa người chơi lên cao. Dùng các đoạn nền bằng ngắn xen dốc và bậc thấp để người chơi chạy, nhảy mà không mất dấu đường chính.                                                                      |
| 3. Đoạn vượt địa hình      | Tạo một số chỗ đổi cao độ và khe/vách nhỏ để kiểm tra chạy, nhảy và va chạm. Chỉ dùng hình khối xám; số lượng, độ khó và hình dạng cuối cùng cần thử trong game.                                                                   |
| 4. Điểm Anh Hùng chặn quái | Đặt cuộc chạm mặt ở nửa sau đường chạy để tạo điểm giải tỏa trước lối ra. Anh Hùng chặn ba quái theo diễn biến đã chốt; cách trình bày bằng hoạt ảnh, lời thoại hay đoạn chuyển cảnh chưa chốt.                                    |
| 5. Lối ra                  | Đường tiếp tục lên cao và dẫn rõ tới Khu Đất Sau Nhà. Khi tới gần, hiện bảng nổi tên khu vực kế tiếp; chỉ chuyển map khi người chơi bước chạm mép cuối cùng của map hoặc click đúp vào lối ra. sẽ tự chạy đến mep map để kích hoạt |
|                            |

Không thêm nhánh nhiệm vụ, trận đánh, vật phẩm bắt buộc hay nhân vật mới vào bản xám. Nấm hiếm và thảo dược quý chỉ được bổ sung sau khi chốt vị trí, tác dụng và cách lấy.

## Cấu trúc cảnh và va chạm

- **Hậu cảnh:** các lớp cây cao thấp đan xen cảnh rừng rú không thấy được bầu trời. Mỗi lớp phủ đủ khung camera và dùng parallax/nền lặp để không lộ mép khi camera chạy.
- **Trung cảnh:** thân cây, bụi cây và cảnh vật phía sau người chơi; giữ tán dày nhưng không che đường vàng hay nhân vật.
- **Địa hình chơi:** Tilemap đất, đá, cỏ và lớp đất/đá nâng đỡ riêng . Bố trí nền liền mạch (vẽ từng đoạn map hình chữ nhật rồi ghép lại với nhau), nhiều cao độ và dốc; collider bám đúng mặt đứng, vách và mép nền.
- **Tiền cảnh:** cỏ, cành và lá tách riêng để có thể chuyển động độc lập. Không đặt vật trang trí che nhân vật ở các đoạn cần đọc bước nhảy.
- **Camera:** camera cuộn ngang theo người chơi. Giới hạn camera theo vùng màn chơi; hậu cảnh chạy theo camera với tốc độ riêng. Kích thước tôi chưa nghĩ ra, nhưng hãy thử test với các thông số sau : nhân vật có chiều cao bằng 1/3 chiều cao hiển thị khung camera. khung camera có chiều cao bằng 1/2 chiều cao Toàn map. có nghĩa là khi người chơi click giữ vào màng hình có thể re chuột để kéo xem các phần còn lại của map như thế nào.
- **Điều khiển thử:** A/D hoặc mũi tên trái/phải để di chuyển; Space hoặc mũi tên lên để nhảy. Người chơi được nhảy tối đa hai lần trước khi chạm đất; số lần nhảy được phục hồi khi tiếp đất.
- **Điểm map:** Khi người chơi bắt đầu di chuyển được 1 giây, ba Khỉ Đột ở cuối map cùng nhảy ra, gầm gừ rồi mới đuổi theo. Chúng không va chặn người chơi hoặc đẩy nhau; khi đứng/chạy gần nhau, chúng tự giữ khoảng cách và thỉnh thoảng đổi thứ tự đội hình bằng cú nhảy vượt nhau. Mỗi lần chỉ một con nhảy vồ; người chơi chỉ bị đẩy lùi và choáng nếu con đó đáp đủ sát. Khi người chơi chạy được khoảng 50% chiều ngang map, Anh Hùng xuất hiện, bay lơ lửng, quơ tay dùng chiêu khiến quái bị choáng và nói: “Nhọc con hãy mau chạy đi”.
- **Đường vàng:** giữ liên tục qua các đoạn quan trọng, nổi trên nền đất và dễ thấy trong cảnh thiếu sáng. Cách vẽ đường (dải nền các bụi vàng sáng).

## Nhịp chơi và điều kiện đạt

1. Người chơi bắt đầu di chuyển được 1 giây thì ba Khỉ Đột cùng nhảy ra, gầm gừ; sau khi kết thúc tiếng gầm chúng mới rượt theo.
2. Ba quái không thể bị hạ gục, không chặn người chơi và không chồng lên nhau khi đứng. Chúng đổi vị trí đội hình theo từng nhịp; chỉ một con được nhảy vồ mỗi lượt.
3. Cú vồ chỉ gây đẩy lùi và choáng khi quái đáp sát người chơi; chạy chạm hoặc lướt xuyên qua quái không gây choáng.
4. Anh Hùng chặn quái theo mạch truyện đã xác nhận.
5. Người chơi tới lối ra; bảng nổi ghi **Khu Đất Sau Nhà** khi đến gần và map chỉ chuyển ở vùng lối ra.

Điều kiện hoàn thành map: người chơi vào vùng lối ra. Nhiệm vụ và mốc truyện phải thuộc về từng người chơi để người khác không làm họ bỏ lỡ diễn biến.
