$(document).ready(function () {
    jQuery(".dt-menu-expand").on("click", function () {
        return event.preventDefault(),
            $(this).hasClass("dt-mean-clicked") ? ($(this).text("+"),
                $(this).prev("ul").length ? $(this).prev("ul").slideUp(400) : $(this).prev(".megamenu-child-container").find("ul:first").slideUp(600)) : ($(this).text("--"),
                    $(this).prev("ul").length ? $(this).prev("ul").slideDown(400) : $(this).prev(".megamenu-child-container").find("ul:first").slideDown(2e3)),
            $(this).toggleClass("dt-mean-clicked"),
            !1
    }),
        AOS.init({
            duration: 1200
        }),
        jQuery(".special-collection,.ttproduct").wrapAll('<div class="main-product"><div class="container"></div></div>'),
        jQuery(".category-featured,.template-index .main-product,.cmsbanner,.bestseller-collection,.categoryslider").wrapAll('<div class="section-bg"></div>'),
        jQuery("#GotoTop").on("click", function () {
            return jQuery("html, body").animate({
                scrollTop: 0
            }, "1000"),
                !1
        }),
        jQuery(".nav-toggle").click(function (event2) {
            jQuery(this).toggleClass("active"),
                event2.stopPropagation(),
                jQuery("#tt-megamenu .tt-mega_menu").slideToggle("2000"),
                jQuery("body").toggleClass("open-header")
        }),
        jQuery("#tt-megamenu .tt-mega_menu").on("click", function (event2) {
            event2.stopPropagation(),
                jQuery(this).removeClass("active")
        }),
        jQuery("#tt-megamenu1 li.tt_menu_item.tt_mm_hassub").hover(function () {
            jQuery("body").addClass("menu-open")
        }, function () {
            jQuery("body").removeClass("menu-open")
        }),
        jQuery(".dark-light-mode .dark-mode").on("click", function (event2) {
            jQuery("body").addClass("darkmode")
        }),
        jQuery(".dark-light-mode .light-mode").on("click", function (event2) {
            jQuery("body").removeClass("darkmode")
        }),
        jQuery(".cart-footer-actions .cart-note").on("click", function (event2) {
            jQuery(".mini-cart-footer .note-detail").addClass("open")
        }),
        jQuery(".fixed-cart-wrap,.note-detail .text-cancle,.note-detail .text-save").click(function () {
            jQuery(".mini-cart-footer .note-detail").removeClass("open")
        }),
        jQuery(".cart-footer-actions .coupen-code").on("click", function (event2) {
            jQuery(".mini-cart-footer .discount-code").addClass("open")
        }),
        jQuery(".fixed-cart-wrap,.discount-code .text-cancle,.discount-code .text-save").click(function () {
            jQuery(".mini-cart-footer .discount-code").removeClass("open")
        }),
        jQuery(".filter-toggle").on("click", function (e2) {
            e2.preventDefault(),
                jQuery(this).toggleClass("active"),
                jQuery(".filter-toggle-wrap").slideToggle("is-visible")
        }),
        jQuery(".product-single__thumbs img").on("click", function () {
            var src = jQuery(this).attr("src");
            src != "" && jQuery(this).closest(".product-wrapper").find("img.grid-view-item__image").first().attr("src", src),
                jQuery(this).parent(".grid-item").addClass("open").siblings(".grid-item").removeClass("open")
        });
    var p = jQuery(".popup_overlay");
    jQuery(".play-icone").click(function () {
        jQuery("body").addClass("popup-toggle"),
            p.css("display", "block")
    }),
        p.click(function (event2) {
            e = event2 || window.event,
                e.target == this && (jQuery(p).css("display", "none"),
                    jQuery("body").removeClass("popup-toggle"))
        }),
        jQuery(".popup_close,.homeslider ul.slick-slider .slick-arrow").click(function () {
            p.css("display", "none"),
                jQuery("body").removeClass("popup-toggle")
        });
    function toggleVideo(state) {
        var div = document.getElementById("popupVid")
            , iframe = div.getElementsByTagName("iframe")[0].contentWindow;
        func = state == "hide" ? "pauseVideo" : "playVideo",
            iframe.postMessage('{"event":"command","func":"' + func + '","args":""}', "*")
    }
    jQuery(".play-icone, .homeslider ul.slick-slider .slick-arrow").click(function () {
        p.css("visibility", "visible").css("opacity", "1")
    }),
        p.click(function (event2) {
            e = event2 || window.event,
                e.target == this && (jQuery(p).css("visibility", "hidden").css("opacity", "0"),
                    toggleVideo("hide"))
        }),
        jQuery(".popup_close").click(function () {
            p.css("visibility", "hidden").css("opacity", "0"),
                toggleVideo("hide")
        });
    var p1 = jQuery(".about-videoblock .popup_overlay");
    jQuery(".about-videoblock #popup_toggle").click(function () {
        jQuery("body").addClass("popup-toggle1"),
            p1.css("display", "block")
    }),
        p1.click(function (event2) {
            e = event2 || window.event,
                e.target == this && (jQuery(p1).css("display", "none"),
                    jQuery("body").removeClass("popup-toggle1"))
        }),
        jQuery(".about-videoblock .popup_close").click(function () {
            p1.css("display", "none"),
                jQuery("body").removeClass("popup-toggle1")
        });
    function toggleVideo1(state) {
        var div = document.getElementById("popupVid")
            , iframe = div.getElementsByTagName("iframe")[0].contentWindow;
        func = state == "hide" ? "pauseVideo" : "playVideo",
            iframe.postMessage('{"event":"command","func":"' + func + '","args":""}', "*")
    }
    jQuery(".about-videoblock #popup_toggle").click(function () {
        p1.css("visibility", "visible").css("opacity", "1")
    }),
        p1.click(function (event2) {
            e = event2 || window.event,
                e.target == this && (jQuery(p1).css("visibility", "hidden").css("opacity", "0"),
                    toggleVideo1("hide"))
        }),
        jQuery(".about-videoblock .popup_close").click(function () {
            p1.css("visibility", "hidden").css("opacity", "0"),
                toggleVideo1("hide")
        }),
        jQuery(".shopify-payment-button .shopify-payment-button__button").prepend(jQuery("<i class='mdi mdi-cart-outline'></i>")),
        jQuery(".header_1 .wrapper-wrap").hasClass("logo_center") && jQuery("body").addClass("logo_center");
    var w_width = $(window).width();
    $(".slider-content-main-wrap").css("width", w_width),
        $(".site-header").hasClass("header_transaparent") && $("body.template-index").addClass("header_transaparent");
    var img_id = jQuery(".product-single__thumbs .slick-active.slick-current").find(".product-single__thumb").data("id");
    jQuery(".product-lightbox-btn").hasClass(img_id) && jQuery(".product-lightbox-btn." + img_id).show(),
        jQuery(".filter-left").on("click", function (e2) {
            e2.preventDefault(),
                jQuery(this).toggleClass("active"),
                jQuery(".off-canvas.position-left").addClass("is-open"),
                jQuery(".js-off-canvas-overlay.is-overlay-fixed").addClass("is-visible is-closable")
        }),
        jQuery(".filter-right").on("click", function (e2) {
            e2.preventDefault(),
                jQuery(this).toggleClass("active"),
                jQuery(".off-canvas.position-right").addClass("is-open"),
                jQuery(".js-off-canvas-overlay.is-overlay-fixed").addClass("is-visible is-closable")
        }),
        jQuery(".off-canvas .sidebar_close").on("click", function (e2) {
            e2.preventDefault(),
                jQuery(".off-canvas.position-left").removeClass("is-open"),
                jQuery(".off-canvas.position-right").removeClass("is-open"),
                jQuery(".js-off-canvas-overlay.is-overlay-fixed").removeClass("is-visible is-closable")
        }),
        jQuery(".is-overlay-fixed").on("click", function (e2) {
            e2.preventDefault(),
                jQuery(".filter-left").trigger("click"),
                jQuery(".filter-right").trigger("click"),
                jQuery(".off-canvas.position-left").removeClass("is-open"),
                jQuery(".off-canvas.position-right").removeClass("is-open"),
                jQuery(".js-off-canvas-overlay.is-overlay-fixed").removeClass("is-visible is-closable")
        }),
        $(".product-360-button a").magnificPopup({
            type: "inline",
            mainClass: "mfp-fade",
            removalDelay: 160,
            disableOn: !1,
            preloader: !1,
            fixedContentPos: !1,
            callbacks: {
                open: function () {
                    $(window).resize()
                }
            }
        }),
        countDownIni(".flip-countdown,.js-flip-countdown"),
        $(".popup-video").magnificPopup({
            disableOn: 300,
            type: "iframe",
            mainClass: "mfp-fade",
            removalDelay: 160,
            preloader: !1,
            fixedContentPos: !1
        }),
        ($("a.product-lightbox-btn").length > 0 || $("a.product-video-popup").length > 0) && $(".product-single__photos .gallery,.design_2 .product-img").magnificPopup({
            delegate: "a",
            type: "image",
            tLoading: '<div class="please-wait dark"><span></span><span></span><span></span></div>',
            removalDelay: 300,
            closeOnContentClick: !0,
            gallery: {
                enabled: !0,
                navigateByImgClick: !1,
                preload: [0, 1]
            },
            image: {
                verticalFit: !1,
                tError: '<a href="%url%">The image #%curr%</a> could not be loaded.'
            },
            callbacks: {
                beforeOpen: function () {
                    var productVideo = $(".product-video-popup").attr("href");
                    if (productVideo) {
                        this.st.mainClass = "has-product-video";
                        var galeryPopup = $.magnificPopup.instance;
                        galeryPopup.items.push({
                            src: productVideo,
                            type: "iframe"
                        }),
                            galeryPopup.updateItemHTML()
                    }
                },
                open: function () { }
            }
        }),
        $(".design_3 .product-img,.design_5 .pro_img").magnificPopup({
            delegate: "a",
            type: "image",
            tLoading: '<div class="please-wait dark"><span></span><span></span><span></span></div>',
            removalDelay: 300,
            closeOnContentClick: !0,
            gallery: {
                enabled: !0,
                navigateByImgClick: !1,
                preload: [0, 1]
            },
            image: {
                verticalFit: !1,
                tError: '<a href="%url%">The image #%curr%</a> could not be loaded.'
            },
            callbacks: {
                beforeOpen: function () {
                    var productVideo = $(".product-video-popup").attr("href");
                    if (productVideo) {
                        this.st.mainClass = "has-product-video";
                        var galeryPopup = $.magnificPopup.instance;
                        galeryPopup.items.push({
                            src: productVideo,
                            type: "iframe"
                        }),
                            galeryPopup.updateItemHTML()
                    }
                },
                open: function () { }
            }
        }),
        $("body").on("click", ".product-lightbox-btn", function (e2) {
            $(".product-wrapper-owlslider").find(".owl-item.active a").click(),
                e2.preventDefault()
        }),
        $(".product-layouts .qtyplus").on("click", function (e2) {
            e2.preventDefault();
            var input_val = jQuery(this).parents(".qty-box-set").find(".quantity")
                , currentVal = parseInt(jQuery(this).parents(".qty-box-set").find(".quantity").val());
            isNaN(currentVal) ? jQuery(this).parents(".qty-box-set").find(".quantity").val(1) : jQuery(this).parents(".qty-box-set").find(".quantity").val(currentVal + 1)
        }),
        $(".product-layouts .qtyminus").on("click", function (e2) {
            e2.preventDefault();
            var input_val = jQuery(this).parents(".qty-box-set").find(".quantity")
                , currentVal = parseInt(jQuery(this).parents(".qty-box-set").find(".quantity").val());
            !isNaN(currentVal) && currentVal > 1 ? jQuery(this).closest(".qty-box-set").find(".quantity").val(currentVal - 1) : jQuery(this).closest(".qty-box-set").find(".quantity").val(1)
        }),
        $(".sticky-data .product-qty .qtyplus").on("click", function (e2) {
            e2.preventDefault();
            var input_val = jQuery(this).parents(".qty-box-set").find(".quantity")
                , currentVal = parseInt(jQuery(this).parents(".qty-box-set").find(".quantity").val());
            isNaN(currentVal) ? jQuery(this).parents(".qty-box-set").find(".quantity").val(1) : jQuery(this).parents(".qty-box-set").find(".quantity").val(currentVal + 1)
        }),
        $(".sticky-data .product-qty .qtyminus").on("click", function (e2) {
            e2.preventDefault();
            var input_val = jQuery(this).parents(".qty-box-set").find(".quantity")
                , currentVal = parseInt(jQuery(this).parents(".qty-box-set").find(".quantity").val());
            !isNaN(currentVal) && currentVal > 1 ? jQuery(this).closest(".qty-box-set").find(".quantity").val(currentVal - 1) : jQuery(this).closest(".qty-box-set").find(".quantity").val(1)
        }),
        $("#navToggle").on("click", function (e2) {
            jQuery(this).next(".Site-navigation").slideToggle(500)
        }),
        $(".menu_toggle_wrap #navToggle").on("click", function (e2) {
            jQuery(this).parent().next(".Site-navigation").slideToggle(500)
        }),
        jQuery("body.footer_style_1 .footer_toggle").on("click", function (e2) {
            jQuery("#shopify-section-footer-model-1").addClass("toggle-footer"),
                jQuery("body").addClass("footer1-open"),
                e2.preventDefault()
        }),
        jQuery("body.footer_style_1 .footer_close_toggle").on("click", function (e2) {
            jQuery("#shopify-section-footer-model-1").removeClass("toggle-footer"),
                jQuery("body").removeClass("footer1-open"),
                e2.preventDefault()
        }),
        jQuery(".site-header__search.search-full .serach_icon").on("click", function (e2) {
            e2.preventDefault(),
                jQuery(this).toggleClass("active"),
                jQuery("body").toggleClass("search_full_active"),
                jQuery(".search-full-screen").slideToggle("slow"),
                jQuery(".full-search-wrapper").addClass("search-overlap"),
                jQuery(".myaccount  .dropdown-toggle").removeClass("open"),
                jQuery(".myaccount  .customer_account").slideUp("fast"),
                jQuery(".site-header .site-header_cart_link").removeClass("active"),
                jQuery(".fixed-cart-wrap").removeClass("active"),
                jQuery(".search-bar > input").focus(),
                jQuery("body").toggleClass("search_toggle"),
                jQuery("body").removeClass("account-toggle"),
                jQuery("body").removeClass("cart_toggle")
        }),
        jQuery(".site-header__search.search-full .close-search").on("click", function () {
            jQuery(".site-header__search.search-full .serach_icon").removeClass("active"),
                jQuery(".full-search-wrapper").removeClass("search-overlap"),
                jQuery(".full-search-wrapper").removeClass("search-overlap"),
                jQuery(".header_3 .search-full-screen").removeClass("active"),
                jQuery("body").removeClass("search_full_active"),
                jQuery("body").removeClass("search_toggle"),
                jQuery(".search-full-screen").slideUp("slow")
        }),
        jQuery(".site-header__search:not(.search-full) .serach_icon").on("click", function () {
            jQuery(".search_wrapper").slideToggle("fast"),
                jQuery(".search-bar > input").focus(),
                jQuery(this).toggleClass("active"),
                jQuery("body").toggleClass("search_toggle"),
                jQuery(".site-header .site-header_cart_link").removeClass("active"),
                jQuery(".myaccount  .dropdown-toggle").removeClass("open"),
                jQuery(".myaccount  .customer_account").slideUp("fast"),
                jQuery(".fixed-cart-wrap").removeClass("active"),
                jQuery("body").removeClass("account-toggle"),
                jQuery("body").removeClass("cart_toggle")
        }),
        jQuery(".myaccount  span.dropdown-toggle").on("click", function (event2) {
            event2.preventDefault(),
                jQuery(".customer_account").slideToggle("fast"),
                jQuery(this).toggleClass("open"),
                jQuery("body").toggleClass("account-toggle"),
                jQuery("body").removeClass("currency-open"),
                jQuery("body").removeClass("language-open"),
                jQuery(".site-header .site-header_cart_link").removeClass("active"),
                jQuery(".fixed-cart-wrap").removeClass("active"),
                jQuery(".currencies.flag-dropdown-menu").slideUp("fast"),
                jQuery(".header_language .disclosure-list").slideUp("fast"),
                jQuery("body").removeClass("search_toggle"),
                jQuery("body").removeClass("cart_toggle")
        }),
        jQuery(".header_language .disclosure__toggle").on("click", function (event2) {
            event2.preventDefault(),
                jQuery(window).width() > 992 && (jQuery(".customer_account").stop(),
                    jQuery("body").toggleClass("language-open"),
                    jQuery("body").removeClass("currency-open"),
                    jQuery("body").removeClass("account-toggle"),
                    jQuery("body").removeClass("myaccount_active"),
                    jQuery("body").removeClass("cart_active"),
                    jQuery(".currency_wrapper").removeClass("active"),
                    jQuery(this).toggleClass("active"),
                    jQuery(".customer_account").slideUp("fast"),
                    jQuery(".disclosure-list").slideToggle("fast"),
                    jQuery(".currencies.flag-dropdown-menu").slideUp("fast"),
                    jQuery(".fixed-cart-wrap").removeClass("active"))
        }),
        $(".header_currency .currency_wrapper.dropdown-toggle").on("click", function (event2) {
            event2.preventDefault(),
                jQuery(window).width() > 992 && (jQuery("body").toggleClass("currency-open"),
                    jQuery("body").removeClass("language-open"),
                    jQuery("body").removeClass("account-toggle"),
                    jQuery("body").removeClass("myaccount_active"),
                    jQuery("body").removeClass("cart_active"),
                    jQuery(".disclosure__toggle").removeClass("active"),
                    $(this).toggleClass("active"),
                    jQuery(".customer_account").slideUp("fast"),
                    jQuery(".header_language .disclosure-list").slideUp("fast"),
                    jQuery(".currencies.flag-dropdown-menu").slideToggle("fast"),
                    jQuery(".fixed-cart-wrap").removeClass("active"))
        });
    var gallery = $(".slider-mobile-gutter .product-grid");
    gallery.owlCarousel({
        items: 6,
        dots: !1,
        loop: !1,
        nav: !0,
        rewind: !1,
        autoplay: !1,
        responsive: {
            100: {
                items: 1
            },
            315: {
                items: 2
            },
            600: {
                items: 3
            },
            992: {
                items: 4
            },
            1300: {
                items: 6
            }
        }
    }),
        jQuery("body").hasClass("disable_menutoggle") ? $("body.disable_menutoggle .slider-specialproduct").owlCarousel({
            items: 1,
            nav: !0,
            autoplay: !0,
            autoplaySpeed: 1500,
            autoplayHoverPause: !0,
            dots: !1,
            responsive: {
                100: {
                    items: 1
                },
                680: {
                    items: 2
                },
                992: {
                    items: 1
                },
                1200: {
                    items: 1
                },
                1700: {
                    items: 1
                }
            }
        }) : $("body .slider-specialproduct").owlCarousel({
            items: 1,
            nav: !0,
            autoplay: !0,
            autoplaySpeed: 1500,
            autoplayHoverPause: !0,
            dots: !1,
            responsive: {
                100: {
                    items: 1
                },
                680: {
                    items: 2
                },
                992: {
                    items: 1
                },
                1200: {
                    items: 1
                },
                1700: {
                    items: 1
                }
            }
        }),
        $(".slider-specialproduct .product-wrapper").each(function () {
            var $desc = $(this).find(".product-description .progress")
                , $qty = $(this).find(".quantity")
                , $pbar = $(this).find(".progress-bar")
                , $progress = $desc
                , $progressBar = $pbar
                , $quantity = $qty.html();
            console.log($quantity);
            var currentWidth = parseInt($progressBar.css("width"))
                , allowedWidth = parseInt($progress.css("width"))
                , addedWidth = currentWidth + parseInt($quantity);
            addedWidth > allowedWidth && (addedWidth = allowedWidth);
            var progress = addedWidth / allowedWidth * 100;
            $progressBar.animate({
                width: progress + "%"
            }, 100)
        }),
        $(".product-single").each(function () {
            var $desc = $(this).find(".product-information .progress")
                , $qty = $(this).find(".quantity")
                , $pbar = $(this).find(".progress-bar")
                , $progress = $desc
                , $progressBar = $pbar
                , $quantity = $qty.html();
            console.log($quantity);
            var currentWidth = parseInt($progressBar.css("width"))
                , allowedWidth = parseInt($progress.css("width"))
                , addedWidth = currentWidth + parseInt($quantity);
            addedWidth > allowedWidth && (addedWidth = allowedWidth);
            var progress = addedWidth / allowedWidth * 100;
            $progressBar.animate({
                width: progress + "%"
            }, 100)
        });
    var banner = $(".product-thumb .slider-nav");
    banner.owlCarousel({
        items: 1,
        dots: !1,
        loop: !0,
        nav: !0,
        rewind: !0,
        autoplay: !0,
        autoplayHoverPause: !0,
        responsive: {
            100: {
                items: 1
            },
            481: {
                items: 1
            },
            992: {
                items: 1
            },
            1200: {
                items: 1
            },
            1300: {
                items: 1
            }
        }
    }),
        $(".cmsblockbanner .ttbanner-wrap").owlCarousel({
            items: 3,
            nav: !1,
            dots: !1,
            loop: !1,
            autoplay: !1,
            responsive: {
                992: {
                    items: 3
                },
                544: {
                    items: 2
                },
                320: {
                    items: 1
                },
                100: {
                    items: 1
                }
            }
        }),
        $(".ttcmsbanner .cms-banner").owlCarousel({
            items: 2,
            nav: !1,
            dots: !1,
            loop: !1,
            autoplay: !1,
            responsive: {
                992: {
                    items: 2
                },
                544: {
                    items: 2
                },
                320: {
                    items: 1
                },
                100: {
                    items: 1
                }
            }
        }),
        $(".block_content .cmsofferblock").owlCarousel({
            items: 7,
            nav: !1,
            dots: !1,
            loop: !1,
            autoplay: !1,
            responsive: {
                992: {
                    items: 7
                },
                544: {
                    items: 2
                },
                320: {
                    items: 1
                },
                100: {
                    items: 1
                }
            }
        }),
        $("body #footer-services .block_content").owlCarousel({
            items: 4,
            nav: !1,
            dots: !0,
            loop: !1,
            autoplay: !1,
            rewindNav: !0,
            responsive: {
                1200: {
                    items: 4
                },
                700: {
                    items: 3
                },
                481: {
                    items: 2
                },
                100: {
                    items: 1
                }
            }
        }),
        $("body #ttcmsservices .block_content").owlCarousel({
            items: 3,
            nav: !1,
            dots: !0,
            loop: !1,
            autoplay: !1,
            rewindNav: !0,
            responsive: {
                1550: {
                    items: 3
                },
                1300: {
                    items: 3
                },
                992: {
                    items: 3
                },
                481: {
                    items: 2
                },
                100: {
                    items: 1
                }
            }
        }),
        $("body .about-services .block_content").owlCarousel({
            items: 3,
            nav: !1,
            dots: !0,
            loop: !1,
            autoplay: !1,
            rewindNav: !0,
            responsive: {
                1200: {
                    items: 3
                },
                992: {
                    items: 3
                },
                481: {
                    items: 2
                },
                100: {
                    items: 1
                }
            }
        }),
        $("body .about-blog").owlCarousel({
            items: 3,
            nav: !1,
            dots: !0,
            loop: !1,
            autoplay: !1,
            rewindNav: !0,
            responsive: {
                1200: {
                    items: 3
                },
                992: {
                    items: 3
                },
                481: {
                    items: 2
                },
                100: {
                    items: 1
                }
            }
        }),
        $("body:not(.rtl) #tt-megamenu .list_product_menu_content").owlCarousel({
            items: 1,
            nav: !1,
            autoPlay: !0,
            autoplaySpeed: 1e3,
            stopOnHover: !1,
            loop: !1,
            dots: !0,
            responsive: {
                768: {
                    items: 1
                },
                360: {
                    items: 1
                },
                100: {
                    items: 1
                }
            }
        }),
        $("body.rtl #tt-megamenu .list_product_menu_content").owlCarousel({
            items: 1,
            nav: !0,
            autoPlay: !0,
            autoplaySpeed: 1e3,
            rtl: !0,
            stopOnHover: !1,
            loop: !1,
            dots: !0,
            responsive: {
                768: {
                    items: 1
                },
                360: {
                    items: 1
                },
                100: {
                    items: 1
                }
            }
        }),
        $("body:not(.rtl) .flash-sale .list_product_menu_content").owlCarousel({
            items: 7,
            nav: !0,
            autoPlay: !0,
            autoplaySpeed: 1e3,
            stopOnHover: !1,
            loop: !1,
            dots: !1,
            responsive: {
                1600: {
                    items: 7
                },
                1300: {
                    items: 6
                },
                1200: {
                    items: 5
                },
                992: {
                    items: 4
                },
                100: {
                    items: 1
                }
            }
        }),
        $("body.rtl .flash-sale .list_product_menu_content").owlCarousel({
            items: 7,
            nav: !0,
            autoPlay: !0,
            autoplaySpeed: 1e3,
            rtl: !0,
            stopOnHover: !1,
            loop: !1,
            dots: !1,
            responsive: {
                1600: {
                    items: 7
                },
                1300: {
                    items: 6
                },
                1200: {
                    items: 5
                },
                992: {
                    items: 4
                },
                100: {
                    items: 1
                }
            }
        }),
        $(".full_gallery_slider.owl-carousel").on("changed.owl.carousel", function (e2) {
            var element = e2.target
                , items = e2.item.count
                , item = e2.relatedTarget.relative(e2.item.index) + 1;
            $(element).parent().find(".num").html(item + "/" + items)
        }),
        $("body:not(.rtl) .full_gallery_slider.owl-carousel").owlCarousel({
            loop: !0,
            startPosition: 0,
            center: !0,
            dots: !0,
            items: 1,
            lazyLoad: !0,
            nav: !1,
            responsive: {
                100: {
                    items: 1
                },
                767: {
                    items: 1
                },
                991: {
                    items: 3
                },
                1199: {
                    items: 3
                },
                1299: {
                    items: 3
                }
            }
        }),
        $("body.rtl .full_gallery_slider.owl-carousel").owlCarousel({
            stagePadding: 200,
            loop: !0,
            startPosition: 0,
            center: !0,
            dots: !0,
            items: 1,
            rtl: !0,
            lazyLoad: !0,
            nav: !1,
            responsive: {
                0: {
                    items: 1,
                    stagePadding: 60
                },
                600: {
                    items: 1,
                    stagePadding: 150
                },
                768: {
                    items: 1,
                    stagePadding: 180
                },
                868: {
                    items: 1,
                    stagePadding: 250
                },
                1800: {
                    items: 1,
                    stagePadding: 300
                }
            }
        }),
        $("body:not(.rtl) .tt-testimonial-wrap .testimonials_wrap").owlCarousel({
            items: 1,
            nav: !0,
            navText: ["<i class='mdi mdi-chevron-left'></i>", "<i class='mdi mdi-chevron-right'></i>"],
            dots: !0,
            loop: !1,
            autoplay: !1,
            autoplayHoverPause: !0,
            responsive: {
                1279: {
                    items: 1
                },
                1250: {
                    items: 1
                },
                600: {
                    items: 1
                }
            }
        }),
        $("body.rtl .tt-testimonial-wrap .testimonials_wrap").owlCarousel({
            items: 1,
            nav: !0,
            navText: ["<i class='mdi mdi-chevron-left'></i>", "<i class='mdi mdi-chevron-right'></i>"],
            rtl: !0,
            dots: !0,
            autoplay: !1,
            loop: !1,
            autoplayHoverPause: !0,
            responsive: {
                1279: {
                    items: 1
                },
                1250: {
                    items: 1
                },
                600: {
                    items: 1
                }
            }
        }),
        $(".product-single").each(function () {
            var $desc = $(this).find(".product-wrapper .progress")
                , $qty = $(this).find(".quantity")
                , $pbar = $(this).find(".progress-bar")
                , $progress = $desc
                , $progressBar = $pbar
                , $quantity = $qty.html()
                , currentWidth = parseInt($progressBar.css("width"))
                , allowedWidth = parseInt($progress.css("width"))
                , addedWidth = currentWidth + parseInt($quantity);
            addedWidth > allowedWidth && (addedWidth = allowedWidth);
            var progress = addedWidth / allowedWidth * 100;
            $progressBar.animate({
                width: progress + "%"
            }, 100)
        });
    var p_col = jQuery(".slider-bestproduct").data("col");
    $("body .slider-bestproduct").owlCarousel({
        items: p_col,
        nav: !0,
        autoplay: !1,
        autoplaySpeed: 1500,
        dots: !1,
        responsive: {
            100: {
                items: 1
            },
            620: {
                items: 2
            },
            1200: {
                items: 3
            },
            1500: {
                items: p_col
            }
        }
    }),
        $(".slider-bestproduct-wrap").each(function () {
            $(this).find(".owl-nav").hasClass("disabled") ? $(this).find(".customNavigation").hide() : $(this).find(".customNavigation").show()
        }),
        $(".slider-bestproduct-wrap .customNavigation .next").click(function () {
            var wrap = $(this).closest(".slider-bestproduct-wrap");
            $(wrap).find(".slider-bestproduct").trigger("next.owl")
        }),
        $(".slider-bestproduct-wrap .customNavigation .prev").click(function () {
            var wrap = $(this).closest(".slider-bestproduct-wrap");
            $(wrap).find(".slider-bestproduct").trigger("prev.owl")
        }),
        $("body:not(.rtl) .category_cms_feature").owlCarousel({
            items: 3,
            nav: !0,
            autoplay: !1,
            autoplaySpeed: 1500,
            dots: !1,
            responsive: {
                100: {
                    items: 1
                },
                320: {
                    items: 1
                },
                481: {
                    items: 2
                },
                992: {
                    items: 3
                },
                1200: {
                    items: 3
                },
                1300: {
                    items: 3
                },
                1460: {
                    items: 3
                }
            }
        }),
        $("body.rtl .category_cms_feature").owlCarousel({
            items: 3,
            nav: !0,
            autoplay: !1,
            autoplaySpeed: 1500,
            dots: !1,
            responsive: {
                100: {
                    items: 1
                },
                320: {
                    items: 1
                },
                481: {
                    items: 2
                },
                992: {
                    items: 3
                },
                1200: {
                    items: 3
                },
                1300: {
                    items: 3
                },
                1460: {
                    items: 3
                }
            }
        }),
        $("body:not(.rtl) .category_feature1").owlCarousel({
            items: 5,
            nav: !1,
            dots: !1,
            autoplay: !0,
            loop: !0,
            rtl: !1,
            autoplayHoverPause: !1,
            lazyLoad: !0,
            smartSpeed: 1e3,
            autoplayTimeout: 3e3,
            responsive: {
                100: {
                    items: 1
                },
                320: {
                    items: 2
                },
                481: {
                    items: 3
                },
                768: {
                    items: 4
                },
                1300: {
                    items: 5
                }
            }
        }),
        $("body.rtl .category_feature1").owlCarousel({
            items: 5,
            nav: !1,
            dots: !1,
            autoplay: !0,
            loop: !0,
            rtl: !1,
            autoplayHoverPause: !1,
            lazyLoad: !0,
            smartSpeed: 1e3,
            autoplayTimeout: 3e3,
            responsive: {
                100: {
                    items: 1
                },
                320: {
                    items: 2
                },
                481: {
                    items: 3
                },
                768: {
                    items: 4
                },
                1300: {
                    items: 5
                }
            }
        }),
        $("body:not(.rtl) .widget_top_rated_products .top-products").owlCarousel({
            items: 1,
            nav: !0,
            dots: !0,
            loop: !1,
            autoplay: !0,
            rtl: !1,
            responsive: {
                1279: {
                    items: 1
                },
                600: {
                    items: 1
                }
            }
        }),
        $("body.rtl .widget_top_rated_products .top-products").owlCarousel({
            items: 1,
            nav: !0,
            dots: !0,
            loop: !1,
            autoplay: !0,
            rtl: !0,
            responsive: {
                1279: {
                    items: 1
                },
                600: {
                    items: 1
                }
            }
        }),
        jQuery(".spr-summary-actions-newreview").on("click", function (e2) {
            e2.preventDefault(),
                jQuery(".spr-content").slideToggle("slow")
        }),
        $(".pro_btn.quick-view-wrap > a,.pro_btn.add_tocart form > a,.pro_btn.add-to-compare .add-in-compare-js").click(function () {
            $(this).addClass("loading"),
                setTimeout(function () {
                    $(".pro_btn.quick-view-wrap > a,.pro_btn.add_tocart form > a, .pro_btn.add-to-compare .add-in-compare-js").removeClass("loading")
                }, 2e3)
        }),
        $(".headerbanner-close").on("click", function () {
            $(".ttcmstopbanner").slideToggle("slow", function () { }),
                jQuery("body").addClass("headerbanner-close")
        }),
        $("body:not(.template-index) .site-header .toggle_menu").hasClass("current-close") && $(".tt-mega_menu").slideUp("2000"),
        $(".toggle_menu").click(function () {
            $(this).hasClass("default-open") && $(window).width() < 992 ? $(this).hasClass("current-close") ? ($(this).addClass("current-open"),
                $("body").addClass("menu-current-open"),
                $(this).removeClass("current-close"),
                $(".tt-mega_menu").slideToggle("2000")) : ($(this).removeClass("default-open"),
                    $(this).addClass("current-open"),
                    $("body").addClass("menu-current-open"),
                    $(this).removeClass("current-close"),
                    $(".tt-mega_menu").slideToggle("2000")) : $(this).hasClass("default-open") ? $(this).hasClass("current-close") ? ($(this).addClass("current-open"),
                        $("body").addClass("menu-current-open"),
                        $(this).removeClass("current-close"),
                        $(".tt-mega_menu").slideToggle("2000")) : $(this).hasClass("current-open") && ($(this).addClass("current-close"),
                            $(this).removeClass("current-open"),
                            $("body").removeClass("menu-current-open"),
                            $(".tt-mega_menu").slideToggle("2000")) : $(this).hasClass("current-open") ? ($(this).addClass("current-close"),
                                $("body").removeClass("menu-current-open"),
                                $(this).removeClass("current-open"),
                                $(".tt-mega_menu").slideToggle("2000")) : ($(this).addClass("current-open"),
                                    $(this).removeClass("current-close"),
                                    $("body").addClass("menu-current-open"),
                                    $(".tt-mega_menu").slideToggle("2000")),
                $(this).hasClass("default-open") && !$(".sticky_header").hasClass("fixed") && ($(this).addClass("current-close"),
                    $(this).removeClass("default-open"),
                    $(".tt-mega_menu").slideToggle("2000")),
                $(this).hasClass("default-open") && $(".sticky_header").hasClass("fixed") && ($(this).addClass("current-open"),
                    $("body").addClass("menu-current-open"),
                    $(this).removeClass("default-open"),
                    $(".tt-mega_menu").slideDown("2000"))
        });
    var gallery = $("#cmsgallery .image-content");
    gallery.owlCarousel({
        items: 5,
        dots: !1,
        loop: !1,
        nav: !0,
        rewind: !1,
        autoplay: !1,
        responsive: {
            100: {
                items: 1
            },
            315: {
                items: 2
            },
            600: {
                items: 3
            },
            992: {
                items: 4
            },
            1300: {
                items: 5
            }
        }
    });
    var prevScrollpos = window.pageYOffset;
    window.onscroll = function (e2) {
        var currentScrollPos = window.pageYOffset;
        prevScrollpos > currentScrollPos ? document.getElementById("header-sticky").style.top = "0" : $("body").hasClass("account-toggle") || $("body").hasClass("menu_hover") || $("body").hasClass("cart_toggle") || $("body").hasClass("search_toggle") || $("body").hasClass("menu-current-open") ? (document.getElementById("header-sticky").style.top = "0",
            e2.stopPropagation()) : document.getElementById("header-sticky").style.top = "-100px",
            prevScrollpos = currentScrollPos
    }
        ,
        jQuery(".product-thumb .grid-view-item__links").each(function () {
            jQuery(this).hoverdir()
        })
});
function moremenu() {
    if (jQuery(document).width() <= 1199)
        var max_elem = 4;
    else
        var max_elem = 5;
    var items = $(".tt_menus_ul1 > li")
        , surplus = items.slice(max_elem, items.length);
    surplus.wrapAll('<li class="more_menu tt_menu_item"><ul class="tt_menus_ul1">'),
        jQuery(".more_menu").prepend('<a href="#" class="level-top topmega-menu-link">More</a>'),
        jQuery(".more_menu").mouseover(function () {
            jQuery(this).children("ul").addClass("shown-link"),
                jQuery("body").addClass("menu-open")
        }),
        jQuery(".more_menu").mouseout(function () {
            jQuery(this).children("ul").removeClass("shown-link"),
                jQuery("body").removeClass("menu-open")
        }),
        $(".tt_menus_ul1").css("display", "inlink-block")
}
jQuery(document).ready(function () {
    moremenu()
}),
    jQuery(window).scroll(function () {
        if (jQuery(document).height() > jQuery(window).height()) {
            var scroll = jQuery(window).scrollTop();
            scroll > 100 ? jQuery("#GotoTop").fadeIn() : jQuery("#GotoTop").fadeOut()
        }
    });
