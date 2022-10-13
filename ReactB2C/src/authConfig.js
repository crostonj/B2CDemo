import {LogLevel} from "@azure/msal-browser";

/**
 * To learn more about user flows, visit: https://docs.microsoft.com/en-us/azure/active-directory-b2c/user-flow-overview
 * To learn more about custom policies, visit: https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-overview
 */
export const b2cPolicies = {
    names: {
        signUpSignIn: "B2C_1_Signin",
        forgotPassword: "B2C_1_Reset",
        editProfile: "B2C_1_Profile"
    },
    authorities: {
        signUpSignIn: {
            authority: "https://3cloudcroston.b2clogin.com/3cloudcroston.onmicrosoft.com/B2C_1_Signin"

        },
        forgotPassword: {
            authority: "https://3cloudcroston.b2clogin.com/3cloudcroston.onmicrosoft.com/B2C_1_Reset"

        },
        editProfile: {
            authority: "https://3cloudcroston.b2clogin.com/3cloudcroston.onmicrosoft.com/B2C_1_Profile"

        }
    },
    authorityDomain: "3cloudcroston.b2clogin.com"
}

/**
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/configuration.md 
 */
export const msalConfig = {
    auth: {
        clientId: "8268e6d5-6634-4815-9c1d-105b620b70b5",
        authority: b2cPolicies.authorities.signUpSignIn.authority,
        knownAuthorities: [b2cPolicies.authorityDomain],
        redirectUri: "/",
        postLogoutRedirectUri: "/",
        navigateToLoginRequestUrl: false
    },
    cache: {
        cacheLocation: "sessionStorage",
        storeAuthStateInCookie: false
    },
    system: {
        loggerOptions: {
            loggerCallback: (level, message, containsPii) => {
                if (containsPii) {
                    return;
                }
                switch (level) {
                    case LogLevel.Error:
                        console.error(message);
                        return;
                    case LogLevel.Info:
                        console.info(message);
                        return;
                    case LogLevel.Verbose:
                        console.debug(message);
                        return;
                    case LogLevel.Warning:
                        console.warn(message);
                        return;
                    default:
                        return;
                }
            }
        }
    }
};

export const apiConfig = {
    apiForecast: {
        b2cScopes: ["https://3cloudcroston.onmicrosoft.com/8268e6d5-6634-4815-9c1d-105b620b70b5/Read.Weather"],
        webApi: "https://localhost:44332/api/WeatherForecast/Get"

    }
};


/**
 * https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-permissions-and-consent#openid-connect-scopes
 */
export const loginRequest = {
    scopes: [...apiConfig.apiForecast.b2cScopes]
};
