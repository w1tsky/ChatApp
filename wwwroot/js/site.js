



var createRoomBtn = document.getElementById('create-room-btn')

var createRoomModal = document.getElementById('create-room-modal')

createRoomBtn.addEventListener('click', function () {
    createRoomModal.classList.add('active')
})

function closeModal() {
    createRoomModal.classList.remove('active')
}

function showBar() {
    var x = document.getElementById("nav");
    if (x.className === "navbar") {
        x.className += " responsive";
    } else {
        x.className = "navbar";
    }
}