function responsiveMenu() {
    jQuery(window).width() < 992 ? (jQuery(".sub-nav__dropdown").css("display", "none"),
        $(".tthorizontal_menu").appendTo("#tt-megamenu .tt_menus_ul"),
        $(".header_1 .bottom_header_1 .ttresponsive_menu").insertBefore(".header_1 .main-header .header_logo_wrap"),
        $(".header_1 .main-header .right-link-icon").insertAfter(".header_1 .main-header .header_logo_wrap"),
        $(".header_1 .bottom_header_1 .wish-com").insertAfter(".header_1 .main-header .right-link-icon"),
        $(".header_1 .top_header_1 .track-order").insertAfter(".header_1 .main-header #tt-megamenu .tt_menus_ul"),
        $(".header_1 .top_header_1 .topright-header").insertAfter(".header_1 .main-header #tt-megamenu .track-order"),
        $(".header_1 .top_header_1 .flash-sale").insertBefore(".header_1 .main-header #tt-megamenu .topright-header"),
        $(".header_language").appendTo(".main-header .right-link-icon .customer_account"),
        $(".header_currency").appendTo(".main-header .right-link-icon .customer_account"),
        $(".header_2 .bottom_header_2 .ttresponsive_menu").insertBefore(".header_2 .main-header .header_logo_wrap"),
        $(".header_2 .top_header_2 .track-order").insertAfter(".header_2 .main-header #tt-megamenu .tt_menus_ul"),
        $(".header_2 .top_header_2 .topright-header").insertAfter(".header_2 .main-header #tt-megamenu .track-order"),
        $(".header_2 .top_header_2 .flash-sale").insertBefore(".header_2 .main-header #tt-megamenu .topright-header"),
        $(".header_2 .bottom_header_2 .wish-com").insertAfter(".header_2 .main-header .right-link-icon"),
        $(".header_3 .bottom_header_3 .flash-sale").insertAfter(".header_3 .main-header #tt-megamenu .tt_menus_ul"),
        $(".header_3 .main-header .right-link-icon").insertAfter(".header_3 .main-header .header_logo_wrap"),
        $(".header_3 .bottom_header_3 .bottom-content .bottom-right-link").insertAfter(".header_3 .main-header .right-link-icon")) : ($(".header_1 #tt-megamenu .tt_menus_ul .tthorizontal_menu").prependTo(".header_1 .bottom_header_1 .container"),
            $(".header_1 .main-header .ttresponsive_menu").appendTo(".header_1 .bottom_header_1 .container"),
            $(".header_1 .main-header .right-link-icon").insertAfter(".header_1_wrapper .main-header .site-header__search"),
            $(".header_1 .main-header .right-link-icon .customer_account .header_currency,.header_2 .main-header .right-link-icon .customer_account .header_currency").appendTo(".header_1_wrapper .top_header_1 .top-header,.header_2_wrapper .top_header_2 .top-header"),
            $(".header_1 .main-header .right-link-icon .customer_account .header_language,.header_2 .main-header .right-link-icon .customer_account .header_language").appendTo(".header_1_wrapper .top_header_1 .top-header,.header_2_wrapper .top_header_2 .top-header"),
            $(".header_1 .track-order").appendTo(".header_1_wrapper .top_header_1 .top-header"),
            $(".header_1 .topright-header,.header_2 .topright-header").insertAfter(".header_1_wrapper .top_header_1 .top-header,.header_2_wrapper .top_header_2 .top-header"),
            $(".header_1 .flash-sale,.header_2 .flash-sale").insertAfter(".header_1_wrapper .top_header_1 .topright-header,.header_2_wrapper .top_header_2 .topright-header"),
            $(".header_1 .main-header .wish-com").appendTo(".header_1 .bottom_header_1 .container"),
            $(".header_2 .main-header .wish-com").prependTo(".header_2 .bottom_header_2 .container .bottom-right"),
            $(".header_2 #tt-megamenu .tt_menus_ul .tthorizontal_menu").insertAfter(".header_2 .main-header .header_logo_wrap"),
            $(".header_2 .main-header .ttresponsive_menu").prependTo(".header_2 .bottom_header_2 .container"),
            $(".header_2 .track-order").appendTo(".header_2_wrapper .top_header_2 .top-header"),
            $(".header_3 .main-header .right-link-icon").insertAfter(".header_3_wrapper .main-header .site-header__search"),
            $(".header_3 #tt-megamenu .tt_menus_ul .tthorizontal_menu").prependTo(".header_3 .bottom_header_3 .container .bottom-content"),
            $(".header_3 .main-header .bottom-right-link").appendTo(".header_3 .bottom_header_3 .container .bottom-content"),
            $(".header_3 .bottom-right-link").insertAfter(".header_3 .tthorizontal_menu"),
            $(".header_3 .main-header .right-link-icon .customer_account .header_currency").prependTo(".header_3 .bottom_header_3 .container .bottom-right-link"),
            $(".header_3 .main-header .right-link-icon .customer_account .header_language").prependTo(".header_3 .bottom_header_3 .container .bottom-right-link"),
            $(".header_3 .flash-sale").prependTo(".header_3 .bottom_header_3 .container .bottom-right-link"))
}
jQuery(document).ready(function () {
    responsiveMenu(),
        jQuery(".product-write-review").on("click", function (e2) {
            e2.preventDefault(),
                $("a[href='#tab-2']").trigger("click"),
                jQuery("html, body").animate({
                    scrollTop: jQuery(".product_tab_wrapper").offset().top - 150
                }, 1e3)
        })
}),
    jQuery(window).resize(function () {
        responsiveMenu();
        var w_width = $(window).width();
        $(".slider-content-main-wrap").css("width", w_width)
    });
