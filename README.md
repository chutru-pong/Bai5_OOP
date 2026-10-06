
## 1. Giới thiệu mục tiêu
Thực hành và củng cố các kiến thức:
- **Mô hình hóa hệ phân cấp lớp bằng kế thừa (Inheritance).**
- **Sử dụng lớp trừu tượng (Abstract Class)** để biểu diễn đặc điểm chung.
- **Sử dụng Interface** để biểu diễn năng lực độc lập với cây kế thừa.
- **Sử dụng kết tập (Aggregation)** để xây dựng đối tượng từ nhiều đối tượng thành phần.
- **Cài đặt đa hình (Polymorphism)** thông qua lớp cơ sở và interface.
- **Kiểm tra tính hợp lệ của dữ liệu (Validation)** và xử lý các trường hợp ngoại lệ.
- **Giải thích bản chất quan hệ:** Kế thừa (*is-a*), Kết tập (*has-a*), hoặc Thực thi interface (*can-do*).

---

## 2. Mô tả nghiệp vụ
Một trường đại học cần xây dựng chương trình quản lý các thiết bị được sử dụng trong các phòng thực hành:
- Mỗi thiết bị có thông tin chung, nhưng cách tính chi phí bảo trì phụ thuộc vào từng loại thiết bị cụ thể.
- Hệ thống bước đầu quản lý 3 loại thiết bị:
  - Máy tính (`Computer`)
  - Máy in (`Printer`)
  - Máy chiếu (`Projector`)
- **Khả năng kết nối mạng:** Không phụ thuộc hoàn toàn vào loại thiết bị:
  - Máy tính luôn có thể kết nối mạng.
  - Một số máy in có thể kết nối mạng.
  - Máy chiếu (trong phiên bản hiện tại) không yêu cầu kết nối mạng.
- **Phòng thực hành (`LabRoom`):** Mỗi phòng có thể chứa nhiều thiết bị. Thiết bị vẫn có thể tồn tại độc lập trong hệ thống khi chưa được phân bổ vào phòng nào.

---

## 3. Yêu cầu thiết kế

### 3.1. Lớp trừu tượng `Device`
Lớp trừu tượng `Device` gồm ít nhất các thông tin:
- **Mã thiết bị** (`string DeviceId`)
- **Tên thiết bị** (`string DeviceName`)
- **Năm đưa vào sử dụng** (`int YearOfManufacture` / `UsageYear`)
- **Giá mua** (`decimal PurchasePrice`)
- **Trạng thái hoạt động** (`DeviceStatus Status`)

#### Quy tắc ràng buộc:
- Mã thiết bị: không được rỗng, **chỉ được gán một lần khi khởi tạo**.
- Giá mua: phải lớn hơn $0$.
- Năm đưa vào sử dụng: không được lớn hơn năm hiện tại.
- Cung cấp phương thức trừu tượng: `decimal CalculateAnnualMaintenanceCost()` (tính chi phí bảo trì dự kiến trong một năm).
- Ghi đè (override) phương thức biểu diễn thông tin thiết bị dạng chuỗi (`ToString()`).

```csharp
public enum DeviceStatus
{
    Active,
    UnderMaintenance,
    Retired
}
```

---

### 3.2. Các lớp dẫn xuất

#### a. Lớp `Computer` (kế thừa `Device`)
- **Thuộc tính bổ sung:**
  - Dung lượng RAM (`int RamCapacityGB`)
  - Loại bộ xử lý / CPU (`string CpuType`)
  - Có GPU rời hay không (`bool HasDiscreteGpu`)
- **Chi phí bảo trì dự kiến / năm:**
  - $5\%$ giá mua cơ bản.
  - Cộng thêm $2\%$ giá mua nếu có GPU rời.
  - Cộng thêm $1\%$ giá mua nếu thiết bị đã sử dụng trên $5$ năm (tính từ năm đưa vào sử dụng đến năm hiện tại).

#### b. Lớp `Printer` (kế thừa `Device`)
- **Thuộc tính bổ sung:**
  - Loại máy in: laser hoặc phun (`string PrinterType` hoặc enum).
  - Có in màu hay không (`bool IsColorPrinter`).
  - Số trang đã in (`int PageCount`).
  - Có hỗ trợ mạng hay không (`bool HasNetworkSupport`).
- **Chi phí bảo trì dự kiến / năm:**
  - $4\%$ giá mua cơ bản.
  - Cộng thêm $500.000\text{ VNĐ}$ nếu số trang đã in $> 100.000$.
  - Cộng thêm $300.000\text{ VNĐ}$ nếu là máy in màu.

#### c. Lớp `Projector` (kế thừa `Device`)
- **Thuộc tính bổ sung:**
  - Độ sáng, tính bằng lumen (`int Lumens`).
  - Số giờ đã sử dụng bóng đèn (`int LampHoursUsed`).
  - *(Được bổ sung thuộc tính cần thiết khác nếu giải thích hợp lý).*
- **Chi phí bảo trì dự kiến / năm:**
  - $3\%$ giá mua cơ bản.
  - Cộng thêm $1.500.000\text{ VNĐ}$ nếu bóng đèn đã sử dụng trên $3.000$ giờ.

---

## 4. Yêu cầu sử dụng Interface

