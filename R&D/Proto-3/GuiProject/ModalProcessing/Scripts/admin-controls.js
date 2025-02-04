console.log("Admin external script working");

setTimeout(function () {
    let content = document.querySelector(".content");
    if (content) {
        let wrapper = document.createElement("main"); // Corrected typo here
        let title = document.createElement("h1");
        title.textContent = "This is admin controls [from external js file]"; // Add a title text to the h1
        let img = document.createElement('img');
        img.src = 'https://cdn-kiaed.nitrocdn.com/enJVXqpLRhJnvcOePEkktsDXFhAHwvRz/assets/images/optimized/rev-7838bb9/miracleaccountingsoftware.com/wp-content/uploads/2022/10/download.jpg';
        img.width = 300;

        wrapper.appendChild(title);  // Append title to wrapper
        wrapper.appendChild(img);    // Append image to wrapper
        content.appendChild(wrapper);  // Append the wrapper to the content
    } else {
        console.warn("Content element not found!");
    }
}, 1000);  // Wait for 1 second before running the code
