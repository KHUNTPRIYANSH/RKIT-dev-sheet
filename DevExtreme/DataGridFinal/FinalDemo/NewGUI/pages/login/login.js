$(function () {
    // Initialize Username textbox with validation
    $("#userName")
      .dxTextBox({
        value: "",
        placeholder: "User Name",
        showClearButton: true,
      })
      .dxValidator({
        validationRules: [
          {
            type: "required",
            message: "User Name is required",
          },
          {
            type: "pattern",
            pattern: "^[a-zA-Z ]*$",
            message: "Only alphabets and spaces are allowed",
          },
        ],
      });

    // Initialize Password textbox with validation
    $("#password")
      .dxTextBox({
        value: "",
        mode: "password",
        placeholder: "Password",
        showClearButton: true,
      })
      .dxValidator({
        validationRules: [
          {
            type: "required",
            message: "Password is required",
          },
          {
            type: "stringLength",
            min: 8,
            message: "Password must be at least 8 characters long",
          },
        ],
      });

    // Initialize Submit button with click event
    $("#submit-button").dxButton({
      text: "Login",
      type: "default",
      useSubmitBehavior: false,
      onClick: function (e) {
        var validationGroup = DevExpress.validationEngine.validateGroup();
        if (!validationGroup.isValid) {
          return;
        }

        // Collecting input values
        let dataObj = {
          UserName: $("#userName").dxTextBox("instance").option("value"),
          Password: $("#password").dxTextBox("instance").option("value"),
        };

        // API URL for login authentication
        let api = "https://localhost:44310/api/LoginPanel/api/auth/login";

        // AJAX request to authenticate user
        $.ajax({
          url: api,
          type: "POST",
          contentType: "application/json",
          data: JSON.stringify(dataObj),
          success: function (res) {
            alert("User login successfully!");
            console.log(res);

            // Storing user data in localStorage
            let uid = res.ID;
            let utoken = res.Token;
            let urole = res.Role;
            let uname = res.UserName;
            let upass = dataObj.Password;
            localStorage.setItem("uid", uid);
            localStorage.setItem("utoken", utoken);
            localStorage.setItem("urole", urole);
            localStorage.setItem("uname", uname);
            localStorage.setItem("upass", upass);

            // Redirecting to home page
            window.location.href = "../../pages/home/home.html";
          },
          error: function (xhr, status, error) {
            alert("Error: " + xhr.responseText);
          },
        });
      },
    });
  });