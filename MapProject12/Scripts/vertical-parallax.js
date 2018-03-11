$(document).ready(function () { //when the document is ready

    windowHeight = $(window).height(); //get the height of the window
    city = windowHeight * 0.675; //create a variable that contains the starting position for bg-city.png
    hills = windowHeight * 0.625; //do the same for bg-hills.png
    mountains = windowHeight * 0.350; //do the same for bg-mountains.png
    sky = 0; //sky starts at the top (0px)

    //change the css of the <html> element to give it multiple backgrounds using CSS3. This contains the variables we just worked out for each individual background
    $('html').css({ "background": "url(/Images/Parallax/bg-city.png) 0 " + city + "px repeat-x, url(/Images/Parallax/bg-hills.png) 0 " + hills + "px repeat-x, url(/Images/Parallax/bg-mountains.png) 0 " + mountains + "px repeat-x, url(/Images/Parallax/bg-sky.png) 0 -" + sky + "px repeat-x #336600" });

    function Move() { //set up a function to be called whenever the window is scrolled or resized
        windowHeight = $(window).height(); //get the height of the window
        pos = $(window).scrollTop(); //get the position of the scrollbar
        city = windowHeight * 0.675 + pos * 1.1; //create a variable that contains the starting position for bg-city.png
        hills = windowHeight * 0.625 + pos * 0.7; //do the same for bg-hills.png
        mountains = windowHeight * 0.350 + pos * 0.51; //do the same for bg-mountains.png
        sky = 0; //keep the sky at the top (0px), it moves naturally with the scroll anyway

        //change the css of the <html> element to give it multiple backgrounds using CSS3. The variables contained will change for every pixel the window is resized or scrolled
        $('html').css({ "background": "url(/Images/Parallax/bg-city.png) 0 " + city + "px repeat-x, url(/Images/Parallax/bg-hills.png) 0 " + hills + "px repeat-x, url(/Images/Parallax/bg-mountains.png) 0 " + mountains + "px repeat-x, url(/Images/Parallax/bg-sky.png) 0 " + sky + "px repeat-x #336600" });
    }

    $(window).resize(function () { //when the window is resized...
        Move(); //call the Move() function
    });

    $(window).bind('scroll', function () { //when the user is scrolling...
        Move(); //call the Move() function
    });

});