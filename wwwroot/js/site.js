// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function(){
    var x = document.getElementById('locationLabel');
    if (navigator.geolocation) {
        return navigator.geolocation.getCurrentPosition(function(position){
            x.innerText = position.coords.longitude + "," + position.coords.latitude;
        });
      }
});