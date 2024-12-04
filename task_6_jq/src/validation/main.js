$(document).ready(function () {
  //! Initialize jQuery Validation Plugin
  $("#signupForm").validate({
    rules: {
      textName: {
        required: true,
        minlength: 3,
      },
      textEmail: {
        required: true,
        email: true,
      },
      textPassword: {
        required: true,
        minlength: 6,
      },
      textPhoneNo: {
        required: true,
        digits: true,
        minlength: 10,
        maxlength: 10,
      },
      terms: {
        required: true,
      },
    },
    messages: {
      textName: {
        required: "Full Name is required.",
        minlength: "Full Name must be at least 3 characters long.",
      },
      textEmail: {
        required: "Email is required.",
        email: "Please enter a valid email address.",
      },
      textPassword: {
        required: "Password is required.",
        minlength: "Password must be at least 6 characters long.",
      },
      textPhoneNo: {
        required: "Mobile Number is required.",
        digits: "Please enter a valid 10-digit mobile number.",
        minlength: "Mobile Number must be exactly 10 digits.",
        maxlength: "Mobile Number must be exactly 10 digits.",
      },
      terms: {
        required: "You must agree to the terms and conditions.",
      },
    },
    errorPlacement: function (error, element) {
      // err : is a message generated for err in any field
      // element : input field in which err is validated
      const id = element.attr("id") + "Error";
      // above line retrieves the id of the element and appends "Error" to it to form a unique ID for the error message container.
      $("#" + id).html(error);
    },
    success: function (label, element) {
      const id = $(element).attr("id") + "Error";
      $("#" + id).html("");
    },
    submitHandler: function (form) {
      // Prevent form submission refresh
      event.preventDefault();
      // Serialize form data
      const serializedData = $(form).serialize();
      console.log("Serialized Data:", serializedData);

      // Deserialize form data
      const deserializedData = {};
      serializedData.split("&").forEach((pair) => {
        const [key, value] = pair.split("=");
        deserializedData[key] = decodeURIComponent(value);
      });
      console.log("Deserialized Data:", deserializedData);

      // Alert for successful submission
      alert("Success! Form submitted with serialized and deserialized data.");
    },
  });
});
