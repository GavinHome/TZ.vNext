import fetchIntercept from 'fetch-intercept';
import StoreCache from "../components/common/TzStoreCache";

export default {
    register: function () {
        fetchIntercept.register({
            request: function (url, config) {
                // Modify the url or config here
                var withDefaults = Object.assign({}, config);

                withDefaults.headers = withDefaults.headers || new Headers({});

                var storeCache = new StoreCache('auth');
                if (storeCache.get('token')) {
                    withDefaults.headers['Authorization'] = 'Bearer ' + storeCache.get('token')
                }

                return [url, withDefaults];
            },

            requestError: function (error) {
                // Called when an error occurred during another 'request' interceptor call
                requestError(error);
                return Promise.reject(error);
            },

            response: function (response) {
                // Modify the response object
                return response;
            },

            responseError: function (error) {
                // Handle an fetch error
                responseError(error);

                return Promise.reject(error);
            }
        })
    }
};

//403 500 401 400
function requestError(error) {
    if (error === "401") {
        clearAuth();
    }
}

//403 500 401 400
function responseError(error) {
    requestError(error);
}

function clearAuth() {
    const cache = new StoreCache("auth");
    cache.clear();
    window.location.href = "/";
}
