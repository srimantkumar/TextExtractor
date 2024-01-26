const login = document.getElementById('loginClick').addEventListener('submit', function (event) {
    event.preventDefault();
    submitFormLogin();
});


const signup = document.getElementById('signupClick').addEventListener('submit', function (event) {
    event.preventDefault();
    submitFormSignup();
});


function submitFormLogin() {
    const userName = document.getElementById('userName').value;
    const password = document.getElementById('password').value;

    //console.log("Inside the function")
    // Get form data
    const formData = new FormData();
    formData.append('userName', userName);
    formData.append('password', password);

    var url = 'https://localhost:7165/api/UserLoginController/authentication';

    // Make the fetch call
    fetch(url, {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            document.getElementById('signupClick').reset();

            // Redirect to another page
            window.location.href = 'http://127.0.0.1:5500/index.html';
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function submitFormSignup() {
    const fullName = document.getElementById('fullName').value;
    const userName = document.getElementById('userNameSignup').value;
    const password = document.getElementById('passwordSignup').value;

    console.log("Inside the SignUp function")
    // Get form data
    const formData = new FormData();
    formData.append('fullName', fullName);
    formData.append('userName', userName);
    formData.append('password', password);
    

    var url = 'https://localhost:7165/api/UserController/SignUp';

    // Make the fetch call
    fetch(url, {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            console.log(data)
            document.getElementById('signupClick').reset();

            // Redirect to another page
            window.location.href = 'http://127.0.0.1:5500/Login.html';
        })
        .catch(error => {
            console.error('Error:', error);
            document.getElementById('signupClick').reset();            
        });
}


const passwordField = document.getElementById('password');
const togglePassword = document.getElementById('togglePassword');

togglePassword.addEventListener('click', function () {
    const type = passwordField.getAttribute('type') === 'password' ? 'text' : 'password';
    passwordField.setAttribute('type', type);
    togglePassword.innerHTML = type === 'password' ? '&#128065;' : '&#128064;';
});

const passwordField1 = document.getElementById('passwordSignup');
const togglePassword1 = document.getElementById('togglePassword1');

togglePassword1.addEventListener('click', function () {
    const type = passwordField1.getAttribute('type') === 'password' ? 'text' : 'password';
    passwordField1.setAttribute('type', type);
    togglePassword1.innerHTML = type === 'password' ? '&#128065;' : '&#128064;';
});