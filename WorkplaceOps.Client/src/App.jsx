import { useEffect, useState } from 'react'; // useState => stores businesses + loading status, useEffect => fetches businesses from API
import './App.css';

function App() { // Defines the main App component that will be rendered in the browser
    const [businesses, setBusinesses] = useState([]); // Creates a state variable whose initial value is an empty array
    const [loading, setLoading] = useState(true); // Creates a state variable whose initial value is true, indicating that the data is being loaded

    // Form input state for creating a new business
    const [legalName, setLegalName] = useState('');
    const [operatingName, setOperatingName] = useState('');
    const [quebecEnterpriseNumber, setQuebecEnterpriseNumber] = useState('');
    const [employeeCount, setEmployeeCount] = useState('');

    // Form validation and general error state
    const [validationErrors, setValidationErrors] = useState({});
    const [generalError, setGeneralError] = useState('');

    // Form submission state
    const [submitting, setSubmitting] = useState(false);

    // Selected business and business details state
    const [selectedBusiness, setSelectedBusiness] = useState(null);
    const [detailsLoading, setDetailsLoading] = useState(false);
    const [detailsError, setDetailsError] = useState('');


    async function handleSubmit(event) {  // Next we will make the form actually submit to the backend API
        event.preventDefault();

        setValidationErrors({});
        setGeneralError('');
        setSubmitting(true);

        const newBusiness = {
            legalName,
            operatingName,
            quebecEnterpriseNumber,
            employeeCount: Number(employeeCount)
        };

        try {
            const response = await fetch('http://localhost:5158/api/Businesses', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newBusiness)
            });

            if (!response.ok) {
                const errorData = await response.json();

                if (errorData.errors) {
                    setValidationErrors(errorData.errors);
                    return;
                }

                throw new Error('Failed to create business.');
            }

            const createdBusiness = await response.json();

            setBusinesses((currentBusinesses) => [
                createdBusiness,
                ...currentBusinesses
            ]);

            setLegalName('');
            setOperatingName('');
            setQuebecEnterpriseNumber('');
            setEmployeeCount('');
        } catch (error) {
            console.error(error);
            setGeneralError(error.message);
        } finally {
            setSubmitting(false);
        }
    }

    
    async function loadBusinessDetails(id) { // We will make the businesses inside the list clickable and with that we will display their details
        setDetailsLoading(true);
        setDetailsError('');

        try {
            const response = await fetch(`http://localhost:5158/api/Businesses/${id}`
            );

            if (!response.ok) {
                throw new Error('Failed to load business details.');
            }

            const data = await response.json();
            setSelectedBusiness(data);
        } catch (error) {
            console.error(error);
            setDetailsError(error.message);
        } finally {
            setDetailsLoading(false);
        }
    }

    useEffect(() => {
        async function loadBusinesses() { // Defining a funcion that fetches businesses from the API and updates the state
            try {
                const response = await fetch('http://localhost:5158/api/Businesses');

                if (!response.ok) {
                    throw new Error('Failed to load businesses.');
                }

                const data = await response.json();
                setBusinesses(data);
            } catch (error) {
                console.error(error); 
            } finally {
                setLoading(false);
            }
        }

        loadBusinesses(); // Calls the loadBusinesses function to initiate the fetch operation when the component mounts
    }, []); // Empty dependency array means this effect runs once when App loads for the first time

    if (loading) {
        return <p>Loading businesses...</p>; 
    }

    return ( // This is the main JSX that will display after loading is complete
        <main>
            <h1 style={{ fontWeight: 'bold' }}>WorkplaceOps</h1>
            <br />

            <form onSubmit={handleSubmit}>
                <h2>Create Business:</h2>
                <br />

                <div>
                    <label htmlFor="legalName">Legal name </label>
                    <input
                        id="legalName"
                        type="text"
                        value={legalName}
                        onChange={(event) => setLegalName(event.target.value)}
                    />
                    {validationErrors.LegalName && (
                        <p className="validation-error">
                            {validationErrors.LegalName[0]}
                        </p>
                    )}
                </div>

                <div>
                    <label htmlFor="operatingName">Operating name </label>
                    <input
                        id="operatingName"
                        type="text"
                        value={operatingName}
                        onChange={(event) => setOperatingName(event.target.value)}
                    />
                </div>

                <div>
                    <label htmlFor="quebecEnterpriseNumber">Quebec Enterprise Number </label>
                    <input
                        id="quebecEnterpriseNumber"
                        type="text"
                        value={quebecEnterpriseNumber}
                        onChange={(event) => setQuebecEnterpriseNumber(event.target.value)}
                    />
                </div>

                <div>
                    <label htmlFor="employeeCount">Employee count</label>
                    <input
                        id="employeeCount"
                        type="number"
                        value={employeeCount}
                        onChange={(event) => setEmployeeCount(event.target.value)}
                    />
                    {validationErrors.EmployeeCount && (
                        <p className="validation-error">
                            {validationErrors.EmployeeCount[0]}
                        </p>
                    )}
                </div>

                <button type="submit" disabled={submitting}>
                    {submitting ? 'Creating...' : 'Create Business'}
                </button>
                
                {generalError && (
                    <p className="general-error">{generalError}</p>
                )}
            </form>
            <br />

            {/* List of Businesses */}
            <h2>Businesses:</h2>
            <br />

            {businesses.length === 0 ? (
                <p>No businesses found.</p>
            ) : (
                <ul>
                    {businesses.map((business) => (
                        <li key={business.id}>
                            <button
                                type="button"
                                className={
                                    selectedBusiness?.id === business.id
                                        ? 'business-button selected'
                                        : 'business-button'
                                }
                                onClick={() => loadBusinessDetails(business.id)}
                            >
                                <strong>{business.legalName}</strong>
                                {' — '}
                                {business.employeeCount} employees
                            </button>
                        </li>
                    ))}
                </ul>
            )}
            
            {/* Business details */}
            {detailsLoading && (
                <p>Loading business details...</p>
            )}

            {detailsError && (
                <p className="details-error">{detailsError}</p>
            )}

            {selectedBusiness && (
                <section className="business-details">
                    <h2>Business Details:</h2>
                    <br />

                    <div>
                        <strong>Legal name:</strong>
                        <span>{selectedBusiness.legalName}</span>
                    </div>

                    <div>
                        <strong>Operating name:</strong>
                        <span>{selectedBusiness.operatingName || 'N/A'}</span>
                    </div>

                    <div>
                        <strong>Quebec Enterprise Number:</strong>
                        <span>{selectedBusiness.quebecEnterpriseNumber || 'N/A'}</span>
                    </div>

                    <div>
                        <strong>Employee count:</strong>
                        <span>{selectedBusiness.employeeCount}</span>
                    </div>

                    <div>
                        <strong>Created at:</strong>
                        <span>
                            {new Date(`${selectedBusiness.createdAtUtc}Z`).toLocaleString()}
                        </span>

                    </div>
                </section>
            )}
        </main>
    );
}

export default App; // For importing the App component into other parts of the application

