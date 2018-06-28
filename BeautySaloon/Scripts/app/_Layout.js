var links = ["Services", "Photos", "Contacts"];
for (var i = 0; i < links.length; i++) {
    if (window.location.href.includes(links[i])) {
        document.getElementById(links[i]).classList.add("active");
    }
}