document.getElementById("login-tab").addEventListener("click", function(event) {
    event.preventDefault();
    document.getElementById("login-tab").classList.add("active");
    document.getElementById("register-tab").classList.remove("active");
    document.getElementById("login-form").classList.add("active");
    document.getElementById("register-form").classList.remove("active");
});

document.getElementById("register-tab").addEventListener("click", function(event) {
    event.preventDefault();
    document.getElementById("register-tab").classList.add("active");
    document.getElementById("login-tab").classList.remove("active");
    document.getElementById("register-form").classList.add("active");
    document.getElementById("login-form").classList.remove("active");
});
// thông báo cho phần đăng nhập 
document.getElementById("login-form-fields").addEventListener("submit", function(event) {
    event.preventDefault();

    var username = document.getElementById("login_username").value;
    var password = document.getElementById("login_password").value;
    var errorMessage = document.getElementById("error-messgage");
    if(username.trim() === "" || password.trim() === ""){
        document.getElementById("error-message").textContent ="Tên đăng nhập và mật khẩu không được để trống";
       
    } else{
         // Kiểm tra tên đăng nhập và mật khẩu
    if (username === "user" && password === "pass") {
        // Đăng nhập thành công
        document.getElementById("error-message").textContent = "Đăng nhập thành công!";
    } else {
        // Hiển thị thông báo lỗi
        document.getElementById("error-message").textContent = "Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng thử lại.";
    }
    }

});

//thông báo cho phần đăng ký 
document.getElementById("register-form-fields").addEventListener("submit", function(event) {
    event.preventDefault();
    var username = document.getElementById("register-username").value;
    var email = document.getElementById("register-email").value;
    var password = document.getElementById("register-password").value;
    var confirmPassword = document.getElementById("confirm-password").value;
    var errorMessage = document.getElementById("error-message");

    // Kiểm tra tính hợp lệ của email
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var isValidEmail = emailRegex.test(email);

    // Kiểm tra mật khẩu và xác nhận mật khẩu trùng khớp
    var isPasswordMatch = password === confirmPassword;

    // Kiểm tra tính hợp lệ của tên đăng nhập và mật khẩu
    if (username.trim() === "" || email.trim() === "" || password.trim() === "" || confirmPassword.trim() === "") {
        document.getElementById("error-message").textContent = "Vui lòng điền đầy đủ thông tin.";
    } else if (!isValidEmail) {
        document.getElementById("error-message").textContent = "Email không hợp lệ.";
    } else if (!isPasswordMatch) {
        document.getElementById("error-message").textContent = "Mật khẩu và xác nhận mật khẩu không trùng khớp.";
    } else {
        document.getElementById("error-message").textContent = "Đăng ký thành công!";
    }
});
