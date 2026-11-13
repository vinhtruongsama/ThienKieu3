# Danh sách ảnh và tài nguyên cần đặt

Ghi ảnh/tài nguyên còn thiếu, đang chờ tạo hoặc chờ duyệt. Dùng tên tiếng Việt không dấu. Chỉ tích hợp phiên bản đã duyệt; tiếp tục việc độc lập trong lúc chờ.

Mỗi tài nguyên có tên riêng, nơi sử dụng, mô tả và trạng thái. Danh sách được chia theo nhóm để dễ đọc và cập nhật.

## Phong cách ảnh

Áp dụng nguyên văn phong cách sau cho mọi ảnh tạo mới hoặc chỉnh sửa:

Whimsical hand-drawn storybook illustration style, stylized cartoon aesthetic, soft painterly textures with visible brush strokes, slightly rough sketchy ink linework, subtle shading, soft lighting, warm and harmonious color palette, playful but balanced proportions, simple yet charming shapes, magical and cozy atmosphere, expressive character design, crisp readable silhouettes, high detail, consistent artistic direction.

Lưu ý khi vẽ trang phục nhân vật: kiểm tra vạt áo nối liền với thân áo khi bị dây lưng che hoặc siết; giữ đúng cúc áo, hoa văn trên áo, dây buộc đầu và dây buộc sau lưng quần. Các lớp vải phải chồng tự nhiên, không tạo khe hở khiến vạt áo trông như bị rời ra.

## Bản đồ gameplay

### `ThungLungHoang`

