# Tự ghép địa hình trong Unity

Hướng dẫn này dành cho lần đầu dùng Unity. Làm trong scene luyện tập riêng; không cần viết mã.

## 1. Hiểu bộ tài nguyên

PNG là hình ảnh. Bản thân hình không làm nhân vật đứng được; phần đi lại cần có vật va chạm riêng.

Bộ cụm địa hình trong `Assets/Art/Map/ThungLungHoang/` gồm:

- `CumDiaHinhDuongDai.png`: đoạn đường dài.
- `CumDiaHinhDuongVua.png`: đoạn đường vừa.
- `CumDiaHinhDuongMoRong.png`: đoạn rộng.
- `CumDiaHinhDuongDocLen.png` và `CumDiaHinhDuongDocXuong.png`: hình dốc.
- `CumDiaHinhDuongBacThap.png`: hình bậc thấp.
- `RungXaThungLungHoang.png`: rừng làm hậu cảnh.
- `CayRungThungLungHoang.png`: các hình cây; dùng mũi tên cạnh ảnh trong cửa sổ Project để xem từng phần đã cắt.

Các cụm đường là ảnh PNG lớn, mỗi ảnh là một đoạn hoàn chỉnh. Hãy kéo chúng vào Scene như vật thể Sprite. Không cần đưa chúng vào Tile Palette. Tile Palette phù hợp hơn với bộ ô nhỏ vuông lặp lại.

## 2. Tạo scene luyện tập

1. Trong Unity, chọn **File > New Scene**, chọn mẫu 2D nếu Unity hỏi.
2. Chọn **File > Save As...** và lưu tại `Assets/Scenes/MapTapGhep.unity`.
3. Trong cửa sổ Hierarchy, nhấp phải và chọn **Create Empty**. Đặt tên `Map`. Tạo thêm các vật thể rỗng con tên `HauCanh`, `DiaHinh`, `VatTrangTri`, `TienCanh` để dễ tìm đồ.
4. Chọn Main Camera. Đặt **Projection** thành **Orthographic**. Có thể bắt đầu với **Size = 4.5**; tăng Size để nhìn rộng hơn. Camera thường nằm ở Z = -10, còn hình map ở Z = 0.
5. Bấm **Ctrl+S** để lưu. Scene này là bản tập; scene cũ vẫn còn nguyên.

## 3. Đặt hậu cảnh

1. Trong cửa sổ **Project**, mở `Assets > Art > Map > ThungLungHoang`. Các hàng ở đây là tài nguyên trong dự án, chưa phải vật thể được đặt trên map.
2. Nếu ảnh có mũi tên bên trái, bấm mũi tên để mở các hình con. Ví dụ, chọn `CayRung01` thay vì kéo cả tấm `CayRungThungLungHoang`.
3. Giữ chuột trái lên ảnh cần dùng, kéo sang vùng làm việc **Scene** ở giữa màn hình rồi thả. Hình sẽ xuất hiện trong Scene và Unity tự tạo một vật thể mới trong Hierarchy.
4. Chọn vật thể mới. Trong Inspector, ở mục **Sprite Renderer**, đặt **Order in Layer = -20** để rừng nằm sau địa hình.
5. Nhấn **W** để di chuyển rừng. Dùng **Ctrl+D** để nhân bản nếu cần phủ rộng hơn.

## 4. Ghép hình địa hình

1. Trong Project, tìm một ảnh `CumDiaHinh...`, kéo ảnh đó vào vùng **Scene** rồi chọn vật thể vừa tạo. Nếu không thấy ảnh, cuộn xuống trong danh sách thư mục.
2. Nhấn **W** để di chuyển, **R** để đổi kích thước. Hoặc sửa **Transform > Position** và **Transform > Scale** trong Inspector.
3. Đặt đoạn đầu thấp ở bên trái. Nối tiếp bằng đoạn dài/vừa; dùng đoạn bậc hoặc dốc để đổi cao độ; kết thúc bằng đoạn dẫn lên cao.
4. Nhấn **Ctrl+D** để nhân bản đoạn đang chọn. Di chuyển bản sao sang vị trí mới, rồi đổi hình qua cửa sổ Project nếu cần.
5. Căn mép ảnh sát nhau và cho hai đầu chồng nhẹ khoảng 0.2–0.5 đơn vị để tránh khe trong suốt. Nhìn ở **Game** để thấy khung camera; nếu hình quá to hoặc nhỏ, chỉnh Scale.
6. Ở **Sprite Renderer > Order in Layer**, đặt địa hình là `0`. Giá trị lớn hơn sẽ hiện phía trước: cây/bụi `5`, nhân vật `10`, cỏ tiền cảnh `20`.

