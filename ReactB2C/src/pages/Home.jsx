import React, {useState, useEffect} from "react";
import '../styles/App.css';
import {MsalAuthenticationTemplate, useMsal} from "@azure/msal-react";


import {InteractionRequiredAuthError, InteractionStatus, InteractionType} from "@azure/msal-browser";

import {callApiWithToken} from "../api";
import {loginRequest, apiConfig} from "../authConfig";

import {ForecastData} from "../components/ForecastData";



function HomeContent() {

    const [forecastData, setForecastData] = useState([]);
    const {instance, inProgress, accounts} = useMsal();

    useEffect(() => {
        const account = accounts[0] || {};
        if (!forecastData.length && inProgress === InteractionStatus.None) {

            const accessTokenRequest = {
                scopes: apiConfig.apiForecast.b2cScopes,
                account: account

            };
            instance.acquireTokenSilent(accessTokenRequest).then((response) => {
                console.log("Silent token acquisition success");
                let accessToken = response.accessToken;

                console.log("endpoint: " + apiConfig.apiForecast.webApi);
                console.log("Calling API with access token: " + accessToken);
                callApiWithToken(accessToken, apiConfig.apiForecast.webApi).then((apiresponse) => {
                    console.log("API response: " + apiresponse);
                    setForecastData(apiresponse);
                }).catch((error) => {
                    console.log("Error calling API: " + error);
                });
            }).catch((error) => { // in case if silent token acquisition fails, fallback to an interactive method
                if (error instanceof InteractionRequiredAuthError) {
                    if (accounts && inProgress === "none") {
                        instance.acquireTokenPopup({scopes: apiConfig.apiForecast.b2cScopes}).then((response) => {
                            console.log("response.accessToken: " + response.accessToken);
                            callApiWithToken(response.accessToken, apiConfig.apiForecast.webApi).then(response => setForecastData(response));
                            console.log(response.accessToken);
                            console.log(response);
                        }).catch(error => console.log(error));
                    }
                }
            });
        }
    }, [forecastData, inProgress, accounts, instance]);


    return (
        <> {
            forecastData ? <ForecastData forecastData={forecastData}/> : null
        } </>
    );


}
/**
 * The `MsalAuthenticationTemplate` component will render its children if a user is authenticated 
 * or attempt to sign a user in. Just provide it with the interaction type you would like to use 
 * (redirect or popup) and optionally a [request object](https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/request-response-object.md)
 * to be passed to the login API, a component to display while authentication is in progress or a component to display if an error occurs. For more, visit:
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-react/docs/getting-started.md
 */
export const Home = () => {
    const authRequest = {
        ...loginRequest
    };

    return (
        <>
            <MsalAuthenticationTemplate interactionType={

                    InteractionType.Redirect
                }
                authenticationRequest={authRequest}>
                <HomeContent/>
            </MsalAuthenticationTemplate>

        </>
    )
};
