function submitForm() {
    const userFile = document.getElementById('adharFile').files[0];

    console.log("Inside the function")
    // Get form data
    const formData = new FormData();
    formData.append('file', userFile);

    var url = 'https://localhost:7165/api/DocumentController/adharImage';

    // Make the fetch call
    fetch(url, {
        method: 'POST',
        body: formData
    })
    .then(response => response.json())
    .then(data => {
        console.log(data)
        displayResult(data);
    })
    .catch(error => {
        console.error('Error:', error);
    });
}

// Assuming your button has an id 'submitButton'
const button = document.getElementById('submitButton');
button.addEventListener('click', function() {
    submitForm();
});


function displayResult(result) {
    // Fill the predefined form fields with values from the JSON response
    document.getElementById('field1').value = result.FullName;
    document.getElementById('field2').value = result.DOB;
    const dropdownField = document.getElementById('field5');
    dropdownField.value = result.Gender;
    document.getElementById('field7').value = result.Address;
    document.getElementById('field9').value = result.AdharNumber;
    var issuedAuthority = "Government of India";
    var IdType = "Adhar Card";
    var occupation = "Student";
    var email = "sanjay@gmail.com";
    var mobileNo = "786-**8-65*2"
    document.getElementById('field3').value = email;
    document.getElementById('field4').value = mobileNo;
    document.getElementById('field6').value = occupation;
    document.getElementById('field8').value = IdType;
    document.getElementById('field10').value = issuedAuthority;

    // Optionally, you can hide the file upload form after successful submission
    //document.getElementById('uploadForm').style.display = 'none';
}
