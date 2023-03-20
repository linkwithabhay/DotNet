const extractTokens = function (address) {
    let returnValue = address.split("#")[1];
    returnValue.split("&").forEach(keyPair => {
        let key = keyPair.split("=")[0];
        let value = keyPair.split("=")[1];
        localStorage.setItem(key, value);
    });

    window.location.href = "/home/index";
};

extractTokens(window.location.href);