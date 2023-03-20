const createSession = function () {
    return "SessionValueZZjhskigbwiuebfiwbfbwwiuegfwb";
};

const createNonce = function () {
    return "NonceValueZZufrgbeiubvibvibeiurbivbeibveru";
};

const signIn = function () {
    const redirectUri = encodeURIComponent("https://localhost:44357/Home/SignIn");
    const responseType = encodeURIComponent("id_token token");
    const scope = encodeURIComponent("openid ApiOne");

    let authUrl = "/connect/authorize/callback?client_id=client_id_js" +
        `&redirect_uri=${redirectUri}` +
        `&response_type=${responseType}` +
        `&scope=${scope}` +
        `&nonce=${createNonce()}` +
        `&state=${createSession()}`;

    let returnUrl = encodeURIComponent(authUrl);

    console.log(authUrl);
    console.log(returnUrl);

    window.location.href = "https://localhost:44378/Auth/Login?ReturnUrl=" + returnUrl;
};