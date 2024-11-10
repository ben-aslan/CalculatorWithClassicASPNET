<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Calculator._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="calculator dark">
            <div class="theme-toggler active">
                <i class="toggler-icon"></i>
            </div>
            <div class="display-screen">
                <div id="display"></div>
            </div>
            <div class="buttons">
                <table>
                    <tr>
                        <td>
                            <button class="btn-operator" id="clear">C</button></td>
                        <td>
                            <button class="btn-operator" id="/">&divide;</button></td>
                        <td>
                            <button class="btn-operator" id="*">&times;</button></td>
                        <td>
                            <button class="btn-operator" id="backspace"><</button></td>
                    </tr>
                    <tr>
                        <td>
                            <button class="btn-number" id="7">7</button></td>
                        <td>
                            <button class="btn-number" id="8">8</button></td>
                        <td>
                            <button class="btn-number" id="9">9</button></td>
                        <td>
                            <button class="btn-operator" id="-">-</button></td>
                    </tr>
                    <tr>
                        <td>
                            <button class="btn-number" id="4">4</button></td>
                        <td>
                            <button class="btn-number" id="5">5</button></td>
                        <td>
                            <button class="btn-number" id="6">6</button></td>
                        <td>
                            <button class="btn-operator" id="+">+</button></td>
                    </tr>
                    <tr>
                        <td>
                            <button class="btn-number" id="1">1</button></td>
                        <td>
                            <button class="btn-number" id="2">2</button></td>
                        <td>
                            <button class="btn-number" id="3">3</button></td>
                        <td rowspan="2">
                            <button class="btn-equal" id="equal">=</button></td>
                    </tr>
                    <tr>
                        <td>
                            <button class="btn-operator" id="(">(</button></td>
                        <td>
                            <button class="btn-number" id="0">0</button></td>
                        <td>
                            <button class="btn-operator" id=")">)</button></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <style>
        * {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
            outline: 0;
            transition: all 0.5s ease;
        }

        body {
            font-family: sans-serif;
        }

        a {
            text-decoration: none;
            color: #fff;
        }

        body {
            background-image: linear-gradient(to bottom right, rgba(189, 13, 34, 1), rgba(2, 0, 184, 1), rgba(0, 0, 0, 1));
        }

        .container {
            height: 100vh;
            width: 100vw;
            display: grid;
            place-items: center;
        }

        .calculator {
            position: relative;
            height: auto;
            width: auto;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 30px #000;
        }

        .theme-toggler {
            position: absolute;
            top: 30px;
            right: 30px;
            color: #fff;
            cursor: pointer;
            z-index: 1;
        }

            .theme-toggler.active {
                color: #333;
            }

                .theme-toggler.active::before {
                    background-color: #fff;
                }

            .theme-toggler::before {
                content: '';
                height: 30px;
                width: 30px;
                position: absolute;
                top: 50%;
                transform: translate(-50%, -50%);
                border-radius: 50%;
                background-color: #333;
                z-index: -1;
            }

        #display {
            margin: 0 10px;
            height: 150px;
            width: auto;
            max-width: 270px;
            display: flex;
            align-items: flex-end;
            justify-content: flex-end;
            font-size: 30px;
            margin-bottom: 20px;
            overflow-x: scroll;
        }

            #display::-webkit-scrollbar {
                display: block;
                height: 3px;
            }

        button {
            height: 60px;
            width: 60px;
            border: 0;
            border-radius: 30px;
            margin: 5px;
            font-size: 20px;
            cursor: pointer;
            transition: all 200ms ease;
        }

            button:hover {
                transform: scale(1.1);
            }

            button#equal {
                height: 130px;
            }

        .calculator {
            background-color: #fff;
        }

            .calculator #display {
                color: #0a1e23;
            }

            .calculator button#clear {
                background-color: #ffd5d8;
                color: #fc4552;
            }

            .calculator button.btn-number {
                background-color: #c3eaff;
                color: #000;
            }

            .calculator button.btn-operator {
                background-color: #ffd0fd;
                color: #f967f3;
            }

            .calculator button.btn-equal {
                background-color: #adf9e7;
                color: #000;
            }

            .calculator.dark {
                background-color: #071115;
            }

                .calculator.dark #display {
                    color: #f8fafd;
                }

                .calculator.dark button#clear {
                    background-color: #2d191e;
                    color: #bd3740;
                }

                .calculator.dark button.btn-number {
                    background-color: #1b2f38;
                    color: #f8fafb;
                }

                .calculator.dark button.btn-operator {
                    background-color: #2e1f39;
                    color: #aa00a4;
                }

                .calculator.dark button.btn-equal {
                    background-color: #223323;
                }
    </style>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script src="jquery.js"></script>
    <script src="jquery.min.js"></script>
    <script type="text/javascript">
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
                    Calculate();

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
                type: "POST",
                url: "CS.aspx/Calculate",
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
    </script>


    <%--<form id="form1" runat="server" style="margin-left: 234px; height: 314px;">

        <asp:TextBox ID="TextBox2" runat="server" Enabled="False" Width="203px" Font-Size="X-Large"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="Result:"></asp:Label>
        &nbsp;&nbsp;
                <asp:Label ID="Label2" runat="server" Text="0" />
        &nbsp;



        <asp:Panel ID="Panel3" runat="server" Height="182px" HorizontalAlign="Left" Width="310px">
            <asp:Button ID="number1Button" runat="server" OnClick="number1Button_Click" Text="1" Height="48px" Width="48px" />
            <asp:Button ID="number2Button" runat="server" OnClick="number2Button_Click" Text="2" Width="48px" Height="48px" />
            <asp:Button ID="number3Button" runat="server" OnClick="number3Button_Click" Text="3" Height="48px" Width="48px" />
            <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="+" Height="48px" Width="48px" />
            <asp:Button ID="backButton3" runat="server" OnClick="backButton2_Click" Text="Back" Width="95px" Height="48px" />
            <br />
            <asp:Button ID="number4Button" runat="server" OnClick="number4Button_Click" Text="4" Height="48px" Width="48px" />
            <asp:Button ID="number5Button" runat="server" OnClick="number5Button_Click" Text="5" Height="48px" Width="48px" />
            <asp:Button ID="number6Button" runat="server" OnClick="number6Button_Click" Text="6" Height="48px" Width="48px" />
            <asp:Button ID="sub" runat="server" OnClick="sub_Click" Text="- " Width="48px" Height="48px" />
            <asp:Button ID="backButton4" runat="server" Height="48px" OnClick="backButton4_Click" Text="C" Width="95px" />
            <br />
            <asp:Button ID="number7Button" runat="server" OnClick="number7Button_Click" Text="7" Height="48px" Width="48px" />
            <asp:Button ID="number8Button" runat="server" OnClick="number8Button_Click" Text="8" Width="48px" Height="48px" />
            <asp:Button ID="number9Button" runat="server" OnClick="number9Button_Click" Text="9" Height="48px" Width="48px" />
            <asp:Button ID="multiButton" runat="server" OnClick="multiButton_Click" Text="* " Height="48px" Width="48px" />
            <asp:Button ID="bracket1" runat="server" Height="48px" OnClick="bracket1_Click" Text="(" Width="48px" />
            <asp:Button ID="bracket2" runat="server" Height="48px" OnClick="bracket2_Click" Text=")" Width="48px" />
            <br />
            <asp:Button ID="dotButton" runat="server" OnClick="dotButton_Click" Text=". " Height="48px" Width="48px" />
            <asp:Button ID="number0Button" runat="server" OnClick="number0Button_Click" Text="0" Height="48px" Width="48px" />
            <asp:Button ID="divitionButton0" runat="server" OnClick="divitionButton0_Click" Text="/ " Height="48px" Width="48px" />
            <asp:Button ID="EqualButton" runat="server" OnClick="Button1_Click" Text="=" Width="89px" Height="48px" />
            <br />
            <asp:Button ID="BaseButton" runat="server" Height="48px" OnClick="BaseButton_Click" Text="^" Width="48px" />
            <asp:Button ID="RootButton" runat="server" Height="48px" OnClick="RootButton_Click" Text="√" Width="48px" />
        </asp:Panel>


        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
    </form>--%>
</asp:Content>
