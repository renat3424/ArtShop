﻿$(document).ready(function () {


    var theForm = $("#theForm");

    theForm.hide();

    var buyButton = $("#buyButton");

    buyButton.on("click", function () {

        console.log("buying item");
    })


    var productInfo = $(".product-props li");
    productInfo.on("click", function () {

        console.log("You clicked on " + $(this).text());
    })


    var loginToggle = $("#loginToggle");
    

    loginToggle.on("click", function () {

        $(".popup-form").slideToggle(1000);
    })

});