Đừng cố làm map đẹp ngay. Trước hết ghép một lối đi rõ ràng từ trái sang phải, có vài chỗ bằng và vài chỗ đổi cao độ. Sau đó mới thêm cây, đá, cỏ và vật trang trí.

## 5. Tạo mặt đất để nhân vật đứng được

Làm va chạm thành vật thể riêng để dễ căn và sửa:

1. Trong Hierarchy, nhấp phải `DiaHinh`, chọn **Create Empty**. Đặt tên `VaCham_Duong_01`.
2. Đặt vị trí của vật thể ngay dưới mặt đường nhìn thấy.
3. Trong Inspector, bấm **Add Component**, tìm `Box Collider 2D` và chọn.
4. Chỉnh **Size X** cho dài bằng đoạn đường; chỉnh **Size Y** khoảng `0.4`–`0.6`. Đưa mép trên của khung collider khớp với mặt đường. Có thể bật **Gizmos** để nhìn viền va chạm trong Scene.
5. Tạo một collider riêng cho mỗi mặt đường bằng. Không thêm **Rigidbody 2D** vào mặt đất đứng yên.

Với đoạn bậc, đặt một Box Collider 2D cho từng mặt bậc. Với dốc, có thể bắt đầu bằng nhiều collider ngắn đặt lệch cao độ để tạo đường đi từng nấc. Cách này dễ căn hơn một collider dốc liền cho người mới.

## 6. Thêm cây và lưu

Kéo từng hình cây từ cửa sổ Project vào Scene, đặt chúng dưới `VatTrangTri`, rồi để **Order in Layer = 5**. Cây nằm trước nhân vật thì tăng Order; cây nằm sau nhân vật thì giảm Order. Không che mặt đường hoặc nhân vật ở đoạn cần nhảy.

Nhấn **Ctrl+S** thường xuyên. Dùng tab **Scene** để sắp xếp; tab **Game** chỉ cho thấy khung hình camera. Nút **Play** chỉ chạy thử va chạm khi scene đã có nhân vật và bộ điều khiển. Nếu chưa có, Game chỉ hiện hình map tĩnh.

## 7. Tách các hình trong một tấm PNG

1. Trong **Project**, chọn đúng tấm PNG. Ở Inspector đặt **Texture Type = Sprite (2D and UI)** và **Sprite Mode = Multiple**, rồi bấm **Apply** nếu Unity hiện nút đó.
2. Mở **Sprite Editor > Slice**. Với các hình rời có nền trong suốt, đặt **Type = Automatic**, **Pivot = Center**, **Pivot Unit Mode = Normalized**. Pivot chỉ là điểm neo khi đặt hình, không quyết định đường cắt.
3. Nếu đây là lần cắt đầu tiên, có thể để **Method = Delete Existing**. Tùy chọn này xóa các khung cắt cũ trong ảnh rồi tạo lại; nếu đã tự sửa các khung và muốn giữ chúng, không dùng tùy chọn này.
4. Bấm nút **Slice** trong bảng. Kiểm tra xem mỗi vật thể có khung riêng ôm quanh nó không. Nếu đúng, bấm **Apply** ở góc trên cửa sổ Sprite Editor để lưu.
5. Trở lại Project và bấm mũi tên cạnh PNG. Các sprite con sẽ hiện ra; kéo từng sprite con vào Scene.

**Automatic chỉ dò ranh giới dựa trên vùng trong suốt.** Nếu nền xanh đen của tấm ảnh là màu thật được lưu trong PNG, Unity sẽ xem cả nền và vật thể là một khối, nên không tự tách đúng. Hãy xem kênh Alpha để kiểm tra nền có trong suốt không. Nếu nền đặc, cần xóa nền bằng phần mềm sửa ảnh rồi lưu PNG có trong suốt; với hình xếp không theo ô đều, không dùng **Grid** để cắt.

## Phím hay dùng

| Phím | Tác dụng |
|---|---|
| W | Di chuyển vật thể |
| E | Xoay vật thể |
| R | Đổi kích thước |
| Ctrl+D | Nhân bản vật thể |
| Ctrl+S | Lưu scene |
| Con lăn chuột | Phóng to/thu nhỏ Scene |
| Giữ con lăn chuột | Kéo khung nhìn Scene |