function height() {
    var maxHeight = $(".design_2 .product-block .image,.design_5 .product-block .image,.design_1 .product-single__thumbs a.product-single__thumbnail img,.design_3 .product-single__thumbs a.product-single__thumbnail img").height();
    $(".design_2 .product-block .extra-video,.design_5 .product-block .extra-video,.design_1 .product-single__thumbs a.product-single__thumbnail.extra-video img,.design_3 .product-single__thumbs a.product-single__thumbnail.extra-video img").height(maxHeight),
        $(".design_2 .product-block .video,.design_5 .product-block .video,.design_1 .product-single__thumbs a.product-single__thumbnail.video img,.design_3 .product-single__thumbs a.product-single__thumbnail.video img").height(maxHeight),
        $(".design_2 .product-block .model,.design_5 .product-block .model,.design_1 .product-single__thumbs a.product-single__thumbnail.model img,.design_3 .product-single__thumbs a.product-single__thumbnail.model img").height(maxHeight)
}
$(document).ready(function () {
    height()
}),
    $(window).resize(function () {
        height()
    }),
    $(window).scroll(function () {
        height()
    });
function productcartsticky() {
    jQuery(window).width() > 319 && jQuery(this).scrollTop() > 550 ? jQuery(".add-to-cart-sticky").addClass("fixed") : jQuery(".add-to-cart-sticky").removeClass("fixed")
}
$(document).ready(function () {
    productcartsticky()
}),
    jQuery(window).resize(function () {
        productcartsticky()
    }),
    jQuery(window).scroll(function () {
        productcartsticky()
    });
