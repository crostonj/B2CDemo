export const callApiWithToken = async (accessToken, apiEndpoint) => {
    const headers = new Headers();
    const bearer = `Bearer ${accessToken}`;

    console.log("endpoint: " + apiEndpoint);
    headers.append("Authorization", bearer);

    const options = {
        method: "GET",
        headers: headers
    };

    return fetch(apiEndpoint, options).then(response => {
        console.log("Success");
        response.json();
    }).then(json => {
        return json
    });

}
