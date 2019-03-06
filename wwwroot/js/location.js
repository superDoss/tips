$(document).ready(function(){
    var x = document.getElementById('locationLabel');
    if (navigator.geolocation) {
        return navigator.geolocation.getCurrentPosition(function(position){
            x.innerText = position.coords.longitude + "," + position.coords.latitude;
        });
      }
});