function footerToggle() {
    jQuery(window).width() < 992 ? (jQuery(".left-sidebar.sidebar").insertAfter(".collection_wrapper"),
        jQuery(".right-sidebar.sidebar .widget > h4,.left-sidebar.sidebar .widget > h4,.blog-section .sidebar .widget > h4").addClass("toggle"),
        jQuery(".right-sidebar.sidebar .widget,.left-sidebar.sidebar .widget,.blog-section .sidebar .widget").children(":nth-child(2)").css("display", "none"),
        jQuery(".right-sidebar.sidebar .widget.active,.left-sidebar.sidebar .widget.active,.blog-section .sidebar .widget.active").children(":nth-child(2)").css("display", "block"),
        jQuery(".right-sidebar.sidebar .widget > h4.toggle,.left-sidebar.sidebar .widget > h4.toggle,.blog-section .sidebar .widget > h4.toggle").unbind("click"),
        jQuery(".right-sidebar.sidebar .widget > h4.toggle,.left-sidebar.sidebar .widget > h4.toggle,.blog-section .sidebar .widget > h4.toggle").on("click", function () {
            jQuery(this).parent().toggleClass("active").children(":nth-child(2)").slideToggle("fast")
        }),
        jQuery(".collection_right .sidebar-block .widget > h4,.collection_left .sidebar-block .widget > h4,.filter-toggle-wrap .sidebar-block .widget > h4").addClass("toggle"),
        jQuery(".collection_right .sidebar-block .widget,.collection_left .sidebar-block .widget,.filter-toggle-wrap .sidebar-block .widget ").children(":nth-child(2)").css("display", "none"),
        jQuery(".collection_right .sidebar-block .widget.active,.collection_left .sidebar-block .widget.active,.filter-toggle-wrap .sidebar-block .widget.active").children(":nth-child(2)").css("display", "block"),
        jQuery(".collection_right .sidebar-block .widget > h4.toggle,.collection_left .sidebar-block .widget > h4.toggle,.filter-toggle-wrap .sidebar-block .widget > h4.toggle").unbind("click"),
        jQuery(".collection_right .sidebar-block .widget > h4.toggle,.collection_left .sidebar-block .widget > h4.toggle,.filter-toggle-wrap .sidebar-block .widget > h4.toggle").on("click", function () {
            jQuery(this).parent().toggleClass("active").children(":nth-child(2)").slideToggle("fast")
        })) : (jQuery(".left-sidebar.sidebar").insertBefore(".collection_wrapper"),
            jQuery(".sidebar .widget > h4,.sidebar-block .widget > h4").unbind("click"),
            jQuery(".sidebar .widget > h4,.sidebar-block .widget > h4").removeClass("toggle"),
            jQuery(".sidebar .widget,.sidebar-block .widget").children(":nth-child(2)").css("display", "block"))
}
jQuery(document).ready(function () {
    footerToggle()
}),
    jQuery(window).resize(function () {
        footerToggle()
    }),
    jQuery(window).load(function () {
        var h = $(".design_3 .product-wrapper-owlslider").height();
        $(".design_3 .product-information-inner.tt-scroll").css("min-height", h + "px")
    }),
    jQuery(window).resize(function () {
        footerToggle();
        var h = $(".design_3 .product-wrapper-owlslider").height();
        $(".design_3 .product-information-inner.tt-scroll").css("min-height", h + "px")
    });
