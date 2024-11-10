



const display = document.querySelector('#display');
const buttons = document.querySelectorAll('button');
buttons.forEach((item) => {
    item.onclick = () => {
        if (item.id == 'clear') {
            display.innerText = '';
        } else if (item.id == 'backspace') {
            let string = display.innerText.toString();
            display.innerText = string.substr(0, string.length - 1);
        } else if (display.innerText != '' && item.id == 'equal') {
            //display.innerText = eval(display.innerText);
            //var postData = { process: display.innerText };
            //Calculate();

        } else if (display.innerText == '' && item.id == 'equal') {
            display.innerText = 'Empty!';
            setTimeout(() => (display.innerText = ''), 2000);
        } else {
            display.innerText += item.id;
        }
    }
})

function Calculate() {
    $.ajax({
        type: "GET",
        url: "/WebForm1.aspx/Calculate",
        //data: JSON.stringify(postData),
        //data: { process: display.innerText },
        //data: '{process: "' + display.innerText + '" }',
        //contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { process: display.innerText },
        //beforeSend: function () {
        //    //Show(); // Show loader icon
        //},
        success: function (response) {
            display.innerText = response;
        },
        failure: function (response) {
            alert(response.d);
        },
        error: function (error) {
            alert(error.d);
        }
        //complete: function () {
        //    Hide(); // Hide loader icon
        //},
        //failure: function (jqXHR, textStatus, errorThrown) {
        //    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message
        //}
    });
}

const themeToggleBtn = document.querySelector('.theme-toggler');
const calculator = document.querySelector('.dark');
const toggleIcon = document.querySelector('.toggler-icon');
let isDark = true;
themeToggleBtn.onclick = () => {
    calculator.classList.toggle('dark');
    themeToggleBtn.classList.toggle('active');
    isDark = !isDark;
}

        //$(document).ready(function () {

        //});

        //function Show() {

        //}

        //function Hide() {

        //}