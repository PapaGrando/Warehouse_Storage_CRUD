// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const img = document.getElementById("productImage")
img.addEventListener("error", function (event) {
    event.target.src = "~/images/DefaultProd.png"
    event.onerror = null
})