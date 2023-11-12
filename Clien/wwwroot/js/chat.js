const friendList = document.getElementById("friend-list");
const chatMessages = document.getElementById("chat-messages");
const messageInput = document.getElementById("message-input");

// Danh sách bạn bè (có thể thay đổi hoặc lấy từ server)
const friends = [
    { name: "Friend 1", avatar: "https://th.bing.com/th/id/R.d159349a26a24ad2cb63130b003c7265?rik=BLyTcwJZhew2Bw&riu=http%3a%2f%2fvergewiki.com%2fuploads%2fbiography%2f2019%2f7%2f6%2fAnh+Do-1562430469201.jpg&ehk=0D%2fXuZZBbZNyHhnUtZXg%2bVUZgSZLh0ICL2SnvVkbzn0%3d&risl=&pid=ImgRaw&r=0" },
    { name: "Friend 2", avatar: "https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2021/5/26/913299/Ngan-Ha25.jpg" },
    { name: "Friend 3", avatar: "https://www.kkday.com/vi/blog/wp-content/uploads/chup-anh-dep-bang-dien-thoai-25.jpg" }
];

/// Hiển thị danh sách bạn bè với tên và ảnh đại diện
friends.forEach(friend => {
    const listItem = document.createElement("li");
    listItem.className ="list-friend"
    listItem.dataset.name = friend.name;
    listItem.dataset.avatar = friend.avatar;
    listItem.addEventListener("click", () => startChat(friend.name, friend.avatar));

    const avatarImg = document.createElement("img");
    avatarImg.src = friend.avatar;
    avatarImg.alt = friend.name;
    avatarImg.className = "avatar";
    listItem.appendChild(avatarImg);

    const friendName = document.createElement("span");
    friendName.textContent = friend.name;
    listItem.appendChild(friendName);

    friendList.appendChild(listItem);
});
// Hàm bắt đầu chat với bạn bè được chọn và hiển thị tên và ảnh đại diện
function startChat(name, avatar) {
    // Hiển thị tên bạn bè và ảnh đại diện ở phần tiêu đề
     const friendAvatar = document.createElement("img");
    document.querySelector(".img h2").textContent = ` ${name}`;
   
    friendAvatar.className ="avatar"
    friendAvatar.src = avatar;
    friendAvatar.alt = name;
    document.querySelector(".chat-header").appendChild(friendAvatar);
    // Xử lý logic nhắn tin ở đây
    // Để đơn giản, chúng ta chỉ hiển thị tin nhắn mẫu
    chatMessages.innerHTML = `<div class="message-container message-received"><strong>${name}:</strong> Hello there!</div>`;
}
// Hàm gửi tin nhắn
function sendMessage() {
    const message = messageInput.value;
    if (message.trim() !== "") {
        const messageElement = document.createElement("div");
        messageElement.className = "message message-sent";
        messageElement.innerHTML = `<strong></strong> ${message}`;
        chatMessages.appendChild(messageElement);
        
        // Xóa nội dung tin nhắn sau khi gửi
        messageInput.value = "";
        // Cuộn xuống cuối cùng để xem tin nhắn mới nhất
        chatMessages.scrollTop = chatMessages.scrollHeight;
    }
}