- **Dùng cho:** Thung Lũng Hoang.
- **Thiết kế theo:** [Kế hoạch MAP-001](KeHoachMAP001.md). Rừng dày, ánh sáng yếu, không thấy bầu trời; địa hình đi từ thấp lên cao. Đường thoát làm bằng bụi vàng sáng.
- **Lớp rừng xa — `RungXaThungLungHoang`:** Nhiều lớp cây ở độ cao và khoảng cách khác nhau; phủ kín khung camera, nối lặp được, không có trời.
- **Cây trung cảnh — `CayRungThungLungHoang`:** Thân cây cao, mảnh và tán dày, tách từng cây/cụm cây để sắp xếp riêng; cành lá có thể làm lay nhẹ.
- **Bộ tiền cảnh — `TienCanhThungLungHoang`:** Ba cụm đá phủ rêu, lá bản lớn, dương xỉ, cỏ và hoa hồng; tách nền trong suốt để xếp riêng. [Tấm tổng](../.Generated/Map/ThungLungHoang/TienCanhThungLungHoangMaster.png), [cụm trái](../.Generated/Map/ThungLungHoang/TienCanhCumDaLa01Trai.png), [cụm thấp giữa](../.Generated/Map/ThungLungHoang/TienCanhCumDaLa02Thap.png), [cụm phải](../.Generated/Map/ThungLungHoang/TienCanhCumDaLa03Phai.png). **Trạng thái:** Chờ người dùng duyệt; chưa tích hợp vào Unity.
- **Bộ tiền cảnh mép dưới bổ sung — `TienCanhThungLungHoangBoSung`:** Sáu hình tách rời gồm lá bản lớn, rễ cây, đá phủ rêu, dương xỉ/cỏ, dây leo và bụi hoa vàng; nền trong suốt để cắt thành Sprite riêng và đặt sát mép dưới map. [Tấm tổng 1536×1024](../.Generated/Map/ThungLungHoang/TienCanhThungLungHoangBoSung.png). **Trạng thái:** Chờ người dùng duyệt; chưa cắt và chưa tích hợp vào Unity.
- **Bộ vật cảnh và dấu vết — `VatCanhVaDauVetThungLungHoang`:** Chín hình nền trong suốt gồm dấu chân Khỉ Đột, vết cào đá, đá nứt, trụ đá khắc mòn, thân cây gãy, cửa hang, thác nhỏ, đá vụn và sương có đom đóm. [Tấm tổng 1536×1024](../.Generated/Map/ThungLungHoang/VatCanhVaDauVetThungLungHoang.png). **Trạng thái:** Chờ người dùng duyệt; chưa cắt và chưa tích hợp vào Unity.
- **Bộ tảng đá rời — `DaThungLungHoang`:** Sáu tảng đá có dáng khác nhau, chỉ có đá, không kèm đất hay rêu; nền trong suốt. [Tấm tổng](../.Generated/Map/ThungLungHoang/DaThungLungHoangMaster.png), [đá tròn lớn](../.Generated/Map/ThungLungHoang/DaRoi01TronLon.png), [đá góc dài](../.Generated/Map/ThungLungHoang/DaRoi02GocDai.png), [đá dẹt thấp](../.Generated/Map/ThungLungHoang/DaRoi03DepThap.png), [đá xếp lớp](../.Generated/Map/ThungLungHoang/DaRoi04XepLop.png), [đá tròn nhỏ](../.Generated/Map/ThungLungHoang/DaRoi05TronNho.png), [đá khối góc](../.Generated/Map/ThungLungHoang/DaRoi06KhoiGoc.png). **Trạng thái:** Chờ người dùng duyệt; chưa tích hợp Unity.
- **Tường đá nứt vỡ, bào mòn — `TuongDaNutVoBaoMonThungLungHoang`:** Một đoạn tường ghép từ các khối đá lớn, có khe nứt sâu, mẻ cạnh và vết phong hóa; PNG nền trong suốt, không có đất. [Tường đá](../.Generated/Map/ThungLungHoang/TuongDaNutVoBaoMonThungLungHoang.png). **Trạng thái:** Chờ người dùng duyệt; chưa tích hợp Unity.
- **Bộ địa hình — `TileDiaHinhThungLungHoang`:** Các miếng đất, đá, cỏ, mặt nền, vách, dốc, bậc và lớp đất/đá đỡ bên dưới. Tạo miếng chữ nhật có thể ghép liền, dùng lại ở nhiều đoạn và không hở mép.
- **Bộ 4 cụm địa hình theo concept:** Bốn khối được vẽ chung trong một master để giữ cùng góc nhìn và chất liệu, theo tỷ lệ gần với bốn vùng khoanh đỏ. Mỗi mặt cỏ rộng, dễ nhìn, phẳng và ngang liên tục; phần đá bên dưới được tạo hình tự nhiên. Không có cảnh nền, tiền cảnh, quái vật, nhân vật hoặc cây lớn; mặt đi không có vật cản.
  - [Master chung 1774×887](../.Generated/Map/ThungLungHoang/BanGhep4KhoiDiaHinhMaster.png).
  - [KhoiDiaHinh01BenTrai.png](../.Generated/Map/ThungLungHoang/KhoiDiaHinh01BenTrai.png) — khối hẹp đứng bên trái (1075×1388).
  - [KhoiDiaHinh02ThungLung.png](../.Generated/Map/ThungLungHoang/KhoiDiaHinh02ThungLung.png) — khối thấp, dài ở giữa (2141×538).
  - [KhoiDiaHinh03BacGiua.png](../.Generated/Map/ThungLungHoang/KhoiDiaHinh03BacGiua.png) — bậc giữa, rộng vừa (1382×1103).
  - [KhoiDiaHinh04VachCao.png](../.Generated/Map/ThungLungHoang/KhoiDiaHinh04VachCao.png) — vách hẹp và cao bên phải (1007×1478).
  - **Trạng thái:** Đã cắt từ master và phóng nét; đã xem lại đủ bốn khối. Chờ người dùng duyệt, chưa tích hợp vào Unity.
