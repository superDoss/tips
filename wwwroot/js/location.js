$(document).ready(function(){
    var x = document.getElementById('locationLabel');
    if (navigator.geolocation) {
        return navigator.geolocation.getCurrentPosition(function(position){
            x.value = position.coords.longitude + "," + position.coords.latitude;
        });
      }
});

$(document).ready(function(){
    var location =  document.getElementById('tipLocation').textContent.replace(/\s/g, "").split(',').map(parseFloat);
        var map = L.map('map').setView(location, 13);

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(map);

        L.marker(location).addTo(map)
            .bindPopup('A pretty CSS3 popup.<br> Easily customizable.')
            .openPopup();
});

