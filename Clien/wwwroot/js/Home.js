// Khởi tạo một mảng lưu trữ dữ liệu bài đăng
var postsData = [];
// Hàm để thêm một bình luận
function addComment() {
    // Lấy ra ô nhập liệu bình luận
    var commentInput = document.getElementById("comment-input");
    // Lấy chỉ số của bài đăng mà người dùng đang bình luận
    var postIndex = commentInput.dataset.postIndex;
    // Lấy nội dung bình luận từ ô nhập liệu
    var commentText = commentInput.value;
    // Kiểm tra xem nội dung bình luận có trống không
    if (commentText.trim() === "") {
        alert("Vui lòng nhập nội dung bình luận!");
        return;
    }
    // Tạo một đối tượng mới đại diện cho bình luận
    var newComment = {
        user: "Người bình luận",
        avatar: "https://th.bing.com/th?id=OIP.9Q-IHG9jdG7NBEXX3JGfxwHaIt&w=230&h=271&c=8&rs=1&qlt=90&o=6&dpr=1.3&pid=3.1&rm=2", // Đường dẫn đến ảnh đại diện của người bình luận
        text: commentText
    };
    // Thêm bình luận vào mảng bình luận của bài đăng tương ứng
    postsData[postIndex].comments.push(newComment);
    // Hiển thị lại danh sách bình luận
    renderComments(postIndex);
    // Xóa nội dung trong ô nhập liệu sau khi đã thêm bình luận
    commentInput.value = "";
}
// Hàm để thêm một bài đăng mới
function addPost() {
    // Lấy giá trị từ ô nhập liệu bài đăng và ô chọn tệp ảnh
    var postInput = document.getElementById("post-input").value;
    var imageInput = document.getElementById("image-input");
    // Lấy tệp ảnh từ ô chọn tệp
    var imageFile = imageInput.files[0];
    // Kiểm tra xem nội dung bài đăng có trống không
    if (postInput.trim() === "") {
        alert("Vui lòng nhập nội dung bài đăng!");
        return;
    }
    // Lấy ngày và giờ hiện tại
    var currentDate = new Date();
    var currentTime = currentDate.toLocaleString();
    // Tạo một đối tượng chứa thông tin của bài đăng mới
    var postData = {
        user: "Tên Tài khoản đăng ",
        avatar: "https://th.bing.com/th?id=OIP.9Q-IHG9jdG7NBEXX3JGfxwHaIt&w=230&h=271&c=8&rs=1&qlt=90&o=6&dpr=1.3&pid=3.1&rm=2", // Đường dẫn đến ảnh đại diện của người đăng
        time: currentTime,
        content: postInput,
        image: imageFile ? URL.createObjectURL(imageFile) : null,
        likes: 0,
        comments: []
    };
    // Thêm thông tin của bài đăng vào mảng chứa dữ liệu bài đăng
    postsData.push(postData);
    // Hiển thị lại danh sách bài đăng
    renderPosts();
    // Xóa nội dung trong ô nhập liệu bài đăng và ô chọn tệp ảnh sau khi đã thêm bài đăng
    document.getElementById("post-input").value = "";
    document.getElementById("image-input").value = "";
}
// Hàm để hiển thị danh sách bình luận của một bài đăng cụ thể
function renderComments(postIndex) {
    // Lấy đối tượng HTML của danh sách bình luận dựa trên ID "comments-list"
    var commentsList = document.getElementById("comments-list");
    commentsList.innerHTML = "";
    // Duyệt qua mỗi bình luận trong mảng bình luận của bài đăng tương ứng
    postsData[postIndex].comments.forEach(function (comment) {
        // Tạo một thẻ div mới để chứa thông tin của bình luận
        var commentDiv = document.createElement("div");
        // Đặt class cho thẻ div, giúp tùy chỉnh giao diện bình luận thông qua CSS
        commentDiv.className = "comment";
        // Thiết lập nội dung của thẻ div bằng HTML, bao gồm ảnh đại diện và nội dung bình luận
        commentDiv.innerHTML = `
        <div style=" display: flex;">
            <img src="${comment.avatar}" alt="Ảnh đại diện"  style="width: 50px;border-radius: 50%;height: 50px;">
            <p style="margin-left:5px ;"><strong>${comment.user}</strong> </p>
            </div>
            <p>${comment.text}</p>
        `;
        // Thêm thẻ div chứa thông tin bình luận vào danh sách bình luận
        commentsList.appendChild(commentDiv);
    });
}
// Hàm để hiển thị danh sách các bài đăng trên trang web
function renderPosts() {
    // Lấy đối tượng HTML của container chứa bài đăng dựa trên ID "posts-container"
    var postsContainer = document.getElementById("posts-container");
    // Xóa toàn bộ nội dung hiện tại của container chứa bài đăng
    postsContainer.innerHTML = "";
   // Duyệt qua mỗi bài đăng trong mảng dữ liệu bài đăng (postsData)
    postsData.forEach(function (postData, index) {
        // Tạo một thẻ div mới để chứa thông tin của mỗi bài đăng
        var postDiv = document.createElement("div");
        // Đặt class cho thẻ div, giúp tùy chỉnh giao diện bài đăng thông qua CSS
        postDiv.className = "post";
       // Tạo nội dung HTML cho mỗi bài đăng dựa trên dữ liệu từ postsData
        var postContent = `
        <div style="width: 780px; margin-left:100px; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2),
        0 6px 20px 0 rgba(0, 0, 0, 0.19); border-radius:10px">
        <div style="margin: 10px 10px 10px 10px  ">
            <div style="display:flex ;margin-top:10px;padding-top: 10px;" >
            <img src="${postData.avatar}" alt="Ảnh đại diện"  style="width: 60px;border-radius: 50%;height: 60px;">
            <p style="margin-left:5px ;"><strong>${postData.user}</strong></p>
            </div>
            <p style=" font-size:10px" ;>${postData.time}</p>
            <p >${postData.content}</p>
            <div  style="width: 780px;" >   
            ${postData.image ? `<img style="max-width: 100%; height: auto;" src="${postData.image}" alt="Bài đăng">` : '' }
            </div>
            <button class="like-button" onclick="likePost(${index})">
                <i class="fas fa-thumbs-up fa-xl"></i>  <span id="like-count-${index}">${postData.likes}</span>
            </button>
            <button class="comment-button " data-post-index="${index}" onclick="openCommentModal(${index})">
                <i class="fas fa-comment fa-xl"></i> Bình luận
            </button>
            <div class="comments-container"></div>
            </div>
            <div id="commentModal" class="modal">
        <div class="modal-content" style="background-color: #ddd;  border-radius: 8px;">
        <button id="btn-close">
            <span >&times;</span>
        </button>
          <div id="comment-form">
            <input
              type="text"
              id="comment-input"
              placeholder="Nhập bình luận..."
            />
            <button onclick="addComment()">Gửi</button>
          </div>
          <div id="comments-list" ></div>
        </div>
        </div>
      </div>
        `;
         // Thiết lập nội dung của thẻ div bằng HTML được tạo ra
        postDiv.innerHTML = postContent;
        // Thêm thẻ div chứa thông tin bài đăng vào container chứa bài đăng
        postsContainer.appendChild(postDiv);
    });
}
// Hàm để tăng số lượt thích của một bài đăng và cập nhật giao diện người dùng
function likePost(postIndex) {
     // Tăng số lượt thích của bài đăng tương ứng trong mảng dữ liệu bài đăng
    postsData[postIndex].likes++;
    // Lấy đối tượng hiển thị số lượt thích thông qua ID động và cập nhật nội dung
    var likeCountElement = document.getElementById(`like-count-${postIndex}`);
    likeCountElement.textContent = postsData[postIndex].likes;
}
// Hàm để mở modal bình luận và hiển thị danh sách bình luận của một bài đăng
function openCommentModal(postIndex) {
    // Lấy đối tượng modal bình luận dựa trên ID "commentModal" và hiển thị nó
    var modal = document.getElementById("commentModal");
    modal.style.display = "block";
    // Lấy đối tượng ô nhập liệu bình luận dựa trên ID "comment-input" và thiết lập thuộc tính dataset để lưu postIndex
    var commentInput = document.getElementById("comment-input");
    commentInput.dataset.postIndex = postIndex;
    // Hiển thị danh sách bình luận của bài đăng tương ứng
    renderComments(postIndex);
}

// Đóng modal khi người dùng nhấn vào nút đóng
// var closeButton = document.querySelector("#close");
// closeButton.onclick = function () {
//     // Lấy đối tượng modal dựa trên ID "commentModal" và ẩn nó khi người dùng nhấn nút đóng
//     var modal = document.getElementById("commentModal");
//     modal.style.display = "none";
// };

// Đóng modal khi người dùng nhấn bên ngoài modal
// window.onclick = function (event) {
//      // Lấy đối tượng modal dựa trên ID "commentModal"
//     var modal = document.getElementById("commentModal");
//     // Kiểm tra xem người dùng đã nhấn vào bên ngoài modal hay không
//     if (event.target === modal) {
//         // Nếu người dùng nhấn bên ngoài modal, ẩn modal đi
//         modal.style.display = "none";
//     }
// };



// const codeInput = document.getElementById("post-input");

// codeInput.addEventListener("input", (event) => {
//   const code = event.target.value;

//   // Sử dụng ký tự xuống dòng để ngắt dòng trong đoạn code
//   code = code.replace(/\n/g, "<br>");

//   // Cập nhật giá trị của input
//   codeInput.value = code;
// });