- **Thanh đất dài bo góc — `ThanhDatDaiBoGoc`:** Ba biến thể được vẽ chung một master để giữ đồng bộ. Mặt cỏ đi ngang phẳng và liên tục; hai đầu gần vuông, cạnh ngắn gần đứng, góc bo nhẹ; phần đá bên dưới giữ hình tự nhiên. PNG nền trong suốt.
  - [Master chung 2160×1006](../.Generated/Map/ThungLungHoang/MasterThanhDatDaiBoGoc.png).
  - [Thanh 01](../.Generated/Map/ThungLungHoang/ThanhDatDaiBoGoc01.png) (2158×327).
  - [Thanh 02](../.Generated/Map/ThungLungHoang/ThanhDatDaiBoGoc02.png) (2160×264).
  - [Thanh 03](../.Generated/Map/ThungLungHoang/ThanhDatDaiBoGoc03.png) (2149×315).
  - **Trạng thái:** Đã cắt từ master, phóng nét bằng model và kiểm tra cả ba ảnh; chờ người dùng duyệt, chưa tích hợp vào Unity.
- **Bản viền vuông bo nhẹ — `ThanhDatDaiBoGocVuongCanh`:** Tạo thêm ba biến thể chung một master theo phản hồi mới. Mặt cỏ vẫn ngang tuyệt đối; đầu thanh gần vuông, góc bo vừa nhẹ, không thu tròn; mặt đá giữ chất liệu và màu sắc cùng bộ trước.
  - [Master 2048×768](../.Generated/Map/ThungLungHoang/ThanhDatDaiBoGocVuongCanhMaster.png).
  - [Thanh 01 2120×240](../.Generated/Map/ThungLungHoang/ThanhDatDaiBoGocVuongCanh01.png), [thanh 02 2126×230](../.Generated/Map/ThungLungHoang/ThanhDatDaiBoGocVuongCanh02.png), [thanh 03 2130×230](../.Generated/Map/ThungLungHoang/ThanhDatDaiBoGocVuongCanh03.png).
  - **Trạng thái:** PNG nền trong suốt, đã cắt, phóng nét bằng model và xem lại; ba ảnh đang được ghép thử vào scene `ThungLungHoang` để duyệt bố cục. Chưa chốt duyệt mỹ thuật.
- **Mẫu nền đất thay thế:** Ba phương án texture ít vụn hơn nền đang dùng: [DatRungReDa01.png](../.Generated/Map/ThungLungHoang/DatRungReDa01.png), [DatRungPhanTang.png](../.Generated/Map/ThungLungHoang/DatRungPhanTang.png), [DatRungReDa02.png](../.Generated/Map/ThungLungHoang/DatRungReDa02.png). Chờ người dùng chọn/duyệt; chưa đưa vào Unity.
- **Bản nháp cũ — `DuongDatCoThua`:** Bản đơn góc nhìn thấp; đã được thay bằng các cụm rời theo phản hồi mới, không dùng khi dựng map.
- **Bộ cụm đường đất — `CumDiaHinhDuong`:** Các ảnh PNG riêng, góc nhìn chếch từ trên để thấy mặt đường; cỏ thưa ở hai mép, nền đất dưới dịu. Dáng ngang gần chữ nhật, hai đầu cắt thẳng, mép ngoài bo nhẹ. Đã ghép vào scene `ThungLungHoang` theo yêu cầu thay địa hình ngày 2026-11-02; bản dùng trong Unity ở `Assets/Art/Map/ThungLungHoang/`. Chờ duyệt mỹ thuật cuối.
  - [CumDiaHinhDuongDai.png](../.Generated/Map/ThungLungHoang/CumDiaHinhDuongDai.png)
  - [CumDiaHinhDuongVua.png](../.Generated/Map/ThungLungHoang/CumDiaHinhDuongVua.png)
  - [CumDiaHinhDuongMoRong.png](../.Generated/Map/ThungLungHoang/CumDiaHinhDuongMoRong.png)
  - [CumDiaHinhDuongDocLen.png](../.Generated/Map/ThungLungHoang/CumDiaHinhDuongDocLen.png) — đoạn dốc lên nhẹ.
  - [CumDiaHinhDuongBacThap.png](../.Generated/Map/ThungLungHoang/CumDiaHinhDuongBacThap.png) — hai bậc thấp, mặt chạy rộng.
  - [CumDiaHinhDuongDocXuong.png](../.Generated/Map/ThungLungHoang/CumDiaHinhDuongDocXuong.png) — đoạn dốc xuống nhẹ.
