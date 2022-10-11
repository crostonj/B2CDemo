import "../styles/App.css";
import {apiConfig} from "../authConfig";

export const ForecastData = (props) => {
    const tableRows = Object.entries(props.forecastData).map((entry, index) => {
        return (<tr key={index}>
            <td><b>{entry[0]}:</b></td>
            <td> {entry[1]}</td></tr>)
    });

    return (<>
        <div className="data-area-div">
            <p>Calling
                <strong>custom protected web API</strong>...</p>
            <ul>
                <li>
                    <strong>endpoint:</strong>
                    <mark> {
                        apiConfig.apiForecast.webApi
                    }</mark>
                </li>
                <li>
                    <strong>scope:</strong>
                    <mark> {apiConfig.apiForecast.b2cScopes[0]}</mark>
                </li>
            </ul>
            <p>Contents of the<strong>response</strong>is below:</p>
        </div>
        <div className="data-area-div">
            <table>
                <thead></thead>
                <tbody>{tableRows}</tbody>
            </table>
        </div>
    </>);
}
