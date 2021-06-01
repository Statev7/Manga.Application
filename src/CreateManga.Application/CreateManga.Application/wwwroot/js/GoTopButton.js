window.onscroll = function () { scroll() };

function scroll() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementById("GoTopBtn").style.display = "block";
    } else {
        document.getElementById("GoTopBtn").style.display = "none";
    }
}

function GoTopFunction() {
    document.documentElement.scrollTop = 0;
}