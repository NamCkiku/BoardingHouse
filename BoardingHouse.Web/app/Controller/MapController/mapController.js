var map;
var marker;
function initMap() {
    var uluru = { lat: 21.0029317912212212, lng: 105.820226663232323 };
    map = new google.maps.Map(document.getElementById('map'), {
        center: uluru,
        zoom: 14
    });
    marker = new google.maps.Marker({
        position: uluru,
        map: map,
        animation: google.maps.Animation.DROP,
        icon: '/Content/img/IconMaker.png'
    });
    var contentString = '<div class="infoW"><div class="propImg"><div class="featured-label"><div class="featured-label-left"></div><div class="featured-label-content"><span class="fa fa-star"></span></div><div class="featured-label-right"></div><div class="clearfix"></div></div><img src="http://mariusn.com/themes/reales-wp/wp-content/uploads/2015/02/img-prop-400x240.jpg"><div class="propBg"><div class="propPrice">$799 000 </div><div class="propType">For Sale</div></div></div><div class="paWrapper"><div class="propTitle">Sophisticated Residence</div><div class="propAddress">600 40th Ave, San Francisco</div></div><ul class="propFeat"><li><span class="fa fa-moon-o"></span> 2</li><li><span class="icon-drop"></span> 3</li><li><span class="icon-frame"></span> 1270 sq ft</li></ul><div class="clearfix"></div><div class="infoButtons"><a class="btn btn-sm btn-round btn-gray btn-o closeInfo">Close</a><a href="http://mariusn.com/themes/reales-wp/properties/sophisticated-residence/" class="btn btn-sm btn-round btn-green viewInfo">View</a></div></div>'
    var infowindow = new google.maps.InfoWindow({
        content: contentString,
        disableAutoPan: false,
        maxWidth: 200,
        pixelOffset: new google.maps.Size(0, 0),
        zIndex: null,
        boxStyle: {
            'background': '#fff',
            'opacity': 1,
            'padding': '5px',
            'box-shadow': '0 1px 2px 0 rgba(0, 0, 0, 0.13)',
            'width': '140px',
            'text-align': 'center',
            'border-radius': '3px'
        },
        closeBoxMargin: "28px 26px 0px 0px",
        closeBoxURL: "",
        infoBoxClearance: new google.maps.Size(1, 1),
        pane: "floatPane",
        enableEventPropagation: false
    });
    marker.addListener('click', function () {
        infowindow.setContent(contentString);
        infowindow.open(map, marker);
    });

}