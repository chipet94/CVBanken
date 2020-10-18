export const authHeader = (contentType = 'application/json') => {
    let headers = {
        'Accept': 'application/json',
        'Content-Type': contentType
    }
    if (localStorage["userData"]) {
        headers.Authorization = 'Bearer ' + JSON.parse(localStorage["userData"]).token;
    }
    //console.log("authheaders: ", headers);
    return headers;
}
