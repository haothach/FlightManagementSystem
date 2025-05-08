
<body>
    <h1>Flight Management System - Hướng Dẫn Cài Đặt</h1>
    <h2>1. Yêu Cầu Hệ Thống</h2>
    <ul>
        <li>Hệ điều hành: Windows 10 hoặc mới hơn</li>
        <li>.NET Framework 4.7.2 hoặc mới hơn</li>
        <li>SQL Server Express hoặc phiên bản đầy đủ</li>
        <li>Visual Studio (khuyến nghị sử dụng bản mới nhất)</li>
    </ul>
    <h2>2. Cài Đặt và Chạy Dự Án</h2>
    <h3>Bước 1: Clone Repository</h3>
    <pre><code>git clone https://github.com/haothach/FlightManagementSystem.git</code></pre>
    <h3>Bước 2: Cấu Hình Database</h3>
    <p>Import file SQL vào SQL Server:</p>
    <pre><code>1. Mở SQL Server Management Studio (SSMS)
2. Kết nối đến SQL Server
3. Chạy file <code>QLChuyenBay.sql</code> để tạo database</code></pre>
    <h3>Bước 3: Cấu Hình Chuỗi Kết Nối</h3>
    <p>Chỉnh sửa file cấu hình trong C#:</p>
    <pre><code>"string cnStr = "Data Source=." +
                ";Initial Catalog=FlightManagement;Integrated Security=True";</code></pre>
    <h3>Bước 4: Chạy Ứng Dụng</h3>
    <pre><code>1. Mở Visual Studio
2. Mở project <code>FlightManagement.sln</code>
3. Nhấn F5 để chạy chương trình</code></pre>
    <h2>3. Chức Năng Mở Rộng</h2>
    <ul>
        <li>📧 <strong>Gửi Mail:</strong> Tự động gửi email xác nhận vé, thông báo thay đổi chuyến bay.</li>
        <li>📊 <strong>Thống Kê & Biểu Đồ:</strong> Vẽ biểu đồ số lượng vé bán, doanh thu theo thời gian.</li>
        <li>🧾 <strong>Xuất Báo Cáo:</strong> Xuất báo cáo ra file PDF hoặc Excel.</li>
        <li>🤖 <strong>Chatbot:</strong> Tích hợp chatbot đơn giản hỗ trợ người dùng hỏi đáp nhanh.</li>
    </ul>
    <h2>4. Liên Hệ</h2>
    <p>Nếu bạn có bất kỳ câu hỏi hoặc cần hỗ trợ, vui lòng liên hệ qua email: <a href="mailto:haonhut.thach@gmail.com">haonhut.thach@gmail.com</a></p>
</body>
</html>
