export const authHeader = (contentType = 'application/json') => {
    let headers = {
        'Accept': 'application/json',
        'Content-Type': contentType
    }
    if (localStorage["sessionData"]) {
        headers.Authorization = 'Bearer ' + JSON.parse(localStorage["sessionData"]).token;
    }
    //console.log("authheaders: ", headers);
    return headers;
}