function splitStr(string, seperator) {
    return string.split(seperator)
}
function countDownIni(countdown) {
    $(countdown).each(function () {
        var countdown2 = $(this), promoperiod;
        countdown2.attr("data-promoperiod") ? promoperiod = new Date().getTime() + parseInt(countdown2.attr("data-promoperiod"), 10) : countdown2.attr("data-countdown") && (promoperiod = countdown2.attr("data-countdown")),
            Date.parse(promoperiod) - Date.parse(new Date) > 0 && ($(this).parent(".simple-countdown").addClass("countdown-block"),
                countdown2.countdown(promoperiod, function (event2) {
                    countdown2.html(event2.strftime('<span><span class="left-txt">Left</span><span>%D</span><span class="time-txt">Days</span></span><span><span>%H</span><span class="time-txt">Hrs</span></span><span><span>%M</span><span class="time-txt">Min</span></span><span><span class="second">%S</span><span class="time-txt">Sec</span></span>'))
                }))
    })
}
function hb_animated_contents() {
    $(".hb-animate-element:in-viewport").each(function (i) {
        var $this2 = $(this);
        $this2.hasClass("hb-in-viewport") || setTimeout(function () {
            $this2.addClass("hb-in-viewport")
        }, 180 * i)
    })
}
$(window).scroll(function () {
    hb_animated_contents()
}),
    $(window).load(function () {
        hb_animated_contents()
    });