- **Đường dẫn — `BuiVangDanDuong`:** Cụm bụi vàng phát sáng, nền trong suốt, có nhiều đoạn nối để rải liên tục trên đường chạy.
- **Đá văng — `HieuUngVoDa`:** Đất bật lên, đá vỡ thành mảnh và bụi đất trên nền trong suốt, dùng khi Khỉ đột nhảy vồ hoặc tiếp đất.
- **Nơi lưu:** `AI/.Generated/Map/ThungLungHoang/`.
- **Đã tạo:** `RungXaThungLungHoang.png`, `CayRungThungLungHoang.png`, `BeMatCoDat.png`, `BoNenDatDa.png`, `DuongDatCoThua.png`, `CumDiaHinhDuongDai.png`, `CumDiaHinhDuongVua.png`, `CumDiaHinhDuongMoRong.png`, `CumDiaHinhDuongDocLen.png`, `CumDiaHinhDuongBacThap.png`, `CumDiaHinhDuongDocXuong.png`, `BuiVangDanDuong.png`, `HieuUngVoDa.png`, `HieuUngBangDong.png`, `BanGhep4KhoiDiaHinhMaster.png`, `KhoiDiaHinh01BenTrai.png`, `KhoiDiaHinh02ThungLung.png`, `KhoiDiaHinh03BacGiua.png`, `KhoiDiaHinh04VachCao.png`, `MasterThanhDatDaiBoGoc.png`, `ThanhDatDaiBoGoc01.png`, `ThanhDatDaiBoGoc02.png`, `ThanhDatDaiBoGoc03.png`.
- **Đã ghép thử:** [Scene Thung Lũng Hoang](../../Assets/Scenes/ThungLungHoang.unity); ảnh gốc ở thư mục trên, bản Unity tại `Assets/Art/Map/ThungLungHoang/`. Scene dùng các cụm đường dài, vừa, mở rộng, dốc lên, bậc thấp và dốc xuống.
- **Trạng thái:** Đã dựng bản thử; chờ duyệt mỹ thuật và tiếp tục hoàn thiện địa hình/hoạt ảnh cảnh vật.

### `KhuDatSauNha`

- **Dùng cho:** Khu Đất Sau Nhà.
- **Cần tạo:** Các lớp cảnh, nền phẳng, cỏ dại, cây lớn, hồ và vật nuôi dưới nước.
- **Đã dựng thử:** Scene khung xám tại `Assets/Scenes/KhuDatSauNha.unity`; chưa tích hợp ảnh cảnh quan được duyệt.
- **Trạng thái:** Ảnh cảnh quan chờ tạo và duyệt.

### `NhaChinh`

- **Dùng cho:** Nhà Chính.
- **Cần tạo:** Các lớp cảnh thanh bình, nhà mái gỗ và cây xoài.
- **Trạng thái:** Chờ tạo và duyệt.

## Quái

### `BaQuaiLinhTruong`

- **Dùng cho:** Thung Lũng Hoang.
- **Cần tạo:** Một mẫu Khỉ Đột dùng cho ba nhân vật cùng loài, kèm hoạt ảnh xuất hiện, gầm, vỗ ngực và cú nhảy vồ riêng. Ba con xuất hiện cùng lúc ở cuối map sau khi người chơi bắt đầu di chuyển 1 giây; chúng gầm xong mới đuổi theo, giữ khoảng đứng riêng, đổi thứ tự đội hình và không cản người chơi. Chỉ cú vồ đáp sát mới gây choáng. Tạo hình phải đọc rõ ở góc nhìn đi cảnh cuộn ngang.
- **Mẫu được dùng:** Ảnh `KhiDotNghiengPhai.png` đã duyệt; tạo mẫu dùng trong scene tại `BaQuaiLinhTruongMaster.png`.
- **Hoạt ảnh đã có:** `idle`, `jump`, `hurt`; controller Unity tại `Assets/Art/Quai/Animations/Gorilla.controller`.
- **Còn thiếu:** Hoạt ảnh riêng cho xuất hiện, gầm/vỗ ngực và cú vồ. Bản thử hiện dùng clip `jump` có sẵn, nhịp co giãn và tiếng gầm tổng hợp khi chạy. Sáu lần tạo clip `attack` bằng HeyGen trước đây đều lỗi trước khi trả video; chưa có clip để ghép.
- **Lưu ý:** Quái không thể bị hạ gục. Tắt va chạm thân quái với nhau và với người chơi; AI giữ khoảng cách khi đứng/rượt để không chồng lên nhau. Chỉ một con nhảy vồ mỗi lượt; nếu đáp sát, người chơi bị đẩy lùi và choáng, không chết.
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