### 4.1. Interface `INetworkable`
```csharp
public interface INetworkable
{
    string IpAddress { get; }
    bool IsConnected { get; }
    void Connect(string ipAddress);
    void Disconnect();
}
```

### 4.2. Ràng buộc và thiết kế
- `Computer` thực thi `INetworkable`.
- `Printer` chỉ thực thi `INetworkable` nếu chọn mô hình mọi máy in đều có kết nối mạng.
- **Đề xuất mô hình cho "chỉ một số máy in có mạng":**
  - **Phương án 1:** Tạo lớp con `NetworkPrinter` kế thừa từ `Printer` và thực thi `INetworkable`.
  - **Phương án 2:** Dùng một thành phần network module (kết tập) bên trong `Printer`.
  - **Yêu cầu:** Sinh viên lựa chọn một phương án và viết bài giải thích trade-off ($3 - 5$ câu).

#### Điều kiện kết nối mạng:
- Địa chỉ IP không được rỗng / null.
- Không cho phép kết nối lại khi thiết bị đang ở trạng thái đã kết nối (`IsConnected == true`).
- Sau khi ngắt kết nối (`Disconnect()`): `IsConnected` phải chuyển thành `false` và không còn lưu giữ địa chỉ IP đang hoạt động (IP gán về `string.Empty` hoặc `null`).

---

## 5. Yêu cầu sử dụng Kết tập (Aggregation)

### 5.1. Lớp `LabRoom`
- **Thuộc tính:**
  - Mã phòng (`string RoomId`)
  - Tên phòng (`string RoomName`)
  - Sức chứa (`int Capacity`)
  - Danh sách thiết bị (`List<Device> Devices`)

- **Phương thức tối thiểu:**
  - `void AddDevice(Device device)`: Thêm thiết bị vào phòng.
  - `bool RemoveDevice(string deviceId)`: Xóa thiết bị theo mã.
  - `Device? FindDevice(string deviceId)`: Tìm kiếm thiết bị theo mã.
  - `decimal CalculateAnnualMaintenanceCost()`: Tính tổng chi phí bảo trì phòng.
  - `List<Device> GetDevicesRequiringMaintenance()`: Lấy danh sách thiết bị cần bảo dưỡng.

### 5.2. Quy tắc nghiệp vụ trong `LabRoom`
1. Không thêm hai thiết bị có cùng mã vào một phòng.
2. Không chấp nhận đối tượng thiết bị `null`.
3. `CalculateAnnualMaintenanceCost()` **bắt buộc dùng tính đa hình** (gọi `device.CalculateAnnualMaintenanceCost()`), tuyệt đối không kiểm tra kiểu bằng `if (device is Computer)` hay `switch-case`.
4. `GetDevicesRequiringMaintenance()` trả về các thiết bị thỏa mãn một trong hai điều kiện:
   - Đang ở trạng thái `DeviceStatus.UnderMaintenance`; **hoặc**
   - Có thời gian sử dụng trên $5$ năm.

### 5.3. Câu hỏi thiết kế
> **Câu hỏi:** Quan hệ giữa `LabRoom` và `Device` là kết tập (Aggregation) hay hợp thành (Composition)?  
> **Giải thích gợi ý:** Dựa trên vòng đời đối tượng. Khi phòng thực hành (`LabRoom`) bị xóa hoặc giải thể, các thiết bị (`Device`) vẫn tồn tại độc lập trong hệ thống để điều chuyển sang phòng khác $\rightarrow$ Đây là quan hệ **Kết tập (Aggregation)**.

---

## 6. Yêu cầu kiểm thử tối thiểu (Main / Test Case)

Chương trình chính (`Program.cs`) phải khởi tạo:
1. **$2$ máy tính:** trong đó có ít nhất $1$ máy có GPU rời.
2. **$2$ máy in:** trong đó có ít nhất $1$ máy đã in trên $100.000$ trang.
3. **$1$ máy chiếu:** có số giờ sử dụng bóng đèn $> 3.000$ giờ.
4. **$2$ phòng thực hành.**

Thực hiện lần lượt các thao tác kiểm thử:
- [x] **1. Thêm thiết bị vào phòng:** Phân bổ các thiết bị vừa tạo vào 2 phòng.
- [x] **2. Bắt lỗi trùng mã:** Thử thêm một thiết bị trùng mã vào phòng và xử lý thông báo.
- [x] **3. In danh sách:** Hiển thị toàn bộ thiết bị trong từng phòng thực hành.
- [x] **4. Tính tổng chi phí bảo trì:** In ra tổng chi phí bảo trì của từng phòng.
- [x] **5. Lọc bảo trì:** Liệt kê các thiết bị cần bảo dưỡng theo tiêu chí quy định.
- [x] **6. Kết nối mạng:** Gọi kết nối mạng cho các đối tượng có thực thi `INetworkable`.
- [x] **7. Đa hình Interface:** Duyệt và kiểm tra trạng thái các thiết bị mạng thông qua kiểu `INetworkable` mà không phụ thuộc vào lớp cụ thể.

## 7. Biểu đồ lớp 
<img width="2130" height="971" alt="image" src="https://github.com/user-attachments/assets/028dec17-2cd1-4a30-a131-1bd45ee87fe4" />

