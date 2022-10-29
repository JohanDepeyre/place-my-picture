

function GenererCarte(json1) {


                    let obj = json1[0];
                    let latFirst = obj.Latitude;
                    let lonFirst = obj.Longitude;
                    //  create map object, tell it to live in 'map' div and give initial latitude, longitude, zoom values
                    var map = L.map('map', { scrollWheelZoom: true }).setView([latFirst, lonFirst], 15);

                    //  add base map tiles from OpenStreetMap and attribution info to 'map' div
                    L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
                        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                    }).addTo(map);


                    for (let i = 0; i < json1.length; i++) {
                        let obj = json1[i];
                        let urlIcon = "";
                        let idCategorie = obj.CategorieId;
                        switch (idCategorie) {
                            case 1:
                                urlIcon = 'img/icon_paysage.png';
                                break;
                            case 2:
                                urlIcon = 'img/icon_group.png';
                                break;
                            case 3:
                                urlIcon = 'img/icon_sport.png';
                                break;
                            case 4:
                                urlIcon = 'img/icon_selfie.png';
                                break;
                            default:
                                urlIcon = 'img/icon_paysage.png'
                        }


                        var imageIcon = L.icon({
                            iconUrl: urlIcon,
                            iconSize: [30, 30], // size of the icon
                            popupAnchor: [0, -15]
                        });

                        let imageLink = obj.ImageUrl;
                        // create popup contents
                        var customPopup = obj.Description + "<br/><img src='" + imageLink + " ' alt='maptime logo gif' width='350px'/>";
                        // specify popup options
                        var customOptions =
                        {
                            'maxWidth': '500',
                            'className': 'custom'
                        }

                        let lat = obj.Latitude;
                        let lon = obj.Longitude;
                        // create marker object, pass custom icon as option, pass content and options to popup, add to map
                        L.marker([lat, lon], { icon: imageIcon }).bindPopup(customPopup, customOptions).addTo(map);

                        console.log(obj.ImageUrl);
                    }

}