function sidebarsticky() {
    $(document).width() <= 1199 ? jQuery(".left-sidebar.sidebar,.right-sidebar.sidebar,.collection_right,.collection_left").theiaStickySidebar({
        additionalMarginBottom: 30,
        additionalMarginTop: 30
    }) : $(document).width() >= 1200 && jQuery(".left-sidebar.sidebar,.right-sidebar.sidebar,.collection_right,.collection_left").theiaStickySidebar({
        additionalMarginBottom: 30,
        additionalMarginTop: 130
    })
}
jQuery(document).ready(function () {
    sidebarsticky()
}),
    jQuery(window).resize(function () {
        sidebarsticky()
    }),
    jQuery(function () {
        var Accordion = function (el, multiple) {
            this.el = el || {},
                this.multiple = multiple || !1,
                $(".cat-cnt").addClass("active");
            var links = this.el.find(".cat-cnt .mobile-nav__sublist-trigger");
            links.on("click", {
                el: this.el,
                multiple: this.multiple
            }, this.dropdown)
        };
        Accordion.prototype.dropdown = function (e2) {
            e2.preventDefault();
            var $el = e2.data.el;
            $this = $(this),
                $next = $this.next(),
                $next.slideToggle(),
                $this.parent().toggleClass("open"),
                e2.data.multiple || $el.find(".tt_sub_menu_linklist").not($next).slideUp().parent().removeClass("open")
        }
            ;
        var accordion = new Accordion($(".topcat_content"), !1)
    }),
    jQuery(function () {
        var Accordion = function (el, multiple) {
            this.el = el || {},
                this.multiple = multiple || !1;
            var links = this.el.find(".menu-item-depth-0 .mobile-nav__sublist-trigger");
            links.on("click", {
                el: this.el,
                multiple: this.multiple
            }, this.dropdown)
        };
        Accordion.prototype.dropdown = function (e2) {
            var $el = e2.data.el;
            $this = $(this),
                $next = $this.next(),
                $next.slideToggle(),
                $this.parent().toggleClass("open"),
                e2.data.multiple || $el.find(".menu-item-depth-0 .sub-nav__dropdown").not($next).slideUp().parent().removeClass("open")
        }
            ;
        var accordion = new Accordion($("#accessibleNav"), !1)
    }),
    jQuery(function () {
        if (!$("body").hasClass("fullnav-open")) {
            var Accordion = function (el, multiple) {
                this.el = el || {},
                    this.multiple = multiple || !1;
                var links = this.el.find("li.tt_mm_hassub .mobile-nav__sublist-trigger");
                links.on("click", {
                    el: this.el,
                    multiple: this.multiple
                }, this.dropdown)
            };
            Accordion.prototype.dropdown = function (e2) {
                var $el = e2.data.el;
                $this = $(this),
                    $next = $this.next(),
                    $next.slideToggle(),
                    $this.parent().toggleClass("open"),
                    e2.data.multiple || $el.find("li.tt_mm_hassub .tt_sub_menu_wrap").not($next).slideUp().parent().removeClass("open")
            }
                ;
            var accordion = new Accordion($(".tt_menus_ul"), !1)
        }
    }),
    jQuery(function () {
        var Accordions = function (el, multiple) {
            this.el = el || {},
                this.multiple = multiple || !1;
            var link = this.el.find(".toggle");
            link.on("click", {
                el: this.el,
                multiple: this.multiple
            }, this.dropdown)
        };
        Accordions.prototype.dropdown = function (e2) {
            var $el = e2.data.el;
            $this = $(this),
                $next = $this.next(),
                $next.slideToggle(),
                $this.parent().toggleClass("active"),
                e2.data.multiple || $el.find(".inline-list").not($next).slideUp().parent().removeClass("active")
        }
            ;
        var accordions = new Accordions($(".footer-column"), !1)
    }),
    $("#AddToCart1").click(function () {
        $("#AddToCart").trigger("click")
    }),
    $(window).load(function () {
        $(".add-to-cart-sticky .qty-box-set .qnt_wrap .button").click(function () {
            var valquantity = $(".add-to-cart-sticky .qty-box-set input.quantity").val();
            $(".quantity__input").val(valquantity),
                $(".add-to-cart-sticky .qty-box-set .quantity").val(valquantity)
        }),
            $(".product-information-inner .qty-box-set").click(function () {
                var quantityval = $(".quantity__input").val();
                $(".quantity__input").val(quantityval),
                    $(".add-to-cart-sticky .qty-box-set .quantity").val(quantityval)
            })
    });
//# sourceMappingURL=/s/files/1/0633/2078/5125/t/2/assets/custom-js.js.map?v=27301275130717618181684493947
