const config = {
    authority: "https://localhost:44378/",
    client_id: "client_id_js",
    redirect_uri: "https://localhost:44357/Home/SignIn",
    post_logout_redirect_uri: "https://localhost:44357/Home/Index",
    //response_type: "id_token token",
    response_type: "code",   // pkce
    scope: "openid random.claim.scope ApiOne",
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage })
};

const userManager = new Oidc.UserManager(config);

userManager.getUser().then(user => {
    console.log("User", user);
    if (user) {
        axios.defaults.headers.common["Authorization"] = `${user["token_type"]} ${user.access_token}`;
    }
});

const signIn = function () {
    userManager.signinRedirect();
};

const signOut = function () {
    userManager.signoutRedirect();
}

const callApi = function () {
    axios.get("https://localhost:44300/secret").then(res => {
        console.log("API response", res);
    });
};

let refreshing = false;

axios.interceptors.response.use(
    (res) => {
        return res;
    },
    (err) => {
        console.log("Axios Error", err.response);

        const axiosConfig = err.response.config;

        // refresh token if status code 401
        if (err.response.status == 401) {

            // if already request sent, do not resend
            if (!refreshing) {
                refreshing = true;

                // do the refresh
                return userManager.signinSilent().then(user => {
                    console.log("Refreshed User", user);

                    // update http request and client
                    axios.defaults.headers.common["Authorization"] = `${user.token_type} ${user.access_token}`;
                    axiosConfig.headers["Authorization"] = `${user.token_type} ${user.access_token}`;

                    refreshing = false;

                    // retry the request
                    return axios(axiosConfig);
                });
            }

        }

        return Promise.reject(err);
    }
);