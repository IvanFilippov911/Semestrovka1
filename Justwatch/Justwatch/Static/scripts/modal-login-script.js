const openModalBtn = document.getElementById('openModalBtn');
const modal = document.getElementById('modal-form-container');
const closeModalBtns = document.querySelectorAll('.close-btn');

openModalBtn.addEventListener('click', function () {
    modal.style.display = 'flex';
});


closeModalBtns.forEach((closeModalBtn) => {
    closeModalBtn.addEventListener('click', function () {
        modal.style.display = 'none';
    });
});


const openLoginBtn = document.getElementById('openLoginBtn');
const openRegisterBtn = document.getElementById('openRegisterBtn');
const loginModal = document.getElementById('loginModal');
const registerModal = document.getElementById('registerModal');

openRegisterBtn.addEventListener('click', function () {
    loginModal.style.display = 'none';
    registerModal.style.display = 'flex';
});

openLoginBtn.addEventListener('click', function () {
    loginModal.style.display = 'flex';
    registerModal.style.display = 'none';
});