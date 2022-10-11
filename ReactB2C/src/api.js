export const callApiWithToken = async (accessToken, apiEndpoint) => {
    const headers = new Headers();
    const bearer = `Bearer ${accessToken}`;

    console.log("endpoint: " + apiEndpoint);
    headers.append("Authorization", bearer);

    const options = {
        method: "GET",
        headers: headers
    };

    const reponse = await fetch(apiEndpoint, options)
    const json = await reponse.json();

    return json;

}
