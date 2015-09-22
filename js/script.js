var nice = false;
// init controller
var controller = new ScrollMagic.Controller();
jQuery(document).ready(function () {

    var owl = $("#owl-demo");

    owl.owlCarousel({

        // Define custom and unlimited items depending from the width
        // If this option is set, itemsDeskop, itemsDesktopSmall, itemsTablet, itemsMobile etc. are disabled
        // For better preview, order the arrays by screen size, but it's not mandatory
        // Don't forget to include the lowest available screen size, otherwise it will take the default one for screens lower than lowest available.
        // In the example there is dimension with 0 with which cover screens between 0 and 450px

        itemsCustom : [
            [0, 1],
            [450, 1],
            [600, 2],
            [768, 2],
            [992, 3],
            [1024, 4],
            [1200, 6],
            [1400, 6],
            [1600, 6]
        ],
        navigation:true,
        navigationText: [
            "<div class='slider-controls left'></div>",
            "<div class='slider-controls right'></div>"
        ]

    });

    var owl2 = $("#owl-demo-2");

    owl2.owlCarousel({

        // Define custom and unlimited items depending from the width
        // If this option is set, itemsDeskop, itemsDesktopSmall, itemsTablet, itemsMobile etc. are disabled
        // For better preview, order the arrays by screen size, but it's not mandatory
        // Don't forget to include the lowest available screen size, otherwise it will take the default one for screens lower than lowest available.
        // In the example there is dimension with 0 with which cover screens between 0 and 450px

        itemsCustom : [
            [0, 1],
            [450, 1],
            [600, 1],
            [768, 2],
            [992, 3],
            [1024, 4],
            [1200, 6],
            [1400, 6],
            [1600, 6]
        ],
        navigation:true,
        navigationText: [
            "<div class='slider-controls left'></div>",
            "<div class='slider-controls right'></div>"
        ]

    });
/*    function sniffer(){
       if (Modernizr.mq('only screen and (max-width: 1023px)')) {
            nice = $("body").niceScroll().hide();
        } else {
            var nice=$('html').niceScroll({
                cursorborder: 'none',
                cursorborderradius:'0px',
                cursorcolor:"#39CCDB",
                autohidemode: false,
                background:"#333333",
                cursorwidth:"15px"
            });
        }
    }
    sniffer();

    $( window ).resize(function() {
        sniffer();
    });*/

/*    $( '#my-slider' ).sliderPro({
            forceSize: 'fullWindow',
            fullScreen: true,
            arrows: true
        });*/
    // build scene
    // build tween
    var tween1 = TweenMax.from(".legend-1 .animations", 1, {opacity:0, y:-100});

    var scene1 = new ScrollMagic.Scene({triggerElement: ".legend-1 .animations", duration: $(window).outerHeight(true), triggerHook: "onEnter"})
        .setTween(tween1) // the tween duration can be omitted and defaults to 1
        //.addIndicators() // add indicators (requires plugin)
        .addTo(controller);

    var tween2 = TweenMax.from(".legend-2 .animations", 1, {opacity:0, y:-100});

    var scene2 = new ScrollMagic.Scene({triggerElement: ".legend-2 .animations", duration: $(window).outerHeight(true), triggerHook: "onEnter"})
        .setTween(tween2) // the tween duration can be omitted and defaults to 1
        //.addIndicators() // add indicators (requires plugin)
        .addTo(controller);

    var tween3 = TweenMax.from(".legend-3 .animations", 1, {opacity:0, y:-100});

    var scene3 = new ScrollMagic.Scene({triggerElement: ".legend-3 .animations", duration: $(window).outerHeight(true), triggerHook: "onEnter"})
        .setTween(tween3) // the tween duration can be omitted and defaults to 1
        //.addIndicators() // add indicators (requires plugin)
        .addTo(controller);

    if($('body').hasClass('homepage_secondary')){
        // build scenes
        new ScrollMagic.Scene({triggerElement: "#legend_1", triggerHook: "onLeave", offset: $('#site_header').outerHeight(true), duration:1200})
            .setClassToggle("#site_header", "hide") // add class toggle
            .setClassToggle("#site_header_mobile", "show") // add class toggle
            //.addIndicators() // add indicators (requires plugin)
            .addTo(controller);
    }


    $("#scroll-page, #scroll-down a[href^='#'], .scroll_top a[href^='#']").on('click', function(e) {

        // prevent default anchor click behavior
        e.preventDefault();

        // store hash
        var hash = this.hash;

        // animate
        $('html, body').animate({
            scrollTop: $(this.hash).offset().top - 0
        }, 1300, function(){

            // when done, add hash to url
            // (default click behaviour)
            // window.location.hash = hash;
        });

    });
});
