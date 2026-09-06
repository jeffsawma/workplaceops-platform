import { useEffect, useState } from 'react'; // useState => stores businesses + loading status, useEffect => fetches businesses from API
import './App.css';

function App() {
    const [businesses, setBusinesses] = useState([]); // Creates a state variable whose initial value is an empty array
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function loadBusinesses() { // Defining a funcion that fetches businesses from the API and updates the state
            try {
                const response = await fetch('http://localhost:5158/api/Businesses');

                if (!response.ok) {
                    throw new Error('Failed to load businesses.');
                }

                const data = await response.json(); // Converts the JSON response into a JavaScript object
                setBusinesses(data); // Updates the businesses state variable with the fetched data
            } catch (error) { // If any error occurs during the fetch operation, it will be caught here
                console.error(error); // Logs the error to the console in the browser for debugging purposes // To be improved later with a user-friendly error message displayed in the UI
            } finally {
                setLoading(false); // Sets loading to false after the fetch operation is complete, regardless of success or failure
            }
        }

        loadBusinesses(); // Calls the loadBusinesses function to initiate the fetch operation when the component mounts
    }, []); // Empty dependency array means this effect runs once when App loads for the first time

    if (loading) {
        return <p>Loading businesses...</p>; // If loading === true, display a loading message to the user while the fetch operation is in progress
    }

    return ( // This is the main JSX that will display after loading is complete
        <main>
            <h1>WorkplaceOps</h1>

            <h2>Businesses</h2>

            {businesses.length === 0 ? (
                <p>No businesses found.</p>
            ) : (
                <ul>
                    {businesses.map((business) => (
                        <li key={business.id}>
                            <strong>{business.legalName}</strong>
                            {' — '}
                            {business.employeeCount} employees
                        </li>
                    ))}
                </ul>
            )}
        </main>
    );
}

export default App; // For importing the App component into other parts of the application

// Replacing the current Vite React template with a custom App component that fetches and displays a list of businesses from an API
