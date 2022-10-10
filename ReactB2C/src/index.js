import React from "react";
import ReactDOM from "react-dom";

import {PublicClientApplication} from "@azure/msal-browser";
import {msalConfig} from "./authConfig";

import App from "./App.jsx";

import "bootstrap/dist/css/bootstrap.min.css";
import "./styles/index.css";

import {reportWebVitals} from "./reportWebVitals";


/**
 * MSAL should be instantiated outside of the component tree to prevent it from being re-instantiated on re-renders. 
 * For more, visit: https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-react/docs/getting-started.md
 */
const msalInstance = new PublicClientApplication(msalConfig);

ReactDOM.render (
    <React.StrictMode>
        <App instance={msalInstance}/>
    </React.StrictMode>,
    document.getElementById('root')
);


// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
// reportWebVitals();
