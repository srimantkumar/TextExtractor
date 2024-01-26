// const form = document.querySelector("form"),
//     nextBtn = form.querySelector(".nextBtn"),
//     allInput = form.querySelectorAll(".first input");



//     nextBtn.addEventListener("click", (event) => {
//         event.preventDefault();
//         let allValuesFilled = true;
    
//         allInput.forEach(input => {
//             if (input.value === "") {
//                 allValuesFilled = false;
//             }
//         });
    
//         if (allValuesFilled) {
//             // All form values are filled, redirect to success.html
//             window.location.href = "http://127.0.0.1:5500/success.html";
//         } else {
//             form.classList.remove('secActive');
//         }
//     });

// nextBtn.addEventListener("click", ()=> {
//     allInput.forEach(input => {
//         if(input.value != ""){
//             form.classList.add('secActive');
//         }else{
//             form.classList.remove('secActive');
//         }
//     })
// })

// backBtn.addEventListener("click", () => form.classList.remove('secActive'));

document.getElementById("bankPageSubmission").addEventListener("submit", function(event) {
    event.preventDefault();
    window.location.href = 'http://127.0.0.1:5500/success.html';
});