// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const dioDisabled = false;
const dioChance = Math.floor(Math.random() * 500) + 1;

if (dioChance === 1 && dioDisabled === false) {
    window.location = "https://i.ibb.co/2gCYNMg/dio.png";
}
