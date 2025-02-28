document.getElementById('register-submit').addEventListener('click', function (event) {
    event.preventDefault();

    const email = document.querySelector('input[name="email"]').value;
    const name = document.querySelector('input[name="name"]').value;
    const birthDate = document.querySelector('input[name="birthDate"]').value;
    const password = document.getElementById('password-register').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    if (password !== confirmPassword) {
        document.getElementById('register-error-message').innerText = "Passwords do not match";
        document.getElementById('register-error-message').style.display = 'block';
        return;
    }

    if (!email || !name || !birthDate || !password || !confirmPassword) {
        document.getElementById('register-error-message').innerText = "ALL fields must be completed";
        document.getElementById('register-error-message').style.display = 'block';
        return;
    }

    console.log(email, name, birthDate, password);

    fetch('/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            Email: email,
            Name: name,
            DateOfBirth: birthDate,
            Password: password
        })
    })
        .then((response) => {
            if (response.redirected) {
                window.location.href = response.url;
            } else {
                return response.json();
            }
        })
        .then(data => {
            if (data && !data.success) {
                const errorMessage = document.getElementById("register-error-message");
                errorMessage.textContent = data.message;
                errorMessage.style.display = "block";
            }
           
        })
        .catch(error => {
            console.error('Error:', error);
            const errorMessage = document.getElementById("register-error-message");
            errorMessage.textContent = "Произошла ошибка при регистрации.";
            errorMessage.style.display = "block";
        });
});

document.getElementById('login-submit').addEventListener('click', function (event) {
    event.preventDefault();

    const email = document.querySelector('input[name="email-login"]').value;
    const password = document.getElementById('password-login').value;

    if (!email || !password) {
        document.getElementById('login-error-message').innerText = "ALL fields must be completed";
        document.getElementById('login-error-message').style.display = 'block';
        return;
    }

    fetch('/loging', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            Email: email,
            Password: password
        }),
        credentials: 'same-origin'
    })
        .then((response) => {
            if (response.redirected) {
                window.location.href = response.url;
            } else {
                return response.json();
            }
        })
        .then(data => {
            if (data && !data.success) {
                const errorMessage = document.getElementById("login-error-message");
                errorMessage.textContent = data.message;
                errorMessage.style.display = "block";
            }
        })
        .catch(error => {
            console.error('Error:', error);
            const errorMessage = document.getElementById("login-error-message");
            errorMessage.textContent = "Произошла ошибка при входе.";
            errorMessage.style.display = "block";
        });
});