### `NamNguyenMasterTest`

- **Dùng cho:** Mẫu Master nhân vật đầu tiên theo thời Nguyễn.
- **Mô tả:** Nam dân thường Bắc Bộ, Hà Nội và vùng ven khoảng 1908–1909; gương mặt và dáng người tham chiếu ảnh người dùng. Trang phục thử gồm áo ngũ thân màu trầm, khăn vấn và quần rộng; không dùng lễ phục quan lại hay họa tiết fantasy. Đây là lựa chọn vùng và tầng lớp để thử tạo hình, chưa chốt vai trò trong truyện.
- **Tệp:** [NhanVatNamNguyenMasterTest.png](../.Generated/NhanVat/NhanVatNamNguyenMasterTest.png), chân dung 1024×1536, tỉ lệ 2:3.
- **Căn cứ hình ảnh:** [Bảo tàng Lịch sử Quốc gia — Kỹ thuật của người An Nam](https://vnmh.com.vn/en/Articles/3220/12663/mechanics-and-crafts-of-the-annamites.html) và [bản tư liệu Henri Oger tại Thư viện Quốc hội Hoa Kỳ](https://www.loc.gov/resource/gdcwdl.wdl_14383/?sp=16); đối chiếu áo ngũ thân nam thời Nguyễn qua [VietnamPlus](https://www.vietnamplus.vn/chiem-nguong-loat-co-phuc-trong-lich-su-nhan-ngay-di-san-van-hoa-vn-post831851.vnp).
- **Trạng thái:** Bản thử chờ người dùng duyệt; chưa tích hợp vào Unity.

### `NguoiChoiNam`

- **Dùng cho:** Nhân vật người chơi tại MAP-001.
- **Mô tả:** Nam, gương mặt anh tú, dáng cao và ốm theo yêu cầu người dùng.
- **Tệp:** [NguoiChoiNam.png](../.Generated/NhanVat/NguoiChoiNam.png); hoạt ảnh gồm `idle`, `run`, `jump`, `hurt`.
- **Đã tích hợp:** `Assets/Art/NhanVat/Animations/Player.controller` và các clip/atlas cùng thư mục.
- **Bản khung ngang đã tạo trước:** [NguoiChoiNamMaster1080x720.png](../.Generated/NhanVat/NguoiChoiNamMaster1080x720.png) (1080×720, tỉ lệ 3:2) và [NguoiChoiNamMaster1280x720.png](../.Generated/NhanVat/NguoiChoiNamMaster1280x720.png) (1280×720, tỉ lệ 16:9). Nền trong suốt, nhân vật cao 200 px.
- **Bản chân dung sửa mới:** [NguoiChoiNamMasterChanDung.png](../.Generated/NhanVat/NguoiChoiNamMasterChanDung.png), 1024×1536, tỉ lệ 2:3. Đã sửa vạt áo liền với thân áo dưới dây lưng; giữ rõ cúc, hoa văn tay áo và dây buộc.
- **Trạng thái:** Bản chân dung sửa mới chờ người dùng duyệt; chưa tích hợp vào Unity.

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
- **Mẫu thử:** Dùng hình tròn mặc định của Unity với mắt/miệng tách rời trong scene thử MAP-002; chỉ để kiểm tra bố cục và thời điểm xuất hiện.
- **Trạng thái:** Chờ tạo hình chính thức và duyệt.

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
