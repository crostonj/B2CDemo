import "../styles/App.css";
import {apiConfig} from "../authConfig";

export const ForecastData = (props) => {
    const tableRows = props.forecastData.map((item, index) => {
        return ([<tr key={index}>
            <td><b>{item.city}:</b></td>
            <td> {item.country}</td>
            <td> {item.date}</td>
            <td> {item.summary}</td>
            <td> {item.temperatureC}</td>
            <td> {item.temperatureF}</td></tr>])
    });

    return (<>
        <div className="data-area-div">
            <p>Calling
                <strong> custom protected web API</strong>...</p>
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
            <p>Contents of the <strong>response</strong> is below: </p>
        </div>
        <div className="data-area-div">
            <table>
                <thead><tr><th>City</th><th>Country</th><th>Date</th><th>Summary</th><th>Temp C</th><th>Temp F</th></tr></thead>
                <tbody>{tableRows}</tbody>
            </table>
        </div>
    </>);
}
